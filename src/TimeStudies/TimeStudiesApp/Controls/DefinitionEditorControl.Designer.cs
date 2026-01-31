namespace TimeStudiesApp.Controls;

partial class DefinitionEditorControl
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
        _lblDefinitionName = new Label();
        _txtDefinitionName = new TextBox();
        _lblDescription = new Label();
        _txtDescription = new TextBox();
        _dgvProcessSteps = new DataGridView();
        _pnlButtons = new FlowLayoutPanel();
        _btnAddStep = new Button();
        _btnRemoveStep = new Button();
        _btnMoveUp = new Button();
        _btnMoveDown = new Button();
        _pnlLocked = new Panel();
        _lblLocked = new Label();
        _btnCreateCopy = new Button();
        _tableLayoutPanel.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)_dgvProcessSteps).BeginInit();
        _pnlButtons.SuspendLayout();
        _pnlLocked.SuspendLayout();
        SuspendLayout();
        // 
        // _tableLayoutPanel
        // 
        _tableLayoutPanel.ColumnCount = 2;
        _tableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.AutoSize));
        _tableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
        _tableLayoutPanel.Controls.Add(_lblDefinitionName, 0, 0);
        _tableLayoutPanel.Controls.Add(_txtDefinitionName, 1, 0);
        _tableLayoutPanel.Controls.Add(_lblDescription, 0, 1);
        _tableLayoutPanel.Controls.Add(_txtDescription, 1, 1);
        _tableLayoutPanel.Controls.Add(_pnlLocked, 0, 2);
        _tableLayoutPanel.Controls.Add(_dgvProcessSteps, 0, 3);
        _tableLayoutPanel.Controls.Add(_pnlButtons, 0, 4);
        _tableLayoutPanel.Dock = DockStyle.Fill;
        _tableLayoutPanel.Location = new Point(0, 0);
        _tableLayoutPanel.Name = "_tableLayoutPanel";
        _tableLayoutPanel.Padding = new Padding(10);
        _tableLayoutPanel.RowCount = 5;
        _tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.AutoSize));
        _tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.AutoSize));
        _tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.AutoSize));
        _tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
        _tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.AutoSize));
        _tableLayoutPanel.Size = new Size(800, 500);
        _tableLayoutPanel.TabIndex = 0;
        // 
        // _lblDefinitionName
        // 
        _lblDefinitionName.Anchor = AnchorStyles.Left | AnchorStyles.Right;
        _lblDefinitionName.AutoSize = true;
        _lblDefinitionName.Location = new Point(13, 18);
        _lblDefinitionName.Margin = new Padding(3, 8, 10, 8);
        _lblDefinitionName.Name = "_lblDefinitionName";
        _lblDefinitionName.Size = new Size(107, 20);
        _lblDefinitionName.TabIndex = 0;
        _lblDefinitionName.Text = "Definition Name";
        // 
        // _txtDefinitionName
        // 
        _txtDefinitionName.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        _txtDefinitionName.Font = new Font("Segoe UI", 10F);
        _txtDefinitionName.Location = new Point(133, 13);
        _txtDefinitionName.Margin = new Padding(3, 3, 3, 8);
        _txtDefinitionName.Name = "_txtDefinitionName";
        _txtDefinitionName.Size = new Size(654, 30);
        _txtDefinitionName.TabIndex = 1;
        _txtDefinitionName.TextChanged += TxtDefinitionName_TextChanged;
        // 
        // _lblDescription
        // 
        _lblDescription.Anchor = AnchorStyles.Left | AnchorStyles.Right;
        _lblDescription.AutoSize = true;
        _lblDescription.Location = new Point(13, 62);
        _lblDescription.Margin = new Padding(3, 8, 10, 8);
        _lblDescription.Name = "_lblDescription";
        _lblDescription.Size = new Size(107, 20);
        _lblDescription.TabIndex = 2;
        _lblDescription.Text = "Description";
        // 
        // _txtDescription
        // 
        _txtDescription.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        _txtDescription.Font = new Font("Segoe UI", 10F);
        _txtDescription.Location = new Point(133, 54);
        _txtDescription.Margin = new Padding(3, 3, 3, 8);
        _txtDescription.Multiline = true;
        _txtDescription.Name = "_txtDescription";
        _txtDescription.Size = new Size(654, 60);
        _txtDescription.TabIndex = 3;
        _txtDescription.TextChanged += TxtDescription_TextChanged;
        // 
        // _pnlLocked
        // 
        _tableLayoutPanel.SetColumnSpan(_pnlLocked, 2);
        _pnlLocked.Controls.Add(_lblLocked);
        _pnlLocked.Controls.Add(_btnCreateCopy);
        _pnlLocked.Dock = DockStyle.Fill;
        _pnlLocked.Location = new Point(13, 125);
        _pnlLocked.Name = "_pnlLocked";
        _pnlLocked.Size = new Size(774, 40);
        _pnlLocked.TabIndex = 4;
        _pnlLocked.Visible = false;
        // 
        // _lblLocked
        // 
        _lblLocked.AutoSize = true;
        _lblLocked.ForeColor = Color.DarkRed;
        _lblLocked.Location = new Point(3, 10);
        _lblLocked.Name = "_lblLocked";
        _lblLocked.Size = new Size(55, 20);
        _lblLocked.TabIndex = 0;
        _lblLocked.Text = "Locked";
        // 
        // _btnCreateCopy
        // 
        _btnCreateCopy.Anchor = AnchorStyles.Right;
        _btnCreateCopy.AutoSize = true;
        _btnCreateCopy.Location = new Point(654, 5);
        _btnCreateCopy.Name = "_btnCreateCopy";
        _btnCreateCopy.Padding = new Padding(8, 2, 8, 2);
        _btnCreateCopy.Size = new Size(117, 30);
        _btnCreateCopy.TabIndex = 1;
        _btnCreateCopy.Text = "Create Copy";
        _btnCreateCopy.UseVisualStyleBackColor = true;
        _btnCreateCopy.Click += BtnCreateCopy_Click;
        // 
        // _dgvProcessSteps
        // 
        _dgvProcessSteps.AllowUserToAddRows = false;
        _dgvProcessSteps.AllowUserToDeleteRows = false;
        _dgvProcessSteps.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        _dgvProcessSteps.BackgroundColor = SystemColors.Window;
        _dgvProcessSteps.BorderStyle = BorderStyle.Fixed3D;
        _tableLayoutPanel.SetColumnSpan(_dgvProcessSteps, 2);
        _dgvProcessSteps.Dock = DockStyle.Fill;
        _dgvProcessSteps.Location = new Point(13, 171);
        _dgvProcessSteps.MultiSelect = false;
        _dgvProcessSteps.Name = "_dgvProcessSteps";
        _dgvProcessSteps.RowHeadersWidth = 30;
        _dgvProcessSteps.RowTemplate.Height = 28;
        _dgvProcessSteps.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        _dgvProcessSteps.Size = new Size(774, 266);
        _dgvProcessSteps.TabIndex = 5;
        _dgvProcessSteps.CellValueChanged += DgvProcessSteps_CellValueChanged;
        _dgvProcessSteps.CurrentCellDirtyStateChanged += DgvProcessSteps_CurrentCellDirtyStateChanged;
        // 
        // _pnlButtons
        // 
        _tableLayoutPanel.SetColumnSpan(_pnlButtons, 2);
        _pnlButtons.Controls.Add(_btnAddStep);
        _pnlButtons.Controls.Add(_btnRemoveStep);
        _pnlButtons.Controls.Add(_btnMoveUp);
        _pnlButtons.Controls.Add(_btnMoveDown);
        _pnlButtons.Dock = DockStyle.Fill;
        _pnlButtons.Location = new Point(13, 443);
        _pnlButtons.Name = "_pnlButtons";
        _pnlButtons.Padding = new Padding(0, 5, 0, 0);
        _pnlButtons.Size = new Size(774, 44);
        _pnlButtons.TabIndex = 6;
        // 
        // _btnAddStep
        // 
        _btnAddStep.AutoSize = true;
        _btnAddStep.Location = new Point(3, 8);
        _btnAddStep.MinimumSize = new Size(100, 32);
        _btnAddStep.Name = "_btnAddStep";
        _btnAddStep.Padding = new Padding(8, 2, 8, 2);
        _btnAddStep.Size = new Size(100, 32);
        _btnAddStep.TabIndex = 0;
        _btnAddStep.Text = "Add Step";
        _btnAddStep.UseVisualStyleBackColor = true;
        _btnAddStep.Click += BtnAddStep_Click;
        // 
        // _btnRemoveStep
        // 
        _btnRemoveStep.AutoSize = true;
        _btnRemoveStep.Location = new Point(109, 8);
        _btnRemoveStep.MinimumSize = new Size(100, 32);
        _btnRemoveStep.Name = "_btnRemoveStep";
        _btnRemoveStep.Padding = new Padding(8, 2, 8, 2);
        _btnRemoveStep.Size = new Size(118, 32);
        _btnRemoveStep.TabIndex = 1;
        _btnRemoveStep.Text = "Remove Step";
        _btnRemoveStep.UseVisualStyleBackColor = true;
        _btnRemoveStep.Click += BtnRemoveStep_Click;
        // 
        // _btnMoveUp
        // 
        _btnMoveUp.AutoSize = true;
        _btnMoveUp.Location = new Point(233, 8);
        _btnMoveUp.MinimumSize = new Size(100, 32);
        _btnMoveUp.Name = "_btnMoveUp";
        _btnMoveUp.Padding = new Padding(8, 2, 8, 2);
        _btnMoveUp.Size = new Size(100, 32);
        _btnMoveUp.TabIndex = 2;
        _btnMoveUp.Text = "Move Up";
        _btnMoveUp.UseVisualStyleBackColor = true;
        _btnMoveUp.Click += BtnMoveUp_Click;
        // 
        // _btnMoveDown
        // 
        _btnMoveDown.AutoSize = true;
        _btnMoveDown.Location = new Point(339, 8);
        _btnMoveDown.MinimumSize = new Size(100, 32);
        _btnMoveDown.Name = "_btnMoveDown";
        _btnMoveDown.Padding = new Padding(8, 2, 8, 2);
        _btnMoveDown.Size = new Size(107, 32);
        _btnMoveDown.TabIndex = 3;
        _btnMoveDown.Text = "Move Down";
        _btnMoveDown.UseVisualStyleBackColor = true;
        _btnMoveDown.Click += BtnMoveDown_Click;
        // 
        // DefinitionEditorControl
        // 
        AutoScaleDimensions = new SizeF(8F, 20F);
        AutoScaleMode = AutoScaleMode.Font;
        Controls.Add(_tableLayoutPanel);
        Name = "DefinitionEditorControl";
        Size = new Size(800, 500);
        _tableLayoutPanel.ResumeLayout(false);
        _tableLayoutPanel.PerformLayout();
        ((System.ComponentModel.ISupportInitialize)_dgvProcessSteps).EndInit();
        _pnlButtons.ResumeLayout(false);
        _pnlButtons.PerformLayout();
        _pnlLocked.ResumeLayout(false);
        _pnlLocked.PerformLayout();
        ResumeLayout(false);
    }

    #endregion

    private TableLayoutPanel _tableLayoutPanel;
    private Label _lblDefinitionName;
    private TextBox _txtDefinitionName;
    private Label _lblDescription;
    private TextBox _txtDescription;
    private Panel _pnlLocked;
    private Label _lblLocked;
    private Button _btnCreateCopy;
    private DataGridView _dgvProcessSteps;
    private FlowLayoutPanel _pnlButtons;
    private Button _btnAddStep;
    private Button _btnRemoveStep;
    private Button _btnMoveUp;
    private Button _btnMoveDown;
}
