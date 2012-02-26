namespace NAudioTutorial5
{
    partial class NAudioTutorial5
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
            waveOutDevice.Dispose();

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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.cmbStop = new System.Windows.Forms.Button();
            this.cmbRecord = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.cmbPauseDirect = new System.Windows.Forms.Button();
            this.cmbStopDirect = new System.Windows.Forms.Button();
            this.cmbRecordDirect = new System.Windows.Forms.Button();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.chkReverse = new System.Windows.Forms.CheckBox();
            this.cmbPlay = new System.Windows.Forms.Button();
            this.cmbOpen = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.cmbStop);
            this.groupBox1.Controls.Add(this.cmbRecord);
            this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(12, 118);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(302, 74);
            this.groupBox1.TabIndex = 11;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Record all availbale audio from the Sound Card";
            // 
            // cmbStop
            // 
            this.cmbStop.Enabled = false;
            this.cmbStop.Location = new System.Drawing.Point(155, 29);
            this.cmbStop.Name = "cmbStop";
            this.cmbStop.Size = new System.Drawing.Size(75, 23);
            this.cmbStop.TabIndex = 10;
            this.cmbStop.Text = "Stop";
            this.cmbStop.UseVisualStyleBackColor = true;
            this.cmbStop.Click += new System.EventHandler(this.cmbStop_Click);
            // 
            // cmbRecord
            // 
            this.cmbRecord.Location = new System.Drawing.Point(72, 29);
            this.cmbRecord.Name = "cmbRecord";
            this.cmbRecord.Size = new System.Drawing.Size(75, 23);
            this.cmbRecord.TabIndex = 9;
            this.cmbRecord.Text = "Record";
            this.cmbRecord.UseVisualStyleBackColor = true;
            this.cmbRecord.Click += new System.EventHandler(this.cmbRecord_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.cmbPauseDirect);
            this.groupBox2.Controls.Add(this.cmbStopDirect);
            this.groupBox2.Controls.Add(this.cmbRecordDirect);
            this.groupBox2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox2.Location = new System.Drawing.Point(12, 220);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(302, 85);
            this.groupBox2.TabIndex = 12;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Record only audio mixed and played by NAudio";
            // 
            // cmbPauseDirect
            // 
            this.cmbPauseDirect.Enabled = false;
            this.cmbPauseDirect.Location = new System.Drawing.Point(115, 39);
            this.cmbPauseDirect.Name = "cmbPauseDirect";
            this.cmbPauseDirect.Size = new System.Drawing.Size(75, 23);
            this.cmbPauseDirect.TabIndex = 10;
            this.cmbPauseDirect.Text = "Pause";
            this.cmbPauseDirect.UseVisualStyleBackColor = true;
            this.cmbPauseDirect.Click += new System.EventHandler(this.cmbPauseDirect_Click);
            // 
            // cmbStopDirect
            // 
            this.cmbStopDirect.Enabled = false;
            this.cmbStopDirect.Location = new System.Drawing.Point(196, 39);
            this.cmbStopDirect.Name = "cmbStopDirect";
            this.cmbStopDirect.Size = new System.Drawing.Size(75, 23);
            this.cmbStopDirect.TabIndex = 9;
            this.cmbStopDirect.Text = "Stop";
            this.cmbStopDirect.UseVisualStyleBackColor = true;
            this.cmbStopDirect.Click += new System.EventHandler(this.cmbStopDirect_Click);
            // 
            // cmbRecordDirect
            // 
            this.cmbRecordDirect.Location = new System.Drawing.Point(34, 39);
            this.cmbRecordDirect.Name = "cmbRecordDirect";
            this.cmbRecordDirect.Size = new System.Drawing.Size(75, 23);
            this.cmbRecordDirect.TabIndex = 8;
            this.cmbRecordDirect.Text = "Record";
            this.cmbRecordDirect.UseVisualStyleBackColor = true;
            this.cmbRecordDirect.Click += new System.EventHandler(this.cmbRecordDirect_Click);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.chkReverse);
            this.groupBox3.Controls.Add(this.cmbPlay);
            this.groupBox3.Controls.Add(this.cmbOpen);
            this.groupBox3.Location = new System.Drawing.Point(12, 12);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(302, 78);
            this.groupBox3.TabIndex = 13;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Open an Audio File for Playback through NAudio";
            // 
            // chkReverse
            // 
            this.chkReverse.AutoSize = true;
            this.chkReverse.Location = new System.Drawing.Point(115, 35);
            this.chkReverse.Name = "chkReverse";
            this.chkReverse.Size = new System.Drawing.Size(66, 17);
            this.chkReverse.TabIndex = 13;
            this.chkReverse.Text = "Reverse";
            this.chkReverse.UseVisualStyleBackColor = true;
            // 
            // cmbPlay
            // 
            this.cmbPlay.Location = new System.Drawing.Point(196, 31);
            this.cmbPlay.Name = "cmbPlay";
            this.cmbPlay.Size = new System.Drawing.Size(75, 23);
            this.cmbPlay.TabIndex = 12;
            this.cmbPlay.Text = "Play";
            this.cmbPlay.UseVisualStyleBackColor = true;
            this.cmbPlay.Click += new System.EventHandler(this.cmbPlay_Click);
            // 
            // cmbOpen
            // 
            this.cmbOpen.Location = new System.Drawing.Point(34, 31);
            this.cmbOpen.Name = "cmbOpen";
            this.cmbOpen.Size = new System.Drawing.Size(75, 23);
            this.cmbOpen.TabIndex = 11;
            this.cmbOpen.Text = "Open";
            this.cmbOpen.UseVisualStyleBackColor = true;
            this.cmbOpen.Click += new System.EventHandler(this.cmbOpen_Click);
            // 
            // NAudioTutorial5
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(329, 330);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Name = "NAudioTutorial5";
            this.Text = "NAudioTutorial5";
            this.Load += new System.EventHandler(this.NAudioTutorial5_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button cmbStop;
        private System.Windows.Forms.Button cmbRecord;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button cmbPauseDirect;
        private System.Windows.Forms.Button cmbStopDirect;
        private System.Windows.Forms.Button cmbRecordDirect;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.CheckBox chkReverse;
        private System.Windows.Forms.Button cmbPlay;
        private System.Windows.Forms.Button cmbOpen;
    }
}

