
namespace Ripperoni
{
    partial class Processing
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
            this.ProcessingLayout = new System.Windows.Forms.TableLayoutPanel();
            this.Progress = new System.Windows.Forms.ProgressBar();
            this.Status = new System.Windows.Forms.Label();
            this.Title = new System.Windows.Forms.Label();
            this.ProcessingLayout.SuspendLayout();
            this.SuspendLayout();
            // 
            // ProcessingLayout
            // 
            this.ProcessingLayout.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(20)))), ((int)(((byte)(20)))));
            this.ProcessingLayout.ColumnCount = 2;
            this.ProcessingLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.ProcessingLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 75F));
            this.ProcessingLayout.Controls.Add(this.Progress, 0, 1);
            this.ProcessingLayout.Controls.Add(this.Status, 0, 0);
            this.ProcessingLayout.Controls.Add(this.Title, 1, 0);
            this.ProcessingLayout.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ProcessingLayout.Location = new System.Drawing.Point(0, 0);
            this.ProcessingLayout.Margin = new System.Windows.Forms.Padding(0);
            this.ProcessingLayout.Name = "ProcessingLayout";
            this.ProcessingLayout.RowCount = 2;
            this.ProcessingLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 80F));
            this.ProcessingLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.ProcessingLayout.Size = new System.Drawing.Size(400, 25);
            this.ProcessingLayout.TabIndex = 0;
            // 
            // Progress
            // 
            this.ProcessingLayout.SetColumnSpan(this.Progress, 2);
            this.Progress.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Progress.Location = new System.Drawing.Point(0, 20);
            this.Progress.Margin = new System.Windows.Forms.Padding(0, 0, 0, 3);
            this.Progress.Name = "Progress";
            this.Progress.Size = new System.Drawing.Size(400, 2);
            this.Progress.Style = System.Windows.Forms.ProgressBarStyle.Marquee;
            this.Progress.TabIndex = 0;
            // 
            // Status
            // 
            this.Status.AutoSize = true;
            this.Status.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Status.Font = new System.Drawing.Font("Segoe UI", 8.25F);
            this.Status.ForeColor = System.Drawing.SystemColors.Control;
            this.Status.Location = new System.Drawing.Point(3, 0);
            this.Status.Name = "Status";
            this.Status.Size = new System.Drawing.Size(94, 20);
            this.Status.TabIndex = 1;
            this.Status.Text = "Retrieving:";
            this.Status.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // Title
            // 
            this.Title.AutoSize = true;
            this.Title.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Title.Font = new System.Drawing.Font("Segoe UI", 8.25F);
            this.Title.ForeColor = System.Drawing.SystemColors.Control;
            this.Title.Location = new System.Drawing.Point(103, 0);
            this.Title.Name = "Title";
            this.Title.Size = new System.Drawing.Size(294, 20);
            this.Title.TabIndex = 2;
            this.Title.Text = "Retrieving...";
            this.Title.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // Processing
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.ProcessingLayout);
            this.Margin = new System.Windows.Forms.Padding(0, 5, 0, 5);
            this.Name = "Processing";
            this.Size = new System.Drawing.Size(400, 25);
            this.Load += new System.EventHandler(this.Processing_Load);
            this.ProcessingLayout.ResumeLayout(false);
            this.ProcessingLayout.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel ProcessingLayout;
        private System.Windows.Forms.ProgressBar Progress;
        private System.Windows.Forms.Label Status;
        private System.Windows.Forms.Label Title;
    }
}
