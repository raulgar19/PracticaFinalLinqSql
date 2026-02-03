using Microsoft.AspNetCore.Mvc;
using PracticaFinalLinqSql.Models;
using PracticaFinalLinqSql.Repositories;

namespace PracticaFinalLinqSql.Controllers
{
    public class PlantillaController : Controller
    {
        private RepositoryPlantilla repo;

        public PlantillaController()
        {
            this.repo = new RepositoryPlantilla();
        }

        public IActionResult Index()
        {
            List<Plantilla> plantilla = this.repo.GetPlantilla();

            return View(plantilla);
        }

        public async Task<IActionResult> Buscador()
        {
            List<string> funciones = this.repo.GetFunciones();

            ViewData["FUNCIONES"] = funciones;

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Buscador(string funcion)
        {
            List<Plantilla> plantilla = this.repo.GetPlantillaFuncion(funcion);

            List<string> funciones = this.repo.GetFunciones();

            ViewData["FUNCIONES"] = funciones;

            return View(plantilla);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Plantilla plantilla)
        {
            await this.repo.Create(plantilla.HospitalCod, plantilla.SalaCod, plantilla.EmpleadoNo, plantilla.Apellido, plantilla.Funcion, plantilla.Turno, plantilla.Salario);

            return RedirectToAction("Index");
        }

        public IActionResult Update(int idEmpleado)
        {
            Plantilla plantilla = this.repo.GetPlantillaEmpleado(idEmpleado);

            return View(plantilla);
        }

        [HttpPost]
        public async Task<IActionResult> Update(Plantilla plantilla)
        {
            await this.repo.Update(plantilla.HospitalCod, plantilla.SalaCod, plantilla.EmpleadoNo, plantilla.Apellido, plantilla.Funcion, plantilla.Turno, plantilla.Salario);

            return RedirectToAction("Index");
        }

        public async Task<ActionResult> DeleteAsync(int idEmpleado)
        {
            await this.repo.Delete(idEmpleado);
            return RedirectToAction("Index");
        }
    }
}