using Microsoft.AspNetCore.Mvc;
using Microsoft.DotNet.Scaffolding.Shared.Messaging;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using SistemaInventario.AccesoDatos.Repositorio;
using SistemaInventario.AccesoDatos.Repositorio.IRepositorio;
using SistemaInventario.Modelos;
using SistemaInventario.Utilidades;

namespace SistemaInventario.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class SeccionController : Controller
    {
        private readonly IUnidadTrabajo _unidadTrabajo;

        public SeccionController(IUnidadTrabajo unidadTrabajo)
        {
            _unidadTrabajo = unidadTrabajo;
        }
        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> Upsert(int? id)
        {
            Seccion seccion = new Seccion();
            if (id == null)
            {
                //Crear una nueva sección
                seccion.Estado = true;
                return View(seccion);
            }
            //Actualizamos la sección
            seccion = await _unidadTrabajo.Seccion.Obtener(id.GetValueOrDefault());
            if(seccion == null)
            {
                return NotFound();
            }
            return View(seccion);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Upsert(Seccion seccion)
        {
            if (ModelState.IsValid)
            {
                if (seccion.Id == 0)
                {
                    await _unidadTrabajo.Seccion.Agregar(seccion);
                    TempData[DefinicionesEstaticas.Exitosa] = "Sección creada exitosamente";
                }
                else
                {
                    _unidadTrabajo.Seccion.Actualizar(seccion);
                    TempData[DefinicionesEstaticas.Exitosa] = "Sección actualizada correctamente";
                }
                await _unidadTrabajo.Guardar();
                return RedirectToAction(nameof(Index)); 
            }
            TempData[DefinicionesEstaticas.Error] = "Error al grabar la sección";
            return View(seccion);
        }

        #region API

        [HttpGet]

        public async Task<IActionResult> ObtenerTodos()
        {
            var todos = await _unidadTrabajo.Seccion.ObtenerTodos();
            return Json(new { data = todos });
        }

        [HttpPost]

        public async Task<IActionResult> Delete(int id)
        {
            var seccionDb = await _unidadTrabajo.Seccion.Obtener(id);
            if(seccionDb == null)
            {
                return Json(new { success = false, mesagge = "Error al borrar la seccion" });
            }
            _unidadTrabajo.Seccion.Remover(seccionDb);
            await _unidadTrabajo.Guardar();
            return Json(new { success = true, message = "Seccion borrada exitosamente" });
        }

        [ActionName("ValidarNombre")]
        public async Task<IActionResult> ValidarNombre(string nombre, int id = 0)
        {
            bool valor = false;
            var lista = await _unidadTrabajo.Seccion.ObtenerTodos();
            if (id == 0)
            {
                valor = lista.Any(b => b.Nombre.ToLower().Trim() == nombre.ToLower().Trim());
            }
            else
            {
                valor = lista.Any(b => b.Nombre.ToLower().Trim() == nombre.ToLower().Trim() && b.Id != id);
            }
            if (valor)
            {
                return Json(new { data = true });
            }
            return Json(new { data = false });

        }

        #endregion
    }
}
