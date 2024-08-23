using Microsoft.Extensions.DependencyInjection;

namespace S4SentryReporter.Services {
    public static class ServiceCollectionExtensions {
        public static void AddCommonServices(this IServiceCollection collection) {
            collection.AddTransient<ReportWindow>();
        }
    }
}
