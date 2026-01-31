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

    #region Windows Form Designer generated code

    private void InitializeComponent()
    {
        _tableLayoutPanel = new TableLayoutPanel();
        _lblLanguage = new Label();
        _cboLanguage = new ComboBox();
        _lblBaseDirectory = new Label();
        _pnlBaseDirectory = new Panel();
        _txtBaseDirectory = new TextBox();
        _btnBrowse = new Button();
        _lblButtonSize = new Label();
        _numButtonSize = new NumericUpDown();
        _lblCsvDelimiter = new Label();
        _cboCsvDelimiter = new ComboBox();
        _chkAutoSave = new CheckBox();
        _chkConfirmClose = new CheckBox();
        _pnlButtons = new FlowLayoutPanel();
        _btnOK = new Button();
        _btnCancel = new Button();
        _tableLayoutPanel.SuspendLayout();
        _pnlBaseDirectory.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)_numButtonSize).BeginInit();
        _pnlButtons.SuspendLayout();
        SuspendLayout();
        // 
        // _tableLayoutPanel
        // 
        _tableLayoutPanel.ColumnCount = 2;
        _tableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.AutoSize));
        _tableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
        _tableLayoutPanel.Controls.Add(_lblLanguage, 0, 0);
        _tableLayoutPanel.Controls.Add(_cboLanguage, 1, 0);
        _tableLayoutPanel.Controls.Add(_lblBaseDirectory, 0, 1);
        _tableLayoutPanel.Controls.Add(_pnlBaseDirectory, 1, 1);
        _tableLayoutPanel.Controls.Add(_lblButtonSize, 0, 2);
        _tableLayoutPanel.Controls.Add(_numButtonSize, 1, 2);
        _tableLayoutPanel.Controls.Add(_lblCsvDelimiter, 0, 3);
        _tableLayoutPanel.Controls.Add(_cboCsvDelimiter, 1, 3);
        _tableLayoutPanel.Controls.Add(_chkAutoSave, 0, 4);
        _tableLayoutPanel.Controls.Add(_chkConfirmClose, 0, 5);
        _tableLayoutPanel.Controls.Add(_pnlButtons, 0, 6);
        _tableLayoutPanel.Dock = DockStyle.Fill;
        _tableLayoutPanel.Location = new Point(0, 0);
        _tableLayoutPanel.Name = "_tableLayoutPanel";
        _tableLayoutPanel.Padding = new Padding(20);
        _tableLayoutPanel.RowCount = 7;
        _tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.AutoSize));
        _tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.AutoSize));
        _tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.AutoSize));
        _tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.AutoSize));
        _tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.AutoSize));
        _tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.AutoSize));
        _tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
        _tableLayoutPanel.Size = new Size(500, 340);
        _tableLayoutPanel.TabIndex = 0;
        // 
        // _lblLanguage
        // 
        _lblLanguage.Anchor = AnchorStyles.Left | AnchorStyles.Right;
        _lblLanguage.AutoSize = true;
        _lblLanguage.Location = new Point(23, 30);
        _lblLanguage.Margin = new Padding(3, 8, 15, 8);
        _lblLanguage.Name = "_lblLanguage";
        _lblLanguage.Size = new Size(100, 20);
        _lblLanguage.TabIndex = 0;
        _lblLanguage.Text = "Language";
        // 
        // _cboLanguage
        // 
        _cboLanguage.Anchor = AnchorStyles.Left | AnchorStyles.Right;
        _cboLanguage.DropDownStyle = ComboBoxStyle.DropDownList;
        _cboLanguage.Font = new Font("Segoe UI", 10F);
        _cboLanguage.FormattingEnabled = true;
        _cboLanguage.Location = new Point(141, 26);
        _cboLanguage.Margin = new Padding(3, 6, 3, 6);
        _cboLanguage.Name = "_cboLanguage";
        _cboLanguage.Size = new Size(336, 31);
        _cboLanguage.TabIndex = 1;
        // 
        // _lblBaseDirectory
        // 
        _lblBaseDirectory.Anchor = AnchorStyles.Left | AnchorStyles.Right;
        _lblBaseDirectory.AutoSize = true;
        _lblBaseDirectory.Location = new Point(23, 74);
        _lblBaseDirectory.Margin = new Padding(3, 8, 15, 8);
        _lblBaseDirectory.Name = "_lblBaseDirectory";
        _lblBaseDirectory.Size = new Size(100, 20);
        _lblBaseDirectory.TabIndex = 2;
        _lblBaseDirectory.Text = "Base Directory";
        // 
        // _pnlBaseDirectory
        // 
        _pnlBaseDirectory.Controls.Add(_txtBaseDirectory);
        _pnlBaseDirectory.Controls.Add(_btnBrowse);
        _pnlBaseDirectory.Dock = DockStyle.Fill;
        _pnlBaseDirectory.Location = new Point(141, 66);
        _pnlBaseDirectory.Name = "_pnlBaseDirectory";
        _pnlBaseDirectory.Size = new Size(336, 36);
        _pnlBaseDirectory.TabIndex = 3;
        // 
        // _txtBaseDirectory
        // 
        _txtBaseDirectory.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        _txtBaseDirectory.Font = new Font("Segoe UI", 10F);
        _txtBaseDirectory.Location = new Point(0, 3);
        _txtBaseDirectory.Name = "_txtBaseDirectory";
        _txtBaseDirectory.Size = new Size(236, 30);
        _txtBaseDirectory.TabIndex = 0;
        // 
        // _btnBrowse
        // 
        _btnBrowse.Anchor = AnchorStyles.Top | AnchorStyles.Right;
        _btnBrowse.Location = new Point(242, 2);
        _btnBrowse.Name = "_btnBrowse";
        _btnBrowse.Size = new Size(94, 32);
        _btnBrowse.TabIndex = 1;
        _btnBrowse.Text = "Browse...";
        _btnBrowse.UseVisualStyleBackColor = true;
        _btnBrowse.Click += BtnBrowse_Click;
        // 
        // _lblButtonSize
        // 
        _lblButtonSize.Anchor = AnchorStyles.Left | AnchorStyles.Right;
        _lblButtonSize.AutoSize = true;
        _lblButtonSize.Location = new Point(23, 117);
        _lblButtonSize.Margin = new Padding(3, 8, 15, 8);
        _lblButtonSize.Name = "_lblButtonSize";
        _lblButtonSize.Size = new Size(100, 20);
        _lblButtonSize.TabIndex = 4;
        _lblButtonSize.Text = "Button Size";
        // 
        // _numButtonSize
        // 
        _numButtonSize.Font = new Font("Segoe UI", 10F);
        _numButtonSize.Location = new Point(141, 111);
        _numButtonSize.Margin = new Padding(3, 6, 3, 6);
        _numButtonSize.Maximum = new decimal(new int[] { 150, 0, 0, 0 });
        _numButtonSize.Minimum = new decimal(new int[] { 48, 0, 0, 0 });
        _numButtonSize.Name = "_numButtonSize";
        _numButtonSize.Size = new Size(100, 30);
        _numButtonSize.TabIndex = 5;
        _numButtonSize.Value = new decimal(new int[] { 60, 0, 0, 0 });
        // 
        // _lblCsvDelimiter
        // 
        _lblCsvDelimiter.Anchor = AnchorStyles.Left | AnchorStyles.Right;
        _lblCsvDelimiter.AutoSize = true;
        _lblCsvDelimiter.Location = new Point(23, 158);
        _lblCsvDelimiter.Margin = new Padding(3, 8, 15, 8);
        _lblCsvDelimiter.Name = "_lblCsvDelimiter";
        _lblCsvDelimiter.Size = new Size(100, 20);
        _lblCsvDelimiter.TabIndex = 6;
        _lblCsvDelimiter.Text = "CSV Delimiter";
        // 
        // _cboCsvDelimiter
        // 
        _cboCsvDelimiter.DropDownStyle = ComboBoxStyle.DropDownList;
        _cboCsvDelimiter.Font = new Font("Segoe UI", 10F);
        _cboCsvDelimiter.FormattingEnabled = true;
        _cboCsvDelimiter.Location = new Point(141, 153);
        _cboCsvDelimiter.Margin = new Padding(3, 6, 3, 6);
        _cboCsvDelimiter.Name = "_cboCsvDelimiter";
        _cboCsvDelimiter.Size = new Size(180, 31);
        _cboCsvDelimiter.TabIndex = 7;
        // 
        // _chkAutoSave
        // 
        _chkAutoSave.AutoSize = true;
        _tableLayoutPanel.SetColumnSpan(_chkAutoSave, 2);
        _chkAutoSave.Location = new Point(23, 196);
        _chkAutoSave.Margin = new Padding(3, 6, 3, 6);
        _chkAutoSave.Name = "_chkAutoSave";
        _chkAutoSave.Size = new Size(177, 24);
        _chkAutoSave.TabIndex = 8;
        _chkAutoSave.Text = "Auto-save recordings";
        _chkAutoSave.UseVisualStyleBackColor = true;
        // 
        // _chkConfirmClose
        // 
        _chkConfirmClose.AutoSize = true;
        _tableLayoutPanel.SetColumnSpan(_chkConfirmClose, 2);
        _chkConfirmClose.Location = new Point(23, 232);
        _chkConfirmClose.Margin = new Padding(3, 6, 3, 6);
        _chkConfirmClose.Name = "_chkConfirmClose";
        _chkConfirmClose.Size = new Size(261, 24);
        _chkConfirmClose.TabIndex = 9;
        _chkConfirmClose.Text = "Confirm before closing recording";
        _chkConfirmClose.UseVisualStyleBackColor = true;
        // 
        // _pnlButtons
        // 
        _pnlButtons.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
        _tableLayoutPanel.SetColumnSpan(_pnlButtons, 2);
        _pnlButtons.Controls.Add(_btnCancel);
        _pnlButtons.Controls.Add(_btnOK);
        _pnlButtons.FlowDirection = FlowDirection.RightToLeft;
        _pnlButtons.Location = new Point(267, 285);
        _pnlButtons.Margin = new Padding(3, 10, 3, 3);
        _pnlButtons.Name = "_pnlButtons";
        _pnlButtons.Size = new Size(210, 32);
        _pnlButtons.TabIndex = 10;
        // 
        // _btnOK
        // 
        _btnOK.Location = new Point(107, 3);
        _btnOK.MinimumSize = new Size(100, 28);
        _btnOK.Name = "_btnOK";
        _btnOK.Size = new Size(100, 28);
        _btnOK.TabIndex = 0;
        _btnOK.Text = "OK";
        _btnOK.UseVisualStyleBackColor = true;
        _btnOK.Click += BtnOK_Click;
        // 
        // _btnCancel
        // 
        _btnCancel.DialogResult = DialogResult.Cancel;
        _btnCancel.Location = new Point(1, 3);
        _btnCancel.MinimumSize = new Size(100, 28);
        _btnCancel.Name = "_btnCancel";
        _btnCancel.Size = new Size(100, 28);
        _btnCancel.TabIndex = 1;
        _btnCancel.Text = "Cancel";
        _btnCancel.UseVisualStyleBackColor = true;
        _btnCancel.Click += BtnCancel_Click;
        // 
        // SettingsDialog
        // 
        AcceptButton = _btnOK;
        AutoScaleDimensions = new SizeF(8F, 20F);
        AutoScaleMode = AutoScaleMode.Font;
        CancelButton = _btnCancel;
        ClientSize = new Size(500, 340);
        Controls.Add(_tableLayoutPanel);
        FormBorderStyle = FormBorderStyle.FixedDialog;
        MaximizeBox = false;
        MinimizeBox = false;
        Name = "SettingsDialog";
        ShowInTaskbar = false;
        StartPosition = FormStartPosition.CenterParent;
        Text = "Settings";
        _tableLayoutPanel.ResumeLayout(false);
        _tableLayoutPanel.PerformLayout();
        _pnlBaseDirectory.ResumeLayout(false);
        _pnlBaseDirectory.PerformLayout();
        ((System.ComponentModel.ISupportInitialize)_numButtonSize).EndInit();
        _pnlButtons.ResumeLayout(false);
        ResumeLayout(false);
    }

    #endregion

    private TableLayoutPanel _tableLayoutPanel;
    private Label _lblLanguage;
    private ComboBox _cboLanguage;
    private Label _lblBaseDirectory;
    private Panel _pnlBaseDirectory;
    private TextBox _txtBaseDirectory;
    private Button _btnBrowse;
    private Label _lblButtonSize;
    private NumericUpDown _numButtonSize;
    private Label _lblCsvDelimiter;
    private ComboBox _cboCsvDelimiter;
    private CheckBox _chkAutoSave;
    private CheckBox _chkConfirmClose;
    private FlowLayoutPanel _pnlButtons;
    private Button _btnOK;
    private Button _btnCancel;
}
