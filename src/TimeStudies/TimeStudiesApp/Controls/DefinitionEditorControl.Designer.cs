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

    private void InitializeComponent()
    {
        components = new System.ComponentModel.Container();

        _tableLayoutPanel = new TableLayoutPanel();
        _toolStrip = new ToolStrip();
        _tsbAdd = new ToolStripButton();
        _tsbRemove = new ToolStripButton();
        _tsbSetDefault = new ToolStripButton();
        _tsbSeparator1 = new ToolStripSeparator();
        _tsbMoveUp = new ToolStripButton();
        _tsbMoveDown = new ToolStripButton();
        _headerPanel = new Panel();
        _lblDefinitionName = new Label();
        _txtDefinitionName = new TextBox();
        _lblDescription = new Label();
        _txtDescription = new TextBox();
        _lblLocked = new Label();
        _dataGridView = new DataGridView();
        _colOrderNumber = new DataGridViewTextBoxColumn();
        _colDescription = new DataGridViewTextBoxColumn();
        _colProduct = new DataGridViewTextBoxColumn();
        _colDimensionType = new DataGridViewComboBoxColumn();
        _colUnit = new DataGridViewTextBoxColumn();
        _colDefaultValue = new DataGridViewTextBoxColumn();
        _colIsDefault = new DataGridViewCheckBoxColumn();

        _tableLayoutPanel.SuspendLayout();
        _toolStrip.SuspendLayout();
        _headerPanel.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)_dataGridView).BeginInit();
        SuspendLayout();

        // _tableLayoutPanel
        _tableLayoutPanel.ColumnCount = 1;
        _tableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
        _tableLayoutPanel.Controls.Add(_headerPanel, 0, 0);
        _tableLayoutPanel.Controls.Add(_toolStrip, 0, 1);
        _tableLayoutPanel.Controls.Add(_dataGridView, 0, 2);
        _tableLayoutPanel.Dock = DockStyle.Fill;
        _tableLayoutPanel.Location = new Point(0, 0);
        _tableLayoutPanel.Name = "_tableLayoutPanel";
        _tableLayoutPanel.RowCount = 3;
        _tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.AutoSize));
        _tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.AutoSize));
        _tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
        _tableLayoutPanel.Size = new Size(800, 500);
        _tableLayoutPanel.TabIndex = 0;

        // _headerPanel
        _headerPanel.AutoSize = true;
        _headerPanel.Controls.Add(_lblLocked);
        _headerPanel.Controls.Add(_txtDescription);
        _headerPanel.Controls.Add(_lblDescription);
        _headerPanel.Controls.Add(_txtDefinitionName);
        _headerPanel.Controls.Add(_lblDefinitionName);
        _headerPanel.Dock = DockStyle.Fill;
        _headerPanel.Location = new Point(3, 3);
        _headerPanel.Name = "_headerPanel";
        _headerPanel.Padding = new Padding(8);
        _headerPanel.Size = new Size(794, 80);
        _headerPanel.TabIndex = 0;

        // _lblDefinitionName
        _lblDefinitionName.AutoSize = true;
        _lblDefinitionName.Location = new Point(11, 11);
        _lblDefinitionName.Name = "_lblDefinitionName";
        _lblDefinitionName.Size = new Size(95, 15);
        _lblDefinitionName.TabIndex = 0;
        _lblDefinitionName.Text = "Definition Name:";

        // _txtDefinitionName
        _txtDefinitionName.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        _txtDefinitionName.Location = new Point(150, 8);
        _txtDefinitionName.Name = "_txtDefinitionName";
        _txtDefinitionName.Size = new Size(400, 23);
        _txtDefinitionName.TabIndex = 1;

        // _lblDescription
        _lblDescription.AutoSize = true;
        _lblDescription.Location = new Point(11, 40);
        _lblDescription.Name = "_lblDescription";
        _lblDescription.Size = new Size(70, 15);
        _lblDescription.TabIndex = 2;
        _lblDescription.Text = "Description:";

        // _txtDescription
        _txtDescription.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        _txtDescription.Location = new Point(150, 37);
        _txtDescription.Name = "_txtDescription";
        _txtDescription.Size = new Size(400, 23);
        _txtDescription.TabIndex = 3;

        // _lblLocked
        _lblLocked.AutoSize = true;
        _lblLocked.ForeColor = Color.Red;
        _lblLocked.Location = new Point(560, 11);
        _lblLocked.Name = "_lblLocked";
        _lblLocked.Size = new Size(50, 15);
        _lblLocked.TabIndex = 4;
        _lblLocked.Text = "LOCKED";
        _lblLocked.Visible = false;

        // _toolStrip
        _toolStrip.ImageScalingSize = new Size(24, 24);
        _toolStrip.Items.AddRange(new ToolStripItem[] {
            _tsbAdd,
            _tsbRemove,
            _tsbSeparator1,
            _tsbSetDefault,
            _tsbMoveUp,
            _tsbMoveDown
        });
        _toolStrip.Location = new Point(0, 86);
        _toolStrip.Name = "_toolStrip";
        _toolStrip.Size = new Size(800, 31);
        _toolStrip.TabIndex = 1;

        // _tsbAdd
        _tsbAdd.DisplayStyle = ToolStripItemDisplayStyle.Text;
        _tsbAdd.Name = "_tsbAdd";
        _tsbAdd.Size = new Size(35, 28);
        _tsbAdd.Text = "Add";
        _tsbAdd.ToolTipText = "Add new process step";

        // _tsbRemove
        _tsbRemove.DisplayStyle = ToolStripItemDisplayStyle.Text;
        _tsbRemove.Name = "_tsbRemove";
        _tsbRemove.Size = new Size(57, 28);
        _tsbRemove.Text = "Remove";
        _tsbRemove.ToolTipText = "Remove selected process step";

        // _tsbSeparator1
        _tsbSeparator1.Name = "_tsbSeparator1";
        _tsbSeparator1.Size = new Size(6, 28);

        // _tsbSetDefault
        _tsbSetDefault.DisplayStyle = ToolStripItemDisplayStyle.Text;
        _tsbSetDefault.Name = "_tsbSetDefault";
        _tsbSetDefault.Size = new Size(89, 28);
        _tsbSetDefault.Text = "Set as Default";
        _tsbSetDefault.ToolTipText = "Set selected step as the default step";

        // _tsbMoveUp
        _tsbMoveUp.DisplayStyle = ToolStripItemDisplayStyle.Text;
        _tsbMoveUp.Name = "_tsbMoveUp";
        _tsbMoveUp.Size = new Size(29, 28);
        _tsbMoveUp.Text = "Up";
        _tsbMoveUp.ToolTipText = "Move selected step up";

        // _tsbMoveDown
        _tsbMoveDown.DisplayStyle = ToolStripItemDisplayStyle.Text;
        _tsbMoveDown.Name = "_tsbMoveDown";
        _tsbMoveDown.Size = new Size(45, 28);
        _tsbMoveDown.Text = "Down";
        _tsbMoveDown.ToolTipText = "Move selected step down";

        // _dataGridView
        _dataGridView.AllowUserToAddRows = false;
        _dataGridView.AllowUserToDeleteRows = false;
        _dataGridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        _dataGridView.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
        _dataGridView.Columns.AddRange(new DataGridViewColumn[] {
            _colOrderNumber,
            _colDescription,
            _colProduct,
            _colDimensionType,
            _colUnit,
            _colDefaultValue,
            _colIsDefault
        });
        _dataGridView.Dock = DockStyle.Fill;
        _dataGridView.Location = new Point(3, 120);
        _dataGridView.MultiSelect = false;
        _dataGridView.Name = "_dataGridView";
        _dataGridView.RowHeadersWidth = 30;
        _dataGridView.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        _dataGridView.Size = new Size(794, 377);
        _dataGridView.TabIndex = 2;

        // _colOrderNumber
        _colOrderNumber.DataPropertyName = "OrderNumber";
        _colOrderNumber.FillWeight = 60F;
        _colOrderNumber.HeaderText = "Order #";
        _colOrderNumber.MinimumWidth = 60;
        _colOrderNumber.Name = "_colOrderNumber";

        // _colDescription
        _colDescription.DataPropertyName = "Description";
        _colDescription.FillWeight = 150F;
        _colDescription.HeaderText = "Description";
        _colDescription.MinimumWidth = 100;
        _colDescription.Name = "_colDescription";

        // _colProduct
        _colProduct.DataPropertyName = "ProductDescription";
        _colProduct.FillWeight = 100F;
        _colProduct.HeaderText = "Product";
        _colProduct.MinimumWidth = 80;
        _colProduct.Name = "_colProduct";

        // _colDimensionType
        _colDimensionType.DataPropertyName = "DimensionType";
        _colDimensionType.FillWeight = 80F;
        _colDimensionType.HeaderText = "Dimension Type";
        _colDimensionType.MinimumWidth = 80;
        _colDimensionType.Name = "_colDimensionType";

        // _colUnit
        _colUnit.DataPropertyName = "DimensionUnit";
        _colUnit.FillWeight = 60F;
        _colUnit.HeaderText = "Unit";
        _colUnit.MinimumWidth = 50;
        _colUnit.Name = "_colUnit";

        // _colDefaultValue
        _colDefaultValue.DataPropertyName = "DefaultDimensionValue";
        _colDefaultValue.FillWeight = 70F;
        _colDefaultValue.HeaderText = "Default Value";
        _colDefaultValue.MinimumWidth = 60;
        _colDefaultValue.Name = "_colDefaultValue";

        // _colIsDefault
        _colIsDefault.DataPropertyName = "IsDefaultStep";
        _colIsDefault.FillWeight = 50F;
        _colIsDefault.HeaderText = "Default";
        _colIsDefault.MinimumWidth = 50;
        _colIsDefault.Name = "_colIsDefault";
        _colIsDefault.ReadOnly = true;

        // DefinitionEditorControl
        AutoScaleDimensions = new SizeF(7F, 15F);
        AutoScaleMode = AutoScaleMode.Font;
        Controls.Add(_tableLayoutPanel);
        Name = "DefinitionEditorControl";
        Size = new Size(800, 500);

        _tableLayoutPanel.ResumeLayout(false);
        _tableLayoutPanel.PerformLayout();
        _toolStrip.ResumeLayout(false);
        _toolStrip.PerformLayout();
        _headerPanel.ResumeLayout(false);
        _headerPanel.PerformLayout();
        ((System.ComponentModel.ISupportInitialize)_dataGridView).EndInit();
        ResumeLayout(false);
    }

    private TableLayoutPanel _tableLayoutPanel;
    private ToolStrip _toolStrip;
    private ToolStripButton _tsbAdd;
    private ToolStripButton _tsbRemove;
    private ToolStripButton _tsbSetDefault;
    private ToolStripSeparator _tsbSeparator1;
    private ToolStripButton _tsbMoveUp;
    private ToolStripButton _tsbMoveDown;
    private Panel _headerPanel;
    private Label _lblDefinitionName;
    private TextBox _txtDefinitionName;
    private Label _lblDescription;
    private TextBox _txtDescription;
    private Label _lblLocked;
    private DataGridView _dataGridView;
    private DataGridViewTextBoxColumn _colOrderNumber;
    private DataGridViewTextBoxColumn _colDescription;
    private DataGridViewTextBoxColumn _colProduct;
    private DataGridViewComboBoxColumn _colDimensionType;
    private DataGridViewTextBoxColumn _colUnit;
    private DataGridViewTextBoxColumn _colDefaultValue;
    private DataGridViewCheckBoxColumn _colIsDefault;
}
