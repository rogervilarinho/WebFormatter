using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using System.Net;
using WebAffinitiesMVC.Models;

namespace WebAffinitiesMVC.Controllers
{
    public class ArquivoController : Controller
    {
        WebFormatterEntities db = new Models.WebFormatterEntities();
        // GET: Arquivo
        public async Task<ActionResult> Index(int? idArquivo)
        {
            if (!idArquivo.HasValue) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            return View(await db.ARQUIVO.FindAsync(idArquivo.Value));
        }
        public static IEnumerable<WebAffinitiesMVC.Models.ARQUIVO> GetArquivos()
        {
            using (WebAffinitiesMVC.Models.WebFormatterEntities db = new Models.WebFormatterEntities())
            {
                var arquivos = db.ARQUIVO.ToList();
                return arquivos;
            }
        }
        public ActionResult Criar()
        {
            return View(new WebAffinitiesMVC.Models.ARQUIVO());
        }
        [HttpPost]
        public async Task<ActionResult> Criar(WebAffinitiesMVC.Models.ARQUIVO arquivo)
        {
            if (!ModelState.IsValid)
            {
                return View(arquivo);
            }
            db.ARQUIVO.Add(arquivo);
            await db.SaveChangesAsync();
            return RedirectToAction("Index", "Home");
        }
        public async Task<ActionResult> Modificar()
        {
            var arquivos = await db.ARQUIVO.ToListAsync();
            return View(arquivos);
        }
        public ActionResult Alterar(int id)
        {
            var arq = db.ARQUIVO.Find(id);
            if (arq == null) HttpNotFound();
            return View(arq);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Alterar(WebAffinitiesMVC.Models.ARQUIVO arquivo)
        {
            if (ModelState.IsValid)
            {
                if (arquivo == null) return HttpNotFound();
                var arq = await db.ARQUIVO.Where(x => x.ID == arquivo.ID).AsNoTracking().FirstOrDefaultAsync();
                if (arq == null) return HttpNotFound();
                db.Entry(arquivo).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Modificar");
            }
            return View(arquivo);
        }
        public async Task<ActionResult> Detalhar(int id)
        {
            var arquivo = await db.ARQUIVO.FindAsync(id);
            if (arquivo == null) return HttpNotFound();
            return View(arquivo);
        }
        [HttpGet]
        public async Task<ActionResult> Excluir(int id)
        {
            var arquivo = await db.ARQUIVO.FindAsync(id);
            if (arquivo == null) return HttpNotFound();
            db.ARQUIVO.Remove(arquivo);
            await db.SaveChangesAsync();
            return RedirectToAction("Modificar");
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