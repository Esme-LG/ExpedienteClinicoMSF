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
    public class ExamenesPacientesController : Controller
    {
        private readonly expedienteContext _context;

        public ExamenesPacientesController(expedienteContext context)
        {
            _context = context;
        }

        // GET: ExamenesPacientes
        public async Task<IActionResult> Index()
        {
            var expedienteContext = _context.ExamenesPacientes.Include(e => e.Consulta).Include(e => e.Examen);
            
            return View(await expedienteContext.ToListAsync());
        }

        // GET: ExamenesPacientes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var examenesPacientes = await _context.ExamenesPacientes
                .Include(e => e.Consulta)
                .Include(e => e.Examen)
                .FirstOrDefaultAsync(m => m.ExamenPacienteId == id);
            if (examenesPacientes == null)
            {
                return NotFound();
            }

            return View(examenesPacientes);
        }

        // GET: ExamenesPacientes/Create
        public IActionResult Create()
        {
            ViewData["ConsultaId"] = new SelectList(_context.ConsultasMedicas, "ConsultaId", "Sintomas");
            ViewData["ExamenId"] = new SelectList(_context.Examenes, "ExamenId", "Examen");
            return View();
        }

        // POST: ExamenesPacientes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ExamenPacienteId,ExamenId,ConsultaId,FechaRealizacion,FechaLectura,Lectura")] ExamenesPacientes examenesPacientes)
        {
            if (ModelState.IsValid)
            {
                _context.Add(examenesPacientes);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ConsultaId"] = new SelectList(_context.ConsultasMedicas, "ConsultaId", "Sintomas", examenesPacientes.ConsultaId);
            ViewData["ExamenId"] = new SelectList(_context.Examenes, "ExamenId", "DescripcionExamen", examenesPacientes.ExamenId);
            return View(examenesPacientes);
        }

        // GET: ExamenesPacientes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var examenesPacientes = await _context.ExamenesPacientes.FindAsync(id);
            if (examenesPacientes == null)
            {
                return NotFound();
            }
            ViewData["ConsultaId"] = new SelectList(_context.ConsultasMedicas, "ConsultaId", "Sintomas", examenesPacientes.ConsultaId);
            ViewData["ExamenId"] = new SelectList(_context.Examenes, "ExamenId", "DescripcionExamen", examenesPacientes.ExamenId);
            return View(examenesPacientes);
        }

        // POST: ExamenesPacientes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ExamenPacienteId,ExamenId,ConsultaId,FechaRealizacion,FechaLectura,Lectura")] ExamenesPacientes examenesPacientes)
        {
            if (id != examenesPacientes.ExamenPacienteId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(examenesPacientes);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ExamenesPacientesExists(examenesPacientes.ExamenPacienteId))
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
            ViewData["ConsultaId"] = new SelectList(_context.ConsultasMedicas, "ConsultaId", "Sintomas", examenesPacientes.ConsultaId);
            ViewData["ExamenId"] = new SelectList(_context.Examenes, "ExamenId", "DescripcionExamen", examenesPacientes.ExamenId);
            return View(examenesPacientes);
        }

        // GET: ExamenesPacientes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var examenesPacientes = await _context.ExamenesPacientes
                .Include(e => e.Consulta)
                .Include(e => e.Examen)
                .FirstOrDefaultAsync(m => m.ExamenPacienteId == id);
            if (examenesPacientes == null)
            {
                return NotFound();
            }

            return View(examenesPacientes);
        }

        // POST: ExamenesPacientes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var examenesPacientes = await _context.ExamenesPacientes.FindAsync(id);
            _context.ExamenesPacientes.Remove(examenesPacientes);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ExamenesPacientesExists(int id)
        {
            return _context.ExamenesPacientes.Any(e => e.ExamenPacienteId == id);
        }
    }
}
