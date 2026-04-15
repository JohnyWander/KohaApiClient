
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace AllcandoJM.KohaFramework.ApiCore
{
    public abstract class QueryParams
    {
        protected StringBuilder argString = new StringBuilder("?");
        protected StringBuilder qBuilder = new StringBuilder("q={");
        public enum _match
        {
            contains,
            exact,
            starts_with,
            ends_with
        }

        private protected abstract QueryParams AddSelection(string selectionstring);



        public QueryParams Add_match(_match criteria)
        {
            argString.Append($"_match={criteria.ToString()}&");
            return this;
        }

        public QueryParams Add_page(string value)
        {
            argString.Append($"_page={value}&");
            return this;
        }

        public QueryParams Add_per_page(string value)
        {
            argString.Append($"_per_page={value}&");
            return this;
        }




        public abstract string GetParamString();


    }
}
