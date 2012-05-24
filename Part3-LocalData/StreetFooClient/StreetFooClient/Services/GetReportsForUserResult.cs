using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StreetFooClient
{
    public class GetReportsForUserResult : ServiceResult
    {
        public List<ReportItem> Reports { get; private set; }

        public GetReportsForUserResult()
        {
            this.Reports = new List<ReportItem>();
        }
    }
}
