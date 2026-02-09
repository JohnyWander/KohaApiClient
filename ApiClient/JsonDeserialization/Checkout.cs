using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace AllcandoJM.KohaFramework.JsonDeserialization
{
    public class Checkout
    {
        [JsonPropertyName("checkout_id")]
        public int? CheckoutId { get; set; }

        [JsonPropertyName("patron_id")]
        public int PatronId { get; set; }

        [JsonPropertyName("item_id")]
        public int ItemId { get; set; }

        [JsonPropertyName("booking_id")]
        public int BookingId { get; set; }

        [JsonPropertyName("external_id")]
        public string? ExternalId { get; set; }

        [JsonPropertyName("due_date")]
        public DateTime? DueDate { get; set; }

        [JsonPropertyName("library_id")]
        public string? LibraryId { get; set; }

        [JsonPropertyName("issuer_id")]
        public int IssuerId { get; set; }

        [JsonPropertyName("checkin_date")]
        public DateTime? CheckinDate { get; set; }

        [JsonPropertyName("checkin_library_id")]
        public string? CheckinLibraryId { get; set; }

        [JsonPropertyName("last_renewed_date")]
        public DateTime? LastRenewedDate { get; set; }

        [JsonPropertyName("renewals_count")]
        public int RenewalsCount { get; set; }

        [JsonPropertyName("unseen_renewals")]
        public int UnseenRenewals { get; set; }

        [JsonPropertyName("auto_renew")]
        public bool AutoRenew { get; set; }

        [JsonPropertyName("auto_renew_error")]
        public string? AutoRenewError { get; set; }

        // Note: this is a string in your sample, not ISO date
        [JsonPropertyName("timestamp")]
        public string? Timestamp { get; set; }

        [JsonPropertyName("checkout_date")]
        public DateTime? CheckoutDate { get; set; }

        [JsonPropertyName("onsite_checkout")]
        public bool OnsiteCheckout { get; set; }

        [JsonPropertyName("note")]
        public string? Note { get; set; }

        [JsonPropertyName("note_date")]
        public DateOnly? NoteDate { get; set; }

        [JsonPropertyName("note_seen")]
        public bool? NoteSeen { get; set; }

        // Empty objects in sample → safest generic representation
        [JsonPropertyName("issuer")]
        public Dictionary<string, object>? Issuer { get; set; }

        [JsonPropertyName("item")]
        public Dictionary<string, object>? Item { get; set; }

        [JsonPropertyName("library")]
        public Dictionary<string, object>? Library { get; set; }

        [JsonPropertyName("patron")]
        public Dictionary<string, object>? Patron { get; set; }

        [JsonPropertyName("booking")]
        public Dictionary<string, object>? Booking { get; set; }
    }
}
