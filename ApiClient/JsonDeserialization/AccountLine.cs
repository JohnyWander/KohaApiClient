using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace AllcandoJM.KohaFramework.JsonDeserialization
{ 
   public class AccountLine
    {
        [JsonPropertyName("account_line_id")]
        public int AccountLineId { get; set; }

        [JsonPropertyName("amount")]
        public decimal Amount { get; set; }

        [JsonPropertyName("amount_outstanding")]
        public decimal AmountOutstanding { get; set; }

        [JsonPropertyName("cash_register_id")]
        public int? CashRegisterId { get; set; }

        [JsonPropertyName("checkout_id")]
        public int? CheckoutId { get; set; }

        [JsonPropertyName("old_checkout_id")]
        public int? OldCheckoutId { get; set; }

        [JsonPropertyName("date")]
        public DateTime Date { get; set; }

        [JsonPropertyName("description")]
        public string? Description { get; set; }

        [JsonPropertyName("interface")]
        public string? Interface { get; set; }

        [JsonPropertyName("internal_note")]
        public string? InternalNote { get; set; }

        [JsonPropertyName("item_id")]
        public int? ItemId { get; set; }

        [JsonPropertyName("library_id")]
        public string? LibraryId { get; set; }

        [JsonPropertyName("patron_id")]
        public int PatronId { get; set; }

        [JsonPropertyName("payout_type")]
        public string? PayoutType { get; set; }

        [JsonPropertyName("status")]
        public string? Status { get; set; }

        [JsonPropertyName("timestamp")]
        public DateTime Timestamp { get; set; }

        [JsonPropertyName("type")]
        public string? Type { get; set; }

        [JsonPropertyName("user_id")]
        public int? UserId { get; set; }
    }
}
