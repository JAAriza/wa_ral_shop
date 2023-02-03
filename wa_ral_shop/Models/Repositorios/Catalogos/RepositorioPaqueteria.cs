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
    public class RepositorioPaqueteria
    {
        public Int16 Alta(PaqueteriaAnonymous PaqueteriaAnonymous)
        {
            Conexion conexion = new Conexion();
            conexion.AbrirConexion(false);
            Int16 IdPaqueteria = 0;
            try
            {
                conexion.sqlCommand.CommandType = CommandType.StoredProcedure;
                conexion.sqlCommand.CommandText = "InsertPaqueteria";
                conexion.sqlCommand.Parameters.Clear();
                conexion.sqlCommand.Parameters.Add(new SqlParameter("@Nombre", SqlDbType.VarChar)).Value = PaqueteriaAnonymous.Nombre;
                conexion.sqlCommand.Parameters.Add(new SqlParameter("@Estrellas", SqlDbType.VarChar)).Value = PaqueteriaAnonymous.Estrellas;
                IdPaqueteria = Convert.ToInt16(conexion.sqlCommand.ExecuteScalar().ToString());
            }

            catch (Exception e)
            {
                throw;
            }
            finally
            {
                conexion.CerrarConexion();
            }
            return IdPaqueteria;
        }
        public DataTable Buscar(PaqueteriaAnonymous PaqueteriaAnonymous)
        {
            SqlDataReader sqldrPaqueterias = null;
            DataTable dtPaqueterias = new DataTable();
            Conexion conexion = new Conexion();
            conexion.AbrirConexion(false);
            try
            {
                conexion.sqlCommand.CommandType = CommandType.StoredProcedure;
                conexion.sqlCommand.CommandText = "SelectPaqueteria";
                conexion.sqlCommand.Parameters.Clear();
                conexion.sqlCommand.Parameters.Add(new SqlParameter("@Nombre", SqlDbType.VarChar)).Value = string.IsNullOrEmpty(PaqueteriaAnonymous.Nombre) ? SqlString.Null : PaqueteriaAnonymous.Nombre;
                //conexion.sqlCommand.Parameters.Add(new SqlParameter("@Estrellas", SqlDbType.VarChar)).Value = string.IsNullOrEmpty(PaqueteriaAnonymous.Estrellas) ? SqlString.Null : PaqueteriaAnonymous.Estrellas;
                conexion.sqlCommand.Parameters.Add(new SqlParameter("@Estatus", SqlDbType.Bit)).Value = string.IsNullOrEmpty(PaqueteriaAnonymous.EstatusSTR) ? SqlBoolean.Null : (PaqueteriaAnonymous.EstatusSTR == "1" ? true : false);
                sqldrPaqueterias = conexion.sqlCommand.ExecuteReader();
                if (sqldrPaqueterias.HasRows)
                {
                    dtPaqueterias.Load(sqldrPaqueterias);
                }
            }

            catch (Exception e)
            {
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

        public int Editar(PaqueteriaAnonymous PaqueteriaAnonymous)
        {
            Conexion conexion = new Conexion();
            conexion.AbrirConexion(false);
            int Editado = 0;
            try
            {
                conexion.sqlCommand.CommandType = CommandType.StoredProcedure;
                conexion.sqlCommand.CommandText = "UpdatePaqueteria";
                conexion.sqlCommand.Parameters.Clear();
                conexion.sqlCommand.Parameters.Add(new SqlParameter("@Id", SqlDbType.SmallInt)).Value = PaqueteriaAnonymous.Id;
                conexion.sqlCommand.Parameters.Add(new SqlParameter("@Nombre", SqlDbType.VarChar)).Value = PaqueteriaAnonymous.Nombre;
                conexion.sqlCommand.Parameters.Add(new SqlParameter("@Estrellas", SqlDbType.VarChar)).Value = PaqueteriaAnonymous.Estrellas;
                conexion.sqlCommand.Parameters.Add(new SqlParameter("@Estatus", SqlDbType.Bit)).Value = PaqueteriaAnonymous.Estatus;
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

        public int Eliminar(Int16 Id)
        {
            Conexion conexion = new Conexion();
            conexion.AbrirConexion(false);
            int Eliminado = 0;
            try
            {
                conexion.sqlCommand.CommandType = CommandType.StoredProcedure;
                conexion.sqlCommand.CommandText = "DeletePaqueteria";
                conexion.sqlCommand.Parameters.Clear();
                conexion.sqlCommand.Parameters.Add(new SqlParameter("@Id", SqlDbType.SmallInt)).Value = Id;
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