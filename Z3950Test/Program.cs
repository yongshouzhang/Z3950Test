using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Z3950;
using Z3950.Marc;
namespace Z3950Test
{
    class Program
    {
        static void Main(string[] args)
        {
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
            var tt = server.GetRecordsMarc(Bib1Attr.Title, "javascript", 10, 1, out count);
            
            tt.ToList().ForEach(obj =>
            {
                //var str=obj.Get_Data_Subfield(200, 'a');
                Console.WriteLine(obj);
            });

            Console.ReadKey();
        }
    }
}
