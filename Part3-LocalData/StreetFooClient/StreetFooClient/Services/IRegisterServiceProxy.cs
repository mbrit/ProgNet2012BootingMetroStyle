using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StreetFooClient
{
    public interface IRegisterServiceProxy
    {
        void Register(string username, string email, string password, string confirm, Action<RegisterResult> success);
    }
}
