using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ExpedienteClinicoMSF.Models;
using Microsoft.AspNetCore.Http;
using System.Data.SqlClient;
using System.Xml.XPath;
using System.Data;
using System.Text;
using System.Globalization;
using System.Data.Common;

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
            var expedienteContext = _context.Expedientes.Include(e => e.Direccion).Include(e => e.EstadoCivil).Include(e => e.Genero)
                .Include(e => e.Paciente).Include(e => e.Paciente.Persona);

            //string query = "Select pa.PACIENTE_ID, p.PRIMER_NOMBRE, p.SEGUNDO_NOMBRE, p.APELLIDO_PATERNO, p.APELLIDO_MATERNO, p.APELLIDO_CASADA, p.FECHA_NACIMIENTO, pa.PACIENTE_EMAIL from PACIENTES as pa left outer join PERSONAS as p on pa.PERSONA_ID = p.PERSONA_ID"
           // var data = _context.Database.ExecuteSqlCommand(query);
            return View(await expedienteContext.ToListAsync());
        }

        // GET: Expedientes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            //String tDeclaracion = "paciente";
            //String expedienteid = id.ToString();
            // var x = _context.Database.ExecuteSqlCommand("spDatosPersona @p0, @p1", parameters: new[] { tDeclaracion, expedienteid });
            var pacientes = (from p in _context.Pacientes
                             join pe in _context.Personas on p.PersonaId equals pe.PersonaId
                             join e in _context.Expedientes on p.PacienteId equals e.PacienteId
                             join d in _context.Direcciones on e.DireccionId equals d.DireccionId
                             join t in _context.Telefonos on e.ExpedienteId equals t.ExpedienteId
                             join ec in _context.EstadosCiviles on e.EstadoCivilId equals ec.EstadoCivilId
                             join g in _context.Generos on e.GeneroId equals g.GeneroId
                             join u in _context.Ubicaciones on d.DireccionId equals u.DireccionId
                             join r in _context.Regiones on u.RegionId equals r.RegionId
                             join sr in _context.Regiones on u.RegionId equals sr.RegRegionId
                             join pa in _context.Paises on d.PaisId equals pa.PaisId
                             where e.ExpedienteId == id 
                             select new
                             {
                                 expid = e.ExpedienteId,
                                 nume = e.NumExpediente,
                                 idp = p.PacienteId,
                                 pnombre = pe.PrimerNombre,
                                 snombre = pe.SegundoNombre,
                                 papellido = pe.ApellidoPaterno,
                                 sapellido = pe.ApellidoMaterno,
                                 capellido = pe.ApellidoCasada,
                                 fnaci = pe.FechaNacimiento,
                                 genero = g.Genero,
                                 estcivil = ec.EstadoCivil,
                                 email = p.PacienteEmail,
                                 tel = t.Numero,
                                 ciudad = d.Ciudad,
                                 calle = d.Calle,
                                 casa = d.NumeroCasa,
                                 pais = pa.Pais,
                                 region = r.Region,
                                 sregion = sr.Region
                             }).ToList();

            var responsable = (from r in _context.Responsables
                               join pe in _context.Personas on r.PersonaId equals pe.PersonaId
                               join e in _context.Expedientes on r.ExpedienteId equals e.PacienteId
                               join t in _context.Telefonos on r.ResponsableId equals t.ResponsableId
                               where e.ExpedienteId == id
                               select new
                               {
                                   expid = e.ExpedienteId,
                                   resid = r.ResponsableId,
                                   pnombrer = pe.PrimerNombre,
                                   snombrer = pe.SegundoNombre,
                                   papellidor = pe.ApellidoPaterno,
                                   sapellidor = pe.ApellidoMaterno,
                                   capellidor = pe.ApellidoCasada,
                                   fnacir = pe.FechaNacimiento,
                                   telr = t.Numero,
                                   rela = r.Relacion
                               }).ToList();

            //ViewData["paciente"] = pacientes[0];
            ViewData["nume"] = pacientes[0].nume;
            ViewData["pnombre"] = pacientes[0].pnombre;
            ViewData["snombre"] = pacientes[0].snombre;
            ViewData["papellido"] = pacientes[0].papellido;
            ViewData["sapellido"] = pacientes[0].sapellido;
            ViewData["capellido"] = pacientes[0].capellido;
            ViewData["fnaci"] = pacientes[0].fnaci;
            ViewData["genero"] = pacientes[0].genero;
            ViewData["estcivil"] = pacientes[0].estcivil;
            ViewData["email"] = pacientes[0].email;
            ViewData["tel"] = pacientes[0].tel;
            ViewData["pais"] = pacientes[0].pais;
            ViewData["region"] = pacientes[0].region;
            ViewData["sregion"] = pacientes[0].sregion;
            ViewData["ciudad"] = pacientes[0].ciudad;
            ViewData["casa"] = pacientes[0].casa;
            ViewData["calle"] = pacientes[0].calle;

            ViewData["pnombrer"] = responsable[0].pnombrer;
            ViewData["snombrer"] = responsable[0].snombrer;
            ViewData["papellidor"] = responsable[0].papellidor;
            ViewData["sapellidor"] = responsable[0].sapellidor;
            ViewData["capellidor"] = responsable[0].capellidor;
            ViewData["fnacir"] = responsable[0].fnacir;
            ViewData["telr"] = responsable[0].telr;
            ViewData["rela"] = responsable[0].rela;

            var expedientes = await _context.Expedientes
                .Include(e => e.EstadoCivil)
                .Include(e => e.Genero)
                .Include(e => e.Paciente)
                .Include(e => e.Direccion)
                .Include(e => e.EstadoCivil)
                .Include(e => e.Genero)
                .Include(e => e.Paciente)
                .Include(e => e.Paciente.Persona)
                .Include(e => e.Paciente.Persona.Responsable)
                .Include(e => e.Paciente.Persona.Responsable.Persona)
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
            var fecha = form["f1-fecha-nacimiento"];
            

            String tDeclaracion = "Insert";
            String expedienteid = null;
            String firstname = form["f1-firstname"];
            String secondname = form["f1-secondname"];
            String lastname1 = form["f1-lastname1"];
            String lastname2 = form["f1-lastname2"];
            String apellidocasada = form["f1-apellido-casada"];
            String fechanacimiento = form["f1-fecha-nacimiento"].ToString();
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

            var expedientes = await _context.Expedientes
                .Include(e => e.EstadoCivil)
                .Include(e => e.Genero)
                .Include(e => e.Paciente)
                .Include(e => e.Direccion)
                .Include(e => e.EstadoCivil)
                .Include(e => e.Genero)
                .Include(e => e.Paciente)
                .Include(e => e.Paciente.Persona)
                .Include(e => e.Paciente.Persona.Responsable)
                .Include(e => e.Paciente.Persona.Responsable.Persona)
                .FirstOrDefaultAsync(m => m.ExpedienteId == id);

            var pacientes = (from p in _context.Pacientes
                             join pe in _context.Personas on p.PersonaId equals pe.PersonaId
                             join e in _context.Expedientes on p.PacienteId equals e.PacienteId
                             join d in _context.Direcciones on e.DireccionId equals d.DireccionId
                             join t in _context.Telefonos on e.ExpedienteId equals t.ExpedienteId
                             join ec in _context.EstadosCiviles on e.EstadoCivilId equals ec.EstadoCivilId
                             join g in _context.Generos on e.GeneroId equals g.GeneroId
                             join u in _context.Ubicaciones on d.DireccionId equals u.DireccionId
                             join r in _context.Regiones on u.RegionId equals r.RegionId
                             join sr in _context.Regiones on u.RegionId equals sr.RegRegionId
                             join pa in _context.Paises on d.PaisId equals pa.PaisId
                             where e.ExpedienteId == id
                             select new { expid = e.ExpedienteId, nume = e.NumExpediente, idp = p.PacienteId, pnombre = pe.PrimerNombre, snombre = pe.SegundoNombre, papellido = pe.ApellidoPaterno, sapellido = pe.ApellidoMaterno, capellido = pe.ApellidoCasada,
                                 fnaci = pe.FechaNacimiento, genero = g.GeneroId, estcivil = ec.EstadoCivilId, email = p.PacienteEmail, tel = t.Numero, ciudad = d.Ciudad, calle = d.Calle, casa = d.NumeroCasa,
                                 pais = pa.PaisId, region = r.RegionId, sregion = sr.RegionId}).ToList();

            var responsable = (from r in _context.Responsables
                               join pe in _context.Personas on r.PersonaId equals pe.PersonaId
                               join e in _context.Expedientes on r.ExpedienteId equals e.PacienteId
                               join t in _context.Telefonos on e.ExpedienteId equals t.ExpedienteId
                               where e.ExpedienteId == id
                               select new
                               {
                                   expid = e.ExpedienteId,
                                   resid = r.ResponsableId,
                                   pnombrer = pe.PrimerNombre,
                                   snombrer = pe.SegundoNombre,
                                   papellidor = pe.ApellidoPaterno,
                                   sapellidor = pe.ApellidoMaterno,
                                   capellidor = pe.ApellidoCasada,
                                   fnacir = pe.FechaNacimiento,
                                   telr = t.Numero,
                                   rela = r.Relacion
                               }).ToList();

            //ViewData["paciente"] = pacientes[0];
            ViewData["nume"] = pacientes[0].nume;
            ViewData["pnombre"] = pacientes[0].pnombre;
            ViewData["snombre"] = pacientes[0].snombre;
            ViewData["papellido"] = pacientes[0].papellido;
            ViewData["sapellido"] = pacientes[0].sapellido;
            ViewData["capellido"] = pacientes[0].capellido;
            ViewData["fnaci"] = pacientes[0].fnaci;
            ViewData["genero"] = pacientes[0].genero;
            ViewData["estcivil"] = pacientes[0].estcivil;
            ViewData["email"] = pacientes[0].email;
            ViewData["tel"] = pacientes[0].tel;
            ViewData["pais"] = pacientes[0].pais;
            ViewData["region"] = pacientes[0].region;
            ViewData["sregion"] = pacientes[0].sregion;
            ViewData["ciudad"] = pacientes[0].ciudad;
            ViewData["casa"] = pacientes[0].casa;
            ViewData["calle"] = pacientes[0].calle;


            ViewData["pnombrer"] = responsable[0].pnombrer;
            ViewData["snombrer"] = responsable[0].snombrer;
            ViewData["papellidor"] = responsable[0].papellidor;
            ViewData["sapellidor"] = responsable[0].sapellidor;
            ViewData["capellidor"] = responsable[0].capellidor;
            ViewData["fnacir"] = responsable[0].fnacir;
            ViewData["telr"] = responsable[0].telr;
            ViewData["rela"] = responsable[0].rela;

            ViewData["EstadoCivilId"] = new SelectList(_context.EstadosCiviles, "EstadoCivilId", "EstadoCivil");
            ViewData["GeneroId"] = new SelectList(_context.Generos, "GeneroId", "Genero");
            ViewData["PaisId"] = new SelectList(_context.Paises.ToList(), "PaisId", "Pais");
            ViewData["RegionId"] = new SelectList(_context.Regiones.Where(x => x.RegRegionId == null).ToList(), "RegionId", "Region");
            ViewData["SubRegionId"] = new SelectList(_context.Regiones.Where(x => x.RegRegionId != null).ToList(), "RegionId", "Region");
            return View(expedientes);
        }

        // POST: Expedientes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Expedientes expedientes, IFormCollection form)
        {
            String tDeclaracion = "Update";
            String expedienteid = id.ToString();
            String firstname = form["f1-firstname"];
            String secondname = form["f1-secondname"];
            String lastname1 = form["f1-lastname1"];
            String lastname2 = form["f1-lastname2"];
            String apellidocasada = form["f1-apellido-casada"];
            String fechanacimiento = form["f1-fecha-nacimiento"].ToString();
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
            String fechanacimientor = form["f1-fecha-nacimiento-r"].ToString();
            String email = form["f1-email"];
            String estcivil = form["f1-est-civil"];
            String gen = form["f1-gen"];
            String numexpediente = form["f1-num-expediente"];
            String tel = form["f1-tel"];
            String telr = form["f1-tel-r"];
            String relacion = form["f1-relacion"];

            var x = _context.Database.ExecuteSqlCommand("spManejarExpedientes @p0, @p1, @p2, @p3, @p4, @p5, @p6, @p7, @p8, @p9, @p10, @p11, @p12, @p13, @p14, @p15, @p16, @p17, @p18, @p19, @p20, @p21, @p22, @p23, @p24, @p25, @p26", parameters: new[] { tDeclaracion, expedienteid, firstname, secondname, lastname1, lastname2, apellidocasada, fechanacimiento, pais, ciudad, calle, casa, region, subregion, firstnamer, secondnamer, lastname1r, lastname2r, apellidocasadar, fechanacimientor, email, estcivil, gen, numexpediente, tel, telr, relacion });
            System.Diagnostics.Debug.WriteLine(x);
            if (id != expedientes.ExpedienteId)
            {
                return NotFound();
            }

            
            ViewData["DireccionId"] = new SelectList(_context.Direcciones, "DireccionId", "Calle", expedientes.DireccionId);
            ViewData["EstadoCivilId"] = new SelectList(_context.EstadosCiviles, "EstadoCivilId", "EstadoCivil", expedientes.EstadoCivilId);
            ViewData["GeneroId"] = new SelectList(_context.Generos, "GeneroId", "Genero", expedientes.GeneroId);
            ViewData["PacienteId"] = new SelectList(_context.Pacientes, "PacienteId", "PacienteEmail", expedientes.PacienteId);
            return RedirectToAction(nameof(Index));
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
