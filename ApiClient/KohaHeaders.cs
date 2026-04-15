using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AllcandoJM.KohaFramework.ApiCore
{
    public static class KohaHeaders
    {
        [Flags]
        public enum ItemXkohaEmbedHeaders
        {
            strings = 1,
            effective_bookable = 2,
            None = 4
        }

        public static IDictionary<int, string> ItemHeaders = new Dictionary<int, string>()
        {
            {(int)ItemXkohaEmbedHeaders.strings,"+strings" },
            {(int)ItemXkohaEmbedHeaders.effective_bookable,"effective_bookable"}
        };

        [Flags]
        public enum ItemsXkohaEmbedHeaders
        {
            strings = 1,
            biblio = 2,
            effective_bookable = 4,
            None = 8
        }

        public static IDictionary<int, string> ItemsHeaders = new Dictionary<int, string>()
        {
            { (int)ItemsXkohaEmbedHeaders.strings,"+strings" },
            { (int)ItemsXkohaEmbedHeaders.biblio,"biblio" },
            { (int)ItemsXkohaEmbedHeaders.effective_bookable,"effective_bookable"}
        };


        [Flags]
        public enum PatronXkohaEmbedHeaders
        {
            extended_attributes = 1,
            checkoutspluscount = 2,
            overduespluscount = 4,
            account_balance = 8,
            library = 16
        }

        public static IDictionary<int, string> PatronHeaders = new Dictionary<int, string>()
        {

        };


        [Flags]
        public enum CheckoutXkohaEmbedHeaders
        {
            booking =1,
            issuer = 2,
            item =4,
            itembiblio =8,
            library = 16,
            renewals = 32,
            patron = 64,
            none = 128
        }

        public static IDictionary<int, string> CheckoutsHeaders = new Dictionary<int, string>()
        {
            {(int)CheckoutXkohaEmbedHeaders.booking,"booking" },
            {(int)CheckoutXkohaEmbedHeaders.issuer,"issuer" },
            {(int)CheckoutXkohaEmbedHeaders.item,"item" },
            {(int)CheckoutXkohaEmbedHeaders.itembiblio,"item.biblio" },
            {(int)CheckoutXkohaEmbedHeaders.library,"library" },
            {(int)CheckoutXkohaEmbedHeaders.renewals,"renewals" },
            {(int)CheckoutXkohaEmbedHeaders.patron,"patron" }

        };



     



    }
}
