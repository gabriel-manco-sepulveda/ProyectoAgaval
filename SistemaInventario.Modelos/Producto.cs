using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace SistemaInventario.Modelos
{
    public class Producto
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Número de serie es requerido")]
        [MaxLength(60, ErrorMessage = "Número de serie sólo acepta 60 caracteres")]
        public string NumeroSerie { get; set; }

        [Required(ErrorMessage = "La descripción es requerida")]
        public string Descripcion { get; set; }

        [Required(ErrorMessage = "El precio es requerido")]
        public double Precio { get; set; }

        [Required(ErrorMessage = "El costo es requerido")]
        public double Costo { get; set; }

        public string ImagenUrl { get; set; }

        [Required(ErrorMessage = "El estado es requerido")]
        public bool Estado { get; set; }

        [Required(ErrorMessage = "SeccionId es requerido")]
        public int SeccionId { get; set; }

        [ForeignKey("SeccionId")]
        public Seccion Seccion { get; set; }

        [Required(ErrorMessage = "ProveedorId es requerido")]
        public int ProveedorId { get; set; }

        [ForeignKey("ProveedorId")]
        public Proveedor Proveedor { get; set; }
        public int? PadreId { get; set; }

        public virtual Producto Padre { get; set; }
    }
}