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
    public class ExamenesResultadosController : Controller
    {
        private readonly expedienteContext _context;

        public ExamenesResultadosController(expedienteContext context)
        {
            _context = context;
        }

        // GET: ExamenesResultados
        public async Task<IActionResult> Index()
        {
            var expedienteContext = _context.ExamenesResultados.Include(e => e.Examen);
            return View(await expedienteContext.ToListAsync());
        }

        // GET: ExamenesResultados/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var examenesResultados = await _context.ExamenesResultados
                .Include(e => e.Examen)
                .FirstOrDefaultAsync(m => m.ExamenResultadoId == id);
            if (examenesResultados == null)
            {
                return NotFound();
            }

            return View(examenesResultados);
        }

        // GET: ExamenesResultados/Create
        public IActionResult Create()
        {
            ViewData["ExamenId"] = new SelectList(_context.Examenes, "ExamenId", "Examen");
            return View();
        }

        // POST: ExamenesResultados/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ExamenResultadoId,ExamenId,ExamenPacienteId,Resultado,Medida,ValorMin,ValorMax,Valor")] ExamenesResultados examenesResultados)
        {
            if (ModelState.IsValid)
            {
                _context.Add(examenesResultados);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ExamenId"] = new SelectList(_context.Examenes, "ExamenId", "DescripcionExamen", examenesResultados.ExamenId);
            ViewData["ExamenPacienteId"] = new SelectList(_context.ExamenesPacientes, "ExamenPacienteId", "ExamenPacienteId", examenesResultados.ExamenPacienteId);
            return View(examenesResultados);
        }

        // GET: ExamenesResultados/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var examenesResultados = await _context.ExamenesResultados.FindAsync(id);
            if (examenesResultados == null)
            {
                return NotFound();
            }
            ViewData["ExamenId"] = new SelectList(_context.Examenes, "ExamenId", "DescripcionExamen", examenesResultados.ExamenId);
            ViewData["ExamenPacienteId"] = new SelectList(_context.ExamenesPacientes, "ExamenPacienteId", "ExamenPacienteId", examenesResultados.ExamenPacienteId);
            return View(examenesResultados);
        }

        // POST: ExamenesResultados/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ExamenResultadoId,ExamenId,ExamenPacienteId,Resultado,Medida,ValorMin,ValorMax,Valor")] ExamenesResultados examenesResultados)
        {
            if (id != examenesResultados.ExamenResultadoId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(examenesResultados);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ExamenesResultadosExists(examenesResultados.ExamenResultadoId))
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
            ViewData["ExamenId"] = new SelectList(_context.Examenes, "ExamenId", "DescripcionExamen", examenesResultados.ExamenId);
            ViewData["ExamenPacienteId"] = new SelectList(_context.ExamenesPacientes, "ExamenPacienteId", "ExamenPacienteId", examenesResultados.ExamenPacienteId);
            return View(examenesResultados);
        }

        // GET: ExamenesResultados/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var examenesResultados = await _context.ExamenesResultados
                .Include(e => e.Examen)
                .Include(e => e.ExamenPaciente)
                .FirstOrDefaultAsync(m => m.ExamenResultadoId == id);
            if (examenesResultados == null)
            {
                return NotFound();
            }

            return View(examenesResultados);
        }

        // POST: ExamenesResultados/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var examenesResultados = await _context.ExamenesResultados.FindAsync(id);
            _context.ExamenesResultados.Remove(examenesResultados);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ExamenesResultadosExists(int id)
        {
            return _context.ExamenesResultados.Any(e => e.ExamenResultadoId == id);
        }
    }
}
