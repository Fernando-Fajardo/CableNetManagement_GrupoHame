using MODELO;
using System;
using System.Data;
using System.Data.SqlClient;

namespace DATOS
{
    public class ServiciosDatos
    {
        ConexionDatos conexionDatos = new ConexionDatos();

        /* ---- CONSULTAR SERVICIOS ---- */
        public DataTable MtdConsultar()
        {
            DataTable dt = new DataTable();

            using (SqlConnection conn = conexionDatos.MtdConexionBDD())
            {
                using (SqlCommand cmd = new SqlCommand("USP_ConsultarServicio", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    try
                    {
                        conn.Open();

                        using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                        {
                            da.Fill(dt);
                        }
                    }
                    catch (Exception)
                    {
                        throw;
                    }
                }
            }

            return dt;
        }

        /* ---- AGREGAR SERVICIO ---- */
        public string MtdAgregarServicio(ServiciosEntidad servicio)
        {
            using (SqlConnection conn = conexionDatos.MtdConexionBDD())
            {
                conn.Open();

                using (SqlCommand cmd = new SqlCommand("usp_AgregarServicio", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@NombreServicio", servicio.NombreServicio);
                    cmd.Parameters.AddWithValue("@TipoServicio", servicio.TipoServicio);

                    cmd.Parameters.AddWithValue("@VelocidadMbps",
                        servicio.VelocidadMbps == 0
                        ? (object)DBNull.Value
                        : servicio.VelocidadMbps);

                    cmd.Parameters.AddWithValue("@TipoCable",
                        string.IsNullOrWhiteSpace(servicio.TipoCable)
                        ? (object)DBNull.Value
                        : servicio.TipoCable);

                    cmd.Parameters.AddWithValue("@CantidadCanales",
                        servicio.CantidadCanales == 0
                        ? (object)DBNull.Value
                        : servicio.CantidadCanales);

                    cmd.Parameters.AddWithValue("@PrecioMensual", servicio.PrecioMensual);

                    var pResultado = new SqlParameter("@Resultado", SqlDbType.Bit)
                    {
                        Direction = ParameterDirection.Output
                    };

                    var pMensaje = new SqlParameter("@Mensaje", SqlDbType.NVarChar, 500)
                    {
                        Direction = ParameterDirection.Output
                    };

                    cmd.Parameters.Add(pResultado);
                    cmd.Parameters.Add(pMensaje);

                    cmd.ExecuteNonQuery();

                    bool resultado = Convert.ToBoolean(pResultado.Value);
                    string mensaje = pMensaje.Value?.ToString() ?? "Sin mensaje del servidor";

                    if (!resultado)
                        throw new Exception(mensaje);

                    return mensaje;
                }
            }
        }

        /* ---- EDITAR SERVICIO ---- */
        public string MtdEditarServicio(ServiciosEntidad servicio)
        {
            using (SqlConnection conn = conexionDatos.MtdConexionBDD())
            {
                conn.Open();

                using (SqlCommand cmd = new SqlCommand("usp_EditarServicio", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@IdServicio", servicio.IdServicio);
                    cmd.Parameters.AddWithValue("@NombreServicio", servicio.NombreServicio);
                    cmd.Parameters.AddWithValue("@TipoServicio", servicio.TipoServicio);

                    cmd.Parameters.AddWithValue("@VelocidadMbps",
                        servicio.VelocidadMbps == 0
                        ? (object)DBNull.Value
                        : servicio.VelocidadMbps);

                    cmd.Parameters.AddWithValue("@TipoCable",
                        string.IsNullOrWhiteSpace(servicio.TipoCable)
                        ? (object)DBNull.Value
                        : servicio.TipoCable);

                    cmd.Parameters.AddWithValue("@CantidadCanales",
                        servicio.CantidadCanales == 0
                        ? (object)DBNull.Value
                        : servicio.CantidadCanales);

                    cmd.Parameters.AddWithValue("@PrecioMensual", servicio.PrecioMensual);
                    cmd.Parameters.AddWithValue("@Estado", servicio.Estado);

                    var pResultado = new SqlParameter("@Resultado", SqlDbType.Bit)
                    {
                        Direction = ParameterDirection.Output
                    };

                    var pMensaje = new SqlParameter("@Mensaje", SqlDbType.NVarChar, 500)
                    {
                        Direction = ParameterDirection.Output
                    };

                    cmd.Parameters.Add(pResultado);
                    cmd.Parameters.Add(pMensaje);

                    cmd.ExecuteNonQuery();

                    bool resultado = Convert.ToBoolean(pResultado.Value);
                    string mensaje = pMensaje.Value?.ToString() ?? "Sin mensaje del servidor";

                    if (!resultado)
                        throw new ApplicationException(mensaje);

                    return mensaje;
                }
            }
        }

        /* ---- ELIMINAR SERVICIO ---- */
        public string MtdEliminarServicio(int idServicio)
        {
            using (SqlConnection conn = conexionDatos.MtdConexionBDD())
            {
                conn.Open();

                using (SqlCommand cmd = new SqlCommand("usp_EliminarServicio", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@IdServicio", idServicio);

                    var pResultado = new SqlParameter("@Resultado", SqlDbType.Bit)
                    {
                        Direction = ParameterDirection.Output
                    };

                    var pMensaje = new SqlParameter("@Mensaje", SqlDbType.NVarChar, 500)
                    {
                        Direction = ParameterDirection.Output
                    };

                    cmd.Parameters.Add(pResultado);
                    cmd.Parameters.Add(pMensaje);

                    cmd.ExecuteNonQuery();

                    bool resultado = Convert.ToBoolean(pResultado.Value);
                    string mensaje = pMensaje.Value?.ToString() ?? "Sin mensaje del servidor";

                    if (!resultado)
                        throw new Exception(mensaje);

                    return mensaje;
                }
            }
        }

        /* ---- BUSCAR SERVICIO ---- */
        public DataTable MtdBuscarServicio(string tipoServicio)
        {
            DataTable dt = new DataTable();

            using (SqlConnection conn = conexionDatos.MtdConexionBDD())
            {
                using (SqlCommand cmd = new SqlCommand("usp_BuscarServicio", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@TipoServicio", tipoServicio);

                    try
                    {
                        conn.Open();

                        using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                        {
                            da.Fill(dt);
                        }
                    }
                    catch (Exception)
                    {
                        throw;
                    }
                }
            }

            return dt;
        }
        public DataTable ListarServiciosCombo()
        {
            DataTable dt = new DataTable();
            string query = "SELECT IdServicio, NombreServicio FROM Servicio ORDER BY NombreServicio ASC";

            try
            {
                using (SqlConnection conn = conexionDatos.MtdConexionBDD())
                {
                    SqlCommand cmd = new SqlCommand(query, conn);
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error en Capa Datos Servicios: " + ex.Message);
            }
            return dt;
        }
    }
}