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
    public class RepositorioPuesto
    {
        public Int16 Alta(PuestoAnonymous PuestoAnonymous)
        {
            Conexion conexion = new Conexion();
            conexion.AbrirConexion(false);
            Int16 IdPuesto = 0;
            try
            {
                conexion.sqlCommand.CommandType = CommandType.StoredProcedure;
                conexion.sqlCommand.CommandText = "InsertPuesto";
                conexion.sqlCommand.Parameters.Clear();
                conexion.sqlCommand.Parameters.Add(new SqlParameter("@Puesto", SqlDbType.VarChar)).Value = PuestoAnonymous.Puesto;
                IdPuesto = Convert.ToInt16(conexion.sqlCommand.ExecuteScalar().ToString());
            }

            catch (Exception e)
            {
                throw;
            }
            finally
            {
                conexion.CerrarConexion();
            }
            return IdPuesto;
        }
        public DataTable Buscar(PuestoAnonymous PuestoAnonymous)
        {
            SqlDataReader sqldrPuestos = null;
            DataTable dtPuestos = new DataTable();
            Conexion conexion = new Conexion();
            conexion.AbrirConexion(false);
            try
            {
                conexion.sqlCommand.CommandType = CommandType.StoredProcedure;
                conexion.sqlCommand.CommandText = "SelectPuesto";
                conexion.sqlCommand.Parameters.Clear();
                conexion.sqlCommand.Parameters.Add(new SqlParameter("@Puesto", SqlDbType.VarChar)).Value = string.IsNullOrEmpty(PuestoAnonymous.Puesto) ? SqlString.Null : PuestoAnonymous.Puesto;
                conexion.sqlCommand.Parameters.Add(new SqlParameter("@Estatus", SqlDbType.Bit)).Value = string.IsNullOrEmpty(PuestoAnonymous.EstatusSTR) ? SqlBoolean.Null : (PuestoAnonymous.EstatusSTR == "1" ? true : false);
                sqldrPuestos = conexion.sqlCommand.ExecuteReader();
                if (sqldrPuestos.HasRows)
                {
                    dtPuestos.Load(sqldrPuestos);
                }
            }

            catch (Exception e)
            {
                throw;
            }
            finally
            {
                if (sqldrPuestos != null)
                {
                    if (!sqldrPuestos.IsClosed)
                    {
                        sqldrPuestos.Close();
                    }
                    sqldrPuestos.Dispose();
                }
                conexion.CerrarConexion();
            }
            return dtPuestos;
        }

        public int Editar(PuestoAnonymous PuestoAnonymous)
        {
            Conexion conexion = new Conexion();
            conexion.AbrirConexion(false);
            int Editado = 0;
            try
            {
                conexion.sqlCommand.CommandType = CommandType.StoredProcedure;
                conexion.sqlCommand.CommandText = "UpdatePuesto";
                conexion.sqlCommand.Parameters.Clear();
                conexion.sqlCommand.Parameters.Add(new SqlParameter("@Id", SqlDbType.TinyInt)).Value = PuestoAnonymous.Id;
                conexion.sqlCommand.Parameters.Add(new SqlParameter("@Puesto", SqlDbType.VarChar)).Value = PuestoAnonymous.Puesto;
                conexion.sqlCommand.Parameters.Add(new SqlParameter("@Estatus", SqlDbType.Bit)).Value = PuestoAnonymous.Estatus;
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
                conexion.sqlCommand.CommandText = "DeletePuesto";
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