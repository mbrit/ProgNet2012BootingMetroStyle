using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StreetFooClient
{
    public class RefreshCacheResult : ServiceResult
    {
        public List<ReportItem> Reports { get; private set; }

        public RefreshCacheResult()
        {
            this.Reports = new List<ReportItem>();
        }
    }
}
