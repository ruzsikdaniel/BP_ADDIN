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
            this.btnGenerate.Location = new System.Drawing.Point(303, 129);
            this.btnGenerate.Name = "btnGenerate";
            this.btnGenerate.Size = new System.Drawing.Size(75, 23);
            this.btnGenerate.TabIndex = 2;
            this.btnGenerate.Text = "Generate!";
            this.btnGenerate.UseVisualStyleBackColor = true;
            this.btnGenerate.Click += new System.EventHandler(this.btnGenerate_Click);
            // 
            // cbxPackages
            // 
            this.cbxPackages.FormattingEnabled = true;
            this.cbxPackages.Location = new System.Drawing.Point(12, 40);
            this.cbxPackages.Name = "cbxPackages";
            this.cbxPackages.Size = new System.Drawing.Size(121, 21);
            this.cbxPackages.TabIndex = 3;
            // 
            // CodeGeneratorForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(390, 164);
            this.Controls.Add(this.cbxPackages);
            this.Controls.Add(this.btnGenerate);
            this.Controls.Add(this.lblText);
            this.Name = "CodeGeneratorForm";
            this.Text = "Generate code from model";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblText;
        private System.Windows.Forms.Button btnGenerate;
        private System.Windows.Forms.ComboBox cbxPackages;
    }
}