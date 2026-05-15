using DATOS;
using MODELO;
using System;
using System.Collections.Generic;
using System.Data;

namespace NEGOCIO
{
    public class ServiciosNegocio
    {
        ServiciosDatos serviciosDatos = new ServiciosDatos();
        private ServiciosDatos objetoCD = new ServiciosDatos();

        /* ----- CONSULTAR ----- */
        public List<ServiciosEntidad> MtdConsultar()
        {
            try
            {
                DataTable dt = serviciosDatos.MtdConsultar();

                List<ServiciosEntidad> lista = new List<ServiciosEntidad>();

                foreach (DataRow row in dt.Rows)
                {
                    ServiciosEntidad servicio = new ServiciosEntidad
                    {
                        IdServicio = Convert.ToInt32(row["IdServicio"]),
                        NombreServicio = row["NombreServicio"].ToString(),
                        TipoServicio = row["TipoServicio"].ToString(),

                        VelocidadMbps = row["VelocidadMbps"] == DBNull.Value
                            ? 0
                            : Convert.ToInt32(row["VelocidadMbps"]),

                        TipoCable = row["TipoCable"] == DBNull.Value
                            ? ""
                            : row["TipoCable"].ToString(),

                        CantidadCanales = row["CantidadCanales"] == DBNull.Value
                            ? 0
                            : Convert.ToInt32(row["CantidadCanales"]),

                        PrecioMensual = Convert.ToDecimal(row["PrecioMensual"]),

                        Estado = row["Estado"].ToString()
                    };

                    lista.Add(servicio);
                }

                return lista;
            }
            catch
            {
                throw;
            }
        }

        /* ----- AGREGAR ----- */
        public string MtdAgregarServicio(ServiciosEntidad servicio)
        {
            if (servicio == null)
                throw new ArgumentNullException(nameof(servicio), "El servicio no puede ser nulo.");

            if (string.IsNullOrWhiteSpace(servicio.NombreServicio))
                throw new ArgumentException("El nombre del servicio no puede estar vacío.");

            if (string.IsNullOrWhiteSpace(servicio.TipoServicio))
                throw new ArgumentException("El tipo de servicio no puede estar vacío.");

            if (servicio.PrecioMensual <= 0)
                throw new ArgumentException("El precio mensual debe ser mayor a cero.");

            try
            {
                return serviciosDatos.MtdAgregarServicio(servicio);
            }
            catch
            {
                throw;
            }
        }

        /* ----- EDITAR ----- */
        public string MtdEditarServicio(ServiciosEntidad servicio)
        {
            if (servicio == null)
                throw new ArgumentNullException(nameof(servicio), "El servicio no puede ser nulo.");

            if (servicio.IdServicio <= 0)
                throw new ArgumentException("Debe seleccionar un servicio válido.");

            if (string.IsNullOrWhiteSpace(servicio.NombreServicio))
                throw new ArgumentException("El nombre del servicio no puede estar vacío.");

            if (string.IsNullOrWhiteSpace(servicio.TipoServicio))
                throw new ArgumentException("El tipo de servicio no puede estar vacío.");

            if (servicio.PrecioMensual <= 0)
                throw new ArgumentException("El precio mensual debe ser mayor a cero.");

            try
            {
                return serviciosDatos.MtdEditarServicio(servicio);
            }
            catch
            {
                throw;
            }
        }

        /* ----- ELIMINAR ----- */
        public string MtdEliminarServicio(int idServicio)
        {
            if (idServicio <= 0)
                throw new Exception("Debe seleccionar un servicio válido.");

            try
            {
                return serviciosDatos.MtdEliminarServicio(idServicio);
            }
            catch
            {
                throw;
            }
        }

        /* ----- BUSCAR ----- */
        public List<ServiciosEntidad> MtdBuscarServicio(string tipoServicio)
        {
            try
            {
                DataTable resultado = serviciosDatos.MtdBuscarServicio(tipoServicio.Trim());

                List<ServiciosEntidad> lista = new List<ServiciosEntidad>();

                foreach (DataRow row in resultado.Rows)
                {
                    ServiciosEntidad servicio = new ServiciosEntidad
                    {
                        IdServicio = Convert.ToInt32(row["IdServicio"]),
                        NombreServicio = row["NombreServicio"].ToString(),
                        TipoServicio = row["TipoServicio"].ToString(),

                        VelocidadMbps = row["VelocidadMbps"] == DBNull.Value
                            ? 0
                            : Convert.ToInt32(row["VelocidadMbps"]),

                        TipoCable = row["TipoCable"] == DBNull.Value
                            ? ""
                            : row["TipoCable"].ToString(),

                        CantidadCanales = row["CantidadCanales"] == DBNull.Value
                            ? 0
                            : Convert.ToInt32(row["CantidadCanales"]),

                        PrecioMensual = Convert.ToDecimal(row["PrecioMensual"]),

                        Estado = row["Estado"].ToString()
                    };

                    lista.Add(servicio);
                }

                return lista;
            }
            catch (Exception)
            {
                throw;
            }
        }
        
        public DataTable ListarServiciosCombo()
        {
            return objetoCD.ListarServiciosCombo();
        }
    }
}