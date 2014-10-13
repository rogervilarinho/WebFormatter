using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Linq.Expressions;
using System.Web.Routing;
using System.ComponentModel.DataAnnotations;
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
            var lAYOUTDETALHE = db.LAYOUTDETALHE.Include(l => l.TIPO).Include(l => l.LISTA).Include(l => l.VALIDACAO);
            var retorno = await lAYOUTDETALHE.Where(x => x.ID_LAYOUT.Equals(idLayout.Value)).OrderBy(x => x.FIXO).ThenBy(x => x.ORDEM).ToListAsync();
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
                if (db.LAYOUTDETALHE.Where(x => x.FIXO.ToUpper().Trim().Equals(lAYOUTDETALHE.FIXO.ToUpper().Trim()) && x.ID_LAYOUT.Equals(lAYOUTDETALHE.ID_LAYOUT)).Count() > 0)
                {
                    int ultimoLayoutDetalhe = await db.LAYOUTDETALHE.Where(x => x.FIXO.ToUpper().Trim().Equals(lAYOUTDETALHE.FIXO.ToUpper().Trim()) && x.ID_LAYOUT.Equals(lAYOUTDETALHE.ID_LAYOUT)).MaxAsync(x => x.FIM);
                    lAYOUTDETALHE.INICIO = ultimoLayoutDetalhe + 1;
                    lAYOUTDETALHE.FIM = lAYOUTDETALHE.INICIO + lAYOUTDETALHE.TAMANHO - 1;
                    lAYOUTDETALHE.ORDEM = await db.LAYOUTDETALHE.Where(x => x.FIXO.ToUpper().Trim().Equals(lAYOUTDETALHE.FIXO.ToUpper().Trim()) && x.ID_LAYOUT.Equals(lAYOUTDETALHE.ID_LAYOUT)).MaxAsync(x => x.ORDEM) + 1;                
                }
                else
                {
                    lAYOUTDETALHE.INICIO = 1;
                    lAYOUTDETALHE.FIM = lAYOUTDETALHE.INICIO + lAYOUTDETALHE.TAMANHO - 1;
                    lAYOUTDETALHE.ORDEM = 1;
                }
                
                db.LAYOUTDETALHE.Add(lAYOUTDETALHE);
                await db.SaveChangesAsync();
                return RedirectToAction("Index", "LayoutDetalhes", new { idLayout = lAYOUTDETALHE.ID_LAYOUT });
            }

            lAYOUTDETALHE.LAYOUT = await db.LAYOUT.FindAsync(lAYOUTDETALHE.ID_LAYOUT);
            SetViewBag(lAYOUTDETALHE.ID_LAYOUT);
            return View(lAYOUTDETALHE);
        }
        public void SetViewBag(int idLayout, LAYOUTDETALHE edit)
        {
            LAYOUT Layout = db.LAYOUT.Find(idLayout);
            ViewBag.ID_LAYOUT = new SelectList(db.LAYOUT.Where(x => x.ID.Equals(Layout.ID)), "ID", "NOME", edit.ID_LAYOUT);
            //ViewBag.ID_HIERARQUIA = new SelectList(db.HIERARQUIA, "ID", "NOME", edit.ID_HIERARQUIA);
            ViewBag.ID_TIPO = new SelectList(db.TIPO, "ID", "NOME", edit.ID_TIPO);
            ViewBag.ID_LISTA = new SelectList(db.LISTA.Where(x => x.ARQUIVO.ID.Equals(Layout.ID_ARQUIVO)), "ID", "NOME", edit.ID_LISTA);
            ViewBag.ID_VALIDACAO = new SelectList(db.VALIDACAO, "ID", "NOME", edit.ID_VALIDACAO);
            ViewBag.ORDEM = new SelectList(db.LAYOUTDETALHE.Where(x => x.ID_LAYOUT.Equals(Layout.ID) && x.FIXO.ToUpper().Trim().Equals(edit.FIXO.ToUpper().Trim())).OrderBy(x => x.ORDEM), "ORDEM", "ORDEM", edit.ORDEM);
        }
        public void SetViewBag(int idLayout)
        {
            LAYOUT Layout = db.LAYOUT.Find(idLayout);
            ViewBag.ID_LAYOUT = new SelectList(db.LAYOUT.Where(x => x.ID.Equals(Layout.ID)), "ID", "NOME");
            //ViewBag.ID_HIERARQUIA = new SelectList(db.HIERARQUIA, "ID", "NOME");
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

            SetViewBag(lAYOUTDETALHE.ID_LAYOUT, lAYOUTDETALHE);

            return View(lAYOUTDETALHE);
        }

        // POST: LayoutDetalhes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "ID,ID_LAYOUT,TAMANHO,INICIO,FIM,ID_TIPO,ID_VALIDACAO,ID_LISTA,ACEITAVEL,FIXO,NOME,ID_HIERARQUIA,OBRIGATORIO,ORDEM")] LAYOUTDETALHE lAYOUTDETALHE)
        {
            if (ModelState.IsValid)
            {
                bool ordernar = false;
                var layoutAux = await db.LAYOUTDETALHE.Where(x => x.ID == lAYOUTDETALHE.ID).AsNoTracking().FirstOrDefaultAsync();
                //SE A ORDEM OU TAMANHO MUDOU, ORDENA NOVAMENTE.
                ordernar = !layoutAux.TAMANHO.Equals(lAYOUTDETALHE.TAMANHO) || !layoutAux.ORDEM.Equals(lAYOUTDETALHE.ORDEM);
                db.Entry(lAYOUTDETALHE).State = EntityState.Modified;
                //await db.SaveChangesAsync();
                if (ordernar)
                {
                    //VERIFICA SE O QUE FOI MUDADO É A ORDEM
                    if (!layoutAux.ORDEM.Equals(lAYOUTDETALHE.ORDEM))
                    {
                        //VERIFICA SE A ALTERAÇÃO É PARA CIMA OU PARA BAIXO
                        bool asc = true;
                        if (layoutAux.ORDEM < lAYOUTDETALHE.ORDEM) asc = false;
                        int? ordemAux = lAYOUTDETALHE.ORDEM;
                        if (asc)
                        {
                            //SE SIM, ALTERA A ORDEM DE TODOS OS CAMPOS APÓS O CAMPO ALTERADO.
                            await db.LAYOUTDETALHE.Where(x => x.ID_LAYOUT.Equals(lAYOUTDETALHE.ID_LAYOUT) && x.FIXO.ToUpper().Trim().Equals(lAYOUTDETALHE.FIXO.ToUpper().Trim()) && x.ORDEM >= lAYOUTDETALHE.ORDEM).OrderBy(x => x.ORDEM).ForEachAsync(delegate(LAYOUTDETALHE detalhe)
                            {
                                if (!lAYOUTDETALHE.ID.Equals(detalhe.ID))
                                {
                                    detalhe.ORDEM = ordemAux + 1;
                                    db.Entry(detalhe).State = EntityState.Modified;
                                    ordemAux = ordemAux + 1;
                                }
                            });
                        }
                        else
                        {
                            //SE SIM, ALTERA A ORDEM DE TODOS OS CAMPOS APÓS O CAMPO ALTERADO.
                            await db.LAYOUTDETALHE.Where(x => x.ID_LAYOUT.Equals(lAYOUTDETALHE.ID_LAYOUT) && x.FIXO.ToUpper().Trim().Equals(lAYOUTDETALHE.FIXO.ToUpper().Trim()) && x.ORDEM <= lAYOUTDETALHE.ORDEM).OrderBy(x => x.ORDEM).ForEachAsync(delegate(LAYOUTDETALHE detalhe)
                            {
                                if (!lAYOUTDETALHE.ID.Equals(detalhe.ID))
                                {
                                    detalhe.ORDEM = ordemAux -1;
                                    db.Entry(detalhe).State = EntityState.Modified;
                                    ordemAux = ordemAux - 1;
                                }
                            });
                        }
                        await db.SaveChangesAsync();
                    }
                    int inicio = 1;
                    await db.LAYOUTDETALHE.Where(x => x.ID_LAYOUT.Equals(lAYOUTDETALHE.ID_LAYOUT) && x.FIXO.ToUpper().Trim().Equals(lAYOUTDETALHE.FIXO.ToUpper().Trim())).OrderBy(x => x.ORDEM).ForEachAsync(delegate(LAYOUTDETALHE detalhe)
                    {
                        if (detalhe.ORDEM.Equals(1))
                        {
                            detalhe.INICIO = inicio;
                            detalhe.FIM = detalhe.TAMANHO;
                            inicio = detalhe.FIM + 1;
                        }
                        else
                        {
                            detalhe.INICIO = inicio;
                            detalhe.FIM = inicio + detalhe.TAMANHO - 1;
                            inicio = detalhe.FIM + 1;
                        }
                        db.Entry(detalhe).State = EntityState.Modified;
                    });
                }
                await db.SaveChangesAsync();
                return RedirectToAction("Index", "LayoutDetalhes", new { idLayout = lAYOUTDETALHE.ID_LAYOUT });
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
            int ordemAux = 0;
            await db.LAYOUTDETALHE.Where(x => x.ID_LAYOUT.Equals(lAYOUTDETALHE.ID_LAYOUT) && x.FIXO.ToUpper().Trim().Equals(lAYOUTDETALHE.FIXO.ToUpper().Trim())).OrderBy(x => x.ORDEM).ForEachAsync(delegate(LAYOUTDETALHE detalhe)
            {
                if (!lAYOUTDETALHE.ID.Equals(detalhe.ID))
                {
                    detalhe.ORDEM = ordemAux + 1;
                    db.Entry(detalhe).State = EntityState.Modified;
                    ordemAux = ordemAux + 1;
                }
            });
            await db.SaveChangesAsync();
            int inicio = 1;
            await db.LAYOUTDETALHE.Where(x => x.ID_LAYOUT.Equals(lAYOUTDETALHE.ID_LAYOUT) && x.FIXO.ToUpper().Trim().Equals(lAYOUTDETALHE.FIXO.ToUpper().Trim())).OrderBy(x => x.ORDEM).ForEachAsync(delegate(LAYOUTDETALHE detalhe)
            {
                if (detalhe.ORDEM.Equals(1))
                {
                    detalhe.INICIO = inicio;
                    detalhe.FIM = detalhe.TAMANHO;
                    inicio = detalhe.FIM + 1;
                }
                else
                {
                    detalhe.INICIO = inicio;
                    detalhe.FIM = inicio + detalhe.TAMANHO - 1;
                    inicio = detalhe.FIM + 1;
                }
                db.Entry(detalhe).State = EntityState.Modified;
            });
            await db.SaveChangesAsync();
            return RedirectToAction("Index", "LayoutDetalhes", new { idLayout = lAYOUTDETALHE.ID_LAYOUT });
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
