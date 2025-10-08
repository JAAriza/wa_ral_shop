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

namespace wa_ral_shop.Models.Repositorios.Catalogos
{
    public class RepositorioCuenta
    {
        public DataTable Buscar(int Ide)
        {
            SqlDataReader sqldrCuentas = null;
            DataTable dtInformacion = new DataTable();
            Conexion conexion = new Conexion();
            conexion.AbrirConexion(false);
            try
            {
                conexion.sqlCommand.CommandType = CommandType.StoredProcedure;
                conexion.sqlCommand.CommandText = "SelectClienteUbica";
                conexion.sqlCommand.Parameters.Clear();
                conexion.sqlCommand.Parameters.Add(new SqlParameter("@Id", SqlDbType.Int)).Value = Ide;
                sqldrCuentas = conexion.sqlCommand.ExecuteReader();
                if (sqldrCuentas.HasRows)
                {
                    dtInformacion.Load(sqldrCuentas);
                }
            }

            catch (Exception e)
            {
                string error = e.Message;
                throw;
            }
            finally
            {
                if (sqldrCuentas != null)
                {
                    if (!sqldrCuentas.IsClosed)
                    {
                        sqldrCuentas.Close();
                    }
                    sqldrCuentas.Dispose();
                }
                conexion.CerrarConexion();
            }
            return dtInformacion;
        }

        public int Editar(ClienteAnonymous clienteAnonymous)
        {
            Conexion conexion = new Conexion();
            conexion.AbrirConexion(false);
            int Editado = 0;
            try
            {
                conexion.sqlCommand.CommandType = CommandType.StoredProcedure;
                conexion.sqlCommand.CommandText = "UpdateClienteCuenta";
                conexion.sqlCommand.Parameters.Clear();
                conexion.sqlCommand.Parameters.Add(new SqlParameter("@Id", SqlDbType.Int)).Value = clienteAnonymous.Id;
                conexion.sqlCommand.Parameters.Add(new SqlParameter("@Nombre", SqlDbType.VarChar)).Value = clienteAnonymous.Nombre;
                conexion.sqlCommand.Parameters.Add(new SqlParameter("@APaterno", SqlDbType.VarChar)).Value = clienteAnonymous.APaterno;
                conexion.sqlCommand.Parameters.Add(new SqlParameter("@AMaterno", SqlDbType.VarChar)).Value = clienteAnonymous.AMaterno;
                conexion.sqlCommand.Parameters.Add(new SqlParameter("@Telefono", SqlDbType.VarChar)).Value = clienteAnonymous.Telefono;
                conexion.sqlCommand.Parameters.Add(new SqlParameter("@EMail", SqlDbType.VarChar)).Value = clienteAnonymous.EMail;
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

        public int EditarUbicacion(CDireccionAnonymous cDireccionAnonymous)
        {
            Conexion conexion = new Conexion();
            conexion.AbrirConexion(false);
            int Editado = 0;
            try
            {
                conexion.sqlCommand.CommandType = CommandType.StoredProcedure;
                conexion.sqlCommand.CommandText = "UpdateClienteDireccion";
                conexion.sqlCommand.Parameters.Clear();
                conexion.sqlCommand.Parameters.Add(new SqlParameter("@Id", SqlDbType.Int)).Value = cDireccionAnonymous.Id;
                conexion.sqlCommand.Parameters.Add(new SqlParameter("@Nombre", SqlDbType.VarChar)).Value = cDireccionAnonymous.Nombre;
                conexion.sqlCommand.Parameters.Add(new SqlParameter("@Telefono", SqlDbType.VarChar)).Value = cDireccionAnonymous.Telefono;
                conexion.sqlCommand.Parameters.Add(new SqlParameter("@CP", SqlDbType.Int)).Value = cDireccionAnonymous.CP;
                conexion.sqlCommand.Parameters.Add(new SqlParameter("@Colonia", SqlDbType.VarChar)).Value = cDireccionAnonymous.Colonia;
                conexion.sqlCommand.Parameters.Add(new SqlParameter("@Calle", SqlDbType.VarChar)).Value = cDireccionAnonymous.Calle;
                conexion.sqlCommand.Parameters.Add(new SqlParameter("@NExterior", SqlDbType.VarChar)).Value = cDireccionAnonymous.NExterior;
                conexion.sqlCommand.Parameters.Add(new SqlParameter("@NInterior", SqlDbType.VarChar)).Value = cDireccionAnonymous.NInterior;
                conexion.sqlCommand.Parameters.Add(new SqlParameter("@EntreCalle", SqlDbType.VarChar)).Value = cDireccionAnonymous.EntreCalle;
                conexion.sqlCommand.Parameters.Add(new SqlParameter("@YCalle", SqlDbType.VarChar)).Value = cDireccionAnonymous.YCalle;
                conexion.sqlCommand.Parameters.Add(new SqlParameter("@Descripcion", SqlDbType.VarChar)).Value = cDireccionAnonymous.Descripcion;
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

        public int Eliminar(int Id)
        {
            Conexion conexion = new Conexion();
            conexion.AbrirConexion(false);
            int Eliminado = 0;
            try
            {
                conexion.sqlCommand.CommandType = CommandType.StoredProcedure;
                conexion.sqlCommand.CommandText = "DeleteClienteDireccion";
                conexion.sqlCommand.Parameters.Clear();
                conexion.sqlCommand.Parameters.Add(new SqlParameter("@Id", SqlDbType.Int)).Value = Id;
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