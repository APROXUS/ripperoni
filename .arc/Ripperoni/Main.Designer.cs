namespace Ripperoni
{
    partial class Main
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Main));
            this.MainLayout = new System.Windows.Forms.TableLayoutPanel();
            this.HandleLayout = new System.Windows.Forms.TableLayoutPanel();
            this.Icon = new System.Windows.Forms.PictureBox();
            this.Title = new System.Windows.Forms.Label();
            this.Exit = new System.Windows.Forms.Button();
            this.Minimize = new System.Windows.Forms.Button();
            this.OutputLayout = new System.Windows.Forms.TableLayoutPanel();
            this.Settings = new System.Windows.Forms.Button();
            this.Metadata = new System.Windows.Forms.Button();
            this.Support = new System.Windows.Forms.Button();
            this.Input = new System.Windows.Forms.TextBox();
            this.LocationLabel = new System.Windows.Forms.Label();
            this.Convert = new System.Windows.Forms.Button();
            this.Output = new System.Windows.Forms.TextBox();
            this.OpenFolder = new System.Windows.Forms.Button();
            this.FooterLayout = new System.Windows.Forms.TableLayoutPanel();
            this.LightLine3 = new System.Windows.Forms.Panel();
            this.LightLine4 = new System.Windows.Forms.Panel();
            this.FooterIcon = new System.Windows.Forms.PictureBox();
            this.FooterItems = new System.Windows.Forms.TableLayoutPanel();
            this.Repository = new System.Windows.Forms.Label();
            this.Folder = new System.Windows.Forms.Label();
            this.Copyright = new System.Windows.Forms.Label();
            this.Website = new System.Windows.Forms.Label();
            this.LinedLayout = new System.Windows.Forms.TableLayoutPanel();
            this.LightLine = new System.Windows.Forms.Panel();
            this.LigntLine2 = new System.Windows.Forms.Panel();
            this.Records = new System.Windows.Forms.FlowLayoutPanel();
            this.SettingsLayout = new System.Windows.Forms.TableLayoutPanel();
            this.Elements = new System.Windows.Forms.ComboBox();
            this.Resolution = new System.Windows.Forms.ComboBox();
            this.Format = new System.Windows.Forms.ComboBox();
            this.FormatLabel = new System.Windows.Forms.Label();
            this.ResolutionLabel = new System.Windows.Forms.Label();
            this.ElementsLabel = new System.Windows.Forms.Label();
            this.MainLayout.SuspendLayout();
            this.HandleLayout.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Icon)).BeginInit();
            this.OutputLayout.SuspendLayout();
            this.FooterLayout.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.FooterIcon)).BeginInit();
            this.FooterItems.SuspendLayout();
            this.LinedLayout.SuspendLayout();
            this.SettingsLayout.SuspendLayout();
            this.SuspendLayout();
            // 
            // MainLayout
            // 
            this.MainLayout.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            resources.ApplyResources(this.MainLayout, "MainLayout");
            this.MainLayout.Controls.Add(this.HandleLayout, 0, 0);
            this.MainLayout.Controls.Add(this.OutputLayout, 0, 3);
            this.MainLayout.Controls.Add(this.FooterLayout, 0, 4);
            this.MainLayout.Controls.Add(this.LinedLayout, 0, 2);
            this.MainLayout.Controls.Add(this.SettingsLayout, 0, 1);
            this.MainLayout.Name = "MainLayout";
            // 
            // HandleLayout
            // 
            this.HandleLayout.BackColor = System.Drawing.Color.Black;
            resources.ApplyResources(this.HandleLayout, "HandleLayout");
            this.HandleLayout.Controls.Add(this.Icon, 0, 0);
            this.HandleLayout.Controls.Add(this.Title, 1, 0);
            this.HandleLayout.Controls.Add(this.Exit, 3, 0);
            this.HandleLayout.Controls.Add(this.Minimize, 2, 0);
            this.HandleLayout.Name = "HandleLayout";
            this.HandleLayout.MouseDown += new System.Windows.Forms.MouseEventHandler(this.HandleLayout_MouseDown);
            this.HandleLayout.MouseMove += new System.Windows.Forms.MouseEventHandler(this.HandleLayout_MouseMove);
            // 
            // Icon
            // 
            resources.ApplyResources(this.Icon, "Icon");
            this.Icon.Image = global::Ripperoni.Properties.Resources.Pepperoni;
            this.Icon.Name = "Icon";
            this.Icon.TabStop = false;
            this.Icon.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Icon_MouseDown);
            this.Icon.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Icon_MouseMove);
            // 
            // Title
            // 
            resources.ApplyResources(this.Title, "Title");
            this.Title.ForeColor = System.Drawing.SystemColors.Control;
            this.Title.Name = "Title";
            this.Title.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Title_MouseDown);
            this.Title.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Title_MouseMove);
            // 
            // Exit
            // 
            resources.ApplyResources(this.Exit, "Exit");
            this.Exit.FlatAppearance.BorderSize = 0;
            this.Exit.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(150)))), ((int)(((byte)(22)))), ((int)(((byte)(37)))));
            this.Exit.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(13)))), ((int)(((byte)(42)))));
            this.Exit.ForeColor = System.Drawing.SystemColors.Control;
            this.Exit.Name = "Exit";
            this.Exit.UseVisualStyleBackColor = true;
            this.Exit.Click += new System.EventHandler(this.Exit_Click);
            // 
            // Minimize
            // 
            resources.ApplyResources(this.Minimize, "Minimize");
            this.Minimize.FlatAppearance.BorderSize = 0;
            this.Minimize.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(93)))), ((int)(((byte)(156)))));
            this.Minimize.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(122)))), ((int)(((byte)(204)))));
            this.Minimize.ForeColor = System.Drawing.SystemColors.Control;
            this.Minimize.Name = "Minimize";
            this.Minimize.UseVisualStyleBackColor = true;
            this.Minimize.Click += new System.EventHandler(this.Minimize_Click);
            // 
            // OutputLayout
            // 
            this.OutputLayout.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            resources.ApplyResources(this.OutputLayout, "OutputLayout");
            this.OutputLayout.Controls.Add(this.Settings, 2, 2);
            this.OutputLayout.Controls.Add(this.Metadata, 3, 2);
            this.OutputLayout.Controls.Add(this.Support, 1, 2);
            this.OutputLayout.Controls.Add(this.Input, 0, 2);
            this.OutputLayout.Controls.Add(this.LocationLabel, 0, 1);
            this.OutputLayout.Controls.Add(this.Convert, 2, 3);
            this.OutputLayout.Controls.Add(this.Output, 0, 3);
            this.OutputLayout.Controls.Add(this.OpenFolder, 1, 3);
            this.OutputLayout.Name = "OutputLayout";
            // 
            // Settings
            // 
            this.Settings.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(20)))), ((int)(((byte)(20)))));
            resources.ApplyResources(this.Settings, "Settings");
            this.Settings.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(100)))), ((int)(((byte)(100)))));
            this.Settings.ForeColor = System.Drawing.SystemColors.Control;
            this.Settings.Name = "Settings";
            this.Settings.UseVisualStyleBackColor = false;
            this.Settings.Click += new System.EventHandler(this.Settings_Click);
            // 
            // Metadata
            // 
            this.Metadata.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(20)))), ((int)(((byte)(20)))));
            resources.ApplyResources(this.Metadata, "Metadata");
            this.Metadata.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(100)))), ((int)(((byte)(100)))));
            this.Metadata.ForeColor = System.Drawing.SystemColors.Control;
            this.Metadata.Name = "Metadata";
            this.Metadata.UseVisualStyleBackColor = false;
            this.Metadata.Click += new System.EventHandler(this.Metadata_Click);
            // 
            // Support
            // 
            this.Support.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(20)))), ((int)(((byte)(20)))));
            resources.ApplyResources(this.Support, "Support");
            this.Support.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(100)))), ((int)(((byte)(100)))));
            this.Support.ForeColor = System.Drawing.SystemColors.Control;
            this.Support.Name = "Support";
            this.Support.UseVisualStyleBackColor = false;
            this.Support.Click += new System.EventHandler(this.Support_Click);
            // 
            // Input
            // 
            this.Input.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(20)))), ((int)(((byte)(20)))));
            this.Input.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            resources.ApplyResources(this.Input, "Input");
            this.Input.ForeColor = System.Drawing.SystemColors.Control;
            this.Input.Name = "Input";
            // 
            // LocationLabel
            // 
            resources.ApplyResources(this.LocationLabel, "LocationLabel");
            this.LocationLabel.ForeColor = System.Drawing.SystemColors.Control;
            this.LocationLabel.Name = "LocationLabel";
            // 
            // Convert
            // 
            this.Convert.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(20)))), ((int)(((byte)(20)))));
            this.OutputLayout.SetColumnSpan(this.Convert, 2);
            resources.ApplyResources(this.Convert, "Convert");
            this.Convert.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(122)))), ((int)(((byte)(204)))));
            this.Convert.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(122)))), ((int)(((byte)(204)))));
            this.Convert.ForeColor = System.Drawing.SystemColors.Control;
            this.Convert.Name = "Convert";
            this.Convert.UseVisualStyleBackColor = false;
            this.Convert.Click += new System.EventHandler(this.Convert_Click);
            // 
            // Output
            // 
            this.Output.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(20)))), ((int)(((byte)(20)))));
            this.Output.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            resources.ApplyResources(this.Output, "Output");
            this.Output.ForeColor = System.Drawing.SystemColors.Control;
            this.Output.Name = "Output";
            // 
            // OpenFolder
            // 
            this.OpenFolder.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(20)))), ((int)(((byte)(20)))));
            resources.ApplyResources(this.OpenFolder, "OpenFolder");
            this.OpenFolder.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(100)))), ((int)(((byte)(100)))));
            this.OpenFolder.ForeColor = System.Drawing.SystemColors.Control;
            this.OpenFolder.Name = "OpenFolder";
            this.OpenFolder.UseVisualStyleBackColor = false;
            this.OpenFolder.Click += new System.EventHandler(this.OpenFolder_Click);
            // 
            // FooterLayout
            // 
            resources.ApplyResources(this.FooterLayout, "FooterLayout");
            this.FooterLayout.Controls.Add(this.LightLine3, 1, 0);
            this.FooterLayout.Controls.Add(this.LightLine4, 0, 0);
            this.FooterLayout.Controls.Add(this.FooterIcon, 0, 1);
            this.FooterLayout.Controls.Add(this.FooterItems, 1, 1);
            this.FooterLayout.Name = "FooterLayout";
            // 
            // LightLine3
            // 
            this.LightLine3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(61)))), ((int)(((byte)(61)))), ((int)(((byte)(61)))));
            resources.ApplyResources(this.LightLine3, "LightLine3");
            this.LightLine3.Name = "LightLine3";
            // 
            // LightLine4
            // 
            this.LightLine4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(61)))), ((int)(((byte)(61)))), ((int)(((byte)(61)))));
            resources.ApplyResources(this.LightLine4, "LightLine4");
            this.LightLine4.Name = "LightLine4";
            // 
            // FooterIcon
            // 
            resources.ApplyResources(this.FooterIcon, "FooterIcon");
            this.FooterIcon.Image = global::Ripperoni.Properties.Resources.aproxiconorg;
            this.FooterIcon.Name = "FooterIcon";
            this.FooterIcon.TabStop = false;
            this.FooterIcon.Click += new System.EventHandler(this.FooterIcon_Click);
            // 
            // FooterItems
            // 
            resources.ApplyResources(this.FooterItems, "FooterItems");
            this.FooterItems.Controls.Add(this.Repository, 0, 0);
            this.FooterItems.Controls.Add(this.Folder, 0, 0);
            this.FooterItems.Controls.Add(this.Copyright, 0, 0);
            this.FooterItems.Controls.Add(this.Website, 0, 0);
            this.FooterItems.Name = "FooterItems";
            // 
            // Repository
            // 
            resources.ApplyResources(this.Repository, "Repository");
            this.Repository.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(122)))), ((int)(((byte)(204)))));
            this.Repository.Name = "Repository";
            this.Repository.Click += new System.EventHandler(this.Repository_Click);
            this.Repository.MouseLeave += new System.EventHandler(this.Repository_MouseLeave);
            this.Repository.MouseHover += new System.EventHandler(this.Repository_MouseHover);
            // 
            // Folder
            // 
            resources.ApplyResources(this.Folder, "Folder");
            this.Folder.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(122)))), ((int)(((byte)(204)))));
            this.Folder.Name = "Folder";
            this.Folder.Click += new System.EventHandler(this.Folder_Click);
            this.Folder.MouseLeave += new System.EventHandler(this.Settings_MouseLeave);
            this.Folder.MouseHover += new System.EventHandler(this.Settings_MouseHover);
            // 
            // Copyright
            // 
            resources.ApplyResources(this.Copyright, "Copyright");
            this.Copyright.ForeColor = System.Drawing.SystemColors.Control;
            this.Copyright.Name = "Copyright";
            // 
            // Website
            // 
            resources.ApplyResources(this.Website, "Website");
            this.Website.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(122)))), ((int)(((byte)(204)))));
            this.Website.Name = "Website";
            this.Website.Click += new System.EventHandler(this.Website_Click);
            this.Website.MouseLeave += new System.EventHandler(this.Website_MouseLeave);
            this.Website.MouseHover += new System.EventHandler(this.Website_MouseHover);
            // 
            // LinedLayout
            // 
            resources.ApplyResources(this.LinedLayout, "LinedLayout");
            this.LinedLayout.Controls.Add(this.LightLine, 0, 0);
            this.LinedLayout.Controls.Add(this.LigntLine2, 0, 2);
            this.LinedLayout.Controls.Add(this.Records, 0, 1);
            this.LinedLayout.Name = "LinedLayout";
            // 
            // LightLine
            // 
            this.LightLine.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(61)))), ((int)(((byte)(61)))), ((int)(((byte)(61)))));
            resources.ApplyResources(this.LightLine, "LightLine");
            this.LightLine.Name = "LightLine";
            // 
            // LigntLine2
            // 
            this.LigntLine2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(61)))), ((int)(((byte)(61)))), ((int)(((byte)(61)))));
            resources.ApplyResources(this.LigntLine2, "LigntLine2");
            this.LigntLine2.Name = "LigntLine2";
            // 
            // Records
            // 
            resources.ApplyResources(this.Records, "Records");
            this.Records.Name = "Records";
            // 
            // SettingsLayout
            // 
            resources.ApplyResources(this.SettingsLayout, "SettingsLayout");
            this.SettingsLayout.Controls.Add(this.Elements, 2, 2);
            this.SettingsLayout.Controls.Add(this.Resolution, 1, 2);
            this.SettingsLayout.Controls.Add(this.Format, 0, 2);
            this.SettingsLayout.Controls.Add(this.FormatLabel, 0, 1);
            this.SettingsLayout.Controls.Add(this.ResolutionLabel, 1, 1);
            this.SettingsLayout.Controls.Add(this.ElementsLabel, 2, 1);
            this.SettingsLayout.Name = "SettingsLayout";
            // 
            // Elements
            // 
            this.Elements.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.Elements.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.Elements.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(20)))), ((int)(((byte)(20)))));
            resources.ApplyResources(this.Elements, "Elements");
            this.Elements.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.Elements.ForeColor = System.Drawing.SystemColors.Control;
            this.Elements.FormattingEnabled = true;
            this.Elements.Items.AddRange(new object[] {
            resources.GetString("Elements.Items"),
            resources.GetString("Elements.Items1"),
            resources.GetString("Elements.Items2")});
            this.Elements.Name = "Elements";
            // 
            // Resolution
            // 
            this.Resolution.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(20)))), ((int)(((byte)(20)))));
            resources.ApplyResources(this.Resolution, "Resolution");
            this.Resolution.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.Resolution.ForeColor = System.Drawing.SystemColors.Control;
            this.Resolution.FormattingEnabled = true;
            this.Resolution.Items.AddRange(new object[] {
            resources.GetString("Resolution.Items"),
            resources.GetString("Resolution.Items1"),
            resources.GetString("Resolution.Items2"),
            resources.GetString("Resolution.Items3"),
            resources.GetString("Resolution.Items4"),
            resources.GetString("Resolution.Items5"),
            resources.GetString("Resolution.Items6"),
            resources.GetString("Resolution.Items7"),
            resources.GetString("Resolution.Items8")});
            this.Resolution.Name = "Resolution";
            // 
            // Format
            // 
            this.Format.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(20)))), ((int)(((byte)(20)))));
            resources.ApplyResources(this.Format, "Format");
            this.Format.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.Format.ForeColor = System.Drawing.SystemColors.Control;
            this.Format.FormattingEnabled = true;
            this.Format.Items.AddRange(new object[] {
            resources.GetString("Format.Items"),
            resources.GetString("Format.Items1"),
            resources.GetString("Format.Items2"),
            resources.GetString("Format.Items3"),
            resources.GetString("Format.Items4"),
            resources.GetString("Format.Items5"),
            resources.GetString("Format.Items6"),
            resources.GetString("Format.Items7"),
            resources.GetString("Format.Items8"),
            resources.GetString("Format.Items9"),
            resources.GetString("Format.Items10"),
            resources.GetString("Format.Items11"),
            resources.GetString("Format.Items12")});
            this.Format.Name = "Format";
            this.Format.SelectedIndexChanged += new System.EventHandler(this.Format_SelectedIndexChanged);
            // 
            // FormatLabel
            // 
            resources.ApplyResources(this.FormatLabel, "FormatLabel");
            this.FormatLabel.ForeColor = System.Drawing.SystemColors.Control;
            this.FormatLabel.Name = "FormatLabel";
            // 
            // ResolutionLabel
            // 
            resources.ApplyResources(this.ResolutionLabel, "ResolutionLabel");
            this.ResolutionLabel.ForeColor = System.Drawing.SystemColors.Control;
            this.ResolutionLabel.Name = "ResolutionLabel";
            // 
            // ElementsLabel
            // 
            resources.ApplyResources(this.ElementsLabel, "ElementsLabel");
            this.ElementsLabel.ForeColor = System.Drawing.SystemColors.Control;
            this.ElementsLabel.Name = "ElementsLabel";
            // 
            // Main
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.Controls.Add(this.MainLayout);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "Main";
            this.Load += new System.EventHandler(this.Main_Load);
            this.MainLayout.ResumeLayout(false);
            this.HandleLayout.ResumeLayout(false);
            this.HandleLayout.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Icon)).EndInit();
            this.OutputLayout.ResumeLayout(false);
            this.OutputLayout.PerformLayout();
            this.FooterLayout.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.FooterIcon)).EndInit();
            this.FooterItems.ResumeLayout(false);
            this.FooterItems.PerformLayout();
            this.LinedLayout.ResumeLayout(false);
            this.SettingsLayout.ResumeLayout(false);
            this.SettingsLayout.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel MainLayout;
        private System.Windows.Forms.TableLayoutPanel HandleLayout;
        private new System.Windows.Forms.PictureBox Icon;
        private System.Windows.Forms.Label Title;
        private System.Windows.Forms.Button Exit;
        private System.Windows.Forms.TableLayoutPanel OutputLayout;
        private System.Windows.Forms.TableLayoutPanel FooterLayout;
        private System.Windows.Forms.TableLayoutPanel LinedLayout;
        private System.Windows.Forms.Panel LightLine;
        private System.Windows.Forms.Panel LigntLine2;
        private System.Windows.Forms.Panel LightLine3;
        private System.Windows.Forms.Panel LightLine4;
        private System.Windows.Forms.PictureBox FooterIcon;
        private System.Windows.Forms.TableLayoutPanel FooterItems;
        private System.Windows.Forms.Label Website;
        private System.Windows.Forms.Label Repository;
        private System.Windows.Forms.Label Folder;
        private System.Windows.Forms.Label Copyright;
        private System.Windows.Forms.ComboBox Format;
        private System.Windows.Forms.Button Convert;
        private System.Windows.Forms.TextBox Output;
        private System.Windows.Forms.Button OpenFolder;
        private System.Windows.Forms.Button Minimize;
        private System.Windows.Forms.Button Metadata;
        private System.Windows.Forms.Button Support;
        private System.Windows.Forms.TextBox Input;
        private System.Windows.Forms.ComboBox Resolution;
        private System.Windows.Forms.ComboBox Elements;
        private System.Windows.Forms.TableLayoutPanel SettingsLayout;
        private System.Windows.Forms.Label LocationLabel;
        private System.Windows.Forms.Label FormatLabel;
        private System.Windows.Forms.Label ResolutionLabel;
        private System.Windows.Forms.Label ElementsLabel;
        private System.Windows.Forms.FlowLayoutPanel Records;
        private System.Windows.Forms.Button Settings;
    }
}

