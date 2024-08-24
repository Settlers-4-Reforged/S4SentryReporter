using Microsoft.Extensions.DependencyInjection;

namespace S4SentryReporter.Services {
    public static class ServiceCollectionExtensions {
        public static void AddCommonServices(this IServiceCollection collection) {
            collection.AddSingleton<ReportInformation>();
            collection.AddSingleton<SentryService>();

            collection.AddTransient<ReportWindow>();
            collection.AddTransient<DetailsWindow>();
        }
    }
}
