using Raven.Client.Documents.Indexes;
using Hotel.Models;

namespace Hotel.Indexes
{
    internal class Bids_ByRoom : AbstractIndexCreationTask<Bid, Bids_ByRoom.Entry>
    {
        internal class Entry
        {
            public string Room { get; set; }

            public int TotalBids { get; set; }

            public int VipBids { get; set; }

            public List<DateOnly> Days { get; set; }
        }

        public Bids_ByRoom()
        {
            Map = bids => from bid in bids
                let days = bid.End.DayNumber - bid.Start.DayNumber + 1
                select new Entry
                {
                    Room = bid.Room,
                    TotalBids = 1,
                    VipBids = bid.Vip ? 1 : 0,
                    Days = Enumerable.Range(0, days).Select(d => bid.Start.AddDays(d)).ToList()
                };

            Reduce = results => from result in results
                group result by new
                {
                    result.Room
                }
                into g
                select new Entry
                {
                    Room = g.Key.Room,
                    TotalBids = g.Sum(x => x.TotalBids),
                    VipBids = g.Sum(x => x.VipBids),
                    Days = g.SelectMany(x => x.Days).Distinct().OrderBy(x => x).ToList()
                };

            StoreAllFields(FieldStorage.Yes);
        }
    }
}
