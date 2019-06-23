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
    public class RegionesController : Controller
    {
        private readonly expedienteContext _context;

        public RegionesController(expedienteContext context)
        {
            _context = context;
        }

        // GET: Regiones
        public async Task<IActionResult> Index()
        {
            var expedienteContext = _context.Regiones.Include(r => r.RegRegion);
            return View(await expedienteContext.ToListAsync());
        }

        // GET: Regiones/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var regiones = await _context.Regiones
                .Include(r => r.RegRegion)
                .FirstOrDefaultAsync(m => m.RegionId == id);
            if (regiones == null)
            {
                return NotFound();
            }

            return View(regiones);
        }

        // GET: Regiones/Create
        public IActionResult Create()
        {
            ViewData["RegRegionId"] = new SelectList(_context.Regiones.Where(x => x.RegRegionId == null).ToList(), "RegionId", "Region");
            return View();
        }

        // POST: Regiones/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("RegionId,RegRegionId,Region")] Regiones regiones)
        {
            if (ModelState.IsValid)
            {
                _context.Add(regiones);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["RegRegionId"] = new SelectList(_context.Regiones, "RegionId", "Region", regiones.RegRegionId);
            return View(regiones);
        }

        // GET: Regiones/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var regiones = await _context.Regiones.FindAsync(id);
            if (regiones == null)
            {
                return NotFound();
            }
            ViewData["RegRegionId"] = new SelectList(_context.Regiones.Where(x => x.RegRegionId == null).ToList(), "RegionId", "Region", regiones.RegRegionId);
            return View(regiones);
        }

        // POST: Regiones/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("RegionId,RegRegionId,Region")] Regiones regiones)
        {
            if (id != regiones.RegionId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(regiones);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RegionesExists(regiones.RegionId))
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
            ViewData["RegRegionId"] = new SelectList(_context.Regiones.Where(x => x.RegRegionId == null), "RegionId", "Region", regiones.RegRegionId);
            return View(regiones);
        }

        // GET: Regiones/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var regiones = await _context.Regiones
                .Include(r => r.RegRegion)
                .FirstOrDefaultAsync(m => m.RegionId == id);
            if (regiones == null)
            {
                return NotFound();
            }

            return View(regiones);
        }

        // POST: Regiones/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var regiones = await _context.Regiones.FindAsync(id);
            _context.Regiones.Remove(regiones);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RegionesExists(int id)
        {
            return _context.Regiones.Any(e => e.RegionId == id);
        }
    }
}
