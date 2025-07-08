using Microsoft.AspNetCore.Mvc;

using WebLogin.Data;
using WebLogin.Models;
using Microsoft.EntityFrameworkCore;
using WebLogin.ViewModels;
using System.Threading.Tasks;

using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace WebLogin.Controllers
{
    public class AccesoController : Controller
    {
        private readonly WebDBContext _WebDBContext;
        public AccesoController(WebDBContext webDBContext)
        {
            _WebDBContext = webDBContext;
        }

        [HttpGet]
        public IActionResult Registrarse()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Registrarse(UsuarioVM modelo)
        {
            if (modelo.Clave != modelo.ConfirmarClave)
            {
                ViewData["Mensaje"] = "Las contraseñas no coinciden XD";
                return View();
            }

            Usuario usuario = new Usuario()
            {
                NombreCompleto = modelo.NombreCompleto,
                Correo = modelo.Correo,
                Clave = modelo.Clave
            };

            await _WebDBContext.Usuarios.AddAsync(usuario);
            await _WebDBContext.SaveChangesAsync();

            if (usuario.IdUsuario != 0) return RedirectToAction("Login", "Acceso");

            ViewData["Mensaje"] = "No se pudo crear el usuario, error fatal";
            return View();
        }


        [HttpGet]
        public IActionResult Login()
        {
            if (User.Identity.IsAuthenticated) return RedirectToAction("Index", "Home");
            {

            }

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginVM modelo)
        {
            Usuario? usuario_encontrado = await _WebDBContext.Usuarios
                                            .Where(u =>
                                                u.Correo == modelo.Correo &&
                                                u.Clave == modelo.Clave
                                                ).FirstOrDefaultAsync();
            if (usuario_encontrado == null)
            {
                ViewData["Mensaje"] = "No se encontraron coincidencias, XD";
                return View();
            }

            List<Claim> claims = new List<Claim>()
            {
                new Claim(ClaimTypes.Name, usuario_encontrado.NombreCompleto)
            };

            ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            AuthenticationProperties properties = new AuthenticationProperties()
            {
                AllowRefresh = true,
            };

            await HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(claimsIdentity),
                properties
            );

            return RedirectToAction("Index", "Home");
        }
    }
}