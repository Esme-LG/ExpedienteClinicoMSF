using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ExpedienteClinicoMSF.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ExpedienteClinicoMSF.Controllers
{
    public class HomeController : Controller
    {
        private readonly expedienteContext _context;

        public HomeController(expedienteContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult SignUp()
        {
            ViewData["GeneroId"] = new SelectList(_context.Generos.ToList(), "GeneroId", "Genero");
            ViewData["EstadoCivilId"] = new SelectList(_context.EstadosCiviles.ToList(), "EstadoCivilId", "EstadoCivil");
            ViewData["PaisId"] = new SelectList(_context.Paises.ToList(), "PaisId", "Pais");
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
