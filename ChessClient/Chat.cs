using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ChessClient
{
    public class Chat
    {
        public string FriendID { get; set; }
        public Panel pnlChat { get; set; }
        private TextBox txtMessages;
        private TextBox txtMessage;
        private Client client;

        public Chat(Client client)
        {
            this.client = client;
            pnlChat = new Panel
            {
                Visible = false,
                Size = new Size(300, 375)
            };
            txtMessages = new TextBox
            {
                Location = new Point(3, 3),
                Margin = new Padding(0),
                Size = new Size(290, 340),
                WordWrap = true,
                BorderStyle = BorderStyle.None,
                Multiline = true,
                BackColor = Color.FromArgb(23, 23, 23),
                ReadOnly = true,
                ForeColor = Color.White,
                ScrollBars = ScrollBars.Vertical
            };
            txtMessage = new TextBox
            {
                Location = new System.Drawing.Point(2, 345),
                Size = new System.Drawing.Size(290, 20),
                TabIndex = 1
            };
            txtMessage.KeyPress += TxtMessage_KeyPress;
            pnlChat.Controls.Add(txtMessages);
            pnlChat.Controls.Add(txtMessage);
        }

        private void TxtMessage_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                MessageViewModel msg = new MessageViewModel
                {
                    Content = txtMessage.Text,
                    DestinationID = FriendID,
                    SourceID = client.CurrentUser.UserID
                };
                client.SendMessage(msg);
                newMessage(msg);
                txtMessage.Clear();
            }
        }

        public void newMessage(MessageViewModel msg)
        {
            string userName = msg.SourceID == client.CurrentUser.UserID ? "You" : msg.SourceID;
            txtMessages.Text += userName + ": " + msg.Content + Environment.NewLine;
        }

        protected bool Equals(Chat other)
        {
            return FriendID == other.FriendID;
        }

        public override int GetHashCode()
        {
            return FriendID.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((Chat)obj);
        }
    }
}
