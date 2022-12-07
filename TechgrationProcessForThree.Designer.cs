
namespace TechGration
{
    partial class TechgrationProcessForThree
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TechgrationProcessForThree));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.lblTime = new System.Windows.Forms.Label();
            this.lblcompletefeeder = new System.Windows.Forms.Label();
            this.lblselectedfeeder = new System.Windows.Forms.Label();
            this.lblTotalfeeder = new System.Windows.Forms.Label();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.lblStatus2 = new System.Windows.Forms.Label();
            this.labFeederID2 = new System.Windows.Forms.Label();
            this.labFeederName2 = new System.Windows.Forms.Label();
            this.labSubstation2 = new System.Windows.Forms.Label();
            this.labdivision2 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.labFeederID1 = new System.Windows.Forms.Label();
            this.lblStatus1 = new System.Windows.Forms.Label();
            this.labFeederName1 = new System.Windows.Forms.Label();
            this.labSubstation1 = new System.Windows.Forms.Label();
            this.labdivision1 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.labFeederID3 = new System.Windows.Forms.Label();
            this.lblStatus3 = new System.Windows.Forms.Label();
            this.labFeederName3 = new System.Windows.Forms.Label();
            this.labSubstation3 = new System.Windows.Forms.Label();
            this.labdivision3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.lblPersentageStatus = new System.Windows.Forms.Label();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.pictureBox3 = new System.Windows.Forms.PictureBox();
            this.groupBox1.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.lblTime);
            this.groupBox1.Controls.Add(this.lblcompletefeeder);
            this.groupBox1.Controls.Add(this.lblselectedfeeder);
            this.groupBox1.Controls.Add(this.lblTotalfeeder);
            this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.ForeColor = System.Drawing.SystemColors.MenuHighlight;
            this.groupBox1.Location = new System.Drawing.Point(7, 88);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(253, 154);
            this.groupBox1.TabIndex = 22;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Status";
            // 
            // lblTime
            // 
            this.lblTime.AutoSize = true;
            this.lblTime.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTime.Location = new System.Drawing.Point(18, 117);
            this.lblTime.Name = "lblTime";
            this.lblTime.Size = new System.Drawing.Size(51, 16);
            this.lblTime.TabIndex = 16;
            this.lblTime.Text = "Time :";
            // 
            // lblcompletefeeder
            // 
            this.lblcompletefeeder.AutoSize = true;
            this.lblcompletefeeder.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblcompletefeeder.Location = new System.Drawing.Point(16, 91);
            this.lblcompletefeeder.Name = "lblcompletefeeder";
            this.lblcompletefeeder.Size = new System.Drawing.Size(136, 16);
            this.lblcompletefeeder.TabIndex = 15;
            this.lblcompletefeeder.Text = "Complete Feeder :";
            // 
            // lblselectedfeeder
            // 
            this.lblselectedfeeder.AutoSize = true;
            this.lblselectedfeeder.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblselectedfeeder.Location = new System.Drawing.Point(18, 62);
            this.lblselectedfeeder.Name = "lblselectedfeeder";
            this.lblselectedfeeder.Size = new System.Drawing.Size(132, 16);
            this.lblselectedfeeder.TabIndex = 14;
            this.lblselectedfeeder.Text = "Selected Feeder :";
            // 
            // lblTotalfeeder
            // 
            this.lblTotalfeeder.AutoSize = true;
            this.lblTotalfeeder.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTotalfeeder.Location = new System.Drawing.Point(18, 33);
            this.lblTotalfeeder.Name = "lblTotalfeeder";
            this.lblTotalfeeder.Size = new System.Drawing.Size(106, 16);
            this.lblTotalfeeder.TabIndex = 13;
            this.lblTotalfeeder.Text = "Total Feeder :";
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 48.5F));
            this.tableLayoutPanel1.Controls.Add(this.groupBox3, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.groupBox4, 0, 0);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(324, 153);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 153F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(382, 309);
            this.tableLayoutPanel1.TabIndex = 23;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.lblStatus2);
            this.groupBox3.Controls.Add(this.labFeederID2);
            this.groupBox3.Controls.Add(this.labFeederName2);
            this.groupBox3.Controls.Add(this.labSubstation2);
            this.groupBox3.Controls.Add(this.labdivision2);
            this.groupBox3.Controls.Add(this.label11);
            this.groupBox3.Location = new System.Drawing.Point(3, 159);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(368, 145);
            this.groupBox3.TabIndex = 11;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Running Feeder 2";
            // 
            // lblStatus2
            // 
            this.lblStatus2.AutoSize = true;
            this.lblStatus2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblStatus2.Location = new System.Drawing.Point(13, 26);
            this.lblStatus2.Name = "lblStatus2";
            this.lblStatus2.Size = new System.Drawing.Size(58, 13);
            this.lblStatus2.TabIndex = 19;
            this.lblStatus2.Text = "Status2 :";
            // 
            // labFeederID2
            // 
            this.labFeederID2.AutoSize = true;
            this.labFeederID2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labFeederID2.Location = new System.Drawing.Point(13, 126);
            this.labFeederID2.Name = "labFeederID2";
            this.labFeederID2.Size = new System.Drawing.Size(66, 13);
            this.labFeederID2.TabIndex = 13;
            this.labFeederID2.Text = "FeederID2";
            // 
            // labFeederName2
            // 
            this.labFeederName2.AutoSize = true;
            this.labFeederName2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labFeederName2.Location = new System.Drawing.Point(13, 101);
            this.labFeederName2.Name = "labFeederName2";
            this.labFeederName2.Size = new System.Drawing.Size(85, 13);
            this.labFeederName2.TabIndex = 12;
            this.labFeederName2.Text = "FeederName2";
            // 
            // labSubstation2
            // 
            this.labSubstation2.AutoSize = true;
            this.labSubstation2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labSubstation2.Location = new System.Drawing.Point(13, 73);
            this.labSubstation2.Name = "labSubstation2";
            this.labSubstation2.Size = new System.Drawing.Size(78, 13);
            this.labSubstation2.TabIndex = 11;
            this.labSubstation2.Text = "Substation 2";
            // 
            // labdivision2
            // 
            this.labdivision2.AutoSize = true;
            this.labdivision2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labdivision2.Location = new System.Drawing.Point(13, 48);
            this.labdivision2.Name = "labdivision2";
            this.labdivision2.Size = new System.Drawing.Size(59, 13);
            this.labdivision2.TabIndex = 10;
            this.labdivision2.Text = "Division2";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(13, 34);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(0, 13);
            this.label11.TabIndex = 9;
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.labFeederID1);
            this.groupBox4.Controls.Add(this.lblStatus1);
            this.groupBox4.Controls.Add(this.labFeederName1);
            this.groupBox4.Controls.Add(this.labSubstation1);
            this.groupBox4.Controls.Add(this.labdivision1);
            this.groupBox4.Controls.Add(this.label7);
            this.groupBox4.Location = new System.Drawing.Point(3, 3);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(368, 150);
            this.groupBox4.TabIndex = 25;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Running Feeder 1";
            // 
            // labFeederID1
            // 
            this.labFeederID1.AutoSize = true;
            this.labFeederID1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labFeederID1.Location = new System.Drawing.Point(13, 127);
            this.labFeederID1.Name = "labFeederID1";
            this.labFeederID1.Size = new System.Drawing.Size(66, 13);
            this.labFeederID1.TabIndex = 13;
            this.labFeederID1.Text = "FeederID1";
            // 
            // lblStatus1
            // 
            this.lblStatus1.AutoSize = true;
            this.lblStatus1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblStatus1.Location = new System.Drawing.Point(13, 34);
            this.lblStatus1.Name = "lblStatus1";
            this.lblStatus1.Size = new System.Drawing.Size(58, 13);
            this.lblStatus1.TabIndex = 18;
            this.lblStatus1.Text = "Status1 :";
            // 
            // labFeederName1
            // 
            this.labFeederName1.AutoSize = true;
            this.labFeederName1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labFeederName1.Location = new System.Drawing.Point(11, 103);
            this.labFeederName1.Name = "labFeederName1";
            this.labFeederName1.Size = new System.Drawing.Size(85, 13);
            this.labFeederName1.TabIndex = 12;
            this.labFeederName1.Text = "FeederName1";
            // 
            // labSubstation1
            // 
            this.labSubstation1.AutoSize = true;
            this.labSubstation1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labSubstation1.Location = new System.Drawing.Point(13, 82);
            this.labSubstation1.Name = "labSubstation1";
            this.labSubstation1.Size = new System.Drawing.Size(78, 13);
            this.labSubstation1.TabIndex = 11;
            this.labSubstation1.Text = "Substation 1";
            // 
            // labdivision1
            // 
            this.labdivision1.AutoSize = true;
            this.labdivision1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labdivision1.Location = new System.Drawing.Point(13, 57);
            this.labdivision1.Name = "labdivision1";
            this.labdivision1.Size = new System.Drawing.Size(59, 13);
            this.labdivision1.TabIndex = 10;
            this.labdivision1.Text = "Division1";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(13, 34);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(0, 13);
            this.label7.TabIndex = 9;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.labFeederID3);
            this.groupBox2.Controls.Add(this.lblStatus3);
            this.groupBox2.Controls.Add(this.labFeederName3);
            this.groupBox2.Controls.Add(this.labSubstation3);
            this.groupBox2.Controls.Add(this.labdivision3);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Location = new System.Drawing.Point(5, 243);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(316, 153);
            this.groupBox2.TabIndex = 10;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Running Feeder 3";
            // 
            // labFeederID3
            // 
            this.labFeederID3.AutoSize = true;
            this.labFeederID3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labFeederID3.Location = new System.Drawing.Point(13, 127);
            this.labFeederID3.Name = "labFeederID3";
            this.labFeederID3.Size = new System.Drawing.Size(66, 13);
            this.labFeederID3.TabIndex = 13;
            this.labFeederID3.Text = "FeederID3";
            // 
            // lblStatus3
            // 
            this.lblStatus3.AutoSize = true;
            this.lblStatus3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblStatus3.Location = new System.Drawing.Point(13, 34);
            this.lblStatus3.Name = "lblStatus3";
            this.lblStatus3.Size = new System.Drawing.Size(58, 13);
            this.lblStatus3.TabIndex = 18;
            this.lblStatus3.Text = "Status3 :";
            // 
            // labFeederName3
            // 
            this.labFeederName3.AutoSize = true;
            this.labFeederName3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labFeederName3.Location = new System.Drawing.Point(11, 103);
            this.labFeederName3.Name = "labFeederName3";
            this.labFeederName3.Size = new System.Drawing.Size(85, 13);
            this.labFeederName3.TabIndex = 12;
            this.labFeederName3.Text = "FeederName3";
            // 
            // labSubstation3
            // 
            this.labSubstation3.AutoSize = true;
            this.labSubstation3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labSubstation3.Location = new System.Drawing.Point(13, 82);
            this.labSubstation3.Name = "labSubstation3";
            this.labSubstation3.Size = new System.Drawing.Size(78, 13);
            this.labSubstation3.TabIndex = 11;
            this.labSubstation3.Text = "Substation 3";
            // 
            // labdivision3
            // 
            this.labdivision3.AutoSize = true;
            this.labdivision3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labdivision3.Location = new System.Drawing.Point(13, 57);
            this.labdivision3.Name = "labdivision3";
            this.labdivision3.Size = new System.Drawing.Size(59, 13);
            this.labdivision3.TabIndex = 10;
            this.labdivision3.Text = "Division3";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(13, 34);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(0, 13);
            this.label4.TabIndex = 9;
            // 
            // lblPersentageStatus
            // 
            this.lblPersentageStatus.AutoSize = true;
            this.lblPersentageStatus.BackColor = System.Drawing.Color.Transparent;
            this.lblPersentageStatus.Font = new System.Drawing.Font("MS Reference Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPersentageStatus.Location = new System.Drawing.Point(636, 471);
            this.lblPersentageStatus.Name = "lblPersentageStatus";
            this.lblPersentageStatus.Size = new System.Drawing.Size(71, 16);
            this.lblPersentageStatus.TabIndex = 27;
            this.lblPersentageStatus.Text = "Status :";
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(109, 468);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(526, 21);
            this.progressBar1.TabIndex = 26;
            // 
            // backgroundWorker1
            // 
            this.backgroundWorker1.WorkerReportsProgress = true;
            this.backgroundWorker1.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorker1_DoWork);
            this.backgroundWorker1.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.backgroundWorker1_ProgressChanged);
            this.backgroundWorker1.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.backgroundWorker1_RunWorkerCompleted);
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // pictureBox2
            // 
            this.pictureBox2.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox2.Image")));
            this.pictureBox2.Location = new System.Drawing.Point(19, 404);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(82, 80);
            this.pictureBox2.TabIndex = 29;
            this.pictureBox2.TabStop = false;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(23, 16);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(200, 69);
            this.pictureBox1.TabIndex = 28;
            this.pictureBox1.TabStop = false;
            // 
            // pictureBox3
            // 
            this.pictureBox3.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox3.Image")));
            this.pictureBox3.Location = new System.Drawing.Point(262, 4);
            this.pictureBox3.Name = "pictureBox3";
            this.pictureBox3.Size = new System.Drawing.Size(442, 144);
            this.pictureBox3.TabIndex = 35;
            this.pictureBox3.TabStop = false;
            // 
            // TechgrationProcessForThree
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.WhiteSmoke;
            this.ClientSize = new System.Drawing.Size(710, 493);
            this.Controls.Add(this.pictureBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.lblPersentageStatus);
            this.Controls.Add(this.progressBar1);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Controls.Add(this.pictureBox2);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximumSize = new System.Drawing.Size(726, 532);
            this.MinimumSize = new System.Drawing.Size(726, 532);
            this.Name = "TechgrationProcessForThree";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "TechgrationProcess";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.TechgrationProcessForThree_FormClosing);
            this.Load += new System.EventHandler(this.TechgrationProcessForThree_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label lblTime;
        private System.Windows.Forms.Label lblcompletefeeder;
        private System.Windows.Forms.Label lblselectedfeeder;
        private System.Windows.Forms.Label lblTotalfeeder;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Label lblStatus2;
        private System.Windows.Forms.Label labFeederID2;
        private System.Windows.Forms.Label labFeederName2;
        private System.Windows.Forms.Label labSubstation2;
        private System.Windows.Forms.Label labdivision2;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label labFeederID3;
        private System.Windows.Forms.Label lblStatus3;
        private System.Windows.Forms.Label labFeederName3;
        private System.Windows.Forms.Label labSubstation3;
        private System.Windows.Forms.Label labdivision3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Label labFeederID1;
        private System.Windows.Forms.Label lblStatus1;
        private System.Windows.Forms.Label labFeederName1;
        private System.Windows.Forms.Label labSubstation1;
        private System.Windows.Forms.Label labdivision1;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label lblPersentageStatus;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.PictureBox pictureBox3;
    }
}