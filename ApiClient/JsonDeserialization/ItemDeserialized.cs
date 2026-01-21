using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace AllcandoJM.KohaFramework.JsonDeserialization
{
    public class ItemDeserialized
    {
        [JsonPropertyName("acquisition_date")]
        public DateTime? AcquisitionDate { get; set; }

        [JsonPropertyName("acquisition_source")]
        public string AcquisitionSource { get; set; }

        [JsonPropertyName("biblio_id")]
        public int BiblioId { get; set; }

        [JsonPropertyName("bookable")]
        public bool Bookable { get; set; }

        [JsonPropertyName("call_number_sort")]
        public string CallNumberSort { get; set; }

        [JsonPropertyName("call_number_source")]
        public string CallNumberSource { get; set; }

        [JsonPropertyName("callnumber")]
        public string CallNumber { get; set; }

        [JsonPropertyName("checked_out_date")]
        public DateTime? CheckedOutDate { get; set; }

        [JsonPropertyName("checkouts_count")]
        public int? CheckoutsCount { get; set; }

        [JsonPropertyName("coded_location_qualifier")]
        public string CodedLocationQualifier { get; set; }

        [JsonPropertyName("collection_code")]
        public string CollectionCode { get; set; }

        [JsonPropertyName("copy_number")]
        public string CopyNumber { get; set; }

        [JsonPropertyName("damaged_date")]
        public DateTime? DamagedDate { get; set; }

        [JsonPropertyName("damaged_status")]
        public int DamagedStatus { get; set; }

        [JsonPropertyName("effective_bookable")]
        public bool EffectiveBookable { get; set; }

        [JsonPropertyName("effective_item_type_id")]
        public string EffectiveItemTypeId { get; set; }

        [JsonPropertyName("effective_not_for_loan_status")]
        public int EffectiveNotForLoanStatus { get; set; }

        [JsonPropertyName("exclude_from_local_holds_priority")]
        public bool ExcludeFromLocalHoldsPriority { get; set; }

        [JsonPropertyName("extended_subfields")]
        public string ExtendedSubfields { get; set; }

        [JsonPropertyName("external_id")]
        public string ExternalId { get; set; }

        [JsonPropertyName("holding_library_id")]
        public string HoldingLibraryId { get; set; }

        [JsonPropertyName("holds_count")]
        public int? HoldsCount { get; set; }

        [JsonPropertyName("home_library_id")]
        public string HomeLibraryId { get; set; }

        [JsonPropertyName("internal_notes")]
        public string InternalNotes { get; set; }

        [JsonPropertyName("inventory_number")]
        public string InventoryNumber { get; set; }

        [JsonPropertyName("item_id")]
        public int ItemId { get; set; }

        [JsonPropertyName("item_type_id")]
        public string ItemTypeId { get; set; }

        [JsonPropertyName("last_checkout_date")]
        public DateTime? LastCheckoutDate { get; set; }

        [JsonPropertyName("last_seen_date")]
        public DateTime? LastSeenDate { get; set; }

        [JsonPropertyName("localuse")]
        public string LocalUse { get; set; }

        [JsonPropertyName("location")]
        public string Location { get; set; }

        [JsonPropertyName("lost_date")]
        public DateTime? LostDate { get; set; }

        [JsonPropertyName("lost_status")]
        public int LostStatus { get; set; }

        [JsonPropertyName("materials_notes")]
        public string MaterialsNotes { get; set; }

        [JsonPropertyName("new_status")]
        public string NewStatus { get; set; }

        [JsonPropertyName("not_for_loan_status")]
        public int NotForLoanStatus { get; set; }

        [JsonPropertyName("permanent_location")]
        public string PermanentLocation { get; set; }

        [JsonPropertyName("public_notes")]
        public string PublicNotes { get; set; }

        [JsonPropertyName("purchase_price")]
        public double? PurchasePrice { get; set; }

        [JsonPropertyName("renewals_count")]
        public int? RenewalsCount { get; set; }

        [JsonPropertyName("replacement_price")]
        public double? ReplacementPrice { get; set; }

        [JsonPropertyName("replacement_price_date")]
        public DateTime? ReplacementPriceDate { get; set; }

        [JsonPropertyName("restricted_status")]
        public string RestrictedStatus { get; set; }

        [JsonPropertyName("serial_issue_number")]
        public string SerialIssueNumber { get; set; }

        [JsonPropertyName("shelving_control_number")]
        public string ShelvingControlNumber { get; set; }

        [JsonPropertyName("timestamp")]
        public DateTime? Timestamp { get; set; }

        [JsonPropertyName("uri")]
        public string Uri { get; set; }

        [JsonPropertyName("withdrawn")]
        public int Withdrawn { get; set; }

        [JsonPropertyName("withdrawn_date")]
        public DateTime? WithdrawnDate { get; set; }
    }
}
