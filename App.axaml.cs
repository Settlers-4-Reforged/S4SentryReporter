using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;

using Microsoft.Extensions.DependencyInjection;

using S4SentryReporter.Services;

namespace S4SentryReporter {
    public partial class App : Application {
        public override void Initialize() {
            AvaloniaXamlLoader.Load(this);
        }

        public override void OnFrameworkInitializationCompleted() {

            ServiceCollection collection = new ServiceCollection();
            collection.AddCommonServices();

            var services = collection.BuildServiceProvider();

            var mainWindow = services.GetRequiredService<ReportWindow>();
            if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop) {
                desktop.MainWindow = mainWindow;
            }

            base.OnFrameworkInitializationCompleted();
        }
    }
}