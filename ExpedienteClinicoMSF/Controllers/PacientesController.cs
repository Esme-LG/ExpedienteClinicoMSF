using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ExpedienteClinicoMSF.Models;

namespace ExpedienteClinicoMSF.Controllers
{
    public class PacientesController : Controller
    {
        private readonly expedienteContext _context;

        public PacientesController(expedienteContext context)
        {
            _context = context;
        }

        // GET: Pacientes
        public async Task<IActionResult> Index()
        {
            var expedienteContext = _context.Pacientes.Include(p => p.Persona);
            return View(await expedienteContext.ToListAsync());
        }

        // GET: Pacientes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pacientes = await _context.Pacientes
                .Include(p => p.Persona)
                .FirstOrDefaultAsync(m => m.PacienteId == id);
            if (pacientes == null)
            {
                return NotFound();
            }

            return View(pacientes);
        }

        // GET: Pacientes/Create
        public IActionResult Create()
        {
            ViewData["PersonaId"] = new SelectList(_context.Personas, "PersonaId", "ApellidoMaterno");
            return View();
        }

        // POST: Pacientes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PacienteId,PersonaId,PacienteEmail")] Pacientes pacientes)
        {
            if (ModelState.IsValid)
            {
                _context.Add(pacientes);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["PersonaId"] = new SelectList(_context.Personas, "PersonaId", "ApellidoMaterno", pacientes.PersonaId);
            return View(pacientes);
        }

        // GET: Pacientes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pacientes = await _context.Pacientes.FindAsync(id);
            if (pacientes == null)
            {
                return NotFound();
            }
            ViewData["PersonaId"] = new SelectList(_context.Personas, "PersonaId", "ApellidoMaterno", pacientes.PersonaId);
            return View(pacientes);
        }

        // POST: Pacientes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("PacienteId,PersonaId,PacienteEmail")] Pacientes pacientes)
        {
            if (id != pacientes.PacienteId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(pacientes);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PacientesExists(pacientes.PacienteId))
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
            ViewData["PersonaId"] = new SelectList(_context.Personas, "PersonaId", "ApellidoMaterno", pacientes.PersonaId);
            return View(pacientes);
        }

        // GET: Pacientes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pacientes = await _context.Pacientes
                .Include(p => p.Persona)
                .FirstOrDefaultAsync(m => m.PacienteId == id);
            if (pacientes == null)
            {
                return NotFound();
            }

            return View(pacientes);
        }

        // POST: Pacientes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var pacientes = await _context.Pacientes.FindAsync(id);
            _context.Pacientes.Remove(pacientes);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PacientesExists(int id)
        {
            return _context.Pacientes.Any(e => e.PacienteId == id);
        }
    }
}
