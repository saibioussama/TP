using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TP2.DAL
{
    class DbConnection
    {
        MySqlConnection conn;

        public DbConnection()
        {
            conn = new MySqlConnection(@"Server=127.0.0.1;Database=net;Uid=root;Pwd=root;");
        }

        private MySqlConnection openConnection()
        {
            if (conn.State == System.Data.ConnectionState.Closed || conn.State == System.Data.ConnectionState.Broken)
            {
                conn.Open();
            }

            return conn;
        }

        public bool ExecuteInsertQuery(string query, MySqlParameter[] mysqlParametres)
        {
            MySqlCommand Command = new MySqlCommand();

            try
            {
                Command.Connection = openConnection();
                Command.CommandText = query;
                Command.Parameters.AddRange(mysqlParametres);
                Command.ExecuteNonQuery();
            }
            catch (MySqlException ex)
            {
                Console.WriteLine($"error - Connection.executeInsertQuery - query :{query}\nexception : {ex.ToString()}");
                return false;
            }

            conn.Close();
            return true;
        }

        public bool ExecuteUpdateQuery(string query, MySqlParameter[] mysqlParametres)
        {
            MySqlCommand Command = new MySqlCommand();

            try
            {
                Command.Connection = openConnection();
                Command.CommandText = query;
                Command.Parameters.AddRange(mysqlParametres);
                Command.ExecuteNonQuery();
            }
            catch (MySqlException ex)
            {
                Console.WriteLine($"error - Connection.executeUpdateQuery - query :{query}\nexception : {ex.ToString()}");
                return false;
            }

            conn.Close();
            return true;
        }

        public bool ExecuteDeleteQuery(string query, MySqlParameter[] MysqlParametres)
        {
            MySqlCommand Command = new MySqlCommand();

            try
            {
                Command.Connection = openConnection();
                Command.CommandText = query;
                Command.Parameters.AddRange(MysqlParametres);
                Command.ExecuteNonQuery();
            }
            catch (MySqlException ex)
            {
                Console.WriteLine($"error - Connection.executeDeleteQuery - query :{query}\nexception : {ex.ToString()}");
                return false;
            }

            conn.Close();
            return true;
        }

        public DataTable ExecuteSelectQueryDisconnectMode(string query)
        {
            MySqlDataAdapter MyAdapter = new MySqlDataAdapter();
            MySqlCommand MyCommand = new MySqlCommand();
            DataSet ds = new DataSet();
            DataTable dataTable = null;
            try
            {
                MyCommand.Connection = openConnection();
                MyCommand.CommandText = query;
                MyCommand.ExecuteNonQuery();
                MyAdapter.SelectCommand = MyCommand;
                MyAdapter.Fill(ds);
                dataTable = ds.Tables[0];
            }
            catch (MySqlException ex)
            {
                Console.WriteLine($"error - Connection.executeSelectQueryDisconnectMode - query :{query}\nexception : {ex.StackTrace.ToString()}");
                return null;
            }

            conn.Close();
            return dataTable;
        }

        public DataTable ExecuteSelectQueryConnectMode(string query, MySqlParameter[] MysqlParametres)
        {
            MySqlCommand MyCommand = new MySqlCommand();
            DataTable MyTable = new DataTable();
            MySqlDataReader MyReader = null;
            try
            {
                MyCommand.Connection = openConnection();
                MyCommand.CommandText = query;
                MyCommand.Parameters.Add(MysqlParametres);
                MyReader = MyCommand.ExecuteReader();
                DataTable schemaTable = MyReader.GetSchemaTable();
                List<string> list = new List<string>();
                foreach (DataRow myRow in schemaTable.Rows)
                {
                    DataColumn MyDataColumn = new DataColumn();
                    MyDataColumn.DataType = (Type)myRow["DataType"];
                    MyDataColumn.ColumnName = myRow["ColumnName"].ToString();
                    list.Add(MyDataColumn.ColumnName);
                    MyTable.Columns.Add(MyDataColumn);
                }
                MyTable.BeginLoadData();

                if (MyReader.HasRows)
                {
                    while (MyReader.Read())
                    {
                        DataRow MyDataRow = MyTable.NewRow();
                        for (int i = 0; i < list.Count; i++)
                        {
                            MyDataRow[list[i]] = MyReader[list[i]];
                        }

                        MyTable.Rows.Add(MyDataRow);
                        MyDataRow = null;
                    }
                }   

                schemaTable = null;
            }
            catch (MySqlException ex )
            {
                Console.WriteLine($"Error - Connection.ExecuteSelectQuery - Query {query} \nException : {ex.StackTrace.ToString()}");
                return null;
            }

            MyReader.Close();
            conn.Close();
            return MyTable;
        }
    }
}
