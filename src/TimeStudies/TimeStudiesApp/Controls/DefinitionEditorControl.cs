using TimeStudiesApp.Models;
using TimeStudiesApp.Properties;

namespace TimeStudiesApp.Controls;

/// <summary>
/// User control for editing time study definitions.
/// </summary>
public partial class DefinitionEditorControl : UserControl
{
    private TimeStudyDefinition? _definition;
    private bool _isLoading;
    private bool _hasUnsavedChanges;

    public DefinitionEditorControl()
    {
        InitializeComponent();
        ApplyLocalization();
        InitializeDimensionTypeColumn();
        UpdateButtonStates();
    }

    /// <summary>
    /// Gets or sets the current definition being edited.
    /// </summary>
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
    public bool HasUnsavedChanges => _hasUnsavedChanges;

    /// <summary>
    /// Event raised when the definition is modified.
    /// </summary>
    public event EventHandler? DefinitionChanged;

    private void ApplyLocalization()
    {
        _lblName.Text = Resources.LblDefinitionName + ":";
        _lblDescription.Text = Resources.LblDescription + ":";
        _lblLocked.Text = Resources.LblLocked + ":";
        _grpProcessSteps.Text = Resources.LblProcessStep + "s";

        _colOrderNumber.HeaderText = Resources.LblOrderNumber;
        _colDescription.HeaderText = Resources.LblDescription;
        _colProduct.HeaderText = Resources.LblProduct;
        _colDimensionType.HeaderText = Resources.LblDimensionType;
        _colDimensionUnit.HeaderText = Resources.LblDimensionUnit;
        _colDefaultValue.HeaderText = Resources.LblDefaultValue;
        _colIsDefault.HeaderText = Resources.LblDefaultStep;

        _btnAddStep.Text = Resources.BtnAddStep;
        _btnRemoveStep.Text = Resources.BtnRemoveStep;
        _btnMoveUp.Text = Resources.BtnMoveUp;
        _btnMoveDown.Text = Resources.BtnMoveDown;
        _btnSetAsDefault.Text = Resources.BtnSetAsDefault;
    }

    private void InitializeDimensionTypeColumn()
    {
        _colDimensionType.Items.Clear();
        _colDimensionType.Items.Add(Resources.DimWeight);
        _colDimensionType.Items.Add(Resources.DimCount);
        _colDimensionType.Items.Add(Resources.DimLength);
        _colDimensionType.Items.Add(Resources.DimArea);
        _colDimensionType.Items.Add(Resources.DimVolume);
        _colDimensionType.Items.Add(Resources.DimCustom);
    }

    private void LoadDefinition()
    {
        _isLoading = true;

        try
        {
            _dataGridSteps.Rows.Clear();

            if (_definition is null)
            {
                _txtName.Text = string.Empty;
                _txtDescription.Text = string.Empty;
                _lblLockedValue.Text = Resources.LblNo;
                Enabled = false;
                return;
            }

            Enabled = !_definition.IsLocked;
            _txtName.Text = _definition.Name;
            _txtDescription.Text = _definition.Description;
            _lblLockedValue.Text = _definition.IsLocked ? Resources.LblYes : Resources.LblNo;
            _lblLockedValue.ForeColor = _definition.IsLocked ? Color.Red : SystemColors.ControlText;

            foreach (ProcessStepDefinition step in _definition.ProcessSteps)
            {
                int rowIndex = _dataGridSteps.Rows.Add();
                DataGridViewRow row = _dataGridSteps.Rows[rowIndex];

                row.Cells[_colOrderNumber.Index].Value = step.OrderNumber;
                row.Cells[_colDescription.Index].Value = step.Description;
                row.Cells[_colProduct.Index].Value = step.ProductDescription;
                row.Cells[_colDimensionType.Index].Value = GetDimensionTypeDisplayName(step.DimensionType);
                row.Cells[_colDimensionUnit.Index].Value = step.DimensionUnit;
                row.Cells[_colDefaultValue.Index].Value = step.DefaultDimensionValue;
                row.Cells[_colIsDefault.Index].Value = step.IsDefaultStep;

                if (step.IsDefaultStep)
                {
                    row.DefaultCellStyle.BackColor = Color.LightYellow;
                }

                row.Tag = step;
            }

            _hasUnsavedChanges = false;
            UpdateButtonStates();
        }
        finally
        {
            _isLoading = false;
        }
    }

