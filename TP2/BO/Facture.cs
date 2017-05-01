using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TP2.BO
{
    class Facture
    {
        private Client proprietaire;

        public Facture() { }

        public Facture(string reference, DateTime dateFacture, double montantFacture, Client _proprietaire)
        {
            Reference = reference;
            DateFacture = dateFacture;
            MontantFacture = montantFacture;
            proprietaire = _proprietaire;
        }

        public string Reference { get; set; }
        public DateTime DateFacture { get; set; }
        public double MontantFacture { get; set; }
        public Client Proprietaire { get { return proprietaire; } }

    }
}
