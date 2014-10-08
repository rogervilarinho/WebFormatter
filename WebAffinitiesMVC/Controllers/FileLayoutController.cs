using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebAffinitiesMVC.Models;
using WebAffinitiesMVC.ViewModels;

namespace WebAffinitiesMVC.Controllers
{
    public class FileLayoutController : Controller
    {
        WebFormatterEntities db = new WebFormatterEntities();
        // GET: FileLayout
        public ActionResult Index(int? idArquivo)
        {
            if (!idArquivo.HasValue) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            var layouts = db.LAYOUT.Where(x => x.ID_ARQUIVO.Equals(idArquivo.Value));
            FileLayout file = new FileLayout();
            file.Layouts = layouts;
            return View(file);
        }
    }
}