using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ExpedienteClinicoMSF.Models;
using ExpedienteClinicoMSF.Models.ExpedienteViewModels;

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
                .Include(s => s.RolesMenus)
                    .ThenInclude(e => e.Menu)
                .AsNoTracking()
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
            var rol = new Roles();
            rol.RolesMenus = new List<RolesMenus>();
            RellenarMenusAsignados(rol);
            return View();
        }

        // POST: Roles/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("RolId,Rol,DescripcionRol,")] Roles roles, string[] opcionesSeleccionadas)
        {
            if (opcionesSeleccionadas != null)
            {
                roles.RolesMenus = new List<RolesMenus>();
                foreach(var opcion in opcionesSeleccionadas)
                {
                    var opcionaAgregar = new RolesMenus { RolId = roles.RolId, MenuId = int.Parse(opcion) };
                    roles.RolesMenus.Add(opcionaAgregar);
                }
            }
            if (ModelState.IsValid)
            {
                _context.Add(roles);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            RellenarMenusAsignados(roles);
            return View(roles);
        }

        // GET: Roles/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var rol = await _context.Roles
                .Include(i => i.RolesMenus).ThenInclude(i => i.Menu)
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.RolId == id);

            if (rol == null)
            {
                return NotFound();
            }
            RellenarMenusAsignados(rol);
            return View(rol);
        }

        private void RellenarMenusAsignados(Roles rol)
        {
            var opciones = _context.Menus;
            var rolMenu = new HashSet<int>(rol.RolesMenus.Select(c => c.MenuId));
            var viewModel = new List<MenuRolData>();
            foreach (var opcion in opciones)
            {
                viewModel.Add(new MenuRolData
                {
                    MenuId = opcion.MenuId,
                    Opcion = opcion.Opcion,
                    Asignado = rolMenu.Contains(opcion.MenuId)
                });
            }
            ViewData["Menus"] = viewModel;
        }

        // POST: Roles/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int? id, string[] opcionesSeleccionadas)
        {
            if (id == null)
            {
                return NotFound();
            }

            var rol = await _context.Roles
                .Include(i => i.RolesMenus)
                    .ThenInclude(i=> i.Menu)
                .FirstOrDefaultAsync(s => s.RolId == id);

            if (await TryUpdateModelAsync<Roles>(rol,"",i => i.Rol, i => i.DescripcionRol))
            {
                System.Diagnostics.Debug.WriteLine("TryUpdateModelAsync");
                ActualizarRolMenu(opcionesSeleccionadas, rol);
                try
                {
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateException /* ex */)
                {
                    //Log the error (uncomment ex variable name and write a log.)
                    ModelState.AddModelError("", "Unable to save changes. " +
                        "Try again, and if the problem persists, " +
                        "see your system administrator.");
                }
                return RedirectToAction(nameof(Index));
            }
            ActualizarRolMenu(opcionesSeleccionadas, rol);
            RellenarMenusAsignados(rol);
            return View(rol);
        }

        private void ActualizarRolMenu(string[] opcionesSeleccionadas, Roles rol)
        {
            System.Diagnostics.Debug.WriteLine("##### ActualizarRolMenu");
            if (opcionesSeleccionadas == null)
            {
                rol.RolesMenus = new List<RolesMenus>();
                return;
            }

            var opcionesSeleccionadasHS = new HashSet<string>(opcionesSeleccionadas);
            var rolMenu = new HashSet<int>(rol.RolesMenus.Select(c => c.Menu.MenuId));
            foreach (var opcion in _context.Menus)
            {
                if (opcionesSeleccionadasHS.Contains(opcion.MenuId.ToString()))
                {
                    System.Diagnostics.Debug.WriteLine(opcion.MenuId);
                    if (!rolMenu.Contains(opcion.MenuId))
                    {
                        System.Diagnostics.Debug.WriteLine("######## Add");
                        rol.RolesMenus.Add(new RolesMenus { RolId = rol.RolId, MenuId = opcion.MenuId });
                    }
                }
                else
                {
                    if (rolMenu.Contains(opcion.MenuId))
                    {
                        RolesMenus opcionAEliminar = rol.RolesMenus.FirstOrDefault(i => i.MenuId == opcion.MenuId);
                        _context.Remove(opcionAEliminar);
                    }
                }
            }
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
