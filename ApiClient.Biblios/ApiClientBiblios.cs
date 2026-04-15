
using AllcandoJM.KohaFramework.ApiCore;
using System.Text;
using System.Xml.Schema;
using AllcandoJM.KohaFramework;
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

        public enum RecordSchema
        {
            DEFAULT=0,
            MARC21=1,
            UNIMARC=2
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

        private async Task<HttpResponseMessage> SendAuthRequest(string AuthID,BHeaders format)
        {
            string req = $"{this.BaseUrl}/api/v1/authorities/{AuthID}";
            var request = new HttpRequestMessage(HttpMethod.Get,req);
            request.Headers.Add("Accept", this.BibliosHeaders[(int)format]);

            var response = await client.SendAsync(request);
            return response;
        }


        /// <summary>
        /// Gets bibliographic record
        /// </summary>
        /// <param name="BiblioNumber">Biblionumber</param>
        /// <param name="format">format of biblio marcxml for example</param>
        /// <returns>ApiResponse object with related response info and deserialization methods</returns>
        public async Task<ApiResponse> GetBiblioResponseAsync(string BiblioNumber, BHeaders format)
        {
            HttpResponseMessage resp = await SendBiblioRequest(BiblioNumber, format);
            return await HandleResponse(resp);
        }

        /// <summary>
        /// Gets bibliographic record in marcxml format
        /// </summary>
        /// <param name="BiblioNumber">Biblio number</param>
        /// <returns>ApiResponse object with related response info and deserialization methods</returns>
        public async Task<ApiResponse> GetBiblioResponseAsync(string BiblioNumber)
        {
            return await GetBiblioResponseAsync(BiblioNumber, BHeaders.ApplicationMarcxmlpluxxml);
        }


        /// <summary>
        /// Gets bibliographic record as string
        /// </summary>
        /// <param name="BiblioNumber">Biblio number</param>
        /// <param name="format">format of biblio marcxml for example</param>
        /// <returns>String of bibliographic record in specified format</returns>
        public async Task<string> GetBiblioStringAsync(string BiblioNumber, BHeaders format )
        {        
            return await HandleString(await SendBiblioRequest(BiblioNumber,format)); ;
        }

        /// <summary>
        /// Gets bibliographic record as marcxml string
        /// </summary>
        /// <param name="BiblioNumber">Biblio number</param>
        /// <returns>bibliographic record as marcxml string</returns>
        public async Task <string> GetBiblioStringAsync(string BiblioNumber)
        {
            return await GetBiblioStringAsync(BiblioNumber, BHeaders.ApplicationMarcxmlpluxxml);
        }


        /// <summary>
        /// Adds biblio
        /// </summary>
        /// <param name="json">Biblio as Json record</param>
        /// <returns></returns>
        public async Task<ApiResponse> AddBiblioAsync(string json)
        {
            HttpRequestMessage msg = new HttpRequestMessage(HttpMethod.Post, $"{this.BaseUrl}/api/v1/biblios");
            msg.Content = new StringContent(json);

            HttpResponseMessage resp  = await client.SendAsync(msg);
            return await HandleResponse(resp);
        }

        public async Task<ApiResponse> AddBiblioAsync(byte[] data, string x_framework_id="",RecordSchema schema=RecordSchema.DEFAULT,bool? x_confirm_not_duplicate=null,string x_record_source_id="")
        {
            HttpRequestMessage msg = new HttpRequestMessage(HttpMethod.Post, $"{this.BaseUrl}/api/v1/biblios");
            if (x_framework_id != "") { msg.Headers.Add("x-framework-id", x_framework_id); }
            switch (schema)
            {
                case RecordSchema.MARC21:
                    msg.Headers.Add("x-record-schema", "MARC21");
                    break;
                case RecordSchema.UNIMARC:
                    msg.Headers.Add("x-record-schema", "UNIMARC");
                    break;
            }

            if(x_confirm_not_duplicate != null) { msg.Headers.Add("x-confirm-not-duplicate", Convert.ToInt32(x_confirm_not_duplicate).ToString()); }
            msg.Content = new ByteArrayContent(data);
            HttpResponseMessage resp = await base.client.SendAsync(msg);
            return await HandleResponse(resp);
        }

        public async Task<ApiResponse> DeleteBiblioAsync(string biblioID)
        {
            HttpRequestMessage msg = new HttpRequestMessage(HttpMethod.Delete, $"{this.BaseUrl}/api/v1/biblios/{biblioID}");
            HttpResponseMessage resp = await base.client.SendAsync(msg);
            return await HandleResponse(resp);
        }




        
        public async Task<string> GetAuthStringAsync(string AuthID)
        {
            return await HandleString(await SendAuthRequest(AuthID, BHeaders.ApplicationMarcxmlpluxxml));
        }

        public async Task<string> GetAuthStringAsync(string AuthID,BHeaders format)
        {
            return await HandleString(await SendAuthRequest(AuthID, format));

        }

        public async Task<ApiResponse> GetAuthResponseAsync(string AuthID)
        {
            return await HandleResponse(await SendAuthRequest(AuthID, BHeaders.ApplicationMarcxmlpluxxml));
        }

        public async Task<ApiResponse> GetAuthResponseAsync(string AuthID,BHeaders format)
        {
            return await HandleResponse(await SendAuthRequest(AuthID,format));
        }



        public async Task<ApiResponse> UpdateAuthAsync(string AuthID,string xml)
        {
            HttpRequestMessage msg =new HttpRequestMessage(HttpMethod.Put, $"{this.BaseUrl}/api/v1/authorities/{AuthID}");
            msg.Content = new StringContent(xml, Encoding.UTF8, this.BibliosHeaders[(int)BHeaders.ApplicationMarcxmlpluxxml]);
            return await HandleResponse(await client.SendAsync(msg));
        }





    }
}