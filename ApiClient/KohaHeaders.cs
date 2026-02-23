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




    }
}
