
namespace Ripperoni
{
    partial class Record
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.RecordLayout = new System.Windows.Forms.TableLayoutPanel();
            this.Download = new System.Windows.Forms.Label();
            this.Length = new System.Windows.Forms.Label();
            this.Author = new System.Windows.Forms.Label();
            this.Thumbnail = new System.Windows.Forms.PictureBox();
            this.Title = new System.Windows.Forms.Label();
            this.Progress = new System.Windows.Forms.ProgressBar();
            this.RecordLayout.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Thumbnail)).BeginInit();
            this.SuspendLayout();
            // 
            // RecordLayout
            // 
            this.RecordLayout.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(20)))), ((int)(((byte)(20)))));
            this.RecordLayout.ColumnCount = 4;
            this.RecordLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.RecordLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.RecordLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 15F));
            this.RecordLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 35F));
            this.RecordLayout.Controls.Add(this.Download, 3, 1);
            this.RecordLayout.Controls.Add(this.Length, 2, 1);
            this.RecordLayout.Controls.Add(this.Author, 1, 1);
            this.RecordLayout.Controls.Add(this.Thumbnail, 0, 0);
            this.RecordLayout.Controls.Add(this.Title, 1, 0);
            this.RecordLayout.Controls.Add(this.Progress, 0, 2);
            this.RecordLayout.Dock = System.Windows.Forms.DockStyle.Fill;
            this.RecordLayout.Location = new System.Drawing.Point(0, 0);
            this.RecordLayout.Margin = new System.Windows.Forms.Padding(0);
            this.RecordLayout.Name = "RecordLayout";
            this.RecordLayout.RowCount = 3;
            this.RecordLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 45F));
            this.RecordLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 45F));
            this.RecordLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.RecordLayout.Size = new System.Drawing.Size(400, 60);
            this.RecordLayout.TabIndex = 0;
            // 
            // Download
            // 
            this.Download.AutoSize = true;
            this.Download.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Download.Font = new System.Drawing.Font("Segoe UI", 8.25F);
            this.Download.ForeColor = System.Drawing.SystemColors.Control;
            this.Download.Location = new System.Drawing.Point(263, 27);
            this.Download.Name = "Download";
            this.Download.Size = new System.Drawing.Size(134, 27);
            this.Download.TabIndex = 4;
            this.Download.Text = "Retrieving...";
            this.Download.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // Length
            // 
            this.Length.AutoSize = true;
            this.Length.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Length.Font = new System.Drawing.Font("Segoe UI", 8.25F);
            this.Length.ForeColor = System.Drawing.SystemColors.Control;
            this.Length.Location = new System.Drawing.Point(203, 27);
            this.Length.Name = "Length";
            this.Length.Size = new System.Drawing.Size(54, 27);
            this.Length.TabIndex = 3;
            this.Length.Text = "--:--:--";
            this.Length.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // Author
            // 
            this.Author.AutoSize = true;
            this.Author.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Author.Font = new System.Drawing.Font("Segoe UI", 8.25F);
            this.Author.ForeColor = System.Drawing.SystemColors.Control;
            this.Author.Location = new System.Drawing.Point(103, 27);
            this.Author.Name = "Author";
            this.Author.Size = new System.Drawing.Size(94, 27);
            this.Author.TabIndex = 2;
            this.Author.Text = "Retrieving...";
            this.Author.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // Thumbnail
            // 
            this.Thumbnail.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Thumbnail.Image = global::Ripperoni.Properties.Resources.Pepperoni;
            this.Thumbnail.Location = new System.Drawing.Point(12, 4);
            this.Thumbnail.Margin = new System.Windows.Forms.Padding(12, 4, 12, 4);
            this.Thumbnail.Name = "Thumbnail";
            this.RecordLayout.SetRowSpan(this.Thumbnail, 2);
            this.Thumbnail.Size = new System.Drawing.Size(76, 46);
            this.Thumbnail.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.Thumbnail.TabIndex = 0;
            this.Thumbnail.TabStop = false;
            // 
            // Title
            // 
            this.Title.AutoSize = true;
            this.RecordLayout.SetColumnSpan(this.Title, 3);
            this.Title.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Title.Font = new System.Drawing.Font("Segoe UI", 8.25F);
            this.Title.ForeColor = System.Drawing.SystemColors.Control;
            this.Title.Location = new System.Drawing.Point(103, 0);
            this.Title.Name = "Title";
            this.Title.Size = new System.Drawing.Size(294, 27);
            this.Title.TabIndex = 1;
            this.Title.Text = "Retrieving...";
            this.Title.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // Progress
            // 
            this.RecordLayout.SetColumnSpan(this.Progress, 4);
            this.Progress.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Progress.Location = new System.Drawing.Point(0, 54);
            this.Progress.Margin = new System.Windows.Forms.Padding(0, 0, 0, 4);
            this.Progress.Name = "Progress";
            this.Progress.Size = new System.Drawing.Size(400, 2);
            this.Progress.Style = System.Windows.Forms.ProgressBarStyle.Marquee;
            this.Progress.TabIndex = 5;
            // 
            // Record
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(20)))), ((int)(((byte)(20)))));
            this.Controls.Add(this.RecordLayout);
            this.DoubleBuffered = true;
            this.Margin = new System.Windows.Forms.Padding(0, 5, 0, 5);
            this.Name = "Record";
            this.Size = new System.Drawing.Size(400, 60);
            this.RecordLayout.ResumeLayout(false);
            this.RecordLayout.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Thumbnail)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel RecordLayout;
        private System.Windows.Forms.Label Download;
        private System.Windows.Forms.Label Length;
        private System.Windows.Forms.Label Author;
        private System.Windows.Forms.PictureBox Thumbnail;
        private System.Windows.Forms.Label Title;
        private System.Windows.Forms.ProgressBar Progress;
    }
}
