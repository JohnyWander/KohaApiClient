using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AllcandoJM.KohaFramework.ApiCore.Helpers.Exceptions
{
    public class ApiAuthException : Exception
    {
        public ApiAuthException()
        {
        
        }

        public ApiAuthException(string message) : base(message)
        {

        }

        public ApiAuthException(string message, Exception innerException)
        : base(message, innerException)
        { }

    }
}
