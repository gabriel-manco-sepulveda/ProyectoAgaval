﻿using Microsoft.AspNetCore.Mvc.Rendering;
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
    internal class ProductoRepositorio : Repositorio<Producto>, IProductoRepositorio
    {
        private readonly ApplicationDbContext _db;

        public ProductoRepositorio(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Actualizar(Producto producto)
        {
            var productoDB = _db.Productos.FirstOrDefault(b => b.Id == producto.Id);
            if (productoDB != null)
            {
                if (producto.ImagenUrl != null)
                {
                    productoDB.ImagenUrl = producto.ImagenUrl;
                }

                productoDB.NumeroSerie = producto.NumeroSerie;
                productoDB.Descripcion = producto.Descripcion;
                productoDB.Padre = producto.Padre;
                productoDB.CategoriaId = producto.CategoriaId;
                productoDB.Costo = producto.Costo;
                productoDB.Precio = producto.Precio;
                productoDB.MarcaId = producto.MarcaId;
                productoDB.Estado = producto.Estado;

                _db.SaveChanges();
            }
        }

        public IEnumerable<SelectListItem> ObtenerTodosDropdownLista(string Obj)
        {
            if (Obj == "Categoria")
            {
                return _db.Categorias.Where(c => c.Estado == true).Select(c => new SelectListItem
                {
                    Text = c.Nombre,
                    Value = c.Id.ToString()
                });
            }

            if (Obj == "Marca")
            {
                return _db.Marcas.Where(c => c.Estado == true).Select(c => new SelectListItem
                {
                    Text = c.Nombre,
                    Value = c.Id.ToString()
                });
            }

            if (Obj == "Producto")
            {
                return _db.Productos.Where(c => c.Estado == true).Select(c => new SelectListItem
                {
                    Text = c.Descripcion,
                    Value = c.Id.ToString()
                });
            }
            return null;

        }
    }
}
