using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StreetFooClient
{
    public interface ILogonServiceProxy
    {
        void Logon(string username, string password, Action<LogonResult> success);
    }
}
