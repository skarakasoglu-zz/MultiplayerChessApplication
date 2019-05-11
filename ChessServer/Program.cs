using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.Threading;
using System.Net;
using ChessClient;
using static ChessClient.ClientServerCommunication;

namespace ChessServer
{
    class Program
    {
        public static List<Handler> handlers = new List<Handler>();
        public static List<GameHandler> games = new List<GameHandler>();
        static object Lock = new object();
        static void Main(string[] args)
        {
            string ipAddress = "127.0.0.1";
            int port = 1453;
            TcpListener serverSocket = new TcpListener(IPAddress.Parse(ipAddress), port);
            serverSocket.Start();
            Console.WriteLine("Server is running now. Listening from port " + port);

            while (true)
            {
                TcpClient clientSocket = serverSocket.AcceptTcpClient();
                Console.WriteLine("A connection established with a client which has the ip " + clientSocket.Client.RemoteEndPoint);
                handlers.Add(new Handler(clientSocket));
            }

        }

        public void removeSession(GameHandler hndler)
        {
            lock (Lock)
            {
                Program.games.Remove(hndler);
            }
        }

    }

    class Handler
    {
        TcpClient clientSocket;
        NetworkStream stream;
        Thread thread;
        public UserViewModel currentUser;
        private GameHandler currentGame;
        private bool disconnect;

        public Handler(TcpClient clientSocket)
        {
            disconnect = false;
            this.clientSocket = clientSocket;
            thread = new Thread(listenFromClient);
            thread.Start();
        }

