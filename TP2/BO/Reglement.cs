using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TP2.BO
{
    class Reglement
    {
        private Facture facture;
        public Reglement()
        {

        }

        public Reglement(string idReglement,DateTime dateReglement, double montantReglement,Facture _facture)
        {
            IdReglement = idReglement;
            DateReglement = dateReglement;
            MontantReglement = montantReglement;
            facture = _facture;
        }

        public string IdReglement { get; set; }
        public DateTime DateReglement { get; set; }
        public double MontantReglement { get; set; }
        public Facture MyFacture { get { return facture; } }
    }
}
