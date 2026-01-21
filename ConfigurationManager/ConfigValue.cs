using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AllcandoJM.KohaFramework.ConfigurationManager
{
    public class ConfigValue
    {
        public int Id;

        public string Key;
        public string Value;

        public string[] ConstraintsRequired;

        public ConfigValue(int id,string key, string value, string[] constraintsRequired)
        {
            Id = id;
            Key = key;
            Value = value;
            ConstraintsRequired = constraintsRequired;
        }





    }
}