        void listenFromClient()
        {

            try
            {
                stream = clientSocket.GetStream();
                ChessDBEntities db = new ChessDBEntities();

                while (true)
                {
                    byte[] serializedObject = new byte[clientSocket.ReceiveBufferSize];
                    int bytesRead = stream.Read(serializedObject, 0, serializedObject.Length);

                    ClientServerCommunication csc = ClientServerCommunication.Deserialize<ClientServerCommunication>(serializedObject);
                    switch (csc.Type)
                    {
                        case CommunicationType.Logout:
                            foreach (var handler in Program.handlers)
                            {
                                if (currentUser.OnlineFriends.Contains(handler.currentUser)) handler.friendGotOffline(currentUser);
                                disconnect = true;
                            }
                            break;
                        case CommunicationType.Login:
                            currentUser = (UserViewModel)csc.Content;
                            int count = 0;
                            foreach (Handler hnd in Program.handlers)
                            {
                                if (currentUser.Equals(hnd.currentUser))
                                {
                                    count++;
                                    if (count == 2)
                                    {
                                        csc = new ClientServerCommunication(CommunicationType.InSuspence);
                                        disconnect = true;
                                        break;
                                    }
                                }
                            }
                            if (count < 2)
                            {
                                var login = db.Users.SingleOrDefault(u => u.UserID == currentUser.UserID && u.Password == currentUser.Password);

                                csc = new ClientServerCommunication(CommunicationType.Login);
                                if (login == null)
                                {
                                    csc.Content = null;
                                    disconnect = true;
                                    Disconnect();
                                }
                                else
                                {
                                    currentUser.FullName = login.FullName;
                                    var friends = (from friendship in db.Friendships
                                                   join user in db.Users on 1 equals 1
                                                   where ((friendship.ReceiverUserID == user.UserID || friendship.SenderUserID == user.UserID) && friendship.IsAccepted == true &&
                                                   (friendship.ReceiverUserID == currentUser.UserID || friendship.SenderUserID == currentUser.UserID))
                                                   select new UserViewModel
                                                   {
                                                       UserID = user.UserID,
                                                       FullName = user.FullName
                                                   }).ToList();

                                    foreach (var handler in Program.handlers)
                                    {
                                        if (friends.Contains(handler.currentUser) && handler.currentUser.UserID != currentUser.UserID)
                                        {
                                            currentUser.OnlineFriends.Add(new UserViewModel { UserID = handler.currentUser.UserID, FullName = handler.currentUser.FullName });
                                            handler.friendGotOnline(currentUser);
                                        }
                                    }

                                    foreach (var friend in friends)
                                    {
                                        if (!currentUser.OnlineFriends.Contains(friend) && currentUser.UserID != friend.UserID)
                                            currentUser.OfflineFriends.Add(new UserViewModel { UserID = friend.UserID, FullName = friend.FullName });
                                    }

                                    csc.Content = currentUser;
                                }
                            }
                            byte[] bytesToSend = ClientServerCommunication.SerializeToByteArray(csc);
                            stream.Write(bytesToSend, 0, bytesToSend.Length);
                            if (disconnect) Disconnect();
                            break;
                        case CommunicationType.SendMessage:
                            MessageViewModel msg = (MessageViewModel)csc.Content;

                            foreach (Handler handler in Program.handlers)
                            {
                                if (handler.currentUser.UserID == msg.DestinationID) handler.getMessage(msg);
                            }

                            break;
                        case CommunicationType.CreateARoom:
                            GameSession session = (GameSession)csc.Content;
                            if (currentGame != null) csc.Content = null;
                            else
                            {
                                GameHandler gameHandler = new GameHandler(session.sessionName);
                                foreach (var item in session.players)
                                {
                                    ChessPiece.PieceColor colorP = item.Key == ChessClient.ChessPiece.PieceColor.Black ? ChessPiece.PieceColor.Black : ChessPiece.PieceColor.White;
                                    gameHandler.players.Add(colorP, item.Value);
                                }
                                currentGame = gameHandler;
                                csc.Content = new GameSession { sessionID = gameHandler.sessionID, players = gameHandler.players, sessionName = gameHandler.sessionName };
                                Program.games.Add(gameHandler);
                            }
                            byte[] sendBytes = ClientServerCommunication.SerializeToByteArray(csc);
                            stream.Write(sendBytes, 0, sendBytes.Length);
                            break;
                        case CommunicationType.GetRooms:
                            List<GameSession> sessions = new List<GameSession>();
                            foreach (GameHandler item in Program.games)
                            {
                                GameSession newSes = new GameSession
                                {
                                    sessionID = item.sessionID,
                                    sessionName = item.sessionName
                                };
                                foreach (var s in item.players)
                                {
                                    ChessClient.ChessPiece.PieceColor colorPC = s.Key == ChessPiece.PieceColor.Black ?
                                        ChessClient.ChessPiece.PieceColor.Black : ChessClient.ChessPiece.PieceColor.White;
                                    newSes.players.Add(colorPC, s.Value);
                                }
                                sessions.Add(newSes);

                            }
                            csc.Content = sessions;
                            byte[] bytesToWrite = ClientServerCommunication.SerializeToByteArray(csc);
                            stream.Write(bytesToWrite, 0, bytesToWrite.Length);
                            break;
                        case CommunicationType.JoinRoom:
                            GameSession sesss = csc.Content as GameSession;
                            if (currentGame != null) sesss = null;
                            else
                            {
                                int index = Program.games.IndexOf(new GameHandler(sesss.sessionName) { sessionID = sesss.sessionID });
                                if (index > -1)
                                {
                                    currentGame = Program.games[index];
                                    if (currentGame.players[ChessPiece.PieceColor.Black] != null &&
                                        currentGame.players[ChessPiece.PieceColor.White] != null)
                                    {
                                        sesss = null;
                                    }
                                    else if (currentGame.players[ChessPiece.PieceColor.Black] == null)
                                    {
                                        currentGame.players[ChessPiece.PieceColor.Black] = currentUser;
                                        sesss.players[ChessPiece.PieceColor.Black] = currentUser;
                                        foreach (var item in Program.handlers)
                                        {
                                            if (currentGame.players[ChessPiece.PieceColor.White].UserID == item.currentUser.UserID)
                                            {
                                                sesss.players[ChessPiece.PieceColor.White] = item.currentUser;
                                                item.gameStart(sesss);
                                                break;
                                            }
                                        }
                                    }
                                    else
                                    {
                                        currentGame.players[ChessPiece.PieceColor.White] = currentUser;
                                        sesss.players[ChessPiece.PieceColor.White] = currentUser;
                                        foreach (var item in Program.handlers)
                                        {
                                            if (currentGame.players[ChessPiece.PieceColor.Black].UserID == item.currentUser.UserID)
                                            {
                                                sesss.players[ChessPiece.PieceColor.Black] = item.currentUser;
                                                item.gameStart(sesss);
                                                break;
                                            }
                                        }

                                    }
                                }
                            }
                            csc.Content = sesss;
                            byte[] bytesSess = ClientServerCommunication.SerializeToByteArray(csc);
                            stream.Write(bytesSess, 0, bytesSess.Length);
                            break;
                        case CommunicationType.Move:
                            ChessPieceServer piece = (ChessPieceServer)csc.Content;
                            string previousKey = null;
                            string currentKey;
                            currentKey = piece.key;

                            foreach (var item in currentGame.chessBoard)
                            {
                                if (item.Value != null)
                                {
                                    if (item.Value.Type == piece.Type && item.Value.Color == piece.Color)
                                    {
                                        previousKey = item.Key;
                                        break;
                                    }
                                }
                            }
                            currentGame.chessBoard[currentKey] = piece;
                            currentGame.chessBoard[previousKey] = null;

                            ChessPiece.PieceColor color = piece.Color == ChessPiece.PieceColor.Black ? ChessPiece.PieceColor.White : ChessPiece.PieceColor.Black;

                            foreach (var item in Program.handlers)
                            {
                                if (currentGame.players[color].UserID == item.currentUser.UserID)
                                {
                                    item.opponentsMovement(piece);
                                }
                            }
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            finally
            {
                Disconnect();
            }
        }

        public void gameStart(GameSession session)
        {

            ClientServerCommunication csc = new ClientServerCommunication(CommunicationType.GameStart);
            csc.Content = session;
            byte[] bytesToSend = ClientServerCommunication.SerializeToByteArray(csc);
            stream.Write(bytesToSend, 0, bytesToSend.Length);
        }

        public void opponentsMovement(ChessPieceServer piece)
        {
            try
            {
                ClientServerCommunication csc = new ClientServerCommunication(CommunicationType.Move);
                csc.Content = piece;
                byte[] bytesToSend = ClientServerCommunication.SerializeToByteArray(csc);
                stream.Write(bytesToSend, 0, bytesToSend.Length);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public void friendGotOnline(UserViewModel friend)
        {
            try
            {
                ClientServerCommunication csc = new ClientServerCommunication(CommunicationType.FriendGotOnline);
                csc.Content = friend;
                currentUser.OfflineFriends.Remove(friend);
                currentUser.OnlineFriends.Add(friend);
                byte[] bytesToSend = ClientServerCommunication.SerializeToByteArray(csc);
                stream.Write(bytesToSend, 0, bytesToSend.Length);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public void friendGotOffline(UserViewModel friend)
        {
            try
            {
                ClientServerCommunication csc = new ClientServerCommunication(CommunicationType.FriendGotOffline);
                csc.Content = friend;
                currentUser.OnlineFriends.Remove(friend);
                currentUser.OfflineFriends.Add(friend);
                byte[] bytesToSend = ClientServerCommunication.SerializeToByteArray(csc);
                stream.Write(bytesToSend, 0, bytesToSend.Length);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public void getMessage(MessageViewModel msg)
        {
            try
            {
                ClientServerCommunication csc = new ClientServerCommunication(CommunicationType.SendMessage);
                csc.Content = msg;
                byte[] bytesToSend = ClientServerCommunication.SerializeToByteArray(csc);
                stream.Write(bytesToSend, 0, bytesToSend.Length);
                ChessDBEntities db = new ChessDBEntities();
                /*db.Messages.Add(new Message { Content = msg.Content, SourceID = msg.SourceID, DestinationID = msg.DestinationID });
                db.SaveChanges();*/
            }
            catch (Exception)
            {

                throw;
            }
        }

        private void Disconnect()
        {
            new DisconnectedException(clientSocket.Client.RemoteEndPoint.ToString());

            stream.Close();
            clientSocket.Close();
            thread.Abort();
            Program.handlers.Remove(this);
        }

    }

    class DisconnectedException
    {
        public DisconnectedException(string ipAddress)
        {
            Console.WriteLine("Client with the ip " + ipAddress + " disconnected from the server.");
        }
    }
}
