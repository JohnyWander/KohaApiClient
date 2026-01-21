using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AllcandoJM.KohaFramework
{
    public enum ValueType
    {
        Text,
        Number
    }
    public class JsonArg
    {
        public string name;
        public string value;
        public ValueType type;

        public JsonArg(string name,string value,ValueType vType)
        {
            this.name = name;
            this.value = value;
            this.type = vType;
        }
    }


    public  class JsonArgsCreator
    {

        public  string CreateArgs(JsonArg[] args)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("{\n");
            bool first = true;
            foreach(JsonArg arg in args)
            {
                string argvaltext="";
                if(arg.type == ValueType.Text)
                {
                    argvaltext = $"\"{arg.value}\"";
                }
                else
                {
                    argvaltext = arg.value;
                }
                if(first == false) 
                {
                    sb.Append(",\n");
                }
                first = false;
                sb.Append($"\"{arg.name}\": {argvaltext}\n");

                
            }
           
            sb.Append("}");
            return sb.ToString();

        }


    }
}
