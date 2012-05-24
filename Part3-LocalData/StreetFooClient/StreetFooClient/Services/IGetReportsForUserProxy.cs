using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StreetFooClient
{
    public interface IGetReportsByUserProxy
    {
        void GetReportsByUser(Action<GetReportsForUserResult> success);
    }
}
