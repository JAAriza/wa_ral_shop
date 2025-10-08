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
    public class RepositorioProducto
    {
        public int Alta(ProductoAnonymous productoAnonymous)
        {
            Conexion conexion = new Conexion();
            conexion.AbrirConexion(false);
            int IdProducto = 0;
            try
            {
                conexion.sqlCommand.CommandType = CommandType.StoredProcedure;
                conexion.sqlCommand.CommandText = "InsertProducto";
                conexion.sqlCommand.Parameters.Clear();
                conexion.sqlCommand.Parameters.Add(new SqlParameter("@IdCategoria", SqlDbType.TinyInt)).Value = productoAnonymous.IdCategoria;
                conexion.sqlCommand.Parameters.Add(new SqlParameter("@Nombre", SqlDbType.VarChar)).Value = productoAnonymous.Nombre;
                conexion.sqlCommand.Parameters.Add(new SqlParameter("@Modelo", SqlDbType.VarChar)).Value = productoAnonymous.Modelo;
                conexion.sqlCommand.Parameters.Add(new SqlParameter("@Descripcion", SqlDbType.VarChar)).Value = productoAnonymous.Descripcion;
                conexion.sqlCommand.Parameters.Add(new SqlParameter("@Largo", SqlDbType.Float)).Value = productoAnonymous.Largo;
                conexion.sqlCommand.Parameters.Add(new SqlParameter("@Ancho", SqlDbType.Float)).Value = productoAnonymous.Ancho;
                conexion.sqlCommand.Parameters.Add(new SqlParameter("@Alto", SqlDbType.Float)).Value = productoAnonymous.Alto;
                conexion.sqlCommand.Parameters.Add(new SqlParameter("@IdUMedidaT", SqlDbType.TinyInt)).Value = productoAnonymous.IdUMedidaT;
                conexion.sqlCommand.Parameters.Add(new SqlParameter("@Peso", SqlDbType.Float)).Value = productoAnonymous.Peso;
                conexion.sqlCommand.Parameters.Add(new SqlParameter("@IdUMedidaP", SqlDbType.TinyInt)).Value = productoAnonymous.IdUMedidaP;
                conexion.sqlCommand.Parameters.Add(new SqlParameter("@CodBarras", SqlDbType.VarChar)).Value = productoAnonymous.CodBarras;
                conexion.sqlCommand.Parameters.Add(new SqlParameter("@PrecioVenta", SqlDbType.Money)).Value = productoAnonymous.PrecioVenta;
                //conexion.sqlCommand.Parameters.Add(new SqlParameter("@Existencias", SqlDbType.Int)).Value = productoAnonymous.Existencias;
                IdProducto = Convert.ToInt32(conexion.sqlCommand.ExecuteScalar().ToString());
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
            return IdProducto;
        }
        public DataTable Buscar(ProductoAnonymous productoAnonymous)
        {
            SqlDataReader sqldrProductos = null;
            DataTable dtProductos = new DataTable();
            Conexion conexion = new Conexion();
            conexion.AbrirConexion(false);
            try
            {
                conexion.sqlCommand.CommandType = CommandType.StoredProcedure;
                conexion.sqlCommand.CommandText = "SelectProducto";
                conexion.sqlCommand.Parameters.Clear();
                conexion.sqlCommand.Parameters.Add(new SqlParameter("@Nombre", SqlDbType.VarChar)).Value = string.IsNullOrEmpty(productoAnonymous.Nombre) ? SqlString.Null : productoAnonymous.Nombre;
                conexion.sqlCommand.Parameters.Add(new SqlParameter("@Estatus", SqlDbType.Bit)).Value = string.IsNullOrEmpty(productoAnonymous.EstatusSTR) ? SqlBoolean.Null : (productoAnonymous.EstatusSTR == "1" ? true : false);
                sqldrProductos = conexion.sqlCommand.ExecuteReader();
                if (sqldrProductos.HasRows)
                {
                    dtProductos.Load(sqldrProductos);
                }
            }

            catch (Exception e)
            {
                string error = e.Message;
                throw;
            }
            finally
            {
                if (sqldrProductos != null)
                {
                    if (!sqldrProductos.IsClosed)
                    {
                        sqldrProductos.Close();
                    }
                    sqldrProductos.Dispose();
                }
                conexion.CerrarConexion();
            }
            return dtProductos;
        }

        public int Editar(ProductoAnonymous ProductoAnonymous)
        {
            Conexion conexion = new Conexion();
            conexion.AbrirConexion(false);
            int Editado = 0;
            try
            {
                conexion.sqlCommand.CommandType = CommandType.StoredProcedure;
                conexion.sqlCommand.CommandText = "UpdateProducto";
                conexion.sqlCommand.Parameters.Clear();
                conexion.sqlCommand.Parameters.Add(new SqlParameter("@Id", SqlDbType.Int)).Value = ProductoAnonymous.Id;
                conexion.sqlCommand.Parameters.Add(new SqlParameter("@IdCategoria", SqlDbType.TinyInt)).Value = ProductoAnonymous.IdCategoria;
                conexion.sqlCommand.Parameters.Add(new SqlParameter("@Nombre", SqlDbType.VarChar)).Value = ProductoAnonymous.Nombre;
                conexion.sqlCommand.Parameters.Add(new SqlParameter("@Modelo", SqlDbType.VarChar)).Value = ProductoAnonymous.Modelo;
                conexion.sqlCommand.Parameters.Add(new SqlParameter("@Descripcion", SqlDbType.VarChar)).Value = ProductoAnonymous.Descripcion;
                conexion.sqlCommand.Parameters.Add(new SqlParameter("@Largo", SqlDbType.Float)).Value = ProductoAnonymous.Largo;
                conexion.sqlCommand.Parameters.Add(new SqlParameter("@Ancho", SqlDbType.Float)).Value = ProductoAnonymous.Ancho;
                conexion.sqlCommand.Parameters.Add(new SqlParameter("@Alto", SqlDbType.Float)).Value = ProductoAnonymous.Alto;
                conexion.sqlCommand.Parameters.Add(new SqlParameter("@IdUMedidaT", SqlDbType.TinyInt)).Value = ProductoAnonymous.IdUMedidaT;
                conexion.sqlCommand.Parameters.Add(new SqlParameter("@Peso", SqlDbType.Float)).Value = ProductoAnonymous.Peso;
                conexion.sqlCommand.Parameters.Add(new SqlParameter("@IdUMedidaP", SqlDbType.TinyInt)).Value = ProductoAnonymous.IdUMedidaP;
                conexion.sqlCommand.Parameters.Add(new SqlParameter("@CodBarras", SqlDbType.VarChar)).Value = ProductoAnonymous.CodBarras;
                conexion.sqlCommand.Parameters.Add(new SqlParameter("@PrecioVenta", SqlDbType.Money)).Value = ProductoAnonymous.PrecioVenta;
                //conexion.sqlCommand.Parameters.Add(new SqlParameter("@Existencias", SqlDbType.Int)).Value = ProductoAnonymous.Existencias;
                conexion.sqlCommand.Parameters.Add(new SqlParameter("@Estatus", SqlDbType.Bit)).Value = ProductoAnonymous.Estatus;
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
                conexion.sqlCommand.CommandText = "DeleteProducto";
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
                conexion.sqlCommand.CommandText = "SelectComboProductos";
                conexion.sqlCommand.Parameters.Clear();

                da = new SqlDataAdapter(conexion.sqlCommand);
                da.Fill(ds);

                lstCombos.Add(ds.Tables[0]);//Categorias
                lstCombos.Add(ds.Tables[1]);//UnidadDeMedida
                lstCombos.Add(ds.Tables[2]);//RutaBase
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

        public DataTable GetImagenes(int IdProducto)
        {
            SqlDataReader sqldrProductos = null;
            DataTable dtProductos = new DataTable();
            Conexion conexion = new Conexion();
            conexion.AbrirConexion(false);
            try
            {
                conexion.sqlCommand.CommandType = CommandType.StoredProcedure;
                conexion.sqlCommand.CommandText = "SelectIMGProducto";
                conexion.sqlCommand.Parameters.Clear();
                conexion.sqlCommand.Parameters.Add(new SqlParameter("@Id", SqlDbType.Int)).Value = IdProducto;
                sqldrProductos = conexion.sqlCommand.ExecuteReader();
                if (sqldrProductos.HasRows)
                {
                    dtProductos.Load(sqldrProductos);
                }
            }

            catch (Exception e)
            {
                string error = e.Message;
                throw;
            }
            finally
            {
                if (sqldrProductos != null)
                {
                    if (!sqldrProductos.IsClosed)
                    {
                        sqldrProductos.Close();
                    }
                    sqldrProductos.Dispose();
                }
                conexion.CerrarConexion();
            }
            return dtProductos;
        }

        public int EliminarImgProd(int Id)
        {
            Conexion conexion = new Conexion();
            conexion.AbrirConexion(false);
            int Eliminado = 0;
            try
            {
                conexion.sqlCommand.CommandType = CommandType.StoredProcedure;
                conexion.sqlCommand.CommandText = "DeleteProductoImagenes";
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

        public int GuardarMedia(DataTable dtImg)
        {
            Conexion conexion = new Conexion();
            conexion.AbrirConexion(false);
            int Hecho = 0;
            try
            {
                conexion.sqlCommand.CommandType = CommandType.StoredProcedure;
                conexion.sqlCommand.CommandText = "InsertPImagenes";
                conexion.sqlCommand.Parameters.Clear();
                conexion.sqlCommand.Parameters.Add(new SqlParameter("@TVP", SqlDbType.Structured)).Value = dtImg;

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

        public DataTable GetProductosActivosDashboard()
        {
            SqlDataReader sqldrProductos = null;
            DataTable dtProductos = new DataTable();
            Conexion conexion = new Conexion();
            conexion.AbrirConexion(false);
            try
            {
                conexion.sqlCommand.CommandType = CommandType.StoredProcedure;
                conexion.sqlCommand.CommandText = "SelectProductoseImagenes";
                conexion.sqlCommand.Parameters.Clear();
                sqldrProductos = conexion.sqlCommand.ExecuteReader();
                if (sqldrProductos.HasRows)
                {
                    dtProductos.Load(sqldrProductos);
                }
            }

            catch (Exception e)
            {
                string error = e.Message;
                throw;
            }
            finally
            {
                if (sqldrProductos != null)
                {
                    if (!sqldrProductos.IsClosed)
                    {
                        sqldrProductos.Close();
                    }
                    sqldrProductos.Dispose();
                }
                conexion.CerrarConexion();
            }
            return dtProductos;
        }

        public DataTable GetProductoDetalle(int IdProducto)
        {
            SqlDataReader sqldrProductos = null;
            DataTable dtProductos = new DataTable();
            Conexion conexion = new Conexion();
            conexion.AbrirConexion(false);
            try
            {
                conexion.sqlCommand.CommandType = CommandType.StoredProcedure;
                conexion.sqlCommand.CommandText = "SelectProductoeImagenes";
                conexion.sqlCommand.Parameters.Clear();
                conexion.sqlCommand.Parameters.Add(new SqlParameter("@IdProducto", SqlDbType.Int)).Value = IdProducto;
                sqldrProductos = conexion.sqlCommand.ExecuteReader();
                if (sqldrProductos.HasRows)
                {
                    dtProductos.Load(sqldrProductos);
                }
            }

            catch (Exception e)
            {
                string error = e.Message;
                throw;
            }
            finally
            {
                if (sqldrProductos != null)
                {
                    if (!sqldrProductos.IsClosed)
                    {
                        sqldrProductos.Close();
                    }
                    sqldrProductos.Dispose();
                }
                conexion.CerrarConexion();
            }
            return dtProductos;
        }

    }
}