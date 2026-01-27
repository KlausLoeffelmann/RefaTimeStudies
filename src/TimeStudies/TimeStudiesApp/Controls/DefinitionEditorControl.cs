using System.ComponentModel;
using TimeStudiesApp.Helpers;
using TimeStudiesApp.Models;
using TimeStudiesApp.Properties;

namespace TimeStudiesApp.Controls;

/// <summary>
/// User control for editing time study definitions.
/// </summary>
public partial class DefinitionEditorControl : UserControl
{
    private TimeStudyDefinition? _definition;
    private bool _isDirty;

    /// <summary>
    /// Event raised when the definition is modified.
    /// </summary>
    public event EventHandler? DefinitionChanged;

    /// <summary>
    /// Gets or sets the current definition being edited.
    /// </summary>
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Browsable(false)]
    public TimeStudyDefinition? Definition
    {
        get => _definition;
        set
        {
            _definition = value;
            LoadDefinition();
            _isDirty = false;
        }
    }

    /// <summary>
    /// Gets whether the definition has unsaved changes.
    /// </summary>
    public bool IsDirty => _isDirty;

    public DefinitionEditorControl()
    {
        InitializeComponent();
        ApplyLocalization();
    }

    private void InitializeComponent()
    {
        SuspendLayout();

        // Main layout
        AutoScroll = true;
        Dock = DockStyle.Fill;
        Padding = new Padding(10);

        // Header panel for definition name/description
        _panelHeader = new Panel
        {
            Dock = DockStyle.Top,
            Height = 100,
            Padding = new Padding(5)
        };

        // Definition Name
        var lblName = new Label
        {
            Text = Resources.LblDefinitionName + ":",
            Location = new Point(5, 10),
            Size = new Size(120, 23),
            TextAlign = ContentAlignment.MiddleLeft
        };

        _txtName = new TextBox
        {
            Location = new Point(130, 8),
            Size = new Size(350, 25)
        };
        _txtName.TextChanged += (s, e) => MarkDirty();

        // Description
        var lblDescription = new Label
        {
            Text = Resources.LblDescription + ":",
            Location = new Point(5, 45),
            Size = new Size(120, 23),
            TextAlign = ContentAlignment.MiddleLeft
        };

        _txtDescription = new TextBox
        {
            Location = new Point(130, 43),
            Size = new Size(350, 25)
        };
        _txtDescription.TextChanged += (s, e) => MarkDirty();

        // Locked indicator
        _lblLocked = new Label
        {
            Location = new Point(5, 75),
            Size = new Size(480, 20),
            ForeColor = Color.OrangeRed,
            Visible = false
        };

        _panelHeader.Controls.AddRange([lblName, _txtName, lblDescription, _txtDescription, _lblLocked]);

        // Process steps group
        _grpSteps = new GroupBox
        {
            Text = Resources.LblProcessSteps,
            Dock = DockStyle.Fill,
            Padding = new Padding(10)
        };

        // DataGridView for process steps
        _dgvSteps = new DataGridView
        {
            Dock = DockStyle.Fill,
            AllowUserToAddRows = false,
            AllowUserToDeleteRows = false,
            AllowUserToResizeRows = false,
            AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
            RowHeadersVisible = false,
            SelectionMode = DataGridViewSelectionMode.FullRowSelect,
            MultiSelect = false,
            EditMode = DataGridViewEditMode.EditOnEnter,
            BackgroundColor = SystemColors.Window,
            BorderStyle = BorderStyle.Fixed3D
        };
        _dgvSteps.CellValueChanged += DgvSteps_CellValueChanged;
        _dgvSteps.CurrentCellDirtyStateChanged += DgvSteps_CurrentCellDirtyStateChanged;

        SetupDataGridColumns();

        // Button panel for step operations
        _panelStepButtons = new Panel
        {
            Dock = DockStyle.Right,
            Width = 130,
            Padding = new Padding(5)
        };

        var buttonY = 10;
        const int buttonHeight = 35;
        const int buttonSpacing = 45;

        _btnAddStep = CreateButton(Resources.BtnAdd, IconFactory.Icons.Add, buttonY);
        _btnAddStep.Click += BtnAddStep_Click;
        buttonY += buttonSpacing;

        _btnRemoveStep = CreateButton(Resources.BtnRemove, IconFactory.Icons.Delete, buttonY);
        _btnRemoveStep.Click += BtnRemoveStep_Click;
        buttonY += buttonSpacing;

        _btnMoveUp = CreateButton(Resources.BtnMoveUp, IconFactory.Icons.Up, buttonY);
        _btnMoveUp.Click += BtnMoveUp_Click;
        buttonY += buttonSpacing;

        _btnMoveDown = CreateButton(Resources.BtnMoveDown, IconFactory.Icons.Down, buttonY);
        _btnMoveDown.Click += BtnMoveDown_Click;
        buttonY += buttonSpacing;

        _btnSetDefault = CreateButton(Resources.BtnSetDefault, IconFactory.Icons.Checkmark, buttonY);
        _btnSetDefault.Click += BtnSetDefault_Click;

        _panelStepButtons.Controls.AddRange([_btnAddStep, _btnRemoveStep, _btnMoveUp, _btnMoveDown, _btnSetDefault]);

        _grpSteps.Controls.Add(_dgvSteps);
        _grpSteps.Controls.Add(_panelStepButtons);

        Controls.Add(_grpSteps);
        Controls.Add(_panelHeader);

        ResumeLayout(true);
    }

    private Button CreateButton(string text, char icon, int y)
    {
        var btn = new Button
        {
            Location = new Point(5, y),
            Size = new Size(115, 35),
            TextImageRelation = TextImageRelation.ImageBeforeText,
            ImageAlign = ContentAlignment.MiddleLeft,
            TextAlign = ContentAlignment.MiddleCenter,
            Text = text,
            Image = IconFactory.CreateIcon(icon, 20, SystemColors.ControlText)
        };
        return btn;
    }

    private void SetupDataGridColumns()
    {
        _dgvSteps.Columns.Clear();

        // Order Number
        var colOrder = new DataGridViewTextBoxColumn
        {
            Name = "OrderNumber",
            HeaderText = Resources.ColOrderNumber,
            Width = 60,
            ValueType = typeof(int)
        };
        _dgvSteps.Columns.Add(colOrder);

        // Description
        var colDesc = new DataGridViewTextBoxColumn
        {
            Name = "Description",
            HeaderText = Resources.ColDescription,
            FillWeight = 100
        };
        _dgvSteps.Columns.Add(colDesc);

        // Product Description
        var colProduct = new DataGridViewTextBoxColumn
        {
            Name = "ProductDescription",
            HeaderText = Resources.ColProduct,
            FillWeight = 80
        };
        _dgvSteps.Columns.Add(colProduct);

        // Dimension Type
        var colDimType = new DataGridViewComboBoxColumn
        {
            Name = "DimensionType",
            HeaderText = Resources.ColDimensionType,
            Width = 100,
            FlatStyle = FlatStyle.Flat
        };
        colDimType.Items.AddRange(
            Resources.DimWeight,
            Resources.DimCount,
            Resources.DimLength,
            Resources.DimArea,
            Resources.DimVolume,
            Resources.DimCustom
        );
        _dgvSteps.Columns.Add(colDimType);

        // Unit
        var colUnit = new DataGridViewTextBoxColumn
        {
            Name = "DimensionUnit",
            HeaderText = Resources.ColUnit,
            Width = 70
        };
        _dgvSteps.Columns.Add(colUnit);

        // Default Value
        var colDefault = new DataGridViewTextBoxColumn
        {
            Name = "DefaultDimensionValue",
            HeaderText = Resources.ColDefaultValue,
            Width = 70,
            ValueType = typeof(decimal)
        };
        _dgvSteps.Columns.Add(colDefault);

        // Is Default Step
        var colIsDefault = new DataGridViewCheckBoxColumn
        {
            Name = "IsDefaultStep",
            HeaderText = Resources.ColIsDefault,
            Width = 80
        };
        _dgvSteps.Columns.Add(colIsDefault);
    }

    private void ApplyLocalization()
    {
        if (_grpSteps != null)
            _grpSteps.Text = Resources.LblProcessSteps;

        if (_dgvSteps?.Columns.Count > 0)
        {
            _dgvSteps.Columns["OrderNumber"].HeaderText = Resources.ColOrderNumber;
            _dgvSteps.Columns["Description"].HeaderText = Resources.ColDescription;
            _dgvSteps.Columns["ProductDescription"].HeaderText = Resources.ColProduct;
            _dgvSteps.Columns["DimensionType"].HeaderText = Resources.ColDimensionType;
            _dgvSteps.Columns["DimensionUnit"].HeaderText = Resources.ColUnit;
            _dgvSteps.Columns["DefaultDimensionValue"].HeaderText = Resources.ColDefaultValue;
            _dgvSteps.Columns["IsDefaultStep"].HeaderText = Resources.ColIsDefault;
        }

        if (_btnAddStep != null) _btnAddStep.Text = Resources.BtnAdd;
        if (_btnRemoveStep != null) _btnRemoveStep.Text = Resources.BtnRemove;
        if (_btnMoveUp != null) _btnMoveUp.Text = Resources.BtnMoveUp;
        if (_btnMoveDown != null) _btnMoveDown.Text = Resources.BtnMoveDown;
        if (_btnSetDefault != null) _btnSetDefault.Text = Resources.BtnSetDefault;
    }

    private void LoadDefinition()
    {
        _dgvSteps.Rows.Clear();

        if (_definition == null)
        {
            _txtName.Text = string.Empty;
            _txtDescription.Text = string.Empty;
            _lblLocked.Visible = false;
            SetControlsEnabled(false);
            return;
        }

        _txtName.Text = _definition.Name;
        _txtDescription.Text = _definition.Description;

        if (_definition.IsLocked)
        {
            _lblLocked.Text = Resources.MsgDefinitionLocked;
            _lblLocked.Visible = true;
            SetControlsEnabled(false);
        }
        else
        {
            _lblLocked.Visible = false;
            SetControlsEnabled(true);
        }

        foreach (var step in _definition.ProcessSteps.OrderBy(s => s.OrderNumber))
        {
            AddStepRow(step);
        }
    }

    private void AddStepRow(ProcessStepDefinition step)
    {
        var rowIndex = _dgvSteps.Rows.Add();
        var row = _dgvSteps.Rows[rowIndex];

        row.Cells["OrderNumber"].Value = step.OrderNumber;
        row.Cells["Description"].Value = step.Description;
        row.Cells["ProductDescription"].Value = step.ProductDescription;
        row.Cells["DimensionType"].Value = GetDimensionTypeDisplayName(step.DimensionType);
        row.Cells["DimensionUnit"].Value = step.DimensionUnit;
        row.Cells["DefaultDimensionValue"].Value = step.DefaultDimensionValue;
        row.Cells["IsDefaultStep"].Value = step.IsDefaultStep;

        if (step.IsDefaultStep)
        {
            row.DefaultCellStyle.BackColor = Color.LightGoldenrodYellow;
        }
    }

    private static string GetDimensionTypeDisplayName(DimensionType type)
    {
        return type switch
        {
            DimensionType.Weight => Resources.DimWeight,
            DimensionType.Count => Resources.DimCount,
            DimensionType.Length => Resources.DimLength,
            DimensionType.Area => Resources.DimArea,
            DimensionType.Volume => Resources.DimVolume,
            DimensionType.Custom => Resources.DimCustom,
            _ => Resources.DimCount
        };
    }

    private static DimensionType GetDimensionTypeFromDisplayName(string displayName)
    {
        if (displayName == Resources.DimWeight) return DimensionType.Weight;
        if (displayName == Resources.DimCount) return DimensionType.Count;
        if (displayName == Resources.DimLength) return DimensionType.Length;
        if (displayName == Resources.DimArea) return DimensionType.Area;
        if (displayName == Resources.DimVolume) return DimensionType.Volume;
        if (displayName == Resources.DimCustom) return DimensionType.Custom;
        return DimensionType.Count;
    }

    private void SetControlsEnabled(bool enabled)
    {
        _txtName.ReadOnly = !enabled;
        _txtDescription.ReadOnly = !enabled;
        _dgvSteps.ReadOnly = !enabled;
        _btnAddStep.Enabled = enabled;
        _btnRemoveStep.Enabled = enabled;
        _btnMoveUp.Enabled = enabled;
        _btnMoveDown.Enabled = enabled;
        _btnSetDefault.Enabled = enabled;
    }

    private void MarkDirty()
    {
        _isDirty = true;
        DefinitionChanged?.Invoke(this, EventArgs.Empty);
    }

    /// <summary>
    /// Saves changes back to the definition object.
    /// </summary>
    public void SaveToDefinition()
    {
        if (_definition == null) return;

        _definition.Name = _txtName.Text.Trim();
        _definition.Description = _txtDescription.Text.Trim();
        _definition.ProcessSteps.Clear();

        foreach (DataGridViewRow row in _dgvSteps.Rows)
        {
            if (row.IsNewRow) continue;

            var step = new ProcessStepDefinition
            {
                OrderNumber = Convert.ToInt32(row.Cells["OrderNumber"].Value ?? 0),
                Description = row.Cells["Description"].Value?.ToString() ?? string.Empty,
                ProductDescription = row.Cells["ProductDescription"].Value?.ToString() ?? string.Empty,
                DimensionType = GetDimensionTypeFromDisplayName(row.Cells["DimensionType"].Value?.ToString() ?? Resources.DimCount),
                DimensionUnit = row.Cells["DimensionUnit"].Value?.ToString() ?? string.Empty,
                DefaultDimensionValue = Convert.ToDecimal(row.Cells["DefaultDimensionValue"].Value ?? 1),
                IsDefaultStep = Convert.ToBoolean(row.Cells["IsDefaultStep"].Value ?? false)
            };

            _definition.ProcessSteps.Add(step);
        }

        var defaultStep = _definition.ProcessSteps.FirstOrDefault(s => s.IsDefaultStep);
        if (defaultStep != null)
        {
            _definition.DefaultProcessStepOrderNumber = defaultStep.OrderNumber;
        }

        _isDirty = false;
    }

    /// <summary>
    /// Validates the current definition and returns any errors.
    /// </summary>
    public List<string> Validate()
    {
        SaveToDefinition();
        return _definition?.Validate() ?? ["No definition loaded."];
    }

    #region Event Handlers

    private void BtnAddStep_Click(object? sender, EventArgs e)
    {
        // Find next available order number
        var maxOrder = 0;
        foreach (DataGridViewRow row in _dgvSteps.Rows)
        {
            if (row.Cells["OrderNumber"].Value is int order && order > maxOrder)
                maxOrder = order;
        }

        var newStep = new ProcessStepDefinition
        {
            OrderNumber = maxOrder + 1,
            Description = "New Step",
            DimensionType = DimensionType.Count,
            DimensionUnit = "pcs",
            DefaultDimensionValue = 1
        };

        AddStepRow(newStep);
        MarkDirty();
    }

    private void BtnRemoveStep_Click(object? sender, EventArgs e)
    {
        if (_dgvSteps.SelectedRows.Count > 0)
        {
            _dgvSteps.Rows.Remove(_dgvSteps.SelectedRows[0]);
            MarkDirty();
        }
    }

    private void BtnMoveUp_Click(object? sender, EventArgs e)
    {
        if (_dgvSteps.SelectedRows.Count > 0)
        {
            var rowIndex = _dgvSteps.SelectedRows[0].Index;
            if (rowIndex > 0)
            {
                var row = _dgvSteps.Rows[rowIndex];
                _dgvSteps.Rows.RemoveAt(rowIndex);
                _dgvSteps.Rows.Insert(rowIndex - 1, row);
                _dgvSteps.ClearSelection();
                _dgvSteps.Rows[rowIndex - 1].Selected = true;
                MarkDirty();
            }
        }
    }

    private void BtnMoveDown_Click(object? sender, EventArgs e)
    {
        if (_dgvSteps.SelectedRows.Count > 0)
        {
            var rowIndex = _dgvSteps.SelectedRows[0].Index;
            if (rowIndex < _dgvSteps.Rows.Count - 1)
            {
                var row = _dgvSteps.Rows[rowIndex];
                _dgvSteps.Rows.RemoveAt(rowIndex);
                _dgvSteps.Rows.Insert(rowIndex + 1, row);
                _dgvSteps.ClearSelection();
                _dgvSteps.Rows[rowIndex + 1].Selected = true;
                MarkDirty();
            }
        }
    }

    private void BtnSetDefault_Click(object? sender, EventArgs e)
    {
        if (_dgvSteps.SelectedRows.Count > 0)
        {
            // Clear all default flags
            foreach (DataGridViewRow row in _dgvSteps.Rows)
            {
                row.Cells["IsDefaultStep"].Value = false;
                row.DefaultCellStyle.BackColor = Color.Empty;
            }

            // Set selected row as default
            var selectedRow = _dgvSteps.SelectedRows[0];
            selectedRow.Cells["IsDefaultStep"].Value = true;
            selectedRow.DefaultCellStyle.BackColor = Color.LightGoldenrodYellow;

            MarkDirty();
            _dgvSteps.Refresh();
        }
    }

    private void DgvSteps_CellValueChanged(object? sender, DataGridViewCellEventArgs e)
    {
        if (e.RowIndex >= 0)
        {
            MarkDirty();

            // Update row highlighting for default step
            if (e.ColumnIndex == _dgvSteps.Columns["IsDefaultStep"].Index)
            {
                var isDefault = Convert.ToBoolean(_dgvSteps.Rows[e.RowIndex].Cells[e.ColumnIndex].Value ?? false);
                if (isDefault)
                {
                    // Clear other defaults
                    foreach (DataGridViewRow row in _dgvSteps.Rows)
                    {
                        if (row.Index != e.RowIndex)
                        {
                            row.Cells["IsDefaultStep"].Value = false;
                            row.DefaultCellStyle.BackColor = Color.Empty;
                        }
                    }
                    _dgvSteps.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.LightGoldenrodYellow;
                }
                else
                {
                    _dgvSteps.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.Empty;
                }
            }
        }
    }

    private void DgvSteps_CurrentCellDirtyStateChanged(object? sender, EventArgs e)
    {
        // Commit checkbox changes immediately
        if (_dgvSteps.IsCurrentCellDirty && _dgvSteps.CurrentCell is DataGridViewCheckBoxCell)
        {
            _dgvSteps.CommitEdit(DataGridViewDataErrorContexts.Commit);
        }
    }

    #endregion

    // Controls
    private Panel _panelHeader = null!;
    private TextBox _txtName = null!;
    private TextBox _txtDescription = null!;
    private Label _lblLocked = null!;
    private GroupBox _grpSteps = null!;
    private DataGridView _dgvSteps = null!;
    private Panel _panelStepButtons = null!;
    private Button _btnAddStep = null!;
    private Button _btnRemoveStep = null!;
    private Button _btnMoveUp = null!;
    private Button _btnMoveDown = null!;
    private Button _btnSetDefault = null!;
}
