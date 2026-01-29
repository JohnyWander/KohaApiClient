using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace AllcandoJM.KohaFramework.JsonDeserialization
{
    public class PatronFull
    {
        [JsonPropertyName("patron_id")]
        public int PatronId { get; set; }

        [JsonPropertyName("cardnumber")]
        public string? CardNumber { get; set; }

        [JsonPropertyName("surname")]
        public string? Surname { get; set; }

        [JsonPropertyName("firstname")]
        public string? FirstName { get; set; }

        [JsonPropertyName("preferred_name")]
        public string? PreferredName { get; set; }

        [JsonPropertyName("middle_name")]
        public string? MiddleName { get; set; }

        [JsonPropertyName("title")]
        public string? Title { get; set; }

        [JsonPropertyName("other_name")]
        public string? OtherName { get; set; }

        [JsonPropertyName("initials")]
        public string? Initials { get; set; }

        [JsonPropertyName("pronouns")]
        public string? Pronouns { get; set; }

        [JsonPropertyName("street_number")]
        public string? StreetNumber { get; set; }

        [JsonPropertyName("street_type")]
        public string? StreetType { get; set; }

        [JsonPropertyName("address")]
        public string? Address { get; set; }

        [JsonPropertyName("address2")]
        public string? Address2 { get; set; }

        [JsonPropertyName("city")]
        public string? City { get; set; }

        [JsonPropertyName("state")]
        public string? State { get; set; }

        [JsonPropertyName("postal_code")]
        public string? PostalCode { get; set; }

        [JsonPropertyName("country")]
        public string? Country { get; set; }

        [JsonPropertyName("email")]
        public string? Email { get; set; }

        [JsonPropertyName("phone")]
        public string? Phone { get; set; }

        [JsonPropertyName("mobile")]
        public string? Mobile { get; set; }

        [JsonPropertyName("fax")]
        public string? Fax { get; set; }

        [JsonPropertyName("secondary_email")]
        public string? SecondaryEmail { get; set; }

        [JsonPropertyName("secondary_phone")]
        public string? SecondaryPhone { get; set; }

        [JsonPropertyName("altaddress_street_number")]
        public string? AltAddressStreetNumber { get; set; }

        [JsonPropertyName("altaddress_street_type")]
        public string? AltAddressStreetType { get; set; }

        [JsonPropertyName("altaddress_address")]
        public string? AltAddressAddress { get; set; }

        [JsonPropertyName("altaddress_address2")]
        public string? AltAddressAddress2 { get; set; }

        [JsonPropertyName("altaddress_city")]
        public string? AltAddressCity { get; set; }

        [JsonPropertyName("altaddress_state")]
        public string? AltAddressState { get; set; }

        [JsonPropertyName("altaddress_postal_code")]
        public string? AltAddressPostalCode { get; set; }

        [JsonPropertyName("altaddress_country")]
        public string? AltAddressCountry { get; set; }

        [JsonPropertyName("altaddress_email")]
        public string? AltAddressEmail { get; set; }

        [JsonPropertyName("altaddress_phone")]
        public string? AltAddressPhone { get; set; }

        [JsonPropertyName("date_of_birth")]
        public DateTime? DateOfBirth { get; set; }

        [JsonPropertyName("library_id")]
        public string? LibraryId { get; set; }

        [JsonPropertyName("category_id")]
        public string? CategoryId { get; set; }

        [JsonPropertyName("date_enrolled")]
        public DateTime? DateEnrolled { get; set; }

        [JsonPropertyName("expiry_date")]
        public DateTime? ExpiryDate { get; set; }

        [JsonPropertyName("date_renewed")]
        public DateTime? DateRenewed { get; set; }

        [JsonPropertyName("incorrect_address")]
        public bool IncorrectAddress { get; set; }

        [JsonPropertyName("patron_card_lost")]
        public bool PatronCardLost { get; set; }

        [JsonPropertyName("expired")]
        public bool Expired { get; set; }

        [JsonPropertyName("restricted")]
        public bool Restricted { get; set; }

        [JsonPropertyName("staff_notes")]
        public string? StaffNotes { get; set; }

        [JsonPropertyName("relationship_type")]
        public string? RelationshipType { get; set; }

        [JsonPropertyName("gender")]
        public string? Gender { get; set; }

        [JsonPropertyName("userid")]
        public string? UserId { get; set; }

        [JsonPropertyName("opac_notes")]
        public string? OpacNotes { get; set; }

        [JsonPropertyName("altaddress_notes")]
        public string? AltAddressNotes { get; set; }

        [JsonPropertyName("statistics_1")]
        public string? Statistics1 { get; set; }

        [JsonPropertyName("statistics_2")]
        public string? Statistics2 { get; set; }

        [JsonPropertyName("autorenew_checkouts")]
        public bool AutoRenewCheckouts { get; set; }

        [JsonPropertyName("altcontact_firstname")]
        public string? AltContactFirstName { get; set; }

        [JsonPropertyName("altcontact_surname")]
        public string? AltContactSurname { get; set; }

        [JsonPropertyName("altcontact_address")]
        public string? AltContactAddress { get; set; }

        [JsonPropertyName("altcontact_address2")]
        public string? AltContactAddress2 { get; set; }

        [JsonPropertyName("altcontact_city")]
        public string? AltContactCity { get; set; }

        [JsonPropertyName("altcontact_state")]
        public string? AltContactState { get; set; }

        [JsonPropertyName("altcontact_postal_code")]
        public string? AltContactPostalCode { get; set; }

        [JsonPropertyName("altcontact_country")]
        public string? AltContactCountry { get; set; }

        [JsonPropertyName("altcontact_phone")]
        public string? AltContactPhone { get; set; }

        [JsonPropertyName("sms_number")]
        public string? SmsNumber { get; set; }

        [JsonPropertyName("sms_provider_id")]
        public int? SmsProviderId { get; set; }

        [JsonPropertyName("privacy")]
        public int Privacy { get; set; }

        [JsonPropertyName("privacy_guarantor_checkouts")]
        public int PrivacyGuarantorCheckouts { get; set; }

        [JsonPropertyName("privacy_guarantor_fines")]
        public bool PrivacyGuarantorFines { get; set; }

        [JsonPropertyName("check_previous_checkout")]
        public string? CheckPreviousCheckout { get; set; }

        [JsonPropertyName("updated_on")]
        public DateTime? UpdatedOn { get; set; }

        [JsonPropertyName("last_seen")]
        public DateTime? LastSeen { get; set; }

        [JsonPropertyName("lang")]
        public string? Lang { get; set; }

        [JsonPropertyName("login_attempts")]
        public int LoginAttempts { get; set; }

        [JsonPropertyName("overdrive_auth_token")]
        public string? OverdriveAuthToken { get; set; }

        [JsonPropertyName("anonymized")]
        public bool Anonymized { get; set; }

        [JsonPropertyName("extended_attributes")]
        public List<ExtendedAttribute>? ExtendedAttributes { get; set; }

        [JsonPropertyName("checkouts_count")]
        public int CheckoutsCount { get; set; }

        [JsonPropertyName("overdues_count")]
        public int OverduesCount { get; set; }

        [JsonPropertyName("account_balance")]
        public decimal AccountBalance { get; set; }

        [JsonPropertyName("library")]
        public Dictionary<string, object>? Library { get; set; }

        [JsonPropertyName("protected")]
        public bool Protected { get; set; }

        [JsonPropertyName("_strings")]
        public Dictionary<string, string>? Strings { get; set; }
    }

    public class ExtendedAttribute
    {
        [JsonPropertyName("extended_attribute_id")]
        public int ExtendedAttributeId { get; set; }

        [JsonPropertyName("type")]
        public string? Type { get; set; }

        [JsonPropertyName("value")]
        public string? Value { get; set; }
    }
}
