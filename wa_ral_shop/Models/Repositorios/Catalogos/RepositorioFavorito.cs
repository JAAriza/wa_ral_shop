using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlTypes;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using wa_ral_shop.Models;
using wa_ral_shop.Models.Anonymous.Catalogos;

namespace wa_ral_shop.Models.Repositorios.Catalogos
{
    public class RepositorioFavorito
    {
        public int Alta(FavoritoAnonymous favoritoAnonymous)
        {
            Conexion conexion = new Conexion();
            conexion.AbrirConexion(false);
            int IdFavorito = 0;
            try
            {
                conexion.sqlCommand.CommandType = CommandType.StoredProcedure;
                conexion.sqlCommand.CommandText = "InsertFavorito";
                conexion.sqlCommand.Parameters.Clear();
                conexion.sqlCommand.Parameters.Add(new SqlParameter("@IdProducto", SqlDbType.Int)).Value = favoritoAnonymous.IdProducto;
                conexion.sqlCommand.Parameters.Add(new SqlParameter("@IdCliente", SqlDbType.Int)).Value = favoritoAnonymous.IdCliente;
                IdFavorito = Convert.ToInt32(conexion.sqlCommand.ExecuteScalar().ToString());
            }

            catch (Exception e)
            {
                throw;
            }
            finally
            {
                conexion.CerrarConexion();
            }
            return IdFavorito;
        }
        public DataTable Buscar(int IdCliente)
        {
            SqlDataReader sqldrFavoritos = null;
            DataTable dtFavoritos = new DataTable();
            Conexion conexion = new Conexion();
            conexion.AbrirConexion(false);
            try
            {
                conexion.sqlCommand.CommandType = CommandType.StoredProcedure;
                conexion.sqlCommand.CommandText = "SelectFavorito";
                conexion.sqlCommand.Parameters.Clear();
                conexion.sqlCommand.Parameters.Add(new SqlParameter("@IdCliente", SqlDbType.Int)).Value = IdCliente;

                sqldrFavoritos = conexion.sqlCommand.ExecuteReader();
                if (sqldrFavoritos.HasRows)
                {
                    dtFavoritos.Load(sqldrFavoritos);
                }
            }

            catch (Exception e)
            {
                throw;
            }
            finally
            {
                if (sqldrFavoritos != null)
                {
                    if (!sqldrFavoritos.IsClosed)
                    {
                        sqldrFavoritos.Close();
                    }
                    sqldrFavoritos.Dispose();
                }
                conexion.CerrarConexion();
            }
            return dtFavoritos;
        }

        public int Eliminar(int IdCliente, int IdProducto)
        {
            Conexion conexion = new Conexion();
            conexion.AbrirConexion(false);
            int Eliminado = 0;
            try
            {
                conexion.sqlCommand.CommandType = CommandType.StoredProcedure;
                conexion.sqlCommand.CommandText = "DeleteFavorito";
                conexion.sqlCommand.Parameters.Clear();
                conexion.sqlCommand.Parameters.Add(new SqlParameter("@IdCliente", SqlDbType.Int)).Value = IdCliente;
                conexion.sqlCommand.Parameters.Add(new SqlParameter("@IdProducto", SqlDbType.Int)).Value = IdProducto;
                Eliminado = int.Parse(conexion.sqlCommand.ExecuteNonQuery().ToString());
            }

            catch (Exception e)
            {
                throw;
            }
            finally
            {
                conexion.CerrarConexion();
            }
            return Eliminado;
        }
    }
}