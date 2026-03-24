using System;
using System.Drawing;
using System.Windows.Forms;

partial class screenScreenA
{
    private System.ComponentModel.IContainer components = null;

    protected override void Dispose(bool disposing)
    {
        if (disposing && (components != null))
            components.Dispose();
        base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
        this.buttonCancel = new BPAddin.UIButton();
        this.editEditControlA = new BPAddin.UITextBox();
        this.statictextLabelA = new BPAddin.UILabel();
        this.buttonOK = new BPAddin.UIButton();
        this.SuspendLayout();

        // buttonCancel
        this.buttonCancel.Name = "buttonCancel";
        this.buttonCancel.Text = "Cancel";
        this.buttonCancel.Location = new System.Drawing.Point(266, 170);
        this.buttonCancel.Size = new System.Drawing.Size(100, 28);
        this.Controls.Add(this.buttonCancel);

        // editEditControlA
        this.editEditControlA.Name = "editEditControlA";
        this.editEditControlA.Text = "Edit Control A";
        this.editEditControlA.Location = new System.Drawing.Point(38, 44);
        this.editEditControlA.Size = new System.Drawing.Size(328, 30);
        this.Controls.Add(this.editEditControlA);

        // statictextLabelA
        this.statictextLabelA.Name = "statictextLabelA";
        this.statictextLabelA.Text = "Label A";
        this.statictextLabelA.Location = new System.Drawing.Point(40, 18);
        this.statictextLabelA.Size = new System.Drawing.Size(100, 16);
        this.Controls.Add(this.statictextLabelA);

        // buttonOK
        this.buttonOK.Name = "buttonOK";
        this.buttonOK.Text = "OK";
        this.buttonOK.Location = new System.Drawing.Point(40, 170);
        this.buttonOK.Size = new System.Drawing.Size(100, 28);
        this.Controls.Add(this.buttonOK);

        // screenScreenA
        this.Text = "screenScreenA";
        this.ClientSize = new System.Drawing.Size(406, 220);
        this.ResumeLayout(false);
    }

    private BPAddin.UIButton buttonCancel;
    private BPAddin.UITextBox editEditControlA;
    private BPAddin.UILabel statictextLabelA;
    private BPAddin.UIButton buttonOK;
}
