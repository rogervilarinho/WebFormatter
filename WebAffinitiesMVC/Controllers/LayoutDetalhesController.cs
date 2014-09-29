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
    public class LayoutDetalhesController : Controller
    {
        private WebFormatterEntities db = new WebFormatterEntities();

        // GET: LayoutDetalhes
        public async Task<ActionResult> Index(int? idLayout)
        {
            if (!idLayout.HasValue) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            var lAYOUTDETALHE = db.LAYOUTDETALHE.Include(l => l.HIERARQUIA).Include(l => l.TIPO).Include(l => l.LISTA).Include(l => l.VALIDACAO);
            var retorno = await lAYOUTDETALHE.Where(x => x.ID_LAYOUT.Equals(idLayout.Value)).ToListAsync();
            if (retorno.Count == 0)
            {
                return View(new List<LAYOUTDETALHE>() { new LAYOUTDETALHE() { LAYOUT = await db.LAYOUT.FindAsync(idLayout.Value) } });
            }
            return View(retorno);
        }

        // GET: LayoutDetalhes/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LAYOUTDETALHE lAYOUTDETALHE = await db.LAYOUTDETALHE.FindAsync(id);
            if (lAYOUTDETALHE == null)
            {
                return HttpNotFound();
            }
            return View(lAYOUTDETALHE);
        }

        // GET: LayoutDetalhes/Create
        public ActionResult Create()
        {
            ViewBag.ID_HIERARQUIA = new SelectList(db.HIERARQUIA, "ID", "NOME");
            ViewBag.ID_TIPO = new SelectList(db.TIPO, "ID", "NOME");
            ViewBag.ID_LISTA = new SelectList(db.LISTA, "ID", "NOME");
            ViewBag.ID_VALIDACAO = new SelectList(db.VALIDACAO, "ID", "NOME");
            return View();
        }

        // POST: LayoutDetalhes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "ID,ID_LAYOUT,TAMANHO,INICIO,FIM,ID_TIPO,ID_VALIDACAO,ID_LISTA,ACEITAVEL,FIXO,NOME,ID_HIERARQUIA,OBRIGATORIO")] LAYOUTDETALHE lAYOUTDETALHE)
        {
            if (ModelState.IsValid)
            {
                db.LAYOUTDETALHE.Add(lAYOUTDETALHE);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.ID_HIERARQUIA = new SelectList(db.HIERARQUIA, "ID", "NOME", lAYOUTDETALHE.ID_HIERARQUIA);
            ViewBag.ID_TIPO = new SelectList(db.TIPO, "ID", "NOME", lAYOUTDETALHE.ID_TIPO);
            ViewBag.ID_LISTA = new SelectList(db.LISTA, "ID", "NOME", lAYOUTDETALHE.ID_LISTA);
            ViewBag.ID_VALIDACAO = new SelectList(db.VALIDACAO, "ID", "NOME", lAYOUTDETALHE.ID_VALIDACAO);
            return View(lAYOUTDETALHE);
        }

        // GET: LayoutDetalhes/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LAYOUTDETALHE lAYOUTDETALHE = await db.LAYOUTDETALHE.FindAsync(id);
            if (lAYOUTDETALHE == null)
            {
                return HttpNotFound();
            }
            ViewBag.ID_HIERARQUIA = new SelectList(db.HIERARQUIA, "ID", "NOME", lAYOUTDETALHE.ID_HIERARQUIA);
            ViewBag.ID_TIPO = new SelectList(db.TIPO, "ID", "NOME", lAYOUTDETALHE.ID_TIPO);
            ViewBag.ID_LISTA = new SelectList(db.LISTA, "ID", "NOME", lAYOUTDETALHE.ID_LISTA);
            ViewBag.ID_VALIDACAO = new SelectList(db.VALIDACAO, "ID", "NOME", lAYOUTDETALHE.ID_VALIDACAO);
            return View(lAYOUTDETALHE);
        }

        // POST: LayoutDetalhes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "ID,ID_LAYOUT,TAMANHO,INICIO,FIM,ID_TIPO,ID_VALIDACAO,ID_LISTA,ACEITAVEL,FIXO,NOME,ID_HIERARQUIA,OBRIGATORIO")] LAYOUTDETALHE lAYOUTDETALHE)
        {
            if (ModelState.IsValid)
            {
                db.Entry(lAYOUTDETALHE).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.ID_HIERARQUIA = new SelectList(db.HIERARQUIA, "ID", "NOME", lAYOUTDETALHE.ID_HIERARQUIA);
            ViewBag.ID_TIPO = new SelectList(db.TIPO, "ID", "NOME", lAYOUTDETALHE.ID_TIPO);
            ViewBag.ID_LISTA = new SelectList(db.LISTA, "ID", "NOME", lAYOUTDETALHE.ID_LISTA);
            ViewBag.ID_VALIDACAO = new SelectList(db.VALIDACAO, "ID", "NOME", lAYOUTDETALHE.ID_VALIDACAO);
            return View(lAYOUTDETALHE);
        }

        // GET: LayoutDetalhes/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LAYOUTDETALHE lAYOUTDETALHE = await db.LAYOUTDETALHE.FindAsync(id);
            if (lAYOUTDETALHE == null)
            {
                return HttpNotFound();
            }
            return View(lAYOUTDETALHE);
        }

        // POST: LayoutDetalhes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            LAYOUTDETALHE lAYOUTDETALHE = await db.LAYOUTDETALHE.FindAsync(id);
            db.LAYOUTDETALHE.Remove(lAYOUTDETALHE);
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
