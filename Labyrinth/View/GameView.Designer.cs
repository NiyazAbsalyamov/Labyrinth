namespace Labyrinth.View
{
    partial class GameView
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.chatRichTextBox = new System.Windows.Forms.RichTextBox();
            this.sendMessageButton = new System.Windows.Forms.Button();
            this.exitButton = new System.Windows.Forms.Button();
            this.timeNameLabel = new System.Windows.Forms.Label();
            this.timeLabel = new System.Windows.Forms.Label();
            this.sendMessagesTextBox = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.panel1.Location = new System.Drawing.Point(13, 13);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(296, 259);
            this.panel1.TabIndex = 0;
            // 
            // chatRichTextBox
            // 
            this.chatRichTextBox.Location = new System.Drawing.Point(13, 279);
            this.chatRichTextBox.Name = "chatRichTextBox";
            this.chatRichTextBox.Size = new System.Drawing.Size(296, 83);
            this.chatRichTextBox.TabIndex = 1;
            this.chatRichTextBox.Text = "";
            // 
            // sendMessageButton
            // 
            this.sendMessageButton.Location = new System.Drawing.Point(13, 394);
            this.sendMessageButton.Name = "sendMessageButton";
            this.sendMessageButton.Size = new System.Drawing.Size(84, 23);
            this.sendMessageButton.TabIndex = 2;
            this.sendMessageButton.Text = "Отправить";
            this.sendMessageButton.UseVisualStyleBackColor = true;
            // 
            // exitButton
            // 
            this.exitButton.Location = new System.Drawing.Point(217, 394);
            this.exitButton.Name = "exitButton";
            this.exitButton.Size = new System.Drawing.Size(92, 23);
            this.exitButton.TabIndex = 3;
            this.exitButton.Text = "Выйти";
            this.exitButton.UseVisualStyleBackColor = true;
            // 
            // timeNameLabel
            // 
            this.timeNameLabel.AutoSize = true;
            this.timeNameLabel.Location = new System.Drawing.Point(316, 13);
            this.timeNameLabel.Name = "timeNameLabel";
            this.timeNameLabel.Size = new System.Drawing.Size(43, 13);
            this.timeNameLabel.TabIndex = 4;
            this.timeNameLabel.Text = "Время:";
            // 
            // timeLabel
            // 
            this.timeLabel.AutoSize = true;
            this.timeLabel.Location = new System.Drawing.Point(378, 13);
            this.timeLabel.Name = "timeLabel";
            this.timeLabel.Size = new System.Drawing.Size(49, 13);
            this.timeLabel.TabIndex = 5;
            this.timeLabel.Text = "00:00:00";
            // 
            // sendMessagesTextBox
            // 
            this.sendMessagesTextBox.Location = new System.Drawing.Point(12, 368);
            this.sendMessagesTextBox.Name = "sendMessagesTextBox";
            this.sendMessagesTextBox.Size = new System.Drawing.Size(297, 20);
            this.sendMessagesTextBox.TabIndex = 6;
            // 
            // ConnectView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(439, 422);
            this.Controls.Add(this.sendMessagesTextBox);
            this.Controls.Add(this.timeLabel);
            this.Controls.Add(this.timeNameLabel);
            this.Controls.Add(this.exitButton);
            this.Controls.Add(this.sendMessageButton);
            this.Controls.Add(this.chatRichTextBox);
            this.Controls.Add(this.panel1);
            this.Name = "ConnectView";
            this.Text = "Connection to server";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.RichTextBox chatRichTextBox;
        private System.Windows.Forms.Button sendMessageButton;
        private System.Windows.Forms.Button exitButton;
        private System.Windows.Forms.Label timeNameLabel;
        private System.Windows.Forms.Label timeLabel;
        private System.Windows.Forms.TextBox sendMessagesTextBox;

    }
}

