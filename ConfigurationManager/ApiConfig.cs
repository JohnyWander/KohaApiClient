using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AllcandoJM.KohaFramework.ConfigurationManager
{
    public class ApiConfig : ConfigBase
    {
        public enum AuthMethod
        {
            HTTP_BASIC = 0,
            OAUTH2 = 1,
        }

        


        public AuthMethod AuthChoosed;


        private const string ApiConfigFname = "ApiConfig.ini";

        public enum ConfigNames
        {
            KOHA_URL =1,
            KOHA_PORT =2,
            API_AUTH =3,
            API_USERNAME=4,
            API_PASSWORD=5,
            KOHA_STAFF_URL=6,
            KOHA_STAFF_PORT=7,
            KOHA_STAFF_USERNAME=8,
            KOHA_STAFF_PASSWORD=9,
            API_CLIENT_ID=10,
            API_CLIENT_SECRET=11,
        }

        private List<ConfigValue> ConfigValues = new List<ConfigValue>()
        {
            new ConfigValue(1,"KOHA URL","",null),
            new ConfigValue(2,"KOHA PORT","",null),
            new ConfigValue(3,"API AUTH","",null),
            new ConfigValue(4,"API USERNAME","",null),
            new ConfigValue(5,"API PASSWORD","",null),
            new ConfigValue(6,"KOHA STAFF URL","",null),
            new ConfigValue(7,"KOHA STAFF PORT","",null),
            new ConfigValue(8,"KOHA STAFF USERNAME","",null),
            new ConfigValue(9,"KOHA STAFF PASSWORD","",null),
            new ConfigValue(10,"API CLIENT_ID","",null),
            new ConfigValue(11,"API CLIENT_SECRET","",null)
        };

        public string GetConfigValue(string key)
        {
            return ConfigValues.Where(x =>x.Key == key ).First().Value;
        }

        public string GetConfigValue(ConfigNames name)
        {
            return ConfigValues.Where(x=>x.Id == (int)name).First().Value;
        }


        public static string DefaultApiConfig =
@"# Basic Api configuration

KOHA URL=http://127.0.0.1
KOHA PORT= # Leave empty if using default http/https
API AUTH=HTTP_BASIC # HTTP_BASIC or oauth2

#Basic auth
API USERNAME= 
API PASSWORD= 

#oauth2
API CLIENT_ID=
API CLIENT_SECRET=


#Private reports - Use Staff url - and staff account

KOHA STAFF URL=http://127.0.0.1
KOHA STAFF PORT= # Leave empty if using default http/https

KOHA STAFF USERNAME=
KOHA STAFF PASSWORD=

";


        public ApiConfig()
        {
            if (!File.Exists(ApiConfigFname))
            {
                WriteDefaulConfig();
            }

            FileInfo f = new FileInfo(ApiConfigFname);
            

            List<string> configLines = File.ReadAllLines(ApiConfigFname).ToList();


            List<string> config = File.ReadAllLines(ApiConfigFname).ToList();

            config.ForEach(c =>
            {
                if (c.Contains("#"))
                {
                    c = c.Substring(0, c.IndexOf("#"));
                }

                if (c == "")
                {

                }
                else
                {
                    string[] split = c.Split('=');

                    ConfigValues.Where(x => x.Key == split[0]).First().Value = split[1].Trim();
                }

            });




        }

        internal void WriteDefaulConfig()
        {
            File.WriteAllText(ApiConfigFname, DefaultApiConfig);
        }


    }
}
