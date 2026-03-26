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
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
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
            this.panel1.Size = new System.Drawing.Size(776, 235);
            this.panel1.TabIndex = 0;
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
    }
}