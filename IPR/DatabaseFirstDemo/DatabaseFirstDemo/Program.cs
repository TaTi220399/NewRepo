using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseFirstDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var db = new DatabaseFirstDemoDBEntities())
            {


                var query = from b in db.Products
                            orderby b.Id
                            select b;

                Console.WriteLine("All products in the database:");

                foreach (var item in query)
                {
                    Console.WriteLine(item.Title + " " + item.Price);
                }

                Console.WriteLine("Press any key to exit...");
                Console.ReadKey();
            }
        }
    }
}
