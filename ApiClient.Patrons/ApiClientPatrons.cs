using AllcandoJM.KohaFramework.ApiCore.Interfaces;
using AllcandoJM.KohaFramework.ApiCore;
using System.Text;
using System.Security.Cryptography.X509Certificates;

namespace AllcandoJM.KohaFramework.ApiClientPatrons
{
    public class ApiClientPatrons : ApiCore.ApiClient
    {
     

        public ApiClientPatrons() { }


        public ApiClientPatrons(AuthMethod authMethod, string url, string username, string password) : base(authMethod, url, username, password)
        {

        }




        #region patrons

        private async Task<HttpResponseMessage> ValidatePatronPasswordAsync(string identifier,string password)
        {
            var request = new HttpRequestMessage(HttpMethod.Post, $"{base.BaseUrl}/api/v1/auth/password/validation");
            string jsonData = "{ \"identifier\": \""+identifier+"\", \"password\": \""+password+"\" }";
            request.Content = new StringContent(jsonData, Encoding.UTF8);

            var response = await client.SendAsync(request);
            return response;
        }

        /// <summary>
        /// Calls /auth/password/validation to verify user login, usefull for login to custom services using koha account
        /// </summary>
        /// <param name="identifier">cardnumber, username</param>
        /// <param name="password">password to koha account</param>
        /// <see cref="https://api.koha-community.org/#tag/patrons/operation/validateUserAndPassword"/>
        /// <returns>response as string</returns>
        public async Task<string> PatronPasswordValidationStringAsync(string identifier,string password)
        {
            return await HandleString(await ValidatePatronPasswordAsync(identifier,password));
        }

        /// <summary>
        /// Calls /auth/password/validation to verify user login, usefull for login to custom services using koha account
        /// </summary>
        /// <param name="identifier">cardnumber, username</param>
        /// <param name="password">password to koha account</param>
        /// <returns>ApiResponse object with related response info and deserialization methods</returns>
        public async Task<ApiResponse> PatronPasswordValidationResponseAsync(string identifier,string password)
        {
            return await HandleResponse(await ValidatePatronPasswordAsync(identifier,password));
        }

        /// <summary>
        /// calls /patrons/
        /// </summary>
        /// <param name="patron_id">id of patron</param>
        /// <returns>Patron data as json string</returns>
        public async Task<string> GetPatronStringByIdAsync(string patron_id)
        {
            var response = await client.GetAsync($"{this.BaseUrl}/api/v1/patrons?patron_id={patron_id}");
            return await HandleString(response);
        }

        /// <summary>
        /// calls /patrons/
        /// </summary>
        /// <param name="patron_id">id of patron</param>
        /// <returns>ApiResponse object with related response info and deserialization methods</returns>
        public async Task<ApiResponse> GetPatronResponseByIdAsync(string patron_id)
        {
            var response = await client.GetAsync($"{this.BaseUrl}/api/v1/patrons?patron_id={patron_id}");
            return await HandleResponse(response);
        }

        
        /// <summary>
        /// calls /patrons/
        /// </summary>
        /// <param name="cardnumber">patron cardnumber</param>
        /// <returns>Patron record as json string</returns>
        public async Task<string> GetPatronStringByCardnumberAsync(string cardnumber)
        {
            var response = await client.GetAsync($"{this.BaseUrl}/api/v1/patrons?cardnumber={cardnumber}");
            return await HandleString(response);
        }


        /// <summary>
        /// calls /patrons/
        /// </summary>
        /// <param name="cardnumber">patron cardnumber</param>
        /// <returns>ApiResponse object with related response info and deserialization methods</returns>
        public async Task<ApiResponse> GetPatronResponseByCardnumberAsync(string cardnumber)
        {
            var response = await client.GetAsync($"{this.BaseUrl}/api/v1/patrons?cardnumber={cardnumber}");
            return await HandleResponse(response);
        }




       
        private async Task<HttpResponseMessage> GetManyByCardNumbersAsync(string[] CardNumbers)
        {
            string PatronsRequest = "/api/v1/patrons?q={\"cardnumber\":{\"-in\":[@arg]}}";
            string arg = "";
            CardNumbers.ToList().ForEach(x =>
            {
                arg += arg + $"\"{x}\",";
            });

            PatronsRequest = PatronsRequest.Replace("@arg", arg.TrimEnd(',')); ;

            var response = await client.GetAsync($"{this.BaseUrl}{PatronsRequest}");
            return response;
        }

