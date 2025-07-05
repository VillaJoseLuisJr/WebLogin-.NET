using Microsoft.AspNetCore.Mvc;

using WebLogin.Data;
using WebLogin.Models;
using Microsoft.EntityFrameworkCore;

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
    }
}