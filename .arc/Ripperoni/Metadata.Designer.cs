
namespace Ripperoni
{
    partial class Metadata
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Metadata));
            this.MetadataLayout = new System.Windows.Forms.TableLayoutPanel();
            this.HandleLayout = new System.Windows.Forms.TableLayoutPanel();
            this.Title = new System.Windows.Forms.Label();
            this.Exit = new System.Windows.Forms.Button();
            this.InfoLayout = new System.Windows.Forms.TableLayoutPanel();
            this.DataLayout = new System.Windows.Forms.TableLayoutPanel();
            this.VideoUploaderLabel = new System.Windows.Forms.Label();
            this.VideoViewsLabel = new System.Windows.Forms.Label();
            this.VideoDateLabel = new System.Windows.Forms.Label();
            this.VideoLengthLabel = new System.Windows.Forms.Label();
            this.VideoUploader = new System.Windows.Forms.TextBox();
            this.VideoViews = new System.Windows.Forms.TextBox();
            this.VideoLength = new System.Windows.Forms.TextBox();
            this.VideoDate = new System.Windows.Forms.TextBox();
            this.VideoTitleLabel = new System.Windows.Forms.Label();
            this.VideoDescLabel = new System.Windows.Forms.Label();
            this.VideoTitle = new System.Windows.Forms.TextBox();
            this.VideoDesc = new System.Windows.Forms.TextBox();
            this.MetadataLayout.SuspendLayout();
            this.HandleLayout.SuspendLayout();
            this.InfoLayout.SuspendLayout();
            this.DataLayout.SuspendLayout();
            this.SuspendLayout();
            // 
            // MetadataLayout
            // 
            this.MetadataLayout.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(27)))), ((int)(((byte)(27)))), ((int)(((byte)(27)))));
            this.MetadataLayout.ColumnCount = 1;
            this.MetadataLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.MetadataLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.MetadataLayout.Controls.Add(this.HandleLayout, 0, 0);
            this.MetadataLayout.Controls.Add(this.InfoLayout, 0, 1);
            this.MetadataLayout.Dock = System.Windows.Forms.DockStyle.Fill;
            this.MetadataLayout.Location = new System.Drawing.Point(0, 0);
            this.MetadataLayout.Margin = new System.Windows.Forms.Padding(0);
            this.MetadataLayout.Name = "MetadataLayout";
            this.MetadataLayout.RowCount = 2;
            this.MetadataLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.MetadataLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 175F));
            this.MetadataLayout.Size = new System.Drawing.Size(300, 450);
            this.MetadataLayout.TabIndex = 0;
            // 
            // HandleLayout
            // 
            this.HandleLayout.BackColor = System.Drawing.Color.Black;
            this.HandleLayout.ColumnCount = 2;
            this.HandleLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 260F));
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
            this.HandleLayout.Size = new System.Drawing.Size(300, 25);
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
            this.Title.Size = new System.Drawing.Size(254, 25);
            this.Title.TabIndex = 0;
            this.Title.Text = "Metadata";
            this.Title.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.Title.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Title_MouseDown);
            this.Title.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Title_MouseMove);
            // 
            // Exit
            // 
            this.Exit.FlatAppearance.BorderSize = 0;
            this.Exit.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(150)))), ((int)(((byte)(22)))), ((int)(((byte)(37)))));
            this.Exit.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(13)))), ((int)(((byte)(42)))));
            this.Exit.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Exit.Font = new System.Drawing.Font("Segoe MDL2 Assets", 8.25F);
            this.Exit.ForeColor = System.Drawing.SystemColors.Control;
            this.Exit.Location = new System.Drawing.Point(260, 0);
            this.Exit.Margin = new System.Windows.Forms.Padding(0);
            this.Exit.Name = "Exit";
            this.Exit.Size = new System.Drawing.Size(40, 23);
            this.Exit.TabIndex = 1;
            this.Exit.Text = "";
            this.Exit.UseVisualStyleBackColor = true;
            this.Exit.Click += new System.EventHandler(this.Exit_Click);
            // 
            // InfoLayout
            // 
            this.InfoLayout.ColumnCount = 1;
            this.InfoLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.InfoLayout.Controls.Add(this.DataLayout, 0, 4);
            this.InfoLayout.Controls.Add(this.VideoTitleLabel, 0, 0);
            this.InfoLayout.Controls.Add(this.VideoDescLabel, 0, 2);
            this.InfoLayout.Controls.Add(this.VideoTitle, 0, 1);
            this.InfoLayout.Controls.Add(this.VideoDesc, 0, 3);
            this.InfoLayout.Dock = System.Windows.Forms.DockStyle.Fill;
            this.InfoLayout.Location = new System.Drawing.Point(3, 28);
            this.InfoLayout.Name = "InfoLayout";
            this.InfoLayout.RowCount = 5;
            this.InfoLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.InfoLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.InfoLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.InfoLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 235F));
            this.InfoLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 110F));
            this.InfoLayout.Size = new System.Drawing.Size(294, 419);
            this.InfoLayout.TabIndex = 1;
            // 
            // DataLayout
            // 
            this.DataLayout.ColumnCount = 2;
            this.DataLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.DataLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.DataLayout.Controls.Add(this.VideoUploaderLabel, 0, 0);
            this.DataLayout.Controls.Add(this.VideoViewsLabel, 1, 0);
            this.DataLayout.Controls.Add(this.VideoDateLabel, 1, 2);
            this.DataLayout.Controls.Add(this.VideoLengthLabel, 0, 2);
            this.DataLayout.Controls.Add(this.VideoUploader, 0, 1);
            this.DataLayout.Controls.Add(this.VideoViews, 1, 1);
            this.DataLayout.Controls.Add(this.VideoLength, 0, 3);
            this.DataLayout.Controls.Add(this.VideoDate, 1, 3);
            this.DataLayout.Dock = System.Windows.Forms.DockStyle.Fill;
            this.DataLayout.Location = new System.Drawing.Point(0, 315);
            this.DataLayout.Margin = new System.Windows.Forms.Padding(0);
            this.DataLayout.Name = "DataLayout";
            this.DataLayout.RowCount = 4;
            this.DataLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.DataLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35F));
            this.DataLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.DataLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35F));
            this.DataLayout.Size = new System.Drawing.Size(294, 110);
            this.DataLayout.TabIndex = 4;
            // 
            // VideoUploaderLabel
            // 
            this.VideoUploaderLabel.AutoSize = true;
            this.VideoUploaderLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.VideoUploaderLabel.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.VideoUploaderLabel.ForeColor = System.Drawing.SystemColors.Control;
            this.VideoUploaderLabel.Location = new System.Drawing.Point(3, 0);
            this.VideoUploaderLabel.Name = "VideoUploaderLabel";
            this.VideoUploaderLabel.Size = new System.Drawing.Size(141, 20);
            this.VideoUploaderLabel.TabIndex = 7;
            this.VideoUploaderLabel.Text = "Author:";
            // 
            // VideoViewsLabel
            // 
            this.VideoViewsLabel.AutoSize = true;
            this.VideoViewsLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.VideoViewsLabel.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.VideoViewsLabel.ForeColor = System.Drawing.SystemColors.Control;
            this.VideoViewsLabel.Location = new System.Drawing.Point(150, 0);
            this.VideoViewsLabel.Name = "VideoViewsLabel";
            this.VideoViewsLabel.Size = new System.Drawing.Size(141, 20);
            this.VideoViewsLabel.TabIndex = 8;
            this.VideoViewsLabel.Text = "Views:";
            // 
            // VideoDateLabel
            // 
            this.VideoDateLabel.AutoSize = true;
            this.VideoDateLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.VideoDateLabel.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.VideoDateLabel.ForeColor = System.Drawing.SystemColors.Control;
            this.VideoDateLabel.Location = new System.Drawing.Point(150, 55);
            this.VideoDateLabel.Name = "VideoDateLabel";
            this.VideoDateLabel.Size = new System.Drawing.Size(141, 20);
            this.VideoDateLabel.TabIndex = 10;
            this.VideoDateLabel.Text = "Date:";
            // 
            // VideoLengthLabel
            // 
            this.VideoLengthLabel.AutoSize = true;
            this.VideoLengthLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.VideoLengthLabel.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.VideoLengthLabel.ForeColor = System.Drawing.SystemColors.Control;
            this.VideoLengthLabel.Location = new System.Drawing.Point(3, 55);
            this.VideoLengthLabel.Name = "VideoLengthLabel";
            this.VideoLengthLabel.Size = new System.Drawing.Size(141, 20);
            this.VideoLengthLabel.TabIndex = 9;
            this.VideoLengthLabel.Text = "Length:";
            // 
            // VideoUploader
            // 
            this.VideoUploader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(10)))), ((int)(((byte)(10)))), ((int)(((byte)(10)))));
            this.VideoUploader.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.VideoUploader.Dock = System.Windows.Forms.DockStyle.Fill;
            this.VideoUploader.Font = new System.Drawing.Font("Segoe UI", 9.25F);
            this.VideoUploader.ForeColor = System.Drawing.SystemColors.Control;
            this.VideoUploader.Location = new System.Drawing.Point(3, 23);
            this.VideoUploader.Name = "VideoUploader";
            this.VideoUploader.ReadOnly = true;
            this.VideoUploader.Size = new System.Drawing.Size(141, 24);
            this.VideoUploader.TabIndex = 11;
            this.VideoUploader.Text = "Retrieving...";
            // 
            // VideoViews
            // 
            this.VideoViews.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(10)))), ((int)(((byte)(10)))), ((int)(((byte)(10)))));
            this.VideoViews.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.VideoViews.Dock = System.Windows.Forms.DockStyle.Fill;
            this.VideoViews.Font = new System.Drawing.Font("Segoe UI", 9.25F);
            this.VideoViews.ForeColor = System.Drawing.SystemColors.Control;
            this.VideoViews.Location = new System.Drawing.Point(150, 23);
            this.VideoViews.Name = "VideoViews";
            this.VideoViews.ReadOnly = true;
            this.VideoViews.Size = new System.Drawing.Size(141, 24);
            this.VideoViews.TabIndex = 12;
            this.VideoViews.Text = "Retrieving...";
            // 
            // VideoLength
            // 
            this.VideoLength.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(10)))), ((int)(((byte)(10)))), ((int)(((byte)(10)))));
            this.VideoLength.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.VideoLength.Dock = System.Windows.Forms.DockStyle.Fill;
            this.VideoLength.Font = new System.Drawing.Font("Segoe UI", 9.25F);
            this.VideoLength.ForeColor = System.Drawing.SystemColors.Control;
            this.VideoLength.Location = new System.Drawing.Point(3, 78);
            this.VideoLength.Name = "VideoLength";
            this.VideoLength.ReadOnly = true;
            this.VideoLength.Size = new System.Drawing.Size(141, 24);
            this.VideoLength.TabIndex = 13;
            this.VideoLength.Text = "Retrieving...";
            // 
            // VideoDate
            // 
            this.VideoDate.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(10)))), ((int)(((byte)(10)))), ((int)(((byte)(10)))));
            this.VideoDate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.VideoDate.Dock = System.Windows.Forms.DockStyle.Fill;
            this.VideoDate.Font = new System.Drawing.Font("Segoe UI", 9.25F);
            this.VideoDate.ForeColor = System.Drawing.SystemColors.Control;
            this.VideoDate.Location = new System.Drawing.Point(150, 78);
            this.VideoDate.Name = "VideoDate";
            this.VideoDate.ReadOnly = true;
            this.VideoDate.Size = new System.Drawing.Size(141, 24);
            this.VideoDate.TabIndex = 14;
            this.VideoDate.Text = "Retrieving...";
            // 
            // VideoTitleLabel
            // 
            this.VideoTitleLabel.AutoSize = true;
            this.VideoTitleLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.VideoTitleLabel.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.VideoTitleLabel.ForeColor = System.Drawing.SystemColors.Control;
            this.VideoTitleLabel.Location = new System.Drawing.Point(3, 0);
            this.VideoTitleLabel.Name = "VideoTitleLabel";
            this.VideoTitleLabel.Size = new System.Drawing.Size(288, 20);
            this.VideoTitleLabel.TabIndex = 0;
            this.VideoTitleLabel.Text = "Title:";
            // 
            // VideoDescLabel
            // 
            this.VideoDescLabel.AutoSize = true;
            this.VideoDescLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.VideoDescLabel.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.VideoDescLabel.ForeColor = System.Drawing.SystemColors.Control;
            this.VideoDescLabel.Location = new System.Drawing.Point(3, 60);
            this.VideoDescLabel.Name = "VideoDescLabel";
            this.VideoDescLabel.Size = new System.Drawing.Size(288, 20);
            this.VideoDescLabel.TabIndex = 5;
            this.VideoDescLabel.Text = "Description:";
            // 
            // VideoTitle
            // 
            this.VideoTitle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(10)))), ((int)(((byte)(10)))), ((int)(((byte)(10)))));
            this.VideoTitle.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.VideoTitle.Dock = System.Windows.Forms.DockStyle.Fill;
            this.VideoTitle.Font = new System.Drawing.Font("Segoe UI", 9.25F);
            this.VideoTitle.ForeColor = System.Drawing.SystemColors.Control;
            this.VideoTitle.Location = new System.Drawing.Point(3, 23);
            this.VideoTitle.Multiline = true;
            this.VideoTitle.Name = "VideoTitle";
            this.VideoTitle.ReadOnly = true;
            this.VideoTitle.Size = new System.Drawing.Size(288, 34);
            this.VideoTitle.TabIndex = 6;
            this.VideoTitle.Text = "Retrieving...";
            // 
            // VideoDesc
            // 
            this.VideoDesc.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(10)))), ((int)(((byte)(10)))), ((int)(((byte)(10)))));
            this.VideoDesc.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.VideoDesc.Dock = System.Windows.Forms.DockStyle.Fill;
            this.VideoDesc.Font = new System.Drawing.Font("Segoe UI", 9.25F);
            this.VideoDesc.ForeColor = System.Drawing.SystemColors.Control;
            this.VideoDesc.Location = new System.Drawing.Point(3, 83);
            this.VideoDesc.Multiline = true;
            this.VideoDesc.Name = "VideoDesc";
            this.VideoDesc.ReadOnly = true;
            this.VideoDesc.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.VideoDesc.Size = new System.Drawing.Size(288, 229);
            this.VideoDesc.TabIndex = 7;
            this.VideoDesc.Text = "Retrieving...";
            // 
            // Metadata
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.ClientSize = new System.Drawing.Size(300, 450);
            this.Controls.Add(this.MetadataLayout);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Metadata";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Metadata";
            this.Load += new System.EventHandler(this.Metadata_Load);
            this.MetadataLayout.ResumeLayout(false);
            this.HandleLayout.ResumeLayout(false);
            this.HandleLayout.PerformLayout();
            this.InfoLayout.ResumeLayout(false);
            this.InfoLayout.PerformLayout();
            this.DataLayout.ResumeLayout(false);
            this.DataLayout.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel MetadataLayout;
        private System.Windows.Forms.TableLayoutPanel HandleLayout;
        private System.Windows.Forms.Label Title;
        private System.Windows.Forms.Button Exit;
        private System.Windows.Forms.TableLayoutPanel InfoLayout;
        private System.Windows.Forms.Label VideoTitleLabel;
        private System.Windows.Forms.Label VideoDescLabel;
        private System.Windows.Forms.TableLayoutPanel DataLayout;
        private System.Windows.Forms.Label VideoDateLabel;
        private System.Windows.Forms.Label VideoLengthLabel;
        private System.Windows.Forms.Label VideoViewsLabel;
        private System.Windows.Forms.Label VideoUploaderLabel;
        private System.Windows.Forms.TextBox VideoUploader;
        private System.Windows.Forms.TextBox VideoViews;
        private System.Windows.Forms.TextBox VideoLength;
        private System.Windows.Forms.TextBox VideoDate;
        private System.Windows.Forms.TextBox VideoTitle;
        private System.Windows.Forms.TextBox VideoDesc;
    }
}