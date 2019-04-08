namespace SPExam
{
    partial class Form1
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
            this.KeyLoggerBrowse = new System.Windows.Forms.Button();
            this.KeyLoggerText = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.KeyLoggerStart = new System.Windows.Forms.Button();
            this.CloseButton = new System.Windows.Forms.Button();
            this.AppLoggerStart = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.AppLoggerText = new System.Windows.Forms.TextBox();
            this.AppLoggerBrowse = new System.Windows.Forms.Button();
            this.ModerButton = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.ReadText = new System.Windows.Forms.TextBox();
            this.WordsListButton = new System.Windows.Forms.Button();
            this.KeyLoggerStopButton = new System.Windows.Forms.Button();
            this.AppLoggerStopButoon = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // KeyLoggerBrowse
            // 
            this.KeyLoggerBrowse.Location = new System.Drawing.Point(494, 57);
            this.KeyLoggerBrowse.Name = "KeyLoggerBrowse";
            this.KeyLoggerBrowse.Size = new System.Drawing.Size(75, 23);
            this.KeyLoggerBrowse.TabIndex = 0;
            this.KeyLoggerBrowse.Text = "Browse";
            this.KeyLoggerBrowse.UseVisualStyleBackColor = true;
            this.KeyLoggerBrowse.Click += new System.EventHandler(this.KeyLoggerBrowse_Click);
            // 
            // KeyLoggerText
            // 
            this.KeyLoggerText.Location = new System.Drawing.Point(139, 58);
            this.KeyLoggerText.Name = "KeyLoggerText";
            this.KeyLoggerText.Size = new System.Drawing.Size(337, 22);
            this.KeyLoggerText.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(23, 61);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(77, 17);
            this.label1.TabIndex = 2;
            this.label1.Text = "KeyLogger";
            // 
            // KeyLoggerStart
            // 
            this.KeyLoggerStart.Location = new System.Drawing.Point(575, 57);
            this.KeyLoggerStart.Name = "KeyLoggerStart";
            this.KeyLoggerStart.Size = new System.Drawing.Size(75, 23);
            this.KeyLoggerStart.TabIndex = 3;
            this.KeyLoggerStart.Text = "Start";
            this.KeyLoggerStart.UseVisualStyleBackColor = true;
            this.KeyLoggerStart.Click += new System.EventHandler(this.KeyLoggerStart_Click);
            // 
            // CloseButton
            // 
            this.CloseButton.Location = new System.Drawing.Point(698, 415);
            this.CloseButton.Name = "CloseButton";
            this.CloseButton.Size = new System.Drawing.Size(75, 23);
            this.CloseButton.TabIndex = 4;
            this.CloseButton.Text = "Close";
            this.CloseButton.UseVisualStyleBackColor = true;
            this.CloseButton.Click += new System.EventHandler(this.CloseButton_Click);
            // 
            // AppLoggerStart
            // 
            this.AppLoggerStart.Location = new System.Drawing.Point(575, 128);
            this.AppLoggerStart.Name = "AppLoggerStart";
            this.AppLoggerStart.Size = new System.Drawing.Size(75, 23);
            this.AppLoggerStart.TabIndex = 8;
            this.AppLoggerStart.Text = "Start";
            this.AppLoggerStart.UseVisualStyleBackColor = true;
            this.AppLoggerStart.Click += new System.EventHandler(this.AppLoggerStart_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(23, 131);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(78, 17);
            this.label2.TabIndex = 7;
            this.label2.Text = "AppLogger";
            // 
            // AppLoggerText
            // 
            this.AppLoggerText.Location = new System.Drawing.Point(139, 128);
            this.AppLoggerText.Name = "AppLoggerText";
            this.AppLoggerText.Size = new System.Drawing.Size(337, 22);
            this.AppLoggerText.TabIndex = 6;
            // 
            // AppLoggerBrowse
            // 
            this.AppLoggerBrowse.Location = new System.Drawing.Point(494, 128);
            this.AppLoggerBrowse.Name = "AppLoggerBrowse";
            this.AppLoggerBrowse.Size = new System.Drawing.Size(75, 23);
            this.AppLoggerBrowse.TabIndex = 5;
            this.AppLoggerBrowse.Text = "Browse";
            this.AppLoggerBrowse.UseVisualStyleBackColor = true;
            this.AppLoggerBrowse.Click += new System.EventHandler(this.AppLoggerBrowse_Click);
            // 
            // ModerButton
            // 
            this.ModerButton.Location = new System.Drawing.Point(26, 175);
            this.ModerButton.Name = "ModerButton";
            this.ModerButton.Size = new System.Drawing.Size(164, 52);
            this.ModerButton.TabIndex = 9;
            this.ModerButton.Text = "Включить модерацию";
            this.ModerButton.UseVisualStyleBackColor = true;
            this.ModerButton.Click += new System.EventHandler(this.ModerButton_Click);
            // 
            // button1
            // 
            this.button1.Enabled = false;
            this.button1.Location = new System.Drawing.Point(196, 175);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(164, 52);
            this.button1.TabIndex = 10;
            this.button1.Text = "Open Key Log";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // ReadText
            // 
            this.ReadText.Location = new System.Drawing.Point(26, 249);
            this.ReadText.Multiline = true;
            this.ReadText.Name = "ReadText";
            this.ReadText.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.ReadText.Size = new System.Drawing.Size(747, 149);
            this.ReadText.TabIndex = 11;
            // 
            // WordsListButton
            // 
            this.WordsListButton.Enabled = false;
            this.WordsListButton.Location = new System.Drawing.Point(366, 175);
            this.WordsListButton.Name = "WordsListButton";
            this.WordsListButton.Size = new System.Drawing.Size(164, 52);
            this.WordsListButton.TabIndex = 13;
            this.WordsListButton.Text = "Open Words List";
            this.WordsListButton.UseVisualStyleBackColor = true;
            this.WordsListButton.Click += new System.EventHandler(this.WordsListButton_Click);
            // 
            // KeyLoggerStopButton
            // 
            this.KeyLoggerStopButton.Enabled = false;
            this.KeyLoggerStopButton.Location = new System.Drawing.Point(656, 57);
            this.KeyLoggerStopButton.Name = "KeyLoggerStopButton";
            this.KeyLoggerStopButton.Size = new System.Drawing.Size(75, 23);
            this.KeyLoggerStopButton.TabIndex = 14;
            this.KeyLoggerStopButton.Text = "Stop";
            this.KeyLoggerStopButton.UseVisualStyleBackColor = true;
            this.KeyLoggerStopButton.Click += new System.EventHandler(this.KeyLoggerStopButton_Click);
            // 
            // AppLoggerStopButoon
            // 
            this.AppLoggerStopButoon.Enabled = false;
            this.AppLoggerStopButoon.Location = new System.Drawing.Point(656, 128);
            this.AppLoggerStopButoon.Name = "AppLoggerStopButoon";
            this.AppLoggerStopButoon.Size = new System.Drawing.Size(75, 23);
            this.AppLoggerStopButoon.TabIndex = 15;
            this.AppLoggerStopButoon.Text = "Stop";
            this.AppLoggerStopButoon.UseVisualStyleBackColor = true;
            this.AppLoggerStopButoon.Click += new System.EventHandler(this.AppLoggerStopButoon_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.AppLoggerStopButoon);
            this.Controls.Add(this.KeyLoggerStopButton);
            this.Controls.Add(this.WordsListButton);
            this.Controls.Add(this.ReadText);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.ModerButton);
            this.Controls.Add(this.AppLoggerStart);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.AppLoggerText);
            this.Controls.Add(this.AppLoggerBrowse);
            this.Controls.Add(this.CloseButton);
            this.Controls.Add(this.KeyLoggerStart);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.KeyLoggerText);
            this.Controls.Add(this.KeyLoggerBrowse);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button KeyLoggerBrowse;
        private System.Windows.Forms.TextBox KeyLoggerText;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button KeyLoggerStart;
        private System.Windows.Forms.Button CloseButton;
        private System.Windows.Forms.Button AppLoggerStart;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox AppLoggerText;
        private System.Windows.Forms.Button AppLoggerBrowse;
        private System.Windows.Forms.Button ModerButton;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox ReadText;
        private System.Windows.Forms.Button WordsListButton;
        private System.Windows.Forms.Button KeyLoggerStopButton;
        private System.Windows.Forms.Button AppLoggerStopButoon;
    }
}