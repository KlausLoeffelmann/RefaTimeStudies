using System.ComponentModel;
using TimeStudiesApp.Models;
using TimeStudiesApp.Properties;

namespace TimeStudiesApp.Controls;

/// <summary>
///  UserControl for editing time study definitions.
/// </summary>
public partial class DefinitionEditorControl : UserControl
{
    private TimeStudyDefinition _definition;

    /// <summary>
    ///  Occurs when the definition data has changed.
    /// </summary>
    public event EventHandler? DefinitionChanged;

    /// <summary>
    ///  Gets a value indicating whether the definition is locked.
    /// </summary>
    public bool IsLocked => _definition.IsLocked;

    /// <summary>
    ///  Initializes a new instance of the <see cref="DefinitionEditorControl"/> class.
    /// </summary>
    public DefinitionEditorControl()
    {
        _definition = new TimeStudyDefinition();
        InitializeComponent();
        ApplyLocalization();
        SetupDataGridView();
    }

    /// <summary>
    ///  Applies localization to all UI elements.
    /// </summary>
    private void ApplyLocalization()
    {
        _lblDefinitionName.Text = Resources.LblDefinitionName;
        _lblDescription.Text = Resources.LblDescription;
        _btnAddStep.Text = Resources.BtnAddStep;
        _btnRemoveStep.Text = Resources.BtnRemoveStep;
        _btnMoveUp.Text = Resources.BtnMoveUp;
        _btnMoveDown.Text = Resources.BtnMoveDown;
        _btnCreateCopy.Text = Resources.BtnCreateCopy;
        _lblLocked.Text = Resources.LblLocked;
    }

    /// <summary>
    ///  Sets up the DataGridView columns.
    /// </summary>
    private void SetupDataGridView()
    {
        _dgvProcessSteps.AutoGenerateColumns = false;
        _dgvProcessSteps.Columns.Clear();

        // Order Number column
        var colOrderNumber = new DataGridViewTextBoxColumn
        {
            Name = "OrderNumber",
            HeaderText = Resources.LblOrderNumber,
            DataPropertyName = "OrderNumber",
            Width = 60,
            ValueType = typeof(int)
        };
        _dgvProcessSteps.Columns.Add(colOrderNumber);

        // Description column
        var colDescription = new DataGridViewTextBoxColumn
        {
            Name = "Description",
            HeaderText = Resources.LblDescription,
            DataPropertyName = "Description",
            Width = 200,
            AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
        };
        _dgvProcessSteps.Columns.Add(colDescription);

        // Product column
        var colProduct = new DataGridViewTextBoxColumn
        {
            Name = "ProductDescription",
            HeaderText = Resources.LblProduct,
            DataPropertyName = "ProductDescription",
            Width = 150
        };
        _dgvProcessSteps.Columns.Add(colProduct);

        // Dimension Type column
        var colDimensionType = new DataGridViewComboBoxColumn
        {
            Name = "DimensionType",
            HeaderText = Resources.LblDimensionType,
            DataPropertyName = "DimensionType",
            Width = 100,
            DataSource = Enum.GetValues<DimensionType>(),
            DisplayStyle = DataGridViewComboBoxDisplayStyle.DropDownButton
        };
        _dgvProcessSteps.Columns.Add(colDimensionType);

        // Unit column
        var colUnit = new DataGridViewTextBoxColumn
        {
            Name = "DimensionUnit",
            HeaderText = Resources.LblDimensionUnit,
            DataPropertyName = "DimensionUnit",
            Width = 80
        };
        _dgvProcessSteps.Columns.Add(colUnit);

        // Default Value column
        var colDefaultValue = new DataGridViewTextBoxColumn
        {
            Name = "DefaultDimensionValue",
            HeaderText = Resources.LblDefaultValue,
            DataPropertyName = "DefaultDimensionValue",
            Width = 80,
            ValueType = typeof(decimal)
        };
        _dgvProcessSteps.Columns.Add(colDefaultValue);

        // Is Default Step column
        var colIsDefault = new DataGridViewCheckBoxColumn
        {
            Name = "IsDefaultStep",
            HeaderText = Resources.LblDefaultStep,
            DataPropertyName = "IsDefaultStep",
            Width = 80
        };
        _dgvProcessSteps.Columns.Add(colIsDefault);
    }

