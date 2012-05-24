using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;
using Windows.Data.Json;

namespace StreetFooClient
{
    public class ReportItem
    {
        [AutoIncrement, PrimaryKey]
        public int Id { get; set; }

        public string NativeId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }

        // updates a local cache of items...
        public static void RefreshCache(Action<RefreshCacheResult> callback)
        {
            // load them from the server (this needs more science in that if the call
            // fails this all falls over)...
            IGetReportsByUserProxy proxy = new GetReportsByUserProxy();
            proxy.GetReportsByUser(async (result) =>
            {
                var toReturn = new RefreshCacheResult();
                if (result.IsOk)
                {
                    // update the cache...
                    var conn = AppRuntime.GetUserDatabase();
                    foreach (var report in result.Reports)
                    {
                        // find it...
                        var existing = conn.Table<ReportItem>().Where(v => v.NativeId == report.NativeId).FirstOrDefault();
                        if (existing != null)
                        {
                            // update it...
                            existing.Title = report.Title;
                            existing.Description = report.Description;
                            existing.Latitude = report.Latitude;
                            existing.Longitude = report.Longitude;

                            // save it...
                            await conn.UpdateAsync(existing);

                            // add...
                            toReturn.Reports.Add(existing);
                        }
                        else
                        {
                            // save it...
                            await conn.InsertAsync(report);

                            // add...
                            toReturn.Reports.Add(report);
                        }
                    }
                }
                else
                    toReturn.SetError(result.Error);

                // ok...
                callback(toReturn);

            });
        }

        internal static ReportItem FromJson(JsonObject report)
        {
            ReportItem item = new ReportItem();
            item.NativeId = report.GetNamedString("_id");
            item.Title = report.GetNamedString("title");
            item.Description = report.GetNamedString("description");
            item.Latitude = report.GetNamedNumber("latitude");
            item.Longitude = report.GetNamedNumber("longitude");

            // return...
            return item;
        }
    }
}
