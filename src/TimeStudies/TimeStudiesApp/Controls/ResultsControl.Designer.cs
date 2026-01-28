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
        _tableLayoutMain = new TableLayoutPanel();
        _grpRecordings = new GroupBox();
        _listRecordings = new ListBox();
        _splitContainer = new SplitContainer();
        _grpDetails = new GroupBox();
        _dataGridDetails = new DataGridView();
        _colSeq = new DataGridViewTextBoxColumn();
        _colOrderNumber = new DataGridViewTextBoxColumn();
        _colDescription = new DataGridViewTextBoxColumn();
        _colTimestamp = new DataGridViewTextBoxColumn();
        _colElapsed = new DataGridViewTextBoxColumn();
        _colDuration = new DataGridViewTextBoxColumn();
        _grpSummary = new GroupBox();
        _dataGridSummary = new DataGridView();
        _colSumOrderNumber = new DataGridViewTextBoxColumn();
        _colSumDescription = new DataGridViewTextBoxColumn();
        _colSumCount = new DataGridViewTextBoxColumn();
        _colSumTotalDuration = new DataGridViewTextBoxColumn();
        _colSumAvgDuration = new DataGridViewTextBoxColumn();

        _tableLayoutMain.SuspendLayout();
        _grpRecordings.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)_splitContainer).BeginInit();
        _splitContainer.Panel1.SuspendLayout();
        _splitContainer.Panel2.SuspendLayout();
        _splitContainer.SuspendLayout();
        _grpDetails.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)_dataGridDetails).BeginInit();
        _grpSummary.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)_dataGridSummary).BeginInit();
        SuspendLayout();

        // _tableLayoutMain
        _tableLayoutMain.ColumnCount = 2;
        _tableLayoutMain.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 250F));
        _tableLayoutMain.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
        _tableLayoutMain.Controls.Add(_grpRecordings, 0, 0);
        _tableLayoutMain.Controls.Add(_splitContainer, 1, 0);
        _tableLayoutMain.Dock = DockStyle.Fill;
        _tableLayoutMain.Location = new Point(0, 0);
        _tableLayoutMain.Name = "_tableLayoutMain";
        _tableLayoutMain.Padding = new Padding(6);
        _tableLayoutMain.RowCount = 1;
        _tableLayoutMain.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
        _tableLayoutMain.Size = new Size(900, 600);
        _tableLayoutMain.TabIndex = 0;

        // _grpRecordings
        _grpRecordings.Controls.Add(_listRecordings);
        _grpRecordings.Dock = DockStyle.Fill;
        _grpRecordings.Location = new Point(9, 9);
        _grpRecordings.Name = "_grpRecordings";
        _grpRecordings.Padding = new Padding(6);
        _grpRecordings.Size = new Size(244, 582);
        _grpRecordings.TabIndex = 0;
        _grpRecordings.TabStop = false;
        _grpRecordings.Text = "Recordings";

        // _listRecordings
        _listRecordings.Dock = DockStyle.Fill;
        _listRecordings.FormattingEnabled = true;
        _listRecordings.ItemHeight = 20;
        _listRecordings.Location = new Point(6, 26);
        _listRecordings.Name = "_listRecordings";
        _listRecordings.Size = new Size(232, 550);
        _listRecordings.TabIndex = 0;
        _listRecordings.SelectedIndexChanged += ListRecordings_SelectedIndexChanged;

        // _splitContainer
        _splitContainer.Dock = DockStyle.Fill;
        _splitContainer.Location = new Point(259, 9);
        _splitContainer.Name = "_splitContainer";
        _splitContainer.Orientation = Orientation.Horizontal;

        // _splitContainer.Panel1
        _splitContainer.Panel1.Controls.Add(_grpDetails);

        // _splitContainer.Panel2
        _splitContainer.Panel2.Controls.Add(_grpSummary);
        _splitContainer.Size = new Size(632, 582);
        _splitContainer.SplitterDistance = 350;
        _splitContainer.TabIndex = 1;

        // _grpDetails
        _grpDetails.Controls.Add(_dataGridDetails);
        _grpDetails.Dock = DockStyle.Fill;
        _grpDetails.Location = new Point(0, 0);
        _grpDetails.Name = "_grpDetails";
        _grpDetails.Padding = new Padding(6);
        _grpDetails.Size = new Size(632, 350);
        _grpDetails.TabIndex = 0;
        _grpDetails.TabStop = false;
        _grpDetails.Text = "Time Entries";

        // _dataGridDetails
        _dataGridDetails.AllowUserToAddRows = false;
        _dataGridDetails.AllowUserToDeleteRows = false;
        _dataGridDetails.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        _dataGridDetails.BackgroundColor = SystemColors.Window;
        _dataGridDetails.BorderStyle = BorderStyle.Fixed3D;
        _dataGridDetails.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
        _dataGridDetails.Columns.AddRange(new DataGridViewColumn[] { _colSeq, _colOrderNumber, _colDescription, _colTimestamp, _colElapsed, _colDuration });
        _dataGridDetails.Dock = DockStyle.Fill;
        _dataGridDetails.Location = new Point(6, 26);
        _dataGridDetails.Name = "_dataGridDetails";
        _dataGridDetails.ReadOnly = true;
        _dataGridDetails.RowHeadersWidth = 30;
        _dataGridDetails.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        _dataGridDetails.Size = new Size(620, 318);
        _dataGridDetails.TabIndex = 0;

        // _colSeq
        _colSeq.FillWeight = 40F;
        _colSeq.HeaderText = "#";
        _colSeq.MinimumWidth = 40;
        _colSeq.Name = "_colSeq";
        _colSeq.ReadOnly = true;

        // _colOrderNumber
        _colOrderNumber.FillWeight = 50F;
        _colOrderNumber.HeaderText = "Order #";
        _colOrderNumber.MinimumWidth = 50;
        _colOrderNumber.Name = "_colOrderNumber";
        _colOrderNumber.ReadOnly = true;

        // _colDescription
        _colDescription.FillWeight = 150F;
        _colDescription.HeaderText = "Description";
        _colDescription.MinimumWidth = 100;
        _colDescription.Name = "_colDescription";
        _colDescription.ReadOnly = true;

        // _colTimestamp
        _colTimestamp.FillWeight = 100F;
        _colTimestamp.HeaderText = "Timestamp";
        _colTimestamp.MinimumWidth = 80;
        _colTimestamp.Name = "_colTimestamp";
        _colTimestamp.ReadOnly = true;

        // _colElapsed
        _colElapsed.FillWeight = 70F;
        _colElapsed.HeaderText = "Elapsed";
        _colElapsed.MinimumWidth = 60;
        _colElapsed.Name = "_colElapsed";
        _colElapsed.ReadOnly = true;

        // _colDuration
        _colDuration.FillWeight = 70F;
        _colDuration.HeaderText = "Duration";
        _colDuration.MinimumWidth = 60;
        _colDuration.Name = "_colDuration";
        _colDuration.ReadOnly = true;

        // _grpSummary
        _grpSummary.Controls.Add(_dataGridSummary);
        _grpSummary.Dock = DockStyle.Fill;
        _grpSummary.Location = new Point(0, 0);
        _grpSummary.Name = "_grpSummary";
        _grpSummary.Padding = new Padding(6);
        _grpSummary.Size = new Size(632, 228);
        _grpSummary.TabIndex = 0;
        _grpSummary.TabStop = false;
        _grpSummary.Text = "Summary";

        // _dataGridSummary
        _dataGridSummary.AllowUserToAddRows = false;
        _dataGridSummary.AllowUserToDeleteRows = false;
        _dataGridSummary.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        _dataGridSummary.BackgroundColor = SystemColors.Window;
        _dataGridSummary.BorderStyle = BorderStyle.Fixed3D;
        _dataGridSummary.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
        _dataGridSummary.Columns.AddRange(new DataGridViewColumn[] { _colSumOrderNumber, _colSumDescription, _colSumCount, _colSumTotalDuration, _colSumAvgDuration });
        _dataGridSummary.Dock = DockStyle.Fill;
        _dataGridSummary.Location = new Point(6, 26);
        _dataGridSummary.Name = "_dataGridSummary";
        _dataGridSummary.ReadOnly = true;
        _dataGridSummary.RowHeadersWidth = 30;
        _dataGridSummary.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        _dataGridSummary.Size = new Size(620, 196);
        _dataGridSummary.TabIndex = 0;

        // _colSumOrderNumber
        _colSumOrderNumber.FillWeight = 50F;
        _colSumOrderNumber.HeaderText = "Order #";
        _colSumOrderNumber.MinimumWidth = 50;
        _colSumOrderNumber.Name = "_colSumOrderNumber";
        _colSumOrderNumber.ReadOnly = true;

        // _colSumDescription
        _colSumDescription.FillWeight = 150F;
        _colSumDescription.HeaderText = "Description";
        _colSumDescription.MinimumWidth = 100;
        _colSumDescription.Name = "_colSumDescription";
        _colSumDescription.ReadOnly = true;

        // _colSumCount
        _colSumCount.FillWeight = 50F;
        _colSumCount.HeaderText = "Count";
        _colSumCount.MinimumWidth = 50;
        _colSumCount.Name = "_colSumCount";
        _colSumCount.ReadOnly = true;

        // _colSumTotalDuration
        _colSumTotalDuration.FillWeight = 80F;
        _colSumTotalDuration.HeaderText = "Total Duration";
        _colSumTotalDuration.MinimumWidth = 70;
        _colSumTotalDuration.Name = "_colSumTotalDuration";
        _colSumTotalDuration.ReadOnly = true;

        // _colSumAvgDuration
        _colSumAvgDuration.FillWeight = 80F;
        _colSumAvgDuration.HeaderText = "Avg Duration";
        _colSumAvgDuration.MinimumWidth = 70;
        _colSumAvgDuration.Name = "_colSumAvgDuration";
        _colSumAvgDuration.ReadOnly = true;

        // ResultsControl
        AutoScaleDimensions = new SizeF(8F, 20F);
        AutoScaleMode = AutoScaleMode.Font;
        Controls.Add(_tableLayoutMain);
        Name = "ResultsControl";
        Size = new Size(900, 600);
        _tableLayoutMain.ResumeLayout(false);
        _grpRecordings.ResumeLayout(false);
        _splitContainer.Panel1.ResumeLayout(false);
        _splitContainer.Panel2.ResumeLayout(false);
        ((System.ComponentModel.ISupportInitialize)_splitContainer).EndInit();
        _splitContainer.ResumeLayout(false);
        _grpDetails.ResumeLayout(false);
        ((System.ComponentModel.ISupportInitialize)_dataGridDetails).EndInit();
        _grpSummary.ResumeLayout(false);
        ((System.ComponentModel.ISupportInitialize)_dataGridSummary).EndInit();
        ResumeLayout(false);
    }

    #endregion

    private TableLayoutPanel _tableLayoutMain;
    private GroupBox _grpRecordings;
    private ListBox _listRecordings;
    private SplitContainer _splitContainer;
    private GroupBox _grpDetails;
    private DataGridView _dataGridDetails;
    private DataGridViewTextBoxColumn _colSeq;
    private DataGridViewTextBoxColumn _colOrderNumber;
    private DataGridViewTextBoxColumn _colDescription;
    private DataGridViewTextBoxColumn _colTimestamp;
    private DataGridViewTextBoxColumn _colElapsed;
    private DataGridViewTextBoxColumn _colDuration;
    private GroupBox _grpSummary;
    private DataGridView _dataGridSummary;
    private DataGridViewTextBoxColumn _colSumOrderNumber;
    private DataGridViewTextBoxColumn _colSumDescription;
    private DataGridViewTextBoxColumn _colSumCount;
    private DataGridViewTextBoxColumn _colSumTotalDuration;
    private DataGridViewTextBoxColumn _colSumAvgDuration;
}
