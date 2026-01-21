using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace AllcandoJM.KohaFramework.JsonDeserialization
{
    public class ItemAfterUpdateDeserialized
    {
        [JsonPropertyName("item_id")]
        public int ItemId { get; set; }
        [JsonPropertyName("external_id")]
        public string ExternalId { get; set; }

        [JsonPropertyName("item_type_id")]
        public string ItemTypeId { get; set; }

    }
}
