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

    #region Component Designer generated code

    private void InitializeComponent()
    {
        components = new System.ComponentModel.Container();

        _tlpMain = new TableLayoutPanel();
        _pnlHeader = new TableLayoutPanel();
        _lblDefinitionCaption = new Label();
        _lblDefinition = new Label();
        _lblStartedCaption = new Label();
        _lblStarted = new Label();
        _lblCompletedCaption = new Label();
        _lblCompleted = new Label();
        _lblTotalDurationCaption = new Label();
        _lblTotalDuration = new Label();
        _lblEntriesCaption = new Label();
        _lblEntries = new Label();
        _tabControl = new TabControl();
        _tabDetail = new TabPage();
        _tabSummary = new TabPage();
        _dgvDetail = new DataGridView();
        _dgvSummary = new DataGridView();

        _tlpMain.SuspendLayout();
        _pnlHeader.SuspendLayout();
        _tabControl.SuspendLayout();
        _tabDetail.SuspendLayout();
        _tabSummary.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)_dgvDetail).BeginInit();
        ((System.ComponentModel.ISupportInitialize)_dgvSummary).BeginInit();
        SuspendLayout();

        // _tlpMain
        _tlpMain.ColumnCount = 1;
        _tlpMain.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
        _tlpMain.Controls.Add(_pnlHeader, 0, 0);
        _tlpMain.Controls.Add(_tabControl, 0, 1);
        _tlpMain.Dock = DockStyle.Fill;
        _tlpMain.Location = new Point(0, 0);
        _tlpMain.Name = "_tlpMain";
        _tlpMain.RowCount = 2;
        _tlpMain.RowStyles.Add(new RowStyle(SizeType.AutoSize));
        _tlpMain.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
        _tlpMain.Size = new Size(800, 500);
        _tlpMain.TabIndex = 0;

        // _pnlHeader
        _pnlHeader.AutoSize = true;
        _pnlHeader.AutoSizeMode = AutoSizeMode.GrowAndShrink;
        _pnlHeader.BackColor = SystemColors.ControlLight;
        _pnlHeader.ColumnCount = 10;
        _pnlHeader.ColumnStyles.Add(new ColumnStyle(SizeType.AutoSize));
        _pnlHeader.ColumnStyles.Add(new ColumnStyle(SizeType.AutoSize));
        _pnlHeader.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 20F));
        _pnlHeader.ColumnStyles.Add(new ColumnStyle(SizeType.AutoSize));
        _pnlHeader.ColumnStyles.Add(new ColumnStyle(SizeType.AutoSize));
        _pnlHeader.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 20F));
        _pnlHeader.ColumnStyles.Add(new ColumnStyle(SizeType.AutoSize));
        _pnlHeader.ColumnStyles.Add(new ColumnStyle(SizeType.AutoSize));
        _pnlHeader.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
        _pnlHeader.ColumnStyles.Add(new ColumnStyle(SizeType.AutoSize));
        _pnlHeader.Controls.Add(_lblDefinitionCaption, 0, 0);
        _pnlHeader.Controls.Add(_lblDefinition, 1, 0);
        _pnlHeader.Controls.Add(_lblStartedCaption, 3, 0);
        _pnlHeader.Controls.Add(_lblStarted, 4, 0);
        _pnlHeader.Controls.Add(_lblCompletedCaption, 6, 0);
        _pnlHeader.Controls.Add(_lblCompleted, 7, 0);
        _pnlHeader.Controls.Add(_lblTotalDurationCaption, 0, 1);
        _pnlHeader.Controls.Add(_lblTotalDuration, 1, 1);
        _pnlHeader.Controls.Add(_lblEntriesCaption, 3, 1);
        _pnlHeader.Controls.Add(_lblEntries, 4, 1);
        _pnlHeader.Dock = DockStyle.Fill;
        _pnlHeader.Location = new Point(3, 3);
        _pnlHeader.Name = "_pnlHeader";
        _pnlHeader.Padding = new Padding(8);
        _pnlHeader.RowCount = 2;
        _pnlHeader.RowStyles.Add(new RowStyle(SizeType.AutoSize));
        _pnlHeader.RowStyles.Add(new RowStyle(SizeType.AutoSize));
        _pnlHeader.Size = new Size(794, 68);
        _pnlHeader.TabIndex = 0;

        // _lblDefinitionCaption
        _lblDefinitionCaption.Anchor = AnchorStyles.Left;
        _lblDefinitionCaption.AutoSize = true;
        _lblDefinitionCaption.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point);
        _lblDefinitionCaption.Location = new Point(11, 11);
        _lblDefinitionCaption.Margin = new Padding(3);
        _lblDefinitionCaption.Name = "_lblDefinitionCaption";
        _lblDefinitionCaption.Size = new Size(80, 20);
        _lblDefinitionCaption.TabIndex = 0;
        _lblDefinitionCaption.Text = "Definition:";

        // _lblDefinition
        _lblDefinition.Anchor = AnchorStyles.Left;
        _lblDefinition.AutoSize = true;
        _lblDefinition.Location = new Point(97, 11);
        _lblDefinition.Margin = new Padding(3);
        _lblDefinition.Name = "_lblDefinition";
        _lblDefinition.Size = new Size(14, 20);
        _lblDefinition.TabIndex = 1;
        _lblDefinition.Text = "-";

        // _lblStartedCaption
        _lblStartedCaption.Anchor = AnchorStyles.Left;
        _lblStartedCaption.AutoSize = true;
        _lblStartedCaption.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point);
        _lblStartedCaption.Location = new Point(137, 11);
        _lblStartedCaption.Margin = new Padding(3);
        _lblStartedCaption.Name = "_lblStartedCaption";
        _lblStartedCaption.Size = new Size(60, 20);
        _lblStartedCaption.TabIndex = 2;
        _lblStartedCaption.Text = "Started:";

        // _lblStarted
        _lblStarted.Anchor = AnchorStyles.Left;
        _lblStarted.AutoSize = true;
        _lblStarted.Location = new Point(203, 11);
        _lblStarted.Margin = new Padding(3);
        _lblStarted.Name = "_lblStarted";
        _lblStarted.Size = new Size(14, 20);
        _lblStarted.TabIndex = 3;
        _lblStarted.Text = "-";

        // _lblCompletedCaption
        _lblCompletedCaption.Anchor = AnchorStyles.Left;
        _lblCompletedCaption.AutoSize = true;
        _lblCompletedCaption.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point);
        _lblCompletedCaption.Location = new Point(243, 11);
        _lblCompletedCaption.Margin = new Padding(3);
        _lblCompletedCaption.Name = "_lblCompletedCaption";
        _lblCompletedCaption.Size = new Size(84, 20);
        _lblCompletedCaption.TabIndex = 4;
        _lblCompletedCaption.Text = "Completed:";

        // _lblCompleted
        _lblCompleted.Anchor = AnchorStyles.Left;
        _lblCompleted.AutoSize = true;
        _lblCompleted.Location = new Point(333, 11);
        _lblCompleted.Margin = new Padding(3);
        _lblCompleted.Name = "_lblCompleted";
        _lblCompleted.Size = new Size(14, 20);
        _lblCompleted.TabIndex = 5;
        _lblCompleted.Text = "-";

        // _lblTotalDurationCaption
        _lblTotalDurationCaption.Anchor = AnchorStyles.Left;
        _lblTotalDurationCaption.AutoSize = true;
        _lblTotalDurationCaption.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point);
        _lblTotalDurationCaption.Location = new Point(11, 37);
        _lblTotalDurationCaption.Margin = new Padding(3);
        _lblTotalDurationCaption.Name = "_lblTotalDurationCaption";
        _lblTotalDurationCaption.Size = new Size(108, 20);
        _lblTotalDurationCaption.TabIndex = 6;
        _lblTotalDurationCaption.Text = "Total Duration:";

        // _lblTotalDuration
        _lblTotalDuration.Anchor = AnchorStyles.Left;
        _lblTotalDuration.AutoSize = true;
        _lblTotalDuration.Font = new Font("Consolas", 10F, FontStyle.Regular, GraphicsUnit.Point);
        _lblTotalDuration.Location = new Point(125, 37);
        _lblTotalDuration.Margin = new Padding(3);
        _lblTotalDuration.Name = "_lblTotalDuration";
        _lblTotalDuration.Size = new Size(16, 22);
        _lblTotalDuration.TabIndex = 7;
        _lblTotalDuration.Text = "-";

        // _lblEntriesCaption
        _lblEntriesCaption.Anchor = AnchorStyles.Left;
        _lblEntriesCaption.AutoSize = true;
        _lblEntriesCaption.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point);
        _lblEntriesCaption.Location = new Point(167, 38);
        _lblEntriesCaption.Margin = new Padding(3);
        _lblEntriesCaption.Name = "_lblEntriesCaption";
        _lblEntriesCaption.Size = new Size(58, 20);
        _lblEntriesCaption.TabIndex = 8;
        _lblEntriesCaption.Text = "Entries:";

        // _lblEntries
        _lblEntries.Anchor = AnchorStyles.Left;
        _lblEntries.AutoSize = true;
        _lblEntries.Location = new Point(231, 38);
        _lblEntries.Margin = new Padding(3);
        _lblEntries.Name = "_lblEntries";
        _lblEntries.Size = new Size(14, 20);
        _lblEntries.TabIndex = 9;
        _lblEntries.Text = "-";

        // _tabControl
        _tabControl.Controls.Add(_tabDetail);
        _tabControl.Controls.Add(_tabSummary);
        _tabControl.Dock = DockStyle.Fill;
        _tabControl.Location = new Point(3, 77);
        _tabControl.Name = "_tabControl";
        _tabControl.SelectedIndex = 0;
        _tabControl.Size = new Size(794, 420);
        _tabControl.TabIndex = 1;

        // _tabDetail
        _tabDetail.Controls.Add(_dgvDetail);
        _tabDetail.Location = new Point(4, 29);
        _tabDetail.Name = "_tabDetail";
        _tabDetail.Size = new Size(786, 387);
        _tabDetail.TabIndex = 0;
        _tabDetail.Text = "Detail";
        _tabDetail.UseVisualStyleBackColor = true;

        // _tabSummary
        _tabSummary.Controls.Add(_dgvSummary);
        _tabSummary.Location = new Point(4, 29);
        _tabSummary.Name = "_tabSummary";
        _tabSummary.Size = new Size(786, 387);
        _tabSummary.TabIndex = 1;
        _tabSummary.Text = "Summary";
        _tabSummary.UseVisualStyleBackColor = true;

        // _dgvDetail
        _dgvDetail.AllowUserToAddRows = false;
        _dgvDetail.AllowUserToDeleteRows = false;
        _dgvDetail.BackgroundColor = SystemColors.Window;
        _dgvDetail.BorderStyle = BorderStyle.None;
        _dgvDetail.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
        _dgvDetail.Dock = DockStyle.Fill;
        _dgvDetail.Location = new Point(0, 0);
        _dgvDetail.Name = "_dgvDetail";
        _dgvDetail.ReadOnly = true;
        _dgvDetail.RowHeadersWidth = 51;
        _dgvDetail.Size = new Size(786, 387);
        _dgvDetail.TabIndex = 0;

        // _dgvSummary
        _dgvSummary.AllowUserToAddRows = false;
        _dgvSummary.AllowUserToDeleteRows = false;
        _dgvSummary.BackgroundColor = SystemColors.Window;
        _dgvSummary.BorderStyle = BorderStyle.None;
        _dgvSummary.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
        _dgvSummary.Dock = DockStyle.Fill;
        _dgvSummary.Location = new Point(0, 0);
        _dgvSummary.Name = "_dgvSummary";
        _dgvSummary.ReadOnly = true;
        _dgvSummary.RowHeadersWidth = 51;
        _dgvSummary.Size = new Size(786, 387);
        _dgvSummary.TabIndex = 0;

        // ResultsControl
        AutoScaleDimensions = new SizeF(8F, 20F);
        AutoScaleMode = AutoScaleMode.Font;
        Controls.Add(_tlpMain);
        Name = "ResultsControl";
        Size = new Size(800, 500);

        _tlpMain.ResumeLayout(false);
        _tlpMain.PerformLayout();
        _pnlHeader.ResumeLayout(false);
        _pnlHeader.PerformLayout();
        _tabControl.ResumeLayout(false);
        _tabDetail.ResumeLayout(false);
        _tabSummary.ResumeLayout(false);
        ((System.ComponentModel.ISupportInitialize)_dgvDetail).EndInit();
        ((System.ComponentModel.ISupportInitialize)_dgvSummary).EndInit();
        ResumeLayout(false);
    }

    #endregion

    private TableLayoutPanel _tlpMain;
    private TableLayoutPanel _pnlHeader;
    private Label _lblDefinitionCaption;
    private Label _lblDefinition;
    private Label _lblStartedCaption;
    private Label _lblStarted;
    private Label _lblCompletedCaption;
    private Label _lblCompleted;
    private Label _lblTotalDurationCaption;
    private Label _lblTotalDuration;
    private Label _lblEntriesCaption;
    private Label _lblEntries;
    private TabControl _tabControl;
    private TabPage _tabDetail;
    private TabPage _tabSummary;
    private DataGridView _dgvDetail;
    private DataGridView _dgvSummary;
}
