using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using wa_ral_shop.Models.Anonymous.Administracion;

namespace wa_ral_shop.Models.Repositorios.Administracion
{
    public class RepositorioCompra
    {
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
                conexion.sqlCommand.CommandText = "SelectComboCompras";
                conexion.sqlCommand.Parameters.Clear();

                da = new SqlDataAdapter(conexion.sqlCommand);
                da.Fill(ds);

                lstCombos.Add(ds.Tables[0]);//Productos
                lstCombos.Add(ds.Tables[1]);//Proveedores
            }

            catch (Exception e)
            {
                throw;
            }
            finally
            {
                conexion.CerrarConexion();
            }
            return lstCombos;
        }

        public DataTable Buscar(DateTime FI, DateTime FF, string Estatus)
        {
            SqlDataReader sqldrCompras = null;
            DataTable dtCompras = new DataTable();
            Conexion conexion = new Conexion();
            conexion.AbrirConexion(false);
            try
            {
                conexion.sqlCommand.CommandType = CommandType.StoredProcedure;
                conexion.sqlCommand.CommandText = "SelectCompra";
                conexion.sqlCommand.Parameters.Clear();
                conexion.sqlCommand.Parameters.Add(new SqlParameter("@FechaInicio", SqlDbType.DateTime)).Value = FI;
                conexion.sqlCommand.Parameters.Add(new SqlParameter("@FechaFin", SqlDbType.DateTime)).Value = FF;
                conexion.sqlCommand.Parameters.Add(new SqlParameter("@Estatus", SqlDbType.VarChar)).Value = string.IsNullOrEmpty(Estatus) ? Convert.ToString(null) : Estatus;
                sqldrCompras = conexion.sqlCommand.ExecuteReader();
                if (sqldrCompras.HasRows)
                {
                    dtCompras.Load(sqldrCompras);
                }
            }

            catch (Exception e)
            {
                throw;
            }
            finally
            {
                if (sqldrCompras != null)
                {
                    if (!sqldrCompras.IsClosed)
                    {
                        sqldrCompras.Close();
                    }
                    sqldrCompras.Dispose();
                }
                conexion.CerrarConexion();
            }
            return dtCompras;
        }

        public int Alta(CompraAnonymous compraAnonymous)
        {
            Conexion conexion = new Conexion();
            conexion.AbrirConexion(false);
            int IdCompra = 0;
            try
            {
                conexion.sqlCommand.CommandType = CommandType.StoredProcedure;
                conexion.sqlCommand.CommandText = "InsertCompra";
                conexion.sqlCommand.Parameters.Clear();
                conexion.sqlCommand.Parameters.Add(new SqlParameter("@CostoEnvio", SqlDbType.Money)).Value = compraAnonymous.CostoEnvio;
                conexion.sqlCommand.Parameters.Add(new SqlParameter("@FechaHoraAvisoImportacion", SqlDbType.DateTime)).Value = compraAnonymous.FechaHoraAvisoImportacion;
                conexion.sqlCommand.Parameters.Add(new SqlParameter("@FechaHoraLlegadaMX", SqlDbType.DateTime)).Value = compraAnonymous.FechaHoraLlegadaMX;
                conexion.sqlCommand.Parameters.Add(new SqlParameter("@CostoTotal", SqlDbType.Money)).Value = compraAnonymous.CostoTotal;
                conexion.sqlCommand.Parameters.Add(new SqlParameter("@Estatus", SqlDbType.VarChar)).Value = compraAnonymous.Estatus;
                IdCompra = Convert.ToInt32(conexion.sqlCommand.ExecuteScalar().ToString());
            }

            catch (Exception e)
            {
                throw;
            }
            finally
            {
                conexion.CerrarConexion();
            }
            return IdCompra;
        }

        public int GuardarDetalle(DataTable dtCompraD)
        {
            Conexion conexion = new Conexion();
            conexion.AbrirConexion(false);
            int Hecho = 0;
            try
            {
                conexion.sqlCommand.CommandType = CommandType.StoredProcedure;
                conexion.sqlCommand.CommandText = "InsertCompraDet";
                conexion.sqlCommand.Parameters.Clear();
                conexion.sqlCommand.Parameters.Add(new SqlParameter("@TVP", SqlDbType.Structured)).Value = dtCompraD;

                Hecho = Convert.ToInt32(conexion.sqlCommand.ExecuteNonQuery().ToString());
            }

            catch (Exception e)
            {
                throw;
            }
            finally
            {
                conexion.CerrarConexion();
            }
            return Hecho;
        }

        public int EliminarCompra(int IdCompra)
        {
            Conexion conexion = new Conexion();
            conexion.AbrirConexion(false);
            int Hecho = 0;
            try
            {
                conexion.sqlCommand.CommandType = CommandType.StoredProcedure;
                conexion.sqlCommand.CommandText = "DeleteCompra";
                conexion.sqlCommand.Parameters.Clear();
                conexion.sqlCommand.Parameters.Add(new SqlParameter("@IdCompra", SqlDbType.Int)).Value = IdCompra;

                Hecho = Convert.ToInt32(conexion.sqlCommand.ExecuteNonQuery().ToString());
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                conexion.CerrarConexion();
            }
            return Hecho;
        }

        public DataTable GetDetalle(int IdCompra)
        {
            SqlDataReader sqldrCompraDet = null;
            DataTable dtCompraDet = new DataTable();
            Conexion conexion = new Conexion();
            conexion.AbrirConexion(false);
            try
            {
                conexion.sqlCommand.CommandType = CommandType.StoredProcedure;
                conexion.sqlCommand.CommandText = "SelectDetalleCompra";
                conexion.sqlCommand.Parameters.Clear();
                conexion.sqlCommand.Parameters.Add(new SqlParameter("@Id", SqlDbType.Int)).Value = IdCompra;
                sqldrCompraDet = conexion.sqlCommand.ExecuteReader();
                if (sqldrCompraDet.HasRows)
                {
                    dtCompraDet.Load(sqldrCompraDet);
                }
            }

            catch (Exception e)
            {
                throw;
            }
            finally
            {
                if (sqldrCompraDet != null)
                {
                    if (!sqldrCompraDet.IsClosed)
                    {
                        sqldrCompraDet.Close();
                    }
                    sqldrCompraDet.Dispose();
                }
                conexion.CerrarConexion();
            }
            return dtCompraDet;
        }

        public int Editar(CompraAnonymous compraA)
        {
            Conexion conexion = new Conexion();
            conexion.AbrirConexion(false);
            int Editado = 0;
            try
            {
                conexion.sqlCommand.CommandType = CommandType.StoredProcedure;
                conexion.sqlCommand.CommandText = "UpdateCompra";
                conexion.sqlCommand.Parameters.Clear();
                conexion.sqlCommand.Parameters.Add(new SqlParameter("@Id", SqlDbType.Int)).Value = compraA.Id;
                conexion.sqlCommand.Parameters.Add(new SqlParameter("@CostoEnvio", SqlDbType.Decimal)).Value = compraA.CostoEnvio;
                conexion.sqlCommand.Parameters.Add(new SqlParameter("@FHAvisoImportacion", SqlDbType.DateTime)).Value = compraA.FechaHoraAvisoImportacion;
                conexion.sqlCommand.Parameters.Add(new SqlParameter("@FHLlegadaMX", SqlDbType.DateTime)).Value = compraA.FechaHoraLlegadaMX;
                conexion.sqlCommand.Parameters.Add(new SqlParameter("@CostoTotal", SqlDbType.Decimal)).Value = compraA.CostoTotal;
                conexion.sqlCommand.Parameters.Add(new SqlParameter("@Estatus", SqlDbType.VarChar)).Value = compraA.Estatus;
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

        public int EditarDetalle(DataTable dtDetCompra)
        {
            Conexion conexion = new Conexion();
            conexion.AbrirConexion(false);
            int Hecho = 0;
            try
            {
                conexion.sqlCommand.CommandType = CommandType.StoredProcedure;
                conexion.sqlCommand.CommandText = "UpdateCompraDet";
                conexion.sqlCommand.Parameters.Clear();
                conexion.sqlCommand.Parameters.Add(new SqlParameter("@TVP", SqlDbType.Structured)).Value = dtDetCompra;

                Hecho = Convert.ToInt32(conexion.sqlCommand.ExecuteNonQuery().ToString());
            }

            catch (Exception e)
            {
                throw;
            }
            finally
            {
                conexion.CerrarConexion();
            }
            return Hecho;
        }

        public int EliminarCompraEditar(int IdCompra)
        {
            Conexion conexion = new Conexion();
            conexion.AbrirConexion(false);
            int Hecho = 0;
            try
            {
                conexion.sqlCommand.CommandType = CommandType.StoredProcedure;
                conexion.sqlCommand.CommandText = "DeleteCompraEditar";
                conexion.sqlCommand.Parameters.Clear();
                conexion.sqlCommand.Parameters.Add(new SqlParameter("@IdCompra", SqlDbType.Int)).Value = IdCompra;

                Hecho = Convert.ToInt32(conexion.sqlCommand.ExecuteNonQuery().ToString());
            }
            catch (Exception)
            {
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