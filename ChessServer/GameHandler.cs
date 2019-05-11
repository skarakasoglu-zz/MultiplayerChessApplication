using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChessClient;

namespace ChessServer
{
    class GameHandler
    {
        public string sessionID { get; set; }
        public string sessionName { get; set; }
        public Dictionary<ChessPiece.PieceColor, UserViewModel> players { get; set; }
        public Dictionary<string, ChessPieceServer> chessBoard { get; set; }
        public static string KeyLetter
        {
            get
            {
                return "ABCDEFGH";
            }
        }
        public static string KeyNumber
        {
            get
            {
                return "12345678";
            }
        }

        public GameHandler(string sessionName)
        {
            Guid ses = Guid.NewGuid();
            sessionID = Convert.ToBase64String(ses.ToByteArray());
            sessionID = sessionID.Replace("=", "");
            sessionID = sessionID.Replace("+", "");
            players = new Dictionary<ChessPiece.PieceColor, UserViewModel>();
            this.sessionName = sessionName;
            chessBoard = new Dictionary<string, ChessPieceServer>();
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    string key = KeyLetter[j].ToString() + KeyNumber[KeyNumber.Length - i - 1].ToString();
                    if (i == 0 || i == 1 || i == 6 || i == 7)
                    {
                        ChessPiece.PieceColor pieceColor = (i == 0 || i == 1) ? ChessPiece.PieceColor.Black : ChessPiece.PieceColor.White;
                        ChessPiece.PieceType pieceType = (i == 1 || i == 6) ? ChessPiece.PieceType.Pawn :
                            (j <= 4) ? (ChessPiece.PieceType)j : (ChessPiece.PieceType)(7 - j);
                        ChessPieceServer pieceServer = new ChessPieceServer(pieceColor, pieceType);
                        chessBoard.Add(key, pieceServer);
                    }
                    else
                        chessBoard.Add(key, new ChessPieceServer());
                }
            }
        }

        public void Start()
        {
        }

        protected bool Equals(GameHandler other)
        {
            return sessionID == other.sessionID;
        }

        public override int GetHashCode()
        {
            return sessionID.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((GameHandler)obj);
        }

    }
}
