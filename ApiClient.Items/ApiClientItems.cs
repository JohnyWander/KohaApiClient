using AllcandoJM.KohaFramework.ApiCore;
using AllcandoJM.KohaFramework.JsonDeserialization;
using AllcandoJM.KohaFramework.ApiCore;
using System.Text;
using AllcandoJM.KohaFramework.ApiCore.Helpers.Exceptions;
using System.ComponentModel.DataAnnotations;

namespace AllcandoJM.KohaFramework.ApiClientItems
{
 
    public class ApiClientItems : ApiCore.ApiClient
    {
        public ApiClientItems() :base()
        {

        }

        public ApiClientItems(AuthMethod authMethod,string url,string username,string password) : base(authMethod,url,username,password)
        {

        }


        public ApiClientItems(ApiClient client) : base(client)
        {

        }


        public IItemDeserializer GetDeserializer()
        {
            return new KohaJsonDeserializer();
        }

        private async Task<HttpResponseMessage> SendUpdateRequest(string ItemId, string biblioId, JsonArg[] args)
        {
            var request = new HttpRequestMessage(HttpMethod.Put, $"{this.BaseUrl}/api/v1/biblios/{biblioId}/items/{ItemId}");
            string payload = this.argCreator.CreateArgs(args);
            request.Content = new StringContent(payload, Encoding.UTF8, "application/json");
            var response = await client.SendAsync(request);
            return response;
        }


        /// <summary>
        /// Updates item with provided json args
        /// </summary>
        /// <param name="ItemId">Item ID</param>
        /// <param name="biblioId">Biblio ID</param>
        /// <param name="args">array of JsonArg objects (name,value,valueType) to change item data</param>
        /// <returns>response as json string</returns>
        public async Task<string> UpdateItemAsync(string ItemId, string biblioId, JsonArg[] args)
        {     
            return await HandleString(await SendUpdateRequest(ItemId,biblioId,args)); 
        }

        /// <summary>
        /// Updates item with provided json args
        /// </summary>
        /// <param name="ItemId">Item ID</param>
        /// <param name="biblioId">Biblio ID</param>
        /// <param name="args">array of JsonArg objects (name,value,valueType) to change item data</param>
        /// <returns>ApiResponse object with related response info and deserialization methods</returns>

        public async Task<ApiResponse> UpdateItemWResponseAsync(string ItemId,string biblioId, JsonArg[] args)
        {
            return await HandleResponse(await  SendUpdateRequest(ItemId, biblioId,args));
        }






        /// <summary>
        /// calls /items/
        /// </summary>
        /// <param name="itemID">id of item</param>
        /// <returns>item data as json string</returns>
        public async Task<string> GetItemStringByIdAsync(string itemID)
        {
            var response = await client.GetAsync($"{this.BaseUrl}/api/v1/items/{itemID}");
            return await HandleString(response);
        }

        /// <summary>
        /// calls /items/
        /// </summary>
        /// <param name="itemID">id of item</param>
        /// <returns>ApiResponse object with related response info and deserialization methods</returns>
        public async Task<ApiResponse> GetItemResponseByIdAsync(string itemID)
        {
            var response = await client.GetAsync($"{this.BaseUrl}/api/v1/items/{itemID}");
            return await HandleResponse(response);
        }

        public async Task<string> GetItemStringByBiblioAsync(string bibnum)
        {
            var response = await client.GetAsync($"{this.BaseUrl}" + "/api/v1/items?q={\"biblio_id\":" + $"{bibnum}" + "}");
            return await HandleString(response);
        }

        public async Task<ApiResponse> GetItemResponseByBiblioAsync(string bibnum)
        {
            var response = await client.GetAsync($"{this.BaseUrl}" + "/api/v1/items?q={\"biblio_id\":" + $"{bibnum}" + "}");
            return await HandleResponse(response);
        }


        public async Task<string> GetItemStringByCallNumberAsync(string callNumber)
        {
            var response = await client.GetAsync($"{this.BaseUrl}" + "/api/v1/items?q={\"callnumber\":" + $"\"{callNumber}\"" + "}");
            return await HandleString(response);
        }

        public async Task<ApiResponse> GetItemResponseByCallNumberAsync(string callNumber)
        {
            var response = await client.GetAsync($"{this.BaseUrl}" + "/api/v1/items?q={\"callnumber\":" + $"\"{callNumber}\"" + "}");
            return await HandleResponse(response);
        }


        public async Task<string> GetItemStringByBarcodeAsync(string barcode)
        {
            var response = await client.GetAsync($"{this.BaseUrl}" + "/api/v1/items?q={\"external_id\":" + $"\"{barcode}\"" + "}");
            return await HandleString(response);

        }

        public async Task<ApiResponse> GetItemResponseByBarcodeAsync(string barcode)
        {
            var response = await client.GetAsync($"{this.BaseUrl}" + "/api/v1/items?q={\"external_id\":" + $"\"{barcode}\"" + "}");
            return await HandleResponse(response);
        }


        public async Task<ApiResponse> GetCheckoutResponseAsync(string checkout_id)
        {
            var response = await client.GetAsync($"{this.BaseUrl}/api/v1/checkouts/{checkout_id}");
            return await HandleResponse(response);
        }


    }
}