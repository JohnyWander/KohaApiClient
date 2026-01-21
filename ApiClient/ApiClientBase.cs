using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AllcandoJM.KohaFramework
{
     public abstract class ApiClientBase
    {
        public async Task<string> StreamTostring(HttpResponseMessage response)
        {
            byte[] buffer = new byte[100000];
            Stream s = await response.Content.ReadAsStreamAsync();
            int read = await s.ReadAsync(buffer, 0, (int)s.Length);

            return Encoding.UTF8.GetString(buffer, 0, read);
        }

    }
}
