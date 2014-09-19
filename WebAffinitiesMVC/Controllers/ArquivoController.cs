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
        public ActionResult Alterar(int id)
        {
            var arq = db.Arquivos.Find(id);
            if (arq == null) HttpNotFound();
            return View(arq);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Alterar(WebAffinitiesMVC.Models.Arquivo arquivo)
        {
            if (ModelState.IsValid)
            {
                if (arquivo == null) return HttpNotFound();
                var arq = await db.Arquivos.Where(x => x.id == arquivo.id).AsNoTracking().FirstOrDefaultAsync();
                if (arq == null) return HttpNotFound();
                db.Entry(arquivo).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Modificar");
            }
            return View(arquivo);
        }
        public async Task<ActionResult> Detalhar(int id)
        {
            var arquivo = await db.Arquivos.FindAsync(id);
            if (arquivo == null) return HttpNotFound();
            return View(arquivo);
        }
        [HttpGet]
        public async Task<ActionResult> Excluir(int id)
        {
            var arquivo = await db.Arquivos.FindAsync(id);
            if (arquivo == null) return HttpNotFound();
            db.Arquivos.Remove(arquivo);
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