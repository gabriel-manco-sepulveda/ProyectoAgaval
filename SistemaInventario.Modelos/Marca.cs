using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaInventario.Modelos
{
    public class Marca
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage ="El nombre es requerido")]
        [MaxLength(60, ErrorMessage ="Máximo 60 caracteres para el nombre")]
        public string Nombre { get; set; }

        [Required(ErrorMessage ="La descripción es requerida")]
        [MaxLength(100, ErrorMessage ="Máximo 100 caracteres para la descripción")]
        public string Descripcion { get; set; }

        [Required(ErrorMessage ="El estado es requerido")]
        public bool Estado { get; set; }
    }
}
