using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ExpedienteClinicoMSF.Models;
using System.Security.Cryptography;
using System.Text;

namespace ExpedienteClinicoMSF.Controllers
{
    public class UsuariosController : Controller
    {
        private readonly expedienteContext _context;

        public UsuariosController(expedienteContext context)
        {
            _context = context;
        }

        // GET: Usuarios
        public async Task<IActionResult> Index()
        {
            var expedienteContext = _context.Usuarios.Include(u => u.Direccion).Include(u => u.EstadoCivil).Include(u => u.Genero).Include(u => u.Hospital).Include(u => u.Persona).Include(u => u.Rol);
            return View(await expedienteContext.ToListAsync());
        }

        // GET: Usuarios/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var usuarios = await _context.Usuarios
                .Include(u => u.Direccion)
                .Include(u => u.EstadoCivil)
                .Include(u => u.Genero)
                .Include(u => u.Hospital)
                .Include(u => u.Persona)
                .Include(u => u.Rol)
                .FirstOrDefaultAsync(m => m.PersonaId == id);
            if (usuarios == null)
            {
                return NotFound();
            }

            return View(usuarios);
        }
        
        // GET: Usuarios/Create
        public IActionResult Create()
        {
            ViewData["DireccionId"] = new SelectList(_context.Direcciones, "DireccionId", "Calle");
            ViewData["EstadoCivilId"] = new SelectList(_context.EstadosCiviles, "EstadoCivilId", "EstadoCivil");
            ViewData["GeneroId"] = new SelectList(_context.Generos, "GeneroId", "Genero");
            ViewData["HospitalId"] = new SelectList(_context.Hospitales, "HospitalId", "HospitalNombre");
            ViewData["PersonaId"] = new SelectList(_context.Personas, "PersonaId", "ApellidoMaterno");
            ViewData["RolId"] = new SelectList(_context.Roles, "RolId", "DescripcionRol");
            return View();
        }

        // POST: Usuarios/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PersonaId,UsuarioId,RolId,HospitalId,EstadoCivilId,DireccionId,GeneroId,Email,Pass,Activo,Bloqueado,Intentos,FechaRegistro")] Usuarios usuarios)
        {
            if (ModelState.IsValid)
            {
                _context.Add(usuarios);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["DireccionId"] = new SelectList(_context.Direcciones, "DireccionId", "Calle", usuarios.DireccionId);
            ViewData["EstadoCivilId"] = new SelectList(_context.EstadosCiviles, "EstadoCivilId", "EstadoCivil", usuarios.EstadoCivilId);
            ViewData["GeneroId"] = new SelectList(_context.Generos, "GeneroId", "Genero", usuarios.GeneroId);
            ViewData["HospitalId"] = new SelectList(_context.Hospitales, "HospitalId", "HospitalNombre", usuarios.HospitalId);
            ViewData["PersonaId"] = new SelectList(_context.Personas, "PersonaId", "ApellidoMaterno", usuarios.PersonaId);
            ViewData["RolId"] = new SelectList(_context.Roles, "RolId", "DescripcionRol", usuarios.RolId);
            return View(usuarios);
        }

        public static string EncryptPassword(string data)
        {
            SHA1 sha = SHA1.Create();
            Byte[] hash = sha.ComputeHash(Encoding.Default.GetBytes(data));
            StringBuilder valor = new StringBuilder();
            int i;

            for(i=0; i < hash.Length - 1; i++)
            {
                valor.Append(hash[i].ToString());
            }

            return valor.ToString();
        }

        public static bool DecryptPassword(string data, string contra)
        {
            return EncryptPassword(contra) == data;
        }

        // GET: Usuarios/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var usuarios = await _context.Usuarios.FindAsync(id);
            if (usuarios == null)
            {
                return NotFound();
            }
            ViewData["DireccionId"] = new SelectList(_context.Direcciones, "DireccionId", "Calle", usuarios.DireccionId);
            ViewData["EstadoCivilId"] = new SelectList(_context.EstadosCiviles, "EstadoCivilId", "EstadoCivil", usuarios.EstadoCivilId);
            ViewData["GeneroId"] = new SelectList(_context.Generos, "GeneroId", "Genero", usuarios.GeneroId);
            ViewData["HospitalId"] = new SelectList(_context.Hospitales, "HospitalId", "HospitalNombre", usuarios.HospitalId);
            ViewData["PersonaId"] = new SelectList(_context.Personas, "PersonaId", "ApellidoMaterno", usuarios.PersonaId);
            ViewData["RolId"] = new SelectList(_context.Roles, "RolId", "DescripcionRol", usuarios.RolId);
            return View(usuarios);
        }

        // POST: Usuarios/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("PersonaId,UsuarioId,RolId,HospitalId,EstadoCivilId,DireccionId,GeneroId,Email,Pass,Activo,Bloqueado,Intentos,FechaRegistro")] Usuarios usuarios)
        {
            if (id != usuarios.PersonaId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(usuarios);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UsuariosExists(usuarios.PersonaId))
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
            ViewData["DireccionId"] = new SelectList(_context.Direcciones, "DireccionId", "Calle", usuarios.DireccionId);
            ViewData["EstadoCivilId"] = new SelectList(_context.EstadosCiviles, "EstadoCivilId", "EstadoCivil", usuarios.EstadoCivilId);
            ViewData["GeneroId"] = new SelectList(_context.Generos, "GeneroId", "Genero", usuarios.GeneroId);
            ViewData["HospitalId"] = new SelectList(_context.Hospitales, "HospitalId", "HospitalNombre", usuarios.HospitalId);
            ViewData["PersonaId"] = new SelectList(_context.Personas, "PersonaId", "ApellidoMaterno", usuarios.PersonaId);
            ViewData["RolId"] = new SelectList(_context.Roles, "RolId", "DescripcionRol", usuarios.RolId);
            return View(usuarios);
        }

        // GET: Usuarios/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var usuarios = await _context.Usuarios
                .Include(u => u.Direccion)
                .Include(u => u.EstadoCivil)
                .Include(u => u.Genero)
                .Include(u => u.Hospital)
                .Include(u => u.Persona)
                .Include(u => u.Rol)
                .FirstOrDefaultAsync(m => m.PersonaId == id);
            if (usuarios == null)
            {
                return NotFound();
            }

            return View(usuarios);
        }

        // POST: Usuarios/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var usuarios = await _context.Usuarios.FindAsync(id);
            _context.Usuarios.Remove(usuarios);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UsuariosExists(int id)
        {
            return _context.Usuarios.Any(e => e.PersonaId == id);
        }

        

                 
    }
}
