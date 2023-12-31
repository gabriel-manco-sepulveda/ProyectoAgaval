﻿using SistemaInventario.AccesoDatos.Data;
using SistemaInventario.AccesoDatos.Repositorio.IRepositorio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaInventario.AccesoDatos.Repositorio
{
    public class UnidadTrabajo : IUnidadTrabajo
    {
        private readonly ApplicationDbContext _db;
        public IClienteRepositorio Cliente { get; private set; }
        public ISeccionRepositorio Seccion { get; private set; }
        public IProveedorRepositorio Proveedor { get; private set; }
        public IProductoRepositorio Producto { get; private set; }
        public UnidadTrabajo(ApplicationDbContext db)
        {
            _db = db;
            Cliente = new ClienteRepositorio(_db);
            Seccion = new SeccionRepositorio(_db);
            Proveedor = new ProveedorRepositorio(_db);
            Producto = new ProductoRepositorio(_db);
        }

        public void Dispose()
        {
            _db.Dispose();

        }

        public async Task Guardar()
        {
            await _db.SaveChangesAsync();
        }
    }
}