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
    public class ConsultasMedicasController : Controller
    {
        private readonly expedienteContext _context;

        public ConsultasMedicasController(expedienteContext context)
        {
            _context = context;
        }

        // GET: ConsultasMedicas
        public async Task<IActionResult> Index()
        {
            var expedienteContext = _context.ConsultasMedicas.Include(c => c.Medico).Include(c => c.Paciente).Include(c => c.SignoVital);
            return View(await expedienteContext.ToListAsync());
        }

        // GET: ConsultasMedicas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var consultasMedicas = await _context.ConsultasMedicas
                .Include(c => c.Medico)
                .Include(c => c.Paciente)
                .Include(c => c.SignoVital)
                .FirstOrDefaultAsync(m => m.ConsultaId == id);
            if (consultasMedicas == null)
            {
                return NotFound();
            }

            return View(consultasMedicas);
        }

        // GET: ConsultasMedicas/Create
        public IActionResult Create()
        {
            ViewData["MedicoId"] = new SelectList(_context.Medicos, "MedicoId", "NumMedico");
            ViewData["PacienteId"] = new SelectList(_context.Pacientes, "PacienteId", "PacienteEmail");
            ViewData["SignoVitalId"] = new SelectList(_context.SignosVitales, "SignoVitalId", "SignoVitalId");

            return View();
        }

        // POST: ConsultasMedicas/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ConsultaId,PacienteId,MedicoId,SignoVitalId,TipoReserva,FechaReserva,FechaConsulta,Sintomas")] ConsultasMedicas consultasMedicas)
        {
            if (ModelState.IsValid)
            {
                _context.Add(consultasMedicas);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["MedicoId"] = new SelectList(_context.Medicos, "MedicoId", "NumMedico", consultasMedicas.MedicoId);
            ViewData["PacienteId"] = new SelectList(_context.Pacientes, "PacienteId", "PacienteEmail", consultasMedicas.PacienteId);
            ViewData["SignoVitalId"] = new SelectList(_context.SignosVitales, "SignoVitalId", "SignoVitalId", consultasMedicas.SignoVitalId);
            return View(consultasMedicas);
        }

        // GET: ConsultasMedicas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var consultasMedicas = await _context.ConsultasMedicas.FindAsync(id);
            if (consultasMedicas == null)
            {
                return NotFound();
            }
            ViewData["MedicoId"] = new SelectList(_context.Medicos, "MedicoId", "NumMedico", consultasMedicas.MedicoId);
            ViewData["PacienteId"] = new SelectList(_context.Pacientes, "PacienteId", "PacienteEmail", consultasMedicas.PacienteId);
            ViewData["SignoVitalId"] = new SelectList(_context.SignosVitales, "SignoVitalId", "SignoVitalId", consultasMedicas.SignoVitalId);
            return View(consultasMedicas);
        }

        // POST: ConsultasMedicas/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ConsultaId,PacienteId,MedicoId,SignoVitalId,TipoReserva,FechaReserva,FechaConsulta,Sintomas")] ConsultasMedicas consultasMedicas)
        {
            if (id != consultasMedicas.ConsultaId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(consultasMedicas);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ConsultasMedicasExists(consultasMedicas.ConsultaId))
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
            ViewData["MedicoId"] = new SelectList(_context.Medicos, "MedicoId", "NumMedico", consultasMedicas.MedicoId);
            ViewData["PacienteId"] = new SelectList(_context.Pacientes, "PacienteId", "PacienteEmail", consultasMedicas.PacienteId);
            ViewData["SignoVitalId"] = new SelectList(_context.SignosVitales, "SignoVitalId", "SignoVitalId", consultasMedicas.SignoVitalId);
            return View(consultasMedicas);
        }

        // GET: ConsultasMedicas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var consultasMedicas = await _context.ConsultasMedicas
                .Include(c => c.Medico)
                .Include(c => c.Paciente)
                .Include(c => c.SignoVital)
                .FirstOrDefaultAsync(m => m.ConsultaId == id);
            if (consultasMedicas == null)
            {
                return NotFound();
            }

            return View(consultasMedicas);
        }

        // POST: ConsultasMedicas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var consultasMedicas = await _context.ConsultasMedicas.FindAsync(id);
            _context.ConsultasMedicas.Remove(consultasMedicas);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ConsultasMedicasExists(int id)
        {
            return _context.ConsultasMedicas.Any(e => e.ConsultaId == id);
        }
    }
}
