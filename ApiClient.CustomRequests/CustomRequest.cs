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

        IDictionary<string,string> headers = new Dictionary<string,string>();

        public CustomRequest(HttpMethod method,string endpoint)
        {
            
            this.method = method;
            this.endpoint = endpoint;
        }

        /// <summary>
        /// Adds header to request, may be called many times for many headers
        /// </summary>
        /// <param name="name">name of the header - for example x-koha-embed, accept etc</param>
        /// <param name="value">value of the header</param>
        /// <returns>this instance of request</returns>
        public CustomRequest AddHeader(string name,string value)
        {
            this.headers.Add(name, value);
            return this;
        }


        internal CustomRequest Build(string baseurl)
        {
            this.request = new HttpRequestMessage(method, $"{baseurl}{endpoint}");
            if (this.headers.Count != 0)
            {
                foreach(var header in this.headers)
                {
                    this.request.Headers.Add(header.Key, header.Value);
                }
            }
            return this;
        }

        /// <summary>
        /// Adds json content to request - for example - arguments for updates, post requests etc
        /// </summary>
        /// <param name="json">argument list in json format</param>
        /// <returns>this instance of request</returns>
        public CustomRequest AddRequestContent(string json)
        {           
            this.request.Content = new StringContent(json,Encoding.UTF8, "application/json");

            return this;
        }

        /// <summary>
        /// Adds json content to request - for example - arguments for updates, post requests etc
        /// </summary>
        /// <param name="args">list of json arg objects - key/value</param>
        /// <returns>this instance of request</returns>
        public CustomRequest AddRequestContent(JsonArg[] args)
        {
            string json = argsCreator.CreateArgs(args);
            return AddRequestContent(json);
        }


    }
}
