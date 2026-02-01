using System.Reflection;
using System.Windows;
using TimeStudiesWpf.Localization;

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

    private void Window_Loaded(object sender, RoutedEventArgs e)
    {
        SetLocalizedStrings();
        LoadApplicationInfo();
    }

    private void SetLocalizedStrings()
    {
        Title = ResourceHelper.GetString("TitleAbout");
        CloseButton.Content = ResourceHelper.GetString("BtnClose");
    }

    private void LoadApplicationInfo()
    {
        var assembly = Assembly.GetExecutingAssembly();
        var appVersion = assembly.GetName().Version?.ToString() ?? "1.0.0";

        AppNameLabel.Content = ResourceHelper.GetString("AppName");
        VersionTextBlock.Text = $"{ResourceHelper.GetString("AppVersion")} {appVersion}";
        DescriptionTextBlock.Text = 
            "REFA Time Study Application for Industrial Time Measurement\n" +
            "Designed for touch-enabled devices like Microsoft Surface.";
        CopyrightTextBlock.Text = $"© {DateTime.Now.Year} REFA Time Study. All rights reserved.";
    }

    private void Close_Click(object sender, RoutedEventArgs e)
    {
        Close();
    }
}