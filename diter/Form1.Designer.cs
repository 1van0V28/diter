namespace diter;

sealed partial class Form1
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
        tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
        groupBox1 = new System.Windows.Forms.GroupBox();
        flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
        btnFill = new System.Windows.Forms.Button();
        btnDelete = new System.Windows.Forms.Button();
        groupBox2 = new System.Windows.Forms.GroupBox();
        flowLayoutPanel2 = new System.Windows.Forms.FlowLayoutPanel();
        btnPolyline = new System.Windows.Forms.Button();
        btnBezierCurve = new System.Windows.Forms.Button();
        btnEllipse = new System.Windows.Forms.Button();
        btnRect = new System.Windows.Forms.Button();
        btnTriangle = new System.Windows.Forms.Button();
        btnPentagon = new System.Windows.Forms.Button();
        btnTrapezoid = new System.Windows.Forms.Button();
        groupBox3 = new System.Windows.Forms.GroupBox();
        tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
        flowLayoutPanel3 = new System.Windows.Forms.FlowLayoutPanel();
        pnlColorView = new System.Windows.Forms.Panel();
        btnOther = new System.Windows.Forms.Button();
        flowLayoutPanel4 = new System.Windows.Forms.FlowLayoutPanel();
        btnBlack = new System.Windows.Forms.Button();
        btnGrey = new System.Windows.Forms.Button();
        btnWhite = new System.Windows.Forms.Button();
        btnRed = new System.Windows.Forms.Button();
        btnOrange = new System.Windows.Forms.Button();
        btnYellow = new System.Windows.Forms.Button();
        btnGreen = new System.Windows.Forms.Button();
        btnLightBlue = new System.Windows.Forms.Button();
        btnBlue = new System.Windows.Forms.Button();
        btnPurple = new System.Windows.Forms.Button();
        ((System.ComponentModel.ISupportInitialize)SplitContainer1).BeginInit();
        SplitContainer1.Panel1.SuspendLayout();
        SplitContainer1.SuspendLayout();
        tableLayoutPanel1.SuspendLayout();
        groupBox1.SuspendLayout();
        flowLayoutPanel1.SuspendLayout();
        groupBox2.SuspendLayout();
        flowLayoutPanel2.SuspendLayout();
        groupBox3.SuspendLayout();
        tableLayoutPanel2.SuspendLayout();
        flowLayoutPanel3.SuspendLayout();
        flowLayoutPanel4.SuspendLayout();
        SuspendLayout();
        // 
        // SplitContainer1
        // 
        SplitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
        SplitContainer1.Location = new System.Drawing.Point(0, 0);
        SplitContainer1.Name = "SplitContainer1";
        SplitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
        // 
        // SplitContainer1.Panel1
        // 
        SplitContainer1.Panel1.Controls.Add(tableLayoutPanel1);
        // 
        // SplitContainer1.Panel2
        // 
        SplitContainer1.Panel2.Paint += DrawPanel_Paint;
        SplitContainer1.Panel2.MouseDown += DrawPanel_MouseDown;
        SplitContainer1.Panel2.MouseMove += DrawPanel_MouseMove;
        SplitContainer1.Panel2.MouseUp += DrawPanel_MouseUp;
        SplitContainer1.Size = new System.Drawing.Size(800, 450);
        SplitContainer1.SplitterDistance = 147;
        SplitContainer1.TabIndex = 0;
        SplitContainer1.Text = "splitContainer1";
        // 
        // tableLayoutPanel1
        // 
        tableLayoutPanel1.ColumnCount = 3;
        tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
        tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 35F));
        tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 40F));
        tableLayoutPanel1.Controls.Add(groupBox1, 0, 0);
        tableLayoutPanel1.Controls.Add(groupBox2, 1, 0);
        tableLayoutPanel1.Controls.Add(groupBox3, 2, 0);
        tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
        tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
        tableLayoutPanel1.Name = "tableLayoutPanel1";
        tableLayoutPanel1.RowCount = 1;
        tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
        tableLayoutPanel1.Size = new System.Drawing.Size(800, 147);
        tableLayoutPanel1.TabIndex = 0;
        // 
        // groupBox1
        // 
        groupBox1.AutoSize = true;
        groupBox1.Controls.Add(flowLayoutPanel1);
        groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
        groupBox1.Location = new System.Drawing.Point(3, 3);
        groupBox1.Name = "groupBox1";
        groupBox1.Size = new System.Drawing.Size(194, 141);
        groupBox1.TabIndex = 0;
        groupBox1.TabStop = false;
        groupBox1.Text = "Инструменты";
        // 
        // flowLayoutPanel1
        // 
        flowLayoutPanel1.Controls.Add(btnFill);
        flowLayoutPanel1.Controls.Add(btnDelete);
        flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
        flowLayoutPanel1.Location = new System.Drawing.Point(3, 23);
        flowLayoutPanel1.Name = "flowLayoutPanel1";
        flowLayoutPanel1.Size = new System.Drawing.Size(188, 115);
        flowLayoutPanel1.TabIndex = 0;
        // 
        // btnFill
        // 
        btnFill.AutoSize = true;
        btnFill.Cursor = System.Windows.Forms.Cursors.Hand;
        btnFill.Location = new System.Drawing.Point(3, 3);
        btnFill.Name = "btnFill";
        btnFill.Size = new System.Drawing.Size(85, 30);
        btnFill.TabIndex = 1;
        btnFill.Text = "Заливка";
        btnFill.UseVisualStyleBackColor = true;
        btnFill.Click += ControlPanel_ToolButtonClick;
        // 
        // btnDelete
        // 
        btnDelete.AutoSize = true;
        btnDelete.Cursor = System.Windows.Forms.Cursors.Hand;
        btnDelete.Location = new System.Drawing.Point(94, 3);
        btnDelete.Name = "btnDelete";
        btnDelete.Size = new System.Drawing.Size(85, 30);
        btnDelete.TabIndex = 3;
        btnDelete.Text = "Удалить";
        btnDelete.UseVisualStyleBackColor = true;
        btnDelete.Click += ControlPanel_ToolButtonClick;
        // 
        // groupBox2
        // 
        groupBox2.Controls.Add(flowLayoutPanel2);
        groupBox2.Dock = System.Windows.Forms.DockStyle.Fill;
        groupBox2.Location = new System.Drawing.Point(203, 3);
        groupBox2.Name = "groupBox2";
        groupBox2.Size = new System.Drawing.Size(274, 141);
        groupBox2.TabIndex = 1;
        groupBox2.TabStop = false;
        groupBox2.Text = "Фигуры";
        // 
        // flowLayoutPanel2
        // 
        flowLayoutPanel2.AutoScroll = true;
        flowLayoutPanel2.Controls.Add(btnPolyline);
        flowLayoutPanel2.Controls.Add(btnBezierCurve);
        flowLayoutPanel2.Controls.Add(btnEllipse);
        flowLayoutPanel2.Controls.Add(btnRect);
        flowLayoutPanel2.Controls.Add(btnTriangle);
        flowLayoutPanel2.Controls.Add(btnPentagon);
        flowLayoutPanel2.Controls.Add(btnTrapezoid);
        flowLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
        flowLayoutPanel2.Location = new System.Drawing.Point(3, 23);
        flowLayoutPanel2.Name = "flowLayoutPanel2";
        flowLayoutPanel2.Size = new System.Drawing.Size(268, 115);
        flowLayoutPanel2.TabIndex = 0;
        // 
        // btnPolyline
        // 
        btnPolyline.AutoSize = true;
        btnPolyline.Location = new System.Drawing.Point(3, 3);
        btnPolyline.Name = "btnPolyline";
        btnPolyline.Size = new System.Drawing.Size(82, 30);
        btnPolyline.TabIndex = 6;
        btnPolyline.Text = "Ломаная";
        btnPolyline.UseVisualStyleBackColor = true;
        btnPolyline.Click += ControlPanel_ShapeButtonClick;
        // 
        // btnBezierCurve
        // 
        btnBezierCurve.AutoSize = true;
        btnBezierCurve.Location = new System.Drawing.Point(91, 3);
        btnBezierCurve.Name = "btnBezierCurve";
        btnBezierCurve.Size = new System.Drawing.Size(79, 30);
        btnBezierCurve.TabIndex = 5;
        btnBezierCurve.Text = "Кривая";
        btnBezierCurve.UseVisualStyleBackColor = true;
        btnBezierCurve.Click += ControlPanel_ShapeButtonClick;
        // 
        // btnEllipse
        // 
        btnEllipse.AutoSize = true;
        btnEllipse.Location = new System.Drawing.Point(176, 3);
        btnEllipse.Name = "btnEllipse";
        btnEllipse.Size = new System.Drawing.Size(75, 30);
        btnEllipse.TabIndex = 4;
        btnEllipse.Text = "Эллипс";
        btnEllipse.UseVisualStyleBackColor = true;
        btnEllipse.Click += ControlPanel_ShapeButtonClick;
        // 
        // btnRect
        // 
        btnRect.AutoSize = true;
        btnRect.Location = new System.Drawing.Point(3, 39);
        btnRect.Name = "btnRect";
        btnRect.Size = new System.Drawing.Size(130, 30);
        btnRect.TabIndex = 3;
        btnRect.Text = "Прямоугольник";
        btnRect.UseVisualStyleBackColor = true;
        btnRect.Click += ControlPanel_ShapeButtonClick;
        // 
        // btnTriangle
        // 
        btnTriangle.AutoSize = true;
        btnTriangle.Location = new System.Drawing.Point(139, 39);
        btnTriangle.Name = "btnTriangle";
        btnTriangle.Size = new System.Drawing.Size(107, 30);
        btnTriangle.TabIndex = 2;
        btnTriangle.Text = "Треугольник";
        btnTriangle.UseVisualStyleBackColor = true;
        btnTriangle.Click += ControlPanel_ShapeButtonClick;
        // 
        // btnPentagon
        // 
        btnPentagon.AutoSize = true;
        btnPentagon.Location = new System.Drawing.Point(3, 75);
        btnPentagon.Name = "btnPentagon";
        btnPentagon.Size = new System.Drawing.Size(116, 30);
        btnPentagon.TabIndex = 1;
        btnPentagon.Text = "Пятиугольник";
        btnPentagon.UseVisualStyleBackColor = true;
        btnPentagon.Click += ControlPanel_ShapeButtonClick;
        // 
        // btnTrapezoid
        // 
        btnTrapezoid.AutoSize = true;
        btnTrapezoid.Location = new System.Drawing.Point(125, 75);
        btnTrapezoid.Name = "btnTrapezoid";
        btnTrapezoid.Size = new System.Drawing.Size(87, 30);
        btnTrapezoid.TabIndex = 0;
        btnTrapezoid.Text = "Трапеция";
        btnTrapezoid.UseVisualStyleBackColor = true;
        btnTrapezoid.Click += ControlPanel_ShapeButtonClick;
        // 
        // groupBox3
        // 
        groupBox3.Controls.Add(tableLayoutPanel2);
        groupBox3.Dock = System.Windows.Forms.DockStyle.Fill;
        groupBox3.Location = new System.Drawing.Point(483, 3);
        groupBox3.Name = "groupBox3";
        groupBox3.Size = new System.Drawing.Size(314, 141);
        groupBox3.TabIndex = 2;
        groupBox3.TabStop = false;
        groupBox3.Text = "Цвета";
        // 
        // tableLayoutPanel2
        // 
        tableLayoutPanel2.ColumnCount = 2;
        tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 28.246754F));
        tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 71.75325F));
        tableLayoutPanel2.Controls.Add(flowLayoutPanel3, 0, 0);
        tableLayoutPanel2.Controls.Add(flowLayoutPanel4, 1, 0);
        tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
        tableLayoutPanel2.Location = new System.Drawing.Point(3, 23);
        tableLayoutPanel2.Name = "tableLayoutPanel2";
        tableLayoutPanel2.RowCount = 1;
        tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
        tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
        tableLayoutPanel2.Size = new System.Drawing.Size(308, 115);
        tableLayoutPanel2.TabIndex = 0;
        // 
        // flowLayoutPanel3
        // 
        flowLayoutPanel3.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
        flowLayoutPanel3.Controls.Add(pnlColorView);
        flowLayoutPanel3.Controls.Add(btnOther);
        flowLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Fill;
        flowLayoutPanel3.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
        flowLayoutPanel3.Location = new System.Drawing.Point(3, 3);
        flowLayoutPanel3.Name = "flowLayoutPanel3";
        flowLayoutPanel3.Size = new System.Drawing.Size(81, 109);
        flowLayoutPanel3.TabIndex = 0;
        flowLayoutPanel3.WrapContents = false;
        // 
        // pnlColorView
        // 
        pnlColorView.Anchor = System.Windows.Forms.AnchorStyles.Top;
        pnlColorView.BackColor = System.Drawing.Color.Black;
        pnlColorView.Location = new System.Drawing.Point(15, 3);
        pnlColorView.Name = "pnlColorView";
        pnlColorView.Size = new System.Drawing.Size(50, 50);
        pnlColorView.TabIndex = 0;
        // 
        // btnOther
        // 
        btnOther.AutoSize = true;
        btnOther.Location = new System.Drawing.Point(3, 59);
        btnOther.Name = "btnOther";
        btnOther.Size = new System.Drawing.Size(75, 30);
        btnOther.TabIndex = 1;
        btnOther.Text = "Другой";
        btnOther.UseVisualStyleBackColor = true;
        btnOther.Click += ControlPanel_ColorOtherButtonClick;
        // 
        // flowLayoutPanel4
        // 
        flowLayoutPanel4.Controls.Add(btnBlack);
        flowLayoutPanel4.Controls.Add(btnGrey);
        flowLayoutPanel4.Controls.Add(btnWhite);
        flowLayoutPanel4.Controls.Add(btnRed);
        flowLayoutPanel4.Controls.Add(btnOrange);
        flowLayoutPanel4.Controls.Add(btnYellow);
        flowLayoutPanel4.Controls.Add(btnGreen);
        flowLayoutPanel4.Controls.Add(btnLightBlue);
        flowLayoutPanel4.Controls.Add(btnBlue);
        flowLayoutPanel4.Controls.Add(btnPurple);
        flowLayoutPanel4.Dock = System.Windows.Forms.DockStyle.Fill;
        flowLayoutPanel4.Location = new System.Drawing.Point(90, 3);
        flowLayoutPanel4.Name = "flowLayoutPanel4";
        flowLayoutPanel4.Size = new System.Drawing.Size(215, 109);
        flowLayoutPanel4.TabIndex = 1;
        // 
        // btnBlack
        // 
        btnBlack.BackColor = System.Drawing.Color.Black;
        btnBlack.Location = new System.Drawing.Point(3, 3);
        btnBlack.Name = "btnBlack";
        btnBlack.Size = new System.Drawing.Size(30, 30);
        btnBlack.TabIndex = 9;
        btnBlack.UseVisualStyleBackColor = false;
        btnBlack.Click += ControlPanel_ColorButtonClick;
        // 
        // btnGrey
        // 
        btnGrey.BackColor = System.Drawing.Color.Gray;
        btnGrey.Location = new System.Drawing.Point(39, 3);
        btnGrey.Name = "btnGrey";
        btnGrey.Size = new System.Drawing.Size(30, 30);
        btnGrey.TabIndex = 8;
        btnGrey.UseVisualStyleBackColor = false;
        btnGrey.Click += ControlPanel_ColorButtonClick;
        // 
        // btnWhite
        // 
        btnWhite.BackColor = System.Drawing.Color.White;
        btnWhite.Location = new System.Drawing.Point(75, 3);
        btnWhite.Name = "btnWhite";
        btnWhite.Size = new System.Drawing.Size(30, 30);
        btnWhite.TabIndex = 7;
        btnWhite.UseVisualStyleBackColor = false;
        btnWhite.Click += ControlPanel_ColorButtonClick;
        // 
        // btnRed
        // 
        btnRed.BackColor = System.Drawing.Color.Red;
        btnRed.Location = new System.Drawing.Point(111, 3);
        btnRed.Name = "btnRed";
        btnRed.Size = new System.Drawing.Size(30, 30);
        btnRed.TabIndex = 6;
        btnRed.UseVisualStyleBackColor = false;
        btnRed.Click += ControlPanel_ColorButtonClick;
        // 
        // btnOrange
        // 
        btnOrange.BackColor = System.Drawing.Color.Orange;
        btnOrange.Location = new System.Drawing.Point(147, 3);
        btnOrange.Name = "btnOrange";
        btnOrange.Size = new System.Drawing.Size(30, 30);
        btnOrange.TabIndex = 5;
        btnOrange.UseVisualStyleBackColor = false;
        btnOrange.Click += ControlPanel_ColorButtonClick;
        // 
        // btnYellow
        // 
        btnYellow.BackColor = System.Drawing.Color.Yellow;
        btnYellow.Location = new System.Drawing.Point(3, 39);
        btnYellow.Name = "btnYellow";
        btnYellow.Size = new System.Drawing.Size(30, 30);
        btnYellow.TabIndex = 4;
        btnYellow.UseVisualStyleBackColor = false;
        btnYellow.Click += ControlPanel_ColorButtonClick;
        // 
        // btnGreen
        // 
        btnGreen.BackColor = System.Drawing.Color.Green;
        btnGreen.Location = new System.Drawing.Point(39, 39);
        btnGreen.Name = "btnGreen";
        btnGreen.Size = new System.Drawing.Size(30, 30);
        btnGreen.TabIndex = 3;
        btnGreen.UseVisualStyleBackColor = false;
        btnGreen.Click += ControlPanel_ColorButtonClick;
        // 
        // btnLightBlue
        // 
        btnLightBlue.BackColor = System.Drawing.Color.DeepSkyBlue;
        btnLightBlue.Location = new System.Drawing.Point(75, 39);
        btnLightBlue.Name = "btnLightBlue";
        btnLightBlue.Size = new System.Drawing.Size(30, 30);
        btnLightBlue.TabIndex = 2;
        btnLightBlue.UseVisualStyleBackColor = false;
        btnLightBlue.Click += ControlPanel_ColorButtonClick;
        // 
        // btnBlue
        // 
        btnBlue.BackColor = System.Drawing.Color.Blue;
        btnBlue.Location = new System.Drawing.Point(111, 39);
        btnBlue.Name = "btnBlue";
        btnBlue.Size = new System.Drawing.Size(30, 30);
        btnBlue.TabIndex = 1;
        btnBlue.UseVisualStyleBackColor = false;
        btnBlue.Click += ControlPanel_ColorButtonClick;
        // 
        // btnPurple
        // 
        btnPurple.BackColor = System.Drawing.Color.Purple;
        btnPurple.Location = new System.Drawing.Point(147, 39);
        btnPurple.Name = "btnPurple";
        btnPurple.Size = new System.Drawing.Size(30, 30);
        btnPurple.TabIndex = 0;
        btnPurple.UseVisualStyleBackColor = false;
        btnPurple.Click += ControlPanel_ColorButtonClick;
        // 
        // Form1
        // 
        AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
        AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        ClientSize = new System.Drawing.Size(800, 450);
        Controls.Add(SplitContainer1);
        Text = "Form1";
        SplitContainer1.Panel1.ResumeLayout(false);
        ((System.ComponentModel.ISupportInitialize)SplitContainer1).EndInit();
        SplitContainer1.ResumeLayout(false);
        tableLayoutPanel1.ResumeLayout(false);
        tableLayoutPanel1.PerformLayout();
        groupBox1.ResumeLayout(false);
        flowLayoutPanel1.ResumeLayout(false);
        flowLayoutPanel1.PerformLayout();
        groupBox2.ResumeLayout(false);
        flowLayoutPanel2.ResumeLayout(false);
        flowLayoutPanel2.PerformLayout();
        groupBox3.ResumeLayout(false);
        tableLayoutPanel2.ResumeLayout(false);
        flowLayoutPanel3.ResumeLayout(false);
        flowLayoutPanel3.PerformLayout();
        flowLayoutPanel4.ResumeLayout(false);
        ResumeLayout(false);
    }

    private System.Windows.Forms.Button btnBlack;

    private System.Windows.Forms.Button btnWhite;
    private System.Windows.Forms.Button btnGrey;

    private System.Windows.Forms.Button btnPurple;
    private System.Windows.Forms.Button btnBlue;
    private System.Windows.Forms.Button btnLightBlue;
    private System.Windows.Forms.Button btnGreen;
    private System.Windows.Forms.Button btnYellow;
    private System.Windows.Forms.Button btnOrange;
    private System.Windows.Forms.Button btnRed;

    private System.Windows.Forms.Panel pnlColorView;
    private System.Windows.Forms.Button btnOther;
    private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel4;

    private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel3;

    private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;

    private System.Windows.Forms.Button btnTrapezoid;
    private System.Windows.Forms.Button btnPentagon;
    private System.Windows.Forms.Button btnTriangle;
    private System.Windows.Forms.Button btnRect;
    private System.Windows.Forms.Button btnEllipse;
    private System.Windows.Forms.Button btnBezierCurve;
    private System.Windows.Forms.Button btnPolyline;

    private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel2;

    private System.Windows.Forms.Button btnFill;
    private System.Windows.Forms.Button btnDelete;

    private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;

    private System.Windows.Forms.GroupBox groupBox3;

    private System.Windows.Forms.GroupBox groupBox2;

    private System.Windows.Forms.GroupBox groupBox1;

    private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;

    private System.Windows.Forms.SplitContainer SplitContainer1;

    #endregion
}