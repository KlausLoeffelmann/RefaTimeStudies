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
        components = new System.ComponentModel.Container();
        _tableLayoutMain = new TableLayoutPanel();
        _tableLayoutHeader = new TableLayoutPanel();
        _lblName = new Label();
        _txtName = new TextBox();
        _lblDescription = new Label();
        _txtDescription = new TextBox();
        _lblLocked = new Label();
        _lblLockedValue = new Label();
        _grpProcessSteps = new GroupBox();
        _tableLayoutSteps = new TableLayoutPanel();
        _dataGridSteps = new DataGridView();
        _colOrderNumber = new DataGridViewTextBoxColumn();
        _colDescription = new DataGridViewTextBoxColumn();
        _colProduct = new DataGridViewTextBoxColumn();
        _colDimensionType = new DataGridViewComboBoxColumn();
        _colDimensionUnit = new DataGridViewTextBoxColumn();
        _colDefaultValue = new DataGridViewTextBoxColumn();
        _colIsDefault = new DataGridViewCheckBoxColumn();
        _flowLayoutStepButtons = new FlowLayoutPanel();
        _btnAddStep = new Button();
        _btnRemoveStep = new Button();
        _btnMoveUp = new Button();
        _btnMoveDown = new Button();
        _btnSetAsDefault = new Button();

        _tableLayoutMain.SuspendLayout();
        _tableLayoutHeader.SuspendLayout();
        _grpProcessSteps.SuspendLayout();
        _tableLayoutSteps.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)_dataGridSteps).BeginInit();
        _flowLayoutStepButtons.SuspendLayout();
        SuspendLayout();

        // _tableLayoutMain
        _tableLayoutMain.ColumnCount = 1;
        _tableLayoutMain.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
        _tableLayoutMain.Controls.Add(_tableLayoutHeader, 0, 0);
        _tableLayoutMain.Controls.Add(_grpProcessSteps, 0, 1);
        _tableLayoutMain.Dock = DockStyle.Fill;
        _tableLayoutMain.Location = new Point(0, 0);
        _tableLayoutMain.Name = "_tableLayoutMain";
        _tableLayoutMain.Padding = new Padding(6);
        _tableLayoutMain.RowCount = 2;
        _tableLayoutMain.RowStyles.Add(new RowStyle(SizeType.AutoSize));
        _tableLayoutMain.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
        _tableLayoutMain.Size = new Size(800, 600);
        _tableLayoutMain.TabIndex = 0;

        // _tableLayoutHeader
        _tableLayoutHeader.AutoSize = true;
        _tableLayoutHeader.AutoSizeMode = AutoSizeMode.GrowAndShrink;
        _tableLayoutHeader.ColumnCount = 4;
        _tableLayoutHeader.ColumnStyles.Add(new ColumnStyle(SizeType.AutoSize));
        _tableLayoutHeader.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
        _tableLayoutHeader.ColumnStyles.Add(new ColumnStyle(SizeType.AutoSize));
        _tableLayoutHeader.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
        _tableLayoutHeader.Controls.Add(_lblName, 0, 0);
        _tableLayoutHeader.Controls.Add(_txtName, 1, 0);
        _tableLayoutHeader.Controls.Add(_lblDescription, 2, 0);
        _tableLayoutHeader.Controls.Add(_txtDescription, 3, 0);
        _tableLayoutHeader.Controls.Add(_lblLocked, 0, 1);
        _tableLayoutHeader.Controls.Add(_lblLockedValue, 1, 1);
        _tableLayoutHeader.Dock = DockStyle.Fill;
        _tableLayoutHeader.Location = new Point(9, 9);
        _tableLayoutHeader.Name = "_tableLayoutHeader";
        _tableLayoutHeader.RowCount = 2;
        _tableLayoutHeader.RowStyles.Add(new RowStyle(SizeType.AutoSize));
        _tableLayoutHeader.RowStyles.Add(new RowStyle(SizeType.AutoSize));
        _tableLayoutHeader.Size = new Size(782, 66);
        _tableLayoutHeader.TabIndex = 0;

        // _lblName
        _lblName.Anchor = AnchorStyles.Left | AnchorStyles.Right;
        _lblName.AutoSize = true;
        _lblName.Location = new Point(3, 6);
        _lblName.Margin = new Padding(3);
        _lblName.Name = "_lblName";
        _lblName.Size = new Size(52, 20);
        _lblName.TabIndex = 0;
        _lblName.Text = "Name:";

        // _txtName
        _txtName.Anchor = AnchorStyles.Left | AnchorStyles.Right;
        _txtName.Location = new Point(61, 3);
        _txtName.Name = "_txtName";
        _txtName.Size = new Size(324, 27);
        _txtName.TabIndex = 1;
        _txtName.TextChanged += TxtName_TextChanged;

        // _lblDescription
        _lblDescription.Anchor = AnchorStyles.Left | AnchorStyles.Right;
        _lblDescription.AutoSize = true;
        _lblDescription.Location = new Point(391, 6);
        _lblDescription.Margin = new Padding(3);
        _lblDescription.Name = "_lblDescription";
        _lblDescription.Size = new Size(88, 20);
        _lblDescription.TabIndex = 2;
        _lblDescription.Text = "Description:";

        // _txtDescription
        _txtDescription.Anchor = AnchorStyles.Left | AnchorStyles.Right;
        _txtDescription.Location = new Point(485, 3);
        _txtDescription.Name = "_txtDescription";
        _txtDescription.Size = new Size(294, 27);
        _txtDescription.TabIndex = 3;
        _txtDescription.TextChanged += TxtDescription_TextChanged;

        // _lblLocked
        _lblLocked.Anchor = AnchorStyles.Left | AnchorStyles.Right;
        _lblLocked.AutoSize = true;
        _lblLocked.Location = new Point(3, 39);
        _lblLocked.Margin = new Padding(3);
        _lblLocked.Name = "_lblLocked";
        _lblLocked.Size = new Size(52, 20);
        _lblLocked.TabIndex = 4;
        _lblLocked.Text = "Locked:";

        // _lblLockedValue
        _lblLockedValue.Anchor = AnchorStyles.Left | AnchorStyles.Right;
        _lblLockedValue.AutoSize = true;
        _lblLockedValue.Location = new Point(61, 39);
        _lblLockedValue.Margin = new Padding(3);
        _lblLockedValue.Name = "_lblLockedValue";
        _lblLockedValue.Size = new Size(324, 20);
        _lblLockedValue.TabIndex = 5;
        _lblLockedValue.Text = "No";

        // _grpProcessSteps
        _grpProcessSteps.AutoSize = true;
        _grpProcessSteps.AutoSizeMode = AutoSizeMode.GrowAndShrink;
        _grpProcessSteps.Controls.Add(_tableLayoutSteps);
        _grpProcessSteps.Dock = DockStyle.Fill;
        _grpProcessSteps.Location = new Point(9, 81);
        _grpProcessSteps.Name = "_grpProcessSteps";
        _grpProcessSteps.Padding = new Padding(6);
        _grpProcessSteps.Size = new Size(782, 510);
        _grpProcessSteps.TabIndex = 1;
        _grpProcessSteps.TabStop = false;
        _grpProcessSteps.Text = "Process Steps";

        // _tableLayoutSteps
        _tableLayoutSteps.ColumnCount = 1;
        _tableLayoutSteps.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
        _tableLayoutSteps.Controls.Add(_dataGridSteps, 0, 0);
        _tableLayoutSteps.Controls.Add(_flowLayoutStepButtons, 0, 1);
        _tableLayoutSteps.Dock = DockStyle.Fill;
        _tableLayoutSteps.Location = new Point(6, 26);
        _tableLayoutSteps.Name = "_tableLayoutSteps";
        _tableLayoutSteps.RowCount = 2;
        _tableLayoutSteps.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
        _tableLayoutSteps.RowStyles.Add(new RowStyle(SizeType.AutoSize));
        _tableLayoutSteps.Size = new Size(770, 478);
        _tableLayoutSteps.TabIndex = 0;

        // _dataGridSteps
        _dataGridSteps.AllowUserToAddRows = false;
        _dataGridSteps.AllowUserToDeleteRows = false;
        _dataGridSteps.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        _dataGridSteps.BackgroundColor = SystemColors.Window;
        _dataGridSteps.BorderStyle = BorderStyle.Fixed3D;
        _dataGridSteps.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
        _dataGridSteps.Columns.AddRange(new DataGridViewColumn[] { _colOrderNumber, _colDescription, _colProduct, _colDimensionType, _colDimensionUnit, _colDefaultValue, _colIsDefault });
        _dataGridSteps.Dock = DockStyle.Fill;
        _dataGridSteps.Location = new Point(3, 3);
        _dataGridSteps.MultiSelect = false;
        _dataGridSteps.Name = "_dataGridSteps";
        _dataGridSteps.RowHeadersWidth = 30;
        _dataGridSteps.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        _dataGridSteps.Size = new Size(764, 426);
        _dataGridSteps.TabIndex = 0;
        _dataGridSteps.CellValueChanged += DataGridSteps_CellValueChanged;
        _dataGridSteps.SelectionChanged += DataGridSteps_SelectionChanged;

        // _colOrderNumber
        _colOrderNumber.FillWeight = 60F;
        _colOrderNumber.HeaderText = "Order #";
        _colOrderNumber.MinimumWidth = 60;
        _colOrderNumber.Name = "_colOrderNumber";

        // _colDescription
        _colDescription.FillWeight = 150F;
        _colDescription.HeaderText = "Description";
        _colDescription.MinimumWidth = 100;
        _colDescription.Name = "_colDescription";

        // _colProduct
        _colProduct.FillWeight = 120F;
        _colProduct.HeaderText = "Product";
        _colProduct.MinimumWidth = 80;
        _colProduct.Name = "_colProduct";

        // _colDimensionType
        _colDimensionType.FillWeight = 80F;
        _colDimensionType.HeaderText = "Dimension";
        _colDimensionType.MinimumWidth = 80;
        _colDimensionType.Name = "_colDimensionType";

        // _colDimensionUnit
        _colDimensionUnit.FillWeight = 60F;
        _colDimensionUnit.HeaderText = "Unit";
        _colDimensionUnit.MinimumWidth = 50;
        _colDimensionUnit.Name = "_colDimensionUnit";

        // _colDefaultValue
        _colDefaultValue.FillWeight = 70F;
        _colDefaultValue.HeaderText = "Default";
        _colDefaultValue.MinimumWidth = 50;
        _colDefaultValue.Name = "_colDefaultValue";

        // _colIsDefault
        _colIsDefault.FillWeight = 50F;
        _colIsDefault.HeaderText = "Default Step";
        _colIsDefault.MinimumWidth = 50;
        _colIsDefault.Name = "_colIsDefault";
        _colIsDefault.Resizable = DataGridViewTriState.True;
        _colIsDefault.SortMode = DataGridViewColumnSortMode.Automatic;

        // _flowLayoutStepButtons
        _flowLayoutStepButtons.AutoSize = true;
        _flowLayoutStepButtons.AutoSizeMode = AutoSizeMode.GrowAndShrink;
        _flowLayoutStepButtons.Controls.Add(_btnAddStep);
        _flowLayoutStepButtons.Controls.Add(_btnRemoveStep);
        _flowLayoutStepButtons.Controls.Add(_btnMoveUp);
        _flowLayoutStepButtons.Controls.Add(_btnMoveDown);
        _flowLayoutStepButtons.Controls.Add(_btnSetAsDefault);
        _flowLayoutStepButtons.Dock = DockStyle.Fill;
        _flowLayoutStepButtons.Location = new Point(3, 435);
        _flowLayoutStepButtons.Name = "_flowLayoutStepButtons";
        _flowLayoutStepButtons.Padding = new Padding(0, 6, 0, 0);
        _flowLayoutStepButtons.Size = new Size(764, 40);
        _flowLayoutStepButtons.TabIndex = 1;

        // _btnAddStep
        _btnAddStep.Location = new Point(3, 9);
        _btnAddStep.MinimumSize = new Size(100, 28);
        _btnAddStep.Name = "_btnAddStep";
        _btnAddStep.Size = new Size(100, 28);
        _btnAddStep.TabIndex = 0;
        _btnAddStep.Text = "Add Step";
        _btnAddStep.UseVisualStyleBackColor = true;
        _btnAddStep.Click += BtnAddStep_Click;

        // _btnRemoveStep
        _btnRemoveStep.Location = new Point(109, 9);
        _btnRemoveStep.MinimumSize = new Size(100, 28);
        _btnRemoveStep.Name = "_btnRemoveStep";
        _btnRemoveStep.Size = new Size(100, 28);
        _btnRemoveStep.TabIndex = 1;
        _btnRemoveStep.Text = "Remove Step";
        _btnRemoveStep.UseVisualStyleBackColor = true;
        _btnRemoveStep.Click += BtnRemoveStep_Click;

        // _btnMoveUp
        _btnMoveUp.Location = new Point(215, 9);
        _btnMoveUp.MinimumSize = new Size(100, 28);
        _btnMoveUp.Name = "_btnMoveUp";
        _btnMoveUp.Size = new Size(100, 28);
        _btnMoveUp.TabIndex = 2;
        _btnMoveUp.Text = "Move Up";
        _btnMoveUp.UseVisualStyleBackColor = true;
        _btnMoveUp.Click += BtnMoveUp_Click;

        // _btnMoveDown
        _btnMoveDown.Location = new Point(321, 9);
        _btnMoveDown.MinimumSize = new Size(100, 28);
        _btnMoveDown.Name = "_btnMoveDown";
        _btnMoveDown.Size = new Size(100, 28);
        _btnMoveDown.TabIndex = 3;
        _btnMoveDown.Text = "Move Down";
        _btnMoveDown.UseVisualStyleBackColor = true;
        _btnMoveDown.Click += BtnMoveDown_Click;

        // _btnSetAsDefault
        _btnSetAsDefault.Location = new Point(427, 9);
        _btnSetAsDefault.MinimumSize = new Size(120, 28);
        _btnSetAsDefault.Name = "_btnSetAsDefault";
        _btnSetAsDefault.Size = new Size(120, 28);
        _btnSetAsDefault.TabIndex = 4;
        _btnSetAsDefault.Text = "Set as Default";
        _btnSetAsDefault.UseVisualStyleBackColor = true;
        _btnSetAsDefault.Click += BtnSetAsDefault_Click;

        // DefinitionEditorControl
        AutoScaleDimensions = new SizeF(8F, 20F);
        AutoScaleMode = AutoScaleMode.Font;
        Controls.Add(_tableLayoutMain);
        Name = "DefinitionEditorControl";
        Size = new Size(800, 600);
        _tableLayoutMain.ResumeLayout(false);
        _tableLayoutMain.PerformLayout();
        _tableLayoutHeader.ResumeLayout(false);
        _tableLayoutHeader.PerformLayout();
        _grpProcessSteps.ResumeLayout(false);
        _tableLayoutSteps.ResumeLayout(false);
        _tableLayoutSteps.PerformLayout();
        ((System.ComponentModel.ISupportInitialize)_dataGridSteps).EndInit();
        _flowLayoutStepButtons.ResumeLayout(false);
        ResumeLayout(false);
    }

    #endregion

    private TableLayoutPanel _tableLayoutMain;
    private TableLayoutPanel _tableLayoutHeader;
    private Label _lblName;
    private TextBox _txtName;
    private Label _lblDescription;
    private TextBox _txtDescription;
    private Label _lblLocked;
    private Label _lblLockedValue;
    private GroupBox _grpProcessSteps;
    private TableLayoutPanel _tableLayoutSteps;
    private DataGridView _dataGridSteps;
    private DataGridViewTextBoxColumn _colOrderNumber;
    private DataGridViewTextBoxColumn _colDescription;
    private DataGridViewTextBoxColumn _colProduct;
    private DataGridViewComboBoxColumn _colDimensionType;
    private DataGridViewTextBoxColumn _colDimensionUnit;
    private DataGridViewTextBoxColumn _colDefaultValue;
    private DataGridViewCheckBoxColumn _colIsDefault;
    private FlowLayoutPanel _flowLayoutStepButtons;
    private Button _btnAddStep;
    private Button _btnRemoveStep;
    private Button _btnMoveUp;
    private Button _btnMoveDown;
    private Button _btnSetAsDefault;
}
