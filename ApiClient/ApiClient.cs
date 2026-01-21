using AllcandoJM.KohaFramework.ConfigurationManager;
using AllcandoJM.KohaFramework.ApiCore.Interfaces;
using System.Security;
using System.Text;
using AllcandoJM.KohaFramework.JsonDeserialization;


namespace AllcandoJM.KohaFramework.ApiCore
{
    public enum AuthMethod
    {
        Basic,
        ApiKey
    }

    public class ApiClient : ApiClientBase
    {

        AuthMethod authMethod;


        ApiConfig config;
        protected HttpClient client;

        protected JsonArgsCreator argCreator = new JsonArgsCreator();



        protected string BaseUrl;

     
        /// <summary>
        /// Creates api client and configures it with config file
        /// </summary>
        public ApiClient()
        {
            config = new ApiConfig();
            client = new HttpClient();
            //Patron = this;


            BaseUrl = config.GetConfigValue("KOHA URL");
            string method = config.GetConfigValue("API AUTH");

            if(method == "HTTP_BASIC")
            {
                authMethod = AuthMethod.Basic;
            }
            else if(method == "oauth2")
            {
                authMethod = AuthMethod.ApiKey;
            }

            switch (authMethod)
            {
                case AuthMethod.Basic:
                    string user = config.GetConfigValue(ApiConfig.ConfigNames.API_USERNAME);
                    string pass = config.GetConfigValue(ApiConfig.ConfigNames.API_PASSWORD);
                    SetHTTPBasicHeader(user, pass);
                break;

                case AuthMethod.ApiKey:
                    string clientID = config.GetConfigValue(ApiConfig.ConfigNames.API_CLIENT_ID);
                    string clientSecret = config.GetConfigValue(ApiConfig.ConfigNames.API_CLIENT_SECRET);
                    SetOauthHeader(clientID, clientSecret);
                    break;


            }

          
          
            
        }

       
       /////

        /// <summary>
        /// Creates api client without using config file
        /// </summary>
        /// <param name="authMethod">Authorization method - basic or oauth2</param>
        /// <param name="url"> url to koha instance</param>
        /// <param name="user">username for Basic auth or client ID for oauth</param>
        /// <param name="secret">password for Basic auth or client secret for oauth</param>
        public ApiClient(AuthMethod authMethod,string url,string user,string secret)
        {
            BaseUrl = url;
            client = new HttpClient();

            switch (authMethod)
            {
                case AuthMethod.Basic:
                    SetHTTPBasicHeader(user,secret);
                    break;
                case AuthMethod.ApiKey:
                   SetOauthHeader(user,secret);                 
                    break;
        }
    }

        private async Task<Token> GetToken(string id,string key)
        {
            var response = await client.PostAsync($"{BaseUrl}/api/v1/oauth/token", new FormUrlEncodedContent(new Dictionary<string,string>
            {
                { "grant_type", "client_credentials" },
                { "client_id", id },
                { "client_secret", key }
            }));
            var json = await StreamTostring(response);
            KohaJsonDeserializer deserializer = new KohaJsonDeserializer();
            //Console.WriteLine(json);
            return deserializer.DeserializeToken(json);         
        }
      
        private void SetHTTPBasicHeader(string u,string p)
        {
            var byteArray = Encoding.UTF8.GetBytes($"{u}:{p}");
            this.client.DefaultRequestHeaders.Authorization =
            new System.Net.Http.Headers.AuthenticationHeaderValue(
            "Basic", Convert.ToBase64String(byteArray));
        }


        private void SetOauthHeader(string ClientId,string ClientSecret)
        {
            Task.Run(async () =>
            {
                Token token = await GetToken(ClientId, ClientSecret);
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue
                    (token.TokenType, token.AccessToken);
            }).Wait();
        }

    }
}