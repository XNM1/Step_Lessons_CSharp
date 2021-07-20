namespace Client
{
    partial class Form1
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.pictureBoxFrame = new System.Windows.Forms.PictureBox();
            this.buttonRecive = new System.Windows.Forms.Button();
            this.buttonStop = new System.Windows.Forms.Button();
            this.textBoxServer = new System.Windows.Forms.TextBox();
            this.labelServer = new System.Windows.Forms.Label();
            this.textBoxPort = new System.Windows.Forms.TextBox();
            this.labelInterval = new System.Windows.Forms.Label();
            this.textBoxInterval = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxFrame)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBoxFrame
            // 
            this.pictureBoxFrame.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBoxFrame.Location = new System.Drawing.Point(16, 15);
            this.pictureBoxFrame.Margin = new System.Windows.Forms.Padding(4);
            this.pictureBoxFrame.Name = "pictureBoxFrame";
            this.pictureBoxFrame.Size = new System.Drawing.Size(709, 446);
            this.pictureBoxFrame.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBoxFrame.TabIndex = 0;
            this.pictureBoxFrame.TabStop = false;
            // 
            // buttonRecive
            // 
            this.buttonRecive.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonRecive.Location = new System.Drawing.Point(177, 511);
            this.buttonRecive.Margin = new System.Windows.Forms.Padding(4);
            this.buttonRecive.Name = "buttonRecive";
            this.buttonRecive.Size = new System.Drawing.Size(100, 28);
            this.buttonRecive.TabIndex = 1;
            this.buttonRecive.Text = "Recive";
            this.buttonRecive.UseVisualStyleBackColor = true;
            this.buttonRecive.Click += new System.EventHandler(this.buttonRecive_Click);
            // 
            // buttonStop
            // 
            this.buttonStop.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonStop.Location = new System.Drawing.Point(469, 511);
            this.buttonStop.Margin = new System.Windows.Forms.Padding(4);
            this.buttonStop.Name = "buttonStop";
            this.buttonStop.Size = new System.Drawing.Size(100, 28);
            this.buttonStop.TabIndex = 2;
            this.buttonStop.Text = "Stop";
            this.buttonStop.UseVisualStyleBackColor = true;
            this.buttonStop.Click += new System.EventHandler(this.buttonStop_Click);
            // 
            // textBoxServer
            // 
            this.textBoxServer.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.textBoxServer.Location = new System.Drawing.Point(149, 474);
            this.textBoxServer.Margin = new System.Windows.Forms.Padding(4);
            this.textBoxServer.Name = "textBoxServer";
            this.textBoxServer.Size = new System.Drawing.Size(176, 22);
            this.textBoxServer.TabIndex = 3;
            this.textBoxServer.Text = "127.0.0.1";
            // 
            // labelServer
            // 
            this.labelServer.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.labelServer.AutoSize = true;
            this.labelServer.Location = new System.Drawing.Point(87, 478);
            this.labelServer.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelServer.Name = "labelServer";
            this.labelServer.Size = new System.Drawing.Size(54, 17);
            this.labelServer.TabIndex = 4;
            this.labelServer.Text = "Server:";
            // 
            // textBoxPort
            // 
            this.textBoxPort.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.textBoxPort.Location = new System.Drawing.Point(335, 474);
            this.textBoxPort.Margin = new System.Windows.Forms.Padding(4);
            this.textBoxPort.Name = "textBoxPort";
            this.textBoxPort.Size = new System.Drawing.Size(57, 22);
            this.textBoxPort.TabIndex = 5;
            this.textBoxPort.Text = "888";
            // 
            // labelInterval
            // 
            this.labelInterval.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.labelInterval.AutoSize = true;
            this.labelInterval.Location = new System.Drawing.Point(420, 478);
            this.labelInterval.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelInterval.Name = "labelInterval";
            this.labelInterval.Size = new System.Drawing.Size(58, 17);
            this.labelInterval.TabIndex = 7;
            this.labelInterval.Text = "Interval:";
            // 
            // textBoxInterval
            // 
            this.textBoxInterval.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxInterval.Location = new System.Drawing.Point(483, 474);
            this.textBoxInterval.Margin = new System.Windows.Forms.Padding(4);
            this.textBoxInterval.Name = "textBoxInterval";
            this.textBoxInterval.Size = new System.Drawing.Size(176, 22);
            this.textBoxInterval.TabIndex = 6;
            this.textBoxInterval.Text = "500";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(741, 554);
            this.Controls.Add(this.labelInterval);
            this.Controls.Add(this.textBoxInterval);
            this.Controls.Add(this.textBoxPort);
            this.Controls.Add(this.labelServer);
            this.Controls.Add(this.textBoxServer);
            this.Controls.Add(this.buttonStop);
            this.Controls.Add(this.buttonRecive);
            this.Controls.Add(this.pictureBoxFrame);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "Form1";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxFrame)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBoxFrame;
        private System.Windows.Forms.Button buttonRecive;
        private System.Windows.Forms.Button buttonStop;
        private System.Windows.Forms.TextBox textBoxServer;
        private System.Windows.Forms.Label labelServer;
        private System.Windows.Forms.TextBox textBoxPort;
        private System.Windows.Forms.Label labelInterval;
        private System.Windows.Forms.TextBox textBoxInterval;
    }
}