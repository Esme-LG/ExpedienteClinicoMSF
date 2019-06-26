using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ExpedienteClinicoMSF.Models;
using Microsoft.AspNetCore.Http;

namespace ExpedienteClinicoMSF.Controllers
{
    public class ExpedientesController : Controller
    {
        private readonly expedienteContext _context;

        public ExpedientesController(expedienteContext context)
        {
            _context = context;
        }

        // GET: Expedientes
        public async Task<IActionResult> Index()
        {
            var expedienteContext = _context.Expedientes.Include(e => e.Direccion).Include(e => e.EstadoCivil).Include(e => e.Genero).Include(e => e.Paciente).ThenInclude(e => e.Persona);
            return View(await expedienteContext.ToListAsync());
        }

        // GET: Expedientes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var expedientes = await _context.Expedientes
                .Include(e => e.EstadoCivil)
                .Include(e => e.Genero)
                .Include(e => e.Paciente)
                .Include(e => e.Direccion)
                .Include(e => e.EstadoCivil)
                .Include(e => e.Genero)
                .Include(e => e.Paciente)
                .FirstOrDefaultAsync(m => m.ExpedienteId == id);
            if (expedientes == null)
            {
                return NotFound();
            }

            return View(expedientes);
        }

        // GET: Expedientes/Create
        public IActionResult Create()
        {
            ViewData["EstadoCivilId"] = new SelectList(_context.EstadosCiviles, "EstadoCivilId", "EstadoCivil");
            ViewData["GeneroId"] = new SelectList(_context.Generos, "GeneroId", "Genero");
            ViewData["PaisId"] = new SelectList(_context.Paises.ToList(), "PaisId", "Pais");
            /*ViewData["GeneroId"] = new SelectList(_context.Generos.ToList(), "GeneroId", "Genero");
            ViewData["EstadoCivilId"] = new SelectList(_context.EstadosCiviles.ToList(), "EstadoCivilId", "EstadoCivil");*/
           // ViewData["PaisId"] = new SelectList(_context.Paises, "PaisId", "Pais");
            ViewData["RegionId"] = new SelectList(_context.Regiones.Where(x => x.RegRegionId == null).ToList(), "RegionId", "Region");
            ViewData["SubRegionId"] = new SelectList(_context.Regiones.Where(x => x.RegRegionId != null).ToList(), "RegionId", "Region");
            return View();
        }

        // POST: Expedientes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind] Expedientes expedientes, IFormCollection form)
        {

            String tDeclaracion = "Insert";
            String expedienteid = null;
            String firstname = form["f1-firstname"];
            String secondname = form["f1-secondname"];
            String lastname1 = form["f1-lastname1"];
            String lastname2 = form["f1-lastname2"];
            String apellidocasada = form["f1-apellido-casada"];
            String fechanacimiento = form["f1-fecha-nacimiento"];
            String pais = form["f1-pais"];
            String ciudad = form["f1-ciudad"];
            String calle = form["f1-calle"];
            String casa = form["f1-casa"];
            String region = form["f1-region"];
            String subregion = form["f1-subRegion"];
            String firstnamer = form["f1-firstname-r"];
            String secondnamer = form["f1-secondname-r"];
            String lastname1r = form["f1-lastname1-r"];
            String lastname2r = form["f1-lastname2-r"];
            String apellidocasadar = form["f1-apellido-casada-r"];
            String fechanacimientor = form["f1-fecha-nacimiento-r"];
            String email = form["f1-email"];
            String estcivil = form["f1-est-civil"];
            String gen = form["f1-gen"];
            String numexpediente = form["f1-num-expediente"];
            String tel = form["f1-tel"];
            String telr = form["f1-tel-r"];
            String relacion = form["f1-relacion"];

            var x = _context.Database.ExecuteSqlCommand("spManejarExpedientes @p0, @p1, @p2, @p3, @p4, @p5, @p6, @p7, @p8, @p9, @p10, @p11, @p12, @p13, @p14, @p15, @p16, @p17, @p18, @p19, @p20, @p21, @p22, @p23, @p24, @p25, @p26", parameters: new[] { tDeclaracion, expedienteid, firstname, secondname, lastname1, lastname2, apellidocasada, fechanacimiento, pais, ciudad, calle, casa, region, subregion, firstnamer, secondnamer, lastname1r, lastname2r, apellidocasadar, fechanacimientor, email, estcivil, gen, numexpediente, tel, telr, relacion });
            System.Diagnostics.Debug.WriteLine(x);

            if (ModelState.IsValid)
            {
                /*_context.Add(expedientes);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));*/
            }
            return RedirectToAction(nameof(Index));
        }

        // GET: Expedientes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var expedientes = await _context.Expedientes.FindAsync(id);
            if (expedientes == null)
            {
                return NotFound();
            }
            ViewData["DireccionId"] = new SelectList(_context.Direcciones, "DireccionId", "Calle", expedientes.DireccionId);
            ViewData["EstadoCivilId"] = new SelectList(_context.EstadosCiviles, "EstadoCivilId", "EstadoCivil", expedientes.EstadoCivilId);
            ViewData["GeneroId"] = new SelectList(_context.Generos, "GeneroId", "Genero", expedientes.GeneroId);
            ViewData["PacienteId"] = new SelectList(_context.Pacientes, "PacienteId", "PacienteEmail", expedientes.PacienteId);
            return View(expedientes);
        }

        // POST: Expedientes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ExpedienteId,PacienteId,EstadoCivilId,DireccionId,GeneroId,NumExpediente,FechaCreacion,ExpEstado")] Expedientes expedientes)
        {
            if (id != expedientes.ExpedienteId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(expedientes);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ExpedientesExists(expedientes.ExpedienteId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["DireccionId"] = new SelectList(_context.Direcciones, "DireccionId", "Calle", expedientes.DireccionId);
            ViewData["EstadoCivilId"] = new SelectList(_context.EstadosCiviles, "EstadoCivilId", "EstadoCivil", expedientes.EstadoCivilId);
            ViewData["GeneroId"] = new SelectList(_context.Generos, "GeneroId", "Genero", expedientes.GeneroId);
            ViewData["PacienteId"] = new SelectList(_context.Pacientes, "PacienteId", "PacienteEmail", expedientes.PacienteId);
            return View(expedientes);
        }

        // GET: Expedientes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var expedientes = await _context.Expedientes
                .Include(e => e.Direccion)
                .Include(e => e.EstadoCivil)
                .Include(e => e.Genero)
                .Include(e => e.Paciente)
                .FirstOrDefaultAsync(m => m.ExpedienteId == id);
            if (expedientes == null)
            {
                return NotFound();
            }

            return View(expedientes);
        }

        // POST: Expedientes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var expedientes = await _context.Expedientes.FindAsync(id);
            _context.Expedientes.Remove(expedientes);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ExpedientesExists(int id)
        {
            return _context.Expedientes.Any(e => e.ExpedienteId == id);
        }
    }
}
