using AllcandoJM.KohaFramework.ApiCore;
using AllcandoJM.KohaFramework.JsonDeserialization;
using AllcandoJM.KohaFramework.ApiCore;
using System.Text;
using AllcandoJM.KohaFramework.ApiCore.Helpers.Exceptions;
using System.ComponentModel.DataAnnotations;
using System.Reflection.Metadata;
using static AllcandoJM.KohaFramework.ApiCore.KohaHeaders;
using System.Text.RegularExpressions;
using static System.Net.Mime.MediaTypeNames;
using System;
using System.Reflection.Emit;

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

        public async Task<ApiResponse> GetHoldsListResponse(string q="")
        {
            return await HandleResponse(await client.GetAsync($"{this.BaseUrl}/api/v1/holds?{q}"));
        }

        public async Task<string> GetHoldsListStringAsync(string q="")
        {
            //var response = await client.GetAsync($"{this.BaseUrl}/api/v1/acquisitions/orders");       
            var response = await client.GetAsync($"{this.BaseUrl}/api/v1/holds?{q}");
            return await HandleString(response);
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

        /////////////////////////////////////////////////////////////////////////////////

        private async Task<HttpResponseMessage> GetItemById(string itemID, KohaHeaders.ItemXkohaEmbedHeaders headers)
        {
            HttpRequestMessage msg = new HttpRequestMessage(HttpMethod.Get, $"{this.BaseUrl}/api/v1/items/{itemID}");
            if(!headers.HasFlag(KohaHeaders.ItemXkohaEmbedHeaders.None))
            {
                List<string> headerValues = new List<string>();
                if (headers.HasFlag(KohaHeaders.ItemXkohaEmbedHeaders.strings))
                {
                    headerValues.Add(KohaHeaders.ItemHeaders[(int)KohaHeaders.ItemXkohaEmbedHeaders.strings]);
                }

                if (headers.HasFlag(KohaHeaders.ItemXkohaEmbedHeaders.effective_bookable))
                {
                    headerValues.Add(KohaHeaders.ItemHeaders[(int)KohaHeaders.ItemXkohaEmbedHeaders.effective_bookable]);
                }

                string header = "";
                headerValues.ForEach(x =>
                {
                    header+= x+",";
                });
                msg.Headers.Add("x-koha-embed",header.TrimEnd(','));

            }

            return await client.SendAsync(msg);

        }


        /// <summary>
        /// calls /items/<itemID>
        /// </summary>
        /// <param name="itemID">id of item</param>
        /// <returns>item data as json string</returns>
        public async Task<string> GetItemStringByIdAsync(string itemID)
        {
            var response = await GetItemById(itemID,KohaHeaders.ItemXkohaEmbedHeaders.None);
            return await HandleString(response);
        }

        /// <summary>
        /// calls /items/<itemID>
        /// </summary>
        /// <param name="itemID">id of item</param>
        /// <param name="headers">Flags of headers to use, you can use single or more like this - 
        /// KohaHeaders.ItemXkohaEmbedHeaders.strings | KohaHeaders</param>
        /// <returns>item data as json string with data added with x-koha-Embed header(s)</returns>
        public async Task<string> GetItemStringByIdAsync(string itemID,KohaHeaders.ItemXkohaEmbedHeaders headers)
        {
            var response = await GetItemById(itemID, headers);
            return await HandleString(response);
        }



        /// <summary>
        /// calls /items/<itemID>
        /// </summary>
        /// <param name="itemID">id of item</param>
        /// <returns>ApiResponse object with related response info and deserialization methods</returns>
        public async Task<ApiResponse> GetItemResponseByIdAsync(string itemID)
        {
            var response = await GetItemById(itemID, KohaHeaders.ItemXkohaEmbedHeaders.None);
            return await HandleResponse(response);
        }

        public async Task<ApiResponse> GetItemResponseByIdAsync(string itemID,KohaHeaders.ItemXkohaEmbedHeaders headers)
        {
            var response = await GetItemById(itemID,headers);
            return await HandleResponse(response);
        }
         
        ///////////////////////////////////////////////
        

        private async Task<HttpResponseMessage> GetItemBy(string query,KohaHeaders.ItemsXkohaEmbedHeaders headers, string _match = null, string _order_by = null, string _page = null, string _per_page = null)
        {
            StringBuilder reqString = new StringBuilder(query);
            if(_match != null) { reqString.Append($"&_match={_match}"); }
            if(_order_by != null) { reqString.Append($"&_order_by={_order_by}"); }
            if(_page != null) { reqString.Append($"&_page={_page}"); }
            if (_per_page != null) { reqString.Append($"&_per_page={_per_page}"); }

            HttpRequestMessage msg = new HttpRequestMessage(HttpMethod.Get,reqString.ToString() );

            if (!headers.HasFlag(KohaHeaders.ItemsXkohaEmbedHeaders.None))
            {
                string headerValue = "";
                List<string> headerValues = new List<string>();
                if (headers.HasFlag(KohaHeaders.ItemsXkohaEmbedHeaders.strings))
                {
                    headerValues.Add(KohaHeaders.ItemsHeaders[(int)KohaHeaders.ItemsXkohaEmbedHeaders.strings]); 
                }

                if (headers.HasFlag(KohaHeaders.ItemsXkohaEmbedHeaders.biblio))
                {
                    headerValues.Add(KohaHeaders.ItemsHeaders[(int)KohaHeaders.ItemsXkohaEmbedHeaders.biblio]);
                }

                if (headers.HasFlag(KohaHeaders.ItemsXkohaEmbedHeaders.effective_bookable))
                {
                    headerValues.Add(KohaHeaders.ItemsHeaders[(int)KohaHeaders.ItemsXkohaEmbedHeaders.effective_bookable]);
                }

                headerValues.ForEach(x =>
                {
                    headerValue += x + ",";
                });
                msg.Headers.Add("x-koha-embed", headerValue.TrimEnd(','));

            }

           return await client.SendAsync(msg);
        }


        public async Task<string> GetItemsStringByIDAsync(string id)
        {
            var response = await GetItemBy($"{this.BaseUrl}" + "/api/v1/items?q={\"item_id\":" + $"{id}" + "}",
            KohaHeaders.ItemsXkohaEmbedHeaders.None, null, null, null, null);
            return await HandleString(response);
        }

        public async Task<string> GetItemsStringByIDAsync(string id,ItemsXkohaEmbedHeaders headers)
        {
            var response = await GetItemBy($"{this.BaseUrl}" + "/api/v1/items?q={\"item_id\":" + $"{id}" + "}",
            headers, null, null, null, null);
            return await HandleString(response);
        }

        public async Task<ApiResponse> GetItemsResponseByIdAsync(string id)
        {
            var response = await GetItemBy($"{this.BaseUrl}" + "/api/v1/items?q={\"item_id\":" + $"{id}" + "}",
            KohaHeaders.ItemsXkohaEmbedHeaders.None, null, null, null, null);
            return await HandleResponse(response);
        }

        public async Task<ApiResponse> GetItemsResponseByIdAsync(string id,ItemsXkohaEmbedHeaders headers)
        {
            var response = await GetItemBy($"{this.BaseUrl}" + "/api/v1/items?q={\"item_id\":" + $"{id}" + "}",
            headers, null, null, null, null);
            return await HandleResponse(response);
        }
        

        public async Task<string> GetItemsStringByBiblioAsync(string bibnum,string _match=null,string _order_by=null,string _page=null,string _per_page =null)
        {
            var response = await GetItemBy($"{this.BaseUrl}" + "/api/v1/items?q={\"biblio_id\":" + $"{bibnum}" + "}",
                KohaHeaders.ItemsXkohaEmbedHeaders.None,_match,_order_by,_page,_per_page);
            return await HandleString(response);
        }


        public async Task<string> GetItemsStringByBiblioAsync(string bibnum, KohaHeaders.ItemsXkohaEmbedHeaders headers, string _match = null, string _order_by = null, string _page = null, string _per_page = null)
        {
            var response = await GetItemBy($"{this.BaseUrl}" + "/api/v1/items?q={\"biblio_id\":" + $"{bibnum}" + "}",
                headers, _match, _order_by, _page, _per_page);
            return await HandleString(response);
        }

        public async Task<ApiResponse> GetItemsResponseByBiblioAsync(string bibnum, string _match = null, string _order_by = null, string _page = null, string _per_page = null)
        {
            var response = await GetItemBy($"{this.BaseUrl}" + "/api/v1/items?q={\"biblio_id\":" + $"{bibnum}" + "}",
               KohaHeaders.ItemsXkohaEmbedHeaders.None,_match, _order_by, _page, _per_page);
            return await HandleResponse(response);
        }

        public async Task<ApiResponse> GetItemsResponseByBiblioAsync(string bibnum, KohaHeaders.ItemsXkohaEmbedHeaders headers, string _match = null, string _order_by = null, string _page = null, string _per_page = null)
        {
            var response = await GetItemBy($"{this.BaseUrl}" + "/api/v1/items?q={\"biblio_id\":" + $"{bibnum}" + "}",
               headers, _match, _order_by, _page, _per_page);
            return await HandleResponse(response);
        }



        public async Task<string> GetItemsStringByCallNumberAsync(string callNumber, string _match = null, string _order_by = null, string _page = null, string _per_page = null)
        {
            var response = await GetItemBy($"{this.BaseUrl}" + "/api/v1/items?q={\"callnumber\":" + $"\"{callNumber}\"" + "}",
                KohaHeaders.ItemsXkohaEmbedHeaders.None,_match, _order_by, _page, _per_page);
            return await HandleString(response);
        }

        public async Task<ApiResponse> GetItemsResponseByCallNumberAsync(string callNumber,KohaHeaders.ItemsXkohaEmbedHeaders headers, string _match = null, string _order_by = null, string _page = null, string _per_page = null)
        {
            var response = await GetItemBy($"{this.BaseUrl}" + "/api/v1/items?q={\"callnumber\":" + $"\"{callNumber}\"" + "}",
                headers, _match, _order_by, _page, _per_page);
            return await HandleResponse(response);
        }


        public async Task<string> GetItemsStringByBarcodeAsync(string barcode,string _match = null, string _order_by = null, string _page = null, string _per_page = null)
        {
            var response = await GetItemBy($"{this.BaseUrl}" + "/api/v1/items?q={\"external_id\":" + $"\"{barcode}\"" + "}"
                ,KohaHeaders.ItemsXkohaEmbedHeaders.None, _match, _order_by, _page, _per_page);
            return await HandleString(response);

        }

        public async Task<string> GetItemsStringByBarcodeAsync(string barcode, KohaHeaders.ItemsXkohaEmbedHeaders header, string _match = null, string _order_by = null, string _page = null, string _per_page = null)
        {
            var response = await GetItemBy($"{this.BaseUrl}" + "/api/v1/items?q={\"external_id\":" + $"\"{barcode}\"" + "}"
                , header, _match, _order_by, _page, _per_page);
            return await HandleString(response);

        }


        public async Task<ApiResponse> GetItemsResponseByBarcodeAsync(string barcode, string _match = null, string _order_by = null, string _page = null, string _per_page = null)
        {
            var response = await GetItemBy($"{this.BaseUrl}" + "/api/v1/items?q={\"external_id\":" + $"\"{barcode}\"" + "}",
                KohaHeaders.ItemsXkohaEmbedHeaders.None, _match, _order_by, _page, _per_page);
            return await HandleResponse(response);
        }


        public async Task<ApiResponse> GetCheckoutResponseAsync(string checkout_id)
        {
            var response = await client.GetAsync($"{this.BaseUrl}/api/v1/checkouts/{checkout_id}");
            return await HandleResponse(response);
        }

        public async Task<string> GetCheckoutsStringByQasync(string q)
        {
            var response = await GetCheckoutsby(q, CheckoutXkohaEmbedHeaders.none);
            return await HandleString(response);
        }
        
        public async Task<string> GetCheckoutsStringByQasync(string q,CheckoutXkohaEmbedHeaders headers)
        {
            var response = await GetCheckoutsby(q, headers);
            return await HandleString(response);
        }

        public async Task<ApiResponse> GetCheckoutsResponseByQasync(string q)
        {
            var response = await GetCheckoutsby(q, CheckoutXkohaEmbedHeaders.none);
            return await HandleResponse(response);
        }

        public async Task<ApiResponse> GetCheckoutsResponseByQasync(string q, CheckoutXkohaEmbedHeaders headers)
        {
            var response = await GetCheckoutsby(q, headers);
            return await HandleResponse(response);
        }



        private async Task<HttpResponseMessage> GetCheckoutsby(string q,CheckoutXkohaEmbedHeaders headers)
        {
            StringBuilder link = new StringBuilder($"{this.BaseUrl}/api/v1/checkouts?_per_page=-1&");
            link.Append(q);

            StringBuilder header = new StringBuilder();
            var req = new HttpRequestMessage(HttpMethod.Get, link.ToString());
            foreach (CheckoutXkohaEmbedHeaders flag in Enum.GetValues(typeof(CheckoutXkohaEmbedHeaders)))
            {
                if (flag == CheckoutXkohaEmbedHeaders.none)
                {
                    continue;
                }

                if (headers.HasFlag(flag))
                {
                    header.Append(CheckoutsHeaders[(int)flag]+",");
                }
            }
            Console.WriteLine(header.ToString());
            Console.WriteLine(link);


            req.Headers.Add("x-koha-embed", header.ToString().TrimEnd(','));
            return await client.SendAsync(req);
            
        }







       
    }
}