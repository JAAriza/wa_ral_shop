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
    public class RepositorioRutaBase
    {
        public byte Alta(RutaBaseAnonymous RutaBaseAnonymous)
        {
            Conexion conexion = new Conexion();
            conexion.AbrirConexion(false);
            byte IdRutaBase = 0;
            try
            {
                conexion.sqlCommand.CommandType = CommandType.StoredProcedure;
                conexion.sqlCommand.CommandText = "InsertRutaBase";
                conexion.sqlCommand.Parameters.Clear();
                conexion.sqlCommand.Parameters.Add(new SqlParameter("@RutaBase", SqlDbType.VarChar)).Value = RutaBaseAnonymous.RutaBase;
                IdRutaBase = Convert.ToByte(conexion.sqlCommand.ExecuteScalar().ToString());
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
            return IdRutaBase;
        }
        public DataTable Buscar(RutaBaseAnonymous RutaBaseAnonymous)
        {
            SqlDataReader sqldrRutasBase = null;
            DataTable dtRutasBase = new DataTable();
            Conexion conexion = new Conexion();
            conexion.AbrirConexion(false);
            try
            {
                conexion.sqlCommand.CommandType = CommandType.StoredProcedure;
                conexion.sqlCommand.CommandText = "SelectRutaBase";
                conexion.sqlCommand.Parameters.Clear();
                conexion.sqlCommand.Parameters.Add(new SqlParameter("@RutaBase", SqlDbType.VarChar)).Value = string.IsNullOrEmpty(RutaBaseAnonymous.RutaBase) ? SqlString.Null : RutaBaseAnonymous.RutaBase;
                conexion.sqlCommand.Parameters.Add(new SqlParameter("@Estatus", SqlDbType.Bit)).Value = string.IsNullOrEmpty(RutaBaseAnonymous.EstatusSTR) ? SqlBoolean.Null : (RutaBaseAnonymous.EstatusSTR == "1" ? true : false);
                sqldrRutasBase = conexion.sqlCommand.ExecuteReader();
                if (sqldrRutasBase.HasRows)
                {
                    dtRutasBase.Load(sqldrRutasBase);
                }
            }

            catch (Exception e)
            {
                string error = e.Message;
                throw;
            }
            finally
            {
                if (sqldrRutasBase != null)
                {
                    if (!sqldrRutasBase.IsClosed)
                    {
                        sqldrRutasBase.Close();
                    }
                    sqldrRutasBase.Dispose();
                }
                conexion.CerrarConexion();
            }
            return dtRutasBase;
        }

        public int Editar(RutaBaseAnonymous RutaBaseAnonymous)
        {
            Conexion conexion = new Conexion();
            conexion.AbrirConexion(false);
            int Editado = 0;
            try
            {
                conexion.sqlCommand.CommandType = CommandType.StoredProcedure;
                conexion.sqlCommand.CommandText = "UpdateRutaBase";
                conexion.sqlCommand.Parameters.Clear();
                conexion.sqlCommand.Parameters.Add(new SqlParameter("@Id", SqlDbType.TinyInt)).Value = RutaBaseAnonymous.Id;
                conexion.sqlCommand.Parameters.Add(new SqlParameter("@RutaBase", SqlDbType.VarChar)).Value = RutaBaseAnonymous.RutaBase;
                conexion.sqlCommand.Parameters.Add(new SqlParameter("@Estatus", SqlDbType.Bit)).Value = RutaBaseAnonymous.Estatus;
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
                conexion.sqlCommand.CommandText = "DeleteRutaBase";
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