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
    public class RepositorioCarrito
    {
        public int Alta(CarritoAnonymous carritoAnonymous)
        {
            Conexion conexion = new Conexion();
            conexion.AbrirConexion(false);
            int IdCarrito = 0;
            try
            {
                conexion.sqlCommand.CommandType = CommandType.StoredProcedure;
                conexion.sqlCommand.CommandText = "InsertCarrito";
                conexion.sqlCommand.Parameters.Clear();
                conexion.sqlCommand.Parameters.Add(new SqlParameter("@IdProducto", SqlDbType.Int)).Value = carritoAnonymous.IdProducto;
                conexion.sqlCommand.Parameters.Add(new SqlParameter("@IdCliente", SqlDbType.Int)).Value = carritoAnonymous.IdCliente;
                conexion.sqlCommand.Parameters.Add(new SqlParameter("@Cantidad", SqlDbType.Int)).Value = carritoAnonymous.Cantidad;
                IdCarrito = Convert.ToInt32(conexion.sqlCommand.ExecuteScalar().ToString());
            }

            catch (Exception e)
            {
                string error = e.Message;
                throw;
            }
            finally
            {
                conexion.CerrarConexion();
            }
            return IdCarrito;
        }

        public int AltaC(CarritoAnonymous carritoAnonymous)
        {
            Conexion conexion = new Conexion();
            conexion.AbrirConexion(false);
            int IdCarrito = 0;
            try
            {
                conexion.sqlCommand.CommandType = CommandType.StoredProcedure;
                conexion.sqlCommand.CommandText = "InsertCarritoC";
                conexion.sqlCommand.Parameters.Clear();
                conexion.sqlCommand.Parameters.Add(new SqlParameter("@IdProducto", SqlDbType.Int)).Value = carritoAnonymous.IdProducto;
                conexion.sqlCommand.Parameters.Add(new SqlParameter("@IdCliente", SqlDbType.Int)).Value = carritoAnonymous.IdCliente;
                conexion.sqlCommand.Parameters.Add(new SqlParameter("@Cantidad", SqlDbType.Int)).Value = carritoAnonymous.Cantidad;
                IdCarrito = Convert.ToInt32(conexion.sqlCommand.ExecuteScalar().ToString());
            }

            catch (Exception e)
            {
                string error = e.Message;
                throw;
            }
            finally
            {
                conexion.CerrarConexion();
            }
            return IdCarrito;
        }

        public DataTable Buscar(int IdCliente)
        {
            SqlDataReader sqldrCarritos = null;
            DataTable dtCarrito = new DataTable();
            Conexion conexion = new Conexion();
            conexion.AbrirConexion(false);
            try
            {
                conexion.sqlCommand.CommandType = CommandType.StoredProcedure;
                conexion.sqlCommand.CommandText = "SelectCarrito";
                conexion.sqlCommand.Parameters.Clear();
                conexion.sqlCommand.Parameters.Add(new SqlParameter("@IdCliente", SqlDbType.Int)).Value = IdCliente;

                sqldrCarritos = conexion.sqlCommand.ExecuteReader();
                if (sqldrCarritos.HasRows)
                {
                    dtCarrito.Load(sqldrCarritos);
                }
            }

            catch (Exception e)
            {
                string error = e.Message;
                throw;
            }
            finally
            {
                if (sqldrCarritos != null)
                {
                    if (!sqldrCarritos.IsClosed)
                    {
                        sqldrCarritos.Close();
                    }
                    sqldrCarritos.Dispose();
                }
                conexion.CerrarConexion();
            }
            return dtCarrito;
        }

        public int Eliminar(int IdCliente, int IdProducto)
        {
            Conexion conexion = new Conexion();
            conexion.AbrirConexion(false);
            int Eliminado = 0;
            try
            {
                conexion.sqlCommand.CommandType = CommandType.StoredProcedure;
                conexion.sqlCommand.CommandText = "DeleteCarrito";
                conexion.sqlCommand.Parameters.Clear();
                conexion.sqlCommand.Parameters.Add(new SqlParameter("@IdCliente", SqlDbType.Int)).Value = IdCliente;
                conexion.sqlCommand.Parameters.Add(new SqlParameter("@IdProducto", SqlDbType.Int)).Value = IdProducto;
                Eliminado = int.Parse(conexion.sqlCommand.ExecuteNonQuery().ToString());
            }

            catch (Exception e)
            {
                string error = e.Message;
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