using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Northwind.Features.AdditionalSources.AveragePrice;
using Northwind.Features.Attachments;
using Northwind.Features.BulkInsert;
using Northwind.Features.Caching;
using Northwind.Features.Changes;
using Northwind.Features.ChangeVector;
using Northwind.Features.Client;
using Northwind.Features.ClusterWideTransactions;
using Northwind.Features.Compression;
using Northwind.Features.Counters;
using Northwind.Features.Create;
using Northwind.Features.CRUD;
using Northwind.Features.Events;
using Northwind.Features.Expiration;
using Northwind.Features.Identifiers;
using Northwind.Features.Includes;
using Northwind.Features.Indexes;
using Northwind.Features.Lazy;
using Northwind.Features.Metadata;
using Northwind.Features.Operations;
using Northwind.Features.OptimisticConcurrency;
using Northwind.Features.Paging;
using Northwind.Features.Patching;
using Northwind.Features.Polymorphism;
using Northwind.Features.Projections;
using Northwind.Features.Refresh;
using Northwind.Features.Revisions;
using Northwind.Features.Search;
using Northwind.Features.DocumentSession;
using Northwind.Features.DynamicGrouping;
using Northwind.Features.Filter;
using Northwind.Features.Sorting;
using Northwind.Features.Spatial;
using Northwind.Features.StaleIndex;
using Northwind.Features.Staleness;
using Northwind.Features.Streaming;
using Northwind.Features.Subscriptions;
using Northwind.Features.Timeseries;
using Northwind.Models.Entity;
using Northwind.Models.ValueObject;
using Raven.Client.Documents;
using Raven.Client.Documents.Indexes;
using Raven.Client.Documents.Linq;
using Raven.Client.Documents.Session;
using Northwind.Features.Facets;
using Northwind.Features.Facets2;
using Northwind.Features.Fanout;
using Northwind.Features.Records;
using Northwind.Features.RequestExecutor;
using Northwind.Features.SearchBasic;
using Northwind.Features.InnerJoin;
using Northwind.Features.BulkInsert.RawJson;
using Northwind.Features.Query;
using Northwind.Features.Customizations;
using Northwind.Features.Indexes.DynamicFields.AllProperties;
using Northwind.Features.Indexes.MultipleResolutions;
using Northwind.Features.Conventions;
using Northwind.Features.Indexes.Recursive;
using Northwind.Features.InnerJoin.DepartmentEmployees;
using Northwind.Features.Indexes.IndexingAttachments;
using Northwind.Features.Indexes.DynamicFields.ProductAttributes;
using Northwind.Features.RawJson;

namespace Northwind
{
    class Program
    {
        static void Main(string[] args)
        {
            using var session = DocumentStoreHolder.Store.OpenSession();
            
            //BulkInsert.DoViaSession();
            //BulkInsert.Do();
            //BulkInsert.DoWithIds();
            
            //Metadata.Create();
            //Metadata.FetchJustMetadata();
            
            //Counters.GetCounters();
            //Counters.UpdateCounters();
            //Counters.CreateCounter();
            
            //Expiration.SetExpiration();
            //Refresh.SetRefresh();

            //await SubscriptionLondonEmployees.Consume();
            
            //Changes.Consume();

            //Caching.DocumentCaching();
            //Caching.QueryCaching();
            //Caching.AggressiveCaching();
            //Caching.AggressiveCachingTwoSessions();
            //Caching.DocumentSessionIdentityMap();
            //Caching.DocumentSessionEvict();
            //Caching.DocumentSessionNotModified();

            //OptimisticConcurrency.UseChangeVector();
            
            //Timeseries.AppendNative();
            
            //Lazy.ManyCalls();
            //Lazy.ManyCallsOptimizedViaLazily();
        }
    }
}
