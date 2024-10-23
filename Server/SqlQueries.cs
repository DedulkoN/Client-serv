using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Configuration;
using System.Data.SqlClient;


namespace Server
{

    class SqlQueriesRun
    {

        private SqlConnectionStringBuilder SqlBuilderConnect = new SqlConnectionStringBuilder();


        public SqlQueriesRun()
        {
            SqlBuilderConnect.ConnectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
        }

        public bool RunEditQuery(string query)
        {
            SqlConnection testConnection = new SqlConnection(SqlBuilderConnect.ToString());
            SqlCommand testCommand = testConnection.CreateCommand();

            try
            {
                testConnection.Open();
                testCommand.CommandText = string.Format("set language \'русский\'");
                testCommand.ExecuteNonQuery();
                testCommand.CommandText = query;
                testCommand.CommandTimeout = 500;
                testCommand.ExecuteNonQuery();
                testConnection.Close();
                return true;
            }
            catch (SqlException ex)
            {

                Console.WriteLine(ex.Message);
                return false;
            }
        }

        public DataTable RunSelectQuery(string query)
        {

            SqlConnection testConnection = new SqlConnection(SqlBuilderConnect.ToString());
            SqlCommand testCommand = testConnection.CreateCommand();
            DataTable ResultTable = new DataTable();

            try
            {
                testConnection.Open();
                testCommand.CommandText = string.Format("set language \'русский\'");
                testCommand.ExecuteNonQuery();
                testCommand.CommandText = query;
                testCommand.CommandTimeout = 500;
                SqlDataAdapter dataAdapter = new SqlDataAdapter(testCommand);
                dataAdapter.Fill(ResultTable);
                testConnection.Close();
                return ResultTable;
            }
            catch (SqlException ex)
            {

                Console.WriteLine(ex.Message);
                testConnection.Close();
                return ResultTable;
            }

        }

        public SqlDataReader RunReaderQuery(string query)
        {
            SqlDataReader reader = null;
            SqlConnection testConnection = new SqlConnection(SqlBuilderConnect.ToString());
            SqlCommand testCommand = testConnection.CreateCommand();
            try
            {
                testConnection.Open();
                testCommand.CommandText = string.Format("set language \'русский\'");
                testCommand.ExecuteNonQuery();
                testCommand.CommandText = query;
                testCommand.CommandTimeout = 500;
                reader = testCommand.ExecuteReader();
                return reader;
            }
            catch (SqlException ex)
            {
                Console.WriteLine(ex.Message);
                return reader;
            }


        }


        public Object RunCalcQuery(string query)
        {

            SqlConnection testConnection = new SqlConnection(SqlBuilderConnect.ToString());
            SqlCommand testCommand = testConnection.CreateCommand();
            Object ResultStr = DBNull.Value;

            try
            {
                testConnection.Open();
                testCommand.CommandText = string.Format("set language \'русский\'");
                testCommand.ExecuteNonQuery();
                testCommand.CommandText = query;
                testCommand.CommandTimeout = 500;
                ResultStr = testCommand.ExecuteScalar();
                testConnection.Close();
                return ResultStr;
            }
            catch (SqlException ex)
            {
               Console.WriteLine(ex.Message);
                testConnection.Close();
                return DBNull.Value;
            }

        }



    }
}
