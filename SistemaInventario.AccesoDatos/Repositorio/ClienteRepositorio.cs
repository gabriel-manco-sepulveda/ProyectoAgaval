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
    public class ClienteRepositorio : Repositorio<Cliente>, IClienteRepositorio
    {
        private readonly ApplicationDbContext _db;
        public ClienteRepositorio(ApplicationDbContext db):base(db)
        {
                _db = db;
        }

        public void Actualizar(Cliente cliente)
        {
            var clienteBD = _db.Clientes.FirstOrDefault(b=>b.Id == cliente.Id);
            if (clienteBD != null)
            {
                clienteBD.Nombre = cliente.Nombre;
                clienteBD.Documento = cliente.Documento;
                clienteBD.Estado = cliente.Estado;
                _db.SaveChanges();
            }
        }
    }
}
