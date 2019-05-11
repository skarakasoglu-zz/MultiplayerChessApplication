using System.Drawing;

namespace ChessClient
{
    partial class ClientForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ClientForm));
            this.lblRooms = new System.Windows.Forms.Label();
            this.lblHome = new System.Windows.Forms.Label();
            this.lblProfile = new System.Windows.Forms.Label();
            this.pnlHeader = new System.Windows.Forms.Panel();
            this.pnlNavigation = new System.Windows.Forms.Panel();
            this.pnlUserInfo = new System.Windows.Forms.Panel();
            this.lblFullName = new System.Windows.Forms.Label();
            this.lblUserName = new System.Windows.Forms.Label();
            this.pnlHome = new System.Windows.Forms.Panel();
            this.btnCreateRoom = new System.Windows.Forms.Button();
            this.pnlRightBottom = new System.Windows.Forms.Panel();
            this.pbMessages = new System.Windows.Forms.PictureBox();
            this.pnlMessages = new System.Windows.Forms.FlowLayoutPanel();
            this.pnlChats = new System.Windows.Forms.FlowLayoutPanel();
            this.pnlChat = new System.Windows.Forms.Panel();
            this.pnlProfile = new System.Windows.Forms.Panel();
            this.pnlRooms = new System.Windows.Forms.FlowLayoutPanel();
            this.pnlCreateRoom = new System.Windows.Forms.Panel();
            this.pnlRoomList = new System.Windows.Forms.Panel();
            this.pnlFriends = new System.Windows.Forms.FlowLayoutPanel();
            this.pnlCreatingRoom = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.lblSideRight = new System.Windows.Forms.Label();
            this.lblWhiteSide = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label3 = new System.Windows.Forms.Label();
            this.lblBlackSide = new System.Windows.Forms.Label();
            this.btnSelect = new System.Windows.Forms.Button();
            this.btnInvite = new System.Windows.Forms.Button();
            this.txtRoomName = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnCreate = new System.Windows.Forms.Button();
            this.pnlHeader.SuspendLayout();
            this.pnlNavigation.SuspendLayout();
            this.pnlUserInfo.SuspendLayout();
            this.pnlRightBottom.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbMessages)).BeginInit();
            this.pnlMessages.SuspendLayout();
            this.pnlRooms.SuspendLayout();
            this.pnlCreateRoom.SuspendLayout();
            this.pnlCreatingRoom.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblRooms
            // 
            this.lblRooms.AutoSize = true;
            this.lblRooms.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lblRooms.Font = new System.Drawing.Font("Gadugi", 16F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblRooms.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(187)))), ((int)(((byte)(188)))), ((int)(((byte)(182)))));
            this.lblRooms.Location = new System.Drawing.Point(34, 35);
            this.lblRooms.Name = "lblRooms";
            this.lblRooms.Size = new System.Drawing.Size(65, 26);
            this.lblRooms.TabIndex = 0;
            this.lblRooms.Tag = "Rooms";
            this.lblRooms.Text = "PLAY";
            this.lblRooms.Click += new System.EventHandler(this.Navigation_Click);
            // 
            // lblHome
            // 
            this.lblHome.AutoSize = true;
            this.lblHome.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lblHome.Font = new System.Drawing.Font("Gadugi", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblHome.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(187)))), ((int)(((byte)(188)))), ((int)(((byte)(182)))));
            this.lblHome.Location = new System.Drawing.Point(146, 40);
            this.lblHome.Name = "lblHome";
            this.lblHome.Size = new System.Drawing.Size(57, 19);
            this.lblHome.TabIndex = 1;
            this.lblHome.Tag = "Home";
            this.lblHome.Text = "HOME";
            this.lblHome.Visible = false;
            this.lblHome.Click += new System.EventHandler(this.Navigation_Click);
            // 
            // lblProfile
            // 
            this.lblProfile.AutoSize = true;
            this.lblProfile.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lblProfile.Font = new System.Drawing.Font("Gadugi", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblProfile.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(187)))), ((int)(((byte)(188)))), ((int)(((byte)(182)))));
            this.lblProfile.Location = new System.Drawing.Point(254, 40);
            this.lblProfile.Name = "lblProfile";
            this.lblProfile.Size = new System.Drawing.Size(71, 19);
            this.lblProfile.TabIndex = 2;
            this.lblProfile.Tag = "Profile";
            this.lblProfile.Text = "PROFILE";
            this.lblProfile.Visible = false;
            this.lblProfile.Click += new System.EventHandler(this.Navigation_Click);
            // 
            // pnlHeader
            // 
            this.pnlHeader.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlHeader.Controls.Add(this.pnlNavigation);
            this.pnlHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlHeader.Location = new System.Drawing.Point(0, 0);
            this.pnlHeader.Name = "pnlHeader";
            this.pnlHeader.Size = new System.Drawing.Size(863, 101);
            this.pnlHeader.TabIndex = 3;
            // 
            // pnlNavigation
            // 
            this.pnlNavigation.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(42)))), ((int)(((byte)(42)))), ((int)(((byte)(42)))));
            this.pnlNavigation.Controls.Add(this.pnlUserInfo);
            this.pnlNavigation.Controls.Add(this.lblHome);
            this.pnlNavigation.Controls.Add(this.lblProfile);
            this.pnlNavigation.Controls.Add(this.lblRooms);
            this.pnlNavigation.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlNavigation.Font = new System.Drawing.Font("Gadugi", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pnlNavigation.Location = new System.Drawing.Point(0, 0);
            this.pnlNavigation.Name = "pnlNavigation";
            this.pnlNavigation.Size = new System.Drawing.Size(861, 100);
            this.pnlNavigation.TabIndex = 4;
            // 
            // pnlUserInfo
            // 
            this.pnlUserInfo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(29)))), ((int)(((byte)(29)))), ((int)(((byte)(29)))));
            this.pnlUserInfo.Controls.Add(this.lblFullName);
            this.pnlUserInfo.Controls.Add(this.lblUserName);
            this.pnlUserInfo.Font = new System.Drawing.Font("Cambria", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pnlUserInfo.Location = new System.Drawing.Point(609, 0);
            this.pnlUserInfo.Name = "pnlUserInfo";
            this.pnlUserInfo.Size = new System.Drawing.Size(254, 101);
            this.pnlUserInfo.TabIndex = 4;
            // 
            // lblFullName
            // 
            this.lblFullName.AutoSize = true;
            this.lblFullName.Cursor = System.Windows.Forms.Cursors.Default;
            this.lblFullName.Font = new System.Drawing.Font("Gadugi", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFullName.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(187)))), ((int)(((byte)(188)))), ((int)(((byte)(182)))));
            this.lblFullName.Location = new System.Drawing.Point(26, 21);
            this.lblFullName.Name = "lblFullName";
            this.lblFullName.Size = new System.Drawing.Size(104, 19);
            this.lblFullName.TabIndex = 4;
            this.lblFullName.Text = "{ Full name }";
            // 
            // lblUserName
            // 
            this.lblUserName.AutoSize = true;
            this.lblUserName.Cursor = System.Windows.Forms.Cursors.Default;
            this.lblUserName.Font = new System.Drawing.Font("Gadugi", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblUserName.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(187)))), ((int)(((byte)(188)))), ((int)(((byte)(182)))));
            this.lblUserName.Location = new System.Drawing.Point(26, 42);
            this.lblUserName.Name = "lblUserName";
            this.lblUserName.Size = new System.Drawing.Size(110, 19);
            this.lblUserName.TabIndex = 3;
            this.lblUserName.Text = "{ User name }";
            // 
            // pnlHome
            // 
            this.pnlHome.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(216)))), ((int)(((byte)(216)))), ((int)(((byte)(216)))));
            this.pnlHome.Location = new System.Drawing.Point(0, 99);
            this.pnlHome.Name = "pnlHome";
            this.pnlHome.Size = new System.Drawing.Size(608, 556);
            this.pnlHome.TabIndex = 5;
            this.pnlHome.Tag = "Home";
            // 
            // btnCreateRoom
            // 
            this.btnCreateRoom.Font = new System.Drawing.Font("Gadugi", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCreateRoom.Location = new System.Drawing.Point(40, 4);
            this.btnCreateRoom.Name = "btnCreateRoom";
            this.btnCreateRoom.Size = new System.Drawing.Size(135, 25);
            this.btnCreateRoom.TabIndex = 0;
            this.btnCreateRoom.Text = "Create a Room";
            this.btnCreateRoom.UseVisualStyleBackColor = true;
            this.btnCreateRoom.Click += new System.EventHandler(this.btnCreateRoom_Click);
            // 
            // pnlRightBottom
            // 
            this.pnlRightBottom.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(83)))), ((int)(((byte)(83)))), ((int)(((byte)(83)))));
            this.pnlRightBottom.Controls.Add(this.pbMessages);
            this.pnlRightBottom.Location = new System.Drawing.Point(608, 618);
            this.pnlRightBottom.Name = "pnlRightBottom";
            this.pnlRightBottom.Size = new System.Drawing.Size(254, 38);
            this.pnlRightBottom.TabIndex = 6;
            // 
            // pbMessages
            // 
            this.pbMessages.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(83)))), ((int)(((byte)(83)))), ((int)(((byte)(83)))));
            this.pbMessages.Dock = System.Windows.Forms.DockStyle.Left;
            this.pbMessages.Image = global::ChessClient.Properties.Resources.message;
            this.pbMessages.InitialImage = null;
            this.pbMessages.Location = new System.Drawing.Point(0, 0);
            this.pbMessages.Name = "pbMessages";
            this.pbMessages.Size = new System.Drawing.Size(35, 38);
            this.pbMessages.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pbMessages.TabIndex = 0;
            this.pbMessages.TabStop = false;
            this.pbMessages.Click += new System.EventHandler(this.pbMessages_Click);
            // 
            // pnlMessages
            // 
            this.pnlMessages.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlMessages.Controls.Add(this.pnlChats);
            this.pnlMessages.Controls.Add(this.pnlChat);
            this.pnlMessages.Location = new System.Drawing.Point(109, 280);
            this.pnlMessages.Name = "pnlMessages";
            this.pnlMessages.Size = new System.Drawing.Size(500, 375);
            this.pnlMessages.TabIndex = 7;
            this.pnlMessages.Visible = false;
            // 
            // pnlChats
            // 
            this.pnlChats.AutoScroll = true;
            this.pnlChats.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(38)))), ((int)(((byte)(38)))), ((int)(((byte)(38)))));
            this.pnlChats.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.pnlChats.Location = new System.Drawing.Point(0, 0);
            this.pnlChats.Margin = new System.Windows.Forms.Padding(0);
            this.pnlChats.Name = "pnlChats";
            this.pnlChats.Size = new System.Drawing.Size(196, 375);
            this.pnlChats.TabIndex = 0;
            // 
            // pnlChat
            // 
            this.pnlChat.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(23)))), ((int)(((byte)(23)))), ((int)(((byte)(23)))));
            this.pnlChat.Location = new System.Drawing.Point(196, 0);
            this.pnlChat.Margin = new System.Windows.Forms.Padding(0);
            this.pnlChat.Name = "pnlChat";
            this.pnlChat.Size = new System.Drawing.Size(300, 375);
            this.pnlChat.TabIndex = 1;
            // 
            // pnlProfile
            // 
            this.pnlProfile.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(216)))), ((int)(((byte)(216)))), ((int)(((byte)(216)))));
            this.pnlProfile.Location = new System.Drawing.Point(0, 99);
            this.pnlProfile.Name = "pnlProfile";
            this.pnlProfile.Size = new System.Drawing.Size(608, 556);
            this.pnlProfile.TabIndex = 0;
            this.pnlProfile.Tag = "Profile";
            // 
            // pnlRooms
            // 
            this.pnlRooms.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(216)))), ((int)(((byte)(216)))), ((int)(((byte)(216)))));
            this.pnlRooms.Controls.Add(this.pnlCreateRoom);
            this.pnlRooms.Controls.Add(this.pnlRoomList);
            this.pnlRooms.Location = new System.Drawing.Point(0, 99);
            this.pnlRooms.Name = "pnlRooms";
            this.pnlRooms.Size = new System.Drawing.Size(608, 556);
            this.pnlRooms.TabIndex = 0;
            this.pnlRooms.Tag = "Rooms";
            // 
            // pnlCreateRoom
            // 
            this.pnlCreateRoom.Controls.Add(this.btnCreateRoom);
            this.pnlCreateRoom.Location = new System.Drawing.Point(3, 3);
            this.pnlCreateRoom.Name = "pnlCreateRoom";
            this.pnlCreateRoom.Size = new System.Drawing.Size(606, 42);
            this.pnlCreateRoom.TabIndex = 0;
            // 
            // pnlRoomList
            // 
            this.pnlRoomList.AutoScroll = true;
            this.pnlRoomList.Location = new System.Drawing.Point(3, 51);
            this.pnlRoomList.Name = "pnlRoomList";
            this.pnlRoomList.Size = new System.Drawing.Size(608, 506);
            this.pnlRoomList.TabIndex = 8;
            // 
            // pnlFriends
            // 
            this.pnlFriends.AutoScroll = true;
            this.pnlFriends.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(29)))), ((int)(((byte)(29)))), ((int)(((byte)(29)))));
            this.pnlFriends.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.pnlFriends.Location = new System.Drawing.Point(609, 101);
            this.pnlFriends.Name = "pnlFriends";
            this.pnlFriends.Size = new System.Drawing.Size(254, 516);
            this.pnlFriends.TabIndex = 0;
            // 
            // pnlCreatingRoom
            // 
            this.pnlCreatingRoom.Controls.Add(this.panel2);
            this.pnlCreatingRoom.Controls.Add(this.panel1);
            this.pnlCreatingRoom.Controls.Add(this.btnSelect);
            this.pnlCreatingRoom.Controls.Add(this.btnInvite);
            this.pnlCreatingRoom.Controls.Add(this.txtRoomName);
            this.pnlCreatingRoom.Controls.Add(this.label1);
            this.pnlCreatingRoom.Controls.Add(this.btnCreate);
            this.pnlCreatingRoom.Location = new System.Drawing.Point(0, 99);
            this.pnlCreatingRoom.Name = "pnlCreatingRoom";
            this.pnlCreatingRoom.Size = new System.Drawing.Size(608, 556);
            this.pnlCreatingRoom.TabIndex = 0;
            this.pnlCreatingRoom.Visible = false;
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.White;
            this.panel2.Controls.Add(this.lblSideRight);
            this.panel2.Controls.Add(this.lblWhiteSide);
            this.panel2.Location = new System.Drawing.Point(294, 53);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(275, 67);
            this.panel2.TabIndex = 7;
            // 
            // lblSideRight
            // 
            this.lblSideRight.AutoSize = true;
            this.lblSideRight.Font = new System.Drawing.Font("Gadugi", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSideRight.ForeColor = System.Drawing.Color.Black;
            this.lblSideRight.Location = new System.Drawing.Point(3, 36);
            this.lblSideRight.Name = "lblSideRight";
            this.lblSideRight.Size = new System.Drawing.Size(105, 21);
            this.lblSideRight.TabIndex = 3;
            this.lblSideRight.Text = "White Side";
            // 
            // lblWhiteSide
            // 
            this.lblWhiteSide.AutoSize = true;
            this.lblWhiteSide.Font = new System.Drawing.Font("Gadugi", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblWhiteSide.ForeColor = System.Drawing.Color.Black;
            this.lblWhiteSide.Location = new System.Drawing.Point(3, 13);
            this.lblWhiteSide.Name = "lblWhiteSide";
            this.lblWhiteSide.Size = new System.Drawing.Size(25, 21);
            this.lblWhiteSide.TabIndex = 2;
            this.lblWhiteSide.Text = "...";
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(42)))), ((int)(((byte)(42)))), ((int)(((byte)(42)))));
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.lblBlackSide);
            this.panel1.Location = new System.Drawing.Point(13, 53);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(275, 67);
            this.panel1.TabIndex = 6;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Gadugi", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.White;
            this.label3.Location = new System.Drawing.Point(165, 36);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(100, 21);
            this.label3.TabIndex = 1;
            this.label3.Text = "Black Side";
            // 
            // lblBlackSide
            // 
            this.lblBlackSide.AutoSize = true;
            this.lblBlackSide.Font = new System.Drawing.Font("Gadugi", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblBlackSide.ForeColor = System.Drawing.Color.White;
            this.lblBlackSide.Location = new System.Drawing.Point(112, 13);
            this.lblBlackSide.Name = "lblBlackSide";
            this.lblBlackSide.Size = new System.Drawing.Size(145, 21);
            this.lblBlackSide.TabIndex = 0;
            this.lblBlackSide.Text = "{{ User Name }}";
            // 
            // btnSelect
            // 
            this.btnSelect.Font = new System.Drawing.Font("Gadugi", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSelect.Location = new System.Drawing.Point(290, 160);
            this.btnSelect.Name = "btnSelect";
            this.btnSelect.Size = new System.Drawing.Size(279, 32);
            this.btnSelect.TabIndex = 5;
            this.btnSelect.Text = "Select the Other Side";
            this.btnSelect.UseVisualStyleBackColor = true;
            this.btnSelect.Click += new System.EventHandler(this.btnSelect_Click);
            // 
            // btnInvite
            // 
            this.btnInvite.Font = new System.Drawing.Font("Gadugi", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnInvite.Location = new System.Drawing.Point(290, 124);
            this.btnInvite.Name = "btnInvite";
            this.btnInvite.Size = new System.Drawing.Size(279, 32);
            this.btnInvite.TabIndex = 4;
            this.btnInvite.Text = "Invite a Friend";
            this.btnInvite.UseVisualStyleBackColor = true;
            // 
            // txtRoomName
            // 
            this.txtRoomName.Location = new System.Drawing.Point(294, 22);
            this.txtRoomName.Name = "txtRoomName";
            this.txtRoomName.Size = new System.Drawing.Size(275, 20);
            this.txtRoomName.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(116)))), ((int)(((byte)(112)))), ((int)(((byte)(112)))));
            this.label1.Font = new System.Drawing.Font("Gadugi", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(13, 18);
            this.label1.Name = "label1";
            this.label1.Padding = new System.Windows.Forms.Padding(5, 5, 150, 5);
            this.label1.Size = new System.Drawing.Size(275, 31);
            this.label1.TabIndex = 0;
            this.label1.Text = "Room Name";
            // 
            // btnCreate
            // 
            this.btnCreate.Font = new System.Drawing.Font("Gadugi", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCreate.Location = new System.Drawing.Point(290, 504);
            this.btnCreate.Name = "btnCreate";
            this.btnCreate.Size = new System.Drawing.Size(279, 32);
            this.btnCreate.TabIndex = 5;
            this.btnCreate.Text = "Create the Room";
            this.btnCreate.UseVisualStyleBackColor = true;
            this.btnCreate.Click += new System.EventHandler(this.btnCreate_Click);
            // 
            // ClientForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(216)))), ((int)(((byte)(216)))), ((int)(((byte)(216)))));
            this.ClientSize = new System.Drawing.Size(863, 655);
            this.Controls.Add(this.pnlMessages);
            this.Controls.Add(this.pnlCreatingRoom);
            this.Controls.Add(this.pnlHeader);
            this.Controls.Add(this.pnlRightBottom);
            this.Controls.Add(this.pnlFriends);
            this.Controls.Add(this.pnlHome);
            this.Controls.Add(this.pnlProfile);
            this.Controls.Add(this.pnlRooms);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "ClientForm";
            this.Text = "Play Online Chess";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ClientForm_FormClosing);
            this.pnlHeader.ResumeLayout(false);
            this.pnlNavigation.ResumeLayout(false);
            this.pnlNavigation.PerformLayout();
            this.pnlUserInfo.ResumeLayout(false);
            this.pnlUserInfo.PerformLayout();
            this.pnlRightBottom.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pbMessages)).EndInit();
            this.pnlMessages.ResumeLayout(false);
            this.pnlRooms.ResumeLayout(false);
            this.pnlCreateRoom.ResumeLayout(false);
            this.pnlCreatingRoom.ResumeLayout(false);
            this.pnlCreatingRoom.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label lblRooms;
        private System.Windows.Forms.Label lblHome;
        private System.Windows.Forms.Label lblProfile;
        private System.Windows.Forms.Panel pnlHeader;
        private System.Windows.Forms.Panel pnlNavigation;
        private System.Windows.Forms.Panel pnlUserInfo;
        private System.Windows.Forms.Label lblUserName;
        private System.Windows.Forms.Label lblFullName;
        private System.Windows.Forms.Panel pnlHome;
        private System.Windows.Forms.Panel pnlProfile;
        private System.Windows.Forms.FlowLayoutPanel pnlRooms;
        private System.Windows.Forms.FlowLayoutPanel pnlFriends;
        private System.Windows.Forms.PictureBox pbMessages;
        private System.Windows.Forms.Panel pnlRightBottom;
        private System.Windows.Forms.FlowLayoutPanel pnlMessages;
        private System.Windows.Forms.FlowLayoutPanel pnlChats;
        private System.Windows.Forms.Panel pnlChat;
        private System.Windows.Forms.Panel pnlCreateRoom;
        private System.Windows.Forms.Button btnCreateRoom;
        private System.Windows.Forms.Panel pnlCreatingRoom;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtRoomName;
        private System.Windows.Forms.Button btnInvite;
        private System.Windows.Forms.Button btnSelect;
        private System.Windows.Forms.Button btnCreate;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label lblBlackSide;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label lblSideRight;
        private System.Windows.Forms.Label lblWhiteSide;
        private System.Windows.Forms.Panel pnlRoomList;
    }
}