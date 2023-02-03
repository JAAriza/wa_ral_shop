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
    public class RepositorioProveedor
    {
        public int Alta(ProveedorAnonymous proveedorAnonymous)
        {
            Conexion conexion = new Conexion();
            conexion.AbrirConexion(false);
            int IdProveedor = 0;
            try
            {
                conexion.sqlCommand.CommandType = CommandType.StoredProcedure;
                conexion.sqlCommand.CommandText = "InsertProveedor";
                conexion.sqlCommand.Parameters.Clear();
                conexion.sqlCommand.Parameters.Add(new SqlParameter("@Nombre", SqlDbType.VarChar)).Value = proveedorAnonymous.Nombre;
                conexion.sqlCommand.Parameters.Add(new SqlParameter("@IdPais", SqlDbType.Int)).Value = proveedorAnonymous.IdPais;
                conexion.sqlCommand.Parameters.Add(new SqlParameter("@Telefono", SqlDbType.VarChar)).Value = proveedorAnonymous.Telefono;
                conexion.sqlCommand.Parameters.Add(new SqlParameter("@Telefono2", SqlDbType.VarChar)).Value = proveedorAnonymous.Telefono2;
                conexion.sqlCommand.Parameters.Add(new SqlParameter("@EMail", SqlDbType.VarChar)).Value = proveedorAnonymous.EMail;
                conexion.sqlCommand.Parameters.Add(new SqlParameter("@EMail2", SqlDbType.VarChar)).Value = proveedorAnonymous.EMail2;
                conexion.sqlCommand.Parameters.Add(new SqlParameter("@Estrellas", SqlDbType.TinyInt)).Value = proveedorAnonymous.Estrellas;
                IdProveedor = Convert.ToInt32(conexion.sqlCommand.ExecuteScalar().ToString());
            }

            catch (Exception e)
            {
                throw;
            }
            finally
            {
                conexion.CerrarConexion();
            }
            return IdProveedor;
        }
        public DataTable Buscar(ProveedorAnonymous proveedorAnonymous)
        {
            SqlDataReader sqldrProveedores = null;
            DataTable dtProveedores = new DataTable();
            Conexion conexion = new Conexion();
            conexion.AbrirConexion(false);
            try
            {
                conexion.sqlCommand.CommandType = CommandType.StoredProcedure;
                conexion.sqlCommand.CommandText = "SelectProveedor";
                conexion.sqlCommand.Parameters.Clear();
                conexion.sqlCommand.Parameters.Add(new SqlParameter("@Nombre", SqlDbType.VarChar)).Value = string.IsNullOrEmpty(proveedorAnonymous.Nombre) ? SqlString.Null : proveedorAnonymous.Nombre;
                conexion.sqlCommand.Parameters.Add(new SqlParameter("@IdPais", SqlDbType.Int)).Value = proveedorAnonymous.IdPais == 0 ? SqlInt32.Null : proveedorAnonymous.IdPais;
                conexion.sqlCommand.Parameters.Add(new SqlParameter("@Estatus", SqlDbType.Bit)).Value = string.IsNullOrEmpty(proveedorAnonymous.EstatusSTR) ? SqlBoolean.Null : (proveedorAnonymous.EstatusSTR == "1" ? true : false);
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

        public int Editar(ProveedorAnonymous proveedorAnonymous)
        {
            Conexion conexion = new Conexion();
            conexion.AbrirConexion(false);
            int Editado = 0;
            try
            {
                conexion.sqlCommand.CommandType = CommandType.StoredProcedure;
                conexion.sqlCommand.CommandText = "UpdateProveedor";
                conexion.sqlCommand.Parameters.Clear();
                conexion.sqlCommand.Parameters.Add(new SqlParameter("@Id", SqlDbType.Int)).Value = proveedorAnonymous.Id;
                conexion.sqlCommand.Parameters.Add(new SqlParameter("@Nombre", SqlDbType.VarChar)).Value = proveedorAnonymous.Nombre;
                conexion.sqlCommand.Parameters.Add(new SqlParameter("@IdPais", SqlDbType.TinyInt)).Value = proveedorAnonymous.IdPais;
                conexion.sqlCommand.Parameters.Add(new SqlParameter("@Telefono", SqlDbType.VarChar)).Value = proveedorAnonymous.Telefono;
                conexion.sqlCommand.Parameters.Add(new SqlParameter("@Telefono2", SqlDbType.VarChar)).Value = proveedorAnonymous.Telefono2;
                conexion.sqlCommand.Parameters.Add(new SqlParameter("@EMail", SqlDbType.VarChar)).Value = proveedorAnonymous.EMail;
                conexion.sqlCommand.Parameters.Add(new SqlParameter("@EMail2", SqlDbType.VarChar)).Value = proveedorAnonymous.EMail2;
                conexion.sqlCommand.Parameters.Add(new SqlParameter("@Estrellas", SqlDbType.TinyInt)).Value = proveedorAnonymous.Estrellas;
                conexion.sqlCommand.Parameters.Add(new SqlParameter("@Estatus", SqlDbType.Bit)).Value = proveedorAnonymous.Estatus;
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
                conexion.sqlCommand.CommandText = "DeleteProveedor";
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
    }
}