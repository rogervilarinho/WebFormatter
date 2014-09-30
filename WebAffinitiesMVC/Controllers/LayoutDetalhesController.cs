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
        public ActionResult Create(int? idLayout)
        {
            if (!idLayout.HasValue) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            var layout = db.LAYOUT.Find(idLayout.Value);
            if (layout == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            SetViewBag(idLayout.Value);
            return View(new LAYOUTDETALHE() { LAYOUT = layout, ID_LAYOUT = layout.ID });
        }

        // POST: LayoutDetalhes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "ID,ID_LAYOUT,TAMANHO,ID_TIPO,ID_VALIDACAO,ID_LISTA,ACEITAVEL,FIXO,NOME,ID_HIERARQUIA,OBRIGATORIO")] LAYOUTDETALHE lAYOUTDETALHE)
        {
            if (ModelState.IsValid)
            {               
                //BUSCA O ULTIMO CARACTER DA HIERARQUIA PARA O LAYOUT EM QUESTÃO.
                int? ultimoLayoutDetalhe = await db.LAYOUTDETALHE.Where(x => x.ID_HIERARQUIA.Equals(lAYOUTDETALHE.ID_HIERARQUIA) && x.ID_LAYOUT.Equals(lAYOUTDETALHE.ID_LAYOUT)).MaxAsync(x => x.FIM);
                lAYOUTDETALHE.INICIO = ultimoLayoutDetalhe == null ? 1 : ultimoLayoutDetalhe.Value + 1;
                lAYOUTDETALHE.FIM = ultimoLayoutDetalhe == null ? lAYOUTDETALHE.TAMANHO : lAYOUTDETALHE.INICIO + lAYOUTDETALHE.TAMANHO - 1;
                db.LAYOUTDETALHE.Add(lAYOUTDETALHE);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(lAYOUTDETALHE);
        }

        public void SetViewBag(int idLayout)
        {
            LAYOUT Layout = db.LAYOUT.Find(idLayout);
            ViewBag.ID_LAYOUT = new SelectList(db.LAYOUT.Where(x => x.ID.Equals(Layout.ID)), "ID", "NOME");
            ViewBag.ID_HIERARQUIA = new SelectList(db.HIERARQUIA, "ID", "NOME");
            ViewBag.ID_TIPO = new SelectList(db.TIPO, "ID", "NOME");
            ViewBag.ID_LISTA = new SelectList(db.LISTA.Where(x => x.ARQUIVO.ID.Equals(Layout.ID_ARQUIVO)), "ID", "NOME");
            ViewBag.ID_VALIDACAO = new SelectList(db.VALIDACAO, "ID", "NOME");
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

            SetViewBag(lAYOUTDETALHE.ID_LAYOUT);
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

            SetViewBag(lAYOUTDETALHE.ID_LAYOUT);
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
