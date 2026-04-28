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
            this.cbxUIDiagramPkg = new System.Windows.Forms.ComboBox();
            this.cbxAppCDPkg = new System.Windows.Forms.ComboBox();
            this.btnImportUILib = new System.Windows.Forms.Button();
            this.btnSync = new System.Windows.Forms.Button();
            this.lblUIDiagramPkg = new System.Windows.Forms.Label();
            this.lblAppCDPkg = new System.Windows.Forms.Label();
            this.btnProject = new System.Windows.Forms.Button();
            this.btnGenerated = new System.Windows.Forms.Button();
            this.tbxProject = new System.Windows.Forms.TextBox();
            this.tbxUILib = new System.Windows.Forms.TextBox();
            this.tbxGenerated = new System.Windows.Forms.TextBox();
            this.lblOutProject = new System.Windows.Forms.Label();
            this.lblUILibrary = new System.Windows.Forms.Label();
            this.lblGenerated = new System.Windows.Forms.Label();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.cbxUIDiagramPkg);
            this.panel1.Controls.Add(this.cbxAppCDPkg);
            this.panel1.Controls.Add(this.btnImportUILib);
            this.panel1.Controls.Add(this.btnSync);
            this.panel1.Controls.Add(this.lblUIDiagramPkg);
            this.panel1.Controls.Add(this.lblAppCDPkg);
            this.panel1.Controls.Add(this.btnProject);
            this.panel1.Controls.Add(this.btnGenerated);
            this.panel1.Controls.Add(this.tbxProject);
            this.panel1.Controls.Add(this.tbxUILib);
            this.panel1.Controls.Add(this.tbxGenerated);
            this.panel1.Controls.Add(this.lblOutProject);
            this.panel1.Controls.Add(this.lblUILibrary);
            this.panel1.Controls.Add(this.lblGenerated);
            this.panel1.Location = new System.Drawing.Point(10, 12);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(667, 174);
            this.panel1.TabIndex = 0;
            // 
            // cbxUIDiagramPkg
            // 
            this.cbxUIDiagramPkg.FormattingEnabled = true;
            this.cbxUIDiagramPkg.Location = new System.Drawing.Point(201, 128);
            this.cbxUIDiagramPkg.Name = "cbxUIDiagramPkg";
            this.cbxUIDiagramPkg.Size = new System.Drawing.Size(300, 21);
            this.cbxUIDiagramPkg.TabIndex = 17;
            // 
            // cbxAppCDPkg
            // 
            this.cbxAppCDPkg.FormattingEnabled = true;
            this.cbxAppCDPkg.Location = new System.Drawing.Point(201, 101);
            this.cbxAppCDPkg.Name = "cbxAppCDPkg";
            this.cbxAppCDPkg.Size = new System.Drawing.Size(300, 21);
            this.cbxAppCDPkg.TabIndex = 16;
            // 
            // btnImportUILib
            // 
            this.btnImportUILib.Location = new System.Drawing.Point(605, 14);
            this.btnImportUILib.Name = "btnImportUILib";
            this.btnImportUILib.Size = new System.Drawing.Size(52, 23);
            this.btnImportUILib.TabIndex = 15;
            this.btnImportUILib.Text = "Import";
            this.btnImportUILib.UseVisualStyleBackColor = true;
            this.btnImportUILib.Click += new System.EventHandler(this.btnImportUILib_Click);
            // 
            // btnSync
            // 
            this.btnSync.Location = new System.Drawing.Point(526, 101);
            this.btnSync.Name = "btnSync";
            this.btnSync.Size = new System.Drawing.Size(131, 25);
            this.btnSync.TabIndex = 14;
            this.btnSync.Text = "Synchronize UI Model";
            this.btnSync.UseVisualStyleBackColor = true;
            this.btnSync.Click += new System.EventHandler(this.btnSync_Click);
            // 
            // lblUIDiagramPkg
            // 
            this.lblUIDiagramPkg.AutoSize = true;
            this.lblUIDiagramPkg.Location = new System.Drawing.Point(17, 133);
            this.lblUIDiagramPkg.Name = "lblUIDiagramPkg";
            this.lblUIDiagramPkg.Size = new System.Drawing.Size(108, 13);
            this.lblUIDiagramPkg.TabIndex = 12;
            this.lblUIDiagramPkg.Text = "UI Diagram package:";
            // 
            // lblAppCDPkg
            // 
            this.lblAppCDPkg.AutoSize = true;
            this.lblAppCDPkg.Location = new System.Drawing.Point(17, 104);
            this.lblAppCDPkg.Name = "lblAppCDPkg";
            this.lblAppCDPkg.Size = new System.Drawing.Size(177, 13);
            this.lblAppCDPkg.TabIndex = 10;
            this.lblAppCDPkg.Text = "Application Class Diagram package:";
            // 
            // btnProject
            // 
            this.btnProject.Location = new System.Drawing.Point(605, 68);
            this.btnProject.Name = "btnProject";
            this.btnProject.Size = new System.Drawing.Size(52, 20);
            this.btnProject.TabIndex = 9;
            this.btnProject.Text = "...";
            this.btnProject.UseVisualStyleBackColor = true;
            this.btnProject.Click += new System.EventHandler(this.btnProject_Click);
            // 
            // btnGenerated
            // 
            this.btnGenerated.Location = new System.Drawing.Point(605, 42);
            this.btnGenerated.Name = "btnGenerated";
            this.btnGenerated.Size = new System.Drawing.Size(52, 20);
            this.btnGenerated.TabIndex = 8;
            this.btnGenerated.Text = "...";
            this.btnGenerated.UseVisualStyleBackColor = true;
            this.btnGenerated.Click += new System.EventHandler(this.btnGenerated_Click);
            // 
            // tbxProject
            // 
            this.tbxProject.Location = new System.Drawing.Point(151, 68);
            this.tbxProject.Name = "tbxProject";
            this.tbxProject.Size = new System.Drawing.Size(448, 20);
            this.tbxProject.TabIndex = 6;
            // 
            // tbxUILib
            // 
            this.tbxUILib.Enabled = false;
            this.tbxUILib.Location = new System.Drawing.Point(151, 16);
            this.tbxUILib.Name = "tbxUILib";
            this.tbxUILib.Size = new System.Drawing.Size(448, 20);
            this.tbxUILib.TabIndex = 5;
            // 
            // tbxGenerated
            // 
            this.tbxGenerated.Location = new System.Drawing.Point(151, 42);
            this.tbxGenerated.Name = "tbxGenerated";
            this.tbxGenerated.Size = new System.Drawing.Size(448, 20);
            this.tbxGenerated.TabIndex = 4;
            // 
            // lblOutProject
            // 
            this.lblOutProject.AutoSize = true;
            this.lblOutProject.Location = new System.Drawing.Point(17, 72);
            this.lblOutProject.Name = "lblOutProject";
            this.lblOutProject.Size = new System.Drawing.Size(119, 13);
            this.lblOutProject.TabIndex = 3;
            this.lblOutProject.Text = "Prototype project folder:";
            // 
            // lblUILibrary
            // 
            this.lblUILibrary.AutoSize = true;
            this.lblUILibrary.Location = new System.Drawing.Point(17, 18);
            this.lblUILibrary.Name = "lblUILibrary";
            this.lblUILibrary.Size = new System.Drawing.Size(128, 13);
            this.lblUILibrary.TabIndex = 2;
            this.lblUILibrary.Text = "Imported UI Library folder:";
            // 
            // lblGenerated
            // 
            this.lblGenerated.AutoSize = true;
            this.lblGenerated.Location = new System.Drawing.Point(17, 46);
            this.lblGenerated.Name = "lblGenerated";
            this.lblGenerated.Size = new System.Drawing.Size(110, 13);
            this.lblGenerated.TabIndex = 0;
            this.lblGenerated.Text = "Generated files folder:";
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(521, 415);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 23);
            this.btnSave.TabIndex = 1;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(602, 415);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 2;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // SettingsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(689, 450);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.btnSave);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
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
        private System.Windows.Forms.Button btnGenerated;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Label lblAppCDPkg;
        private System.Windows.Forms.Label lblUIDiagramPkg;
        private System.Windows.Forms.Button btnSync;
        private System.Windows.Forms.Button btnImportUILib;
        private System.Windows.Forms.ComboBox cbxUIDiagramPkg;
        private System.Windows.Forms.ComboBox cbxAppCDPkg;
    }
}