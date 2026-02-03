namespace TimeStudiesApp.Dialogs;

partial class SettingsDialog
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
        _lblLanguage = new Label();
        _cboLanguage = new ComboBox();
        _lblBaseDir = new Label();
        _txtBaseDir = new TextBox();
        _btnBrowse = new Button();
        _lblButtonSize = new Label();
        _numButtonSize = new NumericUpDown();
        _chkAutoSave = new CheckBox();
        _chkConfirmClose = new CheckBox();
        _flowLayoutPanel = new FlowLayoutPanel();
        _btnOK = new Button();
        _btnCancel = new Button();

        _tableLayoutPanel.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)_numButtonSize).BeginInit();
        _flowLayoutPanel.SuspendLayout();
        SuspendLayout();

        // _tableLayoutPanel
        _tableLayoutPanel.ColumnCount = 3;
        _tableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.AutoSize));
        _tableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
        _tableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.AutoSize));
        _tableLayoutPanel.Controls.Add(_lblLanguage, 0, 0);
        _tableLayoutPanel.Controls.Add(_cboLanguage, 1, 0);
        _tableLayoutPanel.Controls.Add(_lblBaseDir, 0, 1);
        _tableLayoutPanel.Controls.Add(_txtBaseDir, 1, 1);
        _tableLayoutPanel.Controls.Add(_btnBrowse, 2, 1);
        _tableLayoutPanel.Controls.Add(_lblButtonSize, 0, 2);
        _tableLayoutPanel.Controls.Add(_numButtonSize, 1, 2);
        _tableLayoutPanel.Controls.Add(_chkAutoSave, 0, 3);
        _tableLayoutPanel.Controls.Add(_chkConfirmClose, 0, 4);
        _tableLayoutPanel.Controls.Add(_flowLayoutPanel, 0, 5);
        _tableLayoutPanel.Dock = DockStyle.Fill;
        _tableLayoutPanel.Location = new Point(0, 0);
        _tableLayoutPanel.Name = "_tableLayoutPanel";
        _tableLayoutPanel.Padding = new Padding(15);
        _tableLayoutPanel.RowCount = 6;
        _tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.AutoSize));
        _tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.AutoSize));
        _tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.AutoSize));
        _tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.AutoSize));
        _tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.AutoSize));
        _tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
        _tableLayoutPanel.Size = new Size(450, 280);
        _tableLayoutPanel.TabIndex = 0;

        _tableLayoutPanel.SetColumnSpan(_chkAutoSave, 3);
        _tableLayoutPanel.SetColumnSpan(_chkConfirmClose, 3);
        _tableLayoutPanel.SetColumnSpan(_flowLayoutPanel, 3);

        // _lblLanguage
        _lblLanguage.Anchor = AnchorStyles.Left;
        _lblLanguage.AutoSize = true;
        _lblLanguage.Location = new Point(18, 22);
        _lblLanguage.Name = "_lblLanguage";
        _lblLanguage.Size = new Size(62, 15);
        _lblLanguage.TabIndex = 0;
        _lblLanguage.Text = "Language:";

        // _cboLanguage
        _cboLanguage.Anchor = AnchorStyles.Left | AnchorStyles.Right;
        _cboLanguage.DropDownStyle = ComboBoxStyle.DropDownList;
        _cboLanguage.FormattingEnabled = true;
        _cboLanguage.Location = new Point(120, 18);
        _cboLanguage.Name = "_cboLanguage";
        _cboLanguage.Size = new Size(200, 23);
        _cboLanguage.TabIndex = 1;

        // _lblBaseDir
        _lblBaseDir.Anchor = AnchorStyles.Left;
        _lblBaseDir.AutoSize = true;
        _lblBaseDir.Location = new Point(18, 52);
        _lblBaseDir.Name = "_lblBaseDir";
        _lblBaseDir.Size = new Size(88, 15);
        _lblBaseDir.TabIndex = 2;
        _lblBaseDir.Text = "Base Directory:";

        // _txtBaseDir
        _txtBaseDir.Anchor = AnchorStyles.Left | AnchorStyles.Right;
        _txtBaseDir.Location = new Point(120, 48);
        _txtBaseDir.Name = "_txtBaseDir";
        _txtBaseDir.Size = new Size(200, 23);
        _txtBaseDir.TabIndex = 3;

        // _btnBrowse
        _btnBrowse.Location = new Point(326, 47);
        _btnBrowse.Name = "_btnBrowse";
        _btnBrowse.Size = new Size(90, 25);
        _btnBrowse.TabIndex = 4;
        _btnBrowse.Text = "Browse...";
        _btnBrowse.UseVisualStyleBackColor = true;

        // _lblButtonSize
        _lblButtonSize.Anchor = AnchorStyles.Left;
        _lblButtonSize.AutoSize = true;
        _lblButtonSize.Location = new Point(18, 82);
        _lblButtonSize.Name = "_lblButtonSize";
        _lblButtonSize.Size = new Size(68, 15);
        _lblButtonSize.TabIndex = 5;
        _lblButtonSize.Text = "Button Size:";

        // _numButtonSize
        _numButtonSize.Anchor = AnchorStyles.Left;
        _numButtonSize.Location = new Point(120, 78);
        _numButtonSize.Maximum = new decimal(new int[] { 200, 0, 0, 0 });
        _numButtonSize.Minimum = new decimal(new int[] { 48, 0, 0, 0 });
        _numButtonSize.Name = "_numButtonSize";
        _numButtonSize.Size = new Size(80, 23);
        _numButtonSize.TabIndex = 6;
        _numButtonSize.Value = new decimal(new int[] { 60, 0, 0, 0 });

        // _chkAutoSave
        _chkAutoSave.AutoSize = true;
        _chkAutoSave.Location = new Point(18, 109);
        _chkAutoSave.Name = "_chkAutoSave";
        _chkAutoSave.Size = new Size(137, 19);
        _chkAutoSave.TabIndex = 7;
        _chkAutoSave.Text = "Auto-save recordings";
        _chkAutoSave.UseVisualStyleBackColor = true;

        // _chkConfirmClose
        _chkConfirmClose.AutoSize = true;
        _chkConfirmClose.Location = new Point(18, 134);
        _chkConfirmClose.Name = "_chkConfirmClose";
        _chkConfirmClose.Size = new Size(204, 19);
        _chkConfirmClose.TabIndex = 8;
        _chkConfirmClose.Text = "Confirm before closing recording";
        _chkConfirmClose.UseVisualStyleBackColor = true;

        // _flowLayoutPanel
        _flowLayoutPanel.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
        _flowLayoutPanel.AutoSize = true;
        _flowLayoutPanel.Controls.Add(_btnCancel);
        _flowLayoutPanel.Controls.Add(_btnOK);
        _flowLayoutPanel.FlowDirection = FlowDirection.RightToLeft;
        _flowLayoutPanel.Location = new Point(218, 230);
        _flowLayoutPanel.Name = "_flowLayoutPanel";
        _flowLayoutPanel.Size = new Size(214, 32);
        _flowLayoutPanel.TabIndex = 9;

        // _btnOK
        _btnOK.Location = new Point(109, 3);
        _btnOK.MinimumSize = new Size(100, 26);
        _btnOK.Name = "_btnOK";
        _btnOK.Size = new Size(100, 26);
        _btnOK.TabIndex = 0;
        _btnOK.Text = "OK";
        _btnOK.UseVisualStyleBackColor = true;

        // _btnCancel
        _btnCancel.Location = new Point(3, 3);
        _btnCancel.MinimumSize = new Size(100, 26);
        _btnCancel.Name = "_btnCancel";
        _btnCancel.Size = new Size(100, 26);
        _btnCancel.TabIndex = 1;
        _btnCancel.Text = "Cancel";
        _btnCancel.UseVisualStyleBackColor = true;

        // SettingsDialog
        AcceptButton = _btnOK;
        AutoScaleDimensions = new SizeF(7F, 15F);
        AutoScaleMode = AutoScaleMode.Font;
        CancelButton = _btnCancel;
        ClientSize = new Size(450, 280);
        Controls.Add(_tableLayoutPanel);
        FormBorderStyle = FormBorderStyle.FixedDialog;
        MaximizeBox = false;
        MinimizeBox = false;
        Name = "SettingsDialog";
        ShowIcon = false;
        ShowInTaskbar = false;
        StartPosition = FormStartPosition.CenterParent;
        Text = "Settings";

        _tableLayoutPanel.ResumeLayout(false);
        _tableLayoutPanel.PerformLayout();
        ((System.ComponentModel.ISupportInitialize)_numButtonSize).EndInit();
        _flowLayoutPanel.ResumeLayout(false);
        ResumeLayout(false);
    }

    private TableLayoutPanel _tableLayoutPanel;
    private Label _lblLanguage;
    private ComboBox _cboLanguage;
    private Label _lblBaseDir;
    private TextBox _txtBaseDir;
    private Button _btnBrowse;
    private Label _lblButtonSize;
    private NumericUpDown _numButtonSize;
    private CheckBox _chkAutoSave;
    private CheckBox _chkConfirmClose;
    private FlowLayoutPanel _flowLayoutPanel;
    private Button _btnOK;
    private Button _btnCancel;
}
