using SistemaInventario.Modelos.Especificaciones;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace SistemaInventario.AccesoDatos.Repositorio.IRepositorio
{
    public interface IRepositorio<T> where T:class
    {
        Task<T> Obtener(int id);

        Task<IEnumerable<T>> ObtenerTodos(
            Expression<Func<T, bool>> filtro = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderby = null,
            string IncluirPropiedades = null,
            bool istracking = true
            );

        PagedList<T> ObtenerTodosPaginado(Parametros parametros, Expression<Func<T, bool>> filtro = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderby = null,
            string IncluirPropiedades = null,
            bool istracking = true
            );

        Task<T> ObtenerPrimero(
            Expression<Func<T, bool>> filtro = null,
            string IncluirPropiedades = null,
            bool istracking = true
            );

        Task Agregar(T Entidad);

        void Remover(T Entidad);

        void RemoverRango(IEnumerable<T> Entidad);
    }
}
