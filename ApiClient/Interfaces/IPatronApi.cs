using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AllcandoJM.KohaFramework.ApiCore.Interfaces
{
    public interface IPatronApi
    {
        public  Task<string> PostCheckLogin();
        public Task<string> GetPatronById(string patron_id);
        public  Task<string> GetPatronByCardnumber(string cardnumber);

        public Task<string> GetManyPatronsByCardnumbers(string[] CardNumbers);

    }
}
