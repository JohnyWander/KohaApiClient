
using AllcandoJM.KohaFramework.ApiCore;

namespace AllcandoJM.KohaFramework.ApiClientBiblios
{
    public class ApiClientBiblios : ApiCore.ApiClient
    {
        public ApiClientBiblios() { }

        public ApiClientBiblios(AuthMethod authMethod, string url, string username, string password) : base(authMethod, url, username, password) { }

        public ApiClientBiblios(ApiClient client) : base(client) { }


        public enum BHeaders
        {
            ApplictionJson = 0,
            ApplicationMarcxmlpluxxml = 1,
            ApplicationMarcInJson = 2,
            ApplicationMarc = 3,
            TextPlain = 4
        }

        IDictionary<int, string> BibliosHeaders = new Dictionary<int, string>
        {
            {(int)BHeaders.ApplictionJson,"application/json" },
            {(int)BHeaders.ApplicationMarcxmlpluxxml,"application/marcxml+xml"},
            {(int)BHeaders.ApplicationMarcInJson,"application/marc-in-json"},
            {(int)BHeaders.ApplicationMarc,"application/marc" },
            {(int)BHeaders.TextPlain,"text/plain" }
        };

        private async Task<HttpResponseMessage> SendBiblioRequest(string BiblioNumber,BHeaders format)
        {
            string BiblioRequest = $"{this.BaseUrl}/api/v1/biblios/{BiblioNumber}";

            var request = new HttpRequestMessage(HttpMethod.Get, BiblioRequest);
            request.Headers.Add("Accept", this.BibliosHeaders[(int)format]);

            var response = await client.SendAsync(request);
            return response;
        }


        public async Task<ApiResponse> GetBiblioAsync(string BiblioNumber, BHeaders format)
        {
            HttpResponseMessage resp = await SendBiblioRequest(BiblioNumber, format);
            return await HandleResponse(resp);
        }

        public async Task<ApiResponse> GetBiblioAsync(string BiblioNumber)
        {
            return await GetBiblioAsync(BiblioNumber, BHeaders.ApplicationMarcxmlpluxxml);
        }


        public async Task<string> GetBiblioStringAsync(string BiblioNumber, BHeaders format )
        {        
            return await HandleString(await SendBiblioRequest(BiblioNumber,format)); ;
        }

        public async Task <string> GetBiblioStringAsync(string BiblioNumber)
        {
            return await GetBiblioStringAsync(BiblioNumber, BHeaders.ApplicationMarcxmlpluxxml);
        }

        



    }
}