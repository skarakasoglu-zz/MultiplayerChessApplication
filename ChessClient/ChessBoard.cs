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
    public class ChessBoard
    {

        public static string WhiteKeyLetter
        {
            get
            {
                return "ABCDEFGH";
            }
        }

        public static string WhiteKeyNumber
        {
            get
            {
                return "87654321";
            }
        }

        public static string BlackKeyLetter
        {
            get
            {
                return "HGFEDCBA";
            }
        }

        public static string BlackKeyNumber
        {
            get
            {
                return "12345678";
            }
        }

        public Dictionary<string, ChessBoardTile> boardTiles { get; set; }
        public List<string> movements { get; set; }
        public Panel pnlBoard { get; set; }
        public GameForm form { get; set; }
        public PieceColor turn { get; set; }
        public PieceColor currentColor { get; set; }

        public ChessBoard(GameForm gf)
        {
            this.form = gf;
            pnlBoard = new Panel
            {
                Size = new System.Drawing.Size(500, 540),
                Location = new System.Drawing.Point(10, 10)
            };
            movements = new List<string>();
            boardTiles = new Dictionary<string, ChessBoardTile>();
            createBoard();
            turn = PieceColor.White;
        }

        public void movementDone(string move)
        {
            movements.Add(move);
        }

        private void createBoard()
        {
            int x, y;
            x = 0;
            y = 0;
            UserViewModel blackPlayer = form.session.players[PieceColor.Black];
            UserViewModel whitePlayer = form.session.players[PieceColor.White];
            if (blackPlayer.UserID == form.client.CurrentUser.UserID) currentColor = PieceColor.Black;
            else currentColor = PieceColor.White;
            if (currentColor == PieceColor.Black)
            {
                for (int i = 0; i < BlackKeyNumber.Length; i++)
                {
                    Label lbl = new Label
                    {
                        Text = BlackKeyNumber[i].ToString(),
                        Size = new Size(20, 60),
                        TextAlign = ContentAlignment.MiddleCenter,
                        Location = new Point(x, y),
                        BackColor = Color.FromArgb(49, 46, 43),
                        ForeColor = Color.FromArgb(151, 150, 148)
                    };
                    lbl.Font = new Font(lbl.Font, FontStyle.Bold);
                    pnlBoard.Controls.Add(lbl);
                    y += 59;
                }
                x = 20;
                for (int i = 0; i < BlackKeyLetter.Length; i++)
                {
                    Label lbl = new Label
                    {
                        Text = BlackKeyLetter[i].ToString(),
                        Size = new Size(60, 20),
                        TextAlign = ContentAlignment.MiddleCenter,
                        Location = new Point(x, y),
                        BackColor = Color.FromArgb(49, 46, 43),
                        ForeColor = Color.FromArgb(151, 150, 148)
                    };
                    lbl.Font = new Font(lbl.Font, FontStyle.Bold);
                    pnlBoard.Controls.Add(lbl);
                    x += 59;
                }
            }
            else
            {
                for (int i = 0; i < WhiteKeyNumber.Length; i++)
                {
                    Label lbl = new Label
                    {
                        Text = WhiteKeyNumber[i].ToString(),
                        Size = new Size(20, 60),
                        TextAlign = ContentAlignment.MiddleCenter,
                        Location = new Point(x, y),
                        BackColor = Color.FromArgb(49, 46, 43),
                        ForeColor = Color.FromArgb(151, 150, 148)
                    };
                    lbl.Font = new Font(lbl.Font, FontStyle.Bold);
                    pnlBoard.Controls.Add(lbl);
                    y += 59;
                }
                x = 20;
                for (int i = 0; i < WhiteKeyLetter.Length; i++)
                {
                    Label lbl = new Label
                    {
                        Text = WhiteKeyLetter[i].ToString(),
                        Size = new Size(60, 20),
                        TextAlign = ContentAlignment.MiddleCenter,
                        Location = new Point(x, y),
                        BackColor = Color.FromArgb(49, 46, 43),
                        ForeColor = Color.FromArgb(151, 150, 148)
                    };
                    lbl.Font = new Font(lbl.Font, FontStyle.Bold);
                    pnlBoard.Controls.Add(lbl);
                    x += 59;
                }
            }
            y = 0;
            for (int i = 0; i < 8; i++)
            {
                x = 20;
                for (int j = 0; j < 8; j++)
                {
                    Color tileColor = (j + i) % 2 != 0
                        ? ChessBoardTile.boardDark : ChessBoardTile.boardLight;
                    string key = currentColor == PieceColor.Black ?
                        BlackKeyLetter[j].ToString() + BlackKeyNumber[i].ToString()
                        : WhiteKeyLetter[j].ToString() + WhiteKeyNumber[i].ToString();
                    ChessBoardTile chessBoardTile = new ChessBoardTile(x, y, tileColor, key.ToUpper());
                    chessBoardTile.mainBoard = this;
                    if (i == 0 || i == 1 || i == 6 || i == 7)
                    {
                        PieceColor pieceColor;
                        if (currentColor == PieceColor.Black)
                            pieceColor = (i == 6 || i == 7) ? PieceColor.Black : PieceColor.White;
                        else
                            pieceColor = (i == 0 || i == 1) ? PieceColor.Black : PieceColor.White;
                        PieceType pieceType;
                        if (currentColor == PieceColor.White)
                        {
                            pieceType = (i == 1 || i == 6) ? PieceType.Pawn :
                                (j <= 4) ? (PieceType)j : (PieceType)(7 - j);

                        }
                        else
                        {
                            pieceType = (i == 1 || i == 6) ? PieceType.Pawn :
                                (j <= 4) ? j < 3 ? (PieceType)j : j == 3 ? PieceType.King : PieceType.Queen
                                : (PieceType)(7 - j);
                        }
                        ChessPiece chessPiece = new ChessPiece(pieceColor, pieceType);
                        chessPiece.parentTile = chessBoardTile;
                        chessBoardTile.Piece = chessPiece;
                    }
                    pnlBoard.Controls.Add(chessBoardTile);
                    boardTiles.Add(key, chessBoardTile);
                    x += 59;
                }
                y += 59;
            }
        }
    }
}