namespace TimeStudiesApp.Controls;

partial class RecordingControl
{
    private System.ComponentModel.IContainer components = null;

    #region Component Designer generated code

    private void InitializeComponent()
    {
        components = new System.ComponentModel.Container();

        _tlpMain = new TableLayoutPanel();
        _pnlControls = new FlowLayoutPanel();
        _btnStart = new Button();
        _btnStop = new Button();
        _btnPause = new Button();
        _pnlStatus = new TableLayoutPanel();
        _lblProgressiveTimeCaption = new Label();
        _lblProgressiveTime = new Label();
        _lblCurrentStepCaption = new Label();
        _lblCurrentStep = new Label();
        _lblEntriesCaption = new Label();
        _lblEntries = new Label();
        _flpSteps = new FlowLayoutPanel();

        _tlpMain.SuspendLayout();
        _pnlControls.SuspendLayout();
        _pnlStatus.SuspendLayout();
        SuspendLayout();

        // _tlpMain
        _tlpMain.ColumnCount = 1;
        _tlpMain.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
        _tlpMain.Controls.Add(_pnlControls, 0, 0);
        _tlpMain.Controls.Add(_pnlStatus, 0, 1);
        _tlpMain.Controls.Add(_flpSteps, 0, 2);
        _tlpMain.Dock = DockStyle.Fill;
        _tlpMain.Location = new Point(0, 0);
        _tlpMain.Name = "_tlpMain";
        _tlpMain.RowCount = 3;
        _tlpMain.RowStyles.Add(new RowStyle(SizeType.AutoSize));
        _tlpMain.RowStyles.Add(new RowStyle(SizeType.AutoSize));
        _tlpMain.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
        _tlpMain.Size = new Size(800, 600);
        _tlpMain.TabIndex = 0;

        // _pnlControls
        _pnlControls.AutoSize = true;
        _pnlControls.AutoSizeMode = AutoSizeMode.GrowAndShrink;
        _pnlControls.Controls.Add(_btnStart);
        _pnlControls.Controls.Add(_btnStop);
        _pnlControls.Controls.Add(_btnPause);
        _pnlControls.Dock = DockStyle.Fill;
        _pnlControls.Location = new Point(3, 3);
        _pnlControls.Name = "_pnlControls";
        _pnlControls.Padding = new Padding(0, 0, 0, 8);
        _pnlControls.Size = new Size(794, 76);
        _pnlControls.TabIndex = 0;

        // _btnStart
        _btnStart.BackColor = Color.FromArgb(200, 255, 200);
        _btnStart.FlatStyle = FlatStyle.Flat;
        _btnStart.Font = new Font("Segoe UI", 11F, FontStyle.Bold, GraphicsUnit.Point);
        _btnStart.Location = new Point(3, 3);
        _btnStart.Margin = new Padding(3);
        _btnStart.MinimumSize = new Size(120, 60);
        _btnStart.Name = "_btnStart";
        _btnStart.Size = new Size(160, 60);
        _btnStart.TabIndex = 0;
        _btnStart.Text = "Start Recording";
        _btnStart.UseVisualStyleBackColor = false;
        _btnStart.Click += BtnStart_Click;

        // _btnStop
        _btnStop.BackColor = Color.FromArgb(255, 200, 200);
        _btnStop.Enabled = false;
        _btnStop.FlatStyle = FlatStyle.Flat;
        _btnStop.Font = new Font("Segoe UI", 11F, FontStyle.Bold, GraphicsUnit.Point);
        _btnStop.Location = new Point(169, 3);
        _btnStop.Margin = new Padding(3);
        _btnStop.MinimumSize = new Size(120, 60);
        _btnStop.Name = "_btnStop";
        _btnStop.Size = new Size(160, 60);
        _btnStop.TabIndex = 1;
        _btnStop.Text = "Stop Recording";
        _btnStop.UseVisualStyleBackColor = false;
        _btnStop.Click += BtnStop_Click;

        // _btnPause
        _btnPause.BackColor = Color.FromArgb(255, 255, 200);
        _btnPause.Enabled = false;
        _btnPause.FlatStyle = FlatStyle.Flat;
        _btnPause.Font = new Font("Segoe UI", 11F, FontStyle.Bold, GraphicsUnit.Point);
        _btnPause.Location = new Point(335, 3);
        _btnPause.Margin = new Padding(3);
        _btnPause.MinimumSize = new Size(100, 60);
        _btnPause.Name = "_btnPause";
        _btnPause.Size = new Size(120, 60);
        _btnPause.TabIndex = 2;
        _btnPause.Text = "Pause";
        _btnPause.UseVisualStyleBackColor = false;
        _btnPause.Click += BtnPause_Click;

        // _pnlStatus
        _pnlStatus.AutoSize = true;
        _pnlStatus.AutoSizeMode = AutoSizeMode.GrowAndShrink;
        _pnlStatus.BackColor = SystemColors.ControlLight;
        _pnlStatus.ColumnCount = 6;
        _pnlStatus.ColumnStyles.Add(new ColumnStyle(SizeType.AutoSize));
        _pnlStatus.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 180F));
        _pnlStatus.ColumnStyles.Add(new ColumnStyle(SizeType.AutoSize));
        _pnlStatus.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
        _pnlStatus.ColumnStyles.Add(new ColumnStyle(SizeType.AutoSize));
        _pnlStatus.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 60F));
        _pnlStatus.Controls.Add(_lblProgressiveTimeCaption, 0, 0);
        _pnlStatus.Controls.Add(_lblProgressiveTime, 1, 0);
        _pnlStatus.Controls.Add(_lblCurrentStepCaption, 2, 0);
        _pnlStatus.Controls.Add(_lblCurrentStep, 3, 0);
        _pnlStatus.Controls.Add(_lblEntriesCaption, 4, 0);
        _pnlStatus.Controls.Add(_lblEntries, 5, 0);
        _pnlStatus.Dock = DockStyle.Fill;
        _pnlStatus.Location = new Point(3, 85);
        _pnlStatus.Name = "_pnlStatus";
        _pnlStatus.Padding = new Padding(8);
        _pnlStatus.RowCount = 1;
        _pnlStatus.RowStyles.Add(new RowStyle(SizeType.AutoSize));
        _pnlStatus.Size = new Size(794, 52);
        _pnlStatus.TabIndex = 1;

        // _lblProgressiveTimeCaption
        _lblProgressiveTimeCaption.Anchor = AnchorStyles.Left | AnchorStyles.Right;
        _lblProgressiveTimeCaption.AutoSize = true;
        _lblProgressiveTimeCaption.Font = new Font("Segoe UI", 10F, FontStyle.Bold, GraphicsUnit.Point);
        _lblProgressiveTimeCaption.Location = new Point(11, 14);
        _lblProgressiveTimeCaption.Margin = new Padding(3);
        _lblProgressiveTimeCaption.Name = "_lblProgressiveTimeCaption";
        _lblProgressiveTimeCaption.Size = new Size(120, 23);
        _lblProgressiveTimeCaption.TabIndex = 0;
        _lblProgressiveTimeCaption.Text = "Progressive Time:";

        // _lblProgressiveTime
        _lblProgressiveTime.Anchor = AnchorStyles.Left | AnchorStyles.Right;
        _lblProgressiveTime.AutoSize = true;
        _lblProgressiveTime.Font = new Font("Consolas", 14F, FontStyle.Bold, GraphicsUnit.Point);
        _lblProgressiveTime.ForeColor = Color.DarkBlue;
        _lblProgressiveTime.Location = new Point(137, 10);
        _lblProgressiveTime.Margin = new Padding(3);
        _lblProgressiveTime.Name = "_lblProgressiveTime";
        _lblProgressiveTime.Size = new Size(174, 28);
        _lblProgressiveTime.TabIndex = 1;
        _lblProgressiveTime.Text = "00:00:00.0";

        // _lblCurrentStepCaption
        _lblCurrentStepCaption.Anchor = AnchorStyles.Left | AnchorStyles.Right;
        _lblCurrentStepCaption.AutoSize = true;
        _lblCurrentStepCaption.Font = new Font("Segoe UI", 10F, FontStyle.Bold, GraphicsUnit.Point);
        _lblCurrentStepCaption.Location = new Point(317, 14);
        _lblCurrentStepCaption.Margin = new Padding(3);
        _lblCurrentStepCaption.Name = "_lblCurrentStepCaption";
        _lblCurrentStepCaption.Size = new Size(100, 23);
        _lblCurrentStepCaption.TabIndex = 2;
        _lblCurrentStepCaption.Text = "Current Step:";

        // _lblCurrentStep
        _lblCurrentStep.Anchor = AnchorStyles.Left | AnchorStyles.Right;
        _lblCurrentStep.AutoSize = true;
        _lblCurrentStep.Font = new Font("Segoe UI", 11F, FontStyle.Regular, GraphicsUnit.Point);
        _lblCurrentStep.Location = new Point(423, 12);
        _lblCurrentStep.Margin = new Padding(3);
        _lblCurrentStep.Name = "_lblCurrentStep";
        _lblCurrentStep.Size = new Size(226, 25);
        _lblCurrentStep.TabIndex = 3;
        _lblCurrentStep.Text = "-";

        // _lblEntriesCaption
        _lblEntriesCaption.Anchor = AnchorStyles.Left | AnchorStyles.Right;
        _lblEntriesCaption.AutoSize = true;
        _lblEntriesCaption.Font = new Font("Segoe UI", 10F, FontStyle.Bold, GraphicsUnit.Point);
        _lblEntriesCaption.Location = new Point(655, 14);
        _lblEntriesCaption.Margin = new Padding(3);
        _lblEntriesCaption.Name = "_lblEntriesCaption";
        _lblEntriesCaption.Size = new Size(68, 23);
        _lblEntriesCaption.TabIndex = 4;
        _lblEntriesCaption.Text = "Entries:";

        // _lblEntries
        _lblEntries.Anchor = AnchorStyles.Left | AnchorStyles.Right;
        _lblEntries.AutoSize = true;
        _lblEntries.Font = new Font("Segoe UI", 11F, FontStyle.Bold, GraphicsUnit.Point);
        _lblEntries.Location = new Point(729, 12);
        _lblEntries.Margin = new Padding(3);
        _lblEntries.Name = "_lblEntries";
        _lblEntries.Size = new Size(54, 25);
        _lblEntries.TabIndex = 5;
        _lblEntries.Text = "0";

        // _flpSteps
        _flpSteps.AutoScroll = true;
        _flpSteps.Dock = DockStyle.Fill;
        _flpSteps.Location = new Point(3, 143);
        _flpSteps.Name = "_flpSteps";
        _flpSteps.Padding = new Padding(4);
        _flpSteps.Size = new Size(794, 454);
        _flpSteps.TabIndex = 2;

        // RecordingControl
        AutoScaleDimensions = new SizeF(8F, 20F);
        AutoScaleMode = AutoScaleMode.Font;
        Controls.Add(_tlpMain);
        Name = "RecordingControl";
        Size = new Size(800, 600);

        _tlpMain.ResumeLayout(false);
        _tlpMain.PerformLayout();
        _pnlControls.ResumeLayout(false);
        _pnlStatus.ResumeLayout(false);
        _pnlStatus.PerformLayout();
        ResumeLayout(false);
    }

    #endregion

    private TableLayoutPanel _tlpMain;
    private FlowLayoutPanel _pnlControls;
    private Button _btnStart;
    private Button _btnStop;
    private Button _btnPause;
    private TableLayoutPanel _pnlStatus;
    private Label _lblProgressiveTimeCaption;
    private Label _lblProgressiveTime;
    private Label _lblCurrentStepCaption;
    private Label _lblCurrentStep;
    private Label _lblEntriesCaption;
    private Label _lblEntries;
    private FlowLayoutPanel _flpSteps;
}
