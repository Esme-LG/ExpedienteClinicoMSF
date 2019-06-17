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
    public class EstadosCivilesController : Controller
    {
        private readonly expedienteContext _context;

        public EstadosCivilesController(expedienteContext context)
        {
            _context = context;
        }

        // GET: EstadosCiviles
        public async Task<IActionResult> Index()
        {
            return View(await _context.EstadosCiviles.ToListAsync());
        }

        // GET: EstadosCiviles/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var estadosCiviles = await _context.EstadosCiviles
                .FirstOrDefaultAsync(m => m.EstadoCivilId == id);
            if (estadosCiviles == null)
            {
                return NotFound();
            }

            return View(estadosCiviles);
        }

        // GET: EstadosCiviles/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: EstadosCiviles/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("EstadoCivilId,EstadoCivil")] EstadosCiviles estadosCiviles)
        {
            if (ModelState.IsValid)
            {
                _context.Add(estadosCiviles);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(estadosCiviles);
        }

        // GET: EstadosCiviles/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var estadosCiviles = await _context.EstadosCiviles.FindAsync(id);
            if (estadosCiviles == null)
            {
                return NotFound();
            }
            return View(estadosCiviles);
        }

        // POST: EstadosCiviles/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("EstadoCivilId,EstadoCivil")] EstadosCiviles estadosCiviles)
        {
            if (id != estadosCiviles.EstadoCivilId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(estadosCiviles);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EstadosCivilesExists(estadosCiviles.EstadoCivilId))
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
            return View(estadosCiviles);
        }

        // GET: EstadosCiviles/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var estadosCiviles = await _context.EstadosCiviles
                .FirstOrDefaultAsync(m => m.EstadoCivilId == id);
            if (estadosCiviles == null)
            {
                return NotFound();
            }

            return View(estadosCiviles);
        }

        // POST: EstadosCiviles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var estadosCiviles = await _context.EstadosCiviles.FindAsync(id);
            _context.EstadosCiviles.Remove(estadosCiviles);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EstadosCivilesExists(int id)
        {
            return _context.EstadosCiviles.Any(e => e.EstadoCivilId == id);
        }
    }
}