        /// <summary>
        /// calls /patrons/ to get many patron records
        /// </summary>
        /// <param name="CardNumbers">array of patron cardnumbers</param>
        /// <returns>Patron list as json string</returns>
        public async Task<string> GetManyPatronsStringByCardnumbersAsync(string[] CardNumbers)
        {         
            return await HandleString(await GetManyByCardNumbersAsync(CardNumbers));
        }


        /// <summary>
        /// calls /patrons/ to get many patron records
        /// </summary>
        /// <param name="CardNumbers">array of patron cardnumbers</param>
        /// <returns>ApiResponse object with related response info and deserialization methods</returns>
        public async Task<ApiResponse> GetManyPatronsResponseByCardnumbersAsync(string[] CardNumbers)
        {
            return await HandleResponse(await GetManyByCardNumbersAsync(CardNumbers));
        }



        /// <summary>
        /// calls /patrons/ with x-koha-embed overdues+count header to get patron data + overdue data
        /// </summary>
        /// <param name="patron_ID">ID of a patron</param>
        /// <returns>patron record with overdues as json string</returns>
        public async Task<string> GetPatronStringWithOverduesByPatronIDAsync(string patron_ID)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, $"{this.BaseUrl}/api/v1/patrons?patron_id={patron_ID}");
            request.Headers.Add("x-koha-embed", "overdues+count");
            var response = await client.SendAsync(request);
            return await HandleString(response);
        }


        /// <summary>
        /// calls /patrons/ with x-koha-embed overdues+count header to get patron data + overdue data
        /// </summary>
        /// <param name="patron_ID">ID of a patron</param>
        /// <returns>ApiResponse object with related response info and deserialization methods</returns>
        public async Task<ApiResponse> GetPatronResponseWithOverduesByPatronIDAsync(string patron_ID)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, $"{this.BaseUrl}/api/v1/patrons?patron_id={patron_ID}");
            request.Headers.Add("x-koha-embed", "overdues+count");
            var response = await client.SendAsync(request);
            return await HandleResponse(response);
        }



        /// <summary>
        /// calls /patrons/ 
        /// </summary>
        /// <param name="cardnumber">patron cardnumber</param>
        /// <returns>patron record with overdues as json string</returns>
        public async Task<string> GetPatronStringWithOverduesByCardnumberAsync(string cardnumber)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, $"{this.BaseUrl}/api/v1/patrons?cardnumber={cardnumber}");
            request.Headers.Add("x-koha-embed", "overdues+count");
            var response = await client.SendAsync(request);
            return await HandleString(response);
        }




        /// <summary>
        /// calls /patrons/ 
        /// </summary>
        /// <param name="cardnumber">patron cardnumber</param>
        /// <returns>ApiResponse object with related response info and deserialization methods</returns>
        public async Task<ApiResponse> GetPatronResponseWithOverduesByCardnumberAsync(string cardnumber)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, $"{this.BaseUrl}/api/v1/patrons?cardnumber={cardnumber}");
            request.Headers.Add("x-koha-embed", "overdues+count");
            var response = await client.SendAsync(request);
            return await HandleResponse(response);
        }

        /// <summary>
        /// calls /patrons/patron_id/account/debits to list patron debits
        /// </summary>
        /// <param name="patron_id">ID of patron</param>
        /// <returns>patron debits as json string</returns>
        public async Task<string> GetOverduesStringForPatronAsync(string patron_id)
        {
            var response = await client.GetAsync($"{this.BaseUrl}/api/v1/patrons/{patron_id}/account/debits");
            return await HandleString(response);
        }


        /// <summary>
        /// calls /patrons/patron_id/account/debits
        /// </summary>
        /// <param name="patron_id">ID of patron</param>
        /// <returns>ApiResponse object with related response info and deserialization methods</returns>
        public async Task<ApiResponse> GetOverduesResponseForPatronAsync(string patron_id)
        {
            var response = await client.GetAsync($"{this.BaseUrl}/api/v1/patrons/{patron_id}/account/debits");
            return await HandleResponse(response);
        }
        #endregion


    }
}