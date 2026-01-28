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
        _tlpMain = new TableLayoutPanel();
        _tlpHeader = new TableLayoutPanel();
        _lblName = new Label();
        _txtName = new TextBox();
        _lblDescription = new Label();
        _txtDescription = new TextBox();
        _pnlLockedWarning = new Panel();
        _lblLockedWarning = new Label();
        _pnlGridToolbar = new FlowLayoutPanel();
        _btnAdd = new Button();
        _btnRemove = new Button();
        _btnMoveUp = new Button();
        _btnMoveDown = new Button();
        _dgvSteps = new DataGridView();
        _tlpMain.SuspendLayout();
        _tlpHeader.SuspendLayout();
        _pnlLockedWarning.SuspendLayout();
        _pnlGridToolbar.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)_dgvSteps).BeginInit();
        SuspendLayout();
        // 
        // _tlpMain
        // 
        _tlpMain.ColumnCount = 1;
        _tlpMain.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
        _tlpMain.Controls.Add(_tlpHeader, 0, 0);
        _tlpMain.Controls.Add(_pnlLockedWarning, 0, 1);
        _tlpMain.Controls.Add(_pnlGridToolbar, 0, 2);
        _tlpMain.Controls.Add(_dgvSteps, 0, 3);
        _tlpMain.Dock = DockStyle.Fill;
        _tlpMain.Location = new Point(0, 0);
        _tlpMain.Margin = new Padding(4, 4, 4, 4);
        _tlpMain.Name = "_tlpMain";
        _tlpMain.RowCount = 4;
        _tlpMain.RowStyles.Add(new RowStyle());
        _tlpMain.RowStyles.Add(new RowStyle());
        _tlpMain.RowStyles.Add(new RowStyle());
        _tlpMain.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
        _tlpMain.Size = new Size(875, 625);
        _tlpMain.TabIndex = 0;
        // 
        // _tlpHeader
        // 
        _tlpHeader.AutoSize = true;
        _tlpHeader.AutoSizeMode = AutoSizeMode.GrowAndShrink;
        _tlpHeader.ColumnCount = 2;
        _tlpHeader.ColumnStyles.Add(new ColumnStyle());
        _tlpHeader.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
        _tlpHeader.Controls.Add(_lblName, 0, 0);
        _tlpHeader.Controls.Add(_txtName, 1, 0);
        _tlpHeader.Controls.Add(_lblDescription, 0, 1);
        _tlpHeader.Controls.Add(_txtDescription, 1, 1);
        _tlpHeader.Dock = DockStyle.Fill;
        _tlpHeader.Location = new Point(4, 4);
        _tlpHeader.Margin = new Padding(4, 4, 4, 4);
        _tlpHeader.Name = "_tlpHeader";
        _tlpHeader.RowCount = 2;
        _tlpHeader.RowStyles.Add(new RowStyle());
        _tlpHeader.RowStyles.Add(new RowStyle());
        _tlpHeader.Size = new Size(867, 78);
        _tlpHeader.TabIndex = 0;
        // 
        // _lblName
        // 
        _lblName.Anchor = AnchorStyles.Left | AnchorStyles.Right;
        _lblName.AutoSize = true;
        _lblName.Location = new Point(4, 7);
        _lblName.Margin = new Padding(4, 4, 4, 4);
        _lblName.Name = "_lblName";
        _lblName.Size = new Size(141, 25);
        _lblName.TabIndex = 0;
        _lblName.Text = "Definition Name";
        // 
        // _txtName
        // 
        _txtName.Anchor = AnchorStyles.Left | AnchorStyles.Right;
        _txtName.Location = new Point(153, 4);
        _txtName.Margin = new Padding(4, 4, 4, 4);
        _txtName.Name = "_txtName";
        _txtName.Size = new Size(710, 31);
        _txtName.TabIndex = 1;
        _txtName.TextChanged += TxtName_TextChanged;
        // 
        // _lblDescription
        // 
        _lblDescription.Anchor = AnchorStyles.Left | AnchorStyles.Right;
        _lblDescription.AutoSize = true;
        _lblDescription.Location = new Point(4, 46);
        _lblDescription.Margin = new Padding(4, 4, 4, 4);
        _lblDescription.Name = "_lblDescription";
        _lblDescription.Size = new Size(141, 25);
        _lblDescription.TabIndex = 2;
        _lblDescription.Text = "Description";
        // 
        // _txtDescription
        // 
        _txtDescription.Anchor = AnchorStyles.Left | AnchorStyles.Right;
        _txtDescription.Location = new Point(153, 43);
        _txtDescription.Margin = new Padding(4, 4, 4, 4);
        _txtDescription.Name = "_txtDescription";
        _txtDescription.Size = new Size(710, 31);
        _txtDescription.TabIndex = 3;
        _txtDescription.TextChanged += TxtDescription_TextChanged;
        // 
        // _pnlLockedWarning
        // 
        _pnlLockedWarning.AutoSize = true;
        _pnlLockedWarning.AutoSizeMode = AutoSizeMode.GrowAndShrink;
        _pnlLockedWarning.BackColor = SystemColors.Info;
        _pnlLockedWarning.Controls.Add(_lblLockedWarning);
        _pnlLockedWarning.Dock = DockStyle.Fill;
        _pnlLockedWarning.Location = new Point(4, 90);
        _pnlLockedWarning.Margin = new Padding(4, 4, 4, 4);
        _pnlLockedWarning.Name = "_pnlLockedWarning";
        _pnlLockedWarning.Padding = new Padding(10, 10, 10, 10);
        _pnlLockedWarning.Size = new Size(867, 45);
        _pnlLockedWarning.TabIndex = 1;
        _pnlLockedWarning.Visible = false;
        // 
        // _lblLockedWarning
        // 
        _lblLockedWarning.AutoSize = true;
        _lblLockedWarning.Dock = DockStyle.Fill;
        _lblLockedWarning.ForeColor = SystemColors.InfoText;
        _lblLockedWarning.Location = new Point(10, 10);
        _lblLockedWarning.Margin = new Padding(4, 0, 4, 0);
        _lblLockedWarning.Name = "_lblLockedWarning";
        _lblLockedWarning.Size = new Size(573, 25);
        _lblLockedWarning.TabIndex = 0;
        _lblLockedWarning.Text = "This definition is locked because recordings exist. Create a copy to edit.";
        // 
        // _pnlGridToolbar
        // 
        _pnlGridToolbar.AutoSize = true;
        _pnlGridToolbar.AutoSizeMode = AutoSizeMode.GrowAndShrink;
        _pnlGridToolbar.Controls.Add(_btnAdd);
        _pnlGridToolbar.Controls.Add(_btnRemove);
        _pnlGridToolbar.Controls.Add(_btnMoveUp);
        _pnlGridToolbar.Controls.Add(_btnMoveDown);
        _pnlGridToolbar.Dock = DockStyle.Fill;
        _pnlGridToolbar.Location = new Point(4, 143);
        _pnlGridToolbar.Margin = new Padding(4, 4, 4, 4);
        _pnlGridToolbar.Name = "_pnlGridToolbar";
        _pnlGridToolbar.Padding = new Padding(0, 5, 0, 5);
        _pnlGridToolbar.Size = new Size(867, 63);
        _pnlGridToolbar.TabIndex = 2;
        // 
        // _btnAdd
        // 
        _btnAdd.ImageAlign = ContentAlignment.MiddleLeft;
        _btnAdd.Location = new Point(4, 9);
        _btnAdd.Margin = new Padding(4, 4, 4, 4);
        _btnAdd.MinimumSize = new Size(112, 45);
        _btnAdd.Name = "_btnAdd";
        _btnAdd.Size = new Size(112, 45);
        _btnAdd.TabIndex = 0;
        _btnAdd.Text = "Add";
        _btnAdd.TextImageRelation = TextImageRelation.ImageBeforeText;
        _btnAdd.UseVisualStyleBackColor = true;
        _btnAdd.Click += BtnAdd_Click;
        // 
        // _btnRemove
        // 
        _btnRemove.ImageAlign = ContentAlignment.MiddleLeft;
        _btnRemove.Location = new Point(124, 9);
        _btnRemove.Margin = new Padding(4, 4, 4, 4);
        _btnRemove.MinimumSize = new Size(112, 45);
        _btnRemove.Name = "_btnRemove";
        _btnRemove.Size = new Size(112, 45);
        _btnRemove.TabIndex = 1;
        _btnRemove.Text = "Remove";
        _btnRemove.TextImageRelation = TextImageRelation.ImageBeforeText;
        _btnRemove.UseVisualStyleBackColor = true;
        _btnRemove.Click += BtnRemove_Click;
        // 
        // _btnMoveUp
        // 
        _btnMoveUp.ImageAlign = ContentAlignment.MiddleLeft;
        _btnMoveUp.Location = new Point(244, 9);
        _btnMoveUp.Margin = new Padding(4, 4, 4, 4);
        _btnMoveUp.MinimumSize = new Size(112, 45);
        _btnMoveUp.Name = "_btnMoveUp";
        _btnMoveUp.Size = new Size(112, 45);
        _btnMoveUp.TabIndex = 2;
        _btnMoveUp.Text = "Up";
        _btnMoveUp.TextImageRelation = TextImageRelation.ImageBeforeText;
        _btnMoveUp.UseVisualStyleBackColor = true;
        _btnMoveUp.Click += BtnMoveUp_Click;
        // 
        // _btnMoveDown
        // 
        _btnMoveDown.ImageAlign = ContentAlignment.MiddleLeft;
        _btnMoveDown.Location = new Point(364, 9);
        _btnMoveDown.Margin = new Padding(4, 4, 4, 4);
        _btnMoveDown.MinimumSize = new Size(112, 45);
        _btnMoveDown.Name = "_btnMoveDown";
        _btnMoveDown.Size = new Size(112, 45);
        _btnMoveDown.TabIndex = 3;
        _btnMoveDown.Text = "Down";
        _btnMoveDown.TextImageRelation = TextImageRelation.ImageBeforeText;
        _btnMoveDown.UseVisualStyleBackColor = true;
        _btnMoveDown.Click += BtnMoveDown_Click;
        // 
        // _dgvSteps
        // 
        _dgvSteps.AllowUserToAddRows = false;
        _dgvSteps.AllowUserToDeleteRows = false;
        _dgvSteps.BackgroundColor = SystemColors.Window;
        _dgvSteps.BorderStyle = BorderStyle.Fixed3D;
        _dgvSteps.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
        _dgvSteps.Dock = DockStyle.Fill;
        _dgvSteps.Location = new Point(4, 214);
        _dgvSteps.Margin = new Padding(4, 4, 4, 4);
        _dgvSteps.Name = "_dgvSteps";
        _dgvSteps.RowHeadersWidth = 51;
        _dgvSteps.Size = new Size(867, 407);
        _dgvSteps.TabIndex = 3;
        // 
        // DefinitionEditorControl
        // 
        AutoScaleDimensions = new SizeF(10F, 25F);
        AutoScaleMode = AutoScaleMode.Font;
        Controls.Add(_tlpMain);
        Margin = new Padding(4, 4, 4, 4);
        Name = "DefinitionEditorControl";
        Size = new Size(875, 625);
        _tlpMain.ResumeLayout(false);
        _tlpMain.PerformLayout();
        _tlpHeader.ResumeLayout(false);
        _tlpHeader.PerformLayout();
        _pnlLockedWarning.ResumeLayout(false);
        _pnlLockedWarning.PerformLayout();
        _pnlGridToolbar.ResumeLayout(false);
        ((System.ComponentModel.ISupportInitialize)_dgvSteps).EndInit();
        ResumeLayout(false);
    }

    #endregion

    private TableLayoutPanel _tlpMain;
    private TableLayoutPanel _tlpHeader;
    private Label _lblName;
    private TextBox _txtName;
    private Label _lblDescription;
    private TextBox _txtDescription;
    private Panel _pnlLockedWarning;
    private Label _lblLockedWarning;
    private FlowLayoutPanel _pnlGridToolbar;
    private Button _btnAdd;
    private Button _btnRemove;
    private Button _btnMoveUp;
    private Button _btnMoveDown;
    private DataGridView _dgvSteps;
}
