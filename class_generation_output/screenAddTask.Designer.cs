using System;
using System.Drawing;
using System.Windows.Forms;

namespace ApplicationModel
{
partial class screenAddTask
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
        this.labelAddTaskDescription = new labelAddTaskDescription();
        this.buttonCancel = new buttonCancel();
        this.buttonSave = new buttonSave();
        this.buttonSave.Click += new System.EventHandler(this.onbuttonSaveClick);
        this.labelTaskDescription = new labelTaskDescription();
        this.textBoxTaskName = new textBoxTaskName();
        this.SuspendLayout();

        //
        // labelAddTaskDescription
        //
        this.labelAddTaskDescription.Name = "labelAddTaskDescription";
        this.labelAddTaskDescription.Text = "AddTaskDescription";
        this.labelAddTaskDescription.Location = new System.Drawing.Point(34, 26);
        this.labelAddTaskDescription.Size = new System.Drawing.Size(166, 18);
        this.Controls.Add(this.labelAddTaskDescription);

        //
        // buttonCancel
        //
        this.buttonCancel.Name = "buttonCancel";
        this.buttonCancel.Text = "Cancel";
        this.buttonCancel.Location = new System.Drawing.Point(42, 318);
        this.buttonCancel.Size = new System.Drawing.Size(100, 28);
        this.Controls.Add(this.buttonCancel);

        //
        // buttonSave
        //
        this.buttonSave.Name = "buttonSave";
        this.buttonSave.Text = "Save";
        this.buttonSave.Location = new System.Drawing.Point(506, 318);
        this.buttonSave.Size = new System.Drawing.Size(100, 28);
        this.Controls.Add(this.buttonSave);

        //
        // labelTaskDescription
        //
        this.labelTaskDescription.Name = "labelTaskDescription";
        this.labelTaskDescription.Text = "TaskDescription";
        this.labelTaskDescription.Location = new System.Drawing.Point(42, 64);
        this.labelTaskDescription.Size = new System.Drawing.Size(100, 16);
        this.Controls.Add(this.labelTaskDescription);

        //
        // textBoxTaskName
        //
        this.textBoxTaskName.Name = "textBoxTaskName";
        this.textBoxTaskName.Text = "TaskName";
        this.textBoxTaskName.Location = new System.Drawing.Point(154, 58);
        this.textBoxTaskName.Size = new System.Drawing.Size(120, 28);
        this.Controls.Add(this.textBoxTaskName);

        //
        // screenAddTask
        //
        this.Text = "screenAddTask";
        this.ClientSize = new System.Drawing.Size(632, 366);
        this.ResumeLayout(false);
    }

    private BPAddin.UILabel labelAddTaskDescription;
    private BPAddin.UIButton buttonCancel;
    private BPAddin.UIButton buttonSave;
    private BPAddin.UILabel labelTaskDescription;
    private BPAddin.UITextBox textBoxTaskName;
}
}
