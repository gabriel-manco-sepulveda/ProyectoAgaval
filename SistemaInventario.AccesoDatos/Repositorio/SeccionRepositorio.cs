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
    public class SeccionRepositorio : Repositorio<Seccion>, ISeccionRepositorio
    {
        private readonly ApplicationDbContext _db;
        public SeccionRepositorio(ApplicationDbContext db):base(db)
        {
                _db = db;
        }

        public void Actualizar(Seccion seccion)
        {
            var seccionBD = _db.Seccions.FirstOrDefault(b=>b.Id == seccion.Id);
            if (seccionBD != null)
            {
                seccionBD.Nombre = seccion.Nombre;
                seccionBD.Descripcion = seccion.Descripcion;
                seccionBD.Estado = seccion.Estado;
                _db.SaveChanges();
            }
        }
    }
}
