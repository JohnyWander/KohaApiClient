using AllcandoJM.KohaFramework.JsonDeserialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace AllcandoJM.KohaFramework.ApiCore
{
    public class ApiResponse
    {
        public HttpStatusCode ResponseCode;
        public bool IsSuccess;

        public string ErrorMessage;
        public string ErrorCode;

        

        public string ResponseRawJson;

        KohaJsonDeserializer deserializer = new KohaJsonDeserializer();

        public async Task<ApiResponse> ParseApiResponseAsync(HttpResponseMessage response)
        {
            ResponseCode = response.StatusCode;
            ResponseRawJson = await  response.Content.ReadAsStringAsync();
            
            if((int)ResponseCode>=200 && (int)ResponseCode<=206)
            {
                IsSuccess = true;
            }
            else
            {
                IsSuccess = false;
                Error err = deserializer.DeserializeError(ResponseRawJson);
                this.ErrorMessage = err.error;
                this.ErrorCode = err.error_code;
            }

            return this;
        }



    }
}
