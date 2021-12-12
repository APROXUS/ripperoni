
namespace Ripperoni
{
    partial class Settings
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Settings));
            this.SettingsLayout = new System.Windows.Forms.TableLayoutPanel();
            this.HandleLayout = new System.Windows.Forms.TableLayoutPanel();
            this.Title = new System.Windows.Forms.Label();
            this.Exit = new System.Windows.Forms.Button();
            this.SettingsLayout.SuspendLayout();
            this.HandleLayout.SuspendLayout();
            this.SuspendLayout();
            // 
            // SettingsLayout
            // 
            this.SettingsLayout.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(27)))), ((int)(((byte)(27)))), ((int)(((byte)(27)))));
            this.SettingsLayout.ColumnCount = 1;
            this.SettingsLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.SettingsLayout.Controls.Add(this.HandleLayout, 0, 0);
            this.SettingsLayout.Dock = System.Windows.Forms.DockStyle.Fill;
            this.SettingsLayout.Location = new System.Drawing.Point(0, 0);
            this.SettingsLayout.Margin = new System.Windows.Forms.Padding(0);
            this.SettingsLayout.Name = "SettingsLayout";
            this.SettingsLayout.RowCount = 2;
            this.SettingsLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.SettingsLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 275F));
            this.SettingsLayout.Size = new System.Drawing.Size(250, 350);
            this.SettingsLayout.TabIndex = 0;
            // 
            // HandleLayout
            // 
            this.HandleLayout.BackColor = System.Drawing.Color.Black;
            this.HandleLayout.ColumnCount = 2;
            this.HandleLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 210F));
            this.HandleLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.HandleLayout.Controls.Add(this.Title, 0, 0);
            this.HandleLayout.Controls.Add(this.Exit, 1, 0);
            this.HandleLayout.Dock = System.Windows.Forms.DockStyle.Fill;
            this.HandleLayout.Location = new System.Drawing.Point(0, 0);
            this.HandleLayout.Margin = new System.Windows.Forms.Padding(0);
            this.HandleLayout.Name = "HandleLayout";
            this.HandleLayout.RowCount = 1;
            this.HandleLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.HandleLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.HandleLayout.Size = new System.Drawing.Size(250, 25);
            this.HandleLayout.TabIndex = 0;
            // 
            // Title
            // 
            this.Title.AutoSize = true;
            this.Title.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Title.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.Title.ForeColor = System.Drawing.SystemColors.Control;
            this.Title.Location = new System.Drawing.Point(3, 0);
            this.Title.Name = "Title";
            this.Title.Size = new System.Drawing.Size(204, 25);
            this.Title.TabIndex = 0;
            this.Title.Text = "Settings";
            this.Title.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.Title.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Title_MouseDown);
            this.Title.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Title_MouseMove);
            // 
            // Exit
            // 
            this.Exit.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Exit.FlatAppearance.BorderSize = 0;
            this.Exit.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(150)))), ((int)(((byte)(22)))), ((int)(((byte)(37)))));
            this.Exit.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(13)))), ((int)(((byte)(42)))));
            this.Exit.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Exit.Font = new System.Drawing.Font("Segoe MDL2 Assets", 8.25F);
            this.Exit.ForeColor = System.Drawing.SystemColors.Control;
            this.Exit.Location = new System.Drawing.Point(210, 0);
            this.Exit.Margin = new System.Windows.Forms.Padding(0);
            this.Exit.Name = "Exit";
            this.Exit.Size = new System.Drawing.Size(40, 25);
            this.Exit.TabIndex = 1;
            this.Exit.Text = "";
            this.Exit.UseVisualStyleBackColor = true;
            this.Exit.Click += new System.EventHandler(this.Exit_Click);
            // 
            // Settings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(29)))), ((int)(((byte)(27)))), ((int)(((byte)(27)))));
            this.ClientSize = new System.Drawing.Size(250, 350);
            this.Controls.Add(this.SettingsLayout);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Settings";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Settings";
            this.SettingsLayout.ResumeLayout(false);
            this.HandleLayout.ResumeLayout(false);
            this.HandleLayout.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel SettingsLayout;
        private System.Windows.Forms.TableLayoutPanel HandleLayout;
        private System.Windows.Forms.Label Title;
        private System.Windows.Forms.Button Exit;
    }
}