using Microsoft.EntityFrameworkCore;
using SistemaInventario.AccesoDatos.Data;
using SistemaInventario.AccesoDatos.Repositorio.IRepositorio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace SistemaInventario.AccesoDatos.Repositorio
{
    public class Repositorio<T> : IRepositorio<T> where T : class
    {
        private readonly ApplicationDbContext _db;
        internal DbSet<T> dbSet;
        public Repositorio(ApplicationDbContext db)
        {
            _db = db;
            this.dbSet = _db.Set<T>();
        }
        public async Task Agregar(T Entidad)
        {
            await dbSet.AddAsync(Entidad);  //insert into table
        }

        public async Task<T> Obtener(int id)
        {
            return await dbSet.FindAsync(id); //select * from (solo por id)
        }

        public async Task<IEnumerable<T>> ObtenerTodos(Expression<Func<T, bool>> filtro = null, Func<IQueryable<T>,
                          IOrderedQueryable<T>> orderby = null, string IncluirPropiedades = null, bool istracking = true)
        {
            IQueryable<T> query = dbSet;
            if (filtro !=null)
            {
                query = query.Where(filtro); // Select from* where
            }

            if(IncluirPropiedades != null)
            {
                foreach (var IncluirProp in IncluirPropiedades.Split(new char[] { ','}, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(IncluirProp); // Ejemplo "categoria.marca"
                }
            }

            if(orderby != null)
            {
                query = orderby(query);
            }

            if(!istracking)
            {
                query = query.AsNoTracking();
            }

            return await query.ToListAsync();
        }

        public async Task<T> ObtenerPrimero(Expression<Func<T, bool>> filtro = null, 
               string IncluirPropiedades = null, bool istracking = true)
        {
            IQueryable<T> query = dbSet;
            if (filtro != null)
            {
                query = query.Where(filtro); // Select from* where
            }

            if (IncluirPropiedades != null)
            {
                foreach (var IncluirProp in IncluirPropiedades.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(IncluirProp); // Ejemplo "categoria.marca"
                }
            }

            if (!istracking)
            {
                query = query.AsNoTracking();
            }

            return await query.FirstOrDefaultAsync();
        }

        public void Remover(T Entidad)
        {
            dbSet.Remove(Entidad);
        }

        public void RemoverRango(IEnumerable<T> Entidad)
        {
            dbSet.RemoveRange(Entidad);
        }
    }
}
