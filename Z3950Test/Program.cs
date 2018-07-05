using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Z3950;
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
                DatabaseName = "UCS01U",
                Username = "L410581YHH",
                Password = "20141030"
            };
            int count = 0;
            var tt = server.GetRecords(Bib1Attr.Title, "计算机", 10, 1, out count);
            tt.ToList().ForEach(obj =>
            {
                Console.WriteLine(obj);
            });

            Console.ReadKey();
        }
    }
}
