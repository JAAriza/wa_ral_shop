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
    public class RepositorioPorcentaje
    {
        public int Alta(PorcentajeAnonymous PorcentajeAnonymous)
        {
            Conexion conexion = new Conexion();
            conexion.AbrirConexion(false);
            int IdPorcentaje = 0;
            try
            {
                conexion.sqlCommand.CommandType = CommandType.StoredProcedure;
                conexion.sqlCommand.CommandText = "InsertPorcentaje";
                conexion.sqlCommand.Parameters.Clear();
                conexion.sqlCommand.Parameters.Add(new SqlParameter("@Nombre", SqlDbType.VarChar)).Value = PorcentajeAnonymous.Nombre;
                conexion.sqlCommand.Parameters.Add(new SqlParameter("@Porcentaje", SqlDbType.Float)).Value = PorcentajeAnonymous.Porcentaje;
                IdPorcentaje = Convert.ToInt32(conexion.sqlCommand.ExecuteScalar().ToString());
            }

            catch (Exception e)
            {
                throw;
            }
            finally
            {
                conexion.CerrarConexion();
            }
            return IdPorcentaje;
        }
        public DataTable Buscar(PorcentajeAnonymous PorcentajeAnonymous)
        {
            SqlDataReader sqldrPorcentajes = null;
            DataTable dtPorcentajes = new DataTable();
            Conexion conexion = new Conexion();
            conexion.AbrirConexion(false);
            try
            {
                conexion.sqlCommand.CommandType = CommandType.StoredProcedure;
                conexion.sqlCommand.CommandText = "SelectPorcentaje";
                conexion.sqlCommand.Parameters.Clear();
                conexion.sqlCommand.Parameters.Add(new SqlParameter("@Nombre", SqlDbType.VarChar)).Value = string.IsNullOrEmpty(PorcentajeAnonymous.Nombre) ? SqlString.Null : PorcentajeAnonymous.Nombre;
                //conexion.sqlCommand.Parameters.Add(new SqlParameter("@Porcentaje", SqlDbType.Float)).Value = string.IsNullOrEmpty(PorcentajeAnonymous.Porcentaje) ? SqlString.Null : PorcentajeAnonymous.Porcentaje;
                conexion.sqlCommand.Parameters.Add(new SqlParameter("@Estatus", SqlDbType.Bit)).Value = string.IsNullOrEmpty(PorcentajeAnonymous.EstatusSTR) ? SqlBoolean.Null : (PorcentajeAnonymous.EstatusSTR == "1" ? true : false);
                sqldrPorcentajes = conexion.sqlCommand.ExecuteReader();
                if (sqldrPorcentajes.HasRows)
                {
                    dtPorcentajes.Load(sqldrPorcentajes);
                }
            }

            catch (Exception e)
            {
                throw;
            }
            finally
            {
                if (sqldrPorcentajes != null)
                {
                    if (!sqldrPorcentajes.IsClosed)
                    {
                        sqldrPorcentajes.Close();
                    }
                    sqldrPorcentajes.Dispose();
                }
                conexion.CerrarConexion();
            }
            return dtPorcentajes;
        }

        public int Editar(PorcentajeAnonymous PorcentajeAnonymous)
        {
            Conexion conexion = new Conexion();
            conexion.AbrirConexion(false);
            int Editado = 0;
            try
            {
                conexion.sqlCommand.CommandType = CommandType.StoredProcedure;
                conexion.sqlCommand.CommandText = "UpdatePorcentaje";
                conexion.sqlCommand.Parameters.Clear();
                conexion.sqlCommand.Parameters.Add(new SqlParameter("@Id", SqlDbType.SmallInt)).Value = PorcentajeAnonymous.Id;
                conexion.sqlCommand.Parameters.Add(new SqlParameter("@Nombre", SqlDbType.VarChar)).Value = PorcentajeAnonymous.Nombre;
                conexion.sqlCommand.Parameters.Add(new SqlParameter("@Porcentaje", SqlDbType.VarChar)).Value = PorcentajeAnonymous.Porcentaje;
                conexion.sqlCommand.Parameters.Add(new SqlParameter("@Estatus", SqlDbType.Bit)).Value = PorcentajeAnonymous.Estatus;
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
                conexion.sqlCommand.CommandText = "DeletePorcentaje";
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