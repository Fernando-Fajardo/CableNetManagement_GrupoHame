using MODELO;
using System;
using System.Data;
using System.Data.SqlClient;

namespace DATOS
{
    public class ClientesDatos
    {
        ConexionDatos conexionDatos = new ConexionDatos();

        /* =========================
           CONSULTAR CLIENTES
        ========================= */
        public DataTable MtdConsultarClientes()
        {
            DataTable dt = new DataTable();

            using (SqlConnection conn = conexionDatos.MtdConexionBDD())
            {
                using (SqlCommand cmd = new SqlCommand("usp_ConsultarCliente", conn))
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

        /* =========================
           AGREGAR CLIENTE
        ========================= */
        public string MtdAgregarCliente(ClientesEntidad cliente)
        {
            using (SqlConnection conn = conexionDatos.MtdConexionBDD())
            {
                conn.Open();

                using (SqlCommand cmd = new SqlCommand("usp_AgregarCliente", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@CodigoCliente", cliente.CodigoCliente);
                    cmd.Parameters.AddWithValue("@Nombre", cliente.Nombre);
                    cmd.Parameters.AddWithValue("@FechaAlta", cliente.FechaAlta);
                    cmd.Parameters.AddWithValue("@Direccion", cliente.Direccion);
                    cmd.Parameters.AddWithValue("@Correo", cliente.Correo);
                    cmd.Parameters.AddWithValue("@Telefono", cliente.Telefono);
                    cmd.Parameters.AddWithValue("@Estado", cliente.Estado);

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

        /* =========================
           EDITAR CLIENTE
        ========================= */
        public string MtdEditarCliente(ClientesEntidad cliente)
        {
            using (SqlConnection conn = conexionDatos.MtdConexionBDD())
            {
                conn.Open();

                using (SqlCommand cmd = new SqlCommand("usp_EditarCliente", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@IdCliente", cliente.IdCliente);
                    cmd.Parameters.AddWithValue("@CodigoCliente", cliente.CodigoCliente);
                    cmd.Parameters.AddWithValue("@Nombre", cliente.Nombre);
                    cmd.Parameters.AddWithValue("@FechaAlta", cliente.FechaAlta);
                    cmd.Parameters.AddWithValue("@Direccion", cliente.Direccion);
                    cmd.Parameters.AddWithValue("@Correo", cliente.Correo);
                    cmd.Parameters.AddWithValue("@Telefono", cliente.Telefono);
                    cmd.Parameters.AddWithValue("@Estado", cliente.Estado);

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

        /* =========================
           ELIMINAR CLIENTE
        ========================= */
        public string MtdEliminarCliente(int idCliente)
        {
            using (SqlConnection conn = conexionDatos.MtdConexionBDD())
            {
                conn.Open();

                using (SqlCommand cmd = new SqlCommand("usp_EliminarCliente", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@IdCliente", idCliente);

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

        /* =========================
           BUSCAR CLIENTE
        ========================= */
        public DataTable MtdBuscarCliente(string cliente)
        {
            DataTable dt = new DataTable();

            using (SqlConnection conn = conexionDatos.MtdConexionBDD())
            {
                using (SqlCommand cmd = new SqlCommand("usp_BuscarCliente", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@CodigoCliente", cliente);

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
        public DataTable ListarParaCombo()
        {
            DataTable dt = new DataTable();
            string query = "SELECT IdCliente, Nombre FROM Cliente ORDER BY Nombre ASC";

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
                throw new Exception("Error al obtener clientes: " + ex.Message);
            }

            return dt;
        }
        public int MtdObtenerUltimoId()
        {
            int ultimoId = 0;
            string query = "SELECT ISNULL(MAX(IdCliente), 0) FROM Cliente";

            using (SqlConnection conn = conexionDatos.MtdConexionBDD())
            {
                SqlCommand cmd = new SqlCommand(query, conn);
                conn.Open();
                ultimoId = Convert.ToInt32(cmd.ExecuteScalar());
            }
            return ultimoId;
        }
    }
}