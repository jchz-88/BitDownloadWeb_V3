using BitDownloadWeb_V3_1.Data;
using BitDownloadWeb_V3_1.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Data.SqlClient;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Newtonsoft.Json;

namespace BitDownloadWeb_V3_1.Controllers
{
    public class CuentaController : Controller
    {
        private readonly Contexto _contexto;

        public CuentaController(Contexto contexto)
        {
            _contexto = contexto;
        }
        public ActionResult Login()
        {
            ClaimsPrincipal c = HttpContext.User;
            if (c.Identity != null)
            {
                if (c.Identity.IsAuthenticated)
                {
                    return RedirectToAction("Index", "Home");
                }
            }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(Usuarios u)
        {
            try
            {
                using (SqlConnection con= new(_contexto.Conexion))
                {
                    using (SqlCommand cmd= new("validar_usuario", con))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.Add("@Email", System.Data.SqlDbType.VarChar).Value = u.Email;
                        cmd.Parameters.Add("@Clave", System.Data.SqlDbType.VarChar).Value = u.Clave;
                        con.Open();
          
                        var dr= cmd.ExecuteReader();
                        
                        while (dr.Read()) 
                        {
                            
                            if (dr["Email"] != null && u.Email != null)
                            {
                                List<Claim> c = new List<Claim>()
                                {
                                    new Claim(ClaimTypes.NameIdentifier, u.Email),

                                    new Claim(ClaimTypes.Role, dr["Rol"].ToString())
                                };

                                ClaimsIdentity Ci = new(c, CookieAuthenticationDefaults.AuthenticationScheme);
                                AuthenticationProperties p = new();

                                p.AllowRefresh = true;
                                p.IsPersistent = u.MantenerActivo;

                                if (!u.MantenerActivo)
                                {
                                    p.ExpiresUtc = DateTimeOffset.UtcNow.AddMinutes(1);
                                }
                                else
                                {
                                    p.ExpiresUtc = DateTimeOffset.UtcNow.AddDays(1);
                                }

                                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(Ci), p);
                                
                                return RedirectToAction("Index", "Home");
                            }
                            else
                            {
                                ViewBag.Error = "Los datos ingresados no son correctos";
                            }
                        }
                        con.Close();
                    }
                    return View();
                }
            }
            catch (Exception ex)
            {

                return View();
            }
        }
        public async Task<IActionResult> Salir()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login", "Cuenta");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View("Error!");
        }

    }
}
