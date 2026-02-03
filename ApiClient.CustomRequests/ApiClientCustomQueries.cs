using AllcandoJM.KohaFramework.ApiCore;
using ApiClient.CustomRequests;

namespace AllcandoJM.KohaFramework.ApiClientCustomRequests
{
    public class ApiClientCustomQueries : ApiCore.ApiClient
    {
        public ApiClientCustomQueries()
        {

        }

        public ApiClientCustomQueries(AuthMethod authMethod, string url, string username, string password) : base(authMethod, url, username, password)
        {

        }


        public ApiClientCustomQueries(ApiCore.ApiClient client) : base(client)
        {

        }

        
        /// <summary>
        /// Sends your custom request to koha api
        /// </summary>
        /// <param name="customRequest">class with your custom request configuration</param>
        /// <returns>response string</returns>
        public async Task<string> GetCustomStringAsync(CustomRequest customRequest)
        {
            customRequest.Build(base.BaseUrl);
            return await HandleString(await client.SendAsync(customRequest.request));
        }

        /// <summary>
        /// Sends your custom request to koha api
        /// </summary>
        /// <param name="customRequest">class with your custom request configuration</param>
        /// <returns>ApiResponse object with related response info and deserialization methods</returns>
        public async Task<ApiResponse> GetCustomResponseAsync(CustomRequest customRequest)
        {
            customRequest.Build(base.BaseUrl);
            return await HandleResponse(await client.SendAsync(customRequest.request));
        }

        








    }
}