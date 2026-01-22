using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AllcandoJM.KohaFramework.ApiCore.Helpers
{
    public class ExceptionHandler
    {
        public delegate void OnException(Exception ex);

        public event OnException HandleEX;

        internal void RaiseExceptionEvent(Exception ex)
        {
            if (HandleEX != null)
            {
                HandleEX?.Invoke(ex);
            }
            else
            {
                throw ex;
            }
        }
        



    }
}
