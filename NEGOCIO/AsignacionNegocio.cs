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
    public class AsignacionNegocio
    {
        AsignacionDatos asignacionDatos = new AsignacionDatos();

        /* ---- CONSULTAR ---- */
        public List<AsignacionEntidad> MtdConsultar()
        {
            try
            {
                DataTable dt = asignacionDatos.MtdConsultar();

                List<AsignacionEntidad> lista = new List<AsignacionEntidad>();

                foreach (DataRow row in dt.Rows)
                {
                    AsignacionEntidad asignacion = new AsignacionEntidad
                    {
                        IdClienteServicio = Convert.ToInt32(row["IdClienteServicio"]),
                        Cliente = row["Cliente"].ToString(),
                        NombreServicio = row["NombreServicio"].ToString(),
                        DireccionInstalacion = row["DireccionInstalacion"].ToString(),
                        FechaInstalacion = Convert.ToDateTime(row["FechaInstalacion"]),
                        Estado = row["Estado"].ToString()
                    };

                    lista.Add(asignacion);
                }

                return lista;
            }
            catch
            {
                throw;
            }
        }

        /* ---- AGREGAR ---- */
        public string MtdAgregarAsignacion(AsignacionEntidad asignacion)
        {
            if (asignacion == null)
                throw new Exception("No se recibieron datos");

            if (asignacion.IdCliente <= 0)
                throw new Exception("Debe seleccionar un cliente");

            if (asignacion.IdServicio <= 0)
                throw new Exception("Debe seleccionar un servicio");

            if (string.IsNullOrWhiteSpace(asignacion.DireccionInstalacion))
                throw new Exception("Debe ingresar una dirección");

            if (string.IsNullOrWhiteSpace(asignacion.Estado))
                throw new Exception("Debe seleccionar un estado");

            try
            {
                return asignacionDatos.MtdAgregarAsignacion(asignacion);
            }
            catch
            {
                throw;
            }
        }

        /* ---- EDITAR ---- */
        public string MtdEditarAsignacion(AsignacionEntidad asignacion)
        {
            if (asignacion == null)
                throw new Exception("No se recibieron datos");

            if (asignacion.IdClienteServicio <= 0)
                throw new Exception("Debe seleccionar una asignación válida");

            if (asignacion.IdCliente <= 0)
                throw new Exception("Debe seleccionar un cliente");

            if (asignacion.IdServicio <= 0)
                throw new Exception("Debe seleccionar un servicio");

            if (string.IsNullOrWhiteSpace(asignacion.DireccionInstalacion))
                throw new Exception("Debe ingresar una dirección");

            if (string.IsNullOrWhiteSpace(asignacion.Estado))
                throw new Exception("Debe seleccionar un estado");

            try
            {
                return asignacionDatos.MtdEditarAsignacion(asignacion);
            }
            catch
            {
                throw;
            }
        }

        /* ---- ELIMINAR ---- */
        public string MtdEliminarAsignacion(int idClienteServicio)
        {
            if (idClienteServicio <= 0)
                throw new Exception("Debe seleccionar una asignación válida");

            try
            {
                return asignacionDatos.MtdEliminarAsignacion(idClienteServicio);
            }
            catch
            {
                throw;
            }
        }

        /* ---- BUSCAR ---- */
        public List<AsignacionEntidad> MtdBuscarAsignacion(string cliente)
        {
            try
            {
                DataTable dt = asignacionDatos.MtdBuscarAsignacion(cliente.Trim());

                List<AsignacionEntidad> lista = new List<AsignacionEntidad>();

                foreach (DataRow row in dt.Rows)
                {
                    AsignacionEntidad asignacion = new AsignacionEntidad
                    {
                        IdClienteServicio = Convert.ToInt32(row["IdClienteServicio"]),
                        Cliente = row["Cliente"].ToString(),
                        NombreServicio = row["NombreServicio"].ToString(),
                        DireccionInstalacion = row["DireccionInstalacion"].ToString(),
                        FechaInstalacion = Convert.ToDateTime(row["FechaInstalacion"]),
                        Estado = row["Estado"].ToString()
                    };

                    lista.Add(asignacion);
                }

                return lista;
            }
            catch
            {
                throw;
            }
        }
    }
}