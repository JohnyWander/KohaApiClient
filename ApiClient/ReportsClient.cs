using AllcandoJM.KohaFramework.ApiCore;
using AllcandoJM.KohaFramework.ConfigurationManager;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AllcandoJM.KohaFramework
{
    public  class ReportsClient : ApiClient
    {
        HttpClient client;
        ApiConfig config;

        string BaseUrl;


        public ReportsClient()
        {
            config = new ApiConfig();
            client = new HttpClient();

            BaseUrl = config.GetConfigValue("KOHA STAFF URL");
            
            string u = config.GetConfigValue("KOHA STAFF USERNAME");
            string p = config.GetConfigValue("KOHA STAFF PASSWORD");

            string cookie = CookieGrabber(BaseUrl,u,p).GetAwaiter().GetResult(); 
           


        }


        public async Task<string> CookieGrabber(string StaffIface,string user,string password)
        {
            var loginPageRequest = new HttpRequestMessage(HttpMethod.Get, StaffIface);
            var loginpageResponse = await client.SendAsync(loginPageRequest);


            string responseString = await StreamTostring(loginpageResponse);
            string FormString = responseString.Substring(responseString.IndexOf("<form action="), responseString.IndexOf("</form>"));


            string[] lines = FormString.Split("\n");

            string csrfTokenLine = lines.Where(l => l.Contains("csrf_token")).FirstOrDefault();
            string csrfToken = csrfTokenLine.Split("value=\"")[1].Replace("\" />", "");

            var request = new HttpRequestMessage(HttpMethod.Post, $"{StaffIface}/cgi-bin/koha/mainpage.pl");

            var formData = new Dictionary<string, string>
{
    { "csrf_token", csrfToken },
    { "login_userid", user },
    { "login_password", password },
    { "op", "cud-login" },
    { "koha_login_context", "intranet" }
};

            // Set the content as URL-encoded (the correct format for HTML forms)
            request.Content = new FormUrlEncodedContent(formData);

            // Send the request
            var resp = await client.SendAsync(request);

            return await StreamTostring(resp);

        }


        public async Task<string> GetPrivateReport(string reportID,KeyValuePair<string,string>[] args)
        {
            string req = $"{BaseUrl}/cgi-bin/koha/svc/report?id={reportID}";

            foreach(var arg in args)
            {
                req += $"&param_name={arg.Key}&sql_params={arg.Value}";
            }
            var respone = await client.GetAsync(req);
            return await StreamTostring(respone);

        }

        public async Task<string> GetPrivateReport(string reportID)
        {
            string req = $"{BaseUrl}/cgi-bin/koha/svc/report?id={reportID}&op=run&limit=1000";
            var respone = await client.GetAsync(req);
            return await StreamTostring(respone);
        }

        
        public async Task<string> GetPrivateReportCsvAsync(string reportID,string reportname="report")
        {
            string req = $"{BaseUrl}/cgi-bin/koha/reports/guided_reports.pl?op=export&format=csv&id={reportID}&reportname=borr_w_overdues";
            var response = await client.GetAsync(req);
            var csv = await response.Content.ReadAsStringAsync();
            return csv;
        }

        public async Task<string> GetPrivateReportCsvAsync(string reportID, KeyValuePair<string, string>[] args,string reportname="report")
        {

            string req = $"{BaseUrl}/cgi-bin/koha/reports/guided_reports.pl?op=export&format=csv&id={reportID}&reportname={reportname}";
            foreach (var arg in args)
            {
                req += $"&param_name={arg.Key}&sql_params={arg.Value}";
            }
            var response = await client.GetAsync(req);
            var csv = await response.Content.ReadAsStringAsync();
            return csv;
        }

    }
}
