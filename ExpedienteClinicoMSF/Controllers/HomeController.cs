using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ExpedienteClinicoMSF.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Security.Cryptography;
using System.Text;
using Microsoft.AspNetCore.Http;

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

        // GET: SignUp
        public IActionResult SignUp()
        {
            ViewData["GeneroId"] = new SelectList(_context.Generos.ToList(), "GeneroId", "Genero");
            ViewData["EstadoCivilId"] = new SelectList(_context.EstadosCiviles.ToList(), "EstadoCivilId", "EstadoCivil");
            ViewData["PaisId"] = new SelectList(_context.Paises.ToList(), "PaisId", "Pais");
            ViewData["RegionId"] = new SelectList(_context.Regiones.Where(x => x.RegRegionId == null).ToList(), "RegionId", "Region");
            ViewData["SubRegionId"] = new SelectList(_context.Regiones.Where(x => x.RegRegionId != null).ToList(), "RegionId", "Region");
            return View();
        }

        // POST: SignUp
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SignUp(Usuarios usuario)
        {
            System.Diagnostics.Debug.WriteLine("##### REGISTRAR ###################################3");
            if (ModelState.IsValid)
            {
                
                //_context.Add(persona);
                System.Diagnostics.Debug.WriteLine(usuario.EstadoCivil.EstadoCivil);
                System.Diagnostics.Debug.WriteLine("########################################");
                // _context.SaveChangesAsync();
                //return RedirectToAction(nameof(Index));
            }

            return View();
        }

        public static string EncryptPassword(string data)
        {
            SHA1 sha = SHA1.Create();
            Byte[] hash = sha.ComputeHash(Encoding.Default.GetBytes(data));
            StringBuilder valor = new StringBuilder();
            int i;

            for (i = 0; i < hash.Length - 1; i++)
            {
                valor.Append(hash[i].ToString());
            }

            return valor.ToString();
        }

        public static bool DecryptPassword(string data, string contra)
        {
            return EncryptPassword(contra) == data;
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
