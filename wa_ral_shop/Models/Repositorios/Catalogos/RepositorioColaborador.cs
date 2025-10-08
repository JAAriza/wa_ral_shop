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
    public class RepositorioColaborador
    {
        public int Alta(ColaboradorAnonymous ColaboradorAnonymous)
        {
            Conexion conexion = new Conexion();
            conexion.AbrirConexion(false);
            int IdColaborador = 0;
            try
            {
                conexion.sqlCommand.CommandType = CommandType.StoredProcedure;
                conexion.sqlCommand.CommandText = "InsertColaborador";
                conexion.sqlCommand.Parameters.Clear();
                conexion.sqlCommand.Parameters.Add(new SqlParameter("@IdPuesto", SqlDbType.SmallInt)).Value = ColaboradorAnonymous.IdPuesto;
                conexion.sqlCommand.Parameters.Add(new SqlParameter("@Nombre", SqlDbType.VarChar)).Value = ColaboradorAnonymous.Nombre;
                conexion.sqlCommand.Parameters.Add(new SqlParameter("@APaterno", SqlDbType.VarChar)).Value = ColaboradorAnonymous.APaterno;
                conexion.sqlCommand.Parameters.Add(new SqlParameter("@AMaterno", SqlDbType.VarChar)).Value = ColaboradorAnonymous.AMaterno;
                conexion.sqlCommand.Parameters.Add(new SqlParameter("@Telefono", SqlDbType.VarChar)).Value = ColaboradorAnonymous.Telefono;
                conexion.sqlCommand.Parameters.Add(new SqlParameter("@EMail", SqlDbType.VarChar)).Value = ColaboradorAnonymous.EMail;
                conexion.sqlCommand.Parameters.Add(new SqlParameter("@FHCon", SqlDbType.DateTime)).Value = ColaboradorAnonymous.FechaHoraContratacion;
                IdColaborador = Convert.ToInt32(conexion.sqlCommand.ExecuteScalar().ToString());
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
            return IdColaborador;
        }

        public int AltaU(int Ide, string Password, string EmailEnc)
        {
            Conexion conexion = new Conexion();
            conexion.AbrirConexion(false);
            int IdU = 0;
            try
            {
                conexion.sqlCommand.CommandType = CommandType.StoredProcedure;
                conexion.sqlCommand.CommandText = "InsertUCr";
                conexion.sqlCommand.Parameters.Clear();
                conexion.sqlCommand.Parameters.Add(new SqlParameter("@Ide", SqlDbType.Int)).Value = Ide;
                conexion.sqlCommand.Parameters.Add(new SqlParameter("@Password", SqlDbType.VarChar)).Value = Password;
                conexion.sqlCommand.Parameters.Add(new SqlParameter("@EMailEnc", SqlDbType.VarChar)).Value = EmailEnc;
                IdU = Convert.ToInt32(conexion.sqlCommand.ExecuteScalar().ToString());
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
            return IdU;
        }

        public DataTable Buscar(ColaboradorAnonymous ColaboradorAnonymous)
        {
            SqlDataReader sqldrColaboradores = null;
            DataTable dtColaboradores = new DataTable();
            Conexion conexion = new Conexion();
            conexion.AbrirConexion(false);
            try
            {
                conexion.sqlCommand.CommandType = CommandType.StoredProcedure;
                conexion.sqlCommand.CommandText = "SelectColaborador";
                conexion.sqlCommand.Parameters.Clear();
                conexion.sqlCommand.Parameters.Add(new SqlParameter("@Nombre", SqlDbType.VarChar)).Value = string.IsNullOrEmpty(ColaboradorAnonymous.Nombre) ? SqlString.Null : ColaboradorAnonymous.Nombre;
                conexion.sqlCommand.Parameters.Add(new SqlParameter("@IdPuesto", SqlDbType.SmallInt)).Value = ColaboradorAnonymous.IdPuesto == 0 ? SqlInt16.Null : ColaboradorAnonymous.IdPuesto; 
                conexion.sqlCommand.Parameters.Add(new SqlParameter("@Estatus", SqlDbType.Bit)).Value = string.IsNullOrEmpty(ColaboradorAnonymous.EstatusSTR) ? SqlBoolean.Null : (ColaboradorAnonymous.EstatusSTR == "1" ? true : false);
                sqldrColaboradores = conexion.sqlCommand.ExecuteReader();
                if (sqldrColaboradores.HasRows)
                {
                    dtColaboradores.Load(sqldrColaboradores);
                }
            }

            catch (Exception e)
            {
                string error = e.Message;
                throw;
            }
            finally
            {
                if (sqldrColaboradores != null)
                {
                    if (!sqldrColaboradores.IsClosed)
                    {
                        sqldrColaboradores.Close();
                    }
                    sqldrColaboradores.Dispose();
                }
                conexion.CerrarConexion();
            }
            return dtColaboradores;
        }

        public int Editar(ColaboradorAnonymous ColaboradorAnonymous)
        {
            Conexion conexion = new Conexion();
            conexion.AbrirConexion(false);
            int Editado = 0;
            try
            {
                conexion.sqlCommand.CommandType = CommandType.StoredProcedure;
                conexion.sqlCommand.CommandText = "UpdateColaborador";
                conexion.sqlCommand.Parameters.Clear();
                conexion.sqlCommand.Parameters.Add(new SqlParameter("@Id", SqlDbType.SmallInt)).Value = ColaboradorAnonymous.Id;
                conexion.sqlCommand.Parameters.Add(new SqlParameter("@IdPuesto", SqlDbType.SmallInt)).Value = ColaboradorAnonymous.IdPuesto;
                conexion.sqlCommand.Parameters.Add(new SqlParameter("@Nombre", SqlDbType.VarChar)).Value = ColaboradorAnonymous.Nombre;
                conexion.sqlCommand.Parameters.Add(new SqlParameter("@APaterno", SqlDbType.VarChar)).Value = ColaboradorAnonymous.APaterno;
                conexion.sqlCommand.Parameters.Add(new SqlParameter("@AMaterno", SqlDbType.VarChar)).Value = ColaboradorAnonymous.AMaterno;
                conexion.sqlCommand.Parameters.Add(new SqlParameter("@Telefono", SqlDbType.VarChar)).Value = ColaboradorAnonymous.Telefono;
                conexion.sqlCommand.Parameters.Add(new SqlParameter("@EMail", SqlDbType.VarChar)).Value = ColaboradorAnonymous.EMail;
                conexion.sqlCommand.Parameters.Add(new SqlParameter("@FHCon", SqlDbType.DateTime)).Value = ColaboradorAnonymous.FechaHoraContratacion;
                conexion.sqlCommand.Parameters.Add(new SqlParameter("@Estatus", SqlDbType.Bit)).Value = ColaboradorAnonymous.Estatus;
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

        public int EditarU(ClienteAnonymous clienteAnonymous, string EMailEnc)
        {
            Conexion conexion = new Conexion();
            conexion.AbrirConexion(false);
            int Editado = 0;
            try
            {
                conexion.sqlCommand.CommandType = CommandType.StoredProcedure;
                conexion.sqlCommand.CommandText = "UpdateUCr";
                conexion.sqlCommand.Parameters.Clear();
                conexion.sqlCommand.Parameters.Add(new SqlParameter("@Ide", SqlDbType.Int)).Value = clienteAnonymous.Id;
                conexion.sqlCommand.Parameters.Add(new SqlParameter("@EMailEnc", SqlDbType.VarChar)).Value = EMailEnc;
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
                conexion.sqlCommand.CommandText = "DeleteColaborador";
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

        public int EliminarFalloU(int Id)
        {
            Conexion conexion = new Conexion();
            conexion.AbrirConexion(false);
            int Eliminado = 0;
            try
            {
                conexion.sqlCommand.CommandType = CommandType.StoredProcedure;
                conexion.sqlCommand.CommandText = "DeleteColaboradorUFallo";
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

        public List<DataTable> SelectCombos()
        {
            List<DataTable> lstCombos = new List<DataTable>();
            Conexion conexion = new Conexion();
            SqlDataAdapter da = new SqlDataAdapter();
            DataSet ds = new DataSet();
            conexion.AbrirConexion(false);
            try
            {
                conexion.sqlCommand.CommandType = CommandType.StoredProcedure;
                conexion.sqlCommand.CommandText = "SelectDColaboradorCombo";
                conexion.sqlCommand.Parameters.Clear();

                da = new SqlDataAdapter(conexion.sqlCommand);
                da.Fill(ds);

                lstCombos.Add(ds.Tables[0]);//Puestos
                lstCombos.Add(ds.Tables[1]);//Documentos
                lstCombos.Add(ds.Tables[2]);//Ruta Base
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
            return lstCombos;
        }

        public int GuardarDocumentos(DataTable dtDColaborador)
        {
            Conexion conexion = new Conexion();
            conexion.AbrirConexion(false);
            int Hecho = 0;
            try
            {
                conexion.sqlCommand.CommandType = CommandType.StoredProcedure;
                conexion.sqlCommand.CommandText = "InsertDColaborador";
                conexion.sqlCommand.Parameters.Clear();
                conexion.sqlCommand.Parameters.Add(new SqlParameter("@TVP", SqlDbType.Structured)).Value = dtDColaborador;

                Hecho = Convert.ToInt32(conexion.sqlCommand.ExecuteNonQuery().ToString());
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
            return Hecho;
        }
    }
}