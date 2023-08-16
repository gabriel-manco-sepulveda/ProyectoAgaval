using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaInventario.AccesoDatos.Repositorio.IRepositorio
{
    public interface IUnidadTrabajo:IDisposable
    {
        IClienteRepositorio Cliente { get; }
        ISeccionRepositorio Seccion { get; }
        IProveedorRepositorio Proveedor { get; }
        IProductoRepositorio Producto { get; }
        Task Guardar();
    }
}
