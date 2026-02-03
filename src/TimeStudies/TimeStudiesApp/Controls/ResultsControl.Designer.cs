namespace TimeStudiesApp.Controls;

partial class ResultsControl
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
        _headerPanel = new Panel();
        _lblRecordingInfo = new Label();
        _btnExport = new Button();
        _tabControl = new TabControl();
        _tabDetails = new TabPage();
        _dgvDetails = new DataGridView();
        _tabSummary = new TabPage();
        _dgvSummary = new DataGridView();

        _tableLayoutPanel.SuspendLayout();
        _headerPanel.SuspendLayout();
        _tabControl.SuspendLayout();
        _tabDetails.SuspendLayout();
        _tabSummary.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)_dgvDetails).BeginInit();
        ((System.ComponentModel.ISupportInitialize)_dgvSummary).BeginInit();
        SuspendLayout();

        // _tableLayoutPanel
        _tableLayoutPanel.ColumnCount = 1;
        _tableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
        _tableLayoutPanel.Controls.Add(_headerPanel, 0, 0);
        _tableLayoutPanel.Controls.Add(_tabControl, 0, 1);
        _tableLayoutPanel.Dock = DockStyle.Fill;
        _tableLayoutPanel.Location = new Point(0, 0);
        _tableLayoutPanel.Name = "_tableLayoutPanel";
        _tableLayoutPanel.RowCount = 2;
        _tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.AutoSize));
        _tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
        _tableLayoutPanel.Size = new Size(800, 500);
        _tableLayoutPanel.TabIndex = 0;

        // _headerPanel
        _headerPanel.AutoSize = true;
        _headerPanel.Controls.Add(_btnExport);
        _headerPanel.Controls.Add(_lblRecordingInfo);
        _headerPanel.Dock = DockStyle.Fill;
        _headerPanel.Location = new Point(3, 3);
        _headerPanel.Name = "_headerPanel";
        _headerPanel.Padding = new Padding(10);
        _headerPanel.Size = new Size(794, 58);
        _headerPanel.TabIndex = 0;

        // _lblRecordingInfo
        _lblRecordingInfo.AutoSize = true;
        _lblRecordingInfo.Font = new Font("Segoe UI", 11F, FontStyle.Regular, GraphicsUnit.Point);
        _lblRecordingInfo.Location = new Point(13, 13);
        _lblRecordingInfo.Name = "_lblRecordingInfo";
        _lblRecordingInfo.Size = new Size(163, 20);
        _lblRecordingInfo.TabIndex = 0;
        _lblRecordingInfo.Text = "No recording available";

        // _btnExport
        _btnExport.Anchor = AnchorStyles.Top | AnchorStyles.Right;
        _btnExport.Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point);
        _btnExport.Location = new Point(666, 10);
        _btnExport.MinimumSize = new Size(100, 35);
        _btnExport.Name = "_btnExport";
        _btnExport.Size = new Size(115, 35);
        _btnExport.TabIndex = 1;
        _btnExport.Text = "Export CSV";
        _btnExport.UseVisualStyleBackColor = true;

        // _tabControl
        _tabControl.Controls.Add(_tabDetails);
        _tabControl.Controls.Add(_tabSummary);
        _tabControl.Dock = DockStyle.Fill;
        _tabControl.Location = new Point(3, 67);
        _tabControl.Name = "_tabControl";
        _tabControl.SelectedIndex = 0;
        _tabControl.Size = new Size(794, 430);
        _tabControl.TabIndex = 1;

        // _tabDetails
        _tabDetails.Controls.Add(_dgvDetails);
        _tabDetails.Location = new Point(4, 24);
        _tabDetails.Name = "_tabDetails";
        _tabDetails.Padding = new Padding(3);
        _tabDetails.Size = new Size(786, 402);
        _tabDetails.TabIndex = 0;
        _tabDetails.Text = "Details";
        _tabDetails.UseVisualStyleBackColor = true;

        // _dgvDetails
        _dgvDetails.AllowUserToAddRows = false;
        _dgvDetails.AllowUserToDeleteRows = false;
        _dgvDetails.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        _dgvDetails.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
        _dgvDetails.Dock = DockStyle.Fill;
        _dgvDetails.Location = new Point(3, 3);
        _dgvDetails.Name = "_dgvDetails";
        _dgvDetails.ReadOnly = true;
        _dgvDetails.RowHeadersWidth = 30;
        _dgvDetails.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        _dgvDetails.Size = new Size(780, 396);
        _dgvDetails.TabIndex = 0;

        // _tabSummary
        _tabSummary.Controls.Add(_dgvSummary);
        _tabSummary.Location = new Point(4, 24);
        _tabSummary.Name = "_tabSummary";
        _tabSummary.Padding = new Padding(3);
        _tabSummary.Size = new Size(786, 402);
        _tabSummary.TabIndex = 1;
        _tabSummary.Text = "Summary";
        _tabSummary.UseVisualStyleBackColor = true;

        // _dgvSummary
        _dgvSummary.AllowUserToAddRows = false;
        _dgvSummary.AllowUserToDeleteRows = false;
        _dgvSummary.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        _dgvSummary.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
        _dgvSummary.Dock = DockStyle.Fill;
        _dgvSummary.Location = new Point(3, 3);
        _dgvSummary.Name = "_dgvSummary";
        _dgvSummary.ReadOnly = true;
        _dgvSummary.RowHeadersWidth = 30;
        _dgvSummary.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        _dgvSummary.Size = new Size(780, 396);
        _dgvSummary.TabIndex = 0;

        // ResultsControl
        AutoScaleDimensions = new SizeF(7F, 15F);
        AutoScaleMode = AutoScaleMode.Font;
        Controls.Add(_tableLayoutPanel);
        Name = "ResultsControl";
        Size = new Size(800, 500);

        _tableLayoutPanel.ResumeLayout(false);
        _tableLayoutPanel.PerformLayout();
        _headerPanel.ResumeLayout(false);
        _headerPanel.PerformLayout();
        _tabControl.ResumeLayout(false);
        _tabDetails.ResumeLayout(false);
        _tabSummary.ResumeLayout(false);
        ((System.ComponentModel.ISupportInitialize)_dgvDetails).EndInit();
        ((System.ComponentModel.ISupportInitialize)_dgvSummary).EndInit();
        ResumeLayout(false);
    }

    private TableLayoutPanel _tableLayoutPanel;
    private Panel _headerPanel;
    private Label _lblRecordingInfo;
    private Button _btnExport;
    private TabControl _tabControl;
    private TabPage _tabDetails;
    private DataGridView _dgvDetails;
    private TabPage _tabSummary;
    private DataGridView _dgvSummary;
}
