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
    public class ClienteController : Controller
    {
        private readonly IUnidadTrabajo _unidadTrabajo;

        public ClienteController(IUnidadTrabajo unidadTrabajo)
        {
            _unidadTrabajo = unidadTrabajo;
        }
        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> Upsert(int? id)
        {
            Cliente cliente = new Cliente();
            if (id == null)
            {
                //Crear nuevo cliente
                cliente.Estado = true;
                return View(cliente);
            }
            //Actualizamos cliente
            cliente = await _unidadTrabajo.Cliente.Obtener(id.GetValueOrDefault());
            if(cliente == null)
            {
                return NotFound();
            }
            return View(cliente);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Upsert(Cliente cliente)
        {
            if (ModelState.IsValid)
            {
                if (cliente.Id == 0)
                {
                    await _unidadTrabajo.Cliente.Agregar(cliente);
                    TempData[DefinicionesEstaticas.Exitosa] = "Cliente creado exitosamente";
                }
                else
                {
                    _unidadTrabajo.Cliente.Actualizar(cliente);
                    TempData[DefinicionesEstaticas.Exitosa] = "Cliente actualizado correctamente";
                }
                await _unidadTrabajo.Guardar();
                return RedirectToAction(nameof(Index)); 
            }
            TempData[DefinicionesEstaticas.Error] = "Error al grabar Cliente";
            return View(cliente);
        }

        #region API

        [HttpGet]

        public async Task<IActionResult> ObtenerTodos()
        {
            var todos = await _unidadTrabajo.Cliente.ObtenerTodos();
            return Json(new { data = todos });
        }

        [HttpPost]

        public async Task<IActionResult> Delete(int id)
        {
            var clienteDb = await _unidadTrabajo.Cliente.Obtener(id);
            if(clienteDb == null)
            {
                return Json(new { success = false, mesagge = "Error al borrar cliente" });
            }
            _unidadTrabajo.Cliente.Remover(clienteDb);
            await _unidadTrabajo.Guardar();
            return Json(new { success = true, message = "Cliente borrado exitosamente" });
        }

        [ActionName("ValidarNombre")]
        public async Task<IActionResult> ValidarNombre(string nombre, int id = 0)
        {
            bool valor = false;
            var lista = await _unidadTrabajo.Cliente.ObtenerTodos();
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
