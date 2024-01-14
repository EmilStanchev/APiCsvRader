using ApiDatabaseServices.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiDatabaseServices.Helpers
{
    public class ApiPrinter:IApiPrinter
    {
        public void Print(string message) 
        {
            Console.WriteLine(message);
        }
    }
}
