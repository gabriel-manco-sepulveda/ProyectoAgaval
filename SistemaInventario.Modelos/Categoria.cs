﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaInventario.Modelos
{
    public class Categoria
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage ="El nombre es requerido")]
        [MaxLength(60,ErrorMessage ="Nombre permite máximo 60 caracteres")]
        public string Nombre { get; set; }

        [Required(ErrorMessage ="La descripción es requerida")]
        [MaxLength(100, ErrorMessage ="La descripción permite 100 caracteres")]
        public string Descripcion { get; set; }

        [Required(ErrorMessage ="El estado es requerido")]
        public bool Estado { get; set; }
    }
}