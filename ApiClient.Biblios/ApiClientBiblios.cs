
using AllcandoJM.KohaFramework.ApiCore;

namespace AllcandoJM.KohaFramework.ApiClientBiblios
{
    public class ApiClientBiblios : ApiCore.ApiClient
    {
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


        public async Task<string> GetBiblio(string BiblioNumber, BHeaders format)
        {
            string BiblioRequest = $"{this.BaseUrl}/api/v1/biblios/{BiblioNumber}";

            var request = new HttpRequestMessage(HttpMethod.Get, BiblioRequest);
            request.Headers.Add("Accept", this.BibliosHeaders[(int)format]);


            var response = await client.SendAsync(request);
            ApiResponse res = await base.ParseResponseAsync(response);

            return await Handle(response); ;
        }


    }
}