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
    public class HistorialClinicoController : Controller
    {
        private readonly expedienteContext _context;

        public HistorialClinicoController(expedienteContext context)
        {
            _context = context;
        }

        // GET: HistorialClinico/
        public async Task<IActionResult> Index()
        {
            var expedienteContext = _context.Expedientes.Include(e => e.Direccion).Include(e => e.EstadoCivil).Include(e => e.Genero).Include(e => e.Paciente).Include(e => e.Paciente.Persona);

           return View(await expedienteContext.ToListAsync());
         //   return View("Listar");
        }

        // GET: HistorialClinico/Details/
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

           var expedientes = await _context.Expedientes
              .Include(e => e.EstadoCivil)
              .Include(e => e.Genero)
            //  .Include(e => e.Paciente).ThenInclude(e => e.Personas)
              .Include(e => e.Paciente.Persona)
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

        public async Task<IActionResult> AntecedentesPersonales(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }




















            return View();
        }

        public async Task<IActionResult> AntecedentesFamiliares(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            return View();
        }
    }
}