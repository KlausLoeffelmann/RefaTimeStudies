namespace TimeStudiesApp.Controls;

partial class RecordingControl
{
    private System.ComponentModel.IContainer components = null;

    #region Component Designer generated code

    private void InitializeComponent()
    {
        _tableLayoutPanel = new TableLayoutPanel();
        _pnlHeader = new TableLayoutPanel();
        _lblProgressiveTimeCaption = new Label();
        _lblProgressiveTime = new Label();
        _lblCurrentStepCaption = new Label();
        _lblCurrentStep = new Label();
        _lblEntryCountCaption = new Label();
        _lblEntryCount = new Label();
        _pnlControls = new FlowLayoutPanel();
        _btnStart = new Button();
        _btnStop = new Button();
        _btnPause = new Button();
        _pnlStepButtons = new FlowLayoutPanel();
        _tableLayoutPanel.SuspendLayout();
        _pnlHeader.SuspendLayout();
        _pnlControls.SuspendLayout();
        SuspendLayout();
        // 
        // _tableLayoutPanel
        // 
        _tableLayoutPanel.ColumnCount = 1;
        _tableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
        _tableLayoutPanel.Controls.Add(_pnlHeader, 0, 0);
        _tableLayoutPanel.Controls.Add(_pnlControls, 0, 1);
        _tableLayoutPanel.Controls.Add(_pnlStepButtons, 0, 2);
        _tableLayoutPanel.Dock = DockStyle.Fill;
        _tableLayoutPanel.Location = new Point(0, 0);
        _tableLayoutPanel.Name = "_tableLayoutPanel";
        _tableLayoutPanel.Padding = new Padding(10);
        _tableLayoutPanel.RowCount = 3;
        _tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.AutoSize));
        _tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.AutoSize));
        _tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
        _tableLayoutPanel.Size = new Size(800, 600);
        _tableLayoutPanel.TabIndex = 0;
        // 
        // _pnlHeader
        // 
        _pnlHeader.AutoSize = true;
        _pnlHeader.ColumnCount = 6;
        _pnlHeader.ColumnStyles.Add(new ColumnStyle(SizeType.AutoSize));
        _pnlHeader.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.33F));
        _pnlHeader.ColumnStyles.Add(new ColumnStyle(SizeType.AutoSize));
        _pnlHeader.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.33F));
        _pnlHeader.ColumnStyles.Add(new ColumnStyle(SizeType.AutoSize));
        _pnlHeader.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.33F));
        _pnlHeader.Controls.Add(_lblProgressiveTimeCaption, 0, 0);
        _pnlHeader.Controls.Add(_lblProgressiveTime, 1, 0);
        _pnlHeader.Controls.Add(_lblCurrentStepCaption, 2, 0);
        _pnlHeader.Controls.Add(_lblCurrentStep, 3, 0);
        _pnlHeader.Controls.Add(_lblEntryCountCaption, 4, 0);
        _pnlHeader.Controls.Add(_lblEntryCount, 5, 0);
        _pnlHeader.Dock = DockStyle.Fill;
        _pnlHeader.Location = new Point(13, 13);
        _pnlHeader.Name = "_pnlHeader";
        _pnlHeader.Padding = new Padding(0, 0, 0, 10);
        _pnlHeader.RowCount = 1;
        _pnlHeader.RowStyles.Add(new RowStyle(SizeType.AutoSize));
        _pnlHeader.Size = new Size(774, 60);
        _pnlHeader.TabIndex = 0;
        // 
        // _lblProgressiveTimeCaption
        // 
        _lblProgressiveTimeCaption.Anchor = AnchorStyles.Left | AnchorStyles.Right;
        _lblProgressiveTimeCaption.AutoSize = true;
        _lblProgressiveTimeCaption.Font = new Font("Segoe UI", 10F);
        _lblProgressiveTimeCaption.Location = new Point(3, 15);
        _lblProgressiveTimeCaption.Margin = new Padding(3, 0, 10, 0);
        _lblProgressiveTimeCaption.Name = "_lblProgressiveTimeCaption";
        _lblProgressiveTimeCaption.Size = new Size(118, 23);
        _lblProgressiveTimeCaption.TabIndex = 0;
        _lblProgressiveTimeCaption.Text = "Progressive Time:";
        // 
        // _lblProgressiveTime
        // 
        _lblProgressiveTime.Anchor = AnchorStyles.Left | AnchorStyles.Right;
        _lblProgressiveTime.AutoSize = true;
        _lblProgressiveTime.Font = new Font("Segoe UI", 18F, FontStyle.Bold);
        _lblProgressiveTime.ForeColor = Color.FromArgb(0, 120, 215);
        _lblProgressiveTime.Location = new Point(134, 5);
        _lblProgressiveTime.Margin = new Padding(3, 0, 20, 0);
        _lblProgressiveTime.Name = "_lblProgressiveTime";
        _lblProgressiveTime.Size = new Size(110, 41);
        _lblProgressiveTime.TabIndex = 1;
        _lblProgressiveTime.Text = "00:00:00.0";
        // 
        // _lblCurrentStepCaption
        // 
        _lblCurrentStepCaption.Anchor = AnchorStyles.Left | AnchorStyles.Right;
        _lblCurrentStepCaption.AutoSize = true;
        _lblCurrentStepCaption.Font = new Font("Segoe UI", 10F);
        _lblCurrentStepCaption.Location = new Point(267, 15);
        _lblCurrentStepCaption.Margin = new Padding(3, 0, 10, 0);
        _lblCurrentStepCaption.Name = "_lblCurrentStepCaption";
        _lblCurrentStepCaption.Size = new Size(93, 23);
        _lblCurrentStepCaption.TabIndex = 2;
        _lblCurrentStepCaption.Text = "Current Step:";
        // 
        // _lblCurrentStep
        // 
        _lblCurrentStep.Anchor = AnchorStyles.Left | AnchorStyles.Right;
        _lblCurrentStep.AutoSize = true;
        _lblCurrentStep.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold);
        _lblCurrentStep.Location = new Point(373, 11);
        _lblCurrentStep.Margin = new Padding(3, 0, 20, 0);
        _lblCurrentStep.Name = "_lblCurrentStep";
        _lblCurrentStep.Size = new Size(110, 28);
        _lblCurrentStep.TabIndex = 3;
        _lblCurrentStep.Text = "-";
        // 
        // _lblEntryCountCaption
        // 
        _lblEntryCountCaption.Anchor = AnchorStyles.Left | AnchorStyles.Right;
        _lblEntryCountCaption.AutoSize = true;
        _lblEntryCountCaption.Font = new Font("Segoe UI", 10F);
        _lblEntryCountCaption.Location = new Point(506, 15);
        _lblEntryCountCaption.Margin = new Padding(3, 0, 10, 0);
        _lblEntryCountCaption.Name = "_lblEntryCountCaption";
        _lblEntryCountCaption.Size = new Size(56, 23);
        _lblEntryCountCaption.TabIndex = 4;
        _lblEntryCountCaption.Text = "Entries:";
        // 
        // _lblEntryCount
        // 
        _lblEntryCount.Anchor = AnchorStyles.Left | AnchorStyles.Right;
        _lblEntryCount.AutoSize = true;
        _lblEntryCount.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold);
        _lblEntryCount.Location = new Point(575, 11);
        _lblEntryCount.Name = "_lblEntryCount";
        _lblEntryCount.Size = new Size(196, 28);
        _lblEntryCount.TabIndex = 5;
        _lblEntryCount.Text = "0";
        // 
        // _pnlControls
        // 
        _pnlControls.AutoSize = true;
        _pnlControls.Controls.Add(_btnStart);
        _pnlControls.Controls.Add(_btnStop);
        _pnlControls.Controls.Add(_btnPause);
        _pnlControls.Dock = DockStyle.Fill;
        _pnlControls.Location = new Point(13, 79);
        _pnlControls.Name = "_pnlControls";
        _pnlControls.Padding = new Padding(0, 0, 0, 10);
        _pnlControls.Size = new Size(774, 72);
        _pnlControls.TabIndex = 1;
        // 
        // _btnStart
        // 
        _btnStart.BackColor = Color.FromArgb(0, 150, 0);
        _btnStart.FlatAppearance.BorderSize = 0;
        _btnStart.FlatStyle = FlatStyle.Flat;
        _btnStart.Font = new Font("Segoe UI Semibold", 14F, FontStyle.Bold);
        _btnStart.ForeColor = Color.White;
        _btnStart.Location = new Point(3, 3);
        _btnStart.MinimumSize = new Size(180, 56);
        _btnStart.Name = "_btnStart";
        _btnStart.Size = new Size(180, 56);
        _btnStart.TabIndex = 0;
        _btnStart.Text = "Start Recording";
        _btnStart.UseVisualStyleBackColor = false;
        _btnStart.Click += BtnStart_Click;
        // 
        // _btnStop
        // 
        _btnStop.BackColor = Color.FromArgb(200, 0, 0);
        _btnStop.FlatAppearance.BorderSize = 0;
        _btnStop.FlatStyle = FlatStyle.Flat;
        _btnStop.Font = new Font("Segoe UI Semibold", 14F, FontStyle.Bold);
        _btnStop.ForeColor = Color.White;
        _btnStop.Location = new Point(189, 3);
        _btnStop.MinimumSize = new Size(180, 56);
        _btnStop.Name = "_btnStop";
        _btnStop.Size = new Size(180, 56);
        _btnStop.TabIndex = 1;
        _btnStop.Text = "Stop Recording";
        _btnStop.UseVisualStyleBackColor = false;
        _btnStop.Visible = false;
        _btnStop.Click += BtnStop_Click;
        // 
        // _btnPause
        // 
        _btnPause.BackColor = Color.FromArgb(255, 193, 7);
        _btnPause.FlatAppearance.BorderSize = 0;
        _btnPause.FlatStyle = FlatStyle.Flat;
        _btnPause.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold);
        _btnPause.ForeColor = Color.Black;
        _btnPause.Location = new Point(375, 3);
        _btnPause.MinimumSize = new Size(120, 56);
        _btnPause.Name = "_btnPause";
        _btnPause.Size = new Size(120, 56);
        _btnPause.TabIndex = 2;
        _btnPause.Text = "Pause";
        _btnPause.UseVisualStyleBackColor = false;
        _btnPause.Visible = false;
        _btnPause.Click += BtnPause_Click;
        // 
        // _pnlStepButtons
        // 
        _pnlStepButtons.AutoScroll = true;
        _pnlStepButtons.BorderStyle = BorderStyle.FixedSingle;
        _pnlStepButtons.Dock = DockStyle.Fill;
        _pnlStepButtons.Location = new Point(13, 157);
        _pnlStepButtons.Name = "_pnlStepButtons";
        _pnlStepButtons.Padding = new Padding(5);
        _pnlStepButtons.Size = new Size(774, 430);
        _pnlStepButtons.TabIndex = 2;
        // 
        // RecordingControl
        // 
        AutoScaleDimensions = new SizeF(8F, 20F);
        AutoScaleMode = AutoScaleMode.Font;
        Controls.Add(_tableLayoutPanel);
        Name = "RecordingControl";
        Size = new Size(800, 600);
        _tableLayoutPanel.ResumeLayout(false);
        _tableLayoutPanel.PerformLayout();
        _pnlHeader.ResumeLayout(false);
        _pnlHeader.PerformLayout();
        _pnlControls.ResumeLayout(false);
        ResumeLayout(false);
    }

    #endregion

    private TableLayoutPanel _tableLayoutPanel;
    private TableLayoutPanel _pnlHeader;
    private Label _lblProgressiveTimeCaption;
    private Label _lblProgressiveTime;
    private Label _lblCurrentStepCaption;
    private Label _lblCurrentStep;
    private Label _lblEntryCountCaption;
    private Label _lblEntryCount;
    private FlowLayoutPanel _pnlControls;
    private Button _btnStart;
    private Button _btnStop;
    private Button _btnPause;
    private FlowLayoutPanel _pnlStepButtons;
}
