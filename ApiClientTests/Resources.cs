using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace AllcandoJM.KohaFramework.ApiTests
{
    internal class Resources
    {
        internal string errorJson = @"{
  ""error"": ""Current settings prevent the passed due date to be applied"",
  ""error_code"": ""invalid_due_date""
}
";

        internal HttpResponseMessage errorResponse()
        {
            var err = new HttpResponseMessage();
            err.StatusCode = (HttpStatusCode)500;
            HttpContent content = new StringContent(errorJson);
            err.Content = content ;
            return err;
        }
    }
}
