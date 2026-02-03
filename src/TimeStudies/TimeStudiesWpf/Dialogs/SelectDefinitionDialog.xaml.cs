using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using TimeStudiesWpf.Models;

namespace TimeStudiesWpf.Dialogs;

/// <summary>
/// Interaction logic for SelectDefinitionDialog.xaml
/// </summary>
public partial class SelectDefinitionDialog : Window
{
    public TimeStudyDefinition? SelectedDefinition { get; private set; }

    public SelectDefinitionDialog(IEnumerable<TimeStudyDefinition> definitions)
    {
        InitializeComponent();

        DefinitionsListBox.ItemsSource = definitions.ToList();

        if (DefinitionsListBox.Items.Count > 0)
        {
            DefinitionsListBox.SelectedIndex = 0;
        }
    }

    private void DefinitionsListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        var selected = DefinitionsListBox.SelectedItem as TimeStudyDefinition;
        OpenButton.IsEnabled = selected != null;

        if (selected != null)
        {
            NoSelectionText.Visibility = Visibility.Collapsed;
            PreviewPanel.Visibility = Visibility.Visible;

            PreviewName.Text = selected.Name;
            PreviewDescription.Text = selected.Description;
            PreviewSteps.Text = $"{selected.ProcessSteps.Count} process steps";

            if (selected.IsLocked)
            {
                PreviewSteps.Text += " (Locked)";
            }
        }
        else
        {
            NoSelectionText.Visibility = Visibility.Visible;
            PreviewPanel.Visibility = Visibility.Collapsed;
        }
    }

    private void DefinitionsListBox_MouseDoubleClick(object sender, MouseButtonEventArgs e)
    {
        if (DefinitionsListBox.SelectedItem != null)
        {
            OpenButton_Click(sender, e);
        }
    }

    private void OpenButton_Click(object sender, RoutedEventArgs e)
    {
        SelectedDefinition = DefinitionsListBox.SelectedItem as TimeStudyDefinition;
        DialogResult = true;
        Close();
    }

    private void CancelButton_Click(object sender, RoutedEventArgs e)
    {
        DialogResult = false;
        Close();
    }
}