    private void SaveToDefinition()
    {
        if (_definition is null || _isLoading)
        {
            return;
        }

        _definition.Name = _txtName.Text.Trim();
        _definition.Description = _txtDescription.Text.Trim();
        _definition.ProcessSteps.Clear();

        foreach (DataGridViewRow row in _dataGridSteps.Rows)
        {
            if (row.IsNewRow)
            {
                continue;
            }

            ProcessStepDefinition step = new()
            {
                OrderNumber = ParseInt(row.Cells[_colOrderNumber.Index].Value),
                Description = row.Cells[_colDescription.Index].Value?.ToString() ?? string.Empty,
                ProductDescription = row.Cells[_colProduct.Index].Value?.ToString() ?? string.Empty,
                DimensionType = ParseDimensionType(row.Cells[_colDimensionType.Index].Value?.ToString()),
                DimensionUnit = row.Cells[_colDimensionUnit.Index].Value?.ToString() ?? string.Empty,
                DefaultDimensionValue = ParseDecimal(row.Cells[_colDefaultValue.Index].Value),
                IsDefaultStep = ParseBool(row.Cells[_colIsDefault.Index].Value)
            };

            _definition.ProcessSteps.Add(step);
        }

        // Update default step order number
        ProcessStepDefinition? defaultStep = _definition.ProcessSteps.FirstOrDefault(s => s.IsDefaultStep);

        if (defaultStep is not null)
        {
            _definition.DefaultProcessStepOrderNumber = defaultStep.OrderNumber;
        }

        _definition.ModifiedAt = DateTime.Now;
        _hasUnsavedChanges = true;

        DefinitionChanged?.Invoke(this, EventArgs.Empty);
    }

    private void UpdateButtonStates()
    {
        bool hasSelection = _dataGridSteps.SelectedRows.Count > 0;
        bool hasMultipleRows = _dataGridSteps.Rows.Count > 1;
        bool isLocked = _definition?.IsLocked ?? false;

        _btnRemoveStep.Enabled = hasSelection && hasMultipleRows && !isLocked;
        _btnMoveUp.Enabled = hasSelection && _dataGridSteps.SelectedRows[0].Index > 0 && !isLocked;
        _btnMoveDown.Enabled = hasSelection &&
            _dataGridSteps.SelectedRows[0].Index < _dataGridSteps.Rows.Count - 1 && !isLocked;
        _btnSetAsDefault.Enabled = hasSelection && !isLocked;
        _btnAddStep.Enabled = !isLocked;
    }

    private void HighlightDefaultRow()
    {
        foreach (DataGridViewRow row in _dataGridSteps.Rows)
        {
            bool isDefault = ParseBool(row.Cells[_colIsDefault.Index].Value);
            row.DefaultCellStyle.BackColor = isDefault ? Color.LightYellow : Color.Empty;
        }
    }

    private static string GetDimensionTypeDisplayName(DimensionType type) => type switch
    {
        DimensionType.Weight => Resources.DimWeight,
        DimensionType.Count => Resources.DimCount,
        DimensionType.Length => Resources.DimLength,
        DimensionType.Area => Resources.DimArea,
        DimensionType.Volume => Resources.DimVolume,
        DimensionType.Custom => Resources.DimCustom,
        _ => Resources.DimCount
    };

    private static DimensionType ParseDimensionType(string? displayName)
    {
        if (string.IsNullOrEmpty(displayName))
        {
            return DimensionType.Count;
        }

        if (displayName == Resources.DimWeight) return DimensionType.Weight;
        if (displayName == Resources.DimCount) return DimensionType.Count;
        if (displayName == Resources.DimLength) return DimensionType.Length;
        if (displayName == Resources.DimArea) return DimensionType.Area;
        if (displayName == Resources.DimVolume) return DimensionType.Volume;
        if (displayName == Resources.DimCustom) return DimensionType.Custom;

        return DimensionType.Count;
    }

    private static int ParseInt(object? value)
    {
        if (value is null)
        {
            return 0;
        }

        if (int.TryParse(value.ToString(), out int result))
        {
            return result;
        }

        return 0;
    }

    private static decimal ParseDecimal(object? value)
    {
        if (value is null)
        {
            return 1;
        }

        if (decimal.TryParse(value.ToString(), out decimal result))
        {
            return result;
        }

        return 1;
    }

    private static bool ParseBool(object? value)
    {
        if (value is bool b)
        {
            return b;
        }

        return false;
    }

    // Event handlers

    private void TxtName_TextChanged(object? sender, EventArgs e)
    {
        SaveToDefinition();
    }

    private void TxtDescription_TextChanged(object? sender, EventArgs e)
    {
        SaveToDefinition();
    }

    private void DataGridSteps_CellValueChanged(object? sender, DataGridViewCellEventArgs e)
    {
        if (_isLoading || e.RowIndex < 0)
        {
            return;
        }

        // If the IsDefault column changed, update other rows
        if (e.ColumnIndex == _colIsDefault.Index)
        {
            bool isDefault = ParseBool(_dataGridSteps.Rows[e.RowIndex].Cells[_colIsDefault.Index].Value);

            if (isDefault)
            {
                // Uncheck other rows
                _isLoading = true;

                foreach (DataGridViewRow row in _dataGridSteps.Rows)
                {
                    if (row.Index != e.RowIndex)
                    {
                        row.Cells[_colIsDefault.Index].Value = false;
                    }
                }

                _isLoading = false;
            }

            HighlightDefaultRow();
        }

        SaveToDefinition();
    }

