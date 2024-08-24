using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Media;

using Microsoft.Extensions.DependencyInjection;

using S4SentryReporter.Services;

using System;

namespace S4SentryReporter {
    public partial class ReportWindow : Window {
        private readonly SentryService _sentryService;
        private readonly ReportInformation _reportInformation;
        private readonly IServiceProvider _serviceProvider;

        public ReportWindow() {
            InitializeComponent();
        }

        public ReportWindow(SentryService sentryService, ReportInformation reportInformation, IServiceProvider serviceProvider) {
            _sentryService = sentryService;
            _reportInformation = reportInformation;
            _serviceProvider = serviceProvider;

            InitializeComponent();

            ExceptionInfoBlock.Text = _reportInformation.ExceptionName;
        }

        public async void ShowDetails(object? sender, RoutedEventArgs e) {
            DetailsWindow detailsWindow = _serviceProvider.GetRequiredService<DetailsWindow>();
            var background = this.Background;
            this.Background = Brush.Parse("#f1f1f1");
            await detailsWindow.ShowDialog(this);
            this.Background = background;
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