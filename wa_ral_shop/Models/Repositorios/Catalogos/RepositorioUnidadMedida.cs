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
    public class RepositorioUnidadMedida
    {
        public byte Alta(UnidadMedidaAnonymous UnidadMedidaAnonymous)
        {
            Conexion conexion = new Conexion();
            conexion.AbrirConexion(false);
            byte IdUnidadMedida = 0;
            try
            {
                conexion.sqlCommand.CommandType = CommandType.StoredProcedure;
                conexion.sqlCommand.CommandText = "InsertUnidadDeMedida";
                conexion.sqlCommand.Parameters.Clear();
                conexion.sqlCommand.Parameters.Add(new SqlParameter("@Nombre", SqlDbType.VarChar)).Value = UnidadMedidaAnonymous.Nombre;
                IdUnidadMedida = Convert.ToByte(conexion.sqlCommand.ExecuteScalar().ToString());
            }

            catch (Exception e)
            {
                throw;
            }
            finally
            {
                conexion.CerrarConexion();
            }
            return IdUnidadMedida;
        }
        public DataTable Buscar(UnidadMedidaAnonymous UnidadMedidaAnonymous)
        {
            SqlDataReader sqldrUnidadesMedida = null;
            DataTable dtUnidadesMedida = new DataTable();
            Conexion conexion = new Conexion();
            conexion.AbrirConexion(false);
            try
            {
                conexion.sqlCommand.CommandType = CommandType.StoredProcedure;
                conexion.sqlCommand.CommandText = "SelectUnidadMedida";
                conexion.sqlCommand.Parameters.Clear();
                conexion.sqlCommand.Parameters.Add(new SqlParameter("@Nombre", SqlDbType.VarChar)).Value = string.IsNullOrEmpty(UnidadMedidaAnonymous.Nombre) ? SqlString.Null : UnidadMedidaAnonymous.Nombre;
                conexion.sqlCommand.Parameters.Add(new SqlParameter("@Estatus", SqlDbType.Bit)).Value = string.IsNullOrEmpty(UnidadMedidaAnonymous.EstatusSTR) ? SqlBoolean.Null : (UnidadMedidaAnonymous.EstatusSTR == "1" ? true : false);
                sqldrUnidadesMedida = conexion.sqlCommand.ExecuteReader();
                if (sqldrUnidadesMedida.HasRows)
                {
                    dtUnidadesMedida.Load(sqldrUnidadesMedida);
                }
            }

            catch (Exception e)
            {
                throw;
            }
            finally
            {
                if (sqldrUnidadesMedida != null)
                {
                    if (!sqldrUnidadesMedida.IsClosed)
                    {
                        sqldrUnidadesMedida.Close();
                    }
                    sqldrUnidadesMedida.Dispose();
                }
                conexion.CerrarConexion();
            }
            return dtUnidadesMedida;
        }

        public int Editar(UnidadMedidaAnonymous UnidadMedidaAnonymous)
        {
            Conexion conexion = new Conexion();
            conexion.AbrirConexion(false);
            int Editado = 0;
            try
            {
                conexion.sqlCommand.CommandType = CommandType.StoredProcedure;
                conexion.sqlCommand.CommandText = "UpdateUnidadMedida";
                conexion.sqlCommand.Parameters.Clear();
                conexion.sqlCommand.Parameters.Add(new SqlParameter("@Id", SqlDbType.TinyInt)).Value = UnidadMedidaAnonymous.Id;
                conexion.sqlCommand.Parameters.Add(new SqlParameter("@Nombre", SqlDbType.VarChar)).Value = UnidadMedidaAnonymous.Nombre;
                conexion.sqlCommand.Parameters.Add(new SqlParameter("@Estatus", SqlDbType.Bit)).Value = UnidadMedidaAnonymous.Estatus;
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

        public int Eliminar(byte Id)
        {
            Conexion conexion = new Conexion();
            conexion.AbrirConexion(false);
            int Eliminado = 0;
            try
            {
                conexion.sqlCommand.CommandType = CommandType.StoredProcedure;
                conexion.sqlCommand.CommandText = "DeleteUnidadDeMedida";
                conexion.sqlCommand.Parameters.Clear();
                conexion.sqlCommand.Parameters.Add(new SqlParameter("@Id", SqlDbType.TinyInt)).Value = Id;
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