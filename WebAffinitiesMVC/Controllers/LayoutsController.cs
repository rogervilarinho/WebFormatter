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
    public class LayoutsController : Controller
    {
        private WebFormatterEntities db = new WebFormatterEntities();

        // GET: Layouts
        public async Task<ActionResult> Index(int? idArquivo)
        {
            if (!idArquivo.HasValue) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            var lAYOUT = db.LAYOUT.Include(l => l.ARQUIVO);
            var retorno = await lAYOUT.Where(x => x.ID_ARQUIVO.Equals(idArquivo.Value)).ToListAsync();
            if (retorno.Count == 0)
            {
                return View(new List<LAYOUT>() { new LAYOUT() { ARQUIVO = await db.ARQUIVO.FindAsync(idArquivo.Value) } });
            }
            return View(retorno);
        }

        // GET: Layouts/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LAYOUT lAYOUT = await db.LAYOUT.FindAsync(id);
            if (lAYOUT == null)
            {
                return HttpNotFound();
            }
            return View(lAYOUT);
        }

        // GET: Layouts/Create
        public ActionResult Create(int? idArquivo)
        {
            if (!idArquivo.HasValue) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            ViewBag.ID_ARQUIVO = new SelectList(db.ARQUIVO.Where(x => x.ID.Equals(idArquivo.Value)), "ID", "NOME");
            return View(new LAYOUT() { ARQUIVO = db.ARQUIVO.Find(idArquivo.Value) });
        }

        // POST: Layouts/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "ID,NOME,DESCRICAO,ID_ARQUIVO")] LAYOUT lAYOUT)
        {
            if (ModelState.IsValid)
            {
                db.LAYOUT.Add(lAYOUT);
                await db.SaveChangesAsync();
                return RedirectToAction("Index", "Layouts", new { idArquivo = lAYOUT.ID_ARQUIVO });
            }
            ViewBag.ID_ARQUIVO = new SelectList(db.ARQUIVO.Where(x => x.ID.Equals(lAYOUT.ID_ARQUIVO)), "ID", "NOME", lAYOUT.ID_ARQUIVO);
            lAYOUT.ARQUIVO = await db.ARQUIVO.FindAsync(lAYOUT.ID_ARQUIVO);
            return View(lAYOUT);
        }

        // GET: Layouts/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LAYOUT lAYOUT = await db.LAYOUT.FindAsync(id);
            if (lAYOUT == null)
            {
                return HttpNotFound();
            }
            ViewBag.ID_ARQUIVO = new SelectList(db.ARQUIVO, "ID", "NOME", lAYOUT.ID_ARQUIVO);
            return View(lAYOUT);
        }

        // POST: Layouts/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "ID,NOME,DESCRICAO,ID_ARQUIVO")] LAYOUT lAYOUT)
        {
            if (ModelState.IsValid)
            {
                db.Entry(lAYOUT).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.ID_ARQUIVO = new SelectList(db.ARQUIVO, "ID", "NOME", lAYOUT.ID_ARQUIVO);
            return View(lAYOUT);
        }

        // GET: Layouts/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LAYOUT lAYOUT = await db.LAYOUT.FindAsync(id);
            if (lAYOUT == null)
            {
                return HttpNotFound();
            }
            return View(lAYOUT);
        }

        // POST: Layouts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            LAYOUT lAYOUT = await db.LAYOUT.FindAsync(id);
            db.LAYOUT.Remove(lAYOUT);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
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
