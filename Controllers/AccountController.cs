using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using PrimerProyecto.Models;

namespace PrimerProyecto.Controllers;

public class AccountController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public AccountController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    [HttpPost]
    public IActionResult Login(Usuarios usuario)
    {
        if (ModelState.IsValid)
        {
            var usuarioEncontrado = BD.BuscarPersona(usuario.UserName, usuario.Contraseña);
            if (usuarioEncontrado != null)
            {
                return RedirectToAction("Bienvenida", "Home");
            }
            ViewBag.Error = "Nombre de usuario o contraseña incorrectos.";
        }
        return View(usuario);
    }
   [HttpPost]
public IActionResult Register(Usuarios usuario)
{
    var usuarioEncontrado = BD.BuscarPersona(usuario.UserName, usuario.Contraseña);
    if (usuarioEncontrado == null)
    {
        BD.AñadirUsuario(usuario);
        return RedirectToAction("Login"); // O cualquier otra acción como confirmación
    }

    ModelState.AddModelError("", "El usuario ya existe.");
    return View(usuario);
}

    public IActionResult CambiarContraseña(Usuarios usuario, string nuevaContraseña)
    {
        ViewBag.Mensaje = "";
        var usuarioEncontrado = BD.BuscarPersona(usuario.UserName, usuario.Contraseña);
        if (usuarioEncontrado != null)
            {
                BD.CambiarContraseña(usuario.UserName, nuevaContraseña);
             ViewBag.Mensaje = "La contraseña fue cambiada con exito";
            }
            else{
                ViewBag.Mensaje = "Usuario no encontrado";
            }
        return View();
    }
    public IActionResult Bienvenida(){
        return View();
    }
}
