using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace SistemaInventario.Modelos
{
    public class Cliente
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage ="El nombre es requerido")]
        [MaxLength(60, ErrorMessage ="El nombre sólo permite 60 caracteres")]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "El apellido es requerido")]
        [MaxLength(60, ErrorMessage = "El apellido sólo permite 60 caracteres")]
        public string Apellido { get; set; }

        [Required(ErrorMessage ="El documento es requerida")]
        [MaxLength(100, ErrorMessage ="El documento sólo permite 100 caracteres")]
        public string Documento { get; set; }
        public string Persona { get; set; }

        [Required(ErrorMessage ="El estado es requerido")]
        public bool Estado { get; set; }
    }
}
