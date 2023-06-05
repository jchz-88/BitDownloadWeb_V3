using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using BitDownloadWeb_V3_1.Data;
using BitDownloadWeb_V3_1.Models;
using System.Data.SqlClient;
using System.Data;
using NuGet.Protocol;

namespace BitDownloadWeb_V3_1.Controllers
{
    [Authorize]
    public class DownloadsController : Controller
    {
        private readonly Contexto _contexto;

        public DownloadsController(Contexto contexto)
        {
            _contexto = contexto;
        }
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public JsonResult ListarDownloads()
        {
            List<Download> lista = new List<Download>();
            try
            {
                using (SqlConnection con = new(_contexto.Conexion))
                {
                    using (SqlCommand cmd = new("downloads_listar", con))
                    {

                        cmd.CommandType = CommandType.StoredProcedure;

                        con.Open();
                        var dr = cmd.ExecuteReader();
                        
                        while (dr.Read())
                        {

                            lista.Add(new Download
                            {
                                ID = Convert.ToInt32(dr["ID"]),
                                HostName = dr["HostName"].ToString(),
                                FileID = Convert.ToInt32(dr["FileID"]),
                                MoveTo = dr["MoveTo"].ToString(),
                                BytesTransfer = (long)(dr["BytesTransfer"]),
                                Status = (Status)Convert.ToInt32(dr["Status"]),
                                ExitStatus = Convert.ToInt32(dr["ExitStatus"])
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
    }
}