    /// <summary>
    ///  Loads a definition into the editor.
    /// </summary>
    /// <param name="definition">The definition to load.</param>
    public void LoadDefinition(TimeStudyDefinition definition)
    {
        ArgumentNullException.ThrowIfNull(definition);

        _definition = definition;
        _txtDefinitionName.Text = definition.Name;
        _txtDescription.Text = definition.Description;

        RefreshGrid();
        UpdateLockedState();
    }

    /// <summary>
    ///  Gets the current definition with all edits applied.
    /// </summary>
    /// <returns>The edited definition.</returns>
    public TimeStudyDefinition GetDefinition()
    {
        _definition.Name = _txtDefinitionName.Text;
        _definition.Description = _txtDescription.Text;
        _definition.ModifiedAt = DateTime.Now;

        return _definition;
    }

    /// <summary>
    ///  Refreshes the DataGridView with current process steps.
    /// </summary>
    private void RefreshGrid()
    {
        _dgvProcessSteps.DataSource = null;
        _dgvProcessSteps.DataSource = new BindingList<ProcessStepDefinition>(_definition.ProcessSteps);
    }

    /// <summary>
    ///  Updates the UI based on locked state.
    /// </summary>
    private void UpdateLockedState()
    {
        bool isLocked = _definition.IsLocked;

        _txtDefinitionName.ReadOnly = isLocked;
        _txtDescription.ReadOnly = isLocked;
        _dgvProcessSteps.ReadOnly = isLocked;
        _btnAddStep.Enabled = !isLocked;
        _btnRemoveStep.Enabled = !isLocked;
        _btnMoveUp.Enabled = !isLocked;
        _btnMoveDown.Enabled = !isLocked;
        _lblLocked.Visible = isLocked;
        _btnCreateCopy.Visible = isLocked;

        if (isLocked)
        {
            _dgvProcessSteps.BackgroundColor = SystemColors.Control;
        }
        else
        {
            _dgvProcessSteps.BackgroundColor = SystemColors.Window;
        }
    }

    /// <summary>
    ///  Raises the DefinitionChanged event.
    /// </summary>
    protected virtual void OnDefinitionChanged()
    {
        DefinitionChanged?.Invoke(this, EventArgs.Empty);
    }

    #region Event Handlers

    private void TxtDefinitionName_TextChanged(object? sender, EventArgs e)
    {
        OnDefinitionChanged();
    }

    private void TxtDescription_TextChanged(object? sender, EventArgs e)
    {
        OnDefinitionChanged();
    }

    private void DgvProcessSteps_CellValueChanged(object? sender, DataGridViewCellEventArgs e)
    {
        // Handle IsDefaultStep - ensure only one is checked
        if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
        {
            var column = _dgvProcessSteps.Columns[e.ColumnIndex];
            if (column.Name == "IsDefaultStep")
            {
                var changedStep = _definition.ProcessSteps[e.RowIndex];
                if (changedStep.IsDefaultStep)
                {
                    // Uncheck all others
                    foreach (var step in _definition.ProcessSteps)
                    {
                        if (step != changedStep)
                        {
                            step.IsDefaultStep = false;
                        }
                    }
                    RefreshGrid();
                }
            }
        }

        OnDefinitionChanged();
    }

    private void DgvProcessSteps_CurrentCellDirtyStateChanged(object? sender, EventArgs e)
    {
        // Commit checkbox changes immediately
        if (_dgvProcessSteps.IsCurrentCellDirty)
        {
            _dgvProcessSteps.CommitEdit(DataGridViewDataErrorContexts.Commit);
        }
    }

