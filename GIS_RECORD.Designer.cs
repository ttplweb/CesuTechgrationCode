
namespace TechGration
{
    partial class GIS_RECORD
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(GIS_RECORD));
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.label4 = new System.Windows.Forms.Label();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.chk = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.combgislist = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.BtnGISTestconnSql = new System.Windows.Forms.Button();
            this.txt_GISschemaName = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.txtGisPassword = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.txtGisUserid = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.txtGisDatabasename = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txtGisservername = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.BtnSave = new System.Windows.Forms.Button();
            this.BtnQuit = new System.Windows.Forms.Button();
            this.btnsavetable = new System.Windows.Forms.Button();
            this.groupBox5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.groupBox4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.label4);
            this.groupBox5.Controls.Add(this.dataGridView1);
            this.groupBox5.Controls.Add(this.combgislist);
            this.groupBox5.Controls.Add(this.label1);
            this.groupBox5.Controls.Add(this.groupBox4);
            this.groupBox5.Location = new System.Drawing.Point(12, 12);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(469, 480);
            this.groupBox5.TabIndex = 2;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "Source GIS Database";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(380, 353);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(35, 13);
            this.label4.TabIndex = 6;
            this.label4.Text = "label4";
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.chk});
            this.dataGridView1.Location = new System.Drawing.Point(19, 304);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(346, 149);
            this.dataGridView1.TabIndex = 5;
            // 
            // chk
            // 
            this.chk.HeaderText = "CheckBox";
            this.chk.Name = "chk";
            // 
            // combgislist
            // 
            this.combgislist.FormattingEnabled = true;
            this.combgislist.Location = new System.Drawing.Point(177, 261);
            this.combgislist.Name = "combgislist";
            this.combgislist.Size = new System.Drawing.Size(188, 21);
            this.combgislist.TabIndex = 2;
            this.combgislist.SelectedIndexChanged += new System.EventHandler(this.combgislist_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(16, 261);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(86, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "GIS_Table_List :";
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.BtnGISTestconnSql);
            this.groupBox4.Controls.Add(this.txt_GISschemaName);
            this.groupBox4.Controls.Add(this.label9);
            this.groupBox4.Controls.Add(this.txtGisPassword);
            this.groupBox4.Controls.Add(this.label8);
            this.groupBox4.Controls.Add(this.txtGisUserid);
            this.groupBox4.Controls.Add(this.label6);
            this.groupBox4.Controls.Add(this.txtGisDatabasename);
            this.groupBox4.Controls.Add(this.label5);
            this.groupBox4.Controls.Add(this.txtGisservername);
            this.groupBox4.Controls.Add(this.label2);
            this.groupBox4.Location = new System.Drawing.Point(16, 19);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(434, 223);
            this.groupBox4.TabIndex = 0;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "MS SQI Authentication";
            // 
            // BtnGISTestconnSql
            // 
            this.BtnGISTestconnSql.Location = new System.Drawing.Point(179, 188);
            this.BtnGISTestconnSql.Name = "BtnGISTestconnSql";
            this.BtnGISTestconnSql.Size = new System.Drawing.Size(121, 29);
            this.BtnGISTestconnSql.TabIndex = 10;
            this.BtnGISTestconnSql.Text = "Test Connection";
            this.BtnGISTestconnSql.UseVisualStyleBackColor = true;
            this.BtnGISTestconnSql.Click += new System.EventHandler(this.BtnGISTestconnSql_Click);
            // 
            // txt_GISschemaName
            // 
            this.txt_GISschemaName.Location = new System.Drawing.Point(179, 150);
            this.txt_GISschemaName.Name = "txt_GISschemaName";
            this.txt_GISschemaName.Size = new System.Drawing.Size(220, 20);
            this.txt_GISschemaName.TabIndex = 9;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(21, 150);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(83, 13);
            this.label9.TabIndex = 8;
            this.label9.Text = "Schema Name :";
            // 
            // txtGisPassword
            // 
            this.txtGisPassword.Location = new System.Drawing.Point(179, 118);
            this.txtGisPassword.Name = "txtGisPassword";
            this.txtGisPassword.Size = new System.Drawing.Size(220, 20);
            this.txtGisPassword.TabIndex = 7;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(21, 121);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(59, 13);
            this.label8.TabIndex = 6;
            this.label8.Text = "Password :";
            // 
            // txtGisUserid
            // 
            this.txtGisUserid.Location = new System.Drawing.Point(179, 88);
            this.txtGisUserid.Name = "txtGisUserid";
            this.txtGisUserid.Size = new System.Drawing.Size(220, 20);
            this.txtGisUserid.TabIndex = 5;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(21, 91);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(49, 13);
            this.label6.TabIndex = 4;
            this.label6.Text = "User ID :";
            // 
            // txtGisDatabasename
            // 
            this.txtGisDatabasename.Location = new System.Drawing.Point(179, 57);
            this.txtGisDatabasename.Name = "txtGisDatabasename";
            this.txtGisDatabasename.Size = new System.Drawing.Size(220, 20);
            this.txtGisDatabasename.TabIndex = 3;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(21, 60);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(90, 13);
            this.label5.TabIndex = 2;
            this.label5.Text = "Database Name :";
            // 
            // txtGisservername
            // 
            this.txtGisservername.Location = new System.Drawing.Point(179, 23);
            this.txtGisservername.Name = "txtGisservername";
            this.txtGisservername.Size = new System.Drawing.Size(220, 20);
            this.txtGisservername.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(21, 26);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(75, 13);
            this.label2.TabIndex = 0;
            this.label2.Text = "Server Name :";
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::TechGration.Properties.Resources.tech6;
            this.pictureBox1.Location = new System.Drawing.Point(19, 498);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(112, 35);
            this.pictureBox1.TabIndex = 16;
            this.pictureBox1.TabStop = false;
            // 
            // BtnSave
            // 
            this.BtnSave.Location = new System.Drawing.Point(189, 510);
            this.BtnSave.Name = "BtnSave";
            this.BtnSave.Size = new System.Drawing.Size(64, 23);
            this.BtnSave.TabIndex = 15;
            this.BtnSave.Text = "Save";
            this.BtnSave.UseVisualStyleBackColor = true;
            this.BtnSave.Click += new System.EventHandler(this.BtnSave_Click);
            // 
            // BtnQuit
            // 
            this.BtnQuit.Location = new System.Drawing.Point(358, 509);
            this.BtnQuit.Name = "BtnQuit";
            this.BtnQuit.Size = new System.Drawing.Size(69, 23);
            this.BtnQuit.TabIndex = 14;
            this.BtnQuit.Text = "Quit";
            this.BtnQuit.UseVisualStyleBackColor = true;
            this.BtnQuit.Click += new System.EventHandler(this.BtnQuit_Click);
            // 
            // btnsavetable
            // 
            this.btnsavetable.Location = new System.Drawing.Point(269, 509);
            this.btnsavetable.Name = "btnsavetable";
            this.btnsavetable.Size = new System.Drawing.Size(75, 23);
            this.btnsavetable.TabIndex = 17;
            this.btnsavetable.Text = "Save Table";
            this.btnsavetable.UseVisualStyleBackColor = true;
            this.btnsavetable.Click += new System.EventHandler(this.btnsavetable_Click);
            // 
            // GIS_RECORD
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(493, 559);
            this.Controls.Add(this.btnsavetable);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.BtnSave);
            this.Controls.Add(this.BtnQuit);
            this.Controls.Add(this.groupBox5);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "GIS_RECORD";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "GIS_RECORD";
            this.Load += new System.EventHandler(this.GIS_RECORD_Load);
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Button BtnGISTestconnSql;
        private System.Windows.Forms.TextBox txt_GISschemaName;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox txtGisPassword;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox txtGisUserid;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtGisDatabasename;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtGisservername;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button BtnSave;
        private System.Windows.Forms.Button BtnQuit;
        private System.Windows.Forms.ComboBox combgislist;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button btnsavetable;
        private System.Windows.Forms.DataGridViewCheckBoxColumn chk;
    }
}