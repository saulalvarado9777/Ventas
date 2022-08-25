using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;



using CapaEntidad;
using CapaNegocio;

namespace Admin.Controllers
{
    public class GestionUsuariosController : Controller
    {
        // GET: GestionUsuarios
        private CN_Usuarios OUsuario = new CN_Usuarios();
        public ActionResult Usuario()
        {
            ViewBag.Listar = OUsuario.Listar();
            return View();
        }

        [HttpPost]
        public JsonResult GuardarUsuarios(Usuario objeto)
        {
            object Resultado;
            string Mensaje = string.Empty;

            if (objeto.IdUsuario == 0)
            {
                Resultado = new CN_Usuarios().RegistrarUsuarios(objeto, out Mensaje);
            }
            else
            {
                Resultado = new CN_Usuarios().EditarUsuarios(objeto, out Mensaje);
            }

            return Json(new { Resultado = Resultado, Mensaje = Mensaje }, JsonRequestBehavior.AllowGet);

        }
        
        [HttpPost]
        public JsonResult EliminarUsuarios(int Id)
        {
            bool Respuesta = false;
            string Mensaje = string.Empty;

            Respuesta = new CN_Usuarios().EliminarUsuarios(Id, out Mensaje);
            return Json(new { Resultado = Respuesta, Mensaje = Mensaje }, JsonRequestBehavior.AllowGet);

        }
    }
}