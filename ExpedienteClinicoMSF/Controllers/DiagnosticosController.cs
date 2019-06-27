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
    public class DiagnosticosController : Controller
    {
        private readonly expedienteContext _context;

        public DiagnosticosController(expedienteContext context)
        {
            _context = context;
        }

        // GET: Diagnosticos
        public async Task<IActionResult> Index()
        {
            var expedienteContext = _context.Diagnosticos.Include(d => d.Codigo).Include(d => d.Consulta);
            return View(await expedienteContext.ToListAsync());
        }

        // GET: Diagnosticos/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var diagnosticos = await _context.Diagnosticos
                .Include(d => d.Codigo)
                .Include(d => d.Consulta)
                .FirstOrDefaultAsync(m => m.DiagnosticoId == id);
            if (diagnosticos == null)
            {
                return NotFound();
            }

            return View(diagnosticos);
        }

        // GET: Diagnosticos/Create
        public IActionResult Create()
        {
            ViewData["CodigoId"] = new SelectList(_context.CodigosCie10, "CodigoId", "Cie10");
            ViewData["ConsultaId"] = new SelectList(_context.ConsultasMedicas, "ConsultaId", "Sintomas");
            return View();
        }

        // POST: Diagnosticos/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("DiagnosticoId,CodigoId,ConsultaId,Diagnostico,Comentario")] Diagnosticos diagnosticos)
        {
            if (ModelState.IsValid)
            {
                _context.Add(diagnosticos);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CodigoId"] = new SelectList(_context.CodigosCie10, "CodigoId", "Cie10", diagnosticos.CodigoId);
            ViewData["ConsultaId"] = new SelectList(_context.ConsultasMedicas, "ConsultaId", "Sintomas", diagnosticos.ConsultaId);
            return View(diagnosticos);
        }

        // GET: Diagnosticos/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var diagnosticos = await _context.Diagnosticos.FindAsync(id);
            if (diagnosticos == null)
            {
                return NotFound();
            }
            ViewData["CodigoId"] = new SelectList(_context.CodigosCie10, "CodigoId", "Cie10", diagnosticos.CodigoId);
            ViewData["ConsultaId"] = new SelectList(_context.ConsultasMedicas, "ConsultaId", "Sintomas", diagnosticos.ConsultaId);
            return View(diagnosticos);
        }

        // POST: Diagnosticos/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("DiagnosticoId,CodigoId,ConsultaId,Diagnostico,Comentario")] Diagnosticos diagnosticos)
        {
            if (id != diagnosticos.DiagnosticoId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(diagnosticos);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DiagnosticosExists(diagnosticos.DiagnosticoId))
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
            ViewData["CodigoId"] = new SelectList(_context.CodigosCie10, "CodigoId", "Cie10", diagnosticos.CodigoId);
            ViewData["ConsultaId"] = new SelectList(_context.ConsultasMedicas, "ConsultaId", "Sintomas", diagnosticos.ConsultaId);
            return View(diagnosticos);
        }

        // GET: Diagnosticos/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var diagnosticos = await _context.Diagnosticos
                .Include(d => d.Codigo)
                .Include(d => d.Consulta)
                .FirstOrDefaultAsync(m => m.DiagnosticoId == id);
            if (diagnosticos == null)
            {
                return NotFound();
            }

            return View(diagnosticos);
        }

        // POST: Diagnosticos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var diagnosticos = await _context.Diagnosticos.FindAsync(id);
            _context.Diagnosticos.Remove(diagnosticos);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DiagnosticosExists(int id)
        {
            return _context.Diagnosticos.Any(e => e.DiagnosticoId == id);
        }
    }
}
