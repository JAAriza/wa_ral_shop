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
    public class RepositorioDocumento
    {
        public Int16 Alta(DocumentoAnonymous DocumentoAnonymous)
        {
            Conexion conexion = new Conexion();
            conexion.AbrirConexion(false);
            Int16 IdDocumento = 0;
            try
            {
                conexion.sqlCommand.CommandType = CommandType.StoredProcedure;
                conexion.sqlCommand.CommandText = "InsertDocumento";
                conexion.sqlCommand.Parameters.Clear();
                conexion.sqlCommand.Parameters.Add(new SqlParameter("@Documento", SqlDbType.VarChar)).Value = DocumentoAnonymous.Documento;
                IdDocumento = Convert.ToInt16(conexion.sqlCommand.ExecuteScalar().ToString());
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
            return IdDocumento;
        }
        public DataTable Buscar(DocumentoAnonymous DocumentoAnonymous)
        {
            SqlDataReader sqldrDocumentos = null;
            DataTable dtDocumentos = new DataTable();
            Conexion conexion = new Conexion();
            conexion.AbrirConexion(false);
            try
            {
                conexion.sqlCommand.CommandType = CommandType.StoredProcedure;
                conexion.sqlCommand.CommandText = "SelectDocumento";
                conexion.sqlCommand.Parameters.Clear();
                conexion.sqlCommand.Parameters.Add(new SqlParameter("@Documento", SqlDbType.VarChar)).Value = string.IsNullOrEmpty(DocumentoAnonymous.Documento) ? SqlString.Null : DocumentoAnonymous.Documento;
                conexion.sqlCommand.Parameters.Add(new SqlParameter("@Estatus", SqlDbType.Bit)).Value = string.IsNullOrEmpty(DocumentoAnonymous.EstatusSTR) ? SqlBoolean.Null : (DocumentoAnonymous.EstatusSTR == "1" ? true : false);
                sqldrDocumentos = conexion.sqlCommand.ExecuteReader();
                if (sqldrDocumentos.HasRows)
                {
                    dtDocumentos.Load(sqldrDocumentos);
                }
            }

            catch (Exception e)
            {
                string error = e.Message;
                throw;
            }
            finally
            {
                if (sqldrDocumentos != null)
                {
                    if (!sqldrDocumentos.IsClosed)
                    {
                        sqldrDocumentos.Close();
                    }
                    sqldrDocumentos.Dispose();
                }
                conexion.CerrarConexion();
            }
            return dtDocumentos;
        }

        public int Editar(DocumentoAnonymous DocumentoAnonymous)
        {
            Conexion conexion = new Conexion();
            conexion.AbrirConexion(false);
            int Editado = 0;
            try
            {
                conexion.sqlCommand.CommandType = CommandType.StoredProcedure;
                conexion.sqlCommand.CommandText = "UpdateDocumento";
                conexion.sqlCommand.Parameters.Clear();
                conexion.sqlCommand.Parameters.Add(new SqlParameter("@Id", SqlDbType.SmallInt)).Value = DocumentoAnonymous.Id;
                conexion.sqlCommand.Parameters.Add(new SqlParameter("@Documento", SqlDbType.VarChar)).Value = DocumentoAnonymous.Documento;
                conexion.sqlCommand.Parameters.Add(new SqlParameter("@Estatus", SqlDbType.Bit)).Value = DocumentoAnonymous.Estatus;
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
                conexion.sqlCommand.CommandText = "DeleteDocumento";
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
    }
}