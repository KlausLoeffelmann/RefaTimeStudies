namespace TimeStudiesApp
{
    partial class FrmMain
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();

            _menuStrip = new MenuStrip();
            _mnuFile = new ToolStripMenuItem();
            _mnuFileNew = new ToolStripMenuItem();
            _mnuFileOpen = new ToolStripMenuItem();
            _mnuFileSeparator1 = new ToolStripSeparator();
            _mnuFileSave = new ToolStripMenuItem();
            _mnuFileSaveAs = new ToolStripMenuItem();
            _mnuFileSeparator2 = new ToolStripSeparator();
            _mnuFileExport = new ToolStripMenuItem();
            _mnuFileSeparator3 = new ToolStripSeparator();
            _mnuFileExit = new ToolStripMenuItem();
            _mnuTools = new ToolStripMenuItem();
            _mnuToolsSettings = new ToolStripMenuItem();
            _mnuHelp = new ToolStripMenuItem();
            _mnuHelpAbout = new ToolStripMenuItem();
            _toolStrip = new ToolStrip();
            _tsbNew = new ToolStripButton();
            _tsbOpen = new ToolStripButton();
            _tsbSave = new ToolStripButton();
            _tsbSeparator1 = new ToolStripSeparator();
            _tsbStartStop = new ToolStripButton();
            _tsbSeparator2 = new ToolStripSeparator();
            _tsbExport = new ToolStripButton();
            _statusStrip = new StatusStrip();
            _sslStatus = new ToolStripStatusLabel();
            _pnlContent = new Panel();

            _menuStrip.SuspendLayout();
            _toolStrip.SuspendLayout();
            _statusStrip.SuspendLayout();
            SuspendLayout();

            // _menuStrip
            _menuStrip.ImageScalingSize = new Size(20, 20);
            _menuStrip.Items.AddRange(new ToolStripItem[]
            {
                _mnuFile,
                _mnuTools,
                _mnuHelp
            });
            _menuStrip.Location = new Point(0, 0);
            _menuStrip.Name = "_menuStrip";
            _menuStrip.Size = new Size(1024, 28);
            _menuStrip.TabIndex = 0;
            _menuStrip.Text = "menuStrip1";

            // _mnuFile
            _mnuFile.DropDownItems.AddRange(new ToolStripItem[]
            {
                _mnuFileNew,
                _mnuFileOpen,
                _mnuFileSeparator1,
                _mnuFileSave,
                _mnuFileSaveAs,
                _mnuFileSeparator2,
                _mnuFileExport,
                _mnuFileSeparator3,
                _mnuFileExit
            });
            _mnuFile.Name = "_mnuFile";
            _mnuFile.Size = new Size(46, 24);
            _mnuFile.Text = "&File";

            // _mnuFileNew
            _mnuFileNew.Name = "_mnuFileNew";
            _mnuFileNew.ShortcutKeys = Keys.Control | Keys.N;
            _mnuFileNew.Size = new Size(260, 26);
            _mnuFileNew.Text = "&New Definition";
            _mnuFileNew.Click += MnuFileNew_Click;

            // _mnuFileOpen
            _mnuFileOpen.Name = "_mnuFileOpen";
            _mnuFileOpen.ShortcutKeys = Keys.Control | Keys.O;
            _mnuFileOpen.Size = new Size(260, 26);
            _mnuFileOpen.Text = "&Open Definition...";
            _mnuFileOpen.Click += MnuFileOpen_Click;

            // _mnuFileSeparator1
            _mnuFileSeparator1.Name = "_mnuFileSeparator1";
            _mnuFileSeparator1.Size = new Size(257, 6);

            // _mnuFileSave
            _mnuFileSave.Name = "_mnuFileSave";
            _mnuFileSave.ShortcutKeys = Keys.Control | Keys.S;
            _mnuFileSave.Size = new Size(260, 26);
            _mnuFileSave.Text = "&Save Definition";
            _mnuFileSave.Click += MnuFileSave_Click;

            // _mnuFileSaveAs
            _mnuFileSaveAs.Name = "_mnuFileSaveAs";
            _mnuFileSaveAs.ShortcutKeys = Keys.Control | Keys.Shift | Keys.S;
            _mnuFileSaveAs.Size = new Size(260, 26);
            _mnuFileSaveAs.Text = "Save Definition &As...";
            _mnuFileSaveAs.Click += MnuFileSaveAs_Click;

            // _mnuFileSeparator2
            _mnuFileSeparator2.Name = "_mnuFileSeparator2";
            _mnuFileSeparator2.Size = new Size(257, 6);

            // _mnuFileExport
            _mnuFileExport.Name = "_mnuFileExport";
            _mnuFileExport.ShortcutKeys = Keys.Control | Keys.E;
            _mnuFileExport.Size = new Size(260, 26);
            _mnuFileExport.Text = "&Export Time Study (CSV)...";
            _mnuFileExport.Click += MnuFileExport_Click;

            // _mnuFileSeparator3
            _mnuFileSeparator3.Name = "_mnuFileSeparator3";
            _mnuFileSeparator3.Size = new Size(257, 6);

            // _mnuFileExit
            _mnuFileExit.Name = "_mnuFileExit";
            _mnuFileExit.ShortcutKeys = Keys.Alt | Keys.F4;
            _mnuFileExit.Size = new Size(260, 26);
            _mnuFileExit.Text = "E&xit";
            _mnuFileExit.Click += MnuFileExit_Click;

            // _mnuTools
            _mnuTools.DropDownItems.AddRange(new ToolStripItem[]
            {
                _mnuToolsSettings
            });
            _mnuTools.Name = "_mnuTools";
            _mnuTools.Size = new Size(58, 24);
            _mnuTools.Text = "&Tools";

            // _mnuToolsSettings
            _mnuToolsSettings.Name = "_mnuToolsSettings";
            _mnuToolsSettings.Size = new Size(160, 26);
            _mnuToolsSettings.Text = "&Settings...";
            _mnuToolsSettings.Click += MnuToolsSettings_Click;

            // _mnuHelp
            _mnuHelp.DropDownItems.AddRange(new ToolStripItem[]
            {
                _mnuHelpAbout
            });
            _mnuHelp.Name = "_mnuHelp";
            _mnuHelp.Size = new Size(55, 24);
            _mnuHelp.Text = "&Help";

            // _mnuHelpAbout
            _mnuHelpAbout.Name = "_mnuHelpAbout";
            _mnuHelpAbout.Size = new Size(140, 26);
            _mnuHelpAbout.Text = "&About...";
            _mnuHelpAbout.Click += MnuHelpAbout_Click;

            // _toolStrip
            _toolStrip.ImageScalingSize = new Size(24, 24);
            _toolStrip.Items.AddRange(new ToolStripItem[]
            {
                _tsbNew,
                _tsbOpen,
                _tsbSave,
                _tsbSeparator1,
                _tsbStartStop,
                _tsbSeparator2,
                _tsbExport
            });
            _toolStrip.Location = new Point(0, 28);
            _toolStrip.Name = "_toolStrip";
            _toolStrip.Padding = new Padding(0, 0, 2, 0);
            _toolStrip.Size = new Size(1024, 56);
            _toolStrip.TabIndex = 1;
            _toolStrip.Text = "toolStrip1";

            // _tsbNew
            _tsbNew.DisplayStyle = ToolStripItemDisplayStyle.ImageAndText;
            _tsbNew.ImageTransparentColor = Color.Magenta;
            _tsbNew.Margin = new Padding(4, 4, 4, 4);
            _tsbNew.Name = "_tsbNew";
            _tsbNew.Padding = new Padding(8, 4, 8, 4);
            _tsbNew.Size = new Size(75, 48);
            _tsbNew.Text = "New";
            _tsbNew.TextImageRelation = TextImageRelation.ImageAboveText;
            _tsbNew.Click += TsbNew_Click;

            // _tsbOpen
            _tsbOpen.DisplayStyle = ToolStripItemDisplayStyle.ImageAndText;
            _tsbOpen.ImageTransparentColor = Color.Magenta;
            _tsbOpen.Margin = new Padding(4, 4, 4, 4);
            _tsbOpen.Name = "_tsbOpen";
            _tsbOpen.Padding = new Padding(8, 4, 8, 4);
            _tsbOpen.Size = new Size(75, 48);
            _tsbOpen.Text = "Open";
            _tsbOpen.TextImageRelation = TextImageRelation.ImageAboveText;
            _tsbOpen.Click += TsbOpen_Click;

            // _tsbSave
            _tsbSave.DisplayStyle = ToolStripItemDisplayStyle.ImageAndText;
            _tsbSave.ImageTransparentColor = Color.Magenta;
            _tsbSave.Margin = new Padding(4, 4, 4, 4);
            _tsbSave.Name = "_tsbSave";
            _tsbSave.Padding = new Padding(8, 4, 8, 4);
            _tsbSave.Size = new Size(75, 48);
            _tsbSave.Text = "Save";
            _tsbSave.TextImageRelation = TextImageRelation.ImageAboveText;
            _tsbSave.Click += TsbSave_Click;

            // _tsbSeparator1
            _tsbSeparator1.Name = "_tsbSeparator1";
            _tsbSeparator1.Size = new Size(6, 56);

            // _tsbStartStop
            _tsbStartStop.DisplayStyle = ToolStripItemDisplayStyle.ImageAndText;
            _tsbStartStop.ImageTransparentColor = Color.Magenta;
            _tsbStartStop.Margin = new Padding(4, 4, 4, 4);
            _tsbStartStop.Name = "_tsbStartStop";
            _tsbStartStop.Padding = new Padding(8, 4, 8, 4);
            _tsbStartStop.Size = new Size(75, 48);
            _tsbStartStop.Text = "Start";
            _tsbStartStop.TextImageRelation = TextImageRelation.ImageAboveText;
            _tsbStartStop.Click += TsbStartStop_Click;

            // _tsbSeparator2
            _tsbSeparator2.Name = "_tsbSeparator2";
            _tsbSeparator2.Size = new Size(6, 56);

            // _tsbExport
            _tsbExport.DisplayStyle = ToolStripItemDisplayStyle.ImageAndText;
            _tsbExport.ImageTransparentColor = Color.Magenta;
            _tsbExport.Margin = new Padding(4, 4, 4, 4);
            _tsbExport.Name = "_tsbExport";
            _tsbExport.Padding = new Padding(8, 4, 8, 4);
            _tsbExport.Size = new Size(75, 48);
            _tsbExport.Text = "Export";
            _tsbExport.TextImageRelation = TextImageRelation.ImageAboveText;
            _tsbExport.Click += TsbExport_Click;

            // _statusStrip
            _statusStrip.ImageScalingSize = new Size(20, 20);
            _statusStrip.Items.AddRange(new ToolStripItem[]
            {
                _sslStatus
            });
            _statusStrip.Location = new Point(0, 742);
            _statusStrip.Name = "_statusStrip";
            _statusStrip.Size = new Size(1024, 26);
            _statusStrip.TabIndex = 2;
            _statusStrip.Text = "statusStrip1";

            // _sslStatus
            _sslStatus.Name = "_sslStatus";
            _sslStatus.Size = new Size(50, 20);
            _sslStatus.Text = "Ready";

            // _pnlContent
            _pnlContent.Dock = DockStyle.Fill;
            _pnlContent.Location = new Point(0, 84);
            _pnlContent.Name = "_pnlContent";
            _pnlContent.Size = new Size(1024, 658);
            _pnlContent.TabIndex = 3;

            // FrmMain
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1024, 768);
            Controls.Add(_pnlContent);
            Controls.Add(_statusStrip);
            Controls.Add(_toolStrip);
            Controls.Add(_menuStrip);
            MainMenuStrip = _menuStrip;
            MinimumSize = new Size(800, 600);
            Name = "FrmMain";
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
        private ToolStripMenuItem _mnuFile;
        private ToolStripMenuItem _mnuFileNew;
        private ToolStripMenuItem _mnuFileOpen;
        private ToolStripSeparator _mnuFileSeparator1;
        private ToolStripMenuItem _mnuFileSave;
        private ToolStripMenuItem _mnuFileSaveAs;
        private ToolStripSeparator _mnuFileSeparator2;
        private ToolStripMenuItem _mnuFileExport;
        private ToolStripSeparator _mnuFileSeparator3;
        private ToolStripMenuItem _mnuFileExit;
        private ToolStripMenuItem _mnuTools;
        private ToolStripMenuItem _mnuToolsSettings;
        private ToolStripMenuItem _mnuHelp;
        private ToolStripMenuItem _mnuHelpAbout;
        private ToolStrip _toolStrip;
        private ToolStripButton _tsbNew;
        private ToolStripButton _tsbOpen;
        private ToolStripButton _tsbSave;
        private ToolStripSeparator _tsbSeparator1;
        private ToolStripButton _tsbStartStop;
        private ToolStripSeparator _tsbSeparator2;
        private ToolStripButton _tsbExport;
        private StatusStrip _statusStrip;
        private ToolStripStatusLabel _sslStatus;
        private Panel _pnlContent;
    }
}
