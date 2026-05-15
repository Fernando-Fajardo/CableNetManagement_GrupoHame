using MODELO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DATOS
{
    public class AsignacionDatos
    {
        ConexionDatos conexionDatos = new ConexionDatos();

        /* ---- CONSULTAR ---- */
        public DataTable MtdConsultar()
        {
            DataTable dt = new DataTable();

            using (SqlConnection conn = conexionDatos.MtdConexionBDD())
            {
                using (SqlCommand cmd = new SqlCommand("usp_ConsultarClienteServicio", conn))
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

        /* ---- AGREGAR ---- */
        public string MtdAgregarAsignacion(AsignacionEntidad asignacion)
        {
            using (SqlConnection conn = conexionDatos.MtdConexionBDD())
            {
                conn.Open();

                using (SqlCommand cmd = new SqlCommand("usp_AsignarServicioCliente", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@IdCliente", asignacion.IdCliente);
                    cmd.Parameters.AddWithValue("@IdServicio", asignacion.IdServicio);
                    cmd.Parameters.AddWithValue("@DireccionInstalacion", asignacion.DireccionInstalacion);
                    cmd.Parameters.AddWithValue("@FechaInstalacion", asignacion.FechaInstalacion);
                    cmd.Parameters.AddWithValue("@Estado", asignacion.Estado);

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

        /* ---- EDITAR ---- */
        public string MtdEditarAsignacion(AsignacionEntidad asignacion)
        {
            using (SqlConnection conn = conexionDatos.MtdConexionBDD())
            {
                conn.Open();

                using (SqlCommand cmd = new SqlCommand("usp_EditarClienteServicio", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@IdClienteServicio", asignacion.IdClienteServicio);
                    cmd.Parameters.AddWithValue("@IdCliente", asignacion.IdCliente);
                    cmd.Parameters.AddWithValue("@IdServicio", asignacion.IdServicio);
                    cmd.Parameters.AddWithValue("@DireccionInstalacion", asignacion.DireccionInstalacion);
                    cmd.Parameters.AddWithValue("@FechaInstalacion", asignacion.FechaInstalacion);
                    cmd.Parameters.AddWithValue("@Estado", asignacion.Estado);

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

        /* ---- ELIMINAR ---- */
        public string MtdEliminarAsignacion(int idClienteServicio)
        {
            using (SqlConnection conn = conexionDatos.MtdConexionBDD())
            {
                conn.Open();

                using (SqlCommand cmd = new SqlCommand("usp_EliminarClienteServicio", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@IdClienteServicio", idClienteServicio);

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

        /* ---- BUSCAR ---- */
        public DataTable MtdBuscarAsignacion(string cliente)
        {
            DataTable dt = new DataTable();

            using (SqlConnection conn = conexionDatos.MtdConexionBDD())
            {
                using (SqlCommand cmd = new SqlCommand("usp_BuscarClienteServicio", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@NombreCliente", cliente);

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
    }
}