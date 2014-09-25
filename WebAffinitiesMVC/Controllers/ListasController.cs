using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebAffinitiesMVC.Models;

namespace WebAffinitiesMVC.Controllers
{
    public class ListasController : Controller
    {
        private WebFormatterEntities db = new WebFormatterEntities();

        // GET: Listas
        public async Task<ActionResult> Index(int? idArquivo)
        {
            if (!idArquivo.HasValue) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            var lista = db.LISTA.Include(l => l.ARQUIVO);
            var retorno = await lista.Where(x => x.ID_ARQUIVO == idArquivo.Value).ToListAsync();
            if (retorno.Count() == 0)
            {
                return View(new List<LISTA>() { new LISTA() { ARQUIVO = await db.ARQUIVO.FindAsync(idArquivo.Value) }});
            }
            return View(retorno);
        }

        // GET: Listas/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LISTA lISTA = await db.LISTA.FindAsync(id);
            if (lISTA == null)
            {
                return HttpNotFound();
            }
            return View(lISTA);
        }

        // GET: Listas/Create
        public ActionResult Create(int? idArquivo)
        {
            if (!idArquivo.HasValue) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            ViewBag.ID_ARQUIVO = new SelectList(db.ARQUIVO.Where(x => x.ID.Equals(idArquivo.Value)), "ID", "NOME");
            return View(new LISTA() { ARQUIVO = db.ARQUIVO.Find(idArquivo) });
        }

        // POST: Listas/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "ID,NOME,DESCRICAO,ID_ARQUIVO")] LISTA lISTA)
        {
            if (ModelState.IsValid)
            {
                db.LISTA.Add(lISTA);
                await db.SaveChangesAsync();
                return RedirectToAction("Index", "Listas", new { idArquivo = lISTA.ID_ARQUIVO });
            }

            ViewBag.ID_ARQUIVO = new SelectList(db.ARQUIVO.Where(x => x.ID.Equals(lISTA.ID_ARQUIVO)), "ID", "NOME", lISTA.ID_ARQUIVO);
            lISTA.ARQUIVO = db.ARQUIVO.Find(lISTA.ID_ARQUIVO);
            return View(lISTA);
        }

        // GET: Listas/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LISTA lISTA = await db.LISTA.FindAsync(id);
            if (lISTA == null)
            {
                return HttpNotFound();
            }
            ViewBag.ID_ARQUIVO = new SelectList(db.ARQUIVO.Where(x => x.ID.Equals(lISTA.ID_ARQUIVO)), "ID", "NOME", lISTA.ID_ARQUIVO);
            lISTA.ARQUIVO = db.ARQUIVO.Find(lISTA.ID_ARQUIVO);
            return View(lISTA);
        }

        // POST: Listas/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "ID,NOME,DESCRICAO,ID_ARQUIVO")] LISTA lISTA)
        {
            if (ModelState.IsValid)
            {
                db.Entry(lISTA).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index", "Listas", new { idArquivo = lISTA.ID_ARQUIVO });
            }
            ViewBag.ID_ARQUIVO = new SelectList(db.ARQUIVO.Where(x => x.ID.Equals(lISTA.ID_ARQUIVO)), "ID", "NOME", lISTA.ID_ARQUIVO);
            lISTA.ARQUIVO = db.ARQUIVO.Find(lISTA.ID_ARQUIVO);
            return View(lISTA);
        }

        // GET: Listas/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LISTA lISTA = await db.LISTA.FindAsync(id);
            if (lISTA == null)
            {
                return HttpNotFound();
            }
            return View(lISTA);
        }

        // POST: Listas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            LISTA lISTA = await db.LISTA.FindAsync(id);
            //REMOVE AS DEPENDENCIAS
            db.LISTADETALHE.RemoveRange(db.LISTADETALHE.Where(x => x.ID_LISTA.Equals(id)));
            //REMOVE A LISTA
            db.LISTA.Remove(lISTA);
            await db.SaveChangesAsync();
            return RedirectToAction("Index", "Listas", new { idArquivo = lISTA.ID_ARQUIVO });
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
