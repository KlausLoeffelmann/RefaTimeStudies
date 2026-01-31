namespace TimeStudiesApp;

partial class MainForm
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
        _menuStrip = new MenuStrip();
        _menuFile = new ToolStripMenuItem();
        _menuFileNew = new ToolStripMenuItem();
        _menuFileOpen = new ToolStripMenuItem();
        _menuFileSave = new ToolStripMenuItem();
        _menuFileSaveAs = new ToolStripMenuItem();
        _toolStripSeparator1 = new ToolStripSeparator();
        _menuFileExportCsv = new ToolStripMenuItem();
        _toolStripSeparator2 = new ToolStripSeparator();
        _menuFileExit = new ToolStripMenuItem();
        _menuTools = new ToolStripMenuItem();
        _menuToolsSettings = new ToolStripMenuItem();
        _menuHelp = new ToolStripMenuItem();
        _menuHelpAbout = new ToolStripMenuItem();
        _toolStrip = new ToolStrip();
        _toolBtnNew = new ToolStripButton();
        _toolBtnOpen = new ToolStripButton();
        _toolBtnSave = new ToolStripButton();
        _toolStripSeparator3 = new ToolStripSeparator();
        _toolBtnStartStop = new ToolStripButton();
        _toolStripSeparator4 = new ToolStripSeparator();
        _toolBtnExport = new ToolStripButton();
        _statusStrip = new StatusStrip();
        _statusLabel = new ToolStripStatusLabel();
        _contentPanel = new Panel();
        _menuStrip.SuspendLayout();
        _toolStrip.SuspendLayout();
        _statusStrip.SuspendLayout();
        SuspendLayout();
        // 
        // _menuStrip
        // 
        _menuStrip.ImageScalingSize = new Size(20, 20);
        _menuStrip.Items.AddRange(new ToolStripItem[] { _menuFile, _menuTools, _menuHelp });
        _menuStrip.Location = new Point(0, 0);
        _menuStrip.Name = "_menuStrip";
        _menuStrip.Padding = new Padding(6, 3, 0, 3);
        _menuStrip.Size = new Size(1024, 30);
        _menuStrip.TabIndex = 0;
        _menuStrip.Text = "menuStrip1";
        // 
        // _menuFile
        // 
        _menuFile.DropDownItems.AddRange(new ToolStripItem[] { _menuFileNew, _menuFileOpen, _menuFileSave, _menuFileSaveAs, _toolStripSeparator1, _menuFileExportCsv, _toolStripSeparator2, _menuFileExit });
        _menuFile.Name = "_menuFile";
        _menuFile.Size = new Size(46, 24);
        _menuFile.Text = "&File";
        // 
        // _menuFileNew
        // 
        _menuFileNew.Name = "_menuFileNew";
        _menuFileNew.ShortcutKeys = Keys.Control | Keys.N;
        _menuFileNew.Size = new Size(250, 26);
        _menuFileNew.Text = "&New Definition";
        _menuFileNew.Click += MenuFileNew_Click;
        // 
        // _menuFileOpen
        // 
        _menuFileOpen.Name = "_menuFileOpen";
        _menuFileOpen.ShortcutKeys = Keys.Control | Keys.O;
        _menuFileOpen.Size = new Size(250, 26);
        _menuFileOpen.Text = "&Open Definition";
        _menuFileOpen.Click += MenuFileOpen_Click;
        // 
        // _menuFileSave
        // 
        _menuFileSave.Name = "_menuFileSave";
        _menuFileSave.ShortcutKeys = Keys.Control | Keys.S;
        _menuFileSave.Size = new Size(250, 26);
        _menuFileSave.Text = "&Save Definition";
        _menuFileSave.Click += MenuFileSave_Click;
        // 
        // _menuFileSaveAs
        // 
        _menuFileSaveAs.Name = "_menuFileSaveAs";
        _menuFileSaveAs.ShortcutKeys = Keys.Control | Keys.Shift | Keys.S;
        _menuFileSaveAs.Size = new Size(250, 26);
        _menuFileSaveAs.Text = "Save Definition &As...";
        _menuFileSaveAs.Click += MenuFileSaveAs_Click;
        // 
        // _toolStripSeparator1
        // 
        _toolStripSeparator1.Name = "_toolStripSeparator1";
        _toolStripSeparator1.Size = new Size(247, 6);
        // 
        // _menuFileExportCsv
        // 
        _menuFileExportCsv.Name = "_menuFileExportCsv";
        _menuFileExportCsv.ShortcutKeys = Keys.Control | Keys.E;
        _menuFileExportCsv.Size = new Size(250, 26);
        _menuFileExportCsv.Text = "&Export Time Study (CSV)";
        _menuFileExportCsv.Click += MenuFileExportCsv_Click;
        // 
        // _toolStripSeparator2
        // 
        _toolStripSeparator2.Name = "_toolStripSeparator2";
        _toolStripSeparator2.Size = new Size(247, 6);
        // 
        // _menuFileExit
        // 
        _menuFileExit.Name = "_menuFileExit";
        _menuFileExit.ShortcutKeys = Keys.Alt | Keys.F4;
        _menuFileExit.Size = new Size(250, 26);
        _menuFileExit.Text = "E&xit";
        _menuFileExit.Click += MenuFileExit_Click;
        // 
        // _menuTools
        // 
        _menuTools.DropDownItems.AddRange(new ToolStripItem[] { _menuToolsSettings });
        _menuTools.Name = "_menuTools";
        _menuTools.Size = new Size(58, 24);
        _menuTools.Text = "&Tools";
        // 
        // _menuToolsSettings
        // 
        _menuToolsSettings.Name = "_menuToolsSettings";
        _menuToolsSettings.Size = new Size(145, 26);
        _menuToolsSettings.Text = "&Settings";
        _menuToolsSettings.Click += MenuToolsSettings_Click;
        // 
        // _menuHelp
        // 
        _menuHelp.DropDownItems.AddRange(new ToolStripItem[] { _menuHelpAbout });
        _menuHelp.Name = "_menuHelp";
        _menuHelp.Size = new Size(55, 24);
        _menuHelp.Text = "&Help";
        // 
        // _menuHelpAbout
        // 
        _menuHelpAbout.Name = "_menuHelpAbout";
        _menuHelpAbout.Size = new Size(133, 26);
        _menuHelpAbout.Text = "&About";
        _menuHelpAbout.Click += MenuHelpAbout_Click;
        // 
        // _toolStrip
        // 
        _toolStrip.ImageScalingSize = new Size(32, 32);
        _toolStrip.Items.AddRange(new ToolStripItem[] { _toolBtnNew, _toolBtnOpen, _toolBtnSave, _toolStripSeparator3, _toolBtnStartStop, _toolStripSeparator4, _toolBtnExport });
        _toolStrip.Location = new Point(0, 30);
        _toolStrip.Name = "_toolStrip";
        _toolStrip.Padding = new Padding(4, 2, 4, 2);
        _toolStrip.Size = new Size(1024, 55);
        _toolStrip.TabIndex = 1;
        _toolStrip.Text = "toolStrip1";
        // 
        // _toolBtnNew
        // 
        _toolBtnNew.DisplayStyle = ToolStripItemDisplayStyle.Text;
        _toolBtnNew.Font = new Font("Segoe UI", 10F);
        _toolBtnNew.Name = "_toolBtnNew";
        _toolBtnNew.Padding = new Padding(8, 4, 8, 4);
        _toolBtnNew.Size = new Size(56, 48);
        _toolBtnNew.Text = "New";
        _toolBtnNew.Click += ToolBtnNew_Click;
        // 
        // _toolBtnOpen
        // 
        _toolBtnOpen.DisplayStyle = ToolStripItemDisplayStyle.Text;
        _toolBtnOpen.Font = new Font("Segoe UI", 10F);
        _toolBtnOpen.Name = "_toolBtnOpen";
        _toolBtnOpen.Padding = new Padding(8, 4, 8, 4);
        _toolBtnOpen.Size = new Size(64, 48);
        _toolBtnOpen.Text = "Open";
        _toolBtnOpen.Click += ToolBtnOpen_Click;
        // 
        // _toolBtnSave
        // 
        _toolBtnSave.DisplayStyle = ToolStripItemDisplayStyle.Text;
        _toolBtnSave.Font = new Font("Segoe UI", 10F);
        _toolBtnSave.Name = "_toolBtnSave";
        _toolBtnSave.Padding = new Padding(8, 4, 8, 4);
        _toolBtnSave.Size = new Size(55, 48);
        _toolBtnSave.Text = "Save";
        _toolBtnSave.Click += ToolBtnSave_Click;
        // 
        // _toolStripSeparator3
        // 
        _toolStripSeparator3.Name = "_toolStripSeparator3";
        _toolStripSeparator3.Size = new Size(6, 51);
        // 
        // _toolBtnStartStop
        // 
        _toolBtnStartStop.BackColor = Color.FromArgb(0, 120, 215);
        _toolBtnStartStop.DisplayStyle = ToolStripItemDisplayStyle.Text;
        _toolBtnStartStop.Font = new Font("Segoe UI Semibold", 11F, FontStyle.Bold);
        _toolBtnStartStop.ForeColor = Color.White;
        _toolBtnStartStop.Name = "_toolBtnStartStop";
        _toolBtnStartStop.Padding = new Padding(12, 6, 12, 6);
        _toolBtnStartStop.Size = new Size(148, 48);
        _toolBtnStartStop.Text = "Start Recording";
        _toolBtnStartStop.Click += ToolBtnStartStop_Click;
        // 
        // _toolStripSeparator4
        // 
        _toolStripSeparator4.Name = "_toolStripSeparator4";
        _toolStripSeparator4.Size = new Size(6, 51);
        // 
        // _toolBtnExport
        // 
        _toolBtnExport.DisplayStyle = ToolStripItemDisplayStyle.Text;
        _toolBtnExport.Font = new Font("Segoe UI", 10F);
        _toolBtnExport.Name = "_toolBtnExport";
        _toolBtnExport.Padding = new Padding(8, 4, 8, 4);
        _toolBtnExport.Size = new Size(69, 48);
        _toolBtnExport.Text = "Export";
        _toolBtnExport.Click += ToolBtnExport_Click;
        // 
        // _statusStrip
        // 
        _statusStrip.ImageScalingSize = new Size(20, 20);
        _statusStrip.Items.AddRange(new ToolStripItem[] { _statusLabel });
        _statusStrip.Location = new Point(0, 696);
        _statusStrip.Name = "_statusStrip";
        _statusStrip.Padding = new Padding(1, 0, 16, 0);
        _statusStrip.Size = new Size(1024, 26);
        _statusStrip.TabIndex = 2;
        _statusStrip.Text = "statusStrip1";
        // 
        // _statusLabel
        // 
        _statusLabel.Name = "_statusLabel";
        _statusLabel.Size = new Size(50, 20);
        _statusLabel.Text = "Ready";
        // 
        // _contentPanel
        // 
        _contentPanel.Dock = DockStyle.Fill;
        _contentPanel.Location = new Point(0, 85);
        _contentPanel.Margin = new Padding(0);
        _contentPanel.Name = "_contentPanel";
        _contentPanel.Size = new Size(1024, 611);
        _contentPanel.TabIndex = 3;
        // 
        // MainForm
        // 
        AutoScaleDimensions = new SizeF(8F, 20F);
        AutoScaleMode = AutoScaleMode.Font;
        ClientSize = new Size(1024, 722);
        Controls.Add(_contentPanel);
        Controls.Add(_statusStrip);
        Controls.Add(_toolStrip);
        Controls.Add(_menuStrip);
        MainMenuStrip = _menuStrip;
        MinimumSize = new Size(800, 600);
        Name = "MainForm";
        StartPosition = FormStartPosition.CenterScreen;
        Text = "REFA Time Study";
        _menuStrip.ResumeLayout(false);
        _menuStrip.PerformLayout();
        _toolStrip.ResumeLayout(false);
        _toolStrip.PerformLayout();
        _statusStrip.ResumeLayout(false);
        _statusStrip.PerformLayout();
        ResumeLayout(false);
        PerformLayout();
    }

    #endregion

    private MenuStrip _menuStrip;
    private ToolStripMenuItem _menuFile;
    private ToolStripMenuItem _menuFileNew;
    private ToolStripMenuItem _menuFileOpen;
    private ToolStripMenuItem _menuFileSave;
    private ToolStripMenuItem _menuFileSaveAs;
    private ToolStripSeparator _toolStripSeparator1;
    private ToolStripMenuItem _menuFileExportCsv;
    private ToolStripSeparator _toolStripSeparator2;
    private ToolStripMenuItem _menuFileExit;
    private ToolStripMenuItem _menuTools;
    private ToolStripMenuItem _menuToolsSettings;
    private ToolStripMenuItem _menuHelp;
    private ToolStripMenuItem _menuHelpAbout;
    private ToolStrip _toolStrip;
    private ToolStripButton _toolBtnNew;
    private ToolStripButton _toolBtnOpen;
    private ToolStripButton _toolBtnSave;
    private ToolStripSeparator _toolStripSeparator3;
    private ToolStripButton _toolBtnStartStop;
    private ToolStripSeparator _toolStripSeparator4;
    private ToolStripButton _toolBtnExport;
    private StatusStrip _statusStrip;
    private ToolStripStatusLabel _statusLabel;
    private Panel _contentPanel;
}
