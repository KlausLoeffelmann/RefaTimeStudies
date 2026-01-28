using System.ComponentModel;
using TimeStudiesApp.Helpers;
using TimeStudiesApp.Models;
using TimeStudiesApp.Properties;

namespace TimeStudiesApp.Controls;

/// <summary>
/// UserControl for editing time study definitions.
/// </summary>
public partial class DefinitionEditorControl : UserControl
{
    private TimeStudyDefinition? _definition;
    private bool _isDirty;

    /// <summary>
    /// Gets or sets the definition being edited.
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
        }
    }

    /// <summary>
    /// Gets whether there are unsaved changes.
    /// </summary>
    public bool IsDirty => _isDirty;

    /// <summary>
    /// Event raised when the definition is modified.
    /// </summary>
    public event EventHandler? DefinitionChanged;

    public DefinitionEditorControl()
    {
        InitializeComponent();
        ApplyLocalization();
        SetupDataGridView();
        SetupToolbarIcons();
    }

    private void ApplyLocalization()
    {
        _lblName.Text = Resources.LblDefinitionName;
        _lblDescription.Text = Resources.LblDescription;

        _btnAdd.Text = Resources.BtnAdd;
        _btnRemove.Text = Resources.BtnRemove;
        _btnMoveUp.Text = Resources.BtnMoveUp;
        _btnMoveDown.Text = Resources.BtnMoveDown;

        _btnAdd.AccessibleName = Resources.BtnAdd;
        _btnRemove.AccessibleName = Resources.BtnRemove;
        _btnMoveUp.AccessibleName = Resources.BtnMoveUp;
        _btnMoveDown.AccessibleName = Resources.BtnMoveDown;
    }

    private void SetupDataGridView()
    {
        _dgvSteps.AutoGenerateColumns = false;
        _dgvSteps.AllowUserToAddRows = false;
        _dgvSteps.AllowUserToDeleteRows = false;
        _dgvSteps.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        _dgvSteps.MultiSelect = false;
        _dgvSteps.RowHeadersVisible = false;

        // Order Number column
        DataGridViewTextBoxColumn colOrderNumber = new()
        {
            Name = "OrderNumber",
            HeaderText = Resources.ColOrderNumber,
            DataPropertyName = "OrderNumber",
            Width = 60,
            ValueType = typeof(int)
        };
        _dgvSteps.Columns.Add(colOrderNumber);

        // Description column
        DataGridViewTextBoxColumn colDescription = new()
        {
            Name = "Description",
            HeaderText = Resources.ColDescription,
            DataPropertyName = "Description",
            Width = 200,
            AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
        };
        _dgvSteps.Columns.Add(colDescription);

        // Product column
        DataGridViewTextBoxColumn colProduct = new()
        {
            Name = "ProductDescription",
            HeaderText = Resources.ColProduct,
            DataPropertyName = "ProductDescription",
            Width = 120
        };
        _dgvSteps.Columns.Add(colProduct);

        // Dimension Type column
        DataGridViewComboBoxColumn colDimensionType = new()
        {
            Name = "DimensionType",
            HeaderText = Resources.LblDimensionType,
            DataPropertyName = "DimensionType",
            Width = 100,
            DataSource = Enum.GetValues<DimensionType>(),
            DisplayStyle = DataGridViewComboBoxDisplayStyle.DropDownButton,
            FlatStyle = FlatStyle.Flat
        };
        _dgvSteps.Columns.Add(colDimensionType);

        // Unit column
        DataGridViewTextBoxColumn colUnit = new()
        {
            Name = "DimensionUnit",
            HeaderText = Resources.ColUnit,
            DataPropertyName = "DimensionUnit",
            Width = 60
        };
        _dgvSteps.Columns.Add(colUnit);

        // Default Value column
        DataGridViewTextBoxColumn colDefaultValue = new()
        {
            Name = "DefaultDimensionValue",
            HeaderText = Resources.LblDefaultValue,
            DataPropertyName = "DefaultDimensionValue",
            Width = 80,
            ValueType = typeof(decimal)
        };
        _dgvSteps.Columns.Add(colDefaultValue);

        // Is Default Step column
        DataGridViewCheckBoxColumn colIsDefault = new()
        {
            Name = "IsDefaultStep",
            HeaderText = Resources.ColDefault,
            DataPropertyName = "IsDefaultStep",
            Width = 70
        };
        _dgvSteps.Columns.Add(colIsDefault);

        _dgvSteps.CellValueChanged += DgvSteps_CellValueChanged;
        _dgvSteps.CurrentCellDirtyStateChanged += DgvSteps_CurrentCellDirtyStateChanged;
        _dgvSteps.SelectionChanged += DgvSteps_SelectionChanged;
    }

    private void SetupToolbarIcons()
    {
        int iconSize = 20;
        Color foreColor = SystemColors.ControlText;

        _btnAdd.Image = SymbolFactory.RenderSymbol(SymbolFactory.Symbols.Add, iconSize, foreColor);
        _btnRemove.Image = SymbolFactory.RenderSymbol(SymbolFactory.Symbols.Remove, iconSize, foreColor);
        _btnMoveUp.Image = SymbolFactory.RenderSymbol(SymbolFactory.Symbols.Up, iconSize, foreColor);
        _btnMoveDown.Image = SymbolFactory.RenderSymbol(SymbolFactory.Symbols.Down, iconSize, foreColor);
    }

    private void LoadDefinition()
    {
        if (_definition is null)
        {
            _txtName.Text = string.Empty;
            _txtDescription.Text = string.Empty;
            _dgvSteps.DataSource = null;
            SetReadOnly(true);
            return;
        }

        _txtName.Text = _definition.Name;
        _txtDescription.Text = _definition.Description;

        BindingSource bindingSource = new()
        {
            DataSource = _definition.ProcessSteps
        };
        _dgvSteps.DataSource = bindingSource;

        SetReadOnly(_definition.IsLocked);
        _isDirty = false;

        UpdateButtonStates();
    }

    private void SetReadOnly(bool readOnly)
    {
        _txtName.ReadOnly = readOnly;
        _txtDescription.ReadOnly = readOnly;
        _dgvSteps.ReadOnly = readOnly;
        _btnAdd.Enabled = !readOnly;
        _btnRemove.Enabled = !readOnly;
        _btnMoveUp.Enabled = !readOnly;
        _btnMoveDown.Enabled = !readOnly;

        if (readOnly)
        {
            _pnlLockedWarning.Visible = true;
        }
        else
        {
            _pnlLockedWarning.Visible = false;
        }
    }

    private void UpdateButtonStates()
    {
        if (_definition is null || _definition.IsLocked)
        {
            _btnAdd.Enabled = false;
            _btnRemove.Enabled = false;
            _btnMoveUp.Enabled = false;
            _btnMoveDown.Enabled = false;
            return;
        }

        bool hasSelection = _dgvSteps.SelectedRows.Count > 0;
        int selectedIndex = hasSelection ? _dgvSteps.SelectedRows[0].Index : -1;

        _btnAdd.Enabled = true;
        _btnRemove.Enabled = hasSelection && _definition.ProcessSteps.Count > 1;
        _btnMoveUp.Enabled = hasSelection && selectedIndex > 0;
        _btnMoveDown.Enabled = hasSelection && selectedIndex < _definition.ProcessSteps.Count - 1;
    }

    /// <summary>
    /// Saves changes back to the definition object.
    /// </summary>
    public void SaveChanges()
    {
        if (_definition is null)
        {
            return;
        }

        _definition.Name = _txtName.Text.Trim();
        _definition.Description = _txtDescription.Text.Trim();

        // Update default step order number
        ProcessStepDefinition? defaultStep = _definition.ProcessSteps.FirstOrDefault(s => s.IsDefaultStep);

        if (defaultStep is not null)
        {
            _definition.DefaultProcessStepOrderNumber = defaultStep.OrderNumber;
        }

        _isDirty = false;
    }

    /// <summary>
    /// Validates the definition.
    /// </summary>
    /// <returns>List of validation errors.</returns>
    public List<string> Validate()
    {
        if (_definition is null)
        {
            return ["No definition loaded."];
        }

        // Save current changes first
        _definition.Name = _txtName.Text.Trim();
        _definition.Description = _txtDescription.Text.Trim();

        return _definition.Validate();
    }

    private void MarkDirty()
    {
        _isDirty = true;
        DefinitionChanged?.Invoke(this, EventArgs.Empty);
    }

    private void TxtName_TextChanged(object? sender, EventArgs e)
    {
        MarkDirty();
    }

    private void TxtDescription_TextChanged(object? sender, EventArgs e)
    {
        MarkDirty();
    }

    private void DgvSteps_CellValueChanged(object? sender, DataGridViewCellEventArgs e)
    {
        if (e.RowIndex < 0)
        {
            return;
        }

        // Handle IsDefaultStep - ensure only one is selected
        if (_dgvSteps.Columns[e.ColumnIndex].Name == "IsDefaultStep" && _definition is not null)
        {
            object? cellValue = _dgvSteps.Rows[e.RowIndex].Cells[e.ColumnIndex].Value;

            if (cellValue is true)
            {
                // Uncheck all other rows
                for (int i = 0; i < _definition.ProcessSteps.Count; i++)
                {
                    if (i != e.RowIndex)
                    {
                        _definition.ProcessSteps[i].IsDefaultStep = false;
                    }
                }

                _dgvSteps.Refresh();
            }
        }

        MarkDirty();
    }

    private void DgvSteps_CurrentCellDirtyStateChanged(object? sender, EventArgs e)
    {
        // Commit checkbox changes immediately
        if (_dgvSteps.IsCurrentCellDirty &&
            _dgvSteps.CurrentCell is DataGridViewCheckBoxCell)
        {
            _dgvSteps.CommitEdit(DataGridViewDataErrorContexts.Commit);
        }
    }

    private void DgvSteps_SelectionChanged(object? sender, EventArgs e)
    {
        UpdateButtonStates();
    }

    private void BtnAdd_Click(object? sender, EventArgs e)
    {
        if (_definition is null)
        {
            return;
        }

        // Find next available order number
        int maxOrder = _definition.ProcessSteps.Count > 0
            ? _definition.ProcessSteps.Max(s => s.OrderNumber)
            : 0;

        ProcessStepDefinition newStep = new()
        {
            OrderNumber = maxOrder + 1,
            Description = "New Step",
            DimensionType = DimensionType.Count,
            DimensionUnit = "pcs",
            DefaultDimensionValue = 1
        };

        _definition.ProcessSteps.Add(newStep);

        if (_dgvSteps.DataSource is BindingSource bs)
        {
            bs.ResetBindings(false);
        }

        // Select the new row
        _dgvSteps.ClearSelection();

        if (_dgvSteps.Rows.Count > 0)
        {
            _dgvSteps.Rows[^1].Selected = true;
        }

        MarkDirty();
        UpdateButtonStates();
    }

    private void BtnRemove_Click(object? sender, EventArgs e)
    {
        if (_definition is null || _dgvSteps.SelectedRows.Count == 0)
        {
            return;
        }

        if (_definition.ProcessSteps.Count <= 1)
        {
            MessageBox.Show(
                this,
                "At least one process step is required.",
                Resources.TitleWarning,
                MessageBoxButtons.OK,
                MessageBoxIcon.Warning);

            return;
        }

        int selectedIndex = _dgvSteps.SelectedRows[0].Index;
        _definition.ProcessSteps.RemoveAt(selectedIndex);

        if (_dgvSteps.DataSource is BindingSource bs)
        {
            bs.ResetBindings(false);
        }

        MarkDirty();
        UpdateButtonStates();
    }

    private void BtnMoveUp_Click(object? sender, EventArgs e)
    {
        MoveSelectedRow(-1);
    }

    private void BtnMoveDown_Click(object? sender, EventArgs e)
    {
        MoveSelectedRow(1);
    }

    private void MoveSelectedRow(int direction)
    {
        if (_definition is null || _dgvSteps.SelectedRows.Count == 0)
        {
            return;
        }

        int selectedIndex = _dgvSteps.SelectedRows[0].Index;
        int newIndex = selectedIndex + direction;

        if (newIndex < 0 || newIndex >= _definition.ProcessSteps.Count)
        {
            return;
        }

        // Swap the items
        (_definition.ProcessSteps[selectedIndex], _definition.ProcessSteps[newIndex]) =
            (_definition.ProcessSteps[newIndex], _definition.ProcessSteps[selectedIndex]);

        if (_dgvSteps.DataSource is BindingSource bs)
        {
            bs.ResetBindings(false);
        }

        // Keep selection on the moved row
        _dgvSteps.ClearSelection();
        _dgvSteps.Rows[newIndex].Selected = true;

        MarkDirty();
        UpdateButtonStates();
    }
}
