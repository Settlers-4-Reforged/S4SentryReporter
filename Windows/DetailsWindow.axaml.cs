using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;

using S4SentryReporter.Services;

using System.Linq;

namespace S4SentryReporter;

public partial class DetailsWindow : Window {

    public DetailsWindow() {
        InitializeComponent();
    }

    public DetailsWindow(ReportInformation report) {
        InitializeComponent();

        CodeText.Text = $"""
                         ###### Native Stack Trace
                         {report.NativeStacktrace}

                         ###### Managed Stack Trace
                         {report.ManagedStacktrace}
                         """;
    }

    public void CloseWindow(object? sender, RoutedEventArgs e) {
        Close();
    }

}