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

        
        public async Task<string> GetCustomStringAsync(CustomRequest customRequest)
        {
            return await HandleString(await client.SendAsync(customRequest.request));
        }
        
        public async Task<ApiResponse> GetCustomResponseAsync(CustomRequest customRequest)
        {
            return await HandleResponse(await client.SendAsync(customRequest.request));
        }

        








    }
}