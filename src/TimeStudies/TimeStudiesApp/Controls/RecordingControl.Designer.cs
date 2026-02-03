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
        if (disposing)
        {
            _displayTimer?.Dispose();
        }
        base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
        components = new System.ComponentModel.Container();

        _tableLayoutPanel = new TableLayoutPanel();
        _controlPanel = new Panel();
        _btnStart = new Button();
        _btnStop = new Button();
        _btnPause = new Button();
        _lblProgressiveTimeLabel = new Label();
        _lblProgressiveTime = new Label();
        _lblCurrentStepLabel = new Label();
        _lblCurrentStep = new Label();
        _lblEntryCountLabel = new Label();
        _lblEntryCount = new Label();
        _lblStatus = new Label();
        _flowLayoutPanel = new FlowLayoutPanel();

        _tableLayoutPanel.SuspendLayout();
        _controlPanel.SuspendLayout();
        SuspendLayout();

        // _tableLayoutPanel
        _tableLayoutPanel.ColumnCount = 1;
        _tableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
        _tableLayoutPanel.Controls.Add(_controlPanel, 0, 0);
        _tableLayoutPanel.Controls.Add(_flowLayoutPanel, 0, 1);
        _tableLayoutPanel.Dock = DockStyle.Fill;
        _tableLayoutPanel.Location = new Point(0, 0);
        _tableLayoutPanel.Name = "_tableLayoutPanel";
        _tableLayoutPanel.RowCount = 2;
        _tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.AutoSize));
        _tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
        _tableLayoutPanel.Size = new Size(800, 600);
        _tableLayoutPanel.TabIndex = 0;

        // _controlPanel
        _controlPanel.BackColor = SystemColors.ControlLight;
        _controlPanel.Controls.Add(_lblStatus);
        _controlPanel.Controls.Add(_lblEntryCount);
        _controlPanel.Controls.Add(_lblEntryCountLabel);
        _controlPanel.Controls.Add(_lblCurrentStep);
        _controlPanel.Controls.Add(_lblCurrentStepLabel);
        _controlPanel.Controls.Add(_lblProgressiveTime);
        _controlPanel.Controls.Add(_lblProgressiveTimeLabel);
        _controlPanel.Controls.Add(_btnPause);
        _controlPanel.Controls.Add(_btnStop);
        _controlPanel.Controls.Add(_btnStart);
        _controlPanel.Dock = DockStyle.Fill;
        _controlPanel.Location = new Point(3, 3);
        _controlPanel.Name = "_controlPanel";
        _controlPanel.Padding = new Padding(10);
        _controlPanel.Size = new Size(794, 100);
        _controlPanel.TabIndex = 0;

        // _btnStart
        _btnStart.BackColor = Color.LightGreen;
        _btnStart.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point);
        _btnStart.Location = new Point(13, 13);
        _btnStart.MinimumSize = new Size(120, 48);
        _btnStart.Name = "_btnStart";
        _btnStart.Size = new Size(150, 48);
        _btnStart.TabIndex = 0;
        _btnStart.Text = "Start Recording";
        _btnStart.UseVisualStyleBackColor = false;

        // _btnStop
        _btnStop.BackColor = Color.LightCoral;
        _btnStop.Enabled = false;
        _btnStop.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point);
        _btnStop.Location = new Point(170, 13);
        _btnStop.MinimumSize = new Size(120, 48);
        _btnStop.Name = "_btnStop";
        _btnStop.Size = new Size(150, 48);
        _btnStop.TabIndex = 1;
        _btnStop.Text = "Stop Recording";
        _btnStop.UseVisualStyleBackColor = false;

        // _btnPause
        _btnPause.BackColor = Color.LightYellow;
        _btnPause.Enabled = false;
        _btnPause.Font = new Font("Segoe UI", 11F, FontStyle.Regular, GraphicsUnit.Point);
        _btnPause.Location = new Point(327, 13);
        _btnPause.MinimumSize = new Size(100, 48);
        _btnPause.Name = "_btnPause";
        _btnPause.Size = new Size(100, 48);
        _btnPause.TabIndex = 2;
        _btnPause.Text = "Pause";
        _btnPause.UseVisualStyleBackColor = false;

        // _lblProgressiveTimeLabel
        _lblProgressiveTimeLabel.AutoSize = true;
        _lblProgressiveTimeLabel.Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point);
        _lblProgressiveTimeLabel.Location = new Point(450, 13);
        _lblProgressiveTimeLabel.Name = "_lblProgressiveTimeLabel";
        _lblProgressiveTimeLabel.Size = new Size(104, 19);
        _lblProgressiveTimeLabel.TabIndex = 3;
        _lblProgressiveTimeLabel.Text = "Progressive Time:";

        // _lblProgressiveTime
        _lblProgressiveTime.AutoSize = true;
        _lblProgressiveTime.Font = new Font("Segoe UI", 18F, FontStyle.Bold, GraphicsUnit.Point);
        _lblProgressiveTime.ForeColor = Color.DarkBlue;
        _lblProgressiveTime.Location = new Point(560, 6);
        _lblProgressiveTime.Name = "_lblProgressiveTime";
        _lblProgressiveTime.Size = new Size(107, 32);
        _lblProgressiveTime.TabIndex = 4;
        _lblProgressiveTime.Text = "00:00:00";

        // _lblCurrentStepLabel
        _lblCurrentStepLabel.AutoSize = true;
        _lblCurrentStepLabel.Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point);
        _lblCurrentStepLabel.Location = new Point(450, 42);
        _lblCurrentStepLabel.Name = "_lblCurrentStepLabel";
        _lblCurrentStepLabel.Size = new Size(85, 19);
        _lblCurrentStepLabel.TabIndex = 5;
        _lblCurrentStepLabel.Text = "Current Step:";

        // _lblCurrentStep
        _lblCurrentStep.AutoSize = true;
        _lblCurrentStep.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point);
        _lblCurrentStep.ForeColor = Color.DarkGreen;
        _lblCurrentStep.Location = new Point(560, 39);
        _lblCurrentStep.Name = "_lblCurrentStep";
        _lblCurrentStep.Size = new Size(20, 21);
        _lblCurrentStep.TabIndex = 6;
        _lblCurrentStep.Text = "-";

        // _lblEntryCountLabel
        _lblEntryCountLabel.AutoSize = true;
        _lblEntryCountLabel.Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point);
        _lblEntryCountLabel.Location = new Point(450, 68);
        _lblEntryCountLabel.Name = "_lblEntryCountLabel";
        _lblEntryCountLabel.Size = new Size(51, 19);
        _lblEntryCountLabel.TabIndex = 7;
        _lblEntryCountLabel.Text = "Entries:";

        // _lblEntryCount
        _lblEntryCount.AutoSize = true;
        _lblEntryCount.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point);
        _lblEntryCount.Location = new Point(560, 65);
        _lblEntryCount.Name = "_lblEntryCount";
        _lblEntryCount.Size = new Size(19, 21);
        _lblEntryCount.TabIndex = 8;
        _lblEntryCount.Text = "0";

        // _lblStatus
        _lblStatus.AutoSize = true;
        _lblStatus.Font = new Font("Segoe UI", 9F, FontStyle.Italic, GraphicsUnit.Point);
        _lblStatus.ForeColor = Color.Gray;
        _lblStatus.Location = new Point(13, 70);
        _lblStatus.Name = "_lblStatus";
        _lblStatus.Size = new Size(82, 15);
        _lblStatus.TabIndex = 9;
        _lblStatus.Text = "Not Recording";

        // _flowLayoutPanel
        _flowLayoutPanel.AutoScroll = true;
        _flowLayoutPanel.BackColor = SystemColors.Control;
        _flowLayoutPanel.Dock = DockStyle.Fill;
        _flowLayoutPanel.FlowDirection = FlowDirection.LeftToRight;
        _flowLayoutPanel.Location = new Point(3, 109);
        _flowLayoutPanel.Name = "_flowLayoutPanel";
        _flowLayoutPanel.Padding = new Padding(10);
        _flowLayoutPanel.Size = new Size(794, 488);
        _flowLayoutPanel.TabIndex = 1;

        // RecordingControl
        AutoScaleDimensions = new SizeF(7F, 15F);
        AutoScaleMode = AutoScaleMode.Font;
        Controls.Add(_tableLayoutPanel);
        Name = "RecordingControl";
        Size = new Size(800, 600);

        _tableLayoutPanel.ResumeLayout(false);
        _controlPanel.ResumeLayout(false);
        _controlPanel.PerformLayout();
        ResumeLayout(false);
    }

    private TableLayoutPanel _tableLayoutPanel;
    private Panel _controlPanel;
    private Button _btnStart;
    private Button _btnStop;
    private Button _btnPause;
    private Label _lblProgressiveTimeLabel;
    private Label _lblProgressiveTime;
    private Label _lblCurrentStepLabel;
    private Label _lblCurrentStep;
    private Label _lblEntryCountLabel;
    private Label _lblEntryCount;
    private Label _lblStatus;
    private FlowLayoutPanel _flowLayoutPanel;
}
