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
using Microsoft.EntityFrameworkCore;
using System.Data.SqlClient;

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
        public async Task<IActionResult> SignUp(Usuarios usuario, IFormCollection form)
        {
            String firstname = form["f1firstname"]; ;
            String secondname = form["f1secondname"];
            String lastname1 = form["f1lastname1"];
            String lastname2 = form["f1lastname2"];
            String apellidocasada = form["f1apellidocasada"];
            String tel = form["f1-tel"];
            String gen =  form["f1-gen"];
            String estcivil = form["f1-est-civil"];
            String fechanacimiento = form["f1-fecha-nacimiento"];
            String pais = form["f1-pais"];
            String region = form["f1-region"];
            String subregion =  form["f1-subregion"];
            String ciudad = form["f1-ciudad"];
            String calle = form["f1-calle"];
            String casa = form["f1-casa"];
            String hospital = form["f1-hospital"];
            String durconsulta = form["f1-dur-consulta"];
            String paish = form["f1-pais-h"];
            String regionh = form["f1-region-h"];
            String subregionh =  form["f1-subregion-h"];
            String ciudadh = form["f1-ciudad-h"];
            String calleh = form["f1-calle-h"];
            String casah = form["f1-casa-h"];
            String email = form["f1-email"];
            String password = form["f1-password"];
            password = EncryptPassword(password);

            var x = _context.Database.ExecuteSqlCommand("spResgistrarUsuario @p0, @p1, @p2, @p3, @p4, @p5, @p6, @p7, @p8, @p9, @p10, @p11, @p12, @p13, @p14, @p15, @p16, @p17, @p18, @p19, @p20, @p21, @p22, @p23, @p24", parameters: new[] { firstname, secondname, lastname1, lastname2, apellidocasada, fechanacimiento, pais, ciudad, calle, casa, region, subregion, hospital, durconsulta, paish, ciudadh, calleh, casah, regionh, subregionh, email, password,estcivil,gen, tel});
            System.Diagnostics.Debug.WriteLine(x);
            return View("Index");
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
