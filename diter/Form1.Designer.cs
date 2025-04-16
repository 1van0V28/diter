namespace diter;

partial class Form1
{
    /// <summary>
    ///  Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    /// <summary>
    ///  Clean up any resources being used.
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
        SplitContainer1 = new System.Windows.Forms.SplitContainer();
        ((System.ComponentModel.ISupportInitialize)SplitContainer1).BeginInit();
        SplitContainer1.SuspendLayout();
        SuspendLayout();
        // 
        // SplitContainer1
        // 
        SplitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
        SplitContainer1.Location = new System.Drawing.Point(0, 0);
        SplitContainer1.Name = "SplitContainer1";
        SplitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
        // 
        // SplitContainer1.Panel2
        SplitContainer1.Panel2.Paint += DrawPanel_Paint;
        SplitContainer1.Panel2.MouseDown += DrawPanel_MouseDown;
        SplitContainer1.Panel2.MouseMove += DrawPanel_MouseMove;
        SplitContainer1.Panel2.MouseUp += DrawPanel_MouseUp;
        //
        SplitContainer1.Size = new System.Drawing.Size(800, 450);
        SplitContainer1.SplitterDistance = 100;
        SplitContainer1.TabIndex = 0;
        SplitContainer1.Text = "splitContainer1";
        // 
        // Form1
        // 
        AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
        AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        ClientSize = new System.Drawing.Size(800, 450);
        Controls.Add(SplitContainer1);
        Text = "Form1";
        ((System.ComponentModel.ISupportInitialize)SplitContainer1).EndInit();
        SplitContainer1.ResumeLayout(false);
        ResumeLayout(false);
    }

    private System.Windows.Forms.SplitContainer SplitContainer1;

    #endregion
}