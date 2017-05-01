using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TP2.BLL;
using TP2.BO;

namespace TP2.UI
{
    class UserConsoleInterface
    {
        private BLLGestionReglements bll = null;

        public UserConsoleInterface() { }

        public BLLGestionReglements Bll
        {
            get
            {
                if (bll == null)
                    bll = new BLLGestionReglements();
                return bll;
            }
        }

        public Client GetClientFromInterface()
        {
            Client proprietaire = new Client();

            Console.WriteLine("\n***** saisie d'une client ******");
            Console.Write("Identité :");
            proprietaire.Identite = Convert.ToInt32(Console.ReadLine());
            Console.Write("Nom :");
            proprietaire.Nom = Console.ReadLine();

            return proprietaire;
        }

        public void ShowFacture(Facture myFacture)
        {
            Console.WriteLine("reference : " + myFacture.Reference);
            Console.WriteLine("Date : " + myFacture.DateFacture);
            Console.WriteLine("Montant : " + myFacture.MontantFacture);
            Console.WriteLine("Client : " + myFacture.Proprietaire.Nom);
        }

        private int ChoixAction()
        {
            Console.WriteLine("*************Menu*************");
            Console.WriteLine("1 : Ajouter in nouveau client");
            Console.WriteLine("2 : Supprimer les reglements d'une facture");
            Console.WriteLine("3 : Supprimer une facture");
            Console.WriteLine("4 : Afficher le montant total des factures d'un client");
            Console.WriteLine("5 : Afficher touts les factures");
            Console.WriteLine("6 : Quitter");
            Console.WriteLine("*******************************");
            Console.WriteLine("donner votre choix : ");
            return Convert.ToInt32(Console.ReadLine());
        }

        public void Menu()
        {
            int choix;
            do
            {
                choix = ChoixAction();
                switch (choix)
                {
                    case 1:
                        {
                            Console.Clear();
                            Console.WriteLine("*********** Ajout un nouveau client **********");
                            if (bll.AjoutClient(GetClientFromInterface()))
                                Console.WriteLine("le client a été ajouté avec succés");
                            break;
                        }
                    case 2:
                        {
                            Console.Clear();
                            Console.WriteLine("*********** Suppression des reglements d'une facture ********");
                            Console.Write("Liste des factures :");
                            List<Facture> Factures = Bll.GetListeFacture();
                            foreach (var item in Factures)
                            {
                                Console.WriteLine(item.Reference + " ");
                            }
                            Console.WriteLine();
                            Console.Write("donner la reference du facture :");
                            if (Bll.SupprimerReglementFacture(Console.ReadLine()))
                                Console.WriteLine("les reglements ont été supprimé avec succés");
                            break;
                        }
                    case 3:
                        {
                            Console.Clear();
                            Console.WriteLine("********** Suppression d'une facture ********");
                            Console.Write("Liste des factures : ");
                            List<Facture> Factures = Bll.GetListeFacture();
                            foreach (var item in Factures)
                            {
                                Console.WriteLine(item.Reference + " ");
                            }
                            Console.WriteLine();
                            Console.Write("donner la reference du facture : ");
                            if (Bll.SupprimerFacture(Console.ReadLine()))
                                Console.WriteLine("la facture a été supprimé avec succés");
                            break;
                        }

                    case 4:
                        {
                            Console.Clear();
                            Console.WriteLine("********* Affichage du montant total des factures *************");
                            Console.Write("Liste des clients :");
                            List<Client> Clients = Bll.GetListClient();
                            foreach (var item in Clients)
                            {
                                Console.Write(item.Identite + " ");
                            }
                            Console.WriteLine();
                            Console.Write("donner l'identite d'un client ");
                            Console.WriteLine("Montant  total  = " + Bll.GetMontantTotalFacturesClient(Convert.ToInt32(Console.ReadLine())));
                            break;
                        }

                    case 5:
                        {
                            Console.Clear();
                            Console.WriteLine("*********** Affichage des factures **************");
                            List<Facture> Factures = Bll.GetListeFacture();
                            foreach (var item in Factures)
                            {
                                Console.WriteLine("------------------------------------------------");
                                ShowFacture(item);
                            }
                            Console.ReadKey();
                            break;
                        }



                }
            } while (choix != 6);
        }

    }
}
