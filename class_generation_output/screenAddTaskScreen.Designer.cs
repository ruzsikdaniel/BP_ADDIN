using System;
using System.Drawing;
using System.Windows.Forms;

namespace ApplicationModel
{
partial class screenAddTaskScreen
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
        this.buttonCancel = new buttonCancel();
        this.buttonCancel.Click += new System.EventHandler(this.onbuttonCancelClick);
        this.buttonSave = new buttonSave();
        this.buttonSave.Click += new System.EventHandler(this.onbuttonSaveClick);
        this.labelTaskDescription = new labelTaskDescription();
        this.textBoxTaskName = new textBoxTaskName();
        this.SuspendLayout();

        //
        // buttonCancel
        //
        this.buttonCancel.Name = "buttonCancel";
        this.buttonCancel.Text = "Cancel";
        this.buttonCancel.Location = new System.Drawing.Point(492, 316);
        this.buttonCancel.Size = new System.Drawing.Size(92, 28);
        this.Controls.Add(this.buttonCancel);

        //
        // buttonSave
        //
        this.buttonSave.Name = "buttonSave";
        this.buttonSave.Text = "Save";
        this.buttonSave.Location = new System.Drawing.Point(362, 316);
        this.buttonSave.Size = new System.Drawing.Size(100, 28);
        this.Controls.Add(this.buttonSave);

        //
        // labelTaskDescription
        //
        this.labelTaskDescription.Name = "labelTaskDescription";
        this.labelTaskDescription.Text = "TaskDescription";
        this.labelTaskDescription.Location = new System.Drawing.Point(172, 62);
        this.labelTaskDescription.Size = new System.Drawing.Size(112, 14);
        this.Controls.Add(this.labelTaskDescription);

        //
        // textBoxTaskName
        //
        this.textBoxTaskName.Name = "textBoxTaskName";
        this.textBoxTaskName.Text = "TaskName";
        this.textBoxTaskName.Location = new System.Drawing.Point(44, 54);
        this.textBoxTaskName.Size = new System.Drawing.Size(100, 28);
        this.Controls.Add(this.textBoxTaskName);

        //
        // screenAddTaskScreen
        //
        this.Text = "screenAddTaskScreen";
        this.ClientSize = new System.Drawing.Size(632, 366);
        this.ResumeLayout(false);
    }

    private BPAddin.UIButton buttonCancel;
    private BPAddin.UIButton buttonSave;
    private BPAddin.UILabel labelTaskDescription;
    private BPAddin.UITextBox textBoxTaskName;
}
}
