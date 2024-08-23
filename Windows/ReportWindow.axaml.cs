using Avalonia.Controls;
using Avalonia.Interactivity;

using S4SentryReporter.Services;

namespace S4SentryReporter {
    public partial class ReportWindow : Window {
        private readonly SentryService _sentryService;

        public ReportWindow(SentryService sentryService) {
            _sentryService = sentryService;

            InitializeComponent();
        }

        public void CloseWindow(object? sender, RoutedEventArgs e) {
            Close();
        }

        public void SubmitReport(object? sender, RoutedEventArgs e) {
            _sentryService.SendReport(NameText.Text ?? "", ReasonText.Text ?? "");
            Close();
        }
    }
}