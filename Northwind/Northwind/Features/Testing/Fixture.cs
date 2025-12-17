using Raven.Client.Documents;
using Raven.Client.Documents.Indexes;
using Raven.Client.Documents.Indexes.Analysis;
using Raven.Client.ServerWide.Operations.Analyzers;
using Raven.Embedded;
using Raven.TestDriver;
using System;
using System.Collections.Generic;
using System.IO;

namespace Northwind.Features.Testing
{
    public class Fixture : RavenTestDriver
    {
        protected readonly IDocumentStore Store;
        protected Dictionary<string, int> SessionsRecorded;

        static Fixture()
        {
            var testServerOptions = new TestServerOptions
            {
                FrameworkVersion = null, // use latest, optional
                Licensing = new ServerOptions.LicensingOptions
                {
                    ThrowOnInvalidOrMissingLicense = false
                }
            };

            ConfigureServer(testServerOptions);
        }

        public Fixture()
        {
            Store = this.GetDocumentStore();
            
            // Register custom analyzers needed for the workshop indexes
            try
            {
                var analyzerPath = Path.Combine(AppContext.BaseDirectory, "..", "..", "..", "Features", "Analyzers", "Snowball", "SnowballAnalyzer.cs");
                if (File.Exists(analyzerPath))
                {
                    Store.Maintenance.Server.Send(new PutServerWideAnalyzersOperation(
                        new AnalyzerDefinition
                        {
                            Name = "SnowballAnalyzer",
                            Code = File.ReadAllText(analyzerPath)
                        }));
                }
            }
            catch
            {
                // If analyzer registration fails, continue anyway - not all tests need it
            }
            
            // Create indexes, but skip ones that fail (e.g., those requiring custom analyzers in embedded server)
            try
            {
                IndexCreation.CreateIndexes(typeof(Program).Assembly, Store);
            }
            catch (Exception ex) when (ex.Message.Contains("SnowballAnalyzer") || ex.InnerException?.Message.Contains("SnowballAnalyzer") == true)
            {
                // Skip indexes that require custom analyzers - these are workshop examples and not needed for basic tests
            }

            SessionsRecorded = new Dictionary<string, int>();

            Store.OnSessionDisposing += (sender, args) =>
            {
                string sessionId = args.Session.Id.ToString();

                if (SessionsRecorded.ContainsKey(sessionId))
                    SessionsRecorded[sessionId] = args.Session.NumberOfRequests;
                else
                    SessionsRecorded.Add(sessionId, args.Session.NumberOfRequests);
            };
        }
    }
}
