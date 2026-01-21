using AllcandoJM.KohaFramework.ApiCore;
using AllcandoJM.KohaFramework.JsonDeserialization;
using AllcandoJM.KohaFramework.ApiCore;
using System.Text;

namespace AllcandoJM.KohaFramework.ApiClientItems
{
    public interface IItemQuery
    {
        public Task<string> GetItemById(string itemID);
        public Task<string> GetItemByBiblio(string biblio);

        public Task<string> GetItemByCallNumber(string callNumber);

        public Task<string> GetItemByBarcode(string barcode);
    }

    public interface IItemUpdate
    {
        public Task<string> UpdateItem(string ItemId, string biblioId, JsonArg[] args);
    }



    public class ApiClientItems : ApiCore.ApiClient,IItemQuery,IItemUpdate
    {
        public ApiClientItems()
        {

        }

        public ApiClientItems(AuthMethod authMethod,string url,string username,string password) : base(authMethod,url,username,password)
        {

        }


        IItemQuery Queries()
        {
            return this;
        }

        IItemUpdate Updates()
        {
            return this;
        }

        public IItemDeserializer GetDeserializer()
        {
            return new KohaJsonDeserializer();
        }


        public async Task<string> UpdateItem(string ItemId, string biblioId, JsonArg[] args)
        {
            //https://api.koha-community.org/api/v1/biblios/{biblio_id}/items/{item_id}
            var request = new HttpRequestMessage(HttpMethod.Put, $"{this.BaseUrl}/api/v1/biblios/{biblioId}/items/{ItemId}");
            string payload = this.argCreator.CreateArgs(args);
            request.Content = new StringContent(payload, Encoding.UTF8, "application/json");

            var response = await client.SendAsync(request);
            return await StreamTostring(response);

        }


        public async Task<string> GetItemById(string itemID)
        {
            var response = await client.GetAsync($"{this.BaseUrl}/api/v1/items/{itemID}");
            return await StreamTostring(response);
        }

        public async Task<string> GetItemByBiblio(string bibnum)
        {
            var response = await client.GetAsync($"{this.BaseUrl}" + "/api/v1/items?q={\"biblio_id\":" + $"{bibnum}" + "}");
            return await StreamTostring(response);

        }


        public async Task<string> GetItemByCallNumber(string callNumber)
        {
            var response = await client.GetAsync($"{this.BaseUrl}" + "/api/v1/items?q={\"callnumber\":" + $"\"{callNumber}\"" + "}");
            return await StreamTostring(response);
        }


        public async Task<string> GetItemByBarcode(string barcode)
        {
            var response = await client.GetAsync($"{this.BaseUrl}" + "/api/v1/items?q={\"external_id\":" + $"\"{barcode}\"" + "}");
            return await StreamTostring(response);  
        }

    }
}