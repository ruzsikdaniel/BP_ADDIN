using System;
using System.Drawing;
using System.Windows.Forms;

namespace ApplicationModel
{
partial class screenMain
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
        this.buttonAddTask = new buttonAddTask();
        this.buttonAddTask.Click += new System.EventHandler(this.onbuttonAddTaskClick);
        this.buttonClose = new buttonClose();
        this.buttonClose.Click += new System.EventHandler(this.onbuttonCloseClick);
        this.labelDescription = new labelDescription();
        this.listBoxTasks = new listBoxTasks();
        this.SuspendLayout();

        //
        // buttonAddTask
        //
        this.buttonAddTask.Name = "buttonAddTask";
        this.buttonAddTask.Text = "Add Task";
        this.buttonAddTask.Location = new System.Drawing.Point(328, 258);
        this.buttonAddTask.Size = new System.Drawing.Size(100, 28);
        this.Controls.Add(this.buttonAddTask);

        //
        // buttonClose
        //
        this.buttonClose.Name = "buttonClose";
        this.buttonClose.Text = "Close";
        this.buttonClose.Location = new System.Drawing.Point(458, 258);
        this.buttonClose.Size = new System.Drawing.Size(100, 28);
        this.Controls.Add(this.buttonClose);

        //
        // labelDescription
        //
        this.labelDescription.Name = "labelDescription";
        this.labelDescription.Text = "Description";
        this.labelDescription.Location = new System.Drawing.Point(240, 32);
        this.labelDescription.Size = new System.Drawing.Size(306, 132);
        this.Controls.Add(this.labelDescription);

        //
        // listBoxTasks
        //
        this.listBoxTasks.Name = "listBoxTasks";
        this.listBoxTasks.Text = "Tasks";
        this.listBoxTasks.Location = new System.Drawing.Point(22, 24);
        this.listBoxTasks.Size = new System.Drawing.Size(186, 214);
        this.Controls.Add(this.listBoxTasks);

        //
        // screenMain
        //
        this.Text = "screenMain";
        this.ClientSize = new System.Drawing.Size(580, 318);
        this.ResumeLayout(false);
    }

    private BPAddin.UIButton buttonAddTask;
    private BPAddin.UIButton buttonClose;
    private BPAddin.UILabel labelDescription;
    private BPAddin.UIListBox listBoxTasks;
}
}
