using DATOS;
using MODELO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NEGOCIO
{
    public class ClientesNegocio
    {
        ClientesDatos clientesDatos = new ClientesDatos();
        private ClientesDatos objetoCD = new ClientesDatos();
        /* =========================
           CONSULTAR CLIENTES
        ========================= */
        public List<ClientesEntidad> MtdConsultarClientes()
        {
            try
            {
                DataTable dt = clientesDatos.MtdConsultarClientes();

                List<ClientesEntidad> lista = new List<ClientesEntidad>();

                foreach (DataRow row in dt.Rows)
                {
                    ClientesEntidad cliente = new ClientesEntidad
                    {
                        IdCliente = Convert.ToInt32(row["IdCliente"]),
                        CodigoCliente = row["CodigoCliente"].ToString(),
                        Nombre = row["Nombre"].ToString(),
                        FechaAlta = Convert.ToDateTime(row["FechaAlta"]),
                        Direccion = row["Direccion"].ToString(),
                        Correo = row["Correo"].ToString(),
                        Telefono = row["Telefono"].ToString(),
                        Estado = row["Estado"].ToString()
                    };

                    lista.Add(cliente);
                }

                return lista;
            }
            catch
            {
                throw;
            }
        }

        /* =========================
           AGREGAR CLIENTE
        ========================= */
        public string MtdAgregarCliente(ClientesEntidad cliente)
        {
            if (cliente == null)
                throw new ArgumentNullException(nameof(cliente), "El cliente no puede ser nulo.");

            if (string.IsNullOrWhiteSpace(cliente.CodigoCliente))
                throw new ArgumentException("El código de cliente es obligatorio.");

            if (string.IsNullOrWhiteSpace(cliente.Nombre))
                throw new ArgumentException("El nombre es obligatorio.");

            if (string.IsNullOrWhiteSpace(cliente.Direccion))
                throw new ArgumentException("La dirección es obligatoria.");

            if (string.IsNullOrWhiteSpace(cliente.Correo))
                throw new ArgumentException("El correo es obligatorio.");

            if (string.IsNullOrWhiteSpace(cliente.Telefono))
                throw new ArgumentException("El teléfono es obligatorio.");

            if (string.IsNullOrWhiteSpace(cliente.Estado))
                throw new ArgumentException("El estado es obligatorio.");

            try
            {
                return clientesDatos.MtdAgregarCliente(cliente);
            }
            catch
            {
                throw;
            }
        }

        /* =========================
           EDITAR CLIENTE
        ========================= */
        public string MtdEditarCliente(ClientesEntidad cliente)
        {
            if (cliente == null)
                throw new ArgumentNullException(nameof(cliente), "El cliente no puede ser nulo.");

            if (cliente.IdCliente <= 0)
                throw new ArgumentException("Debe seleccionar un cliente válido.");

            if (string.IsNullOrWhiteSpace(cliente.CodigoCliente))
                throw new ArgumentException("El código de cliente es obligatorio.");

            if (string.IsNullOrWhiteSpace(cliente.Nombre))
                throw new ArgumentException("El nombre es obligatorio.");

            if (string.IsNullOrWhiteSpace(cliente.Direccion))
                throw new ArgumentException("La dirección es obligatoria.");

            if (string.IsNullOrWhiteSpace(cliente.Correo))
                throw new ArgumentException("El correo es obligatorio.");

            if (string.IsNullOrWhiteSpace(cliente.Telefono))
                throw new ArgumentException("El teléfono es obligatorio.");

            if (string.IsNullOrWhiteSpace(cliente.Estado))
                throw new ArgumentException("El estado es obligatorio.");

            try
            {
                return clientesDatos.MtdEditarCliente(cliente);
            }
            catch
            {
                throw;
            }
        }

        /* =========================
           ELIMINAR CLIENTE
        ========================= */
        public string MtdEliminarCliente(int idCliente)
        {
            if (idCliente <= 0)
                throw new Exception("Debe seleccionar un cliente válido.");

            try
            {
                return clientesDatos.MtdEliminarCliente(idCliente);
            }
            catch
            {
                throw;
            }
        }

        /* =========================
           BUSCAR CLIENTE
        ========================= */
        public List<ClientesEntidad> MtdBuscarCliente(string cliente)
        {
            try
            {
                DataTable dt = clientesDatos.MtdBuscarCliente(cliente.Trim());

                List<ClientesEntidad> lista = new List<ClientesEntidad>();

                foreach (DataRow row in dt.Rows)
                {
                    ClientesEntidad clientes = new ClientesEntidad
                    {
                        IdCliente = Convert.ToInt32(row["IdCliente"]),
                        CodigoCliente = row["CodigoCliente"].ToString(),
                        Nombre = row["Nombre"].ToString(),
                        FechaAlta = Convert.ToDateTime(row["FechaAlta"]),
                        Direccion = row["Direccion"].ToString(),
                        Correo = row["Correo"].ToString(),
                        Telefono = row["Telefono"].ToString(),
                        Estado = row["Estado"].ToString()
                    };

                    lista.Add(clientes);
                }

                return lista;
            }
            catch
            {
                throw;
            }
        }
        public DataTable ListarParaCombo()
        {
            return objetoCD.ListarParaCombo();
        }
        public string MtdGenerarNuevoCodigo()
        {
            ClientesDatos datos = new ClientesDatos();
            int siguienteNumero = datos.MtdObtenerUltimoId() + 1;

            return "CLI" + siguienteNumero.ToString("D4");
        }
    }
}