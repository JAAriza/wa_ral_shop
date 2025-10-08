using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlTypes;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using wa_ral_shop.Models;
using wa_ral_shop.Models.Anonymous.Administracion;

namespace wa_ral_shop.Models.Repositorios.Administracion
{
    public class RepositorioCPaqueteria
    {
        public Int16 Alta(ComentarioPaqueteriaAnonymous comentarioPaqueteriaAnonymous)
        {
            Conexion conexion = new Conexion();
            conexion.AbrirConexion(false);
            Int16 IdCPaqueteria = 0;
            try
            {
                conexion.sqlCommand.CommandType = CommandType.StoredProcedure;
                conexion.sqlCommand.CommandText = "InsertCPaqueteria";
                conexion.sqlCommand.Parameters.Clear();
                conexion.sqlCommand.Parameters.Add(new SqlParameter("@IdPaqueteria", SqlDbType.SmallInt)).Value = comentarioPaqueteriaAnonymous.IdPaqueteria;
                conexion.sqlCommand.Parameters.Add(new SqlParameter("@Comentario", SqlDbType.VarChar)).Value = comentarioPaqueteriaAnonymous.Comentario;
                IdCPaqueteria = Convert.ToInt16(conexion.sqlCommand.ExecuteScalar().ToString());
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
            return IdCPaqueteria;
        }
        public DataTable Buscar(ComentarioPaqueteriaAnonymous comentarioPaqueteriaAnonymous)
        {
            SqlDataReader sqldrCPaqueterias = null;
            DataTable dtCPAqueterias = new DataTable();
            Conexion conexion = new Conexion();
            conexion.AbrirConexion(false);
            try
            {
                conexion.sqlCommand.CommandType = CommandType.StoredProcedure;
                conexion.sqlCommand.CommandText = "SelectCPaqueteria";
                conexion.sqlCommand.Parameters.Clear();
                conexion.sqlCommand.Parameters.Add(new SqlParameter("@IdPaqueteria", SqlDbType.SmallInt)).Value = comentarioPaqueteriaAnonymous.IdPaqueteria == 0 ? SqlInt16.Null : comentarioPaqueteriaAnonymous.IdPaqueteria;
                conexion.sqlCommand.Parameters.Add(new SqlParameter("@Estatus", SqlDbType.Bit)).Value = string.IsNullOrEmpty(comentarioPaqueteriaAnonymous.EstatusSTR) ? SqlBoolean.Null : (comentarioPaqueteriaAnonymous.EstatusSTR == "1" ? true : false);
                sqldrCPaqueterias = conexion.sqlCommand.ExecuteReader();
                if (sqldrCPaqueterias.HasRows)
                {
                    dtCPAqueterias.Load(sqldrCPaqueterias);
                }
            }

            catch (Exception e)
            {
                string error = e.Message;
                throw;
            }
            finally
            {
                if (sqldrCPaqueterias != null)
                {
                    if (!sqldrCPaqueterias.IsClosed)
                    {
                        sqldrCPaqueterias.Close();
                    }
                    sqldrCPaqueterias.Dispose();
                }
                conexion.CerrarConexion();
            }
            return dtCPAqueterias;
        }

        public int Editar(ComentarioPaqueteriaAnonymous comentarioPaqueteriaAnonymous)
        {
            Conexion conexion = new Conexion();
            conexion.AbrirConexion(false);
            int Editado = 0;
            try
            {
                conexion.sqlCommand.CommandType = CommandType.StoredProcedure;
                conexion.sqlCommand.CommandText = "UpdateCPaqueteria";
                conexion.sqlCommand.Parameters.Clear();
                conexion.sqlCommand.Parameters.Add(new SqlParameter("@Id", SqlDbType.SmallInt)).Value = comentarioPaqueteriaAnonymous.Id;
                conexion.sqlCommand.Parameters.Add(new SqlParameter("@IdPaqueteria", SqlDbType.SmallInt)).Value = comentarioPaqueteriaAnonymous.IdPaqueteria;
                conexion.sqlCommand.Parameters.Add(new SqlParameter("@Comentario", SqlDbType.VarChar)).Value = comentarioPaqueteriaAnonymous.Comentario;
                conexion.sqlCommand.Parameters.Add(new SqlParameter("@Estatus", SqlDbType.Bit)).Value = comentarioPaqueteriaAnonymous.Estatus;
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

        public int Eliminar(Int16 Id)
        {
            Conexion conexion = new Conexion();
            conexion.AbrirConexion(false);
            int Eliminado = 0;
            try
            {
                conexion.sqlCommand.CommandType = CommandType.StoredProcedure;
                conexion.sqlCommand.CommandText = "DeleteCPaqueteria";
                conexion.sqlCommand.Parameters.Clear();
                conexion.sqlCommand.Parameters.Add(new SqlParameter("@Id", SqlDbType.SmallInt)).Value = Id;
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

        public DataTable SelectPaqueterias()
        {
            SqlDataReader sqldrPaqueterias = null;
            DataTable dtPaqueterias = new DataTable();
            Conexion conexion = new Conexion();
            conexion.AbrirConexion(false);
            try
            {
                conexion.sqlCommand.CommandType = CommandType.StoredProcedure;
                conexion.sqlCommand.CommandText = "SelectPaqueteriasCombo";
                conexion.sqlCommand.Parameters.Clear();

                sqldrPaqueterias = conexion.sqlCommand.ExecuteReader();
                if (sqldrPaqueterias.HasRows)
                {
                    dtPaqueterias.Load(sqldrPaqueterias);
                }
            }

            catch (Exception e)
            {
                string error = e.Message;
                throw;
            }
            finally
            {
                if (sqldrPaqueterias != null)
                {
                    if (!sqldrPaqueterias.IsClosed)
                    {
                        sqldrPaqueterias.Close();
                    }
                    sqldrPaqueterias.Dispose();
                }
                conexion.CerrarConexion();
            }
            return dtPaqueterias;
        }

    }
}