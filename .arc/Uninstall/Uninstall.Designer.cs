
namespace Uninstall
{
    partial class Uninstall
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Uninstall));
            this.UninstallLayout = new System.Windows.Forms.TableLayoutPanel();
            this.Progress = new System.Windows.Forms.ProgressBar();
            this.Status = new System.Windows.Forms.Label();
            this.UninstallLayout.SuspendLayout();
            this.SuspendLayout();
            // 
            // UninstallLayout
            // 
            this.UninstallLayout.ColumnCount = 1;
            this.UninstallLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.UninstallLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.UninstallLayout.Controls.Add(this.Progress, 0, 1);
            this.UninstallLayout.Controls.Add(this.Status, 0, 0);
            this.UninstallLayout.Dock = System.Windows.Forms.DockStyle.Fill;
            this.UninstallLayout.Location = new System.Drawing.Point(0, 0);
            this.UninstallLayout.Name = "UninstallLayout";
            this.UninstallLayout.RowCount = 2;
            this.UninstallLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.UninstallLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.UninstallLayout.Size = new System.Drawing.Size(280, 50);
            this.UninstallLayout.TabIndex = 0;
            // 
            // Progress
            // 
            this.Progress.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Progress.ForeColor = System.Drawing.Color.Lime;
            this.Progress.Location = new System.Drawing.Point(6, 31);
            this.Progress.Margin = new System.Windows.Forms.Padding(6, 6, 6, 17);
            this.Progress.MarqueeAnimationSpeed = 10;
            this.Progress.Name = "Progress";
            this.Progress.Size = new System.Drawing.Size(268, 2);
            this.Progress.Style = System.Windows.Forms.ProgressBarStyle.Marquee;
            this.Progress.TabIndex = 0;
            // 
            // Status
            // 
            this.Status.AutoSize = true;
            this.Status.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Status.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold);
            this.Status.ForeColor = System.Drawing.SystemColors.Control;
            this.Status.Location = new System.Drawing.Point(3, 0);
            this.Status.Name = "Status";
            this.Status.Size = new System.Drawing.Size(274, 25);
            this.Status.TabIndex = 1;
            this.Status.Text = "Starting uninstall...";
            this.Status.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            // 
            // Uninstall
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Gray;
            this.ClientSize = new System.Drawing.Size(280, 50);
            this.Controls.Add(this.UninstallLayout);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Uninstall";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Ripperoni Setup";
            this.TopMost = true;
            this.TransparencyKey = System.Drawing.Color.Gray;
            this.UninstallLayout.ResumeLayout(false);
            this.UninstallLayout.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel UninstallLayout;
        private System.Windows.Forms.ProgressBar Progress;
        private System.Windows.Forms.Label Status;
    }
}

