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
    public class ExamenesController : Controller
    {
        private readonly expedienteContext _context;

        public ExamenesController(expedienteContext context)
        {
            _context = context;
        }

        // GET: Examenes
        public async Task<IActionResult> Index()
        {
            var expedienteContext = _context.Examenes.Include(e => e.Categoria);
            return View(await expedienteContext.ToListAsync());
        }

        // GET: Examenes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var examenes = await _context.Examenes
                .Include(e => e.Categoria)
                .FirstOrDefaultAsync(m => m.ExamenId == id);

            ViewData["ResultadoId"] = new SelectList(_context.ExamenesResultados.Where(x => x.ExamenPacienteId == null).ToList());
            if (examenes == null)
            {
                return NotFound();
            }

            return View(examenes);
        }

        // GET: Examenes/Create
        public IActionResult Create()
        {
            ViewData["CategoriaId"] = new SelectList(_context.Categoria, "CategoriaId", "Categoria1");
            ViewData["TipoMultimediaId"] = new SelectList(_context.TiposMultimedia, "TipoMultimediaId", "TipoMultimedia");
            return View();
        }

        // POST: Examenes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ExamenId,CategoriaId,Examen,DescripcionExamen")] Examenes examenes)
        {
            if (ModelState.IsValid)
            {
                _context.Add(examenes);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CategoriaId"] = new SelectList(_context.Categoria, "CategoriaId", "Categoria1", examenes.CategoriaId);
            return View(examenes);
        }

        // GET: Examenes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var examenes = await _context.Examenes.FindAsync(id);
            if (examenes == null)
            {
                return NotFound();
            }
            ViewData["CategoriaId"] = new SelectList(_context.Categoria, "CategoriaId", "Categoria1", examenes.CategoriaId);
            return View(examenes);
        }

        // POST: Examenes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ExamenId,CategoriaId,Examen,DescripcionExamen")] Examenes examenes)
        {
            if (id != examenes.ExamenId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(examenes);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ExamenesExists(examenes.ExamenId))
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
            ViewData["CategoriaId"] = new SelectList(_context.Categoria, "CategoriaId", "Categoria1", examenes.CategoriaId);
            return View(examenes);
        }

        // GET: Examenes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var examenes = await _context.Examenes
                .Include(e => e.Categoria)
                .FirstOrDefaultAsync(m => m.ExamenId == id);
            if (examenes == null)
            {
                return NotFound();
            }

            return View(examenes);
        }

        // POST: Examenes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var examenes = await _context.Examenes.FindAsync(id);
            _context.Examenes.Remove(examenes);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ExamenesExists(int id)
        {
            return _context.Examenes.Any(e => e.ExamenId == id);
        }
    }
}
