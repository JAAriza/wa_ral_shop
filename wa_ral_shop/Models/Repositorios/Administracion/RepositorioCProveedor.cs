using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlTypes;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using wa_ral_shop.Models;
using wa_ral_shop.Models.Anonymous.Catalogos;

namespace wa_ral_shop.Models.Repositorios.Administracion
{
    public class RepositorioCProveedor
    {
        public int Alta(CProveedorAnonymous cProveedorAnonymous)
        {
            Conexion conexion = new Conexion();
            conexion.AbrirConexion(false);
            int IdCProveedor = 0;
            try
            {
                conexion.sqlCommand.CommandType = CommandType.StoredProcedure;
                conexion.sqlCommand.CommandText = "InsertCProveedor";
                conexion.sqlCommand.Parameters.Clear();
                conexion.sqlCommand.Parameters.Add(new SqlParameter("@IdProveedor", SqlDbType.Int)).Value = cProveedorAnonymous.IdProveedor;
                conexion.sqlCommand.Parameters.Add(new SqlParameter("@Comentario", SqlDbType.VarChar)).Value = cProveedorAnonymous.Comentario;
                IdCProveedor = int.Parse(conexion.sqlCommand.ExecuteScalar().ToString());
            }

            catch (Exception e)
            {
                throw;
            }
            finally
            {
                conexion.CerrarConexion();
            }
            return IdCProveedor;
        }
        public DataTable Buscar(CProveedorAnonymous cProveedorAnonymous)
        {
            SqlDataReader sqldrCProveedores = null;
            DataTable dtCProveedores = new DataTable();
            Conexion conexion = new Conexion();
            conexion.AbrirConexion(false);
            try
            {
                conexion.sqlCommand.CommandType = CommandType.StoredProcedure;
                conexion.sqlCommand.CommandText = "SelectCProveedor";
                conexion.sqlCommand.Parameters.Clear();
                conexion.sqlCommand.Parameters.Add(new SqlParameter("@IdProveedor", SqlDbType.Int)).Value = cProveedorAnonymous.IdProveedor == 0 ? SqlInt32.Null : cProveedorAnonymous.IdProveedor;
                conexion.sqlCommand.Parameters.Add(new SqlParameter("@Estatus", SqlDbType.Bit)).Value = string.IsNullOrEmpty(cProveedorAnonymous.EstatusSTR) ? SqlBoolean.Null : (cProveedorAnonymous.EstatusSTR == "1" ? true : false);
                sqldrCProveedores = conexion.sqlCommand.ExecuteReader();
                if (sqldrCProveedores.HasRows)
                {
                    dtCProveedores.Load(sqldrCProveedores);
                }
            }

            catch (Exception e)
            {
                throw;
            }
            finally
            {
                if (sqldrCProveedores != null)
                {
                    if (!sqldrCProveedores.IsClosed)
                    {
                        sqldrCProveedores.Close();
                    }
                    sqldrCProveedores.Dispose();
                }
                conexion.CerrarConexion();
            }
            return dtCProveedores;
        }

        public int Editar(CProveedorAnonymous cProveedorAnonymous)
        {
            Conexion conexion = new Conexion();
            conexion.AbrirConexion(false);
            int Editado = 0;
            try
            {
                conexion.sqlCommand.CommandType = CommandType.StoredProcedure;
                conexion.sqlCommand.CommandText = "UpdateCProveedor";
                conexion.sqlCommand.Parameters.Clear();
                conexion.sqlCommand.Parameters.Add(new SqlParameter("@Id", SqlDbType.Int)).Value = cProveedorAnonymous.Id;
                conexion.sqlCommand.Parameters.Add(new SqlParameter("@IdProveedor", SqlDbType.SmallInt)).Value = cProveedorAnonymous.IdProveedor;
                conexion.sqlCommand.Parameters.Add(new SqlParameter("@Comentario", SqlDbType.VarChar)).Value = cProveedorAnonymous.Comentario;
                conexion.sqlCommand.Parameters.Add(new SqlParameter("@Estatus", SqlDbType.Bit)).Value = cProveedorAnonymous.Estatus;
                Editado = int.Parse(conexion.sqlCommand.ExecuteNonQuery().ToString());
            }

            catch (Exception e)
            {
                throw;
            }
            finally
            {
                conexion.CerrarConexion();
            }
            return Editado;
        }

        public int Eliminar(int Id)
        {
            Conexion conexion = new Conexion();
            conexion.AbrirConexion(false);
            int Eliminado = 0;
            try
            {
                conexion.sqlCommand.CommandType = CommandType.StoredProcedure;
                conexion.sqlCommand.CommandText = "DeleteCProveedor";
                conexion.sqlCommand.Parameters.Clear();
                conexion.sqlCommand.Parameters.Add(new SqlParameter("@Id", SqlDbType.Int)).Value = Id;
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

        public DataTable SelectProveedores()
        {
            SqlDataReader sqldrProveedores = null;
            DataTable dtProveedores = new DataTable();
            Conexion conexion = new Conexion();
            conexion.AbrirConexion(false);
            try
            {
                conexion.sqlCommand.CommandType = CommandType.StoredProcedure;
                conexion.sqlCommand.CommandText = "SelectProveedoresCombo";
                conexion.sqlCommand.Parameters.Clear();

                sqldrProveedores = conexion.sqlCommand.ExecuteReader();
                if (sqldrProveedores.HasRows)
                {
                    dtProveedores.Load(sqldrProveedores);
                }
            }

            catch (Exception e)
            {
                throw;
            }
            finally
            {
                if (sqldrProveedores != null)
                {
                    if (!sqldrProveedores.IsClosed)
                    {
                        sqldrProveedores.Close();
                    }
                    sqldrProveedores.Dispose();
                }
                conexion.CerrarConexion();
            }
            return dtProveedores;
        }

    }
}