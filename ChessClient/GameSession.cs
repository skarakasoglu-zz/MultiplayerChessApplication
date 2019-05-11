using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static ChessClient.ChessPiece;

namespace ChessClient
{
    [Serializable]
    public class GameSession
    {
        public string sessionID { get; set; }
        public string sessionName { get; set; }
        public Dictionary<ChessPiece.PieceColor, UserViewModel> players { get; set; }

        public GameSession()
        {
            players = new Dictionary<PieceColor, UserViewModel>();
        }

        protected bool Equals(GameSession other)
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
            return Equals((GameSession)obj);
        }
    }
}
