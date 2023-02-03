using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlTypes;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using wa_ral_shop.Models;
using wa_ral_shop.Models.Anonymous.Catalogos;
using wa_ral_shop.Models.Anonymous.Administracion;

namespace wa_ral_shop.Models.Repositorios.Administracion
{
    public class RepositorioCDireccion
    {
        public DataTable BuscarColoniasPorCP(int CP)
        {
            SqlDataReader sqldrColonias = null;
            DataTable dtColonias = new DataTable();
            Conexion conexion = new Conexion();
            conexion.AbrirConexion(false);
            try
            {
                conexion.sqlCommand.CommandType = CommandType.StoredProcedure;
                conexion.sqlCommand.CommandText = "SelectColoniasPorCP";
                conexion.sqlCommand.Parameters.Clear();
                conexion.sqlCommand.Parameters.Add(new SqlParameter("@CP", SqlDbType.Int)).Value = CP;
                sqldrColonias = conexion.sqlCommand.ExecuteReader();
                if (sqldrColonias.HasRows)
                {
                    dtColonias.Load(sqldrColonias);
                }
            }

            catch (Exception e)
            {
                throw;
            }
            finally
            {
                if (sqldrColonias != null)
                {
                    if (!sqldrColonias.IsClosed)
                    {
                        sqldrColonias.Close();
                    }
                    sqldrColonias.Dispose();
                }
                conexion.CerrarConexion();
            }
            return dtColonias;
        }

        public DataTable BuscarEdoyMpioPorCP(int CP)
        {
            SqlDataReader sqldrEdoMpio = null;
            DataTable dtEdoMpio = new DataTable();
            Conexion conexion = new Conexion();
            conexion.AbrirConexion(false);
            try
            {
                conexion.sqlCommand.CommandType = CommandType.StoredProcedure;
                conexion.sqlCommand.CommandText = "SelectEdoyMpioPorCP";
                conexion.sqlCommand.Parameters.Clear();
                conexion.sqlCommand.Parameters.Add(new SqlParameter("@CP", SqlDbType.Int)).Value = CP;
                sqldrEdoMpio = conexion.sqlCommand.ExecuteReader();
                if (sqldrEdoMpio.HasRows)
                {
                    dtEdoMpio.Load(sqldrEdoMpio);
                }
            }

            catch (Exception e)
            {
                throw;
            }
            finally
            {
                if (sqldrEdoMpio != null)
                {
                    if (!sqldrEdoMpio.IsClosed)
                    {
                        sqldrEdoMpio.Close();
                    }
                    sqldrEdoMpio.Dispose();
                }
                conexion.CerrarConexion();
            }
            return dtEdoMpio;
        }

        public int Alta(CDireccionAnonymous cDireccionAnonymous)
        {
            Conexion conexion = new Conexion();
            conexion.AbrirConexion(false);
            int IdCliente = 0;
            try
            {
                conexion.sqlCommand.CommandType = CommandType.StoredProcedure;
                conexion.sqlCommand.CommandText = "InsertCDireccion";
                conexion.sqlCommand.Parameters.Clear();
                conexion.sqlCommand.Parameters.Add(new SqlParameter("@IdCliente", SqlDbType.Int)).Value = cDireccionAnonymous.IdCliente;
                conexion.sqlCommand.Parameters.Add(new SqlParameter("@CP", SqlDbType.Int)).Value = cDireccionAnonymous.CP;
                conexion.sqlCommand.Parameters.Add(new SqlParameter("@Colonia", SqlDbType.VarChar)).Value = cDireccionAnonymous.Colonia;
                conexion.sqlCommand.Parameters.Add(new SqlParameter("@Calle", SqlDbType.VarChar)).Value = cDireccionAnonymous.Calle;
                conexion.sqlCommand.Parameters.Add(new SqlParameter("@NExterior", SqlDbType.VarChar)).Value = cDireccionAnonymous.NExterior;
                conexion.sqlCommand.Parameters.Add(new SqlParameter("@NInterior", SqlDbType.VarChar)).Value = cDireccionAnonymous.NInterior;
                conexion.sqlCommand.Parameters.Add(new SqlParameter("@Nombre", SqlDbType.VarChar)).Value = cDireccionAnonymous.Nombre;
                conexion.sqlCommand.Parameters.Add(new SqlParameter("@Telefono", SqlDbType.VarChar)).Value = cDireccionAnonymous.Telefono;
                conexion.sqlCommand.Parameters.Add(new SqlParameter("@Descripcion", SqlDbType.VarChar)).Value = cDireccionAnonymous.Descripcion;
                conexion.sqlCommand.Parameters.Add(new SqlParameter("@EntreCalle", SqlDbType.VarChar)).Value = cDireccionAnonymous.EntreCalle;
                conexion.sqlCommand.Parameters.Add(new SqlParameter("@YCalle", SqlDbType.VarChar)).Value = cDireccionAnonymous.YCalle;
                IdCliente = Convert.ToInt32(conexion.sqlCommand.ExecuteScalar().ToString());
            }

            catch (Exception e)
            {
                throw;
            }
            finally
            {
                conexion.CerrarConexion();
            }
            return IdCliente;
        }
    }
}