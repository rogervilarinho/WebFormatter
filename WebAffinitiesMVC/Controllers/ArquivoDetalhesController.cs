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
    public class ArquivoDetalhesController : Controller
    {
        private WebFormatterEntities db = new WebFormatterEntities();

        // GET: ArquivoDetalhes
        public async Task<ActionResult> Index()
        {
            var aRQUIVODETALHE = db.ARQUIVODETALHE.Include(a => a.ARQUIVO.ID).Include(a => a.LAYOUT);
            return View(await aRQUIVODETALHE.ToListAsync());
        }

        // GET: ArquivoDetalhes/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ARQUIVODETALHE aRQUIVODETALHE = await db.ARQUIVODETALHE.FindAsync(id);
            if (aRQUIVODETALHE == null)
            {
                return HttpNotFound();
            }
            return View(aRQUIVODETALHE);
        }

        // GET: ArquivoDetalhes/Create
        public ActionResult Create(int? idArquivo)
        {
            if (!idArquivo.HasValue) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            ViewBag.ID_ARQUIVO = new SelectList(db.ARQUIVO, "ID", "NOME");
            ViewBag.ID_LAYOUT = new SelectList(db.LAYOUT, "ID", "NOME");
            return View(new ARQUIVODETALHE() { ARQUIVO = db.ARQUIVO.Find(idArquivo.Value) });
        }

        // POST: ArquivoDetalhes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "ID,NOME,DESCRICAO,ID_ARQUIVO,ID_LAYOUT,CRIACAO,ALTERACAO")] ARQUIVODETALHE aRQUIVODETALHE)
        {
            if (ModelState.IsValid)
            {
                db.ARQUIVODETALHE.Add(aRQUIVODETALHE);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.ID_ARQUIVO = new SelectList(db.ARQUIVO, "ID", "NOME", aRQUIVODETALHE.ID_ARQUIVO);
            ViewBag.ID_LAYOUT = new SelectList(db.LAYOUT, "ID", "NOME", aRQUIVODETALHE.ID_LAYOUT);
            aRQUIVODETALHE.ARQUIVO = db.ARQUIVO.Find(aRQUIVODETALHE.ID_ARQUIVO);
            return View(aRQUIVODETALHE);
        }

        // GET: ArquivoDetalhes/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ARQUIVODETALHE aRQUIVODETALHE = await db.ARQUIVODETALHE.FindAsync(id);
            if (aRQUIVODETALHE == null)
            {
                return HttpNotFound();
            }
            ViewBag.ID_ARQUIVO = new SelectList(db.ARQUIVO, "ID", "NOME", aRQUIVODETALHE.ID_ARQUIVO);
            ViewBag.ID_LAYOUT = new SelectList(db.LAYOUT, "ID", "NOME", aRQUIVODETALHE.ID_LAYOUT);
            return View(aRQUIVODETALHE);
        }

        // POST: ArquivoDetalhes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "ID,NOME,DESCRICAO,ID_ARQUIVO,ID_LAYOUT,CRIACAO,ALTERACAO")] ARQUIVODETALHE aRQUIVODETALHE)
        {
            if (ModelState.IsValid)
            {
                db.Entry(aRQUIVODETALHE).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.ID_ARQUIVO = new SelectList(db.ARQUIVO, "ID", "NOME", aRQUIVODETALHE.ID_ARQUIVO);
            ViewBag.ID_LAYOUT = new SelectList(db.LAYOUT, "ID", "NOME", aRQUIVODETALHE.ID_LAYOUT);
            return View(aRQUIVODETALHE);
        }

        // GET: ArquivoDetalhes/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ARQUIVODETALHE aRQUIVODETALHE = await db.ARQUIVODETALHE.FindAsync(id);
            if (aRQUIVODETALHE == null)
            {
                return HttpNotFound();
            }
            return View(aRQUIVODETALHE);
        }

        // POST: ArquivoDetalhes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            ARQUIVODETALHE aRQUIVODETALHE = await db.ARQUIVODETALHE.FindAsync(id);
            db.ARQUIVODETALHE.Remove(aRQUIVODETALHE);
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
