using GestaoConsultasUVV.Data;
using GestaoConsultasUVV.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication;
using System.Security.Claims;
using Microsoft.AspNetCore.Identity;

namespace GestaoConsultasUVV.Controllers
{
    public class UsuarioController : Controller
    {
        private readonly AppDbContext _context;

        public UsuarioController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult Cadastro()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Cadastro(Usuario usuario)
        {

            var emailJaCadastrado = await _context.Usuarios
                .AnyAsync(u => u.Email == usuario.Email);

            if (emailJaCadastrado)
            {
                ModelState.AddModelError("Email", "Este e-mail já está cadastrado.");
            }
            
            if (ModelState.IsValid)
            {
                usuario.DataCadastro = DateTime.Now;

                var hasher = new PasswordHasher<Usuario>();
                usuario.Senha = hasher.HashPassword(usuario, usuario.Senha);        

                _context.Usuarios.Add(usuario);
                await _context.SaveChangesAsync();

                return RedirectToAction("Login");
            }

            return View(usuario);
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(string email, string senha)
        {
            var usuario = await _context.Usuarios
            .FirstOrDefaultAsync(u => u.Email == email);

            if (usuario != null)
            {
                var hasher = new PasswordHasher<Usuario>();

                var resultado = hasher.VerifyHashedPassword(
                    usuario,
                    usuario.Senha,
                    senha
                );

                if (resultado == PasswordVerificationResult.Success)
                {
                    var claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.NameIdentifier, usuario.Id.ToString()),
                        new Claim(ClaimTypes.Name, usuario.Nome),
                        new Claim(ClaimTypes.Email, usuario.Email)
                    };

                    var identidade = new ClaimsIdentity(claims, "Cookies");

                    var principal = new ClaimsPrincipal(identidade);

                    await HttpContext.SignInAsync("Cookies", principal);

                    return RedirectToAction("Index", "Consulta");
                }
            }

            ModelState.AddModelError("", "E-mail ou senha inválidos.");

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync("Cookies");

            return RedirectToAction("Login");
        }
    }
}