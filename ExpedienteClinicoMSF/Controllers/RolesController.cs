using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ExpedienteClinicoMSF.Models;
using ExpedienteClinicoMSF.Models.ExpedienteViewModels;
using Microsoft.AspNetCore.Authorization;

namespace ExpedienteClinicoMSF.Controllers
{
    public class RolesController : Controller
    {
        private readonly expedienteContext _context;
        UserDataAccessLayer objUser = new UserDataAccessLayer();

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
        [Authorize(Roles = "Administrador")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var roles = await _context.Roles
                .Include(s => s.RolesMenus)
                    .ThenInclude(e => e.Menu).Include(s => s.RolesPermisos)
                    .ThenInclude(s => s.Permiso).AsNoTracking()
                .FirstOrDefaultAsync(m => m.RolId == id);
                RellenarPermisosAsignados(roles);
                RellenarMenusAsignados(roles);
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
            rol.RolesPermisos = new List<RolesPermisos>();
            RellenarPermisosAsignados(rol); 
            RellenarMenusAsignados(rol);
            
            ViewBag.Nombre= objUser.ToString();
            return View();
        }

        // POST: Roles/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("RolId,Rol,DescripcionRol,")] Roles roles, int[] selectedMenu = null, int [] selectedPermiso = null)
        {
            if (selectedMenu != null)
            {
                foreach(int opcion in selectedMenu)
                {
                    var opcionaAgregar = new RolesMenus { RolId = roles.RolId, MenuId = opcion };
                    roles.RolesMenus.Add(opcionaAgregar);
                }
            }

            if (selectedPermiso != null)
            {
                foreach (int option in selectedPermiso)
                {
                    var opcionAgregar = new RolesPermisos { RolId = roles.RolId, PermisoId = option };
                    roles.RolesPermisos.Add(opcionAgregar);
                }
            }

            if (ModelState.IsValid)
            {
                _context.Add(roles);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            RellenarMenusAsignados(roles);
            RellenarPermisosAsignados(roles);
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
                .Include(p => p.RolesPermisos).ThenInclude(p => p.Permiso)
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.RolId == id);

            if (rol == null)
            {
                return NotFound();
            }
            RellenarMenusAsignados(rol);
            RellenarPermisosAsignados(rol);
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

        private void RellenarPermisosAsignados(Roles rol)
        {
            var opcioness = _context.Permisos;
            var rolPermiso = new HashSet<int>(rol.RolesPermisos.Select(c => c.PermisoId));
            var viewModel = new List<PermisoRolData>();
            foreach (var option in opcioness)
            {
                viewModel.Add(new PermisoRolData
                {
                    PermisoId = option.PermisoId,
                    Option = option.Permiso,
                    Asignado = rolPermiso.Contains(option.PermisoId)
                });
            }
            ViewData["Permisos"] = viewModel;
        }
        // POST: Roles/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int? id, string[] selectedMenus, string[] selectedPermisos)
        {
            if (id == null)
            {
                return NotFound();
            }

            var rol = await _context.Roles
                .Include(i => i.RolesMenus)
                    .ThenInclude(i=> i.Menu).Include(i => i.RolesPermisos)
                    .ThenInclude(i => i.Permiso)
                .FirstOrDefaultAsync(i => i.RolId == id);        

            if (await TryUpdateModelAsync<Roles>(rol,"",i => i.Rol, i => i.DescripcionRol))
            {
                System.Diagnostics.Debug.WriteLine("TryUpdateModelAsync");
                ActualizarRolMenu(selectedMenus, rol);
                ActualizarRolPermiso(selectedPermisos, rol);
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
            ActualizarRolMenu(selectedMenus, rol);
            ActualizarRolPermiso(selectedPermisos, rol);
            RellenarMenusAsignados(rol);
            RellenarPermisosAsignados(rol);
            return View(rol);
        }

        private void ActualizarRolMenu(string[] selectedMenus, Roles rol)
        {
            System.Diagnostics.Debug.WriteLine("##### ActualizarRolMenu");
            if (selectedMenus == null)
            {
                rol.RolesMenus = new List<RolesMenus>();
                return;
            }

            var opcionesSeleccionadasHS = new HashSet<string>(selectedMenus);
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

        private void ActualizarRolPermiso(string[] selectedPermisos, Roles rol)
        {
            System.Diagnostics.Debug.WriteLine("##### ActualizarRolPermiso");
            if (selectedPermisos == null)
            {
                rol.RolesPermisos = new List<RolesPermisos>();
                return;
            }

            var opcionesSeleccionadasHS = new HashSet<string>(selectedPermisos);
            var rolPermiso = new HashSet<int>(rol.RolesPermisos.Select(c => c.Permiso.PermisoId));
            foreach (var option in _context.Permisos)
            {
                if (opcionesSeleccionadasHS.Contains(option.PermisoId.ToString()))
                {
                    System.Diagnostics.Debug.WriteLine(option.PermisoId);
                    if (!rolPermiso.Contains(option.PermisoId))
                    {
                        System.Diagnostics.Debug.WriteLine("######## Add");
                        rol.RolesPermisos.Add(new RolesPermisos { RolId = rol.RolId, PermisoId = option.PermisoId });
                    }
                }
                else
                {
                    if (rolPermiso.Contains(option.PermisoId))
                    {
                        RolesPermisos opcionAEliminar = rol.RolesPermisos.FirstOrDefault(i => i.PermisoId == option.PermisoId);
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
