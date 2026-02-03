using System.Windows;

namespace TimeStudiesWpf.Dialogs;

/// <summary>
/// Interaction logic for AboutDialog.xaml
/// </summary>
public partial class AboutDialog : Window
{
    public AboutDialog()
    {
        InitializeComponent();
    }

    private void OkButton_Click(object sender, RoutedEventArgs e)
    {
        Close();
    }
}
