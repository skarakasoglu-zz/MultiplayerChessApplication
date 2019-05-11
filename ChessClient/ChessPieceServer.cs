using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static ChessClient.ChessPiece;

namespace ChessClient
{
    [Serializable]
    public class ChessPieceServer
    {
        public PieceColor Color
        {
            get
            {
                return _color;
            }
        }

        public PieceType Type
        {
            get
            {
                return _type;
            }
        }

        public string key { get; set; }
        public string previousKey { get; set; }

        private readonly PieceColor _color;
        private readonly PieceType _type;
        public string movementString { get; set; }

        public ChessPieceServer(PieceColor color, PieceType type)
        {
            _color = color;
            _type = type;
        }

        public ChessPieceServer()
        {

        }
    }
}
