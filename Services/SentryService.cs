using Sentry;
using Sentry.Infrastructure;

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace S4SentryReporter.Services {
    public class SentryService {
        private readonly ReportInformation _report;

        class DebugLogger : DiagnosticLogger {
            public DebugLogger(SentryLevel minimalLevel) : base(minimalLevel) { }
            protected override void LogMessage(string message) {
                Debug.WriteLine(message);
            }
        }

        public SentryService(ReportInformation report) {
            _report = report;

            SentrySdk.Init(config => {
                config.Dsn =
                    "https://a5d9124fef748e98c437e552e17f77bc@o4503905572225024.ingest.us.sentry.io/4506394193494016";
                config.DisableAppDomainUnhandledExceptionCapture();
                config.DisableDiagnosticSourceIntegration();
                config.DisableSystemDiagnosticsMetricsIntegration();
                config.DisableWinUiUnhandledExceptionIntegration();
                config.AutoSessionTracking = false;
                config.AttachStacktrace = false;
            });

        }

        public void SendReport(string name, string info) {
            SentryId sentryId = SentryId.Parse(_report.EventId);
            SentrySdk.CaptureUserFeedback(sentryId, name, info);
            SentrySdk.Flush(TimeSpan.FromSeconds(2));
        }
    }
}
