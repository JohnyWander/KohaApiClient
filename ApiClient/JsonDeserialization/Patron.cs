using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json.Serialization;

namespace AllcandoJM.KohaFramework.JsonDeserialization
{
    public class Patron
    {
        [JsonPropertyName("firstname")]
        public string Firstname { get; set; }

        [JsonPropertyName("surname")]
        public string Surname { get; set; }


        [JsonPropertyName("cardnumber")]
        public string Cardnumber { get; set; }

        [JsonPropertyName("category_id")]
        public string CategoryID { get; set; }

    }
}
