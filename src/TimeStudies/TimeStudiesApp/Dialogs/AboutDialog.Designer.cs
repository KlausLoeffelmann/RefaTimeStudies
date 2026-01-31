namespace TimeStudiesApp.Dialogs;

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
        _pnlContent = new TableLayoutPanel();
        _lblAppName = new Label();
        _lblVersion = new Label();
        _lblCopyright = new Label();
        _lblDescription = new Label();
        _btnOK = new Button();
        _pnlContent.SuspendLayout();
        SuspendLayout();
        // 
        // _pnlContent
        // 
        _pnlContent.ColumnCount = 1;
        _pnlContent.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
        _pnlContent.Controls.Add(_lblAppName, 0, 0);
        _pnlContent.Controls.Add(_lblVersion, 0, 1);
        _pnlContent.Controls.Add(_lblCopyright, 0, 2);
        _pnlContent.Controls.Add(_lblDescription, 0, 3);
        _pnlContent.Controls.Add(_btnOK, 0, 4);
        _pnlContent.Dock = DockStyle.Fill;
        _pnlContent.Location = new Point(0, 0);
        _pnlContent.Name = "_pnlContent";
        _pnlContent.Padding = new Padding(30, 20, 30, 20);
        _pnlContent.RowCount = 5;
        _pnlContent.RowStyles.Add(new RowStyle(SizeType.AutoSize));
        _pnlContent.RowStyles.Add(new RowStyle(SizeType.AutoSize));
        _pnlContent.RowStyles.Add(new RowStyle(SizeType.AutoSize));
        _pnlContent.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
        _pnlContent.RowStyles.Add(new RowStyle(SizeType.AutoSize));
        _pnlContent.Size = new Size(400, 280);
        _pnlContent.TabIndex = 0;
        // 
        // _lblAppName
        // 
        _lblAppName.Anchor = AnchorStyles.Top;
        _lblAppName.AutoSize = true;
        _lblAppName.Font = new Font("Segoe UI", 18F, FontStyle.Bold);
        _lblAppName.ForeColor = Color.FromArgb(0, 120, 215);
        _lblAppName.Location = new Point(93, 20);
        _lblAppName.Margin = new Padding(3, 0, 3, 10);
        _lblAppName.Name = "_lblAppName";
        _lblAppName.Size = new Size(214, 41);
        _lblAppName.TabIndex = 0;
        _lblAppName.Text = "REFA Time Study";
        _lblAppName.TextAlign = ContentAlignment.MiddleCenter;
        // 
        // _lblVersion
        // 
        _lblVersion.Anchor = AnchorStyles.Top;
        _lblVersion.AutoSize = true;
        _lblVersion.Font = new Font("Segoe UI", 10F);
        _lblVersion.Location = new Point(147, 71);
        _lblVersion.Margin = new Padding(3, 0, 3, 5);
        _lblVersion.Name = "_lblVersion";
        _lblVersion.Size = new Size(106, 23);
        _lblVersion.TabIndex = 1;
        _lblVersion.Text = "Version 1.0.0";
        _lblVersion.TextAlign = ContentAlignment.MiddleCenter;
        // 
        // _lblCopyright
        // 
        _lblCopyright.Anchor = AnchorStyles.Top;
        _lblCopyright.AutoSize = true;
        _lblCopyright.Font = new Font("Segoe UI", 9F);
        _lblCopyright.ForeColor = SystemColors.GrayText;
        _lblCopyright.Location = new Point(108, 99);
        _lblCopyright.Margin = new Padding(3, 0, 3, 15);
        _lblCopyright.Name = "_lblCopyright";
        _lblCopyright.Size = new Size(183, 20);
        _lblCopyright.TabIndex = 2;
        _lblCopyright.Text = "© 2026 REFA Time Study";
        _lblCopyright.TextAlign = ContentAlignment.MiddleCenter;
        // 
        // _lblDescription
        // 
        _lblDescription.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        _lblDescription.Location = new Point(33, 134);
        _lblDescription.Name = "_lblDescription";
        _lblDescription.Size = new Size(334, 80);
        _lblDescription.TabIndex = 3;
        _lblDescription.Text = "Industrial time study application for REFA-style measurements.";
        _lblDescription.TextAlign = ContentAlignment.TopCenter;
        // 
        // _btnOK
        // 
        _btnOK.Anchor = AnchorStyles.Bottom;
        _btnOK.DialogResult = DialogResult.OK;
        _btnOK.Location = new Point(150, 232);
        _btnOK.MinimumSize = new Size(100, 28);
        _btnOK.Name = "_btnOK";
        _btnOK.Size = new Size(100, 28);
        _btnOK.TabIndex = 4;
        _btnOK.Text = "OK";
        _btnOK.UseVisualStyleBackColor = true;
        _btnOK.Click += BtnOK_Click;
        // 
        // AboutDialog
        // 
        AcceptButton = _btnOK;
        AutoScaleDimensions = new SizeF(8F, 20F);
        AutoScaleMode = AutoScaleMode.Font;
        ClientSize = new Size(400, 280);
        Controls.Add(_pnlContent);
        FormBorderStyle = FormBorderStyle.FixedDialog;
        MaximizeBox = false;
        MinimizeBox = false;
        Name = "AboutDialog";
        ShowInTaskbar = false;
        StartPosition = FormStartPosition.CenterParent;
        Text = "About";
        _pnlContent.ResumeLayout(false);
        _pnlContent.PerformLayout();
        ResumeLayout(false);
    }

    #endregion

    private TableLayoutPanel _pnlContent;
    private Label _lblAppName;
    private Label _lblVersion;
    private Label _lblCopyright;
    private Label _lblDescription;
    private Button _btnOK;
}
