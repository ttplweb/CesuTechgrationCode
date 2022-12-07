
namespace TechGration
{
    partial class TechgrationProcessForOne
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TechgrationProcessForOne));
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.labFeederID1 = new System.Windows.Forms.Label();
            this.lblStatus1 = new System.Windows.Forms.Label();
            this.labFeederName1 = new System.Windows.Forms.Label();
            this.labSubstation1 = new System.Windows.Forms.Label();
            this.labdivision1 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.lblTime = new System.Windows.Forms.Label();
            this.lblcompletefeeder = new System.Windows.Forms.Label();
            this.lblselectedfeeder = new System.Windows.Forms.Label();
            this.lblTotalfeeder = new System.Windows.Forms.Label();
            this.lblPersentageStatus = new System.Windows.Forms.Label();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.pictureBox3 = new System.Windows.Forms.PictureBox();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.labFeederID1);
            this.groupBox2.Controls.Add(this.lblStatus1);
            this.groupBox2.Controls.Add(this.labFeederName1);
            this.groupBox2.Controls.Add(this.labSubstation1);
            this.groupBox2.Controls.Add(this.labdivision1);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Location = new System.Drawing.Point(211, 151);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(434, 165);
            this.groupBox2.TabIndex = 17;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Running Feeder";
            // 
            // labFeederID1
            // 
            this.labFeederID1.AutoSize = true;
            this.labFeederID1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labFeederID1.Location = new System.Drawing.Point(13, 137);
            this.labFeederID1.Name = "labFeederID1";
            this.labFeederID1.Size = new System.Drawing.Size(66, 13);
            this.labFeederID1.TabIndex = 13;
            this.labFeederID1.Text = "FeederID1";
            // 
            // lblStatus1
            // 
            this.lblStatus1.AutoSize = true;
            this.lblStatus1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblStatus1.Location = new System.Drawing.Point(13, 30);
            this.lblStatus1.Name = "lblStatus1";
            this.lblStatus1.Size = new System.Drawing.Size(58, 13);
            this.lblStatus1.TabIndex = 18;
            this.lblStatus1.Text = "Status1 :";
            // 
            // labFeederName1
            // 
            this.labFeederName1.AutoSize = true;
            this.labFeederName1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labFeederName1.Location = new System.Drawing.Point(11, 109);
            this.labFeederName1.Name = "labFeederName1";
            this.labFeederName1.Size = new System.Drawing.Size(85, 13);
            this.labFeederName1.TabIndex = 12;
            this.labFeederName1.Text = "FeederName1";
            // 
            // labSubstation1
            // 
            this.labSubstation1.AutoSize = true;
            this.labSubstation1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labSubstation1.Location = new System.Drawing.Point(13, 84);
            this.labSubstation1.Name = "labSubstation1";
            this.labSubstation1.Size = new System.Drawing.Size(78, 13);
            this.labSubstation1.TabIndex = 11;
            this.labSubstation1.Text = "Substation 1";
            // 
            // labdivision1
            // 
            this.labdivision1.AutoSize = true;
            this.labdivision1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labdivision1.Location = new System.Drawing.Point(13, 58);
            this.labdivision1.Name = "labdivision1";
            this.labdivision1.Size = new System.Drawing.Size(59, 13);
            this.labdivision1.TabIndex = 10;
            this.labdivision1.Text = "Division1";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(13, 34);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(0, 13);
            this.label4.TabIndex = 9;
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(215, 325);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(430, 25);
            this.progressBar1.TabIndex = 18;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.lblTime);
            this.groupBox1.Controls.Add(this.lblcompletefeeder);
            this.groupBox1.Controls.Add(this.lblselectedfeeder);
            this.groupBox1.Controls.Add(this.lblTotalfeeder);
            this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.ForeColor = System.Drawing.SystemColors.MenuHighlight;
            this.groupBox1.Location = new System.Drawing.Point(12, 96);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(180, 136);
            this.groupBox1.TabIndex = 21;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Status";
            // 
            // lblTime
            // 
            this.lblTime.AutoSize = true;
            this.lblTime.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTime.Location = new System.Drawing.Point(18, 111);
            this.lblTime.Name = "lblTime";
            this.lblTime.Size = new System.Drawing.Size(51, 16);
            this.lblTime.TabIndex = 16;
            this.lblTime.Text = "Time :";
            // 
            // lblcompletefeeder
            // 
            this.lblcompletefeeder.AutoSize = true;
            this.lblcompletefeeder.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblcompletefeeder.Location = new System.Drawing.Point(17, 87);
            this.lblcompletefeeder.Name = "lblcompletefeeder";
            this.lblcompletefeeder.Size = new System.Drawing.Size(136, 16);
            this.lblcompletefeeder.TabIndex = 15;
            this.lblcompletefeeder.Text = "Complete Feeder :";
            // 
            // lblselectedfeeder
            // 
            this.lblselectedfeeder.AutoSize = true;
            this.lblselectedfeeder.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblselectedfeeder.Location = new System.Drawing.Point(18, 58);
            this.lblselectedfeeder.Name = "lblselectedfeeder";
            this.lblselectedfeeder.Size = new System.Drawing.Size(132, 16);
            this.lblselectedfeeder.TabIndex = 14;
            this.lblselectedfeeder.Text = "Selected Feeder :";
            // 
            // lblTotalfeeder
            // 
            this.lblTotalfeeder.AutoSize = true;
            this.lblTotalfeeder.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTotalfeeder.Location = new System.Drawing.Point(18, 31);
            this.lblTotalfeeder.Name = "lblTotalfeeder";
            this.lblTotalfeeder.Size = new System.Drawing.Size(106, 16);
            this.lblTotalfeeder.TabIndex = 13;
            this.lblTotalfeeder.Text = "Total Feeder :";
            // 
            // lblPersentageStatus
            // 
            this.lblPersentageStatus.AutoSize = true;
            this.lblPersentageStatus.Font = new System.Drawing.Font("MS Reference Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPersentageStatus.Location = new System.Drawing.Point(106, 325);
            this.lblPersentageStatus.Name = "lblPersentageStatus";
            this.lblPersentageStatus.Size = new System.Drawing.Size(86, 24);
            this.lblPersentageStatus.TabIndex = 22;
            this.lblPersentageStatus.Text = "Status :";
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // backgroundWorker1
            // 
            this.backgroundWorker1.WorkerReportsProgress = true;
            this.backgroundWorker1.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorker1_DoWork);
            this.backgroundWorker1.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.backgroundWorker1_ProgressChanged);
            this.backgroundWorker1.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.backgroundWorker1_RunWorkerCompleted);
            // 
            // pictureBox2
            // 
            this.pictureBox2.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox2.Image")));
            this.pictureBox2.Location = new System.Drawing.Point(12, 267);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(82, 80);
            this.pictureBox2.TabIndex = 24;
            this.pictureBox2.TabStop = false;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(5, 22);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(200, 68);
            this.pictureBox1.TabIndex = 23;
            this.pictureBox1.TabStop = false;
            // 
            // pictureBox3
            // 
            this.pictureBox3.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox3.Image")));
            this.pictureBox3.Location = new System.Drawing.Point(211, 1);
            this.pictureBox3.Name = "pictureBox3";
            this.pictureBox3.Size = new System.Drawing.Size(438, 144);
            this.pictureBox3.TabIndex = 35;
            this.pictureBox3.TabStop = false;
            // 
            // TechgrationProcessForOne
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(653, 359);
            this.Controls.Add(this.pictureBox3);
            this.Controls.Add(this.pictureBox2);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.lblPersentageStatus);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.progressBar1);
            this.Controls.Add(this.groupBox2);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximumSize = new System.Drawing.Size(669, 398);
            this.MinimumSize = new System.Drawing.Size(669, 398);
            this.Name = "TechgrationProcessForOne";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "TechgrationProcess";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.TechgrationProcessForOne_FormClosing);
            this.Load += new System.EventHandler(this.TechgrationProcessForOne_Load);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label labFeederID1;
        private System.Windows.Forms.Label lblStatus1;
        private System.Windows.Forms.Label labFeederName1;
        private System.Windows.Forms.Label labSubstation1;
        private System.Windows.Forms.Label labdivision1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label lblTime;
        private System.Windows.Forms.Label lblcompletefeeder;
        private System.Windows.Forms.Label lblselectedfeeder;
        private System.Windows.Forms.Label lblTotalfeeder;
        private System.Windows.Forms.Label lblPersentageStatus;
        private System.Windows.Forms.Timer timer1;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.PictureBox pictureBox3;
    }
}