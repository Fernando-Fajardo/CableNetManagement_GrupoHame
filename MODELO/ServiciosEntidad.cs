using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MODELO
{
    public class ServiciosEntidad
    {
        [Key]
        [Required]
        public int IdServicio { get; set; }
        [Required]
        public string NombreServicio { get; set; }
        [Required]
        public string TipoServicio { get; set; }
        public int VelocidadMbps { get; set; }
        public string TipoCable { get; set; }
        public int CantidadCanales { get; set; }
        [Required]
        public decimal PrecioMensual { get; set; }
        [Required]
        public string Estado { get; set; }
    }
}