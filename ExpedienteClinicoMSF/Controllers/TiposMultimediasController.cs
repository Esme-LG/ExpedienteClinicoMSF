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
    public class TiposMultimediasController : Controller
    {
        private readonly expedienteContext _context;

        public TiposMultimediasController(expedienteContext context)
        {
            _context = context;
        }

        // GET: TiposMultimedias
        public async Task<IActionResult> Index()
        {
            return View(await _context.TiposMultimedia.ToListAsync());
        }

        // GET: TiposMultimedias/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tiposMultimedia = await _context.TiposMultimedia
                .FirstOrDefaultAsync(m => m.TipoMultimediaId == id);
            if (tiposMultimedia == null)
            {
                return NotFound();
            }

            return View(tiposMultimedia);
        }

        // GET: TiposMultimedias/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: TiposMultimedias/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("TipoMultimediaId,TipoMultimedia")] TiposMultimedia tiposMultimedia)
        {
            if (ModelState.IsValid)
            {
                _context.Add(tiposMultimedia);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(tiposMultimedia);
        }

        // GET: TiposMultimedias/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tiposMultimedia = await _context.TiposMultimedia.FindAsync(id);
            if (tiposMultimedia == null)
            {
                return NotFound();
            }
            return View(tiposMultimedia);
        }

        // POST: TiposMultimedias/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("TipoMultimediaId,TipoMultimedia")] TiposMultimedia tiposMultimedia)
        {
            if (id != tiposMultimedia.TipoMultimediaId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tiposMultimedia);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TiposMultimediaExists(tiposMultimedia.TipoMultimediaId))
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
            return View(tiposMultimedia);
        }

        // GET: TiposMultimedias/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tiposMultimedia = await _context.TiposMultimedia
                .FirstOrDefaultAsync(m => m.TipoMultimediaId == id);
            if (tiposMultimedia == null)
            {
                return NotFound();
            }

            return View(tiposMultimedia);
        }

        // POST: TiposMultimedias/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var tiposMultimedia = await _context.TiposMultimedia.FindAsync(id);
            _context.TiposMultimedia.Remove(tiposMultimedia);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TiposMultimediaExists(int id)
        {
            return _context.TiposMultimedia.Any(e => e.TipoMultimediaId == id);
        }
    }
}
