using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Windows.Data.Json;

namespace StreetFooClient
{
    public class ServiceProxy
    {
        private string Url { get; set; }

        // get an API key from https://streetfoo.apphb.com/. Don't use this one!  :-)
        private const string ApiKey = "4f41463a-dfc7-45dd-8d95-bf339f040933";

        protected ServiceProxy(string handlerName)
        {
            // in the CP version, HTTPS doesn't resolve proper.y..
            this.Url = string.Format("http://streetfoo.apphb.com/handlers/Handle{0}.ashx", handlerName);
        }

        protected void Execute<T>(JsonObject input, Action<JsonObject, T> success)
            where T : ServiceResult
        {
            // put in the API key...
            input.Add("apiKey", ApiKey);

            // do we have a logon token?
            if (!(string.IsNullOrEmpty(AppRuntime.LogonToken)))
                input.Add("logonToken", AppRuntime.LogonToken);

            // make the call...
            var request = HttpWebRequest.CreateHttp(this.Url);
            request.Method = "POST";

            // get a request stream up to the server...
            var grsTask = request.GetRequestStreamAsync().ContinueWith(async (grsResult) =>
            {
                // send up the JSON to the server...
                string json = input.Stringify();
                byte[] bs = Encoding.UTF8.GetBytes(json);
                using (var stream = grsResult.Result)
                    stream.Write(bs, 0, bs.Length);

                // get the response...
                var response = await request.GetResponseAsync();
                using (var reader = new StreamReader(response.GetResponseStream()))
                    json = reader.ReadToEnd();

                // load it...
                var output = JsonObject.Parse(json);

                // create a result object...
                T result = (T)Activator.CreateInstance(typeof(T));
                if (!(output.GetNamedBoolean("isOk")))
                {
                    string error = output.GetNamedString("error");
                    result.SetError(error);
                }

                // call the success handler...
                success(output, result);

            });
        }
    }
}

