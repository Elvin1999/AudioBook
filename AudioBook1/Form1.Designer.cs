namespace AudioBook1
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
            this.listViewPdfs = new System.Windows.Forms.ListView();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // listViewPdfs
            // 
            this.listViewPdfs.AllowDrop = true;
            this.listViewPdfs.BackColor = System.Drawing.SystemColors.HotTrack;
            this.listViewPdfs.Location = new System.Drawing.Point(23, 23);
            this.listViewPdfs.Name = "listViewPdfs";
            this.listViewPdfs.Size = new System.Drawing.Size(205, 381);
            this.listViewPdfs.TabIndex = 0;
            this.listViewPdfs.UseCompatibleStateImageBehavior = false;
            this.listViewPdfs.SelectedIndexChanged += new System.EventHandler(this.listViewPdfs_SelectedIndexChanged);
            this.listViewPdfs.DragDrop += new System.Windows.Forms.DragEventHandler(this.listViewPdfs_DragDrop);
            this.listViewPdfs.DragEnter += new System.Windows.Forms.DragEventHandler(this.listViewPdfs_DragEnter);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Location = new System.Drawing.Point(276, 23);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(317, 381);
            this.pictureBox1.TabIndex = 1;
            this.pictureBox1.TabStop = false;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(669, 450);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.listViewPdfs);
            this.Name = "Form1";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListView listViewPdfs;
        private System.Windows.Forms.PictureBox pictureBox1;
    }
}

