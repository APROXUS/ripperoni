
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
            this.controlQuality = new System.Windows.Forms.Label();
            this.controlLength = new System.Windows.Forms.Label();
            this.controlAuthor = new System.Windows.Forms.Label();
            this.controlThumbnail = new System.Windows.Forms.PictureBox();
            this.controlTitle = new System.Windows.Forms.Label();
            this.RecordLayout.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.controlThumbnail)).BeginInit();
            this.SuspendLayout();
            // 
            // RecordLayout
            // 
            this.RecordLayout.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(20)))), ((int)(((byte)(20)))));
            this.RecordLayout.ColumnCount = 4;
            this.RecordLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.RecordLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.RecordLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.RecordLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.RecordLayout.Controls.Add(this.controlQuality, 3, 1);
            this.RecordLayout.Controls.Add(this.controlLength, 2, 1);
            this.RecordLayout.Controls.Add(this.controlAuthor, 1, 1);
            this.RecordLayout.Controls.Add(this.controlThumbnail, 0, 0);
            this.RecordLayout.Controls.Add(this.controlTitle, 1, 0);
            this.RecordLayout.Dock = System.Windows.Forms.DockStyle.Fill;
            this.RecordLayout.Location = new System.Drawing.Point(0, 0);
            this.RecordLayout.Name = "RecordLayout";
            this.RecordLayout.RowCount = 2;
            this.RecordLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.RecordLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.RecordLayout.Size = new System.Drawing.Size(350, 75);
            this.RecordLayout.TabIndex = 0;
            // 
            // controlQuality
            // 
            this.controlQuality.AutoSize = true;
            this.controlQuality.Dock = System.Windows.Forms.DockStyle.Fill;
            this.controlQuality.Font = new System.Drawing.Font("Segoe UI", 8.25F);
            this.controlQuality.ForeColor = System.Drawing.SystemColors.Control;
            this.controlQuality.Location = new System.Drawing.Point(264, 37);
            this.controlQuality.Name = "controlQuality";
            this.controlQuality.Size = new System.Drawing.Size(83, 38);
            this.controlQuality.TabIndex = 4;
            this.controlQuality.Text = "Retrieving...";
            this.controlQuality.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // controlLength
            // 
            this.controlLength.AutoSize = true;
            this.controlLength.Dock = System.Windows.Forms.DockStyle.Fill;
            this.controlLength.Font = new System.Drawing.Font("Segoe UI", 8.25F);
            this.controlLength.ForeColor = System.Drawing.SystemColors.Control;
            this.controlLength.Location = new System.Drawing.Point(177, 37);
            this.controlLength.Name = "controlLength";
            this.controlLength.Size = new System.Drawing.Size(81, 38);
            this.controlLength.TabIndex = 3;
            this.controlLength.Text = "Retrieving...";
            this.controlLength.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // controlAuthor
            // 
            this.controlAuthor.AutoSize = true;
            this.controlAuthor.Dock = System.Windows.Forms.DockStyle.Fill;
            this.controlAuthor.Font = new System.Drawing.Font("Segoe UI", 8.25F);
            this.controlAuthor.ForeColor = System.Drawing.SystemColors.Control;
            this.controlAuthor.Location = new System.Drawing.Point(90, 37);
            this.controlAuthor.Name = "controlAuthor";
            this.controlAuthor.Size = new System.Drawing.Size(81, 38);
            this.controlAuthor.TabIndex = 2;
            this.controlAuthor.Text = "Retrieving...";
            this.controlAuthor.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // controlThumbnail
            // 
            this.controlThumbnail.Dock = System.Windows.Forms.DockStyle.Fill;
            this.controlThumbnail.Image = global::Ripperoni.Properties.Resources.Pepperoni;
            this.controlThumbnail.Location = new System.Drawing.Point(3, 3);
            this.controlThumbnail.Name = "controlThumbnail";
            this.RecordLayout.SetRowSpan(this.controlThumbnail, 2);
            this.controlThumbnail.Size = new System.Drawing.Size(81, 69);
            this.controlThumbnail.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.controlThumbnail.TabIndex = 0;
            this.controlThumbnail.TabStop = false;
            // 
            // controlTitle
            // 
            this.controlTitle.AutoSize = true;
            this.RecordLayout.SetColumnSpan(this.controlTitle, 3);
            this.controlTitle.Dock = System.Windows.Forms.DockStyle.Fill;
            this.controlTitle.Font = new System.Drawing.Font("Segoe UI", 8.25F);
            this.controlTitle.ForeColor = System.Drawing.SystemColors.Control;
            this.controlTitle.Location = new System.Drawing.Point(90, 0);
            this.controlTitle.Name = "controlTitle";
            this.controlTitle.Size = new System.Drawing.Size(257, 37);
            this.controlTitle.TabIndex = 1;
            this.controlTitle.Text = "Retrieving...";
            this.controlTitle.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // Record
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.RecordLayout);
            this.Name = "Record";
            this.Size = new System.Drawing.Size(350, 75);
            this.RecordLayout.ResumeLayout(false);
            this.RecordLayout.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.controlThumbnail)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel RecordLayout;
        private System.Windows.Forms.Label controlQuality;
        private System.Windows.Forms.Label controlLength;
        private System.Windows.Forms.Label controlAuthor;
        private System.Windows.Forms.PictureBox controlThumbnail;
        private System.Windows.Forms.Label controlTitle;
    }
}
