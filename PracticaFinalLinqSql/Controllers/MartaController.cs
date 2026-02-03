using Microsoft.AspNetCore.Mvc;
using PracticaFinalLinqSql.Models;
using PracticaFinalLinqSql.Repositories;

namespace PracticaFinalLinqSql.Controllers
{
    public class MartaController : Controller
    {
        private RepositoryMarta repo;

        public MartaController()
        {
            this.repo = new RepositoryMarta();
        }

        public IActionResult Index()
        {
            List<Usuario> usuarios = this.repo.GetUsuarios();

            return View(usuarios);
        }

        public IActionResult Details(int id)
        {
            DetallesAlumno detalles = this.repo.GetDetails(id);

            return View(detalles);
        }
    }
}