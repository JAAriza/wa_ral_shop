﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlTypes;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using wa_ral_shop.Models;
using wa_ral_shop.Models.Anonymous.Catalogos;
using wa_ral_shop.Models.Anonymous.Administracion;

namespace wa_ral_shop.Models.Repositorios
{
    public class RepositorioSesion
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
                //conexion.sqlCommand.Parameters.Add(new SqlParameter("@Estrellas", SqlDbType.TinyInt)).Value = clienteAnonymous.Estrellas;
                conexion.sqlCommand.Parameters.Add(new SqlParameter("@Telefono", SqlDbType.VarChar)).Value = clienteAnonymous.Telefono;
                conexion.sqlCommand.Parameters.Add(new SqlParameter("@EMail", SqlDbType.VarChar)).Value = clienteAnonymous.EMail;
                //conexion.sqlCommand.Parameters.Add(new SqlParameter("@Password", SqlDbType.VarChar)).Value = clienteAnonymous.Password;
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

        public int AltaD(CDireccionAnonymous cDireccionAnonymous)
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

        public int ValidaLogin(string UserEnc, string PassEnc, int TUsuario)
        {
            int Val;
            Conexion conexion = new Conexion();
            conexion.AbrirConexion(false);
            try
            {
                conexion.sqlCommand.CommandType = CommandType.StoredProcedure;
                conexion.sqlCommand.CommandText = "SelectValidaLogin";
                conexion.sqlCommand.Parameters.Clear();
                conexion.sqlCommand.Parameters.Add(new SqlParameter("@Usuario", SqlDbType.VarChar)).Value=UserEnc;
                conexion.sqlCommand.Parameters.Add(new SqlParameter("@Password", SqlDbType.VarChar)).Value = PassEnc;
                conexion.sqlCommand.Parameters.Add(new SqlParameter("@TUsuario", SqlDbType.TinyInt)).Value = byte.Parse(TUsuario.ToString());
                var a = conexion.sqlCommand.ExecuteScalar();
                if (a !=null)
                {
                    Val = Convert.ToInt32(a);
                }
                else
                {
                    Val = 0;
                }
            }
            catch (Exception)
            {

                throw;
            }
            return Val;
        }
    }
}