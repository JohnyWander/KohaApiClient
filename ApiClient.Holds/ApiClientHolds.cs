using AllcandoJM.KohaFramework.ApiCore;

namespace AllcandoJM.KohaFramework.ApiClientHolds
{
    public class ApiClientHolds : ApiCore.ApiClient
    {

        public ApiClientHolds() { }
        #region Holds

        public async Task<string> CancelHold(string id)
        {
            string CancelString = $"{this.BaseUrl}/api/v1/holds/{id}";
            var request = new HttpRequestMessage(HttpMethod.Delete, CancelString);



            var response = await client.SendAsync(request);
            return await HandleString(response); 

        }

        public async Task<string> GetWaitingListStringAsync()
        {
            //var response = await client.GetAsync($"{this.BaseUrl}/api/v1/acquisitions/orders");       
            var response = await client.GetAsync($"{this.BaseUrl}/api/v1/holds");
            return await HandleString(response);
        }

        public async Task<ApiResponse> GetWaitingListResponse()
        {
            return await HandleResponse(await client.GetAsync($"{this.BaseUrl}/api/v1/holds"));
        }



        #endregion
    }
}