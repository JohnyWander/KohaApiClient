using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AllcandoJM.KohaFramework.ApiCore
{
    public class ListItemsQParams : QueryParams
    {
        


        public ListItemsQParams() { }


        public ListItemsQParams AddExternalId(string value)
        {
            AddSelection($"external_id={value}&");
            return this;
        }



        private protected override ListItemsQParams AddSelection(string selectionstring)
        {
            base.argString.Append($"{selectionstring}&");
            return this;
        }


        public override string GetParamString()
        {
            return argString.ToString();
        }

    }
}
