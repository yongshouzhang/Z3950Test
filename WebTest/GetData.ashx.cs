using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Z3950;
using Newtonsoft.Json;

namespace WebTest
{
    /// <summary>
    /// GetData 的摘要说明
    /// </summary>
    public class GetData : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            var server = new Server()
            {
                Name = "mytest",
                //Uri = "z3950.loc.gov",
                Uri = "202.96.31.28",
                Port = 9991,
                DatabaseName = "UCS01U",
                Username = "L410581YHH",
                Password = "20141030"
            };
            int count = 0;
            var tt = server.GetRecords(Bib1Attr.Title, "javascript", 10, 1, out count);
            context.Response.Write(JsonConvert.SerializeObject(tt));

        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}