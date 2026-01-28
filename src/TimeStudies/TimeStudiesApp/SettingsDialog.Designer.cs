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

        _tabControl = new TabControl();
        _tabGeneral = new TabPage();
        _tabRecording = new TabPage();
        _tabExport = new TabPage();

        _tlpGeneral = new TableLayoutPanel();
        _lblLanguage = new Label();
        _cboLanguage = new ComboBox();
        _lblBaseDirectory = new Label();
        _txtBaseDirectory = new TextBox();
        _btnBrowse = new Button();
        _lblButtonSize = new Label();
        _numButtonSize = new NumericUpDown();

        _tlpRecording = new TableLayoutPanel();
        _chkAutoSave = new CheckBox();
        _chkConfirmClose = new CheckBox();

        _tlpExport = new TableLayoutPanel();
        _lblCsvDelimiter = new Label();
        _cboDelimiter = new ComboBox();

        _flpButtons = new FlowLayoutPanel();
        _btnOK = new Button();
        _btnCancel = new Button();

        _tabControl.SuspendLayout();
        _tabGeneral.SuspendLayout();
        _tabRecording.SuspendLayout();
        _tabExport.SuspendLayout();
        _tlpGeneral.SuspendLayout();
        _tlpRecording.SuspendLayout();
        _tlpExport.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)_numButtonSize).BeginInit();
        _flpButtons.SuspendLayout();
        SuspendLayout();

        // _tabControl
        _tabControl.Dock = DockStyle.Fill;
        _tabControl.Location = new Point(12, 12);
        _tabControl.Name = "_tabControl";
        _tabControl.SelectedIndex = 0;
        _tabControl.Size = new Size(460, 280);
        _tabControl.TabIndex = 0;
        _tabControl.Controls.Add(_tabGeneral);
        _tabControl.Controls.Add(_tabRecording);
        _tabControl.Controls.Add(_tabExport);

        // _tabGeneral
        _tabGeneral.Controls.Add(_tlpGeneral);
        _tabGeneral.Location = new Point(4, 29);
        _tabGeneral.Name = "_tabGeneral";
        _tabGeneral.Padding = new Padding(8);
        _tabGeneral.Size = new Size(452, 247);
        _tabGeneral.TabIndex = 0;
        _tabGeneral.Text = "General";
        _tabGeneral.UseVisualStyleBackColor = true;

        // _tlpGeneral
        _tlpGeneral.ColumnCount = 3;
        _tlpGeneral.ColumnStyles.Add(new ColumnStyle(SizeType.AutoSize));
        _tlpGeneral.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
        _tlpGeneral.ColumnStyles.Add(new ColumnStyle(SizeType.AutoSize));
        _tlpGeneral.Controls.Add(_lblLanguage, 0, 0);
        _tlpGeneral.Controls.Add(_cboLanguage, 1, 0);
        _tlpGeneral.Controls.Add(_lblBaseDirectory, 0, 1);
        _tlpGeneral.Controls.Add(_txtBaseDirectory, 1, 1);
        _tlpGeneral.Controls.Add(_btnBrowse, 2, 1);
        _tlpGeneral.Controls.Add(_lblButtonSize, 0, 2);
        _tlpGeneral.Controls.Add(_numButtonSize, 1, 2);
        _tlpGeneral.Dock = DockStyle.Fill;
        _tlpGeneral.Location = new Point(8, 8);
        _tlpGeneral.Name = "_tlpGeneral";
        _tlpGeneral.RowCount = 4;
        _tlpGeneral.RowStyles.Add(new RowStyle(SizeType.AutoSize));
        _tlpGeneral.RowStyles.Add(new RowStyle(SizeType.AutoSize));
        _tlpGeneral.RowStyles.Add(new RowStyle(SizeType.AutoSize));
        _tlpGeneral.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
        _tlpGeneral.Size = new Size(436, 231);
        _tlpGeneral.TabIndex = 0;

        // _lblLanguage
        _lblLanguage.Anchor = AnchorStyles.Left | AnchorStyles.Right;
        _lblLanguage.AutoSize = true;
        _lblLanguage.Location = new Point(3, 7);
        _lblLanguage.Margin = new Padding(3);
        _lblLanguage.Name = "_lblLanguage";
        _lblLanguage.Size = new Size(100, 20);
        _lblLanguage.TabIndex = 0;
        _lblLanguage.Text = "Language";

        // _cboLanguage
        _cboLanguage.Anchor = AnchorStyles.Left | AnchorStyles.Right;
        _cboLanguage.DropDownStyle = ComboBoxStyle.DropDownList;
        _cboLanguage.Location = new Point(109, 3);
        _cboLanguage.Margin = new Padding(3);
        _cboLanguage.Name = "_cboLanguage";
        _cboLanguage.Size = new Size(224, 28);
        _cboLanguage.TabIndex = 1;

        // _lblBaseDirectory
        _lblBaseDirectory.Anchor = AnchorStyles.Left | AnchorStyles.Right;
        _lblBaseDirectory.AutoSize = true;
        _lblBaseDirectory.Location = new Point(3, 44);
        _lblBaseDirectory.Margin = new Padding(3);
        _lblBaseDirectory.Name = "_lblBaseDirectory";
        _lblBaseDirectory.Size = new Size(100, 20);
        _lblBaseDirectory.TabIndex = 2;
        _lblBaseDirectory.Text = "Base Directory";

        // _txtBaseDirectory
        _txtBaseDirectory.Anchor = AnchorStyles.Left | AnchorStyles.Right;
        _txtBaseDirectory.Location = new Point(109, 40);
        _txtBaseDirectory.Margin = new Padding(3);
        _txtBaseDirectory.Name = "_txtBaseDirectory";
        _txtBaseDirectory.Size = new Size(224, 27);
        _txtBaseDirectory.TabIndex = 3;

        // _btnBrowse
        _btnBrowse.Anchor = AnchorStyles.Left | AnchorStyles.Right;
        _btnBrowse.Location = new Point(339, 37);
        _btnBrowse.Margin = new Padding(3);
        _btnBrowse.Name = "_btnBrowse";
        _btnBrowse.Size = new Size(94, 34);
        _btnBrowse.TabIndex = 4;
        _btnBrowse.Text = "Browse...";
        _btnBrowse.UseVisualStyleBackColor = true;
        _btnBrowse.Click += BtnBrowse_Click;

        // _lblButtonSize
        _lblButtonSize.Anchor = AnchorStyles.Left | AnchorStyles.Right;
        _lblButtonSize.AutoSize = true;
        _lblButtonSize.Location = new Point(3, 82);
        _lblButtonSize.Margin = new Padding(3);
        _lblButtonSize.Name = "_lblButtonSize";
        _lblButtonSize.Size = new Size(100, 20);
        _lblButtonSize.TabIndex = 5;
        _lblButtonSize.Text = "Button Size";

        // _numButtonSize
        _numButtonSize.Anchor = AnchorStyles.Left;
        _numButtonSize.Location = new Point(109, 77);
        _numButtonSize.Margin = new Padding(3);
        _numButtonSize.Maximum = 120;
        _numButtonSize.Minimum = 48;
        _numButtonSize.Name = "_numButtonSize";
        _numButtonSize.Size = new Size(80, 27);
        _numButtonSize.TabIndex = 6;
        _numButtonSize.Value = 60;

        // _tabRecording
        _tabRecording.Controls.Add(_tlpRecording);
        _tabRecording.Location = new Point(4, 29);
        _tabRecording.Name = "_tabRecording";
        _tabRecording.Padding = new Padding(8);
        _tabRecording.Size = new Size(452, 247);
        _tabRecording.TabIndex = 1;
        _tabRecording.Text = "Recording";
        _tabRecording.UseVisualStyleBackColor = true;

        // _tlpRecording
        _tlpRecording.ColumnCount = 1;
        _tlpRecording.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
        _tlpRecording.Controls.Add(_chkAutoSave, 0, 0);
        _tlpRecording.Controls.Add(_chkConfirmClose, 0, 1);
        _tlpRecording.Dock = DockStyle.Fill;
        _tlpRecording.Location = new Point(8, 8);
        _tlpRecording.Name = "_tlpRecording";
        _tlpRecording.RowCount = 3;
        _tlpRecording.RowStyles.Add(new RowStyle(SizeType.AutoSize));
        _tlpRecording.RowStyles.Add(new RowStyle(SizeType.AutoSize));
        _tlpRecording.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
        _tlpRecording.Size = new Size(436, 231);
        _tlpRecording.TabIndex = 0;

        // _chkAutoSave
        _chkAutoSave.AutoSize = true;
        _chkAutoSave.Location = new Point(3, 3);
        _chkAutoSave.Margin = new Padding(3);
        _chkAutoSave.Name = "_chkAutoSave";
        _chkAutoSave.Size = new Size(180, 24);
        _chkAutoSave.TabIndex = 0;
        _chkAutoSave.Text = "Auto-save recordings";
        _chkAutoSave.UseVisualStyleBackColor = true;

        // _chkConfirmClose
        _chkConfirmClose.AutoSize = true;
        _chkConfirmClose.Location = new Point(3, 33);
        _chkConfirmClose.Margin = new Padding(3);
        _chkConfirmClose.Name = "_chkConfirmClose";
        _chkConfirmClose.Size = new Size(250, 24);
        _chkConfirmClose.TabIndex = 1;
        _chkConfirmClose.Text = "Confirm before closing recording";
        _chkConfirmClose.UseVisualStyleBackColor = true;

        // _tabExport
        _tabExport.Controls.Add(_tlpExport);
        _tabExport.Location = new Point(4, 29);
        _tabExport.Name = "_tabExport";
        _tabExport.Padding = new Padding(8);
        _tabExport.Size = new Size(452, 247);
        _tabExport.TabIndex = 2;
        _tabExport.Text = "Export";
        _tabExport.UseVisualStyleBackColor = true;

        // _tlpExport
        _tlpExport.ColumnCount = 2;
        _tlpExport.ColumnStyles.Add(new ColumnStyle(SizeType.AutoSize));
        _tlpExport.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
        _tlpExport.Controls.Add(_lblCsvDelimiter, 0, 0);
        _tlpExport.Controls.Add(_cboDelimiter, 1, 0);
        _tlpExport.Dock = DockStyle.Fill;
        _tlpExport.Location = new Point(8, 8);
        _tlpExport.Name = "_tlpExport";
        _tlpExport.RowCount = 2;
        _tlpExport.RowStyles.Add(new RowStyle(SizeType.AutoSize));
        _tlpExport.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
        _tlpExport.Size = new Size(436, 231);
        _tlpExport.TabIndex = 0;

        // _lblCsvDelimiter
        _lblCsvDelimiter.Anchor = AnchorStyles.Left | AnchorStyles.Right;
        _lblCsvDelimiter.AutoSize = true;
        _lblCsvDelimiter.Location = new Point(3, 7);
        _lblCsvDelimiter.Margin = new Padding(3);
        _lblCsvDelimiter.Name = "_lblCsvDelimiter";
        _lblCsvDelimiter.Size = new Size(100, 20);
        _lblCsvDelimiter.TabIndex = 0;
        _lblCsvDelimiter.Text = "CSV Delimiter";

        // _cboDelimiter
        _cboDelimiter.Anchor = AnchorStyles.Left;
        _cboDelimiter.DropDownStyle = ComboBoxStyle.DropDownList;
        _cboDelimiter.Location = new Point(109, 3);
        _cboDelimiter.Margin = new Padding(3);
        _cboDelimiter.Name = "_cboDelimiter";
        _cboDelimiter.Size = new Size(180, 28);
        _cboDelimiter.TabIndex = 1;

        // _flpButtons
        _flpButtons.AutoSize = true;
        _flpButtons.Controls.Add(_btnCancel);
        _flpButtons.Controls.Add(_btnOK);
        _flpButtons.Dock = DockStyle.Bottom;
        _flpButtons.FlowDirection = FlowDirection.RightToLeft;
        _flpButtons.Location = new Point(12, 292);
        _flpButtons.Name = "_flpButtons";
        _flpButtons.Padding = new Padding(0, 8, 0, 0);
        _flpButtons.Size = new Size(460, 48);
        _flpButtons.TabIndex = 1;

        // _btnOK
        _btnOK.Location = new Point(290, 11);
        _btnOK.Margin = new Padding(8, 3, 3, 3);
        _btnOK.MinimumSize = new Size(80, 34);
        _btnOK.Name = "_btnOK";
        _btnOK.Size = new Size(80, 34);
        _btnOK.TabIndex = 1;
        _btnOK.Text = "OK";
        _btnOK.UseVisualStyleBackColor = true;
        _btnOK.Click += BtnOK_Click;

        // _btnCancel
        _btnCancel.Location = new Point(377, 11);
        _btnCancel.Margin = new Padding(3);
        _btnCancel.MinimumSize = new Size(80, 34);
        _btnCancel.Name = "_btnCancel";
        _btnCancel.Size = new Size(80, 34);
        _btnCancel.TabIndex = 0;
        _btnCancel.Text = "Cancel";
        _btnCancel.UseVisualStyleBackColor = true;
        _btnCancel.Click += BtnCancel_Click;

        // SettingsDialog
        AcceptButton = _btnOK;
        AutoScaleDimensions = new SizeF(8F, 20F);
        AutoScaleMode = AutoScaleMode.Font;
        CancelButton = _btnCancel;
        ClientSize = new Size(484, 352);
        Controls.Add(_tabControl);
        Controls.Add(_flpButtons);
        FormBorderStyle = FormBorderStyle.FixedDialog;
        MaximizeBox = false;
        MinimizeBox = false;
        Name = "SettingsDialog";
        Padding = new Padding(12);
        ShowIcon = false;
        ShowInTaskbar = false;
        StartPosition = FormStartPosition.CenterParent;
        Text = "Settings";

        _tabControl.ResumeLayout(false);
        _tabGeneral.ResumeLayout(false);
        _tabRecording.ResumeLayout(false);
        _tabExport.ResumeLayout(false);
        _tlpGeneral.ResumeLayout(false);
        _tlpGeneral.PerformLayout();
        _tlpRecording.ResumeLayout(false);
        _tlpRecording.PerformLayout();
        _tlpExport.ResumeLayout(false);
        _tlpExport.PerformLayout();
        ((System.ComponentModel.ISupportInitialize)_numButtonSize).EndInit();
        _flpButtons.ResumeLayout(false);
        ResumeLayout(false);
        PerformLayout();
    }

    #endregion

    private TabControl _tabControl;
    private TabPage _tabGeneral;
    private TabPage _tabRecording;
    private TabPage _tabExport;
    private TableLayoutPanel _tlpGeneral;
    private Label _lblLanguage;
    private ComboBox _cboLanguage;
    private Label _lblBaseDirectory;
    private TextBox _txtBaseDirectory;
    private Button _btnBrowse;
    private Label _lblButtonSize;
    private NumericUpDown _numButtonSize;
    private TableLayoutPanel _tlpRecording;
    private CheckBox _chkAutoSave;
    private CheckBox _chkConfirmClose;
    private TableLayoutPanel _tlpExport;
    private Label _lblCsvDelimiter;
    private ComboBox _cboDelimiter;
    private FlowLayoutPanel _flpButtons;
    private Button _btnOK;
    private Button _btnCancel;
}
