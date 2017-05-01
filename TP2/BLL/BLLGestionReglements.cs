using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TP2.BO;
using TP2.DAL;

namespace TP2.BLL
{
    class BLLGestionReglements
    {
        private DALGestionReglements dal = null;

        public BLLGestionReglements()
        {

        }

        public DALGestionReglements Dal { get { if (dal == null) dal = new DALGestionReglements();return dal; } }

        public bool AjoutClient(Client proprietaire)
        {
            return Dal.InsertClientQuery(proprietaire.Identite, proprietaire.Nom);
        }

        public bool SupprimerReglementFacture(string reference)
        {
            return Dal.DeleteRegelementsFactureQuery(reference);
        }

        public bool SupprimerFacture(string reference)
        {
            return Dal.DeleteFactureQuery(reference);
        }

        public List<Facture> GetListeFactureClient(int identite)
        {
            List<Facture> Factures = new List<Facture>();

            DataTable MyTable = new DataTable();
            MyTable = Dal.SelectFactureByIdClient(identite);
            foreach (DataRow row in MyTable.Rows)
            {
                Facture fact = new Facture((string)row[0],(DateTime)row[1],(double)row[2],GetClientById((int)row[3]));
                Factures.Add(fact);
            }

            return Factures;
        }

        public List<Facture> GetListeFacture()
        {
            List<Facture> Factures = new List<Facture>();

            DataTable MyTable = new DataTable();
            MyTable = Dal.SelectAllFacture();
            foreach (DataRow row in MyTable.Rows)
            {
                Facture fact = new Facture((string)row[0], (DateTime)row[1], (double)row[2], GetClientById((int)row[3]));
                Factures.Add(fact);
            }
            return Factures;
        }

        public double GetMontantTotalFacturesClient(int identite)
        {
            DataTable MyTable = Dal.SelectClientByIdClient(identite);
            double MontantTotalFactures = 0.0;
            foreach (DataRow MyRow in MyTable.Rows)
            {
                MontantTotalFactures += Convert.ToDouble(MyRow[0]);
            }
            return MontantTotalFactures;
        }

        public Client GetClientById(int identite)
        {
            DataTable MyTable = Dal.SelectClientByIdClient(identite);
            return new Client(Convert.ToInt32(MyTable.Rows[0][0]), (MyTable.Rows[0][1]).ToString());
        }

        public List<Client> GetListClient()
        {
            List<Client> Clients = new List<Client>();
            DataTable MyTable = Dal.SelectAllClient();
            foreach (DataRow MyRow in MyTable.Rows)
            {
                Client client = new Client((int)MyRow[0], MyRow[1].ToString());
                Clients.Add(client);
            }
            return Clients;
        }

    }
}
