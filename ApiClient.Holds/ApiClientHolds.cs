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
            return await StreamTostring(response);

        }

        public async Task<string> GetWaitingList()
        {
            //var response = await client.GetAsync($"{this.BaseUrl}/api/v1/acquisitions/orders");       
            var response = await client.GetAsync($"{this.BaseUrl}/api/v1/holds");
            return await StreamTostring(response);
        }
        #endregion
    }
}