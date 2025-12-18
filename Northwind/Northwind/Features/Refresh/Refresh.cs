using System;
using Northwind.Models.Entity;
using Raven.Client;
    
namespace Northwind.Features.Refresh
{
    public class Refresh
    {
        public static void SetRefresh()
        {
            using var session = DocumentStoreHolder.Store.OpenSession();

            var employee = session.Load<Employee>("employees/7-A");
            
            
            session.Advanced.GetMetadataFor(employee)[Constants.Documents.Metadata.Refresh] = DateTime.UtcNow.AddSeconds(30);
            session.SaveChanges();
        }
    }
}
