namespace BPAddin
{
    partial class CodeGeneratorForm
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
            this.lblText = new System.Windows.Forms.Label();
            this.btnGenerate = new System.Windows.Forms.Button();
            this.cbxPackages = new System.Windows.Forms.ComboBox();
            this.lblGenerated = new System.Windows.Forms.Label();
            this.lblProject = new System.Windows.Forms.Label();
            this.tbxGenerated = new System.Windows.Forms.TextBox();
            this.tbxProject = new System.Windows.Forms.TextBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.lblMainScreen = new System.Windows.Forms.Label();
            this.cbxMainScreen = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // lblText
            // 
            this.lblText.AutoSize = true;
            this.lblText.Location = new System.Drawing.Point(13, 13);
            this.lblText.Name = "lblText";
            this.lblText.Size = new System.Drawing.Size(38, 13);
            this.lblText.TabIndex = 0;
            this.lblText.Text = "lblText";
            // 
            // btnGenerate
            // 
            this.btnGenerate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnGenerate.Location = new System.Drawing.Point(350, 229);
            this.btnGenerate.Name = "btnGenerate";
            this.btnGenerate.Size = new System.Drawing.Size(77, 23);
            this.btnGenerate.TabIndex = 2;
            this.btnGenerate.Text = "Generate!";
            this.btnGenerate.UseVisualStyleBackColor = true;
            this.btnGenerate.Click += new System.EventHandler(this.btnGenerate_Click);
            // 
            // cbxPackages
            // 
            this.cbxPackages.FormattingEnabled = true;
            this.cbxPackages.Location = new System.Drawing.Point(14, 29);
            this.cbxPackages.Name = "cbxPackages";
            this.cbxPackages.Size = new System.Drawing.Size(173, 21);
            this.cbxPackages.TabIndex = 3;
            // 
            // lblGenerated
            // 
            this.lblGenerated.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblGenerated.AutoSize = true;
            this.lblGenerated.Location = new System.Drawing.Point(13, 132);
            this.lblGenerated.Name = "lblGenerated";
            this.lblGenerated.Size = new System.Drawing.Size(91, 13);
            this.lblGenerated.TabIndex = 4;
            this.lblGenerated.Text = "Generation folder:";
            // 
            // lblProject
            // 
            this.lblProject.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblProject.AutoSize = true;
            this.lblProject.Location = new System.Drawing.Point(13, 158);
            this.lblProject.Name = "lblProject";
            this.lblProject.Size = new System.Drawing.Size(119, 13);
            this.lblProject.TabIndex = 5;
            this.lblProject.Text = "Prototype project folder:";
            // 
            // tbxGenerated
            // 
            this.tbxGenerated.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbxGenerated.Location = new System.Drawing.Point(138, 129);
            this.tbxGenerated.Name = "tbxGenerated";
            this.tbxGenerated.ReadOnly = true;
            this.tbxGenerated.Size = new System.Drawing.Size(277, 20);
            this.tbxGenerated.TabIndex = 6;
            // 
            // tbxProject
            // 
            this.tbxProject.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbxProject.Location = new System.Drawing.Point(138, 155);
            this.tbxProject.Name = "tbxProject";
            this.tbxProject.ReadOnly = true;
            this.tbxProject.Size = new System.Drawing.Size(277, 20);
            this.tbxProject.TabIndex = 7;
            // 
            // panel1
            // 
            this.panel1.AutoSize = true;
            this.panel1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.panel1.Location = new System.Drawing.Point(12, 65);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(0, 0);
            this.panel1.TabIndex = 9;
            // 
            // lblMainScreen
            // 
            this.lblMainScreen.AutoSize = true;
            this.lblMainScreen.Location = new System.Drawing.Point(13, 65);
            this.lblMainScreen.Name = "lblMainScreen";
            this.lblMainScreen.Size = new System.Drawing.Size(74, 13);
            this.lblMainScreen.TabIndex = 10;
            this.lblMainScreen.Text = "lblMainScreen";
            // 
            // cbxMainScreen
            // 
            this.cbxMainScreen.FormattingEnabled = true;
            this.cbxMainScreen.Location = new System.Drawing.Point(14, 81);
            this.cbxMainScreen.Name = "cbxMainScreen";
            this.cbxMainScreen.Size = new System.Drawing.Size(173, 21);
            this.cbxMainScreen.TabIndex = 11;
            // 
            // CodeGeneratorForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(439, 264);
            this.Controls.Add(this.cbxMainScreen);
            this.Controls.Add(this.lblMainScreen);
            this.Controls.Add(this.tbxGenerated);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.tbxProject);
            this.Controls.Add(this.lblGenerated);
            this.Controls.Add(this.cbxPackages);
            this.Controls.Add(this.lblProject);
            this.Controls.Add(this.btnGenerate);
            this.Controls.Add(this.lblText);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "CodeGeneratorForm";
            this.Text = "Generate code from model";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblText;
        private System.Windows.Forms.Button btnGenerate;
        private System.Windows.Forms.ComboBox cbxPackages;
        private System.Windows.Forms.Label lblGenerated;
        private System.Windows.Forms.Label lblProject;
        private System.Windows.Forms.TextBox tbxGenerated;
        private System.Windows.Forms.TextBox tbxProject;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label lblMainScreen;
        private System.Windows.Forms.ComboBox cbxMainScreen;
    }
}