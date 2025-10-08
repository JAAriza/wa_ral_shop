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
    public class RepositorioAlmacen
    {
        public byte Alta(AlmacenAnonymous AlmacenAnonymous)
        {
            Conexion conexion = new Conexion();
            conexion.AbrirConexion(false);
            byte IdAlmacen = 0;
            try
            {
                conexion.sqlCommand.CommandType = CommandType.StoredProcedure;
                conexion.sqlCommand.CommandText = "InsertAlmacen";
                conexion.sqlCommand.Parameters.Clear();
                conexion.sqlCommand.Parameters.Add(new SqlParameter("@Almacen", SqlDbType.VarChar)).Value = AlmacenAnonymous.Almacen;
                IdAlmacen = Convert.ToByte(conexion.sqlCommand.ExecuteScalar().ToString());
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
            return IdAlmacen;
        }
        public DataTable Buscar(AlmacenAnonymous AlmacenAnonymous)
        {
            SqlDataReader sqldrAlmacenes = null;
            DataTable dtAlmacenes = new DataTable();
            Conexion conexion = new Conexion();
            conexion.AbrirConexion(false);
            try
            {
                conexion.sqlCommand.CommandType = CommandType.StoredProcedure;
                conexion.sqlCommand.CommandText = "SelectAlmacen";
                conexion.sqlCommand.Parameters.Clear();
                conexion.sqlCommand.Parameters.Add(new SqlParameter("@Almacen", SqlDbType.VarChar)).Value = string.IsNullOrEmpty(AlmacenAnonymous.Almacen) ? SqlString.Null : AlmacenAnonymous.Almacen;
                conexion.sqlCommand.Parameters.Add(new SqlParameter("@Estatus", SqlDbType.Bit)).Value = string.IsNullOrEmpty(AlmacenAnonymous.EstatusSTR) ? SqlBoolean.Null : (AlmacenAnonymous.EstatusSTR == "1" ? true : false);
                sqldrAlmacenes = conexion.sqlCommand.ExecuteReader();
                if (sqldrAlmacenes.HasRows)
                {
                    dtAlmacenes.Load(sqldrAlmacenes);
                }
            }

            catch (Exception e)
            {
                string error = e.Message;
                throw;
            }
            finally
            {
                if (sqldrAlmacenes != null)
                {
                    if (!sqldrAlmacenes.IsClosed)
                    {
                        sqldrAlmacenes.Close();
                    }
                    sqldrAlmacenes.Dispose();
                }
                conexion.CerrarConexion();
            }
            return dtAlmacenes;
        }

        public int Editar(AlmacenAnonymous AlmacenAnonymous)
        {
            Conexion conexion = new Conexion();
            conexion.AbrirConexion(false);
            int Editado = 0;
            try
            {
                conexion.sqlCommand.CommandType = CommandType.StoredProcedure;
                conexion.sqlCommand.CommandText = "UpdateAlmacen";
                conexion.sqlCommand.Parameters.Clear();
                conexion.sqlCommand.Parameters.Add(new SqlParameter("@Id", SqlDbType.TinyInt)).Value = AlmacenAnonymous.Id;
                conexion.sqlCommand.Parameters.Add(new SqlParameter("@Almacen", SqlDbType.VarChar)).Value = AlmacenAnonymous.Almacen;
                conexion.sqlCommand.Parameters.Add(new SqlParameter("@Estatus", SqlDbType.Bit)).Value = AlmacenAnonymous.Estatus;
                Editado = int.Parse(conexion.sqlCommand.ExecuteNonQuery().ToString());
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
            return Editado;
        }

        public int Eliminar(byte Id)
        {
            Conexion conexion = new Conexion();
            conexion.AbrirConexion(false);
            int Eliminado = 0;
            try
            {
                conexion.sqlCommand.CommandType = CommandType.StoredProcedure;
                conexion.sqlCommand.CommandText = "DeleteAlmacen";
                conexion.sqlCommand.Parameters.Clear();
                conexion.sqlCommand.Parameters.Add(new SqlParameter("@Id", SqlDbType.TinyInt)).Value = Id;
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