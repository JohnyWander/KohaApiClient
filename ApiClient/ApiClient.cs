using AllcandoJM.KohaFramework.ConfigurationManager;
using AllcandoJM.KohaFramework.ApiCore.Interfaces;
using System.Security;
using System.Text;
using AllcandoJM.KohaFramework.JsonDeserialization;
using AllcandoJM.KohaFramework.ApiCore.Helpers;
using AllcandoJM.KohaFramework.ApiCore.Helpers.Exceptions;
using AllcandoJM.KohaFramework.ApiCore;
using System.Diagnostics;

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
        public ExceptionHandler ExceptionHandler = new ExceptionHandler();

        protected void FireExceptionEvent(Exception e)
        {
            this.ExceptionHandler.RaiseExceptionEvent(e);
        }
     
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
            

            if (method == "HTTP_BASIC")
            {
                authMethod = AuthMethod.Basic;
            }
            else if (method == "oauth2")
            {
                authMethod = AuthMethod.ApiKey;
            }
            else
            {
                throw new ArgumentException("No such auth method");
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

        /// <summary>
        /// Creates api client with same Http context as other api client
        /// </summary>
        /// <param name="client"></param>
        public ApiClient(ApiClient client)
        {
            this.BaseUrl = client.BaseUrl;
            this.client = client.client;
            this.authMethod = client.authMethod;
        }


        protected async Task<string> HandleString(HttpResponseMessage response)
        {
            ApiResponse resp = await base.ParseResponseAsync(response);
            if (resp.IsSuccess)
            {
                return resp.ResponseRawJson;
            }
            else
            {
                string err = $"Failed - HttpCode: {(int)resp.ResponseCode}({resp.ResponseCode.ToString()}) {resp.ErrorCode},{resp.ErrorMessage} when trying - {response.RequestMessage.RequestUri}";
                FireExceptionEvent(new ApiRequestException(err));

                return $"{err}";
            }

        }

        public async Task<ApiResponse> HandleResponse(HttpResponseMessage response)
        {
            return await base.ParseResponseAsync(response);
        }



        private async Task<Token> GetToken(string id,string key)
        {
            var response = await client.PostAsync($"{BaseUrl}/api/v1/oauth/token", new FormUrlEncodedContent(new Dictionary<string,string>
            {
                { "grant_type", "client_credentials" },
                { "client_id", id },
                { "client_secret", key }
            }));
          
            ApiResponse resp = await base.ParseResponseAsync(response);

            if (resp.IsSuccess)
            {
                KohaJsonDeserializer deserializer = new KohaJsonDeserializer();
                //Console.WriteLine(json);
                return deserializer.DeserializeToken(resp.ResponseRawJson);
            }
            else
            {
                this.ExceptionHandler.RaiseExceptionEvent(
                new ApiAuthException($"Could not get token with error - HttpStatusCode:{(int)resp.ResponseCode}, ErrCode:{resp.ErrorCode},Msg:{resp.ErrorMessage}"));
                return null;
            }
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