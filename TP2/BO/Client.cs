using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TP2.BO
{
    class Client
    {
        public int Identite { get; set; }
        public string Nom { get; set; }

        public Client() {}

        public Client(int identite,string nom)
        {
            Identite = identite;
            Nom = nom;
        }
    }
}
