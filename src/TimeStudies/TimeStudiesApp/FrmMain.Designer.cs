namespace TimeStudiesApp;

partial class FrmMain
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

        _menuStrip = new MenuStrip();
        _tsmFile = new ToolStripMenuItem();
        _tsmFileNew = new ToolStripMenuItem();
        _tsmFileOpen = new ToolStripMenuItem();
        _tsmFileSave = new ToolStripMenuItem();
        _tsmFileSaveAs = new ToolStripMenuItem();
        _tsmFileSep1 = new ToolStripSeparator();
        _tsmFileExportCsv = new ToolStripMenuItem();
        _tsmFileSep2 = new ToolStripSeparator();
        _tsmFileExit = new ToolStripMenuItem();
        _tsmTools = new ToolStripMenuItem();
        _tsmToolsSettings = new ToolStripMenuItem();
        _tsmHelp = new ToolStripMenuItem();
        _tsmHelpAbout = new ToolStripMenuItem();
        _toolStrip = new ToolStrip();
        _tsbNew = new ToolStripButton();
        _tsbOpen = new ToolStripButton();
        _tsbSave = new ToolStripButton();
        _tsbSep1 = new ToolStripSeparator();
        _tsbStartStop = new ToolStripButton();
        _tsbSep2 = new ToolStripSeparator();
        _tsbExport = new ToolStripButton();
        _statusStrip = new StatusStrip();
        _tslStatus = new ToolStripStatusLabel();
        _tslView = new ToolStripStatusLabel();
        _mainPanel = new Panel();

        _menuStrip.SuspendLayout();
        _toolStrip.SuspendLayout();
        _statusStrip.SuspendLayout();
        SuspendLayout();

        // _menuStrip
        _menuStrip.Items.AddRange(new ToolStripItem[] {
            _tsmFile,
            _tsmTools,
            _tsmHelp
        });
        _menuStrip.Location = new Point(0, 0);
        _menuStrip.Name = "_menuStrip";
        _menuStrip.Size = new Size(1024, 24);
        _menuStrip.TabIndex = 0;
        _menuStrip.Text = "menuStrip";

        // _tsmFile
        _tsmFile.DropDownItems.AddRange(new ToolStripItem[] {
            _tsmFileNew,
            _tsmFileOpen,
            _tsmFileSave,
            _tsmFileSaveAs,
            _tsmFileSep1,
            _tsmFileExportCsv,
            _tsmFileSep2,
            _tsmFileExit
        });
        _tsmFile.Name = "_tsmFile";
        _tsmFile.Size = new Size(37, 20);
        _tsmFile.Text = "File";

        // _tsmFileNew
        _tsmFileNew.Name = "_tsmFileNew";
        _tsmFileNew.ShortcutKeys = Keys.Control | Keys.N;
        _tsmFileNew.Size = new Size(200, 22);
        _tsmFileNew.Text = "New Definition";

        // _tsmFileOpen
        _tsmFileOpen.Name = "_tsmFileOpen";
        _tsmFileOpen.ShortcutKeys = Keys.Control | Keys.O;
        _tsmFileOpen.Size = new Size(200, 22);
        _tsmFileOpen.Text = "Open Definition";

        // _tsmFileSave
        _tsmFileSave.Name = "_tsmFileSave";
        _tsmFileSave.ShortcutKeys = Keys.Control | Keys.S;
        _tsmFileSave.Size = new Size(200, 22);
        _tsmFileSave.Text = "Save Definition";

        // _tsmFileSaveAs
        _tsmFileSaveAs.Name = "_tsmFileSaveAs";
        _tsmFileSaveAs.ShortcutKeys = Keys.Control | Keys.Shift | Keys.S;
        _tsmFileSaveAs.Size = new Size(200, 22);
        _tsmFileSaveAs.Text = "Save Definition As...";

        // _tsmFileSep1
        _tsmFileSep1.Name = "_tsmFileSep1";
        _tsmFileSep1.Size = new Size(197, 6);

        // _tsmFileExportCsv
        _tsmFileExportCsv.Name = "_tsmFileExportCsv";
        _tsmFileExportCsv.ShortcutKeys = Keys.Control | Keys.E;
        _tsmFileExportCsv.Size = new Size(200, 22);
        _tsmFileExportCsv.Text = "Export Time Study (CSV)";

        // _tsmFileSep2
        _tsmFileSep2.Name = "_tsmFileSep2";
        _tsmFileSep2.Size = new Size(197, 6);

        // _tsmFileExit
        _tsmFileExit.Name = "_tsmFileExit";
        _tsmFileExit.ShortcutKeys = Keys.Alt | Keys.F4;
        _tsmFileExit.Size = new Size(200, 22);
        _tsmFileExit.Text = "Exit";

        // _tsmTools
        _tsmTools.DropDownItems.AddRange(new ToolStripItem[] {
            _tsmToolsSettings
        });
        _tsmTools.Name = "_tsmTools";
        _tsmTools.Size = new Size(46, 20);
        _tsmTools.Text = "Tools";

        // _tsmToolsSettings
        _tsmToolsSettings.Name = "_tsmToolsSettings";
        _tsmToolsSettings.Size = new Size(200, 22);
        _tsmToolsSettings.Text = "Settings";

        // _tsmHelp
        _tsmHelp.DropDownItems.AddRange(new ToolStripItem[] {
            _tsmHelpAbout
        });
        _tsmHelp.Name = "_tsmHelp";
        _tsmHelp.Size = new Size(44, 20);
        _tsmHelp.Text = "Help";

        // _tsmHelpAbout
        _tsmHelpAbout.Name = "_tsmHelpAbout";
        _tsmHelpAbout.Size = new Size(200, 22);
        _tsmHelpAbout.Text = "About";

        // _toolStrip
        _toolStrip.ImageScalingSize = new Size(32, 32);
        _toolStrip.Items.AddRange(new ToolStripItem[] {
            _tsbNew,
            _tsbOpen,
            _tsbSave,
            _tsbSep1,
            _tsbStartStop,
            _tsbSep2,
            _tsbExport
        });
        _toolStrip.Location = new Point(0, 24);
        _toolStrip.Name = "_toolStrip";
        _toolStrip.Padding = new Padding(5);
        _toolStrip.Size = new Size(1024, 50);
        _toolStrip.TabIndex = 1;

        // _tsbNew
        _tsbNew.DisplayStyle = ToolStripItemDisplayStyle.Text;
        _tsbNew.Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point);
        _tsbNew.Name = "_tsbNew";
        _tsbNew.Padding = new Padding(8, 4, 8, 4);
        _tsbNew.Size = new Size(55, 37);
        _tsbNew.Text = "New";
        _tsbNew.ToolTipText = "Create new definition";

        // _tsbOpen
        _tsbOpen.DisplayStyle = ToolStripItemDisplayStyle.Text;
        _tsbOpen.Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point);
        _tsbOpen.Name = "_tsbOpen";
        _tsbOpen.Padding = new Padding(8, 4, 8, 4);
        _tsbOpen.Size = new Size(61, 37);
        _tsbOpen.Text = "Open";
        _tsbOpen.ToolTipText = "Open definition";

        // _tsbSave
        _tsbSave.DisplayStyle = ToolStripItemDisplayStyle.Text;
        _tsbSave.Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point);
        _tsbSave.Name = "_tsbSave";
        _tsbSave.Padding = new Padding(8, 4, 8, 4);
        _tsbSave.Size = new Size(55, 37);
        _tsbSave.Text = "Save";
        _tsbSave.ToolTipText = "Save definition";

        // _tsbSep1
        _tsbSep1.Name = "_tsbSep1";
        _tsbSep1.Size = new Size(6, 40);

        // _tsbStartStop
        _tsbStartStop.BackColor = Color.LightGreen;
        _tsbStartStop.DisplayStyle = ToolStripItemDisplayStyle.Text;
        _tsbStartStop.Font = new Font("Segoe UI", 11F, FontStyle.Bold, GraphicsUnit.Point);
        _tsbStartStop.Name = "_tsbStartStop";
        _tsbStartStop.Padding = new Padding(12, 4, 12, 4);
        _tsbStartStop.Size = new Size(122, 37);
        _tsbStartStop.Text = "Start Recording";
        _tsbStartStop.ToolTipText = "Start/Stop recording";

        // _tsbSep2
        _tsbSep2.Name = "_tsbSep2";
        _tsbSep2.Size = new Size(6, 40);

        // _tsbExport
        _tsbExport.DisplayStyle = ToolStripItemDisplayStyle.Text;
        _tsbExport.Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point);
        _tsbExport.Name = "_tsbExport";
        _tsbExport.Padding = new Padding(8, 4, 8, 4);
        _tsbExport.Size = new Size(66, 37);
        _tsbExport.Text = "Export";
        _tsbExport.ToolTipText = "Export to CSV";

        // _statusStrip
        _statusStrip.Items.AddRange(new ToolStripItem[] {
            _tslStatus,
            _tslView
        });
        _statusStrip.Location = new Point(0, 746);
        _statusStrip.Name = "_statusStrip";
        _statusStrip.Size = new Size(1024, 22);
        _statusStrip.TabIndex = 2;

        // _tslStatus
        _tslStatus.Name = "_tslStatus";
        _tslStatus.Size = new Size(899, 17);
        _tslStatus.Spring = true;
        _tslStatus.Text = "Ready";
        _tslStatus.TextAlign = ContentAlignment.MiddleLeft;

        // _tslView
        _tslView.BorderSides = ToolStripStatusLabelBorderSides.Left;
        _tslView.BorderStyle = Border3DStyle.Etched;
        _tslView.Name = "_tslView";
        _tslView.Size = new Size(110, 17);
        _tslView.Text = "Definition Editor";

        // _mainPanel
        _mainPanel.Dock = DockStyle.Fill;
        _mainPanel.Location = new Point(0, 74);
        _mainPanel.Name = "_mainPanel";
        _mainPanel.Size = new Size(1024, 672);
        _mainPanel.TabIndex = 3;

        // FrmMain
        AutoScaleDimensions = new SizeF(7F, 15F);
        AutoScaleMode = AutoScaleMode.Font;
        ClientSize = new Size(1024, 768);
        Controls.Add(_mainPanel);
        Controls.Add(_statusStrip);
        Controls.Add(_toolStrip);
        Controls.Add(_menuStrip);
        MainMenuStrip = _menuStrip;
        MinimumSize = new Size(800, 600);
        Name = "FrmMain";
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

    private MenuStrip _menuStrip;
    private ToolStripMenuItem _tsmFile;
    private ToolStripMenuItem _tsmFileNew;
    private ToolStripMenuItem _tsmFileOpen;
    private ToolStripMenuItem _tsmFileSave;
    private ToolStripMenuItem _tsmFileSaveAs;
    private ToolStripSeparator _tsmFileSep1;
    private ToolStripMenuItem _tsmFileExportCsv;
    private ToolStripSeparator _tsmFileSep2;
    private ToolStripMenuItem _tsmFileExit;
    private ToolStripMenuItem _tsmTools;
    private ToolStripMenuItem _tsmToolsSettings;
    private ToolStripMenuItem _tsmHelp;
    private ToolStripMenuItem _tsmHelpAbout;
    private ToolStrip _toolStrip;
    private ToolStripButton _tsbNew;
    private ToolStripButton _tsbOpen;
    private ToolStripButton _tsbSave;
    private ToolStripSeparator _tsbSep1;
    private ToolStripButton _tsbStartStop;
    private ToolStripSeparator _tsbSep2;
    private ToolStripButton _tsbExport;
    private StatusStrip _statusStrip;
    private ToolStripStatusLabel _tslStatus;
    private ToolStripStatusLabel _tslView;
    private Panel _mainPanel;
}
