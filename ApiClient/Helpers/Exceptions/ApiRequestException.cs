using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AllcandoJM.KohaFramework.ApiCore.Helpers.Exceptions
{
    public class ApiRequestException : Exception
    {

        public ApiRequestException() { }
        public ApiRequestException(string message) :base(message) { }

        public ApiRequestException(string message, Exception innerException)
       : base(message, innerException)
        { }

    }
}
