using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaInventario.Modelos.ViewModel
{
    public class ProductoVM
    {
        public Producto Producto { get; set; }

        public IEnumerable<SelectListItem> ProveedorLista {  get; set; }
        public IEnumerable<SelectListItem> SeccionLista { get; set; }
        public IEnumerable<SelectListItem> PadreLista { get; set; }
    }
}