    private void BtnAddStep_Click(object? sender, EventArgs e)
    {
        int maxOrderNumber = _definition.ProcessSteps.Count > 0
            ? _definition.ProcessSteps.Max(p => p.OrderNumber)
            : 0;

        var newStep = new ProcessStepDefinition
        {
            OrderNumber = maxOrderNumber + 1,
            Description = $"Step {maxOrderNumber + 1}",
            DimensionType = DimensionType.Count,
            DimensionUnit = "pcs",
            DefaultDimensionValue = 1
        };

        _definition.ProcessSteps.Add(newStep);
        RefreshGrid();
        OnDefinitionChanged();

        // Select the new row
        int newRowIndex = _definition.ProcessSteps.Count - 1;
        if (newRowIndex >= 0 && newRowIndex < _dgvProcessSteps.Rows.Count)
        {
            _dgvProcessSteps.ClearSelection();
            _dgvProcessSteps.Rows[newRowIndex].Selected = true;
            _dgvProcessSteps.CurrentCell = _dgvProcessSteps.Rows[newRowIndex].Cells[0];
        }
    }

    private void BtnRemoveStep_Click(object? sender, EventArgs e)
    {
        if (_dgvProcessSteps.SelectedRows.Count == 0 && _dgvProcessSteps.CurrentRow is null)
        {
            return;
        }

        int rowIndex = _dgvProcessSteps.CurrentRow?.Index ?? _dgvProcessSteps.SelectedRows[0].Index;

        if (rowIndex >= 0 && rowIndex < _definition.ProcessSteps.Count)
        {
            var result = MessageBox.Show(
                Resources.MsgConfirmDelete,
                Resources.TitleConfirm,
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                _definition.ProcessSteps.RemoveAt(rowIndex);
                RefreshGrid();
                OnDefinitionChanged();
            }
        }
    }

    private void BtnMoveUp_Click(object? sender, EventArgs e)
    {
        if (_dgvProcessSteps.CurrentRow is null)
        {
            return;
        }

        int rowIndex = _dgvProcessSteps.CurrentRow.Index;

        if (rowIndex > 0 && rowIndex < _definition.ProcessSteps.Count)
        {
            var step = _definition.ProcessSteps[rowIndex];
            _definition.ProcessSteps.RemoveAt(rowIndex);
            _definition.ProcessSteps.Insert(rowIndex - 1, step);
            RefreshGrid();
            OnDefinitionChanged();

            _dgvProcessSteps.ClearSelection();
            _dgvProcessSteps.Rows[rowIndex - 1].Selected = true;
            _dgvProcessSteps.CurrentCell = _dgvProcessSteps.Rows[rowIndex - 1].Cells[0];
        }
    }

    private void BtnMoveDown_Click(object? sender, EventArgs e)
    {
        if (_dgvProcessSteps.CurrentRow is null)
        {
            return;
        }

        int rowIndex = _dgvProcessSteps.CurrentRow.Index;

        if (rowIndex >= 0 && rowIndex < _definition.ProcessSteps.Count - 1)
        {
            var step = _definition.ProcessSteps[rowIndex];
            _definition.ProcessSteps.RemoveAt(rowIndex);
            _definition.ProcessSteps.Insert(rowIndex + 1, step);
            RefreshGrid();
            OnDefinitionChanged();

            _dgvProcessSteps.ClearSelection();
            _dgvProcessSteps.Rows[rowIndex + 1].Selected = true;
            _dgvProcessSteps.CurrentCell = _dgvProcessSteps.Rows[rowIndex + 1].Cells[0];
        }
    }

    private void BtnCreateCopy_Click(object? sender, EventArgs e)
    {
        var copy = _definition.CreateCopy();
        LoadDefinition(copy);
        OnDefinitionChanged();

        MessageBox.Show(
            Resources.TitleCreateCopy,
            Resources.TitleInformation,
            MessageBoxButtons.OK,
            MessageBoxIcon.Information);
    }

    #endregion
}