    private void DataGridSteps_SelectionChanged(object? sender, EventArgs e)
    {
        UpdateButtonStates();
    }

    private void BtnAddStep_Click(object? sender, EventArgs e)
    {
        // Find the next available order number
        int maxOrderNumber = 0;

        foreach (DataGridViewRow row in _dataGridSteps.Rows)
        {
            int orderNumber = ParseInt(row.Cells[_colOrderNumber.Index].Value);

            if (orderNumber > maxOrderNumber)
            {
                maxOrderNumber = orderNumber;
            }
        }

        int newOrderNumber = maxOrderNumber + 1;

        int rowIndex = _dataGridSteps.Rows.Add();
        DataGridViewRow newRow = _dataGridSteps.Rows[rowIndex];

        newRow.Cells[_colOrderNumber.Index].Value = newOrderNumber;
        newRow.Cells[_colDescription.Index].Value = $"Step {newOrderNumber}";
        newRow.Cells[_colProduct.Index].Value = string.Empty;
        newRow.Cells[_colDimensionType.Index].Value = Resources.DimCount;
        newRow.Cells[_colDimensionUnit.Index].Value = "pcs";
        newRow.Cells[_colDefaultValue.Index].Value = 1m;
        newRow.Cells[_colIsDefault.Index].Value = false;

        SaveToDefinition();
        _dataGridSteps.CurrentCell = newRow.Cells[_colDescription.Index];
        _dataGridSteps.BeginEdit(true);
    }

    private void BtnRemoveStep_Click(object? sender, EventArgs e)
    {
        if (_dataGridSteps.SelectedRows.Count == 0 || _dataGridSteps.Rows.Count <= 1)
        {
            return;
        }

        _dataGridSteps.Rows.Remove(_dataGridSteps.SelectedRows[0]);
        SaveToDefinition();
    }

    private void BtnMoveUp_Click(object? sender, EventArgs e)
    {
        if (_dataGridSteps.SelectedRows.Count == 0)
        {
            return;
        }

        int index = _dataGridSteps.SelectedRows[0].Index;

        if (index <= 0)
        {
            return;
        }

        SwapRows(index, index - 1);
        _dataGridSteps.Rows[index - 1].Selected = true;
        _dataGridSteps.CurrentCell = _dataGridSteps.Rows[index - 1].Cells[0];
        SaveToDefinition();
    }

    private void BtnMoveDown_Click(object? sender, EventArgs e)
    {
        if (_dataGridSteps.SelectedRows.Count == 0)
        {
            return;
        }

        int index = _dataGridSteps.SelectedRows[0].Index;

        if (index >= _dataGridSteps.Rows.Count - 1)
        {
            return;
        }

        SwapRows(index, index + 1);
        _dataGridSteps.Rows[index + 1].Selected = true;
        _dataGridSteps.CurrentCell = _dataGridSteps.Rows[index + 1].Cells[0];
        SaveToDefinition();
    }

    private void SwapRows(int index1, int index2)
    {
        _isLoading = true;

        DataGridViewRow row1 = _dataGridSteps.Rows[index1];
        DataGridViewRow row2 = _dataGridSteps.Rows[index2];

        object?[] values1 = new object[_dataGridSteps.Columns.Count];
        object?[] values2 = new object[_dataGridSteps.Columns.Count];

        for (int i = 0; i < _dataGridSteps.Columns.Count; i++)
        {
            values1[i] = row1.Cells[i].Value;
            values2[i] = row2.Cells[i].Value;
        }

        for (int i = 0; i < _dataGridSteps.Columns.Count; i++)
        {
            row1.Cells[i].Value = values2[i];
            row2.Cells[i].Value = values1[i];
        }

        // Swap cell styles too
        var style1 = row1.DefaultCellStyle.Clone();
        row1.DefaultCellStyle = row2.DefaultCellStyle;
        row2.DefaultCellStyle = style1;

        _isLoading = false;
    }

    private void BtnSetAsDefault_Click(object? sender, EventArgs e)
    {
        if (_dataGridSteps.SelectedRows.Count == 0)
        {
            return;
        }

        _isLoading = true;

        // Uncheck all rows
        foreach (DataGridViewRow row in _dataGridSteps.Rows)
        {
            row.Cells[_colIsDefault.Index].Value = false;
        }

        // Check selected row
        _dataGridSteps.SelectedRows[0].Cells[_colIsDefault.Index].Value = true;

        _isLoading = false;

        HighlightDefaultRow();
        SaveToDefinition();
    }

    /// <summary>
    /// Marks the definition as saved.
    /// </summary>
    public void MarkAsSaved()
    {
        _hasUnsavedChanges = false;
    }
}
