using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebAffinitiesMVC.Models;

namespace WebAffinitiesMVC.Controllers
{
    public class ArquivoDetalheLayoutController : Controller
    {
        WebFormatterContext db = new WebFormatterContext();
        // GET: ArquivoDetalheLayout
        public ActionResult Index(int id)
        {
            var arquivo = db.ArquivoDetalheLayouts.Where(x => x.arquivo.id == id);
            if (arquivo == null) HttpNotFound();
            //RETORNA TODOS OS LAYOUTS CRIDADOS
            return View(arquivo);
        }
    }
}