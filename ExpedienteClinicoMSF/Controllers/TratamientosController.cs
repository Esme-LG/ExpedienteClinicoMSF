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
    public class TratamientosController : Controller
    {
        private readonly expedienteContext _context;

        public TratamientosController(expedienteContext context)
        {
            _context = context;
        }

        // GET: Tratamientos
        public async Task<IActionResult> Index()
        {
            var expedienteContext = _context.Tratamientos.Include(t => t.Consulta);
            return View(await expedienteContext.ToListAsync());
        }

        // GET: Tratamientos/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tratamientos = await _context.Tratamientos
                .Include(t => t.Consulta)
                .FirstOrDefaultAsync(m => m.TratamientoId == id);
            if (tratamientos == null)
            {
                return NotFound();
            }

            return View(tratamientos);
        }

        // GET: Tratamientos/Create
        public IActionResult Create()
        {
            ViewData["ConsultaId"] = new SelectList(_context.ConsultasMedicas, "ConsultaId", "Sintomas");
            return View();
        }

        // POST: Tratamientos/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("TratamientoId,ConsultaId,Dosis,Frecuencia,Durante")] Tratamientos tratamientos)
        {
            if (ModelState.IsValid)
            {
                _context.Add(tratamientos);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ConsultaId"] = new SelectList(_context.ConsultasMedicas, "ConsultaId", "Sintomas", tratamientos.ConsultaId);
            return View(tratamientos);
        }

        // GET: Tratamientos/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tratamientos = await _context.Tratamientos.FindAsync(id);
            if (tratamientos == null)
            {
                return NotFound();
            }
            ViewData["ConsultaId"] = new SelectList(_context.ConsultasMedicas, "ConsultaId", "Sintomas", tratamientos.ConsultaId);
            return View(tratamientos);
        }

        // POST: Tratamientos/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("TratamientoId,ConsultaId,Dosis,Frecuencia,Durante")] Tratamientos tratamientos)
        {
            if (id != tratamientos.TratamientoId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tratamientos);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TratamientosExists(tratamientos.TratamientoId))
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
            ViewData["ConsultaId"] = new SelectList(_context.ConsultasMedicas, "ConsultaId", "Sintomas", tratamientos.ConsultaId);
            return View(tratamientos);
        }

        // GET: Tratamientos/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tratamientos = await _context.Tratamientos
                .Include(t => t.Consulta)
                .FirstOrDefaultAsync(m => m.TratamientoId == id);
            if (tratamientos == null)
            {
                return NotFound();
            }

            return View(tratamientos);
        }

        // POST: Tratamientos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var tratamientos = await _context.Tratamientos.FindAsync(id);
            _context.Tratamientos.Remove(tratamientos);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TratamientosExists(int id)
        {
            return _context.Tratamientos.Any(e => e.TratamientoId == id);
        }
    }
}
