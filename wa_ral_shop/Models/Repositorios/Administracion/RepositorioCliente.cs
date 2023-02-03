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
    public class RepositorioCliente
    {
        public int Alta(ClienteAnonymous clienteAnonymous)
        {
            Conexion conexion = new Conexion();
            conexion.AbrirConexion(false);
            int IdCliente = 0;
            try
            {
                conexion.sqlCommand.CommandType = CommandType.StoredProcedure;
                conexion.sqlCommand.CommandText = "InsertCliente";
                conexion.sqlCommand.Parameters.Clear();
                conexion.sqlCommand.Parameters.Add(new SqlParameter("@Nombre", SqlDbType.VarChar)).Value = clienteAnonymous.Nombre;
                conexion.sqlCommand.Parameters.Add(new SqlParameter("@APaterno", SqlDbType.VarChar)).Value = clienteAnonymous.APaterno;
                conexion.sqlCommand.Parameters.Add(new SqlParameter("@AMaterno", SqlDbType.VarChar)).Value = clienteAnonymous.AMaterno;
                conexion.sqlCommand.Parameters.Add(new SqlParameter("@Estrellas", SqlDbType.TinyInt)).Value = clienteAnonymous.Estrellas;
                conexion.sqlCommand.Parameters.Add(new SqlParameter("@Telefono", SqlDbType.VarChar)).Value = clienteAnonymous.Telefono;
                conexion.sqlCommand.Parameters.Add(new SqlParameter("@EMail", SqlDbType.VarChar)).Value = clienteAnonymous.EMail;
                
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
        public int AltaU(int Ide, string Password, string EmailEnc)
        {
            Conexion conexion = new Conexion();
            conexion.AbrirConexion(false);
            int IdU = 0;
            try
            {
                conexion.sqlCommand.CommandType = CommandType.StoredProcedure;
                conexion.sqlCommand.CommandText = "InsertUC";
                conexion.sqlCommand.Parameters.Clear();
                conexion.sqlCommand.Parameters.Add(new SqlParameter("@Ide", SqlDbType.Int)).Value = Ide;
                conexion.sqlCommand.Parameters.Add(new SqlParameter("@Password", SqlDbType.VarChar)).Value = Password;
                conexion.sqlCommand.Parameters.Add(new SqlParameter("@EMailEnc", SqlDbType.VarChar)).Value = EmailEnc;
                IdU = Convert.ToInt32(conexion.sqlCommand.ExecuteScalar().ToString());
            }

            catch (Exception e)
            {
                throw;
            }
            finally
            {
                conexion.CerrarConexion();
            }
            return IdU;
        }
        public DataTable Buscar(ClienteAnonymous clienteAnonymous)
        {
            SqlDataReader sqldrClientes = null;
            DataTable dtClientes = new DataTable();
            Conexion conexion = new Conexion();
            conexion.AbrirConexion(false);
            try
            {
                conexion.sqlCommand.CommandType = CommandType.StoredProcedure;
                conexion.sqlCommand.CommandText = "SelectCliente";
                conexion.sqlCommand.Parameters.Clear();
                conexion.sqlCommand.Parameters.Add(new SqlParameter("@Nombre", SqlDbType.VarChar)).Value = string.IsNullOrEmpty(clienteAnonymous.Nombre) ? SqlString.Null : clienteAnonymous.Nombre;
                conexion.sqlCommand.Parameters.Add(new SqlParameter("@APaterno", SqlDbType.VarChar)).Value = string.IsNullOrEmpty(clienteAnonymous.APaterno) ? SqlString.Null : clienteAnonymous.APaterno;
                conexion.sqlCommand.Parameters.Add(new SqlParameter("@AMaterno", SqlDbType.VarChar)).Value = string.IsNullOrEmpty(clienteAnonymous.AMaterno) ? SqlString.Null : clienteAnonymous.AMaterno;
                conexion.sqlCommand.Parameters.Add(new SqlParameter("@Estatus", SqlDbType.Bit)).Value = string.IsNullOrEmpty(clienteAnonymous.EstatusSTR) ? SqlBoolean.Null : (clienteAnonymous.EstatusSTR == "1" ? true : false);
                sqldrClientes = conexion.sqlCommand.ExecuteReader();
                if (sqldrClientes.HasRows)
                {
                    dtClientes.Load(sqldrClientes);
                }
            }

            catch (Exception e)
            {
                throw;
            }
            finally
            {
                if (sqldrClientes != null)
                {
                    if (!sqldrClientes.IsClosed)
                    {
                        sqldrClientes.Close();
                    }
                    sqldrClientes.Dispose();
                }
                conexion.CerrarConexion();
            }
            return dtClientes;
        }

        public int Editar(ClienteAnonymous clienteAnonymous, string EMailEnc)
        {
            Conexion conexion = new Conexion();
            conexion.AbrirConexion(false);
            int Editado = 0;
            try
            {
                conexion.sqlCommand.CommandType = CommandType.StoredProcedure;
                conexion.sqlCommand.CommandText = "UpdateCliente";
                conexion.sqlCommand.Parameters.Clear();
                conexion.sqlCommand.Parameters.Add(new SqlParameter("@Id", SqlDbType.Int)).Value = clienteAnonymous.Id;
                conexion.sqlCommand.Parameters.Add(new SqlParameter("@Nombre", SqlDbType.VarChar)).Value = clienteAnonymous.Nombre;
                conexion.sqlCommand.Parameters.Add(new SqlParameter("@APaterno", SqlDbType.VarChar)).Value = clienteAnonymous.APaterno;
                conexion.sqlCommand.Parameters.Add(new SqlParameter("@AMaterno", SqlDbType.VarChar)).Value = clienteAnonymous.AMaterno;
                conexion.sqlCommand.Parameters.Add(new SqlParameter("@Estrellas", SqlDbType.TinyInt)).Value = clienteAnonymous.Estrellas;
                conexion.sqlCommand.Parameters.Add(new SqlParameter("@Telefono", SqlDbType.VarChar)).Value = clienteAnonymous.Telefono;
                conexion.sqlCommand.Parameters.Add(new SqlParameter("@EMail", SqlDbType.VarChar)).Value = clienteAnonymous.EMail;
                conexion.sqlCommand.Parameters.Add(new SqlParameter("@Estatus", SqlDbType.Bit)).Value = clienteAnonymous.Estatus;
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

        public int EditarU(ClienteAnonymous clienteAnonymous, string EMailEnc)
        {
            Conexion conexion = new Conexion();
            conexion.AbrirConexion(false);
            int Editado = 0;
            try
            {
                conexion.sqlCommand.CommandType = CommandType.StoredProcedure;
                conexion.sqlCommand.CommandText = "UpdateUC";
                conexion.sqlCommand.Parameters.Clear();
                conexion.sqlCommand.Parameters.Add(new SqlParameter("@Ide", SqlDbType.Int)).Value = clienteAnonymous.Id;
                conexion.sqlCommand.Parameters.Add(new SqlParameter("@EMailEnc", SqlDbType.VarChar)).Value = EMailEnc;
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
                conexion.sqlCommand.CommandText = "DeleteCliente";
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

        public int EliminarFalloU(int Id)
        {
            Conexion conexion = new Conexion();
            conexion.AbrirConexion(false);
            int Eliminado = 0;
            try
            {
                conexion.sqlCommand.CommandType = CommandType.StoredProcedure;
                conexion.sqlCommand.CommandText = "DeleteClienteUFallo";
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

        public int EliminarUC(int Id)
        {
            Conexion conexion = new Conexion();
            conexion.AbrirConexion(false);
            int Eliminado = 0;
            try
            {
                conexion.sqlCommand.CommandType = CommandType.StoredProcedure;
                conexion.sqlCommand.CommandText = "DeleteUC";
                conexion.sqlCommand.Parameters.Clear();
                conexion.sqlCommand.Parameters.Add(new SqlParameter("@Ide", SqlDbType.Int)).Value = Id;
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