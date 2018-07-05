namespace Grate
{
    partial class ImagePreview
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
            this.imageView1 = new Grate.ImageView();
            this.SuspendLayout();
            // 
            // imageView1
            // 
            this.imageView1.BackColor = System.Drawing.SystemColors.ButtonShadow;
            this.imageView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.imageView1.Image = null;
            this.imageView1.Location = new System.Drawing.Point(0, 0);
            this.imageView1.Name = "imageView1";
            this.imageView1.Size = new System.Drawing.Size(584, 385);
            this.imageView1.TabIndex = 0;
            // 
            // ImagePreview
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(584, 385);
            this.Controls.Add(this.imageView1);
            this.Name = "ImagePreview";
            this.Text = "ImagePreview";
            this.ResumeLayout(false);

        }

        #endregion

        private ImageView imageView1;
    }
}