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
        _tableLayoutPanel = new TableLayoutPanel();
        _pnlHeader = new TableLayoutPanel();
        _lblDefinitionNameCaption = new Label();
        _lblDefinitionName = new Label();
        _lblStartTimeCaption = new Label();
        _lblStartTime = new Label();
        _lblEndTimeCaption = new Label();
        _lblEndTime = new Label();
        _lblTotalDurationCaption = new Label();
        _lblTotalDuration = new Label();
        _lblEntryCountCaption = new Label();
        _lblEntryCount = new Label();
        _splitContainer = new SplitContainer();
        _pnlDetail = new Panel();
        _dgvDetail = new DataGridView();
        _lblDetailTitle = new Label();
        _pnlSummary = new Panel();
        _dgvSummary = new DataGridView();
        _lblSummaryTitle = new Label();
        _tableLayoutPanel.SuspendLayout();
        _pnlHeader.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)_splitContainer).BeginInit();
        _splitContainer.Panel1.SuspendLayout();
        _splitContainer.Panel2.SuspendLayout();
        _splitContainer.SuspendLayout();
        _pnlDetail.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)_dgvDetail).BeginInit();
        _pnlSummary.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)_dgvSummary).BeginInit();
        SuspendLayout();
        // 
        // _tableLayoutPanel
        // 
        _tableLayoutPanel.ColumnCount = 1;
        _tableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
        _tableLayoutPanel.Controls.Add(_pnlHeader, 0, 0);
        _tableLayoutPanel.Controls.Add(_splitContainer, 0, 1);
        _tableLayoutPanel.Dock = DockStyle.Fill;
        _tableLayoutPanel.Location = new Point(0, 0);
        _tableLayoutPanel.Name = "_tableLayoutPanel";
        _tableLayoutPanel.Padding = new Padding(10);
        _tableLayoutPanel.RowCount = 2;
        _tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.AutoSize));
        _tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
        _tableLayoutPanel.Size = new Size(900, 600);
        _tableLayoutPanel.TabIndex = 0;
        // 
        // _pnlHeader
        // 
        _pnlHeader.AutoSize = true;
        _pnlHeader.ColumnCount = 10;
        _pnlHeader.ColumnStyles.Add(new ColumnStyle(SizeType.AutoSize));
        _pnlHeader.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));
        _pnlHeader.ColumnStyles.Add(new ColumnStyle(SizeType.AutoSize));
        _pnlHeader.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));
        _pnlHeader.ColumnStyles.Add(new ColumnStyle(SizeType.AutoSize));
        _pnlHeader.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));
        _pnlHeader.ColumnStyles.Add(new ColumnStyle(SizeType.AutoSize));
        _pnlHeader.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 12.5F));
        _pnlHeader.ColumnStyles.Add(new ColumnStyle(SizeType.AutoSize));
        _pnlHeader.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 12.5F));
        _pnlHeader.Controls.Add(_lblDefinitionNameCaption, 0, 0);
        _pnlHeader.Controls.Add(_lblDefinitionName, 1, 0);
        _pnlHeader.Controls.Add(_lblStartTimeCaption, 2, 0);
        _pnlHeader.Controls.Add(_lblStartTime, 3, 0);
        _pnlHeader.Controls.Add(_lblEndTimeCaption, 4, 0);
        _pnlHeader.Controls.Add(_lblEndTime, 5, 0);
        _pnlHeader.Controls.Add(_lblTotalDurationCaption, 6, 0);
        _pnlHeader.Controls.Add(_lblTotalDuration, 7, 0);
        _pnlHeader.Controls.Add(_lblEntryCountCaption, 8, 0);
        _pnlHeader.Controls.Add(_lblEntryCount, 9, 0);
        _pnlHeader.Dock = DockStyle.Fill;
        _pnlHeader.Location = new Point(13, 13);
        _pnlHeader.Name = "_pnlHeader";
        _pnlHeader.Padding = new Padding(0, 0, 0, 10);
        _pnlHeader.RowCount = 1;
        _pnlHeader.RowStyles.Add(new RowStyle(SizeType.AutoSize));
        _pnlHeader.Size = new Size(874, 40);
        _pnlHeader.TabIndex = 0;
        // 
        // _lblDefinitionNameCaption
        // 
        _lblDefinitionNameCaption.Anchor = AnchorStyles.Left | AnchorStyles.Right;
        _lblDefinitionNameCaption.AutoSize = true;
        _lblDefinitionNameCaption.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
        _lblDefinitionNameCaption.Location = new Point(3, 5);
        _lblDefinitionNameCaption.Margin = new Padding(3, 0, 5, 0);
        _lblDefinitionNameCaption.Name = "_lblDefinitionNameCaption";
        _lblDefinitionNameCaption.Size = new Size(74, 20);
        _lblDefinitionNameCaption.TabIndex = 0;
        _lblDefinitionNameCaption.Text = "Definition:";
        // 
        // _lblDefinitionName
        // 
        _lblDefinitionName.Anchor = AnchorStyles.Left | AnchorStyles.Right;
        _lblDefinitionName.AutoSize = true;
        _lblDefinitionName.Location = new Point(85, 5);
        _lblDefinitionName.Margin = new Padding(3, 0, 10, 0);
        _lblDefinitionName.Name = "_lblDefinitionName";
        _lblDefinitionName.Size = new Size(113, 20);
        _lblDefinitionName.TabIndex = 1;
        _lblDefinitionName.Text = "-";
        // 
        // _lblStartTimeCaption
        // 
        _lblStartTimeCaption.Anchor = AnchorStyles.Left | AnchorStyles.Right;
        _lblStartTimeCaption.AutoSize = true;
        _lblStartTimeCaption.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
        _lblStartTimeCaption.Location = new Point(211, 5);
        _lblStartTimeCaption.Margin = new Padding(3, 0, 5, 0);
        _lblStartTimeCaption.Name = "_lblStartTimeCaption";
        _lblStartTimeCaption.Size = new Size(42, 20);
        _lblStartTimeCaption.TabIndex = 2;
        _lblStartTimeCaption.Text = "Start:";
        // 
        // _lblStartTime
        // 
        _lblStartTime.Anchor = AnchorStyles.Left | AnchorStyles.Right;
        _lblStartTime.AutoSize = true;
        _lblStartTime.Location = new Point(261, 5);
        _lblStartTime.Margin = new Padding(3, 0, 10, 0);
        _lblStartTime.Name = "_lblStartTime";
        _lblStartTime.Size = new Size(113, 20);
        _lblStartTime.TabIndex = 3;
        _lblStartTime.Text = "-";
        // 
        // _lblEndTimeCaption
        // 
        _lblEndTimeCaption.Anchor = AnchorStyles.Left | AnchorStyles.Right;
        _lblEndTimeCaption.AutoSize = true;
        _lblEndTimeCaption.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
        _lblEndTimeCaption.Location = new Point(387, 5);
        _lblEndTimeCaption.Margin = new Padding(3, 0, 5, 0);
        _lblEndTimeCaption.Name = "_lblEndTimeCaption";
        _lblEndTimeCaption.Size = new Size(34, 20);
        _lblEndTimeCaption.TabIndex = 4;
        _lblEndTimeCaption.Text = "End:";
        // 
        // _lblEndTime
        // 
        _lblEndTime.Anchor = AnchorStyles.Left | AnchorStyles.Right;
        _lblEndTime.AutoSize = true;
        _lblEndTime.Location = new Point(429, 5);
        _lblEndTime.Margin = new Padding(3, 0, 10, 0);
        _lblEndTime.Name = "_lblEndTime";
        _lblEndTime.Size = new Size(113, 20);
        _lblEndTime.TabIndex = 5;
        _lblEndTime.Text = "-";
        // 
        // _lblTotalDurationCaption
        // 
        _lblTotalDurationCaption.Anchor = AnchorStyles.Left | AnchorStyles.Right;
        _lblTotalDurationCaption.AutoSize = true;
        _lblTotalDurationCaption.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
        _lblTotalDurationCaption.Location = new Point(555, 5);
        _lblTotalDurationCaption.Margin = new Padding(3, 0, 5, 0);
        _lblTotalDurationCaption.Name = "_lblTotalDurationCaption";
        _lblTotalDurationCaption.Size = new Size(43, 20);
        _lblTotalDurationCaption.TabIndex = 6;
        _lblTotalDurationCaption.Text = "Total:";
        // 
        // _lblTotalDuration
        // 
        _lblTotalDuration.Anchor = AnchorStyles.Left | AnchorStyles.Right;
        _lblTotalDuration.AutoSize = true;
        _lblTotalDuration.Location = new Point(606, 5);
        _lblTotalDuration.Margin = new Padding(3, 0, 10, 0);
        _lblTotalDuration.Name = "_lblTotalDuration";
        _lblTotalDuration.Size = new Size(50, 20);
        _lblTotalDuration.TabIndex = 7;
        _lblTotalDuration.Text = "-";
        // 
        // _lblEntryCountCaption
        // 
        _lblEntryCountCaption.Anchor = AnchorStyles.Left | AnchorStyles.Right;
        _lblEntryCountCaption.AutoSize = true;
        _lblEntryCountCaption.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
        _lblEntryCountCaption.Location = new Point(669, 5);
        _lblEntryCountCaption.Margin = new Padding(3, 0, 5, 0);
        _lblEntryCountCaption.Name = "_lblEntryCountCaption";
        _lblEntryCountCaption.Size = new Size(53, 20);
        _lblEntryCountCaption.TabIndex = 8;
        _lblEntryCountCaption.Text = "Entries:";
        // 
        // _lblEntryCount
        // 
        _lblEntryCount.Anchor = AnchorStyles.Left | AnchorStyles.Right;
        _lblEntryCount.AutoSize = true;
        _lblEntryCount.Location = new Point(730, 5);
        _lblEntryCount.Name = "_lblEntryCount";
        _lblEntryCount.Size = new Size(141, 20);
        _lblEntryCount.TabIndex = 9;
        _lblEntryCount.Text = "0";
        // 
        // _splitContainer
        // 
        _splitContainer.Dock = DockStyle.Fill;
        _splitContainer.Location = new Point(13, 59);
        _splitContainer.Name = "_splitContainer";
        _splitContainer.Orientation = Orientation.Horizontal;
        // 
        // _splitContainer.Panel1
        // 
        _splitContainer.Panel1.Controls.Add(_pnlDetail);
        // 
        // _splitContainer.Panel2
        // 
        _splitContainer.Panel2.Controls.Add(_pnlSummary);
        _splitContainer.Size = new Size(874, 528);
        _splitContainer.SplitterDistance = 350;
        _splitContainer.TabIndex = 1;
        // 
        // _pnlDetail
        // 
        _pnlDetail.Controls.Add(_dgvDetail);
        _pnlDetail.Controls.Add(_lblDetailTitle);
        _pnlDetail.Dock = DockStyle.Fill;
        _pnlDetail.Location = new Point(0, 0);
        _pnlDetail.Name = "_pnlDetail";
        _pnlDetail.Size = new Size(874, 350);
        _pnlDetail.TabIndex = 0;
        // 
        // _dgvDetail
        // 
        _dgvDetail.AllowUserToAddRows = false;
        _dgvDetail.AllowUserToDeleteRows = false;
        _dgvDetail.BackgroundColor = SystemColors.Window;
        _dgvDetail.BorderStyle = BorderStyle.Fixed3D;
        _dgvDetail.Dock = DockStyle.Fill;
        _dgvDetail.Location = new Point(0, 28);
        _dgvDetail.MultiSelect = false;
        _dgvDetail.Name = "_dgvDetail";
        _dgvDetail.ReadOnly = true;
        _dgvDetail.RowHeadersWidth = 30;
        _dgvDetail.RowTemplate.Height = 24;
        _dgvDetail.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        _dgvDetail.Size = new Size(874, 322);
        _dgvDetail.TabIndex = 1;
        // 
        // _lblDetailTitle
        // 
        _lblDetailTitle.Dock = DockStyle.Top;
        _lblDetailTitle.Font = new Font("Segoe UI Semibold", 11F, FontStyle.Bold);
        _lblDetailTitle.Location = new Point(0, 0);
        _lblDetailTitle.Margin = new Padding(3, 0, 3, 5);
        _lblDetailTitle.Name = "_lblDetailTitle";
        _lblDetailTitle.Size = new Size(874, 28);
        _lblDetailTitle.TabIndex = 0;
        _lblDetailTitle.Text = "Recording Details";
        // 
        // _pnlSummary
        // 
        _pnlSummary.Controls.Add(_dgvSummary);
        _pnlSummary.Controls.Add(_lblSummaryTitle);
        _pnlSummary.Dock = DockStyle.Fill;
        _pnlSummary.Location = new Point(0, 0);
        _pnlSummary.Name = "_pnlSummary";
        _pnlSummary.Size = new Size(874, 174);
        _pnlSummary.TabIndex = 0;
        // 
        // _dgvSummary
        // 
        _dgvSummary.AllowUserToAddRows = false;
        _dgvSummary.AllowUserToDeleteRows = false;
        _dgvSummary.BackgroundColor = SystemColors.Window;
        _dgvSummary.BorderStyle = BorderStyle.Fixed3D;
        _dgvSummary.Dock = DockStyle.Fill;
        _dgvSummary.Location = new Point(0, 28);
        _dgvSummary.MultiSelect = false;
        _dgvSummary.Name = "_dgvSummary";
        _dgvSummary.ReadOnly = true;
        _dgvSummary.RowHeadersWidth = 30;
        _dgvSummary.RowTemplate.Height = 24;
        _dgvSummary.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        _dgvSummary.Size = new Size(874, 146);
        _dgvSummary.TabIndex = 1;
        // 
        // _lblSummaryTitle
        // 
        _lblSummaryTitle.Dock = DockStyle.Top;
        _lblSummaryTitle.Font = new Font("Segoe UI Semibold", 11F, FontStyle.Bold);
        _lblSummaryTitle.Location = new Point(0, 0);
        _lblSummaryTitle.Margin = new Padding(3, 0, 3, 5);
        _lblSummaryTitle.Name = "_lblSummaryTitle";
        _lblSummaryTitle.Size = new Size(874, 28);
        _lblSummaryTitle.TabIndex = 0;
        _lblSummaryTitle.Text = "Summary";
        // 
        // ResultsControl
        // 
        AutoScaleDimensions = new SizeF(8F, 20F);
        AutoScaleMode = AutoScaleMode.Font;
        Controls.Add(_tableLayoutPanel);
        Name = "ResultsControl";
        Size = new Size(900, 600);
        _tableLayoutPanel.ResumeLayout(false);
        _tableLayoutPanel.PerformLayout();
        _pnlHeader.ResumeLayout(false);
        _pnlHeader.PerformLayout();
        _splitContainer.Panel1.ResumeLayout(false);
        _splitContainer.Panel2.ResumeLayout(false);
        ((System.ComponentModel.ISupportInitialize)_splitContainer).EndInit();
        _splitContainer.ResumeLayout(false);
        _pnlDetail.ResumeLayout(false);
        ((System.ComponentModel.ISupportInitialize)_dgvDetail).EndInit();
        _pnlSummary.ResumeLayout(false);
        ((System.ComponentModel.ISupportInitialize)_dgvSummary).EndInit();
        ResumeLayout(false);
    }

    #endregion

    private TableLayoutPanel _tableLayoutPanel;
    private TableLayoutPanel _pnlHeader;
    private Label _lblDefinitionNameCaption;
    private Label _lblDefinitionName;
    private Label _lblStartTimeCaption;
    private Label _lblStartTime;
    private Label _lblEndTimeCaption;
    private Label _lblEndTime;
    private Label _lblTotalDurationCaption;
    private Label _lblTotalDuration;
    private Label _lblEntryCountCaption;
    private Label _lblEntryCount;
    private SplitContainer _splitContainer;
    private Panel _pnlDetail;
    private DataGridView _dgvDetail;
    private Label _lblDetailTitle;
    private Panel _pnlSummary;
    private DataGridView _dgvSummary;
    private Label _lblSummaryTitle;
}
