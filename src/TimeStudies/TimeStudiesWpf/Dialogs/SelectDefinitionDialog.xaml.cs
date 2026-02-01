using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using TimeStudiesWpf.Localization;
using TimeStudiesWpf.Models;
using TimeStudiesWpf.Services;

namespace TimeStudiesWpf.Dialogs;

/// <summary>
/// Interaction logic for SelectDefinitionDialog.xaml
/// </summary>
public partial class SelectDefinitionDialog : Window
{
    private readonly DefinitionService _definitionService;
    private TimeStudyDefinition? _selectedDefinition;

    public TimeStudyDefinition? SelectedDefinition => _selectedDefinition;

    public SelectDefinitionDialog()
    {
        InitializeComponent();
        _definitionService = App.DefinitionService;
        _selectedDefinition = null;
    }

    private void Window_Loaded(object sender, RoutedEventArgs e)
    {
        SetLocalizedStrings();
        LoadDefinitions();
    }

    private void SetLocalizedStrings()
    {
        Title = ResourceHelper.GetString("MenuFileOpen");
        HeaderLabel.Content = ResourceHelper.GetString("MenuFileOpen");
        DefinitionsLabel.Content = ResourceHelper.GetString("DefinitionName");
        OpenButton.Content = ResourceHelper.GetString("BtnOpenDefinition");
        CancelButton.Content = ResourceHelper.GetString("BtnCancel");

        // Set DataGrid column headers
        NameColumn.Header = ResourceHelper.GetString("DefinitionName");
        DescriptionColumn.Header = ResourceHelper.GetString("Description");
        CreatedColumn.Header = ResourceHelper.GetString("CreatedAt");
        ModifiedColumn.Header = ResourceHelper.GetString("ModifiedAt");
        LockedColumn.Header = ResourceHelper.GetString("IsLocked");
    }

    private void LoadDefinitions()
    {
        DefinitionsDataGrid.Items.Clear();

        var definitions = _definitionService.GetAllDefinitions();
        foreach (var definition in definitions)
        {
            DefinitionsDataGrid.Items.Add(definition);
        }
    }

    private void DefinitionsDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        if (DefinitionsDataGrid.SelectedItem is TimeStudyDefinition definition)
        {
            _selectedDefinition = definition;
            OpenButton.IsEnabled = true;
        }
        else
        {
            _selectedDefinition = null;
            OpenButton.IsEnabled = false;
        }
    }

    private void DefinitionsDataGrid_MouseDoubleClick(object sender, MouseButtonEventArgs e)
    {
        if (DefinitionsDataGrid.SelectedItem is TimeStudyDefinition definition)
        {
            _selectedDefinition = definition;
            DialogResult = true;
            Close();
        }
    }

    private void Open_Click(object sender, RoutedEventArgs e)
    {
        if (_selectedDefinition != null)
        {
            DialogResult = true;
            Close();
        }
    }

    private void Cancel_Click(object sender, RoutedEventArgs e)
    {
        DialogResult = false;
        Close();
    }
}