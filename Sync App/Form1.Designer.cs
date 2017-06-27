namespace Sync_App
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.start_BTN = new System.Windows.Forms.Button();
            this.parent_textbox = new System.Windows.Forms.TextBox();
            this.final_textbox = new System.Windows.Forms.TextBox();
            this.browse_parent_btn = new System.Windows.Forms.Button();
            this.browse_final_btn = new System.Windows.Forms.Button();
            this.stop_BTN = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.label3 = new System.Windows.Forms.Label();
            this.browse_logs_btn = new System.Windows.Forms.Button();
            this.logs_textbox = new System.Windows.Forms.TextBox();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.exclude_btn = new System.Windows.Forms.Button();
            this.timer2 = new System.Windows.Forms.Timer(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // start_BTN
            // 
            this.start_BTN.Location = new System.Drawing.Point(135, 52);
            this.start_BTN.Name = "start_BTN";
            this.start_BTN.Size = new System.Drawing.Size(89, 36);
            this.start_BTN.TabIndex = 0;
            this.start_BTN.Text = "Start";
            this.start_BTN.UseVisualStyleBackColor = true;
            this.start_BTN.Click += new System.EventHandler(this.start_BTN_Click);
            // 
            // parent_textbox
            // 
            this.parent_textbox.Location = new System.Drawing.Point(102, 133);
            this.parent_textbox.Name = "parent_textbox";
            this.parent_textbox.Size = new System.Drawing.Size(237, 20);
            this.parent_textbox.TabIndex = 1;
            this.parent_textbox.Text = "C:\\FTP DATA";
            // 
            // final_textbox
            // 
            this.final_textbox.Location = new System.Drawing.Point(102, 187);
            this.final_textbox.Name = "final_textbox";
            this.final_textbox.Size = new System.Drawing.Size(237, 20);
            this.final_textbox.TabIndex = 2;
            this.final_textbox.Text = "C:\\GoodSync Data";
            // 
            // browse_parent_btn
            // 
            this.browse_parent_btn.Location = new System.Drawing.Point(12, 131);
            this.browse_parent_btn.Name = "browse_parent_btn";
            this.browse_parent_btn.Size = new System.Drawing.Size(75, 23);
            this.browse_parent_btn.TabIndex = 3;
            this.browse_parent_btn.Text = "Browse";
            this.browse_parent_btn.UseVisualStyleBackColor = true;
            this.browse_parent_btn.Click += new System.EventHandler(this.browse_parent_btn_Click);
            // 
            // browse_final_btn
            // 
            this.browse_final_btn.Location = new System.Drawing.Point(12, 185);
            this.browse_final_btn.Name = "browse_final_btn";
            this.browse_final_btn.Size = new System.Drawing.Size(75, 23);
            this.browse_final_btn.TabIndex = 4;
            this.browse_final_btn.Text = "Browse";
            this.browse_final_btn.UseVisualStyleBackColor = true;
            this.browse_final_btn.Click += new System.EventHandler(this.browse_final_btn_Click);
            // 
            // stop_BTN
            // 
            this.stop_BTN.Location = new System.Drawing.Point(250, 52);
            this.stop_BTN.Name = "stop_BTN";
            this.stop_BTN.Size = new System.Drawing.Size(89, 36);
            this.stop_BTN.TabIndex = 5;
            this.stop_BTN.Text = "Stop";
            this.stop_BTN.UseVisualStyleBackColor = true;
            this.stop_BTN.Click += new System.EventHandler(this.stop_BTN_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(99, 113);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(125, 17);
            this.label1.TabIndex = 6;
            this.label1.Text = "Temp Parent Folder";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(104, 167);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(120, 17);
            this.label2.TabIndex = 7;
            this.label2.Text = "Final Parent Folder";
            // 
            // pictureBox1
            // 
            this.pictureBox1.Location = new System.Drawing.Point(3, 8);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(160, 90);
            this.pictureBox1.TabIndex = 8;
            this.pictureBox1.TabStop = false;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(104, 222);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(78, 17);
            this.label3.TabIndex = 11;
            this.label3.Text = "Logs Folder";
            // 
            // browse_logs_btn
            // 
            this.browse_logs_btn.Location = new System.Drawing.Point(12, 240);
            this.browse_logs_btn.Name = "browse_logs_btn";
            this.browse_logs_btn.Size = new System.Drawing.Size(75, 23);
            this.browse_logs_btn.TabIndex = 10;
            this.browse_logs_btn.Text = "Browse";
            this.browse_logs_btn.UseVisualStyleBackColor = true;
            this.browse_logs_btn.Click += new System.EventHandler(this.browse_logs_btn_Click);
            // 
            // logs_textbox
            // 
            this.logs_textbox.Location = new System.Drawing.Point(102, 242);
            this.logs_textbox.Name = "logs_textbox";
            this.logs_textbox.Size = new System.Drawing.Size(237, 20);
            this.logs_textbox.TabIndex = 9;
            this.logs_textbox.Text = "C:\\Sync App Logs";
            // 
            // exclude_btn
            // 
            this.exclude_btn.Location = new System.Drawing.Point(248, 10);
            this.exclude_btn.Name = "exclude_btn";
            this.exclude_btn.Size = new System.Drawing.Size(91, 36);
            this.exclude_btn.TabIndex = 12;
            this.exclude_btn.Text = "Exclude Folders";
            this.exclude_btn.UseVisualStyleBackColor = true;
            this.exclude_btn.Click += new System.EventHandler(this.exclude_btn_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(366, 306);
            this.Controls.Add(this.exclude_btn);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.browse_logs_btn);
            this.Controls.Add(this.logs_textbox);
            this.Controls.Add(this.start_BTN);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.stop_BTN);
            this.Controls.Add(this.browse_final_btn);
            this.Controls.Add(this.browse_parent_btn);
            this.Controls.Add(this.final_textbox);
            this.Controls.Add(this.parent_textbox);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Form1";
            this.Text = "FTP Copy";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button start_BTN;
        private System.Windows.Forms.TextBox parent_textbox;
        private System.Windows.Forms.TextBox final_textbox;
        private System.Windows.Forms.Button browse_parent_btn;
        private System.Windows.Forms.Button browse_final_btn;
        private System.Windows.Forms.Button stop_BTN;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button browse_logs_btn;
        private System.Windows.Forms.TextBox logs_textbox;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Button exclude_btn;
        private System.Windows.Forms.Timer timer2;
    }
}

