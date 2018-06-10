using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json;
using VisualizeHealthData.Web.Models.Domain.Model.Existio;

namespace VisualizeHealthData.Web.Models.Domain.Services
{
    public class ExistioService
    {
        public string UserName { get; set; }
        public string Password { get; set; }

        public ExistioService()
        {
            UserName = ConfigurationManager.AppSettings["ExistioUsername"];
            Password = ConfigurationManager.AppSettings["ExistioPassword"];
        }

        public List<ExistioDataPoint> Get(ExistioDataType dataType)
        {
            List<ExistioDataPoint> data = new List<ExistioDataPoint>();

            var token = GetAuthenticationToken();
            using (var client = new WebClient())
            {
                client.Headers[HttpRequestHeader.ContentType] = "application/x-www-form-urlencoded";
                var tokenStr = "Token " + token;

                var url = string.Format("https://exist.io/api/1/users/$self/attributes/{0}/?date_min=2013-01-01",
                    dataType);

                client.Headers[HttpRequestHeader.Authorization] = tokenStr;
                try
                {
                    var res = client.DownloadString(url);
                    var obj = JsonConvert.DeserializeObject<ExistioResultSetDTO>(res);

                    foreach (var ob in obj.results)
                    {
                        if (ob.value.HasValue && ob.value.Value > 0)
                        {
                            if (dataType == ExistioDataType.energy)
                            {

                                ob.value = ob.value.Value / 4.184m; // convert from kJ to kcal

                            }

                            data.Add(new ExistioDataPoint(ob));
                        }
                    }
                }
                catch (Exception e)
                {
                }
            }
            return data;
        }

        private string GetAuthenticationToken()
        {
            using (var client = new WebClient())
            {
                client.Headers[HttpRequestHeader.ContentType] = "application/x-www-form-urlencoded";

                var reqparm = new System.Collections.Specialized.NameValueCollection();
                reqparm.Add("username", UserName);
                reqparm.Add("password", Password);
                byte[] responsebytes = client.UploadValues("https://exist.io/api/1/auth/simple-token/", "POST",
                    reqparm);
                string responsebody = Encoding.UTF8.GetString(responsebytes);

                var token = JsonConvert.DeserializeObject<ExistioToken>(responsebody);
                return token.Token;
            }
        }
    }
}