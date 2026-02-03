using AllcandoJM.KohaFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiClient.CustomRequests
{
    public class CustomRequest
    {
        public HttpRequestMessage request;
        JsonArgsCreator argsCreator = new JsonArgsCreator();

        HttpMethod method;
        string endpoint;

        public CustomRequest(HttpMethod method,string endpoint)
        {
            
            this.method = method;
            this.endpoint = endpoint;
        }

        private CustomRequest Build(string baseurl)
        {
            this.request = new HttpRequestMessage(method, $"{baseurl}{endpoint}");
            return this;
        }

        public CustomRequest AddRequestContent(string json)
        {           
            this.request.Content = new StringContent(json,Encoding.UTF8, "application/json");

            return this;
        }
        
        public CustomRequest AddRequestContent(JsonArg[] args)
        {
            string json = argsCreator.CreateArgs(args);
            return AddRequestContent(json);
        }


    }
}
