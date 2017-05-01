using System;
using System.Data;
using MySql.Data.MySqlClient;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TP2.DAL
{
    public class DALGestionReglements
    {

        private DbConnection Conn;

        public DALGestionReglements()
        {
            Conn = new DbConnection();
        }

        public bool InsertClientQuery(int Identite, string Nom)
        {
            string Query = @"INSERT INTO Client VALUES(@Identite,@Nom)";
            MySqlParameter[] mysqlparameters = new MySqlParameter[2];
            mysqlparameters[0] = new MySqlParameter("@Identite", MySqlDbType.Int32);
            mysqlparameters[1] = new MySqlParameter("@Nom", MySqlDbType.VarChar);
            mysqlparameters[0].Value = Identite;
            mysqlparameters[1].Value = Nom.ToString();
            return Conn.ExecuteInsertQuery(Query,mysqlparameters);
        }

        public bool DeleteRegelementsFactureQuery(string reference)
        {
            string Query = @"DELETE FROM Reglement WHERE reference = @reference";
            MySqlParameter[] mysqlparametres = new MySqlParameter[1];
            mysqlparametres[0] = new MySqlParameter("@reference", MySqlDbType.VarChar);
            mysqlparametres[0].Value = reference.ToString();
            return Conn.ExecuteDeleteQuery(Query,mysqlparametres);
        }

        public bool DeleteFactureQuery(string reference)
        {
            string Query = @"DELETE FROM facture WHERE reference = @reference";
            MySqlParameter[] mysqlparametres = new MySqlParameter[1];
            mysqlparametres[0] = new MySqlParameter("@reference", MySqlDbType.VarChar);
            mysqlparametres[0].Value = reference.ToString();
            return Conn.ExecuteDeleteQuery(Query, mysqlparametres);
        }

        public DataTable SelectFactureByIdClient(int Identite)
        {
            string Query = "select * from facture where identite = @identite";
            MySqlParameter[] mysqlparameters = new MySqlParameter[1];
            mysqlparameters[0] = new MySqlParameter("@identite", MySqlDbType.Int32);
            mysqlparameters[0].Value = Identite;
            return Conn.ExecuteSelectQueryConnectMode(Query,mysqlparameters);
        }

        public DataTable SelectClientByIdClient(int Identite)
        {
            string Query = "select * from client where identite = @identite";
            MySqlParameter[] mysqlparameters = new MySqlParameter[1];
            mysqlparameters[0] = new MySqlParameter("@identite", MySqlDbType.Int32);
            mysqlparameters[0].Value = Identite;
            return Conn.ExecuteSelectQueryConnectMode(Query, mysqlparameters);
        }

        public DataTable SelectAllClient()
        {
            string Query = "select * from Client";
            return Conn.ExecuteSelectQueryDisconnectMode(Query);
        }

        public DataTable SelectAllFacture()
        {
            string Query = "select * from facture";
            return Conn.ExecuteSelectQueryDisconnectMode(Query);
        }


    }
}
