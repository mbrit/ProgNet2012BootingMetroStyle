using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Data.Json;

namespace StreetFooClient
{
    public class GetReportsByUserProxy : ServiceProxy, IGetReportsByUserProxy
    {
        public GetReportsByUserProxy()
            : base("GetReportsByUser")
        {
        }

        public void GetReportsByUser(Action<GetReportsForUserResult> success)
        {
            this.Execute<GetReportsForUserResult>(new JsonObject(), (output, result) =>
            {
                // walk...
                if (result.IsOk)
                {
                    string json = output.GetNamedString("reports");
                    var reports = JsonArray.Parse(json);
                    foreach (var report in reports)
                    {
                        // create...
                        ReportItem item = ReportItem.FromJson(report.GetObject());
                        result.Reports.Add(item);
                    }
                }

                // pass it on...
                success(result);

            });
        }
    }
}

