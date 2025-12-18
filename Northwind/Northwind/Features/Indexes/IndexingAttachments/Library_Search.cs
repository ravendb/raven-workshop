using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Raven.Client.Documents.Indexes;

namespace Northwind.Features.Indexes.IndexingAttachments
{
    public class Library_Search : AbstractMultiMapIndexCreationTask
    {
        public class Entry
        {
            public string Content { get; set; }
        }

        public Library_Search()
        {
            AddMap<Book>(books =>
                from book in books
                let txts = AttachmentsFor(book)
                            .Where(att => att.Name.EndsWith(".txt"))
                            .Select(att => LoadAttachment(book, att.Name).GetContentAsString())

                from txt in txts
                select new
                {
                    Content = txt
                }
            );

            AddMap<Book>(books =>
                from book in books
                let zips = AttachmentsFor(book)
                    .Where(att => att.Name.EndsWith(".zip"))
                    .Select(att => LoadAttachment(book, att.Name).GetContentAsStream())

                from zip in zips
                let content = Extract.DataFromZipFile(zip)

                select new
                {
                    Content = content
                }
            );

            Index("Content", FieldIndexing.Search);

            AdditionalSources = new Dictionary<string, string>
            {
                {
                    $"{nameof(Extract)}", File.ReadAllText(
                        Path.Combine(AppContext.BaseDirectory, "..", "..", "..", "Features", "Indexes", "IndexingAttachments", $"{nameof(Extract)}.cs"))
                }
            };

            AdditionalAssemblies = [AdditionalAssembly.FromRuntime("System.IO.Compression", ["System.IO.Compression"])];
        }
    }
}
