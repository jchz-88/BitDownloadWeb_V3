using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using BitDownloadWeb_V3_1.Data;
using BitDownloadWeb_V3_1.Models;
using System.Data.SqlClient;
using System.Data;
using System;
using System.Reflection;
using Newtonsoft.Json;
using Microsoft.CodeAnalysis.VisualBasic.Syntax;
using Microsoft.DotNet.Scaffolding.Shared.Messaging;
using System.Text.Json;
//using System.Web.Mvc;

namespace BitDownloadWeb_V3_1.Controllers
{
    [Authorize(Roles = "Administrador")]
    public class UsuariosController : Controller
    {
        private readonly Contexto _contexto;

        public UsuariosController(Contexto contexto)
        {
            _contexto = contexto;
        }

        // GET: UsuariosController
        public ActionResult Index()
        {
            return View();
        }


        [HttpGet]
        public JsonResult ListarUsuarios()
        {
            List<Usuarios> lista = new List<Usuarios>();
            try
            {
                using (SqlConnection con = new(_contexto.Conexion))
                {
                    using (SqlCommand cmd = new("usuarios_listar", con))
                    {

                        cmd.CommandType = CommandType.StoredProcedure;

                        con.Open();
                        var dr = cmd.ExecuteReader();



                        while (dr.Read())
                        {

                            lista.Add(new Usuarios
                            {
                                Id = Convert.ToInt32(dr["Id"]),
                                Nombre = dr["Nombre"].ToString(),
                                Apellido = dr["Apellido"].ToString(),
                                Email = dr["Email"].ToString(),
                                Activo = Convert.ToInt32(dr["Activo"]),
                                FechaRegistro = Convert.ToDateTime(dr["FechaRegistro"].ToString()),
                                UltimaConexion = Convert.ToDateTime(dr["UltimaConexion"].ToString()),
                                Telefono = dr["Telefono"].ToString()
                            });
                        }

                    }
                    return Json(new { Data = lista });
                }
            }
            catch (Exception)
            {
                return Json(ViewBag.Error = "No Se Encotraron resultados");
            }

        }

        public JsonResult ObtenerUsuario(int id)
        {
            Usuarios usuario1 = new Usuarios();
            try
            {
                using (SqlConnection con = new(_contexto.Conexion))
                {
                    using (SqlCommand cmd = new("sp_usuario_obtener", con))
                    {

                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@Id", SqlDbType.Int).Value = id;

                        con.Open();
                        var dr = cmd.ExecuteReader();
                        while (dr.Read())
                        {

                            usuario1.Id = Convert.ToInt32(dr["Id"]);
                            usuario1.Nombre = dr["Nombre"].ToString();
                            usuario1.Apellido = dr["Apellido"].ToString();
                            usuario1.Email = dr["Email"].ToString();
                            usuario1.Activo = Convert.ToInt32(dr["Activo"]);
                            usuario1.FechaRegistro = Convert.ToDateTime(dr["FechaRegistro"].ToString());
                            usuario1.UltimaConexion = Convert.ToDateTime(dr["UltimaConexion"].ToString());
                            usuario1.Telefono = dr["Telefono"].ToString();
                        }
                    }
                    return Json(usuario1);
                }
            }
            catch (Exception)
            {
                return Json(ViewBag.Error = "No Se Encotraron resultados");
            }

        }

        public JsonResult EliminarUsuario(int id)
        {
            var respuesta = true;
            Usuarios usuario1 = new Usuarios();
            try
            {
                using (SqlConnection con = new(_contexto.Conexion))
                {
                    using (SqlCommand cmd = new("sp_usuario_eliminar", con))
                    {

                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@Id", SqlDbType.Int).Value = id;

                        con.Open();
                        var dr = cmd.ExecuteNonQuery();

                    }

                }
            }
            catch (Exception)
            {
                respuesta = false;
            }
            return Json(new { resultado = respuesta });
        }

        [HttpPost]
        public IActionResult GuardarUsuario(Usuarios oUser)
        {

            bool respuesta = true;


            try
            {

                if (oUser.Id == 0)
                {
                    using (SqlConnection con = new(_contexto.Conexion))
                    {
                        using (SqlCommand cmd = new("sp_usuario_insertar", con))
                        {

                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Parameters.Add("@Nombre", SqlDbType.VarChar).Value = oUser.Nombre;
                            cmd.Parameters.Add("@Apellido", SqlDbType.VarChar).Value = oUser.Apellido;
                            cmd.Parameters.Add("@Telefono", SqlDbType.VarChar).Value = oUser.Telefono;
                            cmd.Parameters.Add("@Email", SqlDbType.VarChar).Value = oUser.Email;
                            cmd.Parameters.Add("@Activo", SqlDbType.Int).Value = oUser.Activo;
                            cmd.Parameters.Add("@Clave", SqlDbType.VarChar).Value = "123";
                            cmd.Parameters.Add("@RolId", SqlDbType.Int).Value = 1;

                            con.Open();
                            var dr = cmd.ExecuteNonQuery();

                        }

                    }
                }
                else
                {
                    using (SqlConnection con = new(_contexto.Conexion))
                    {
                        using (SqlCommand cmd = new("sp_usuario_actualizar", con))
                        {

                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Parameters.Add("@Id", SqlDbType.Int).Value = oUser.Id;
                            cmd.Parameters.Add("@Nombre", SqlDbType.Int).Value = oUser.Nombre;
                            cmd.Parameters.Add("@Apellido", SqlDbType.Int).Value = oUser.Apellido;
                            cmd.Parameters.Add("@Telefono", SqlDbType.Int).Value = oUser.Telefono;
                            cmd.Parameters.Add("@Email", SqlDbType.Int).Value = oUser.Email;
                            cmd.Parameters.Add("@Activo", SqlDbType.Int).Value = oUser.Activo;

                            con.Open();
                            var dr = cmd.ExecuteNonQuery();

                        }

                    }
                }
                
            }
            catch
            {
                respuesta = false;
            }


            if (respuesta)
            {
                return RedirectToAction("Index");
            }
            else
            {
                return View();
            }

        }
    }
}
