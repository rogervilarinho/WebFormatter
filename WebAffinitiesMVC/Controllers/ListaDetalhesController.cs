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
    public class ListaDetalhesController : Controller
    {
        private WebFormatterEntities db = new WebFormatterEntities();

        // GET: ListaDetalhes
        public async Task<ActionResult> Index(int? idLista)
        {
            if (!idLista.HasValue) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            var lISTADETALHE = db.LISTADETALHE.Include(l => l.LISTA);
            var retorno = await lISTADETALHE.Where(x => x.ID_LISTA.Equals(idLista.Value)).ToListAsync();
            if (retorno.Count == 0)
            {
                return View(new List<LISTADETALHE>() { new LISTADETALHE() { LISTA = await db.LISTA.FindAsync(idLista.Value) } });
            }
            return View(retorno);
        }

        // GET: ListaDetalhes/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LISTADETALHE lISTADETALHE = await db.LISTADETALHE.FindAsync(id);
            if (lISTADETALHE == null)
            {
                return HttpNotFound();
            }
            return View(lISTADETALHE);
        }

        // GET: ListaDetalhes/Create
        public ActionResult Create(int? idLista)
        {
            if (!idLista.HasValue) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            ViewBag.ID_LISTA = new SelectList(db.LISTA.Where(x => x.ID.Equals(idLista.Value)), "ID", "NOME");
            return View(new LISTADETALHE() { LISTA = db.LISTA.Find(idLista.Value) });
        }

        // POST: ListaDetalhes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "ID,VALOR,ID_LISTA")] LISTADETALHE lISTADETALHE)
        {
            if(db.LISTADETALHE.Where(x => x.ID_LISTA == lISTADETALHE.ID_LISTA && x.VALOR.Trim().Equals(lISTADETALHE.VALOR.Trim())).Count() > 0)
            {
                //ModelStateValue 
                ModelState.AddModelError("Valor", "O valor inserido já existe na lista atual!");
            }
            if (ModelState.IsValid)
            {
                db.LISTADETALHE.Add(lISTADETALHE);
                await db.SaveChangesAsync();
                return RedirectToAction("Index", "ListaDetalhes", new { idLista = lISTADETALHE.ID_LISTA });
            }

            ViewBag.ID_LISTA = new SelectList(db.LISTA.Where(x => x.ID == lISTADETALHE.ID_LISTA), "ID", "NOME", lISTADETALHE.ID_LISTA);
            lISTADETALHE.LISTA = await db.LISTA.FindAsync(lISTADETALHE.ID_LISTA);
            return View(lISTADETALHE);
        }

        // GET: ListaDetalhes/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LISTADETALHE lISTADETALHE = await db.LISTADETALHE.FindAsync(id);
            if (lISTADETALHE == null)
            {
                return HttpNotFound();
            }
            ViewBag.ID_LISTA = new SelectList(db.LISTA.Where(x => x.ID == lISTADETALHE.ID_LISTA), "ID", "NOME", lISTADETALHE.ID_LISTA);
            return View(lISTADETALHE);
        }

        // POST: ListaDetalhes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "ID,VALOR,ID_LISTA")] LISTADETALHE lISTADETALHE)
        {
            if (db.LISTADETALHE.Where(x => x.ID != lISTADETALHE.ID && x.ID_LISTA == lISTADETALHE.ID_LISTA && x.VALOR.Trim().Equals(lISTADETALHE.VALOR.Trim())).Count() > 0)
            {
                //ModelStateValue 
                ModelState.AddModelError("Valor", "O valor inserido já existe na lista atual!");
            }
            if (ModelState.IsValid)
            {
                db.Entry(lISTADETALHE).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index", "ListaDetalhes", new { idLista = lISTADETALHE.ID_LISTA });
            }
            ViewBag.ID_LISTA = new SelectList(db.LISTA.Where(x => x.ID == lISTADETALHE.ID_LISTA), "ID", "NOME", lISTADETALHE.ID_LISTA);
            lISTADETALHE.LISTA = await db.LISTA.FindAsync(lISTADETALHE.ID_LISTA);
            return View(lISTADETALHE);
        }

        // GET: ListaDetalhes/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LISTADETALHE lISTADETALHE = await db.LISTADETALHE.FindAsync(id);
            if (lISTADETALHE == null)
            {
                return HttpNotFound();
            }
            return View(lISTADETALHE);
        }

        // POST: ListaDetalhes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            LISTADETALHE lISTADETALHE = await db.LISTADETALHE.FindAsync(id);
            db.LISTADETALHE.Remove(lISTADETALHE);
            await db.SaveChangesAsync();
            return RedirectToAction("Index", "ListaDetalhes", new { idLista = lISTADETALHE.ID_LISTA });
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
