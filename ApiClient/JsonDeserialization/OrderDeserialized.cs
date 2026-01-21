using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AllcandoJM.KohaFramework.JsonDeserialization
{
    public class OrderDeserialized
    {

        public int biblio_id { get; set; }
        public string? cancellation_date { get; set; }
        public string? cancellation_reason { get; set; }
        public int? deleted_biblio_id { get; set; }
        public int? desk_id { get; set; }
        public string? expiration_date { get; set; }
        public string hold_date { get; set; }
        public int hold_id { get; set; }
        public int? item_group_id { get; set; }
        public int? item_id { get; set; }
        public bool item_level { get; set; }
        public string? item_type { get; set; }
        public bool lowest_priority { get; set; }
        public bool non_priority { get; set; }
        public string notes { get; set; }
        public int patron_id { get; set; }
        public string pickup_library_id { get; set; }
        public int priority { get; set; }
        public string? status { get; set; }
        public bool suspended { get; set; }
        public string? suspended_until { get; set; }
        public string timestamp { get; set; }
        public string? waiting_date { get; set; }
    }
}
