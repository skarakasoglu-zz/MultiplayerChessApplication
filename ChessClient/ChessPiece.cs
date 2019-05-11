using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ChessClient
{
    [Serializable]
    public class ChessPiece : PictureBox
    {

        private readonly PieceColor _color;
        private readonly PieceType _type;
        private readonly string keyLetter;
        private readonly string keyNumber;
        public ChessBoardTile parentTile { get; set; }
        public List<ChessBoardTile> movableTiles { get; set; }
        public List<ChessBoardTile> capturableTiles { get; set; }
        public string previousKey { get; set; }
        public int moveCount { get; set; }
        public string movementString
        {
            get
            {
                switch (Type)
                {
                    case PieceType.Knight:
                        return "N" + key.ToLower();
                    case PieceType.Pawn:
                        return key.ToLower();
                    default:
                        return Type.ToString()[0] + key.ToLower();
                }
            }
        }

        private string key
        {
            get
            {
                return parentTile.Key;
            }
        }

        private int columnIndex
        {
            get
            {
                if (Color == PieceColor.Black)
                    return ChessBoard.BlackKeyLetter.IndexOf(key[0]);
                else
                    return ChessBoard.WhiteKeyLetter.IndexOf(key[0]);
            }
        }

        private int rowIndex
        {
            get
            {
                if (Color == PieceColor.Black)
                    return ChessBoard.BlackKeyNumber.IndexOf(key[1]);
                else
                    return ChessBoard.WhiteKeyNumber.IndexOf(key[1]);
            }
        }

        private Dictionary<string, ChessBoardTile> tiles;

        public ChessPiece(PieceColor color, PieceType type)
        {
            AllowDrop = true;
            _color = color;
            _type = type;
            moveCount = 0;
            if (Color == PieceColor.Black)
            {
                keyLetter = ChessBoard.BlackKeyLetter;
                keyNumber = ChessBoard.BlackKeyNumber;
            }
            else
            {
                keyLetter = ChessBoard.WhiteKeyLetter;
                keyNumber = ChessBoard.WhiteKeyNumber;
            }
            movableTiles = new List<ChessBoardTile>();
            capturableTiles = new List<ChessBoardTile>();
            Size = new System.Drawing.Size(60, 60);
            Image = System.Drawing.Image.FromFile("../../images/" + color.ToString() + type.ToString() + ".png");
            SizeMode = PictureBoxSizeMode.StretchImage;
            MouseDown += ChessPiece_MouseDown;
            MouseHover += ChessPiece_MouseHover;
            DragOver += ChessPiece_DragOver;
            DragDrop += ChessPiece_DragDrop;
        }

        private void ChessPiece_MouseHover(object sender, EventArgs e)
        {
            Cursor = Cursors.Hand;
        }

        private void ChessPiece_DragOver(object sender, DragEventArgs e)
        {
            if (e.KeyState == 1)
            {
                e.Effect = DragDropEffects.Move;
                Cursor = Cursors.Hand;
            }
        }

        private void ChessPiece_DragDrop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(typeof(ChessPiece)))
            {
                ChessPiece currentPiece = (ChessPiece)e.Data.GetData(typeof(ChessPiece));
                if (currentPiece.capturableTiles.Contains(parentTile))
                {
                    currentPiece.previousKey = currentPiece.parentTile.Key;
                    parentTile.Piece = currentPiece;
                    currentPiece.parentTile.BackColor = currentPiece.parentTile.mainColor;
                    currentPiece.parentTile.Piece = null;
                    currentPiece.parentTile = parentTile;
                    parentTile = null;
                    var player = new System.Media.SoundPlayer("../../effects/sound.wav");
                    player.Play();
                    currentPiece.parentTile.mainBoard.movementDone(currentPiece.movementString + "+");
                    currentPiece.parentTile.mainBoard.turn = currentPiece.Color == PieceColor.Black ? PieceColor.White : PieceColor.Black;
                    if (Type == PieceType.King)
                    {
                        MessageBox.Show("Game OVER for " + Color.ToString());
                        currentPiece.parentTile.mainBoard.pnlBoard.Enabled = false;
                    }
                    currentPiece.parentTile.mainBoard.form.updateTurn();
                    currentPiece.parentTile.mainBoard.form.getMovements(currentPiece.movementString);
                    currentPiece.parentTile.mainBoard.form.client.Movement(currentPiece);
                }
                currentPiece.parentTile.BackColor = currentPiece.parentTile.mainColor;
                foreach (ChessBoardTile tile in currentPiece.movableTiles)
                    tile.BackColor = tile.mainColor;
                foreach (ChessBoardTile tile in currentPiece.capturableTiles)
                    tile.BackColor = tile.mainColor;
            }
        }

        private void ChessPiece_MouseDown(object sender, MouseEventArgs e)
        {
            movableTiles.Clear();
            capturableTiles.Clear();

            if (e.Button == MouseButtons.Left && parentTile.mainBoard.turn == Color && Color == parentTile.mainBoard.currentColor)
            {
                ChessBoardTile cbt;
                tiles = parentTile.mainBoard.boardTiles;
                parentTile.BackColor = ChessBoardTile.boardCurrent;

                switch (Type)
                {
                    case PieceType.Rook:
                        #region Rook Movements
                        straightMovement();
                        break;
                    #endregion
                    case PieceType.Knight:
                        #region Knight Movements
                        parentTile.BackColor = ChessBoardTile.boardCurrent;

                        for (int i = -2; i < 3; i++)
                        {
                            for (int j = -2; j < 3; j++)
                            {
                                if (Math.Abs(i) + Math.Abs(j) == 3
                                    && keyLetter.Length > columnIndex + i && keyNumber.Length > rowIndex + j
                                    && columnIndex + i > -1 && rowIndex + j > -1)
                                {
                                    string newKey = keyLetter[columnIndex + i].ToString() + keyNumber[rowIndex + j].ToString();
                                    if (tiles.Keys.Contains(newKey))
                                    {
                                        lookForMovement(tiles[newKey]);
                                    }
                                }
                            }
                        }
                        break;
                    #endregion
                    case PieceType.Bishop:
                        #region Bishop Movements
                        //Bishop movement rules
                        diagonalMovement();
                        break;
                    #endregion
                    case PieceType.Queen:
                        #region Queen Movements
                        straightMovement();
                        diagonalMovement();
                        #endregion
                        break;
                    case PieceType.King:
                        #region King Movements
                        int x = columnIndex;
                        int y = rowIndex;
                        for (int i = -1; i < 2; i++)
                        {
                            for (int j = -1; j < 2; j++)
                            {
                                if (x + i < keyLetter.Length && x + i > -1 && y + j < keyNumber.Length && y + j > -1)
                                {
                                    string newKey = keyLetter[x + i].ToString() + keyNumber[y + j].ToString();
                                    cbt = tiles[newKey];
                                    lookForMovement(cbt);
                                }
                            }
                        }
                        #endregion
                        break;
                    case PieceType.Pawn:
                        #region Pawn Movements
                        int rowPawn = rowIndex;
                        int columnPawn = columnIndex;
                        if (Color == PieceColor.Black)
                        {
                            rowPawn--;
                            if (rowPawn > -1) pawnMovement(columnPawn, rowPawn);
                            if (moveCount == 0)
                            {
                                rowPawn--;
                                string newKey = ChessBoard.BlackKeyLetter[columnPawn].ToString() + ChessBoard.BlackKeyNumber[rowPawn].ToString();
                                if (tiles.Keys.Contains(newKey))
                                {
                                    lookForMovement(tiles[newKey]);
                                }
                            }
                        }
                        else if (Color == PieceColor.White)
                        {
                            rowPawn--;
                            if (rowPawn > -1)
                                pawnMovement(columnPawn, rowPawn);
                            if (moveCount == 0)
                            {
                                rowPawn--;
                                string newKey = ChessBoard.WhiteKeyLetter[columnPawn].ToString() + ChessBoard.WhiteKeyNumber[rowPawn].ToString();
                                if (tiles.Keys.Contains(newKey))
                                {
                                    lookForMovement(tiles[newKey]);
                                }
                            }

                        }
                        break;
                    #endregion
                    default:
                        break;
                }
                DoDragDrop(this, DragDropEffects.All);
            }
        }

        #region Straight Movements
        //Deals with straight movements like Rook
        private void straightMovement()
        {
            int y = rowIndex;
            int x = columnIndex;

            bool searchToLeft = true;
            bool searchToRight = true;
            bool searchToTop = true;
            bool searchToBottom = true;

            int i = 1;
            while (searchToRight || searchToLeft || searchToTop || searchToBottom)
            {
                string newKey;
                if (searchToLeft)
                    searchToLeft = columnIndex - i > -1 && searchToLeft;
                if (searchToRight)
                    searchToRight = columnIndex + i < keyLetter.Length && searchToRight;
                if (searchToTop)
                    searchToTop = rowIndex + i < keyNumber.Length && searchToTop;
                if (searchToBottom)
                    searchToBottom = rowIndex - i > -1 && searchToBottom;

                if (searchToLeft)
                {
                    newKey = keyLetter[columnIndex - i].ToString() + keyNumber[y].ToString();
                    searchToLeft = lookForMovement(tiles[newKey]);
                }
                if (searchToRight)
                {
                    newKey = keyLetter[columnIndex + i].ToString() + keyNumber[y].ToString();
                    searchToRight = lookForMovement(tiles[newKey]);
                }
                if (searchToTop)
                {
                    newKey = keyLetter[x].ToString() + keyNumber[rowIndex + i].ToString();
                    searchToTop = lookForMovement(tiles[newKey]);
                }
                if (searchToBottom)
                {
                    newKey = keyLetter[x].ToString() + keyNumber[rowIndex - i].ToString();
                    searchToBottom = lookForMovement(tiles[newKey]);
                }
                i++;
            }
        }
        #endregion

        #region Diagonal Movements
        //Deals with diagonal movements like Bishop
        private void diagonalMovement()
        {
            int x = columnIndex + 1;
            int y = rowIndex + 1;


            bool searchToTopLeft = true;
            bool searchToTopRight = true;
            bool searchToBottomLeft = true;
            bool searchToBottomRight = true;

            int i = 1;
            while (searchToTopLeft || searchToTopRight || searchToBottomLeft || searchToBottomRight)
            {
                string newKey;

                if (searchToTopLeft)
                    searchToTopLeft = (columnIndex - i > -1 && rowIndex + i < keyNumber.Length) && searchToTopLeft;
                if (searchToTopRight)
                    searchToTopRight = (columnIndex + i < keyLetter.Length && rowIndex + i < keyNumber.Length) && searchToTopRight;
                if (searchToBottomLeft)
                    searchToBottomLeft = (columnIndex - i > -1 && rowIndex - i > -1) && searchToBottomLeft;
                if (searchToBottomRight)
                    searchToBottomRight = (columnIndex + i < keyLetter.Length && rowIndex - i > -1) && searchToBottomRight;

                if (searchToTopLeft)
                {
                    newKey = keyLetter[columnIndex - i].ToString() + keyNumber[rowIndex + i].ToString();
                    searchToTopLeft = lookForMovement(tiles[newKey]);
                }
                if (searchToTopRight)
                {
                    newKey = keyLetter[columnIndex + i].ToString() + keyNumber[rowIndex + i].ToString();
                    searchToTopRight = lookForMovement(tiles[newKey]);
                }
                if (searchToBottomLeft)
                {
                    newKey = keyLetter[columnIndex - i].ToString() + keyNumber[rowIndex - i].ToString();
                    searchToBottomLeft = lookForMovement(tiles[newKey]);
                }
                if (searchToBottomRight)
                {
                    newKey = keyLetter[columnIndex + i].ToString() + keyNumber[rowIndex - i].ToString();
                    searchToBottomRight = lookForMovement(tiles[newKey]);
                }
                i++;
            }
        }
        #endregion

        private bool lookForMovement(ChessBoardTile cbt)
        {

            if (cbt.Piece != null)
            {
                if (Type != PieceType.Pawn) lookForCapturing(cbt);
                return false;
            }
            else
            {
                cbt.BackColor = ChessBoardTile.boardAvailable;
                movableTiles.Add(cbt);
                return true;
            }
        }

        private void lookForCapturing(ChessBoardTile cbt)
        {
            if (cbt.Piece != null && cbt.Piece.Color != Color)
            {
                cbt.BackColor = ChessBoardTile.boardCurrent;
                capturableTiles.Add(cbt);
            }
        }

        private void pawnMovement(int columnIndex, int rowIndex)
        {
            string newKey;
            if (Color == PieceColor.Black)
                newKey = ChessBoard.BlackKeyLetter[columnIndex].ToString() + ChessBoard.BlackKeyNumber[rowIndex].ToString();
            else
                newKey = ChessBoard.WhiteKeyLetter[columnIndex].ToString() + ChessBoard.WhiteKeyNumber[rowIndex].ToString();
            if (tiles.Keys.Contains(newKey))
            {
                lookForMovement(tiles[newKey]);

                for (int i = -1; i < 2; i++)
                {
                    int column = columnIndex + i;
                    if (i != 0 && column < keyLetter.Length && column > -1)
                    {
                        newKey = keyLetter[column].ToString() + keyNumber[rowIndex].ToString();
                        if (tiles.Keys.Contains(newKey))
                        {
                            lookForCapturing(tiles[newKey]);
                        }
                    }
                }
            }
        }

        public PieceType Type
        {
            get { return _type; }
        }

        public PieceColor Color
        {
            get { return _color; }
        }

        public enum PieceType
        {
            Rook,
            Knight,
            Bishop,
            Queen,
            King,
            Pawn
        }

        public enum PieceColor
        {
            Black,
            White
        }
    }
}
