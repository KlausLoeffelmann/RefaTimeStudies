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

        _tlpMain = new TableLayoutPanel();
        _picIcon = new PictureBox();
        _lblAppName = new Label();
        _lblVersion = new Label();
        _lblDescription = new Label();
        _lblCopyright = new Label();
        _btnOK = new Button();

        ((System.ComponentModel.ISupportInitialize)_picIcon).BeginInit();
        _tlpMain.SuspendLayout();
        SuspendLayout();

        // _tlpMain
        _tlpMain.ColumnCount = 2;
        _tlpMain.ColumnStyles.Add(new ColumnStyle(SizeType.AutoSize));
        _tlpMain.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
        _tlpMain.Controls.Add(_picIcon, 0, 0);
        _tlpMain.Controls.Add(_lblAppName, 1, 0);
        _tlpMain.Controls.Add(_lblVersion, 1, 1);
        _tlpMain.Controls.Add(_lblDescription, 0, 2);
        _tlpMain.Controls.Add(_lblCopyright, 0, 3);
        _tlpMain.Controls.Add(_btnOK, 0, 4);
        _tlpMain.Dock = DockStyle.Fill;
        _tlpMain.Location = new Point(16, 16);
        _tlpMain.Name = "_tlpMain";
        _tlpMain.RowCount = 5;
        _tlpMain.RowStyles.Add(new RowStyle(SizeType.AutoSize));
        _tlpMain.RowStyles.Add(new RowStyle(SizeType.AutoSize));
        _tlpMain.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
        _tlpMain.RowStyles.Add(new RowStyle(SizeType.AutoSize));
        _tlpMain.RowStyles.Add(new RowStyle(SizeType.AutoSize));
        _tlpMain.Size = new Size(368, 218);
        _tlpMain.TabIndex = 0;
        _tlpMain.SetRowSpan(_picIcon, 2);
        _tlpMain.SetColumnSpan(_lblDescription, 2);
        _tlpMain.SetColumnSpan(_lblCopyright, 2);
        _tlpMain.SetColumnSpan(_btnOK, 2);

        // _picIcon
        _picIcon.Location = new Point(3, 3);
        _picIcon.Margin = new Padding(3);
        _picIcon.Name = "_picIcon";
        _picIcon.Size = new Size(64, 64);
        _picIcon.SizeMode = PictureBoxSizeMode.CenterImage;
        _picIcon.TabIndex = 0;
        _picIcon.TabStop = false;

        // _lblAppName
        _lblAppName.Anchor = AnchorStyles.Left;
        _lblAppName.AutoSize = true;
        _lblAppName.Font = new Font("Segoe UI", 14F, FontStyle.Bold, GraphicsUnit.Point);
        _lblAppName.Location = new Point(73, 3);
        _lblAppName.Margin = new Padding(3);
        _lblAppName.Name = "_lblAppName";
        _lblAppName.Size = new Size(180, 32);
        _lblAppName.TabIndex = 1;
        _lblAppName.Text = "REFA Time Study";

        // _lblVersion
        _lblVersion.Anchor = AnchorStyles.Left;
        _lblVersion.AutoSize = true;
        _lblVersion.Location = new Point(73, 41);
        _lblVersion.Margin = new Padding(3);
        _lblVersion.Name = "_lblVersion";
        _lblVersion.Size = new Size(100, 20);
        _lblVersion.TabIndex = 2;
        _lblVersion.Text = "Version: 1.0.0";

        // _lblDescription
        _lblDescription.AutoSize = true;
        _lblDescription.Location = new Point(3, 70);
        _lblDescription.Margin = new Padding(3, 0, 3, 3);
        _lblDescription.Name = "_lblDescription";
        _lblDescription.Padding = new Padding(0, 12, 0, 12);
        _lblDescription.Size = new Size(350, 44);
        _lblDescription.TabIndex = 3;
        _lblDescription.Text = "A Windows application for conducting REFA-style industrial time studies.";

        // _lblCopyright
        _lblCopyright.AutoSize = true;
        _lblCopyright.ForeColor = SystemColors.GrayText;
        _lblCopyright.Location = new Point(3, 144);
        _lblCopyright.Margin = new Padding(3, 0, 3, 12);
        _lblCopyright.Name = "_lblCopyright";
        _lblCopyright.Size = new Size(250, 20);
        _lblCopyright.TabIndex = 4;
        _lblCopyright.Text = "© 2025 REFA Time Study Application";

        // _btnOK
        _btnOK.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
        _btnOK.Location = new Point(285, 179);
        _btnOK.Margin = new Padding(3);
        _btnOK.MinimumSize = new Size(80, 36);
        _btnOK.Name = "_btnOK";
        _btnOK.Size = new Size(80, 36);
        _btnOK.TabIndex = 5;
        _btnOK.Text = "OK";
        _btnOK.UseVisualStyleBackColor = true;
        _btnOK.Click += BtnOK_Click;

        // AboutDialog
        AcceptButton = _btnOK;
        AutoScaleDimensions = new SizeF(8F, 20F);
        AutoScaleMode = AutoScaleMode.Font;
        ClientSize = new Size(400, 250);
        Controls.Add(_tlpMain);
        FormBorderStyle = FormBorderStyle.FixedDialog;
        MaximizeBox = false;
        MinimizeBox = false;
        Name = "AboutDialog";
        Padding = new Padding(16);
        ShowIcon = false;
        ShowInTaskbar = false;
        StartPosition = FormStartPosition.CenterParent;
        Text = "About";

        ((System.ComponentModel.ISupportInitialize)_picIcon).EndInit();
        _tlpMain.ResumeLayout(false);
        _tlpMain.PerformLayout();
        ResumeLayout(false);
    }

    #endregion

    private TableLayoutPanel _tlpMain;
    private PictureBox _picIcon;
    private Label _lblAppName;
    private Label _lblVersion;
    private Label _lblDescription;
    private Label _lblCopyright;
    private Button _btnOK;
}
