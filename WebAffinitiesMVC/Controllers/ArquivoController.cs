using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;

namespace WebAffinitiesMVC.Controllers
{
    public class ArquivoController : Controller
    {
        WebAffinitiesMVC.Models.WebAffinitiesContext db = new Models.WebAffinitiesContext();
        // GET: Arquivo
        public ActionResult Index()
        {
            return View();
        }
        public static IEnumerable<WebAffinitiesMVC.Models.Arquivo> GetArquivos()
        {
            using (WebAffinitiesMVC.Models.WebAffinitiesContext db = new Models.WebAffinitiesContext())
            {
                var arquivos = db.Arquivos.ToList();
                return arquivos;
            }
        }
        public ActionResult Criar()
        {
            return View(new WebAffinitiesMVC.Models.Arquivo());
        }
        [HttpPost]
        public async Task<ActionResult> Criar(WebAffinitiesMVC.Models.Arquivo arquivo)
        {
            if (!ModelState.IsValid)
            {
                return View(arquivo);
            }
            db.Arquivos.Add(arquivo);
            await db.SaveChangesAsync();
            return RedirectToAction("Index", "Home");
        }
        public async Task<ActionResult> Modificar()
        {
            var arquivos = await db.Arquivos.ToListAsync();
            return View(arquivos);
        }
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}