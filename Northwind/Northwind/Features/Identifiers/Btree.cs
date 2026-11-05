using System;
using Northwind.Models.Entity;
using NUlid;
using NUlid.Rng;

namespace Northwind.Features.Identifiers
{
    // http://127.0.0.1:8080/databases/demo/debug/storage/btree-structure?name=Docs

    public class Btree
    {
        public static void Balanced()
        {
            using var bulk = DocumentStoreHolder.Store.BulkInsert();

            for (int i = 0; i < 100 * 1000; i++)
            {
                bulk.Store(new Employee
                {
                    FirstName = "FirstName #" + i,
                    LastName = "LastName #" + i
                });
            }
        }

        public static void Unbalanced()
        {
            using var bulk = DocumentStoreHolder.Store.BulkInsert();

            for (int i = 0; i < 100 * 1000; i++)
            {
                bulk.Store(new Employee
                {
                    Id = Guid.NewGuid().ToString(),
                    FirstName = "FirstName #" + i,
                    LastName = "LastName #" + i
                });
            }
        }

        public static void Nulids()
        {
            var rng = new MonotonicUlidRng();

            using var bulk = DocumentStoreHolder.Store.BulkInsert();

            for (int i = 0; i < 100 * 1000; i++)
            {
                bulk.Store(new Employee
                {
                    Id = Ulid.NewUlid(rng).ToString(),
                    FirstName = "FirstName #" + i,
                    LastName = "LastName #" + i
                });
            }
        }

        public static void RTcomb()
        {
            using var bulk = DocumentStoreHolder.Store.BulkInsert();

            for (int i = 0; i < 100 * 1000; i++)
            {
                bulk.Store(new Employee
                {
                    Id = RT.Comb.Provider.PostgreSql.Create().ToString(),
                    FirstName = "FirstName #" + i,
                    LastName = "LastName #" + i
                });
            }
        }

        public static void Youtube()
        {
            using var bulk = DocumentStoreHolder.Store.BulkInsert();

            for (int i = 0; i < 100 * 1000; i++)
            {
                bulk.Store(new Employee
                {
                    Id = YoutubeId.Generate(),
                    FirstName = "FirstName #" + i,
                    LastName = "LastName #" + i
                });
            }
        }
    }
}
