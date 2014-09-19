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
        WebAffinitiesMVC.Models.WebFormatterContext db = new Models.WebFormatterContext();
        // GET: Arquivo
        public ActionResult Index()
        {
            return View();
        }
        public static IEnumerable<WebAffinitiesMVC.Models.Arquivo> GetArquivos()
        {
            using (WebAffinitiesMVC.Models.WebFormatterContext db = new Models.WebFormatterContext())
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
        public ActionResult Alterar()
        {
            return View(new WebAffinitiesMVC.Models.Arquivo());
        }
        [HttpPost]
        public async Task<ActionResult> Alterar(WebAffinitiesMVC.Models.Arquivo arquivo)
        {
            if(arquivo == null) return HttpNotFound();
            var arq = await db.Arquivos.FindAsync(arquivo.id);
            if (arq == null) return HttpNotFound();
            db.Entry(arquivo).State = EntityState.Modified;
            await db.SaveChangesAsync();
            return View(arquivo);
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