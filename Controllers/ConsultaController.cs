using GestaoConsultasUVV.Data;
using GestaoConsultasUVV.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace GestaoConsultasUVV.Controllers
{
    [Authorize]
    public class ConsultaController : Controller
    {
        private readonly AppDbContext _context;

        public ConsultaController(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var usuarioId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));

            var consultas = await _context.Consultas
                .Where(c => c.UsuarioId == usuarioId)
                .ToListAsync();

            return View(consultas);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Consulta consulta)
        {
            var usuarioId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));

            consulta.UsuarioId = usuarioId;

            if (ModelState.IsValid)
            {
                _context.Consultas.Add(consulta);
                await _context.SaveChangesAsync();

                return RedirectToAction("Index");
            }

            return View(consulta);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var usuarioId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));

            var consulta = await _context.Consultas
                .FirstOrDefaultAsync(c => c.Id == id && c.UsuarioId == usuarioId);

            if (consulta == null)
            {
                return NotFound();
            }

            return View(consulta);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, Consulta consulta)
        {
            var usuarioId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));

            var consultaExistente = await _context.Consultas
                .FirstOrDefaultAsync(c => c.Id == id && c.UsuarioId == usuarioId);

            if (consultaExistente == null)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                consultaExistente.Especialidade = consulta.Especialidade;
                consultaExistente.DataHora = consulta.DataHora;
                consultaExistente.Descricao = consulta.Descricao;

                await _context.SaveChangesAsync();

                return RedirectToAction("Index");
            }

            return View(consulta);
}
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var usuarioId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));

            var consulta = await _context.Consultas
                .FirstOrDefaultAsync(c => c.Id == id && c.UsuarioId == usuarioId);

            if (consulta == null)
            {
                return NotFound();
            }

            return View(consulta);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var usuarioId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));

            var consulta = await _context.Consultas
                .FirstOrDefaultAsync(c => c.Id == id && c.UsuarioId == usuarioId);

            if (consulta != null)
            {
                _context.Consultas.Remove(consulta);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction("Index");
        }
    }
}