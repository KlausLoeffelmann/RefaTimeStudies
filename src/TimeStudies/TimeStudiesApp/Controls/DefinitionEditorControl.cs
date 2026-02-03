using System.ComponentModel;
using TimeStudiesApp.Models;
using TimeStudiesApp.ViewModels;

namespace TimeStudiesApp.Controls;

/// <summary>
/// User control for editing time study definitions.
/// </summary>
public partial class DefinitionEditorControl : UserControl
{
    private readonly DefinitionEditorViewModel _viewModel;
    private readonly BindingSource _bindingSource;
    private bool _isBinding;

    /// <summary>
    /// Event raised when the definition is modified.
    /// </summary>
    public event EventHandler? DefinitionModified;

    public DefinitionEditorControl()
    {
        InitializeComponent();

        _viewModel = new DefinitionEditorViewModel();
        _bindingSource = new BindingSource();

        SetupBindings();
        SetupEventHandlers();
        PopulateDimensionTypeColumn();
    }

    private void SetupBindings()
    {
        _bindingSource.DataSource = _viewModel.ProcessSteps;
        _dataGridView.DataSource = _bindingSource;
    }

    private void SetupEventHandlers()
    {
        _tsbAdd.Click += OnAddClick;
        _tsbRemove.Click += OnRemoveClick;
        _tsbSetDefault.Click += OnSetDefaultClick;
        _tsbMoveUp.Click += OnMoveUpClick;
        _tsbMoveDown.Click += OnMoveDownClick;

        _txtDefinitionName.TextChanged += OnDefinitionNameChanged;
        _txtDescription.TextChanged += OnDescriptionChanged;

        _dataGridView.SelectionChanged += OnSelectionChanged;
        _dataGridView.CellValueChanged += OnCellValueChanged;
        _dataGridView.CellEndEdit += OnCellEndEdit;

        _viewModel.DefinitionModified += OnViewModelModified;
    }

    private void PopulateDimensionTypeColumn()
    {
        _colDimensionType.DataSource = Enum.GetValues(typeof(DimensionType));
    }

    /// <summary>
    /// Loads a definition into the editor.
    /// </summary>
    public void LoadDefinition(TimeStudyDefinition? definition)
    {
        _isBinding = true;
        try
        {
            _viewModel.LoadDefinition(definition);

            if (definition != null)
            {
                _txtDefinitionName.Text = definition.Name;
                _txtDescription.Text = definition.Description;
                _lblLocked.Visible = definition.IsLocked;
            }
            else
            {
                _txtDefinitionName.Text = string.Empty;
                _txtDescription.Text = string.Empty;
                _lblLocked.Visible = false;
            }

            UpdateControlState();
            _bindingSource.ResetBindings(false);
        }
        finally
        {
            _isBinding = false;
        }
    }

    /// <summary>
    /// Gets the current definition from the editor.
    /// </summary>
    public TimeStudyDefinition? GetDefinition()
    {
        _viewModel.SyncToDefinition();
        return _viewModel.Definition;
    }

    private void UpdateControlState()
    {
        bool canModify = _viewModel.CanModify();

        _txtDefinitionName.ReadOnly = !canModify;
        _txtDescription.ReadOnly = !canModify;
        _dataGridView.ReadOnly = !canModify;

        _tsbAdd.Enabled = canModify;
        _tsbRemove.Enabled = _viewModel.CanRemoveStep();
        _tsbSetDefault.Enabled = _viewModel.CanSetAsDefault();
        _tsbMoveUp.Enabled = _viewModel.CanMoveUp();
        _tsbMoveDown.Enabled = _viewModel.CanMoveDown();
    }

    private void OnAddClick(object? sender, EventArgs e)
    {
        _viewModel.AddStep();
        _bindingSource.ResetBindings(false);
        UpdateControlState();
    }

    private void OnRemoveClick(object? sender, EventArgs e)
    {
        _viewModel.RemoveStep();
        _bindingSource.ResetBindings(false);
        UpdateControlState();
    }

    private void OnSetDefaultClick(object? sender, EventArgs e)
    {
        _viewModel.SetAsDefault();
        _bindingSource.ResetBindings(false);
        UpdateControlState();
    }

    private void OnMoveUpClick(object? sender, EventArgs e)
    {
        _viewModel.MoveUp();
        _bindingSource.ResetBindings(false);
        UpdateControlState();
    }

    private void OnMoveDownClick(object? sender, EventArgs e)
    {
        _viewModel.MoveDown();
        _bindingSource.ResetBindings(false);
        UpdateControlState();
    }

    private void OnDefinitionNameChanged(object? sender, EventArgs e)
    {
        if (_isBinding || _viewModel.Definition == null)
            return;

        _viewModel.Definition.Name = _txtDefinitionName.Text;
        DefinitionModified?.Invoke(this, EventArgs.Empty);
    }

    private void OnDescriptionChanged(object? sender, EventArgs e)
    {
        if (_isBinding || _viewModel.Definition == null)
            return;

        _viewModel.Definition.Description = _txtDescription.Text;
        DefinitionModified?.Invoke(this, EventArgs.Empty);
    }

    private void OnSelectionChanged(object? sender, EventArgs e)
    {
        if (_dataGridView.SelectedRows.Count > 0)
        {
            var row = _dataGridView.SelectedRows[0];
            _viewModel.SelectedStep = row.DataBoundItem as ProcessStepDefinition;
        }
        else
        {
            _viewModel.SelectedStep = null;
        }

        UpdateControlState();
    }

    private void OnCellValueChanged(object? sender, DataGridViewCellEventArgs e)
    {
        if (_isBinding)
            return;

        _viewModel.SyncToDefinition();
        DefinitionModified?.Invoke(this, EventArgs.Empty);
    }

    private void OnCellEndEdit(object? sender, DataGridViewCellEventArgs e)
    {
        if (_isBinding)
            return;

        _viewModel.SyncToDefinition();
        DefinitionModified?.Invoke(this, EventArgs.Empty);
    }

    private void OnViewModelModified(object? sender, EventArgs e)
    {
        if (_isBinding)
            return;

        DefinitionModified?.Invoke(this, EventArgs.Empty);
    }

    /// <summary>
    /// Applies localization to the control.
    /// </summary>
    public void ApplyLocalization(
        string lblDefinitionName,
        string lblDescription,
        string btnAdd,
        string btnRemove,
        string btnSetDefault,
        string colOrderNumber,
        string colDescription,
        string colProduct,
        string colDimensionType,
        string colUnit,
        string colDefaultValue,
        string colIsDefault)
    {
        _lblDefinitionName.Text = lblDefinitionName;
        _lblDescription.Text = lblDescription;
        _tsbAdd.Text = btnAdd;
        _tsbRemove.Text = btnRemove;
        _tsbSetDefault.Text = btnSetDefault;
        _colOrderNumber.HeaderText = colOrderNumber;
        _colDescription.HeaderText = colDescription;
        _colProduct.HeaderText = colProduct;
        _colDimensionType.HeaderText = colDimensionType;
        _colUnit.HeaderText = colUnit;
        _colDefaultValue.HeaderText = colDefaultValue;
        _colIsDefault.HeaderText = colIsDefault;
    }
}
