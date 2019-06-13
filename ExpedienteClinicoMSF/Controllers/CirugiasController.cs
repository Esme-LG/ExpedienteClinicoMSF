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
    public class CirugiasController : Controller
    {
        private readonly expedienteContext _context;

        public CirugiasController(expedienteContext context)
        {
            _context = context;
        }

        // GET: Cirugias
        public async Task<IActionResult> Index()
        {
            var expedienteContext = _context.Cirugias.Include(c => c.Especialidad);
            return View(await expedienteContext.ToListAsync());
        }

        // GET: Cirugias/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cirugias = await _context.Cirugias
                .Include(c => c.Especialidad)
                .FirstOrDefaultAsync(m => m.CirugiaId == id);
            if (cirugias == null)
            {
                return NotFound();
            }

            return View(cirugias);
        }

        // GET: Cirugias/Create
        public IActionResult Create()
        {
            ViewData["EspecialidadId"] = new SelectList(_context.Especialidades, "EspecialidadId", "DescripcionEsp");
            return View();
        }

        // POST: Cirugias/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CirugiaId,EspecialidadId,Cirugia")] Cirugias cirugias)
        {
            if (ModelState.IsValid)
            {
                _context.Add(cirugias);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["EspecialidadId"] = new SelectList(_context.Especialidades, "EspecialidadId", "DescripcionEsp", cirugias.EspecialidadId);
            return View(cirugias);
        }

        // GET: Cirugias/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cirugias = await _context.Cirugias.FindAsync(id);
            if (cirugias == null)
            {
                return NotFound();
            }
            ViewData["EspecialidadId"] = new SelectList(_context.Especialidades, "EspecialidadId", "DescripcionEsp", cirugias.EspecialidadId);
            return View(cirugias);
        }

        // POST: Cirugias/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CirugiaId,EspecialidadId,Cirugia")] Cirugias cirugias)
        {
            if (id != cirugias.CirugiaId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(cirugias);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CirugiasExists(cirugias.CirugiaId))
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
            ViewData["EspecialidadId"] = new SelectList(_context.Especialidades, "EspecialidadId", "DescripcionEsp", cirugias.EspecialidadId);
            return View(cirugias);
        }

        // GET: Cirugias/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cirugias = await _context.Cirugias
                .Include(c => c.Especialidad)
                .FirstOrDefaultAsync(m => m.CirugiaId == id);
            if (cirugias == null)
            {
                return NotFound();
            }

            return View(cirugias);
        }

        // POST: Cirugias/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var cirugias = await _context.Cirugias.FindAsync(id);
            _context.Cirugias.Remove(cirugias);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CirugiasExists(int id)
        {
            return _context.Cirugias.Any(e => e.CirugiaId == id);
        }
    }
}
