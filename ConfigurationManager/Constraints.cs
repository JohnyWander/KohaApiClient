using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace AllcandoJM.KohaFramework.ConfigurationManager
{
    internal class Constraints
    {
        public delegate bool Constraint(string ConfVal);
        public IDictionary<string, Constraint> ConfigConstraints = new Dictionary<string, Constraint>();

        public Constraints()
        {
            Constraint test = (x) => { return true; };


        }





    }
}
