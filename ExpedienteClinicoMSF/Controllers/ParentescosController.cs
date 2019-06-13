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
    public class ParentescosController : Controller
    {
        private readonly expedienteContext _context;

        public ParentescosController(expedienteContext context)
        {
            _context = context;
        }

        // GET: Parentescos
        public async Task<IActionResult> Index()
        {
            return View(await _context.Parentescos.ToListAsync());
        }

        // GET: Parentescos/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var parentescos = await _context.Parentescos
                .FirstOrDefaultAsync(m => m.ParentescoId == id);
            if (parentescos == null)
            {
                return NotFound();
            }

            return View(parentescos);
        }

        // GET: Parentescos/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Parentescos/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ParentescoId,Parentesco")] Parentescos parentescos)
        {
            if (ModelState.IsValid)
            {
                _context.Add(parentescos);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(parentescos);
        }

        // GET: Parentescos/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var parentescos = await _context.Parentescos.FindAsync(id);
            if (parentescos == null)
            {
                return NotFound();
            }
            return View(parentescos);
        }

        // POST: Parentescos/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ParentescoId,Parentesco")] Parentescos parentescos)
        {
            if (id != parentescos.ParentescoId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(parentescos);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ParentescosExists(parentescos.ParentescoId))
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
            return View(parentescos);
        }

        // GET: Parentescos/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var parentescos = await _context.Parentescos
                .FirstOrDefaultAsync(m => m.ParentescoId == id);
            if (parentescos == null)
            {
                return NotFound();
            }

            return View(parentescos);
        }

        // POST: Parentescos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var parentescos = await _context.Parentescos.FindAsync(id);
            _context.Parentescos.Remove(parentescos);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ParentescosExists(int id)
        {
            return _context.Parentescos.Any(e => e.ParentescoId == id);
        }
    }
}
