using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MODELO
{
    public class ClientesEntidad
    {
        [Key]
        [Required]
        public int IdCliente { get; set; }

        [Required]
        [StringLength(20)]
        public string CodigoCliente { get; set; }

        [Required]
        [StringLength(150)]
        public string Nombre { get; set; }

        [Required]
        public DateTime FechaAlta { get; set; }

        [Required]
        [StringLength(250)]
        public string Direccion { get; set; }

        [Required]
        [StringLength(100)]
        public string Correo { get; set; }

        [Required]
        [StringLength(20)]
        public string Telefono { get; set; }

        [Required]
        [StringLength(20)]
        public string Estado { get; set; }
    }
}
