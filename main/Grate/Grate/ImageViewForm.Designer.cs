namespace Grate
{
    partial class ImageViewForm
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
            this.menuStrip = new System.Windows.Forms.MenuStrip();
            this.loadImage = new System.Windows.Forms.ToolStripMenuItem();
            this.processingGroup = new System.Windows.Forms.ToolStripMenuItem();
            this.graphiteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.reactorToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.reactor2ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.reactor22ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.reactor3ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.imagePanel = new System.Windows.Forms.Panel();
            this.imageView = new Grate.ImageView();
            this.reactor4ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip.SuspendLayout();
            this.imagePanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip
            // 
            this.menuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.loadImage,
            this.processingGroup});
            this.menuStrip.Location = new System.Drawing.Point(0, 0);
            this.menuStrip.Name = "menuStrip";
            this.menuStrip.Size = new System.Drawing.Size(467, 24);
            this.menuStrip.TabIndex = 0;
            this.menuStrip.Text = "menuStrip1";
            // 
            // loadImage
            // 
            this.loadImage.Name = "loadImage";
            this.loadImage.Size = new System.Drawing.Size(81, 20);
            this.loadImage.Text = "Load image";
            this.loadImage.Click += new System.EventHandler(this.loadImage_Click);
            // 
            // processingGroup
            // 
            this.processingGroup.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.graphiteToolStripMenuItem,
            this.reactorToolStripMenuItem,
            this.reactor2ToolStripMenuItem,
            this.reactor22ToolStripMenuItem,
            this.reactor3ToolStripMenuItem,
            this.reactor4ToolStripMenuItem});
            this.processingGroup.Name = "processingGroup";
            this.processingGroup.Size = new System.Drawing.Size(76, 20);
            this.processingGroup.Text = "Processing";
            // 
            // graphiteToolStripMenuItem
            // 
            this.graphiteToolStripMenuItem.Name = "graphiteToolStripMenuItem";
            this.graphiteToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.graphiteToolStripMenuItem.Text = "Graphite";
            this.graphiteToolStripMenuItem.Click += new System.EventHandler(this.graphiteToolStripMenuItem_Click);
            // 
            // reactorToolStripMenuItem
            // 
            this.reactorToolStripMenuItem.Name = "reactorToolStripMenuItem";
            this.reactorToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.reactorToolStripMenuItem.Text = "Reactor";
            this.reactorToolStripMenuItem.Click += new System.EventHandler(this.reactorToolStripMenuItem_Click);
            // 
            // reactor2ToolStripMenuItem
            // 
            this.reactor2ToolStripMenuItem.Name = "reactor2ToolStripMenuItem";
            this.reactor2ToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.reactor2ToolStripMenuItem.Text = "Reactor2-1";
            this.reactor2ToolStripMenuItem.Click += new System.EventHandler(this.reactor2ToolStripMenuItem_Click);
            // 
            // reactor22ToolStripMenuItem
            // 
            this.reactor22ToolStripMenuItem.Name = "reactor22ToolStripMenuItem";
            this.reactor22ToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.reactor22ToolStripMenuItem.Text = "Reactor2-2";
            this.reactor22ToolStripMenuItem.Click += new System.EventHandler(this.reactor22ToolStripMenuItem_Click);
            // 
            // reactor3ToolStripMenuItem
            // 
            this.reactor3ToolStripMenuItem.Name = "reactor3ToolStripMenuItem";
            this.reactor3ToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.reactor3ToolStripMenuItem.Text = "Reactor3";
            this.reactor3ToolStripMenuItem.Click += new System.EventHandler(this.reactor3ToolStripMenuItem_Click);
            // 
            // imagePanel
            // 
            this.imagePanel.AutoScroll = true;
            this.imagePanel.Controls.Add(this.imageView);
            this.imagePanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.imagePanel.Location = new System.Drawing.Point(0, 24);
            this.imagePanel.Name = "imagePanel";
            this.imagePanel.Size = new System.Drawing.Size(467, 343);
            this.imagePanel.TabIndex = 1;
            // 
            // imageView
            // 
            this.imageView.BackColor = System.Drawing.SystemColors.Control;
            this.imageView.Image = null;
            this.imageView.Location = new System.Drawing.Point(0, 0);
            this.imageView.Name = "imageView";
            this.imageView.Size = new System.Drawing.Size(278, 198);
            this.imageView.TabIndex = 0;
            // 
            // reactor4ToolStripMenuItem
            // 
            this.reactor4ToolStripMenuItem.Name = "reactor4ToolStripMenuItem";
            this.reactor4ToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.reactor4ToolStripMenuItem.Text = "Reactor4";
            this.reactor4ToolStripMenuItem.Click += new System.EventHandler(this.reactor4ToolStripMenuItem_Click);
            // 
            // ImageViewForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(467, 367);
            this.Controls.Add(this.imagePanel);
            this.Controls.Add(this.menuStrip);
            this.MainMenuStrip = this.menuStrip;
            this.Name = "ImageViewForm";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Show;
            this.Text = "Grate 0.0.1";
            this.menuStrip.ResumeLayout(false);
            this.menuStrip.PerformLayout();
            this.imagePanel.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip;
        private System.Windows.Forms.ToolStripMenuItem loadImage;
        private System.Windows.Forms.Panel imagePanel;
        private ImageView imageView;
        private System.Windows.Forms.ToolStripMenuItem processingGroup;
        private System.Windows.Forms.ToolStripMenuItem graphiteToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem reactorToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem reactor2ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem reactor22ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem reactor3ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem reactor4ToolStripMenuItem;
    }
}

