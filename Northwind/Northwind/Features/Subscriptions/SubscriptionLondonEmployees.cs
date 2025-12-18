using System;
using Raven.Client.Documents.Subscriptions;
using System.Threading.Tasks;
using Northwind.Models.Entity;
using Raven.Client.Exceptions.Documents.Subscriptions;

namespace Northwind.Features.Subscriptions
{
    public class SubscriptionLondonEmployees
    {
        public static async Task Consume()
        {
            // create subscription if it does not exist
            try
            {
                await DocumentStoreHolder.Store.Subscriptions.GetSubscriptionStateAsync("LondonEmployees");
            }
            catch (SubscriptionDoesNotExistException)
            {
                await DocumentStoreHolder.Store.Subscriptions.CreateAsync(new SubscriptionCreationOptions<Employee>
                {
                    Name = "LondonEmployees",
                    Filter = employee => employee.Address.City == "London"
                });
            }

            // consume subscription
            await using var subscription = DocumentStoreHolder.Store.Subscriptions.GetSubscriptionWorker<Employee>(
                new SubscriptionWorkerOptions("LondonEmployees"));
            
            await subscription.Run(batch =>
            {
                Console.WriteLine("Batch start:");
                foreach (var item in batch.Items)
                {
                    Console.WriteLine(item.Id);
                }
                Console.WriteLine("Batch end:");
            });
        }
    }
}
