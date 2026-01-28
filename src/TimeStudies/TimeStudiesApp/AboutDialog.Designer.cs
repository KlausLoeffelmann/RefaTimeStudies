namespace TimeStudiesApp;

partial class AboutDialog
{
    private System.ComponentModel.IContainer components = null;

    protected override void Dispose(bool disposing)
    {
        if (disposing && (components != null))
        {
            components.Dispose();
        }
        base.Dispose(disposing);
    }

    #region Windows Form Designer generated code

    private void InitializeComponent()
    {
        components = new System.ComponentModel.Container();
        _tableLayoutMain = new TableLayoutPanel();
        _picIcon = new PictureBox();
        _lblAppName = new Label();
        _lblVersion = new Label();
        _lblDescription = new Label();
        _btnOK = new Button();

        ((System.ComponentModel.ISupportInitialize)_picIcon).BeginInit();
        _tableLayoutMain.SuspendLayout();
        SuspendLayout();

        // _tableLayoutMain
        _tableLayoutMain.ColumnCount = 2;
        _tableLayoutMain.ColumnStyles.Add(new ColumnStyle(SizeType.AutoSize));
        _tableLayoutMain.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
        _tableLayoutMain.Controls.Add(_picIcon, 0, 0);
        _tableLayoutMain.Controls.Add(_lblAppName, 1, 0);
        _tableLayoutMain.Controls.Add(_lblVersion, 1, 1);
        _tableLayoutMain.Controls.Add(_lblDescription, 0, 2);
        _tableLayoutMain.Controls.Add(_btnOK, 1, 3);
        _tableLayoutMain.Dock = DockStyle.Fill;
        _tableLayoutMain.Location = new Point(0, 0);
        _tableLayoutMain.Name = "_tableLayoutMain";
        _tableLayoutMain.Padding = new Padding(20);
        _tableLayoutMain.RowCount = 4;
        _tableLayoutMain.RowStyles.Add(new RowStyle(SizeType.AutoSize));
        _tableLayoutMain.RowStyles.Add(new RowStyle(SizeType.AutoSize));
        _tableLayoutMain.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
        _tableLayoutMain.RowStyles.Add(new RowStyle(SizeType.AutoSize));
        _tableLayoutMain.Size = new Size(450, 240);
        _tableLayoutMain.TabIndex = 0;

        // _picIcon
        _picIcon.Location = new Point(23, 23);
        _picIcon.Name = "_picIcon";
        _tableLayoutMain.SetRowSpan(_picIcon, 2);
        _picIcon.Size = new Size(64, 64);
        _picIcon.SizeMode = PictureBoxSizeMode.Zoom;
        _picIcon.TabIndex = 0;
        _picIcon.TabStop = false;

        // _lblAppName
        _lblAppName.Anchor = AnchorStyles.Left;
        _lblAppName.AutoSize = true;
        _lblAppName.Font = new Font("Segoe UI", 14F, FontStyle.Bold, GraphicsUnit.Point);
        _lblAppName.Location = new Point(93, 23);
        _lblAppName.Name = "_lblAppName";
        _lblAppName.Size = new Size(180, 32);
        _lblAppName.TabIndex = 1;
        _lblAppName.Text = "REFA Time Study";

        // _lblVersion
        _lblVersion.Anchor = AnchorStyles.Left;
        _lblVersion.AutoSize = true;
        _lblVersion.Location = new Point(93, 58);
        _lblVersion.Name = "_lblVersion";
        _lblVersion.Size = new Size(95, 20);
        _lblVersion.TabIndex = 2;
        _lblVersion.Text = "Version: 1.0.0";

        // _lblDescription
        _lblDescription.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        _lblDescription.AutoSize = true;
        _tableLayoutMain.SetColumnSpan(_lblDescription, 2);
        _lblDescription.Location = new Point(23, 90);
        _lblDescription.Margin = new Padding(3, 20, 3, 0);
        _lblDescription.MaximumSize = new Size(400, 0);
        _lblDescription.Name = "_lblDescription";
        _lblDescription.Size = new Size(400, 40);
        _lblDescription.TabIndex = 3;
        _lblDescription.Text = "A Windows Forms application for conducting REFA-style industrial time studies.";

        // _btnOK
        _btnOK.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
        _btnOK.DialogResult = DialogResult.OK;
        _btnOK.Location = new Point(327, 190);
        _btnOK.MinimumSize = new Size(100, 33);
        _btnOK.Name = "_btnOK";
        _btnOK.Size = new Size(100, 33);
        _btnOK.TabIndex = 4;
        _btnOK.Text = "OK";
        _btnOK.UseVisualStyleBackColor = true;

        // AboutDialog
        AcceptButton = _btnOK;
        AutoScaleDimensions = new SizeF(8F, 20F);
        AutoScaleMode = AutoScaleMode.Font;
        CancelButton = _btnOK;
        ClientSize = new Size(450, 240);
        Controls.Add(_tableLayoutMain);
        FormBorderStyle = FormBorderStyle.FixedDialog;
        MaximizeBox = false;
        MinimizeBox = false;
        Name = "AboutDialog";
        ShowInTaskbar = false;
        StartPosition = FormStartPosition.CenterParent;
        Text = "About";
        ((System.ComponentModel.ISupportInitialize)_picIcon).EndInit();
        _tableLayoutMain.ResumeLayout(false);
        _tableLayoutMain.PerformLayout();
        ResumeLayout(false);
    }

    #endregion

    private TableLayoutPanel _tableLayoutMain;
    private PictureBox _picIcon;
    private Label _lblAppName;
    private Label _lblVersion;
    private Label _lblDescription;
    private Button _btnOK;
}
