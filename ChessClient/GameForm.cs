using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ChessClient
{
    public partial class GameForm : Form
    {
        public Client client { get; set; }
        ChessBoard cb;
        public GameSession session { get; set; }
        public GameForm(GameSession ses, Client client)
        {
            InitializeComponent();
            this.client = client;
            session = ses;
            cb = new ChessBoard(this);
            Controls.Add(cb.pnlBoard);
            label2.Text = ses.players[ChessPiece.PieceColor.Black].UserID;
            label1.Text = ses.players[ChessPiece.PieceColor.White].UserID;
            label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            string userId = ses.players[ChessPiece.PieceColor.Black].UserID == client.CurrentUser.UserID ?
                ses.players[ChessPiece.PieceColor.White].UserID : ses.players[ChessPiece.PieceColor.Black].UserID;
            Text = "Playing against " + userId;
        }

        public void getMovements(string movement)
        {
            txtMovements.Text += movement + Environment.NewLine;
        }

        public void opponentsMovement(ChessPieceServer piece)
        {
            string previousKey = null;
            string currentKey = null;
            cb.turn = cb.turn == ChessPiece.PieceColor.Black ? ChessPiece.PieceColor.White : ChessPiece.PieceColor.Black;
            currentKey = piece.key;
            ChessPiece found = null;
            foreach (var item in cb.boardTiles)
            {
                ChessPiece currentPiece = item.Value.Piece;
                if (currentPiece != null)
                {

                    if (currentPiece.Type == piece.Type && currentPiece.Color == piece.Color && item.Key == piece.previousKey)
                    {
                        found = currentPiece;
                    }
                }
            }
            bool pnlEnable = true;
            if (cb.boardTiles[currentKey].Piece != null)
            {
                if (cb.boardTiles[currentKey].Piece.Type == ChessPiece.PieceType.King)
                    pnlEnable = false;
            }
            cb.boardTiles[currentKey].Piece = found;
            ChessBoardTile cbt = cb.boardTiles[piece.previousKey];
            found.parentTile = cb.boardTiles[currentKey];
            cbt.Piece = null;
            cb.pnlBoard.Enabled = pnlEnable;
            getMovements(found.movementString);
            var player = new System.Media.SoundPlayer("../../effects/sound.wav");
            player.Play();
            updateTurn();
        }

        private void GameForm_FormClosing(object sender, FormClosingEventArgs e)
        {
        }

        public void updateTurn()
        {
            if (cb.turn == ChessPiece.PieceColor.White)
            {
                this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
                this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            }
            else
            {
                this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
                this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));

            }
        }
    }
}
