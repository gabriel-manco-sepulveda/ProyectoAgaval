using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace SistemaInventario.Modelos
{
    public class Bodega
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage ="El nombre es requerido")]
        public string Nombre { get; set; }
        [Required(ErrorMessage ="La descripción es requerida")]
        public string Descripcion { get; set; }
        [Required(ErrorMessage ="El estado es requerido")]
        public bool Estado { get; set; }
    }
}
