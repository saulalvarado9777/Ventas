using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Admin.Controllers
{
    public class GestionProductosController : Controller
    {
        // GET: GestionProductos
        public ActionResult Categoria()
        {
            return View();
        }
        public ActionResult Producto()
        {
            return View();
        }
    }
}