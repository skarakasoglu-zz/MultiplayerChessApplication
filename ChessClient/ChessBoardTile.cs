using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static ChessClient.ChessPiece;

namespace ChessClient
{
    [Serializable]
    public class ChessBoardTile : Label
    {
        public static Color boardDark;
        public static Color boardLight;
        public static Color boardAvailable;
        public static Color boardCurrent;
        public ChessBoard mainBoard { get; set; }
        private readonly string _key;
        private ChessPiece _piece;
        public Color mainColor { get; set; }

        public ChessPiece Piece
        {
            get
            {
                return _piece;
            }
            set
            {
                Controls.Clear();
                _piece = value;
                Controls.Add(_piece);
            }
        }

        static ChessBoardTile()
        {
            boardDark = Color.FromArgb(118, 150, 86);
            boardLight = Color.FromArgb(238, 238, 210);
            boardAvailable = Color.FromArgb(246, 246, 131);
            boardCurrent = Color.FromArgb(186, 202, 69);
        }

        public ChessBoardTile(int x, int y, Color mainColor, string key)
        {
            AllowDrop = true;
            this._key = key.ToUpper();
            Size = new Size(60, 60);
            BorderStyle = BorderStyle.FixedSingle;
            Location = new Point(x, y);
            DragOver += ChessBoardTile_DragOver;
            DragDrop += ChessBoardTile_DragDrop;
            MouseHover += ChessBoardTile_MouseHover;
            this.mainColor = mainColor;
            BackColor = mainColor;
        }

        private void ChessBoardTile_MouseHover(object sender, EventArgs e)
        {
            if (Piece == null) Cursor = Cursors.Default;
        }

        private void ChessBoardTile_DragDrop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(typeof(ChessPiece)))
            {
                ChessPiece currentPiece = (ChessPiece)e.Data.GetData(typeof(ChessPiece));
                ChessBoardTile cbtCurrent = currentPiece.parentTile;
                bool contains = currentPiece.movableTiles.Contains(this);
                if (contains)
                {
                    currentPiece.previousKey = currentPiece.parentTile.Key;
                    
                    Cursor = Cursors.Hand;
                    cbtCurrent.Piece = null;
                    currentPiece.parentTile = this;
                    Piece = currentPiece;
                    currentPiece.moveCount++;
                    var player = new System.Media.SoundPlayer("../../effects/sound.wav");
                    player.Play();
                    mainBoard.movementDone(Piece.movementString);
                    currentPiece.parentTile.mainBoard.turn = currentPiece.Color == PieceColor.Black ? PieceColor.White : PieceColor.Black;
                    mainBoard.form.client.Movement(currentPiece);
                    mainBoard.form.getMovements(currentPiece.movementString);
                    mainBoard.form.updateTurn();
                }
                cbtCurrent.BackColor = cbtCurrent.mainColor;
                foreach (ChessBoardTile tile in currentPiece.movableTiles)
                {
                    tile.BackColor = tile.mainColor;
                }
                foreach (ChessBoardTile tile in currentPiece.capturableTiles)
                    tile.BackColor = tile.mainColor;
            }
        }

        private void ChessBoardTile_DragOver(object sender, DragEventArgs e)
        {
            if (e.KeyState == 1)
            {
                e.Effect = DragDropEffects.Move;
            }
        }


        public string Key
        {
            get
            {
                return _key;
            }
        }

        protected bool Equals(ChessBoardTile other)
        {
            return Key == other.Key;
        }

        public override int GetHashCode()
        {
            return Key.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((ChessBoardTile)obj);
        }

    }
}
