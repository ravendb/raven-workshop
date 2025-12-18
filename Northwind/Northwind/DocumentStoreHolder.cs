using System;
using System.Security.Cryptography.X509Certificates;
using Raven.Client.Documents;
using Raven.Client.Documents.Indexes;

namespace Northwind
{
    public static class DocumentStoreHolder
    {
        public static IDocumentStore Store => LazyStore.Value;

        public static IDocumentStore GetStore()
        {
            return new DocumentStore
            {
                // Unsecured settings
                Urls = ["http://127.0.0.1:8080"],
                Database = "demo",
            };
        }

        private static readonly Lazy<IDocumentStore> LazyStore =
            new Lazy<IDocumentStore>(() =>
            {
                IDocumentStore store = GetStore();

                store.Initialize();

                //IndexCreation.CreateIndexes(typeof(Program).Assembly, store);

                return store;
            });
    }
}
