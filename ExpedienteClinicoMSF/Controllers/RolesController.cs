﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ExpedienteClinicoMSF.Models;

namespace ExpedienteClinicoMSF.Controllers
{
    public class RolesController : Controller
    {
        private readonly expedienteContext _context;

        public RolesController(expedienteContext context)
        {
            _context = context;
        }

        // GET: Roles
        public async Task<IActionResult> Index(string sortOrder, string cadenaBusqueda)
        {
            ViewData["ordenarRol"] = String.IsNullOrEmpty(sortOrder) ? "Rol" : "";
            ViewData["ordenarDescripcion"] = sortOrder == "descripcion_asc" ? "descripcion_desc" : "descripcion_asc";
            ViewData["filtro"] = cadenaBusqueda;

            var roles = from s in _context.Roles select s;

            if (!String.IsNullOrEmpty(cadenaBusqueda))
            {
                roles = roles.Where(s => s.Rol.Contains(cadenaBusqueda) || s.DescripcionRol.Contains(cadenaBusqueda));
            }
            
            switch (sortOrder)
            {
                case "nombre_desc":
                    roles = roles.OrderByDescending(s => s.Rol);
                    break;
                case "descripcion_desc":
                    roles = roles.OrderByDescending(s => s.DescripcionRol);
                    break;
                case "descripcion_asc":
                    roles = roles.OrderBy(s => s.DescripcionRol);
                    break;
                default:
                    roles = roles.OrderBy(s => s.Rol);
                    break;
            }

            return View(await roles.AsNoTracking().ToListAsync());
            //return View(await _context.Roles.ToListAsync());
        }

        // GET: Roles/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var roles = await _context.Roles
                .FirstOrDefaultAsync(m => m.RolId == id);
            if (roles == null)
            {
                return NotFound();
            }

            return View(roles);
        }

        // GET: Roles/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Roles/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("RolId,Rol,DescripcionRol")] Roles roles)
        {
            if (ModelState.IsValid)
            {
                _context.Add(roles);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(roles);
        }

        // GET: Roles/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var roles = await _context.Roles.FindAsync(id);
            if (roles == null)
            {
                return NotFound();
            }
            return View(roles);
        }

        // POST: Roles/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("RolId,Rol,DescripcionRol")] Roles roles)
        {
            if (id != roles.RolId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(roles);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RolesExists(roles.RolId))
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
            return View(roles);
        }

        // GET: Roles/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var roles = await _context.Roles
                .FirstOrDefaultAsync(m => m.RolId == id);
            if (roles == null)
            {
                return NotFound();
            }

            return View(roles);
        }

        // POST: Roles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var roles = await _context.Roles.FindAsync(id);
            _context.Roles.Remove(roles);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RolesExists(int id)
        {
            return _context.Roles.Any(e => e.RolId == id);
        }
    }
}
