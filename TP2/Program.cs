using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TP2.DAL;
using TP2.UI;

namespace TP2
{
    class Program
    {
        static void Main(string[] args)
        {
            UserConsoleInterface ui = new UserConsoleInterface();
            ui.Menu();
        }
    }
}
