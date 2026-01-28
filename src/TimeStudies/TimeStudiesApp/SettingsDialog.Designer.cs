namespace TimeStudiesApp;

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

    #region Windows Form Designer generated code

    private void InitializeComponent()
    {
        components = new System.ComponentModel.Container();
        _tableLayoutMain = new TableLayoutPanel();
        _tabControl = new TabControl();
        _tabGeneral = new TabPage();
        _tableLayoutGeneral = new TableLayoutPanel();
        _lblLanguage = new Label();
        _cboLanguage = new ComboBox();
        _lblBaseDir = new Label();
        _txtBaseDir = new TextBox();
        _btnBrowse = new Button();
        _lblButtonSize = new Label();
        _numButtonSize = new NumericUpDown();
        _tabRecording = new TabPage();
        _tableLayoutRecording = new TableLayoutPanel();
        _chkAutoSave = new CheckBox();
        _chkConfirmClose = new CheckBox();
        _tabExport = new TabPage();
        _tableLayoutExport = new TableLayoutPanel();
        _lblCsvDelimiter = new Label();
        _cboCsvDelimiter = new ComboBox();
        _flowLayoutButtons = new FlowLayoutPanel();
        _btnOK = new Button();
        _btnCancel = new Button();

        _tableLayoutMain.SuspendLayout();
        _tabControl.SuspendLayout();
        _tabGeneral.SuspendLayout();
        _tableLayoutGeneral.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)_numButtonSize).BeginInit();
        _tabRecording.SuspendLayout();
        _tableLayoutRecording.SuspendLayout();
        _tabExport.SuspendLayout();
        _tableLayoutExport.SuspendLayout();
        _flowLayoutButtons.SuspendLayout();
        SuspendLayout();

        // _tableLayoutMain
        _tableLayoutMain.ColumnCount = 1;
        _tableLayoutMain.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
        _tableLayoutMain.Controls.Add(_tabControl, 0, 0);
        _tableLayoutMain.Controls.Add(_flowLayoutButtons, 0, 1);
        _tableLayoutMain.Dock = DockStyle.Fill;
        _tableLayoutMain.Location = new Point(0, 0);
        _tableLayoutMain.Name = "_tableLayoutMain";
        _tableLayoutMain.Padding = new Padding(12);
        _tableLayoutMain.RowCount = 2;
        _tableLayoutMain.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
        _tableLayoutMain.RowStyles.Add(new RowStyle(SizeType.AutoSize));
        _tableLayoutMain.Size = new Size(484, 361);
        _tableLayoutMain.TabIndex = 0;

        // _tabControl
        _tabControl.Controls.Add(_tabGeneral);
        _tabControl.Controls.Add(_tabRecording);
        _tabControl.Controls.Add(_tabExport);
        _tabControl.Dock = DockStyle.Fill;
        _tabControl.Location = new Point(15, 15);
        _tabControl.Name = "_tabControl";
        _tabControl.SelectedIndex = 0;
        _tabControl.Size = new Size(454, 286);
        _tabControl.TabIndex = 0;

        // _tabGeneral
        _tabGeneral.Controls.Add(_tableLayoutGeneral);
        _tabGeneral.Location = new Point(4, 29);
        _tabGeneral.Name = "_tabGeneral";
        _tabGeneral.Padding = new Padding(12);
        _tabGeneral.Size = new Size(446, 253);
        _tabGeneral.TabIndex = 0;
        _tabGeneral.Text = "General";
        _tabGeneral.UseVisualStyleBackColor = true;

        // _tableLayoutGeneral
        _tableLayoutGeneral.ColumnCount = 3;
        _tableLayoutGeneral.ColumnStyles.Add(new ColumnStyle(SizeType.AutoSize));
        _tableLayoutGeneral.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
        _tableLayoutGeneral.ColumnStyles.Add(new ColumnStyle(SizeType.AutoSize));
        _tableLayoutGeneral.Controls.Add(_lblLanguage, 0, 0);
        _tableLayoutGeneral.Controls.Add(_cboLanguage, 1, 0);
        _tableLayoutGeneral.Controls.Add(_lblBaseDir, 0, 1);
        _tableLayoutGeneral.Controls.Add(_txtBaseDir, 1, 1);
        _tableLayoutGeneral.Controls.Add(_btnBrowse, 2, 1);
        _tableLayoutGeneral.Controls.Add(_lblButtonSize, 0, 2);
        _tableLayoutGeneral.Controls.Add(_numButtonSize, 1, 2);
        _tableLayoutGeneral.Dock = DockStyle.Fill;
        _tableLayoutGeneral.Location = new Point(12, 12);
        _tableLayoutGeneral.Name = "_tableLayoutGeneral";
        _tableLayoutGeneral.RowCount = 4;
        _tableLayoutGeneral.RowStyles.Add(new RowStyle(SizeType.AutoSize));
        _tableLayoutGeneral.RowStyles.Add(new RowStyle(SizeType.AutoSize));
        _tableLayoutGeneral.RowStyles.Add(new RowStyle(SizeType.AutoSize));
        _tableLayoutGeneral.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
        _tableLayoutGeneral.Size = new Size(422, 229);
        _tableLayoutGeneral.TabIndex = 0;

        // _lblLanguage
        _lblLanguage.Anchor = AnchorStyles.Left | AnchorStyles.Right;
        _lblLanguage.AutoSize = true;
        _lblLanguage.Location = new Point(3, 7);
        _lblLanguage.Margin = new Padding(3);
        _lblLanguage.Name = "_lblLanguage";
        _lblLanguage.Size = new Size(75, 20);
        _lblLanguage.TabIndex = 0;
        _lblLanguage.Text = "Language:";

        // _cboLanguage
        _cboLanguage.Anchor = AnchorStyles.Left | AnchorStyles.Right;
        _cboLanguage.DropDownStyle = ComboBoxStyle.DropDownList;
        _cboLanguage.FormattingEnabled = true;
        _cboLanguage.Location = new Point(84, 3);
        _cboLanguage.Name = "_cboLanguage";
        _cboLanguage.Size = new Size(235, 28);
        _cboLanguage.TabIndex = 1;

        // _lblBaseDir
        _lblBaseDir.Anchor = AnchorStyles.Left | AnchorStyles.Right;
        _lblBaseDir.AutoSize = true;
        _lblBaseDir.Location = new Point(3, 44);
        _lblBaseDir.Margin = new Padding(3);
        _lblBaseDir.Name = "_lblBaseDir";
        _lblBaseDir.Size = new Size(75, 20);
        _lblBaseDir.TabIndex = 2;
        _lblBaseDir.Text = "Base Dir:";

        // _txtBaseDir
        _txtBaseDir.Anchor = AnchorStyles.Left | AnchorStyles.Right;
        _txtBaseDir.Location = new Point(84, 40);
        _txtBaseDir.Name = "_txtBaseDir";
        _txtBaseDir.Size = new Size(235, 27);
        _txtBaseDir.TabIndex = 3;

        // _btnBrowse
        _btnBrowse.Anchor = AnchorStyles.Left;
        _btnBrowse.Location = new Point(325, 37);
        _btnBrowse.Name = "_btnBrowse";
        _btnBrowse.Size = new Size(94, 34);
        _btnBrowse.TabIndex = 4;
        _btnBrowse.Text = "Browse...";
        _btnBrowse.UseVisualStyleBackColor = true;
        _btnBrowse.Click += BtnBrowse_Click;

        // _lblButtonSize
        _lblButtonSize.Anchor = AnchorStyles.Left | AnchorStyles.Right;
        _lblButtonSize.AutoSize = true;
        _lblButtonSize.Location = new Point(3, 81);
        _lblButtonSize.Margin = new Padding(3);
        _lblButtonSize.Name = "_lblButtonSize";
        _lblButtonSize.Size = new Size(75, 20);
        _lblButtonSize.TabIndex = 5;
        _lblButtonSize.Text = "Button Size:";

        // _numButtonSize
        _numButtonSize.Anchor = AnchorStyles.Left;
        _numButtonSize.Location = new Point(84, 77);
        _numButtonSize.Maximum = new decimal(new int[] { 200, 0, 0, 0 });
        _numButtonSize.Minimum = new decimal(new int[] { 48, 0, 0, 0 });
        _numButtonSize.Name = "_numButtonSize";
        _numButtonSize.Size = new Size(100, 27);
        _numButtonSize.TabIndex = 6;
        _numButtonSize.Value = new decimal(new int[] { 60, 0, 0, 0 });

        // _tabRecording
        _tabRecording.Controls.Add(_tableLayoutRecording);
        _tabRecording.Location = new Point(4, 29);
        _tabRecording.Name = "_tabRecording";
        _tabRecording.Padding = new Padding(12);
        _tabRecording.Size = new Size(446, 253);
        _tabRecording.TabIndex = 1;
        _tabRecording.Text = "Recording";
        _tabRecording.UseVisualStyleBackColor = true;

        // _tableLayoutRecording
        _tableLayoutRecording.ColumnCount = 1;
        _tableLayoutRecording.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
        _tableLayoutRecording.Controls.Add(_chkAutoSave, 0, 0);
        _tableLayoutRecording.Controls.Add(_chkConfirmClose, 0, 1);
        _tableLayoutRecording.Dock = DockStyle.Fill;
        _tableLayoutRecording.Location = new Point(12, 12);
        _tableLayoutRecording.Name = "_tableLayoutRecording";
        _tableLayoutRecording.RowCount = 3;
        _tableLayoutRecording.RowStyles.Add(new RowStyle(SizeType.AutoSize));
        _tableLayoutRecording.RowStyles.Add(new RowStyle(SizeType.AutoSize));
        _tableLayoutRecording.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
        _tableLayoutRecording.Size = new Size(422, 229);
        _tableLayoutRecording.TabIndex = 0;

        // _chkAutoSave
        _chkAutoSave.AutoSize = true;
        _chkAutoSave.Location = new Point(3, 3);
        _chkAutoSave.Name = "_chkAutoSave";
        _chkAutoSave.Size = new Size(175, 24);
        _chkAutoSave.TabIndex = 0;
        _chkAutoSave.Text = "Auto-save recordings";
        _chkAutoSave.UseVisualStyleBackColor = true;

        // _chkConfirmClose
        _chkConfirmClose.AutoSize = true;
        _chkConfirmClose.Location = new Point(3, 33);
        _chkConfirmClose.Name = "_chkConfirmClose";
        _chkConfirmClose.Size = new Size(245, 24);
        _chkConfirmClose.TabIndex = 1;
        _chkConfirmClose.Text = "Confirm before closing recording";
        _chkConfirmClose.UseVisualStyleBackColor = true;

        // _tabExport
        _tabExport.Controls.Add(_tableLayoutExport);
        _tabExport.Location = new Point(4, 29);
        _tabExport.Name = "_tabExport";
        _tabExport.Padding = new Padding(12);
        _tabExport.Size = new Size(446, 253);
        _tabExport.TabIndex = 2;
        _tabExport.Text = "Export";
        _tabExport.UseVisualStyleBackColor = true;

        // _tableLayoutExport
        _tableLayoutExport.ColumnCount = 2;
        _tableLayoutExport.ColumnStyles.Add(new ColumnStyle(SizeType.AutoSize));
        _tableLayoutExport.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
        _tableLayoutExport.Controls.Add(_lblCsvDelimiter, 0, 0);
        _tableLayoutExport.Controls.Add(_cboCsvDelimiter, 1, 0);
        _tableLayoutExport.Dock = DockStyle.Fill;
        _tableLayoutExport.Location = new Point(12, 12);
        _tableLayoutExport.Name = "_tableLayoutExport";
        _tableLayoutExport.RowCount = 2;
        _tableLayoutExport.RowStyles.Add(new RowStyle(SizeType.AutoSize));
        _tableLayoutExport.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
        _tableLayoutExport.Size = new Size(422, 229);
        _tableLayoutExport.TabIndex = 0;

        // _lblCsvDelimiter
        _lblCsvDelimiter.Anchor = AnchorStyles.Left | AnchorStyles.Right;
        _lblCsvDelimiter.AutoSize = true;
        _lblCsvDelimiter.Location = new Point(3, 7);
        _lblCsvDelimiter.Margin = new Padding(3);
        _lblCsvDelimiter.Name = "_lblCsvDelimiter";
        _lblCsvDelimiter.Size = new Size(100, 20);
        _lblCsvDelimiter.TabIndex = 0;
        _lblCsvDelimiter.Text = "CSV Delimiter:";

        // _cboCsvDelimiter
        _cboCsvDelimiter.Anchor = AnchorStyles.Left;
        _cboCsvDelimiter.DropDownStyle = ComboBoxStyle.DropDownList;
        _cboCsvDelimiter.FormattingEnabled = true;
        _cboCsvDelimiter.Location = new Point(109, 3);
        _cboCsvDelimiter.Name = "_cboCsvDelimiter";
        _cboCsvDelimiter.Size = new Size(120, 28);
        _cboCsvDelimiter.TabIndex = 1;

        // _flowLayoutButtons
        _flowLayoutButtons.Anchor = AnchorStyles.Right;
        _flowLayoutButtons.AutoSize = true;
        _flowLayoutButtons.AutoSizeMode = AutoSizeMode.GrowAndShrink;
        _flowLayoutButtons.Controls.Add(_btnCancel);
        _flowLayoutButtons.Controls.Add(_btnOK);
        _flowLayoutButtons.FlowDirection = FlowDirection.RightToLeft;
        _flowLayoutButtons.Location = new Point(252, 307);
        _flowLayoutButtons.Name = "_flowLayoutButtons";
        _flowLayoutButtons.Size = new Size(217, 39);
        _flowLayoutButtons.TabIndex = 1;

        // _btnOK
        _btnOK.Location = new Point(111, 3);
        _btnOK.Margin = new Padding(6, 3, 3, 3);
        _btnOK.MinimumSize = new Size(100, 33);
        _btnOK.Name = "_btnOK";
        _btnOK.Size = new Size(100, 33);
        _btnOK.TabIndex = 1;
        _btnOK.Text = "OK";
        _btnOK.UseVisualStyleBackColor = true;
        _btnOK.Click += BtnOK_Click;

        // _btnCancel
        _btnCancel.DialogResult = DialogResult.Cancel;
        _btnCancel.Location = new Point(3, 3);
        _btnCancel.MinimumSize = new Size(100, 33);
        _btnCancel.Name = "_btnCancel";
        _btnCancel.Size = new Size(100, 33);
        _btnCancel.TabIndex = 0;
        _btnCancel.Text = "Cancel";
        _btnCancel.UseVisualStyleBackColor = true;

        // SettingsDialog
        AcceptButton = _btnOK;
        AutoScaleDimensions = new SizeF(8F, 20F);
        AutoScaleMode = AutoScaleMode.Font;
        CancelButton = _btnCancel;
        ClientSize = new Size(484, 361);
        Controls.Add(_tableLayoutMain);
        FormBorderStyle = FormBorderStyle.FixedDialog;
        MaximizeBox = false;
        MinimizeBox = false;
        Name = "SettingsDialog";
        ShowInTaskbar = false;
        StartPosition = FormStartPosition.CenterParent;
        Text = "Settings";
        _tableLayoutMain.ResumeLayout(false);
        _tableLayoutMain.PerformLayout();
        _tabControl.ResumeLayout(false);
        _tabGeneral.ResumeLayout(false);
        _tableLayoutGeneral.ResumeLayout(false);
        _tableLayoutGeneral.PerformLayout();
        ((System.ComponentModel.ISupportInitialize)_numButtonSize).EndInit();
        _tabRecording.ResumeLayout(false);
        _tableLayoutRecording.ResumeLayout(false);
        _tableLayoutRecording.PerformLayout();
        _tabExport.ResumeLayout(false);
        _tableLayoutExport.ResumeLayout(false);
        _tableLayoutExport.PerformLayout();
        _flowLayoutButtons.ResumeLayout(false);
        ResumeLayout(false);
    }

    #endregion

    private TableLayoutPanel _tableLayoutMain;
    private TabControl _tabControl;
    private TabPage _tabGeneral;
    private TabPage _tabRecording;
    private TabPage _tabExport;
    private TableLayoutPanel _tableLayoutGeneral;
    private Label _lblLanguage;
    private ComboBox _cboLanguage;
    private Label _lblBaseDir;
    private TextBox _txtBaseDir;
    private Button _btnBrowse;
    private Label _lblButtonSize;
    private NumericUpDown _numButtonSize;
    private TableLayoutPanel _tableLayoutRecording;
    private CheckBox _chkAutoSave;
    private CheckBox _chkConfirmClose;
    private TableLayoutPanel _tableLayoutExport;
    private Label _lblCsvDelimiter;
    private ComboBox _cboCsvDelimiter;
    private FlowLayoutPanel _flowLayoutButtons;
    private Button _btnOK;
    private Button _btnCancel;
}
