using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data.Interfaces;
namespace Data.Helpers
{
    public class Printer: IPrinter
    {
        public void Print(string message)
        {
            Console.WriteLine(message);
        }
    }
}
