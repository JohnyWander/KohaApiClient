using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace AllcandoJM.KohaFramework.JsonDeserialization
{
    public class Biblio
    {
        [JsonPropertyName("abstract")]
        public string? Abstract { get; set; }

        [JsonPropertyName("author")]
        public string? Author { get; set; }

        [JsonPropertyName("biblio_id")]
        public int BiblioId { get; set; }

        [JsonPropertyName("copyright_date")]
        public int? CopyrightDate { get; set; }

        [JsonPropertyName("creation_date")]
        public DateTime? CreationDate { get; set; }

        [JsonPropertyName("edition_statement")]
        public string? EditionStatement { get; set; }

        [JsonPropertyName("illustrations")]
        public string? Illustrations { get; set; }

        [JsonPropertyName("isbn")]
        public string? Isbn { get; set; }

        [JsonPropertyName("material_size")]
        public string? MaterialSize { get; set; }

        [JsonPropertyName("pages")]
        public string? Pages { get; set; }

        [JsonPropertyName("publication_place")]
        public string? PublicationPlace { get; set; }

        [JsonPropertyName("publisher")]
        public string? Publisher { get; set; }

        [JsonPropertyName("serial")]
        public bool Serial { get; set; }

        [JsonPropertyName("timestamp")]
        public DateTime? Timestamp { get; set; }

        [JsonPropertyName("title")]
        public string? Title { get; set; }

        [JsonPropertyName("uniform_title")]
        public string? UniformTitle { get; set; }
    }
}
