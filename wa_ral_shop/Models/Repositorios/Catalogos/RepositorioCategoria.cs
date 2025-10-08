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
    public class RepositorioCategoria
    {
        public byte Alta(CategoriaAnonymous categoriaAnonymous)
        {
            Conexion conexion = new Conexion();
            conexion.AbrirConexion(false);
            byte IdCategoria = 0;
            try
            {
                conexion.sqlCommand.CommandType = CommandType.StoredProcedure;
                conexion.sqlCommand.CommandText = "InsertCategoria";
                conexion.sqlCommand.Parameters.Clear();
                conexion.sqlCommand.Parameters.Add(new SqlParameter("@Categoria", SqlDbType.VarChar)).Value = categoriaAnonymous.Categoria;
                IdCategoria = Convert.ToByte(conexion.sqlCommand.ExecuteScalar().ToString());
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
            return IdCategoria;
        }
        public DataTable Buscar(CategoriaAnonymous categoriaAnonymous)
        {
            SqlDataReader sqldrCategorias = null;
            DataTable dtCategorias = new DataTable();
            Conexion conexion = new Conexion();
            conexion.AbrirConexion(false);
            try
            {
                conexion.sqlCommand.CommandType = CommandType.StoredProcedure;
                conexion.sqlCommand.CommandText = "SelectCategoria";
                conexion.sqlCommand.Parameters.Clear();
                conexion.sqlCommand.Parameters.Add(new SqlParameter("@Categoria", SqlDbType.VarChar)).Value = string.IsNullOrEmpty(categoriaAnonymous.Categoria) ? SqlString.Null : categoriaAnonymous.Categoria;
                conexion.sqlCommand.Parameters.Add(new SqlParameter("@Estatus", SqlDbType.Bit)).Value = string.IsNullOrEmpty(categoriaAnonymous.EstatusSTR) ? SqlBoolean.Null : (categoriaAnonymous.EstatusSTR == "1" ? true : false);
                sqldrCategorias = conexion.sqlCommand.ExecuteReader();
                if (sqldrCategorias.HasRows)
                {
                    dtCategorias.Load(sqldrCategorias);
                }
            }

            catch (Exception e)
            {
                string error = e.Message;
                throw;
            }
            finally
            {
                if (sqldrCategorias != null)
                {
                    if (!sqldrCategorias.IsClosed)
                    {
                        sqldrCategorias.Close();
                    }
                    sqldrCategorias.Dispose();
                }
                conexion.CerrarConexion();
            }
            return dtCategorias;
        }

        public int Editar(CategoriaAnonymous categoriaAnonymous)
        {
            Conexion conexion = new Conexion();
            conexion.AbrirConexion(false);
            int Editado = 0;
            try
            {
                conexion.sqlCommand.CommandType = CommandType.StoredProcedure;
                conexion.sqlCommand.CommandText = "UpdateCategoria";
                conexion.sqlCommand.Parameters.Clear();
                conexion.sqlCommand.Parameters.Add(new SqlParameter("@Id", SqlDbType.TinyInt)).Value = categoriaAnonymous.Id;
                conexion.sqlCommand.Parameters.Add(new SqlParameter("@Categoria", SqlDbType.VarChar)).Value = categoriaAnonymous.Categoria;
                conexion.sqlCommand.Parameters.Add(new SqlParameter("@Estatus", SqlDbType.Bit)).Value = categoriaAnonymous.Estatus;
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

        public int Eliminar(byte Id)
        {
            Conexion conexion = new Conexion();
            conexion.AbrirConexion(false);
            int Eliminado = 0;
            try
            {
                conexion.sqlCommand.CommandType = CommandType.StoredProcedure;
                conexion.sqlCommand.CommandText = "DeleteCategoria";
                conexion.sqlCommand.Parameters.Clear();
                conexion.sqlCommand.Parameters.Add(new SqlParameter("@Id", SqlDbType.TinyInt)).Value = Id;
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