using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SistemaInventario.AccesoDatos.Repositorio.IRepositorio;
using SistemaInventario.Modelos;
using SistemaInventario.Utilidades;
using System.Data;

namespace SistemaInventario.Areas.Admin.Controllers
{
    [Area("Admin")]
    
    public class ProveedorController : Controller
    {

        private readonly IUnidadTrabajo _unidadTrabajo;

        public ProveedorController(IUnidadTrabajo unidadTrabajo)
        {
            _unidadTrabajo = unidadTrabajo;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> Upsert(int? id)
        {
            Proveedor proveedor = new Proveedor();

            if (id == null)
            {
                // Crear una nuevo Proveedor
                proveedor.Estado = true;
                return View(proveedor);
            }
            // Actualizamos proveedor
            proveedor = await _unidadTrabajo.Proveedor.Obtener(id.GetValueOrDefault());
            if (proveedor == null)
            {
                return NotFound();
            }
            return View(proveedor);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Upsert(Proveedor proveedor)
        {
            if (ModelState.IsValid)
            {
                if (proveedor.Id == 0)
                {
                    await _unidadTrabajo.Proveedor.Agregar(proveedor);
                    TempData[DefinicionesEstaticas.Exitosa] = "proveedor creado Exitosamente";
                }
                else
                {
                    _unidadTrabajo.Proveedor.Actualizar(proveedor);
                    TempData[DefinicionesEstaticas.Exitosa] = "Proveedor actualizado Exitosamente";
                }
                await _unidadTrabajo.Guardar();
                return RedirectToAction(nameof(Index));
            }
            TempData[DefinicionesEstaticas.Error] = "Error al grabar al grabar proveedor";
            return View(proveedor);
        }


        #region API

        [HttpGet]
        public async Task<IActionResult> ObtenerTodos()
        {
            var todos = await _unidadTrabajo.Proveedor.ObtenerTodos();
            return Json(new { data = todos });
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var proveedorDb = await _unidadTrabajo.Proveedor.Obtener(id);
            if (proveedorDb == null)
            {
                return Json(new { success = false, message = "Error al borrar Proveedor" });
            }
            _unidadTrabajo.Proveedor.Remover(proveedorDb);
            await _unidadTrabajo.Guardar();
            return Json(new { success = true, message = "Proveedor borrado exitosamente" });
        }

        [ActionName("ValidarNombre")]
        public async Task<IActionResult> ValidarNombre(string nombre, int id = 0)
        {
            bool valor = false;
            var lista = await _unidadTrabajo.Proveedor.ObtenerTodos();
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