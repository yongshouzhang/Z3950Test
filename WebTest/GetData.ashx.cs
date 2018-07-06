using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Z3950;
using Z3950.Marc;
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

            string pi = context.Request["pi"] ?? "1", ps = context.Request["ps"] ?? "10";
            int pageIndex = int.Parse(pi), pageSize = int.Parse(ps);
            context.Response.ContentType = "text/plain";
            string type = context.Request["type"];
            Bib1Attr SearchType = Bib1Attr.Title;
            if (type == "1")
            {
                SearchType = Bib1Attr.Author;
            }else if (type == "2")
            {
                SearchType = Bib1Attr.ISBN;
            }
            string kw = context.Request["kw"] ?? "java";


            var server = new Server()
            {
                Name = "mytest",
                //Uri = "z3950.loc.gov",
                Uri = "202.96.31.28",
                Port = 9991,
                DatabaseName = "UCS01",
                Username = "L410581YHH",
                Password = "20141030"
            };
            int count = 0;
            var tt = server.GetRecordsMarc(SearchType, kw, pageSize, pageIndex, out count).Select(obj =>
            {
                return new {
                    Title=obj.Get_Data_Subfield(200,'a'),
                    Author= obj.Get_Data_Subfield(200,'f'),
                    ISBN= obj.Get_Data_Subfield(10,'a'),
                    Year= obj.Get_Data_Subfield(210,'a')
                };
            });
                


            context.Response.Write(JsonConvert.SerializeObject(new { list = tt, count = count }));
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