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

    private void InitializeComponent()
    {
        components = new System.ComponentModel.Container();

        _tableLayoutPanel = new TableLayoutPanel();
        _lblAppName = new Label();
        _lblVersion = new Label();
        _lblDescription = new Label();
        _lblCopyright = new Label();
        _btnOK = new Button();

        _tableLayoutPanel.SuspendLayout();
        SuspendLayout();

        // _tableLayoutPanel
        _tableLayoutPanel.ColumnCount = 1;
        _tableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
        _tableLayoutPanel.Controls.Add(_lblAppName, 0, 0);
        _tableLayoutPanel.Controls.Add(_lblVersion, 0, 1);
        _tableLayoutPanel.Controls.Add(_lblDescription, 0, 2);
        _tableLayoutPanel.Controls.Add(_lblCopyright, 0, 3);
        _tableLayoutPanel.Controls.Add(_btnOK, 0, 4);
        _tableLayoutPanel.Dock = DockStyle.Fill;
        _tableLayoutPanel.Location = new Point(0, 0);
        _tableLayoutPanel.Name = "_tableLayoutPanel";
        _tableLayoutPanel.Padding = new Padding(20);
        _tableLayoutPanel.RowCount = 5;
        _tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.AutoSize));
        _tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.AutoSize));
        _tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.AutoSize));
        _tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
        _tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.AutoSize));
        _tableLayoutPanel.Size = new Size(350, 220);
        _tableLayoutPanel.TabIndex = 0;

        // _lblAppName
        _lblAppName.AutoSize = true;
        _lblAppName.Dock = DockStyle.Fill;
        _lblAppName.Font = new Font("Segoe UI", 16F, FontStyle.Bold, GraphicsUnit.Point);
        _lblAppName.Location = new Point(23, 20);
        _lblAppName.Name = "_lblAppName";
        _lblAppName.Padding = new Padding(0, 0, 0, 5);
        _lblAppName.Size = new Size(304, 35);
        _lblAppName.TabIndex = 0;
        _lblAppName.Text = "REFA Time Study";
        _lblAppName.TextAlign = ContentAlignment.MiddleCenter;

        // _lblVersion
        _lblVersion.AutoSize = true;
        _lblVersion.Dock = DockStyle.Fill;
        _lblVersion.Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point);
        _lblVersion.Location = new Point(23, 55);
        _lblVersion.Name = "_lblVersion";
        _lblVersion.Padding = new Padding(0, 0, 0, 10);
        _lblVersion.Size = new Size(304, 29);
        _lblVersion.TabIndex = 1;
        _lblVersion.Text = "Version 1.0.0";
        _lblVersion.TextAlign = ContentAlignment.MiddleCenter;

        // _lblDescription
        _lblDescription.AutoSize = true;
        _lblDescription.Dock = DockStyle.Fill;
        _lblDescription.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point);
        _lblDescription.Location = new Point(23, 84);
        _lblDescription.Name = "_lblDescription";
        _lblDescription.Padding = new Padding(0, 0, 0, 10);
        _lblDescription.Size = new Size(304, 40);
        _lblDescription.TabIndex = 2;
        _lblDescription.Text = "Industrial time study application following REFA standards.";
        _lblDescription.TextAlign = ContentAlignment.TopCenter;

        // _lblCopyright
        _lblCopyright.AutoSize = true;
        _lblCopyright.Dock = DockStyle.Fill;
        _lblCopyright.Font = new Font("Segoe UI", 8F, FontStyle.Regular, GraphicsUnit.Point);
        _lblCopyright.ForeColor = SystemColors.GrayText;
        _lblCopyright.Location = new Point(23, 124);
        _lblCopyright.Name = "_lblCopyright";
        _lblCopyright.Size = new Size(304, 43);
        _lblCopyright.TabIndex = 3;
        _lblCopyright.Text = "© 2025";
        _lblCopyright.TextAlign = ContentAlignment.BottomCenter;

        // _btnOK
        _btnOK.Anchor = AnchorStyles.None;
        _btnOK.Location = new Point(125, 174);
        _btnOK.MinimumSize = new Size(100, 26);
        _btnOK.Name = "_btnOK";
        _btnOK.Size = new Size(100, 26);
        _btnOK.TabIndex = 4;
        _btnOK.Text = "OK";
        _btnOK.UseVisualStyleBackColor = true;

        // AboutDialog
        AcceptButton = _btnOK;
        AutoScaleDimensions = new SizeF(7F, 15F);
        AutoScaleMode = AutoScaleMode.Font;
        ClientSize = new Size(350, 220);
        Controls.Add(_tableLayoutPanel);
        FormBorderStyle = FormBorderStyle.FixedDialog;
        MaximizeBox = false;
        MinimizeBox = false;
        Name = "AboutDialog";
        ShowIcon = false;
        ShowInTaskbar = false;
        StartPosition = FormStartPosition.CenterParent;
        Text = "About";

        _tableLayoutPanel.ResumeLayout(false);
        _tableLayoutPanel.PerformLayout();
        ResumeLayout(false);
    }

    private TableLayoutPanel _tableLayoutPanel;
    private Label _lblAppName;
    private Label _lblVersion;
    private Label _lblDescription;
    private Label _lblCopyright;
    private Button _btnOK;
}
