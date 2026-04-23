using System;
using System.Drawing;
using System.Windows.Forms;

namespace class diagram
{
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
        this.SuspendLayout();

        //
        // screenScreenA
        //
        this.Text = "screenScreenA";
        this.ClientSize = new System.Drawing.Size(800, 600);
        this.ResumeLayout(false);
    }

}
}
