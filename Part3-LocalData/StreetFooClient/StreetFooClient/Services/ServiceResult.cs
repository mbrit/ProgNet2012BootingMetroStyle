using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StreetFooClient
{
    public abstract class ServiceResult
    {
        public bool IsOk { get; private set; }
        public string Error { get; private set; }

        protected ServiceResult()
        {
            this.IsOk = true;
        }

        internal void SetError(string error)
        {
            this.IsOk = false;
            this.Error = error;
        }
    }
}
