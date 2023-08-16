using Microsoft.EntityFrameworkCore;
using SistemaInventario.AccesoDatos.Data;
using SistemaInventario.AccesoDatos.Repositorio.IRepositorio;
using SistemaInventario.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaInventario.AccesoDatos.Repositorio
{
    public class ProveedorRepositorio : Repositorio<Proveedor>, IProveedorRepositorio
    {
        private readonly ApplicationDbContext _db;
        public ProveedorRepositorio(ApplicationDbContext db):base(db)
        {
                _db = db;
        }

        public void Actualizar(Proveedor proveedor)
        {
            var proveedorBD = _db.Proveedors.FirstOrDefault(b=>b.Id == proveedor.Id);
            if (proveedorBD != null)
            {
                proveedorBD.Nombre = proveedor.Nombre;
                proveedorBD.Descripcion = proveedor.Descripcion;
                proveedorBD.Estado = proveedor.Estado;
                _db.SaveChanges();
            }
        }
    }
}
