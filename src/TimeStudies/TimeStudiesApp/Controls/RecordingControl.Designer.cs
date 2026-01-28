namespace TimeStudiesApp.Controls;

partial class RecordingControl
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
        _timerUpdate = new System.Windows.Forms.Timer(components);
        _tableLayoutMain = new TableLayoutPanel();
        _tableLayoutTop = new TableLayoutPanel();
        _btnStartRecording = new Button();
        _btnStopRecording = new Button();
        _btnPause = new Button();
        _tableLayoutInfo = new TableLayoutPanel();
        _lblProgressiveTimeCaption = new Label();
        _lblProgressiveTime = new Label();
        _lblCurrentStepCaption = new Label();
        _lblCurrentStep = new Label();
        _lblEntryCountCaption = new Label();
        _lblEntryCount = new Label();
        _grpProcessSteps = new GroupBox();
        _flowLayoutSteps = new FlowLayoutPanel();
        _grpEntries = new GroupBox();
        _dataGridEntries = new DataGridView();
        _colSeq = new DataGridViewTextBoxColumn();
        _colOrderNumber = new DataGridViewTextBoxColumn();
        _colDescription = new DataGridViewTextBoxColumn();
        _colTimestamp = new DataGridViewTextBoxColumn();
        _colElapsed = new DataGridViewTextBoxColumn();
        _colDuration = new DataGridViewTextBoxColumn();

        _tableLayoutMain.SuspendLayout();
        _tableLayoutTop.SuspendLayout();
        _tableLayoutInfo.SuspendLayout();
        _grpProcessSteps.SuspendLayout();
        _grpEntries.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)_dataGridEntries).BeginInit();
        SuspendLayout();

        // _timerUpdate
        _timerUpdate.Interval = 100;
        _timerUpdate.Tick += TimerUpdate_Tick;

        // _tableLayoutMain
        _tableLayoutMain.ColumnCount = 1;
        _tableLayoutMain.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
        _tableLayoutMain.Controls.Add(_tableLayoutTop, 0, 0);
        _tableLayoutMain.Controls.Add(_grpProcessSteps, 0, 1);
        _tableLayoutMain.Controls.Add(_grpEntries, 0, 2);
        _tableLayoutMain.Dock = DockStyle.Fill;
        _tableLayoutMain.Location = new Point(0, 0);
        _tableLayoutMain.Name = "_tableLayoutMain";
        _tableLayoutMain.Padding = new Padding(6);
        _tableLayoutMain.RowCount = 3;
        _tableLayoutMain.RowStyles.Add(new RowStyle(SizeType.AutoSize));
        _tableLayoutMain.RowStyles.Add(new RowStyle(SizeType.Percent, 60F));
        _tableLayoutMain.RowStyles.Add(new RowStyle(SizeType.Percent, 40F));
        _tableLayoutMain.Size = new Size(900, 700);
        _tableLayoutMain.TabIndex = 0;

        // _tableLayoutTop
        _tableLayoutTop.AutoSize = true;
        _tableLayoutTop.AutoSizeMode = AutoSizeMode.GrowAndShrink;
        _tableLayoutTop.ColumnCount = 4;
        _tableLayoutTop.ColumnStyles.Add(new ColumnStyle(SizeType.AutoSize));
        _tableLayoutTop.ColumnStyles.Add(new ColumnStyle(SizeType.AutoSize));
        _tableLayoutTop.ColumnStyles.Add(new ColumnStyle(SizeType.AutoSize));
        _tableLayoutTop.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
        _tableLayoutTop.Controls.Add(_btnStartRecording, 0, 0);
        _tableLayoutTop.Controls.Add(_btnStopRecording, 1, 0);
        _tableLayoutTop.Controls.Add(_btnPause, 2, 0);
        _tableLayoutTop.Controls.Add(_tableLayoutInfo, 3, 0);
        _tableLayoutTop.Dock = DockStyle.Fill;
        _tableLayoutTop.Location = new Point(9, 9);
        _tableLayoutTop.Name = "_tableLayoutTop";
        _tableLayoutTop.RowCount = 1;
        _tableLayoutTop.RowStyles.Add(new RowStyle(SizeType.AutoSize));
        _tableLayoutTop.Size = new Size(882, 62);
        _tableLayoutTop.TabIndex = 0;

        // _btnStartRecording
        _btnStartRecording.BackColor = Color.FromArgb(34, 139, 34);
        _btnStartRecording.FlatStyle = FlatStyle.Flat;
        _btnStartRecording.Font = new Font("Segoe UI", 11F, FontStyle.Bold, GraphicsUnit.Point);
        _btnStartRecording.ForeColor = Color.White;
        _btnStartRecording.Location = new Point(3, 3);
        _btnStartRecording.MinimumSize = new Size(140, 56);
        _btnStartRecording.Name = "_btnStartRecording";
        _btnStartRecording.Size = new Size(140, 56);
        _btnStartRecording.TabIndex = 0;
        _btnStartRecording.Text = "Start Recording";
        _btnStartRecording.UseVisualStyleBackColor = false;
        _btnStartRecording.Click += BtnStartRecording_Click;

        // _btnStopRecording
        _btnStopRecording.BackColor = Color.FromArgb(178, 34, 34);
        _btnStopRecording.Enabled = false;
        _btnStopRecording.FlatStyle = FlatStyle.Flat;
        _btnStopRecording.Font = new Font("Segoe UI", 11F, FontStyle.Bold, GraphicsUnit.Point);
        _btnStopRecording.ForeColor = Color.White;
        _btnStopRecording.Location = new Point(149, 3);
        _btnStopRecording.MinimumSize = new Size(140, 56);
        _btnStopRecording.Name = "_btnStopRecording";
        _btnStopRecording.Size = new Size(140, 56);
        _btnStopRecording.TabIndex = 1;
        _btnStopRecording.Text = "Stop Recording";
        _btnStopRecording.UseVisualStyleBackColor = false;
        _btnStopRecording.Click += BtnStopRecording_Click;

        // _btnPause
        _btnPause.BackColor = Color.FromArgb(255, 165, 0);
        _btnPause.Enabled = false;
        _btnPause.FlatStyle = FlatStyle.Flat;
        _btnPause.Font = new Font("Segoe UI", 11F, FontStyle.Bold, GraphicsUnit.Point);
        _btnPause.ForeColor = Color.White;
        _btnPause.Location = new Point(295, 3);
        _btnPause.MinimumSize = new Size(120, 56);
        _btnPause.Name = "_btnPause";
        _btnPause.Size = new Size(120, 56);
        _btnPause.TabIndex = 2;
        _btnPause.Text = "Pause";
        _btnPause.UseVisualStyleBackColor = false;
        _btnPause.Click += BtnPause_Click;

        // _tableLayoutInfo
        _tableLayoutInfo.Anchor = AnchorStyles.Left | AnchorStyles.Right;
        _tableLayoutInfo.AutoSize = true;
        _tableLayoutInfo.AutoSizeMode = AutoSizeMode.GrowAndShrink;
        _tableLayoutInfo.ColumnCount = 2;
        _tableLayoutInfo.ColumnStyles.Add(new ColumnStyle(SizeType.AutoSize));
        _tableLayoutInfo.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
        _tableLayoutInfo.Controls.Add(_lblProgressiveTimeCaption, 0, 0);
        _tableLayoutInfo.Controls.Add(_lblProgressiveTime, 1, 0);
        _tableLayoutInfo.Controls.Add(_lblCurrentStepCaption, 0, 1);
        _tableLayoutInfo.Controls.Add(_lblCurrentStep, 1, 1);
        _tableLayoutInfo.Controls.Add(_lblEntryCountCaption, 0, 2);
        _tableLayoutInfo.Controls.Add(_lblEntryCount, 1, 2);
        _tableLayoutInfo.Location = new Point(421, 3);
        _tableLayoutInfo.Name = "_tableLayoutInfo";
        _tableLayoutInfo.RowCount = 3;
        _tableLayoutInfo.RowStyles.Add(new RowStyle(SizeType.AutoSize));
        _tableLayoutInfo.RowStyles.Add(new RowStyle(SizeType.AutoSize));
        _tableLayoutInfo.RowStyles.Add(new RowStyle(SizeType.AutoSize));
        _tableLayoutInfo.Size = new Size(458, 56);
        _tableLayoutInfo.TabIndex = 3;

        // _lblProgressiveTimeCaption
        _lblProgressiveTimeCaption.Anchor = AnchorStyles.Left;
        _lblProgressiveTimeCaption.AutoSize = true;
        _lblProgressiveTimeCaption.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point);
        _lblProgressiveTimeCaption.Location = new Point(3, 0);
        _lblProgressiveTimeCaption.Name = "_lblProgressiveTimeCaption";
        _lblProgressiveTimeCaption.Size = new Size(130, 18);
        _lblProgressiveTimeCaption.TabIndex = 0;
        _lblProgressiveTimeCaption.Text = "Progressive Time:";

        // _lblProgressiveTime
        _lblProgressiveTime.Anchor = AnchorStyles.Left;
        _lblProgressiveTime.AutoSize = true;
        _lblProgressiveTime.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point);
        _lblProgressiveTime.Location = new Point(139, 0);
        _lblProgressiveTime.Name = "_lblProgressiveTime";
        _lblProgressiveTime.Size = new Size(67, 18);
        _lblProgressiveTime.TabIndex = 1;
        _lblProgressiveTime.Text = "00:00:00";

        // _lblCurrentStepCaption
        _lblCurrentStepCaption.Anchor = AnchorStyles.Left;
        _lblCurrentStepCaption.AutoSize = true;
        _lblCurrentStepCaption.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point);
        _lblCurrentStepCaption.Location = new Point(3, 18);
        _lblCurrentStepCaption.Name = "_lblCurrentStepCaption";
        _lblCurrentStepCaption.Size = new Size(98, 18);
        _lblCurrentStepCaption.TabIndex = 2;
        _lblCurrentStepCaption.Text = "Current Step:";

        // _lblCurrentStep
        _lblCurrentStep.Anchor = AnchorStyles.Left;
        _lblCurrentStep.AutoSize = true;
        _lblCurrentStep.Location = new Point(139, 18);
        _lblCurrentStep.Name = "_lblCurrentStep";
        _lblCurrentStep.Size = new Size(16, 20);
        _lblCurrentStep.TabIndex = 3;
        _lblCurrentStep.Text = "-";

        // _lblEntryCountCaption
        _lblEntryCountCaption.Anchor = AnchorStyles.Left;
        _lblEntryCountCaption.AutoSize = true;
        _lblEntryCountCaption.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point);
        _lblEntryCountCaption.Location = new Point(3, 36);
        _lblEntryCountCaption.Name = "_lblEntryCountCaption";
        _lblEntryCountCaption.Size = new Size(60, 18);
        _lblEntryCountCaption.TabIndex = 4;
        _lblEntryCountCaption.Text = "Entries:";

        // _lblEntryCount
        _lblEntryCount.Anchor = AnchorStyles.Left;
        _lblEntryCount.AutoSize = true;
        _lblEntryCount.Location = new Point(139, 38);
        _lblEntryCount.Name = "_lblEntryCount";
        _lblEntryCount.Size = new Size(17, 18);
        _lblEntryCount.TabIndex = 5;
        _lblEntryCount.Text = "0";

        // _grpProcessSteps
        _grpProcessSteps.Controls.Add(_flowLayoutSteps);
        _grpProcessSteps.Dock = DockStyle.Fill;
        _grpProcessSteps.Location = new Point(9, 77);
        _grpProcessSteps.Name = "_grpProcessSteps";
        _grpProcessSteps.Padding = new Padding(6);
        _grpProcessSteps.Size = new Size(882, 399);
        _grpProcessSteps.TabIndex = 1;
        _grpProcessSteps.TabStop = false;
        _grpProcessSteps.Text = "Process Steps";

        // _flowLayoutSteps
        _flowLayoutSteps.AutoScroll = true;
        _flowLayoutSteps.Dock = DockStyle.Fill;
        _flowLayoutSteps.Location = new Point(6, 26);
        _flowLayoutSteps.Name = "_flowLayoutSteps";
        _flowLayoutSteps.Padding = new Padding(3);
        _flowLayoutSteps.Size = new Size(870, 367);
        _flowLayoutSteps.TabIndex = 0;

        // _grpEntries
        _grpEntries.Controls.Add(_dataGridEntries);
        _grpEntries.Dock = DockStyle.Fill;
        _grpEntries.Location = new Point(9, 482);
        _grpEntries.Name = "_grpEntries";
        _grpEntries.Padding = new Padding(6);
        _grpEntries.Size = new Size(882, 209);
        _grpEntries.TabIndex = 2;
        _grpEntries.TabStop = false;
        _grpEntries.Text = "Time Entries";

        // _dataGridEntries
        _dataGridEntries.AllowUserToAddRows = false;
        _dataGridEntries.AllowUserToDeleteRows = false;
        _dataGridEntries.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        _dataGridEntries.BackgroundColor = SystemColors.Window;
        _dataGridEntries.BorderStyle = BorderStyle.Fixed3D;
        _dataGridEntries.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
        _dataGridEntries.Columns.AddRange(new DataGridViewColumn[] { _colSeq, _colOrderNumber, _colDescription, _colTimestamp, _colElapsed, _colDuration });
        _dataGridEntries.Dock = DockStyle.Fill;
        _dataGridEntries.Location = new Point(6, 26);
        _dataGridEntries.Name = "_dataGridEntries";
        _dataGridEntries.ReadOnly = true;
        _dataGridEntries.RowHeadersWidth = 30;
        _dataGridEntries.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        _dataGridEntries.Size = new Size(870, 177);
        _dataGridEntries.TabIndex = 0;

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

        // RecordingControl
        AutoScaleDimensions = new SizeF(8F, 20F);
        AutoScaleMode = AutoScaleMode.Font;
        Controls.Add(_tableLayoutMain);
        Name = "RecordingControl";
        Size = new Size(900, 700);
        _tableLayoutMain.ResumeLayout(false);
        _tableLayoutMain.PerformLayout();
        _tableLayoutTop.ResumeLayout(false);
        _tableLayoutTop.PerformLayout();
        _tableLayoutInfo.ResumeLayout(false);
        _tableLayoutInfo.PerformLayout();
        _grpProcessSteps.ResumeLayout(false);
        _grpEntries.ResumeLayout(false);
        ((System.ComponentModel.ISupportInitialize)_dataGridEntries).EndInit();
        ResumeLayout(false);
    }

    #endregion

    private System.Windows.Forms.Timer _timerUpdate;
    private TableLayoutPanel _tableLayoutMain;
    private TableLayoutPanel _tableLayoutTop;
    private Button _btnStartRecording;
    private Button _btnStopRecording;
    private Button _btnPause;
    private TableLayoutPanel _tableLayoutInfo;
    private Label _lblProgressiveTimeCaption;
    private Label _lblProgressiveTime;
    private Label _lblCurrentStepCaption;
    private Label _lblCurrentStep;
    private Label _lblEntryCountCaption;
    private Label _lblEntryCount;
    private GroupBox _grpProcessSteps;
    private FlowLayoutPanel _flowLayoutSteps;
    private GroupBox _grpEntries;
    private DataGridView _dataGridEntries;
    private DataGridViewTextBoxColumn _colSeq;
    private DataGridViewTextBoxColumn _colOrderNumber;
    private DataGridViewTextBoxColumn _colDescription;
    private DataGridViewTextBoxColumn _colTimestamp;
    private DataGridViewTextBoxColumn _colElapsed;
    private DataGridViewTextBoxColumn _colDuration;
}
