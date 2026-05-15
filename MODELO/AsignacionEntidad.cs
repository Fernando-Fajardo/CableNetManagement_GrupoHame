using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MODELO
{
    public class AsignacionEntidad
    {
        [Key]
        [Required]
        public int IdClienteServicio { get; set; }
        [Required]
        public int IdCliente { get; set; }
        [Required]
        public int IdServicio { get; set; }
        [Required]
        public string DireccionInstalacion { get; set; }
        [Required]
        public DateTime FechaInstalacion { get; set; }
        [Required]
        public string Estado { get; set; }
        public string Cliente { get; set; }

        public string NombreServicio { get; set; }
    }
}