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
    public class MenusController : Controller
    {
        private readonly expedienteContext _context;

        public MenusController(expedienteContext context)
        {
            _context = context;
        }

        // GET: Menus
        public async Task<IActionResult> Index()
        {
            var expedienteContext = _context.Menus.Include(m => m.MenMenu);
            return View(await expedienteContext.ToListAsync());
        }

        // GET: Menus/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var menus = await _context.Menus
                .Include(m => m.MenMenu)
                .FirstOrDefaultAsync(m => m.MenuId == id);
            if (menus == null)
            {
                return NotFound();
            }

            return View(menus);
        }

        // GET: Menus/Create
        public IActionResult Create()
        {
            ViewData["MenMenuId"] = new SelectList(_context.Menus, "MenuId", "Direccion");
            return View();
        }

        // POST: Menus/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MenuId,MenMenuId,Opcion,Direccion")] Menus menus)
        {
            if (ModelState.IsValid)
            {
                _context.Add(menus);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["MenMenuId"] = new SelectList(_context.Menus, "MenuId", "Direccion", menus.MenMenuId);
            return View(menus);
        }

        // GET: Menus/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var menus = await _context.Menus.FindAsync(id);
            if (menus == null)
            {
                return NotFound();
            }
            ViewData["MenMenuId"] = new SelectList(_context.Menus, "MenuId", "Direccion", menus.MenMenuId);
            return View(menus);
        }

        // POST: Menus/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("MenuId,MenMenuId,Opcion,Direccion")] Menus menus)
        {
            if (id != menus.MenuId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(menus);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MenusExists(menus.MenuId))
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
            ViewData["MenMenuId"] = new SelectList(_context.Menus, "MenuId", "Direccion", menus.MenMenuId);
            return View(menus);
        }

        // GET: Menus/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var menus = await _context.Menus
                .Include(m => m.MenMenu)
                .FirstOrDefaultAsync(m => m.MenuId == id);
            if (menus == null)
            {
                return NotFound();
            }

            return View(menus);
        }

        // POST: Menus/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var menus = await _context.Menus.FindAsync(id);
            _context.Menus.Remove(menus);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MenusExists(int id)
        {
            return _context.Menus.Any(e => e.MenuId == id);
        }
    }
}
