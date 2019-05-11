using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.Threading;
using System.Windows.Forms;

namespace ChessClient
{
    public partial class Client : Form
    {
        public UserViewModel CurrentUser { get; set; }

        private Thread thread;
        private TcpClient clientSocket;
        private NetworkStream stream;
        private ClientForm cf;
        private GameForm gf;
        private GameSession currentSession;

        public Client()
        {
            InitializeComponent();
            CheckForIllegalCrossThreadCalls = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Connect(txtUserID.Text, txtPassword.Text);
        }

        public void Connect(string UserName, string Password)
        {
            clientSocket = new TcpClient();
            clientSocket.Connect("127.0.0.1", 1453);

            stream = clientSocket.GetStream();

            sendToServer(new UserViewModel { UserID = UserName, Password = Password }, ClientServerCommunication.CommunicationType.Login);

            thread = new Thread(listenFromServer);
            thread.Start();
        }

        public void SendMessage(MessageViewModel msg)
        {
            sendToServer(msg, ClientServerCommunication.CommunicationType.SendMessage);
        }

        public void CreateARoom(GameSession session)
        {
            sendToServer(session, ClientServerCommunication.CommunicationType.CreateARoom);
        }

        public void JoinRoom(string sessionID)
        {
            sendToServer(new GameSession { sessionID = sessionID }, ClientServerCommunication.CommunicationType.JoinRoom);
        }

        public void getRooms()
        {
            sendToServer(null, ClientServerCommunication.CommunicationType.GetRooms);
        }

        private void listenFromServer()
        {
            try
            {
                stream = clientSocket.GetStream();

                while (true)
                {
                    ClientServerCommunication csc = obtainFromServer();

                    switch (csc.Type)
                    {
                        case ClientServerCommunication.CommunicationType.Login:
                            UserViewModel user = csc.Content as UserViewModel;

                            if (user == null) MessageBox.Show("Failed to login");
                            else
                            {
                                CurrentUser = user;
                                BeginInvoke((Action)delegate
                                {
                                    cf = new ClientForm(this);
                                    cf.Show();
                                });
                                Hide();
                            }
                            break;
                        case ClientServerCommunication.CommunicationType.InSuspence:
                            MessageBox.Show("Your account is in suspense.");
                            break;
                        case ClientServerCommunication.CommunicationType.FriendGotOnline:
                            UserViewModel onFriend = csc.Content as UserViewModel;
                            cf.BeginInvoke((Action)delegate
                            {
                                cf.friendGotOnline(onFriend);
                            });
                            break;
                        case ClientServerCommunication.CommunicationType.FriendGotOffline:
                            UserViewModel ofFriend = csc.Content as UserViewModel;
                            cf.BeginInvoke((Action)delegate
                            {
                                cf.friendGotOffline(ofFriend);
                            });
                            break;
                        case ClientServerCommunication.CommunicationType.SendMessage:
                            MessageViewModel msg = csc.Content as MessageViewModel;

                            cf.BeginInvoke((Action)delegate
                            {
                                cf.getMessage(msg);
                            });

                            break;
                        case ClientServerCommunication.CommunicationType.CreateARoom:
                            if (csc.Content == null)
                            {
                                cf.BeginInvoke((Action)delegate
                                {
                                    cf.FailedToCreate();
                                });
                            }
                            else
                            {
                                currentSession = (GameSession)csc.Content;
                                cf.BeginInvoke((Action)delegate
                                {
                                    cf.roomCreated(currentSession);
                                });
                            }
                            break;
                        case ClientServerCommunication.CommunicationType.GetRooms:
                            List<GameSession> sessions = (List<GameSession>)csc.Content;

                            cf.BeginInvoke((Action)delegate
                            {
                                cf.ListRooms(sessions);
                            });
                            break;
                        case ClientServerCommunication.CommunicationType.JoinRoom:
                            currentSession = csc.Content as GameSession;
                            if (currentSession == null)
                            {
                                MessageBox.Show("You can't join the session. You are in a game or the lobby is full.");
                            }
                            else
                            {
                                gf = null;
                                BeginInvoke((Action)delegate
                                {
                                    gf = new GameForm(currentSession, this);
                                    gf.Show();
                                });

                            }

                            break;
                        case ClientServerCommunication.CommunicationType.Move:
                            ChessPieceServer piece = (ChessPieceServer)csc.Content;
                            gf.BeginInvoke((Action)delegate
                            {
                                gf.opponentsMovement(piece);
                            });

                            break;
                        case ClientServerCommunication.CommunicationType.GameStart:
                            currentSession = (GameSession)csc.Content;
                            BeginInvoke((Action)delegate
                            {
                                gf = new GameForm(currentSession, this);
                                gf.Show();
                            });
                            break;
                    }

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void sendToServer(object content, ClientServerCommunication.CommunicationType type)
        {

            ClientServerCommunication csc = new ClientServerCommunication(type);
            csc.Content = content;

            byte[] bytesToSend = ClientServerCommunication.SerializeToByteArray(csc);
            stream.Write(bytesToSend, 0, bytesToSend.Length);
        }

        public void closeSession(GameSession ses)
        {
            sendToServer(ses, ClientServerCommunication.CommunicationType.CloseSession);
        }

        public void Movement(ChessPiece piece)
        {
            ChessPieceServer pieceServer = new ChessPieceServer(piece.Color, piece.Type);
            pieceServer.key = piece.parentTile.Key.ToUpper();
            pieceServer.previousKey = piece.previousKey.ToUpper();
            pieceServer.movementString = piece.movementString;
            sendToServer(pieceServer, ClientServerCommunication.CommunicationType.Move);
        }

        private ClientServerCommunication obtainFromServer()
        {
            byte[] serializedObject = new byte[clientSocket.ReceiveBufferSize];
            int bytesRead = stream.Read(serializedObject, 0, serializedObject.Length);

            return ClientServerCommunication.Deserialize<ClientServerCommunication>(serializedObject);
        }

        private void Client_FormClosing(object sender, FormClosingEventArgs e)
        {
            Disconnect();
        }

        public void Disconnect()
        {
            sendToServer(null, ClientServerCommunication.CommunicationType.Logout);
            stream.Close();
            clientSocket.Close();
            thread.Abort();
        }
    }
}
