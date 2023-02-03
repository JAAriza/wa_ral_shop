using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace wa_ral_shop.Models.Repositorios.Administracion
{
    public class RepositorioPais
    {
        public DataTable BuscarPaises()
        {
            SqlDataReader sqldrPaises = null;
            DataTable dtPaises = new DataTable();
            Conexion conexion = new Conexion();
            conexion.AbrirConexion(false);
            try
            {
                conexion.sqlCommand.CommandType = CommandType.StoredProcedure;
                conexion.sqlCommand.CommandText = "SelectPaises";
                conexion.sqlCommand.Parameters.Clear();
                sqldrPaises = conexion.sqlCommand.ExecuteReader();
                if (sqldrPaises.HasRows)
                {
                    dtPaises.Load(sqldrPaises);
                }
            }

            catch (Exception e)
            {
                throw;
            }
            finally
            {
                if (sqldrPaises != null)
                {
                    if (!sqldrPaises.IsClosed)
                    {
                        sqldrPaises.Close();
                    }
                    sqldrPaises.Dispose();
                }
                conexion.CerrarConexion();
            }
            return dtPaises;
        }
    }
}