using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Text.Json.Serialization;

namespace AllcandoJM.KohaFramework.JsonDeserialization
{
    public class Error
    {
       [JsonPropertyName("error")]
       public string error { get; set; }

       [JsonPropertyName("error_code")]
       public string error_code { get; set; }


    }
}
