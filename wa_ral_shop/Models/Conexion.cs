using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;


namespace wa_ral_shop.Models
{
    public class Conexion
    {
        private SqlConnection sqlConnection;

        public SqlCommand sqlCommand { get; set; }
        public SqlTransaction sqlTransaction;
        public Conexion()
        {
            try
            {
                sqlConnection = new SqlConnection();
                sqlConnection.ConnectionString = ConfigurationManager.ConnectionStrings["RAL_SHOP"].ConnectionString;
            }
            catch (Exception e)
            {
                throw new ArgumentException(e.Message);
            }
        }
        public void AbrirConexion(Boolean transaccion)
        {
            if (sqlConnection.State == ConnectionState.Closed)
            {
                try
                {
                    sqlConnection.Open();
                    sqlCommand = sqlConnection.CreateCommand();
                    sqlCommand.CommandTimeout = 0;
                    if (transaccion)
                    {
                        sqlTransaction = sqlConnection.BeginTransaction();
                        sqlCommand.Transaction = sqlTransaction;
                    }
                }
                catch (Exception)
                {
                    throw;
                }
            }
        }

        public void CerrarConexion()
        {
            try
            {
                if (sqlConnection.State == ConnectionState.Open)
                {
                    sqlConnection.Close();
                    sqlConnection.Dispose();
                    if (sqlCommand != null)
                    {
                        sqlCommand.Dispose();
                    }
                    if (sqlTransaction != null)
                    {
                        sqlTransaction.Dispose();
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}