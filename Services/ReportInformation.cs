using Avalonia;

using Sentry;

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace S4SentryReporter.Services {
    public class ReportInformation {
        public string Reason { get; private set; }
        public SentryEvent? Report { get; private set; }

        public string EventId => Report?.EventId.ToString() ?? "-";
        public string ExceptionName => Report?.SentryExceptions?.First().Value ?? "Unknown error";

        public string NativeStacktrace =>
            Report?.Breadcrumbs?.Where(b => b.Category == "Native Stack Trace").FirstOrDefault()?.Message ?? "";
        public string ManagedStacktrace => Report?.SentryThreads?.First().Stacktrace?.Frames?.Reverse().Aggregate(new StringBuilder(),
            (sb, f) => sb.AppendLine(f.Function)).ToString() ?? "";

        public bool IsErrorReport => Reason == "error";
        public bool IsFeedbackReport => Reason == "feedback";

        public ReportInformation() {
            string[] startArguments = Environment.GetCommandLineArgs().Skip(1).ToArray();

            if (startArguments.Length < 2) {
                Debug.WriteLine("Not enough arguments passed to the application. Exiting.");
                return;
            }

            Reason = startArguments[0];
            string reportFile = startArguments[1];

            try {
                Debug.WriteLine("Arguments passed to the application:");
                Debug.WriteLine($"Event ID: {EventId}");
                Debug.WriteLine($"Report file: {reportFile}");

                Debug.WriteLine("Reading report json file...");
                string reportJson = File.ReadAllText(reportFile);
                Report = SentryEvent.FromJson(JsonDocument.Parse(reportJson).RootElement);

            } catch (Exception e) {
                Debug.WriteLine($"Error reading report file: {e.Message}");
            } finally {
                // Ensure the report file is deleted after reading
                // Could also fail if the file is locked by another process, so try to catch that too
                try {
#if !DEBUG
                    if (File.Exists(reportFile))
                        File.Delete(reportFile);
#endif
                } catch (Exception e) {
                    Debug.WriteLine($"Error deleting report file: {e.Message}");
                }
            }
        }
    }
}
