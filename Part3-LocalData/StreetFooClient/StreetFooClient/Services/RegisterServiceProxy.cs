using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Data.Json;

namespace StreetFooClient
{
    public class RegisterServiceProxy : ServiceProxy, IRegisterServiceProxy
    {
        public RegisterServiceProxy()
            : base("Register")
        {
        }

        public void Register(string username, string email, string password, string confirm, Action<RegisterResult> success)
        {
            // setup the arguments...
            JsonObject input = new JsonObject();
            input.Add("username", username);
            input.Add("email", email);
            input.Add("password", password);
            input.Add("confirm", confirm);

            // call...
            this.Execute<RegisterResult>(input, (output, result) =>
            {
                // ok?
                if (result.IsOk)
                    result.UserId = output.GetNamedString("userId");

                // pass it on...
                success(result);

            });
        }
    }
}
