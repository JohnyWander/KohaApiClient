using AllcandoJM.KohaFramework.ApiCore.Interfaces;
using AllcandoJM.KohaFramework.ApiCore;
using System.Text;

namespace AllcandoJM.KohaFramework.ApiClientPatrons
{
    public class ApiClientPatrons : ApiCore.ApiClient
    {
     

        public ApiClientPatrons() { }


        #region patrons
        public async Task<string> PostCheckLogin()
        {
            var request = new HttpRequestMessage(HttpMethod.Post, $"{base.BaseUrl}/api/v1/auth/password/validation");
            string jsonData = "{ \"identifier\": \"00727832\", \"password\": \"test\" }";
            request.Content = new StringContent(jsonData, Encoding.UTF8);

            var response = await client.SendAsync(request);
            Console.WriteLine(request.Headers.Authorization.ToString());
            return await Handle(response);
        }

        public async Task<string> GetPatronById(string patron_id)
        {
            var response = await client.GetAsync($"{this.BaseUrl}/api/v1/patrons?patron_id={patron_id}");
            //response.Content.J
            return await Handle(response);

        }

        public async Task<string> GetPatronByCardnumber(string cardnumber)
        {
            var response = await client.GetAsync($"{this.BaseUrl}/api/v1/patrons?cardnumber={cardnumber}");
            return await Handle(response);
        }


        public async Task<string> GetManyPatronsByCardnumbers(string[] CardNumbers)
        {
            //string PatronsRequest = "api/v1/patrons?q={\"cardnumber\":{\"-in\":[\"00386245\",\"00727832\"]}}";
            string PatronsRequest = "/api/v1/patrons?q={\"cardnumber\":{\"-in\":[@arg]}}";
            string arg = "";
            CardNumbers.ToList().ForEach(x =>
            {
                arg += arg + $"\"{x}\",";
            });

            PatronsRequest = PatronsRequest.Replace("@arg", arg.TrimEnd(',')); ;

            var response = await client.GetAsync($"{this.BaseUrl}{PatronsRequest}");
            return await Handle(response);


        }

        #endregion


    }
}