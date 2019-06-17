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
    public class CodigosCie10Controller : Controller
    {
        private readonly expedienteContext _context;

        public CodigosCie10Controller(expedienteContext context)
        {
            _context = context;
        }

        // GET: CodigosCie10
        public async Task<IActionResult> Index()
        {
            return View(await _context.CodigosCie10.ToListAsync());
        }

        // GET: CodigosCie10/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var codigosCie10 = await _context.CodigosCie10
                .FirstOrDefaultAsync(m => m.CodigoId == id);
            if (codigosCie10 == null)
            {
                return NotFound();
            }

            return View(codigosCie10);
        }

        // GET: CodigosCie10/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: CodigosCie10/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CodigoId,Cie10,NomEnfermedad")] CodigosCie10 codigosCie10)
        {
            if (ModelState.IsValid)
            {
                _context.Add(codigosCie10);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(codigosCie10);
        }

        // GET: CodigosCie10/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var codigosCie10 = await _context.CodigosCie10.FindAsync(id);
            if (codigosCie10 == null)
            {
                return NotFound();
            }
            return View(codigosCie10);
        }

        // POST: CodigosCie10/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CodigoId,Cie10,NomEnfermedad")] CodigosCie10 codigosCie10)
        {
            if (id != codigosCie10.CodigoId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(codigosCie10);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CodigosCie10Exists(codigosCie10.CodigoId))
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
            return View(codigosCie10);
        }

        // GET: CodigosCie10/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var codigosCie10 = await _context.CodigosCie10
                .FirstOrDefaultAsync(m => m.CodigoId == id);
            if (codigosCie10 == null)
            {
                return NotFound();
            }

            return View(codigosCie10);
        }

        // POST: CodigosCie10/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var codigosCie10 = await _context.CodigosCie10.FindAsync(id);
            _context.CodigosCie10.Remove(codigosCie10);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CodigosCie10Exists(int id)
        {
            return _context.CodigosCie10.Any(e => e.CodigoId == id);
        }
    }
}
