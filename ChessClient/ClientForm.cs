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
    public partial class ClientForm : Form
    {
        Client client;
        public UserViewModel currentUser;
        public List<Chat> lstChats;
        Panel[] navigationPanels;
        Font defaultFont;
        bool odd;
        ChessPiece.PieceColor usersColor, opponentsColor;

        public ClientForm(Client client)
        {
            InitializeComponent();
            CheckForIllegalCrossThreadCalls = false;
            this.client = client;
            currentUser = client.CurrentUser;


            navigationPanels = new Panel[3];
            navigationPanels[0] = pnlHome;
            navigationPanels[1] = pnlProfile;
            navigationPanels[2] = pnlRooms;

            lstChats = new List<Chat>();

            pbMessages.Cursor = Cursors.Hand;

            lblUserName.Text = currentUser.UserID;
            lblFullName.Text = currentUser.FullName;

            defaultFont = lblUserName.Font;

            odd = true;

            foreach (var onlineFriend in currentUser.OnlineFriends)
            {
                addToOnlineFriendList(onlineFriend, odd);
                odd = !odd;
            }
        }

        private void ClientForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            client.Disconnect();
            client.Dispose();
        }

        private void Navigation_Click(object sender, EventArgs e)
        {
            Label current = (Label)sender;
            switchForms(current.Tag.ToString());

            if (current.Tag.ToString() == "Rooms")
            {
                client.getRooms();
            }
        }

        public void FailedToCreate()
        {
            MessageBox.Show("You are already in a game. You can't create new room.");
        }

        public void ListRooms(List<GameSession> sessions)
        {
            pnlRoomList.Controls.Clear();
            int x = 5, y = 5;
            foreach (GameSession session in sessions)
            {
                Panel pnlGnl = new Panel
                {
                    Size = new Size(180, 130),
                    Location = new Point(x, y)
                };

                Panel pnlName = new Panel
                {
                    Size = new Size(180, 24),
                    Location = new Point(0, 0),
                    BackColor = Color.FromArgb(116, 112, 112)
                };
                Label lblRoomName = new Label
                {
                    Text = session.sessionName,
                    Font = new System.Drawing.Font("Gadugi", 12F, FontStyle.Regular, GraphicsUnit.Point, ((byte)(0))),
                    ForeColor = Color.White
                };
                pnlName.Controls.Add(lblRoomName);

                Panel pnlRoom = new Panel
                {
                    Size = new Size(180, 75),
                    Location = new Point(0, 24),
                    BackColor = Color.FromArgb(145, 208, 80)
                };

                Label lblWaitingUser = new Label();

                if (session.players[ChessPiece.PieceColor.Black] != null)
                    lblWaitingUser.Text = session.players[ChessPiece.PieceColor.Black].UserID;
                else
                    lblWaitingUser.Text = session.players[ChessPiece.PieceColor.White].UserID;

                lblWaitingUser.Location = new Point(15, 5);
                lblWaitingUser.AutoSize = true;

                Label lbl = new Label
                {
                    Text = "Waiting for a player...",
                    Location = new Point(20, 50),
                    AutoSize = true
                };

                Button btnJoin = new Button
                {
                    Size = new Size(120, 24),
                    Text = "Join Room",
                    Location = new Point(60, 102),
                    Font = new Font("Gadugi", 12F, FontStyle.Regular, GraphicsUnit.Point, 0),
                    Tag = session.sessionID
                };

                btnJoin.Click += BtnJoin_Click;

                x += 185;
                if (x > 540) { y += 135; x = 5; }

                pnlRoom.Controls.Add(lblWaitingUser);
                pnlRoom.Controls.Add(lbl);
                pnlGnl.Controls.Add(pnlName);
                pnlGnl.Controls.Add(pnlRoom);
                pnlGnl.Controls.Add(btnJoin);
                pnlRoomList.Controls.Add(pnlGnl);
            }
        }

        private void BtnJoin_Click(object sender, EventArgs e)
        {
            Button current = (Button)sender;
            string sessionId = current.Tag.ToString();
            client.JoinRoom(sessionId);
        }

        private void switchForms(string tag)
        {
            foreach (Panel pnl in navigationPanels)
            {
                if (pnl.Tag.ToString() == tag) { pnl.Visible = true; pnl.BringToFront(); }
                else pnl.Visible = false;
            }
        }

        public void roomCreated(GameSession ses)
        {
            MessageBox.Show("You created the room. Congrats!");
            pnlCreatingRoom.Visible = false;
            client.getRooms();
            pnlRooms.Visible = true;
            pnlRooms.BringToFront();
        }

        public void friendGotOnline(UserViewModel friend)
        {
            currentUser.OnlineFriends.Add(friend);
            addToOnlineFriendList(friend, odd);
            odd = !odd;
        }

        public void friendGotOffline(UserViewModel friend)
        {
            currentUser.OnlineFriends.Remove(friend);
            currentUser.OfflineFriends.Add(friend);
            deleteFromOnlineFriendList(friend);
        }

        public void getMessage(MessageViewModel msg)
        {
            Chat chat = new Chat(client) { FriendID = msg.SourceID };
            int index = lstChats.IndexOf(chat);
            if (index != -1)
            {
                foreach (Panel chatPnl in pnlChats.Controls)
                {
                    if (chatPnl.Tag.ToString() == lstChats[index].FriendID)
                    {
                        chatPnl.BackColor = Color.FromArgb(44, 53, 59);
                    }
                    else chatPnl.BackColor = Color.FromArgb(0, 10, 20);
                }

                for (int i = 0; i < lstChats.Count; i++)
                {
                    if (i != index) lstChats[i].pnlChat.Visible = false;
                }
            }
            else
            {
                Panel pnlFriendChat = new Panel()
                {
                    Font = defaultFont,
                    Name = "pnlUserInfo",
                    Size = new System.Drawing.Size(254, 55),
                    TabIndex = 4,
                    Cursor = Cursors.Hand,
                    Tag = msg.SourceID,
                    Margin = new Padding(0),
                    BackColor = Color.FromArgb(44, 53, 59)
                };
                pnlFriendChat.Click += PnlFriendChat_Click;
                Label lblUserID = new Label
                {
                    ForeColor = Color.FromArgb(187, 188, 182),
                    AutoSize = true,
                    Font = defaultFont,
                    Location = new Point(20, 10),
                    TabIndex = 3,
                    Text = msg.SourceID
                };

                foreach (Panel chatPnl in pnlChats.Controls)
                {
                    chatPnl.BackColor = Color.FromArgb(0, 10, 20);
                }
                index = lstChats.Count;
                lstChats.Add(chat);
                pnlFriendChat.Controls.Add(lblUserID);
                pnlChats.Controls.Add(pnlFriendChat);
                pnlChat.Controls.Add(chat.pnlChat);
            }
            lstChats[index].newMessage(msg);
            lstChats[index].pnlChat.Visible = true;
            lstChats[index].pnlChat.BringToFront();
            pnlMessages.Visible = true;
            pnlMessages.BringToFront();
        }

        private void addToOnlineFriendList(UserViewModel onlineFriend, bool odd)
        {
            Panel pnlFriendInfo = new Panel()
            {
                Font = defaultFont,
                Name = "pnlUserInfo",
                Size = new System.Drawing.Size(254, 70),
                BorderStyle = BorderStyle.FixedSingle,
                Margin = new Padding(0),
                TabIndex = 4,
                Cursor = Cursors.Hand,
                Tag = onlineFriend.UserID,
                BackColor = odd ? Color.FromArgb(61, 60, 58) : Color.FromArgb(83, 83, 83)
            };
            // pnlFriendInfo.Click += Control_Click;
            ContextMenu contextMenu = new ContextMenu();
            MenuItem menuProfile = new MenuItem
            {
                Text = "Profile"
            };
            MenuItem menuInvite = new MenuItem
            {
                Text = "Invite to play"
            };
            MenuItem menuMsg = new MenuItem
            {
                Text = "Send message"
            };
            menuMsg.Click += Control_Click;
            MenuItem menuRemove = new MenuItem
            {
                Text = "Remove"
            };
            contextMenu.MenuItems.Add(menuProfile);
            contextMenu.MenuItems.Add(menuInvite);
            contextMenu.MenuItems.Add(menuMsg);
            contextMenu.MenuItems.Add(menuRemove);
            pnlFriendInfo.ContextMenu = contextMenu;
            Label lblUserID = new Label
            {
                ForeColor = Color.FromArgb(187, 188, 182),
                AutoSize = true,
                Font = defaultFont,
                Location = new Point(65, 10),
                TabIndex = 3,
                Text = onlineFriend.UserID
            };

            ToolTip toolTipUserID = new ToolTip();
            toolTipUserID.SetToolTip(lblUserID, lblUserID.Text);

            Label lblFullName = new Label
            {
                Text = onlineFriend.FullName,
                ForeColor = Color.FromArgb(187, 188, 182),
                AutoSize = true,
                Font = defaultFont,
                Location = new Point(65, 36),
                TabIndex = 3,
            };

            ToolTip toolTipName = new ToolTip();
            toolTipName.SetToolTip(lblFullName, lblFullName.Text);

            pnlFriendInfo.Controls.Add(lblUserID);
            pnlFriendInfo.Controls.Add(lblFullName);
            pnlFriends.Controls.Add(pnlFriendInfo);
        }

        private void Control_Click(object sender, EventArgs e)
        {
            Panel pnl;

            ContextMenu cm = (ContextMenu)((MenuItem)sender).Parent;
            pnl = (Panel)cm.SourceControl;

            Panel pnlFriendChat = null;
            int index = lstChats.IndexOf(new Chat(client) { FriendID = pnl.Tag.ToString() });
            Chat chat = null;
            if (index != -1)
            {
                foreach (Panel chatPnl in pnlChats.Controls)
                {
                    if (chatPnl.Tag.ToString() == pnl.Tag.ToString())
                    {
                        pnlFriendChat = chatPnl;
                        chat = lstChats[index];
                    }
                    else chatPnl.BackColor = Color.FromArgb(0, 10, 20);
                }
            }
            else
            {
                pnlFriendChat = new Panel()
                {
                    Font = defaultFont,
                    Name = "pnlUserInfo",
                    Size = new System.Drawing.Size(254, 55),
                    TabIndex = 4,
                    Cursor = Cursors.Hand,
                    Tag = pnl.Tag.ToString(),
                    Margin = new Padding(0),
                    BackColor = Color.FromArgb(0, 10, 20)
                };
                Label lblUserID = new Label
                {
                    ForeColor = Color.FromArgb(187, 188, 182),
                    AutoSize = true,
                    Font = defaultFont,
                    Location = new Point(20, 10),
                    TabIndex = 3,
                    Text = pnl.Tag.ToString()
                };

                foreach (Panel chatPnl in pnlChats.Controls)
                {
                    chatPnl.BackColor = Color.FromArgb(0, 10, 20);
                }
                chat = new Chat(client) { FriendID = pnl.Tag.ToString() };

                lstChats.Add(chat);
                pnlFriendChat.Controls.Add(lblUserID);
                pnlChats.Controls.Add(pnlFriendChat);
                pnlChat.Controls.Add(chat.pnlChat);
            }
            chat.pnlChat.Visible = true;
            chat.pnlChat.BringToFront();
            pnlFriendChat.BackColor = Color.FromArgb(44, 53, 59);
            pnlMessages.Visible = true;
            pnlMessages.BringToFront();
        }

        private void PnlFriendChat_Click(object sender, EventArgs e)
        {
            Panel pnl = (Panel)sender;

            pnl.BackColor = Color.FromArgb(82, 82, 82);

            foreach (Panel pnlChat in pnlChats.Controls)
            {
                if (pnl != pnlChat) pnlChat.BackColor = Color.FromArgb(0, 10, 20);
            }

            Chat chat = new Chat(client) { FriendID = pnl.Tag.ToString() };
            int index = lstChats.IndexOf(chat);
            chat = lstChats[index];

            foreach (Panel pnlFM in pnlChat.Controls)
            {
                if (pnlFM != chat.pnlChat) pnlFM.Visible = false;
            }

            chat.pnlChat.Visible = true;
        }

        private void deleteFromOnlineFriendList(UserViewModel offlineFriend)
        {
            foreach (Panel pnlFriend in pnlFriends.Controls)
            {
                if (pnlFriend.Tag.ToString() == offlineFriend.UserID)
                {
                    pnlFriends.Controls.Remove(pnlFriend);
                }
            }
        }

        private void pbMessages_Click(object sender, EventArgs e)
        {
            pnlMessages.Visible = !pnlMessages.Visible;
            pnlMessages.BringToFront();
        }

        private void btnCreateRoom_Click(object sender, EventArgs e)
        {
            pnlCreatingRoom.Visible = true;
            usersColor = ChessPiece.PieceColor.Black;
            opponentsColor = ChessPiece.PieceColor.White;

            lblBlackSide.Text = currentUser.UserID;
            lblWhiteSide.Text = "...";
            pnlCreatingRoom.BringToFront();
        }

        private void btnCreate_Click(object sender, EventArgs e)
        {
            GameSession gs = new GameSession()
            {
                sessionName = txtRoomName.Text
            };
            if (usersColor == ChessPiece.PieceColor.Black)
            {
                gs.players.Add(ChessPiece.PieceColor.Black, currentUser);
                gs.players.Add(ChessPiece.PieceColor.White, null);
            }
            else
            {
                gs.players.Add(ChessPiece.PieceColor.Black, null);
                gs.players.Add(ChessPiece.PieceColor.White, currentUser);
            }
            client.CreateARoom(gs);
        }

        private void btnSelect_Click(object sender, EventArgs e)
        {
            if (usersColor == ChessPiece.PieceColor.Black)
            {
                usersColor = ChessPiece.PieceColor.White;
                opponentsColor = ChessPiece.PieceColor.Black;
                lblWhiteSide.Text = currentUser.UserID;
                lblBlackSide.Text = "...";
            }
            else
            {
                usersColor = ChessPiece.PieceColor.Black;
                opponentsColor = ChessPiece.PieceColor.White;
                lblBlackSide.Text = currentUser.UserID;
                lblWhiteSide.Text = "...";
            }
        }
    }
}
