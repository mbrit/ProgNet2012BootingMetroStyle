using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Data.Json;

namespace StreetFooClient
{
    public class LogonServiceProxy : ServiceProxy, ILogonServiceProxy
    {
        public LogonServiceProxy()
            : base("Logon")
        {
        }

        public void Logon(string username, string password, Action<LogonResult> success)
        {
            JsonObject input = new JsonObject();
            input.Add("username", username);
            input.Add("password", password);

            // go...
            this.Execute<LogonResult>(input, (output, result) => {

                // were we ok?
                if (result.IsOk)
                {
                    // get the logon token...
                    result.LogonToken = output.GetNamedString("token");
                }

                // pass it on...
                success(result);

            });
        }
    }
}
