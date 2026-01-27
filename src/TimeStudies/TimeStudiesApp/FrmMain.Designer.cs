using TimeStudiesApp.Helpers;

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
            
            SuspendLayout();

            // Form settings
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1200, 800);
            MinimumSize = new Size(900, 600);
            Text = "REFA Time Study";
            StartPosition = FormStartPosition.CenterScreen;

            // Menu Strip
            _menuStrip = new MenuStrip
            {
                Dock = DockStyle.Top
            };

            // File Menu
            _menuFile = new ToolStripMenuItem { Text = "&File" };
            _menuFileNew = new ToolStripMenuItem { Text = "&New Definition", ShortcutKeys = Keys.Control | Keys.N };
            _menuFileNew.Click += MenuFileNew_Click;
            _menuFileOpen = new ToolStripMenuItem { Text = "&Open Definition...", ShortcutKeys = Keys.Control | Keys.O };
            _menuFileOpen.Click += MenuFileOpen_Click;
            _menuFileSave = new ToolStripMenuItem { Text = "&Save Definition", ShortcutKeys = Keys.Control | Keys.S };
            _menuFileSave.Click += MenuFileSave_Click;
            _menuFileSaveAs = new ToolStripMenuItem { Text = "Save Definition &As..." };
            _menuFileSaveAs.Click += MenuFileSaveAs_Click;
            _menuFileExport = new ToolStripMenuItem { Text = "&Export Time Study (CSV)...", ShortcutKeys = Keys.Control | Keys.E };
            _menuFileExport.Click += MenuFileExport_Click;
            _menuFileExit = new ToolStripMenuItem { Text = "E&xit" };
            _menuFileExit.Click += MenuFileExit_Click;

            _menuFile.DropDownItems.AddRange([
                _menuFileNew,
                _menuFileOpen,
                new ToolStripSeparator(),
                _menuFileSave,
                _menuFileSaveAs,
                new ToolStripSeparator(),
                _menuFileExport,
                new ToolStripSeparator(),
                _menuFileExit
            ]);

            // Recording Menu
            _menuRecording = new ToolStripMenuItem { Text = "&Recording" };
            _menuRecordingStart = new ToolStripMenuItem { Text = "&Start Recording", ShortcutKeys = Keys.F5 };
            _menuRecordingStart.Click += MenuRecordingStart_Click;
            _menuRecordingStop = new ToolStripMenuItem { Text = "S&top Recording", ShortcutKeys = Keys.F6 };
            _menuRecordingStop.Click += MenuRecordingStop_Click;
            _menuRecordingPause = new ToolStripMenuItem { Text = "&Pause (Default Step)", ShortcutKeys = Keys.F7 };
            _menuRecordingPause.Click += MenuRecordingPause_Click;

            _menuRecording.DropDownItems.AddRange([
                _menuRecordingStart,
                _menuRecordingStop,
                new ToolStripSeparator(),
                _menuRecordingPause
            ]);

            // Tools Menu
            _menuTools = new ToolStripMenuItem { Text = "&Tools" };
            _menuToolsSettings = new ToolStripMenuItem { Text = "&Settings..." };
            _menuToolsSettings.Click += MenuToolsSettings_Click;
            _menuTools.DropDownItems.Add(_menuToolsSettings);

            // Help Menu
            _menuHelp = new ToolStripMenuItem { Text = "&Help" };
            _menuHelpAbout = new ToolStripMenuItem { Text = "&About..." };
            _menuHelpAbout.Click += MenuHelpAbout_Click;
            _menuHelp.DropDownItems.Add(_menuHelpAbout);

            _menuStrip.Items.AddRange([_menuFile, _menuRecording, _menuTools, _menuHelp]);

            // Tool Strip
            _toolStrip = new ToolStrip
            {
                Dock = DockStyle.Top,
                ImageScalingSize = new Size(32, 32),
                AutoSize = false,
                Height = 50,
                GripStyle = ToolStripGripStyle.Hidden,
                Padding = new Padding(5, 5, 5, 5)
            };

            var buttonSize = new Size(48, 40);

            _tsbNew = new ToolStripButton
            {
                Image = IconFactory.CreateToolbarIcon(IconFactory.Icons.NewDocument),
                DisplayStyle = ToolStripItemDisplayStyle.Image,
                AutoSize = false,
                Size = buttonSize
            };
            _tsbNew.Click += MenuFileNew_Click;

            _tsbOpen = new ToolStripButton
            {
                Image = IconFactory.CreateToolbarIcon(IconFactory.Icons.OpenFile),
                DisplayStyle = ToolStripItemDisplayStyle.Image,
                AutoSize = false,
                Size = buttonSize
            };
            _tsbOpen.Click += MenuFileOpen_Click;

            _tsbSave = new ToolStripButton
            {
                Image = IconFactory.CreateToolbarIcon(IconFactory.Icons.Save),
                DisplayStyle = ToolStripItemDisplayStyle.Image,
                AutoSize = false,
                Size = buttonSize
            };
            _tsbSave.Click += MenuFileSave_Click;

            _tsbExport = new ToolStripButton
            {
                Image = IconFactory.CreateToolbarIcon(IconFactory.Icons.Export),
                DisplayStyle = ToolStripItemDisplayStyle.Image,
                AutoSize = false,
                Size = buttonSize
            };
            _tsbExport.Click += MenuFileExport_Click;

            _tsbStartRecording = new ToolStripButton
            {
                Image = IconFactory.CreateIcon(IconFactory.Icons.Play, 32, Color.Green),
                DisplayStyle = ToolStripItemDisplayStyle.Image,
                AutoSize = false,
                Size = buttonSize
            };
            _tsbStartRecording.Click += MenuRecordingStart_Click;

            _tsbStopRecording = new ToolStripButton
            {
                Image = IconFactory.CreateIcon(IconFactory.Icons.Stop, 32, Color.Red),
                DisplayStyle = ToolStripItemDisplayStyle.Image,
                AutoSize = false,
                Size = buttonSize
            };
            _tsbStopRecording.Click += MenuRecordingStop_Click;

            _toolStrip.Items.AddRange([
                _tsbNew,
                _tsbOpen,
                _tsbSave,
                new ToolStripSeparator(),
                _tsbExport,
                new ToolStripSeparator(),
                _tsbStartRecording,
                _tsbStopRecording
            ]);

            // View Tab Strip (styled as tabs)
            _viewTabStrip = new ToolStrip
            {
                Dock = DockStyle.Top,
                GripStyle = ToolStripGripStyle.Hidden,
                AutoSize = false,
                Height = 35,
                BackColor = Color.FromArgb(240, 240, 240),
                Padding = new Padding(10, 0, 0, 0)
            };

            _tabEditor = new ToolStripButton
            {
                Text = "Definition Editor",
                DisplayStyle = ToolStripItemDisplayStyle.Text,
                CheckOnClick = false,
                Checked = true,
                Font = new Font(Font.FontFamily, 10),
                Padding = new Padding(10, 0, 10, 0)
            };
            _tabEditor.Click += TabEditor_Click;

            _tabRecording = new ToolStripButton
            {
                Text = "Recording",
                DisplayStyle = ToolStripItemDisplayStyle.Text,
                CheckOnClick = false,
                Font = new Font(Font.FontFamily, 10),
                Padding = new Padding(10, 0, 10, 0)
            };
            _tabRecording.Click += TabRecording_Click;

            _tabResults = new ToolStripButton
            {
                Text = "Results",
                DisplayStyle = ToolStripItemDisplayStyle.Text,
                CheckOnClick = false,
                Font = new Font(Font.FontFamily, 10),
                Padding = new Padding(10, 0, 10, 0)
            };
            _tabResults.Click += TabResults_Click;

            _viewTabStrip.Items.AddRange([_tabEditor, _tabRecording, _tabResults]);

            // Status Strip
            _statusStrip = new StatusStrip
            {
                Dock = DockStyle.Bottom
            };

            _statusLabel = new ToolStripStatusLabel
            {
                Text = "Ready",
                Spring = true,
                TextAlign = ContentAlignment.MiddleLeft
            };
            _statusStrip.Items.Add(_statusLabel);

            // Content Panel
            _panelContent = new Panel
            {
                Dock = DockStyle.Fill,
                Padding = new Padding(5)
            };

            // Add controls in order (bottom to top for docking)
            Controls.Add(_panelContent);
            Controls.Add(_viewTabStrip);
            Controls.Add(_toolStrip);
            Controls.Add(_menuStrip);
            Controls.Add(_statusStrip);

            MainMenuStrip = _menuStrip;

            ResumeLayout(true);
        }

        #endregion

        // Menu
        private MenuStrip _menuStrip = null!;
        private ToolStripMenuItem _menuFile = null!;
        private ToolStripMenuItem _menuFileNew = null!;
        private ToolStripMenuItem _menuFileOpen = null!;
        private ToolStripMenuItem _menuFileSave = null!;
        private ToolStripMenuItem _menuFileSaveAs = null!;
        private ToolStripMenuItem _menuFileExport = null!;
        private ToolStripMenuItem _menuFileExit = null!;
        private ToolStripMenuItem _menuRecording = null!;
        private ToolStripMenuItem _menuRecordingStart = null!;
        private ToolStripMenuItem _menuRecordingStop = null!;
        private ToolStripMenuItem _menuRecordingPause = null!;
        private ToolStripMenuItem _menuTools = null!;
        private ToolStripMenuItem _menuToolsSettings = null!;
        private ToolStripMenuItem _menuHelp = null!;
        private ToolStripMenuItem _menuHelpAbout = null!;

        // Toolbar
        private ToolStrip _toolStrip = null!;
        private ToolStripButton _tsbNew = null!;
        private ToolStripButton _tsbOpen = null!;
        private ToolStripButton _tsbSave = null!;
        private ToolStripButton _tsbExport = null!;
        private ToolStripButton _tsbStartRecording = null!;
        private ToolStripButton _tsbStopRecording = null!;

        // View tabs
        private ToolStrip _viewTabStrip = null!;
        private ToolStripButton _tabEditor = null!;
        private ToolStripButton _tabRecording = null!;
        private ToolStripButton _tabResults = null!;

        // Status
        private StatusStrip _statusStrip = null!;
        private ToolStripStatusLabel _statusLabel = null!;

        // Content
        private Panel _panelContent = null!;
    }
}
