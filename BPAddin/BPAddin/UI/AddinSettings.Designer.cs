namespace BPAddin
{
    partial class SettingsForm
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnSync = new System.Windows.Forms.Button();
            this.tbxUIDiagramPkg = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.tbxApplicationCDPkg = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnProject = new System.Windows.Forms.Button();
            this.btnUILib = new System.Windows.Forms.Button();
            this.btnGenerated = new System.Windows.Forms.Button();
            this.tbxProject = new System.Windows.Forms.TextBox();
            this.tbxUILib = new System.Windows.Forms.TextBox();
            this.tbxGenerated = new System.Windows.Forms.TextBox();
            this.lblOutProject = new System.Windows.Forms.Label();
            this.lblUILibrary = new System.Windows.Forms.Label();
            this.lblGenerated = new System.Windows.Forms.Label();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnImportUILib = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btnImportUILib);
            this.panel1.Controls.Add(this.btnSync);
            this.panel1.Controls.Add(this.tbxUIDiagramPkg);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.tbxApplicationCDPkg);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.btnProject);
            this.panel1.Controls.Add(this.btnUILib);
            this.panel1.Controls.Add(this.btnGenerated);
            this.panel1.Controls.Add(this.tbxProject);
            this.panel1.Controls.Add(this.tbxUILib);
            this.panel1.Controls.Add(this.tbxGenerated);
            this.panel1.Controls.Add(this.lblOutProject);
            this.panel1.Controls.Add(this.lblUILibrary);
            this.panel1.Controls.Add(this.lblGenerated);
            this.panel1.Location = new System.Drawing.Point(12, 12);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(776, 269);
            this.panel1.TabIndex = 0;
            // 
            // btnSync
            // 
            this.btnSync.Location = new System.Drawing.Point(633, 156);
            this.btnSync.Name = "btnSync";
            this.btnSync.Size = new System.Drawing.Size(131, 23);
            this.btnSync.TabIndex = 14;
            this.btnSync.Text = "Synchronize UI Model";
            this.btnSync.UseVisualStyleBackColor = true;
            this.btnSync.Click += new System.EventHandler(this.btnSync_Click);
            // 
            // tbxUIDiagramPkg
            // 
            this.tbxUIDiagramPkg.Location = new System.Drawing.Point(201, 130);
            this.tbxUIDiagramPkg.Name = "tbxUIDiagramPkg";
            this.tbxUIDiagramPkg.Size = new System.Drawing.Size(563, 20);
            this.tbxUIDiagramPkg.TabIndex = 13;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(20, 132);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(108, 13);
            this.label2.TabIndex = 12;
            this.label2.Text = "UI Diagram package:";
            // 
            // tbxApplicationCDPkg
            // 
            this.tbxApplicationCDPkg.Location = new System.Drawing.Point(201, 104);
            this.tbxApplicationCDPkg.Name = "tbxApplicationCDPkg";
            this.tbxApplicationCDPkg.Size = new System.Drawing.Size(563, 20);
            this.tbxApplicationCDPkg.TabIndex = 11;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(17, 104);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(177, 13);
            this.label1.TabIndex = 10;
            this.label1.Text = "Application Class Diagram package:";
            // 
            // btnProject
            // 
            this.btnProject.Location = new System.Drawing.Point(712, 68);
            this.btnProject.Name = "btnProject";
            this.btnProject.Size = new System.Drawing.Size(52, 20);
            this.btnProject.TabIndex = 9;
            this.btnProject.Text = "...";
            this.btnProject.UseVisualStyleBackColor = true;
            this.btnProject.Click += new System.EventHandler(this.btnProject_Click);
            // 
            // btnUILib
            // 
            this.btnUILib.Location = new System.Drawing.Point(712, 42);
            this.btnUILib.Name = "btnUILib";
            this.btnUILib.Size = new System.Drawing.Size(52, 20);
            this.btnUILib.TabIndex = 8;
            this.btnUILib.Text = "...";
            this.btnUILib.UseVisualStyleBackColor = true;
            this.btnUILib.Click += new System.EventHandler(this.btnUILib_Click);
            // 
            // btnGenerated
            // 
            this.btnGenerated.Location = new System.Drawing.Point(712, 16);
            this.btnGenerated.Name = "btnGenerated";
            this.btnGenerated.Size = new System.Drawing.Size(52, 20);
            this.btnGenerated.TabIndex = 7;
            this.btnGenerated.Text = "...";
            this.btnGenerated.UseVisualStyleBackColor = true;
            this.btnGenerated.Click += new System.EventHandler(this.btnGenerated_Click);
            // 
            // tbxProject
            // 
            this.tbxProject.Location = new System.Drawing.Point(184, 68);
            this.tbxProject.Name = "tbxProject";
            this.tbxProject.Size = new System.Drawing.Size(511, 20);
            this.tbxProject.TabIndex = 6;
            // 
            // tbxUILib
            // 
            this.tbxUILib.Location = new System.Drawing.Point(184, 42);
            this.tbxUILib.Name = "tbxUILib";
            this.tbxUILib.Size = new System.Drawing.Size(511, 20);
            this.tbxUILib.TabIndex = 5;
            // 
            // tbxGenerated
            // 
            this.tbxGenerated.Location = new System.Drawing.Point(184, 16);
            this.tbxGenerated.Name = "tbxGenerated";
            this.tbxGenerated.Size = new System.Drawing.Size(511, 20);
            this.tbxGenerated.TabIndex = 4;
            // 
            // lblOutProject
            // 
            this.lblOutProject.AutoSize = true;
            this.lblOutProject.Location = new System.Drawing.Point(17, 71);
            this.lblOutProject.Name = "lblOutProject";
            this.lblOutProject.Size = new System.Drawing.Size(102, 13);
            this.lblOutProject.TabIndex = 3;
            this.lblOutProject.Text = "Directory for project:";
            // 
            // lblUILibrary
            // 
            this.lblUILibrary.AutoSize = true;
            this.lblUILibrary.Location = new System.Drawing.Point(17, 44);
            this.lblUILibrary.Name = "lblUILibrary";
            this.lblUILibrary.Size = new System.Drawing.Size(158, 13);
            this.lblUILibrary.TabIndex = 2;
            this.lblUILibrary.Text = "Directory for imported UI Library:";
            // 
            // lblGenerated
            // 
            this.lblGenerated.AutoSize = true;
            this.lblGenerated.Location = new System.Drawing.Point(17, 16);
            this.lblGenerated.Name = "lblGenerated";
            this.lblGenerated.Size = new System.Drawing.Size(139, 13);
            this.lblGenerated.TabIndex = 0;
            this.lblGenerated.Text = "Directory for generated files:";
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(632, 415);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 23);
            this.btnSave.TabIndex = 1;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(713, 415);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 2;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnImportUILib
            // 
            this.btnImportUILib.Location = new System.Drawing.Point(633, 185);
            this.btnImportUILib.Name = "btnImportUILib";
            this.btnImportUILib.Size = new System.Drawing.Size(131, 23);
            this.btnImportUILib.TabIndex = 15;
            this.btnImportUILib.Text = "Import UI Library";
            this.btnImportUILib.UseVisualStyleBackColor = true;
            this.btnImportUILib.Click += new System.EventHandler(this.btnImportUILib_Click);
            // 
            // SettingsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.panel1);
            this.Name = "SettingsForm";
            this.Text = "AddinSettings";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label lblGenerated;
        private System.Windows.Forms.Label lblUILibrary;
        private System.Windows.Forms.TextBox tbxProject;
        private System.Windows.Forms.TextBox tbxUILib;
        private System.Windows.Forms.TextBox tbxGenerated;
        private System.Windows.Forms.Label lblOutProject;
        private System.Windows.Forms.Button btnProject;
        private System.Windows.Forms.Button btnUILib;
        private System.Windows.Forms.Button btnGenerated;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.TextBox tbxApplicationCDPkg;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox tbxUIDiagramPkg;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnSync;
        private System.Windows.Forms.Button btnImportUILib;
    }
}