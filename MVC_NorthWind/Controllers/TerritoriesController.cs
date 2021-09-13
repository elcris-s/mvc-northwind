using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MVC_NorthWind.Models;
using MVC_NorthWind.Models.Model2;

namespace MVC_NorthWind.Controllers
{
    public class TerritoriesController : Controller
    {
        private DBNorthWind db = new DBNorthWind();

        // GET: Territories
        public ActionResult Index()
        {
            return View();
        }

        public async Task<ActionResult> TodosTerritorios()
        {
            var territories = db.Territories.Include(t => t.Region);
            return View(await territories.ToListAsync());
        }

        // GET: Territories/Details/5
        public async Task<ActionResult> Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Territories territories = await db.Territories.FindAsync(id);
            if (territories == null)
            {
                return HttpNotFound();
            }
            return View(territories);
        }

        // GET: Territories/Create
        public ActionResult Create()
        {
            ViewBag.RegionID = new SelectList(db.Region, "RegionID", "RegionDescription");
            return View();
        }

        // POST: Territories/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "TerritoryID,TerritoryDescription,RegionID")] Territories territories)
        {
            territories.TerritoryID = territories.TerritoryID.Trim();
            var a = db.Territories.Where(x => x.TerritoryID == territories.TerritoryID);
            if(a.Count() != 0)
            {
                ViewBag.ErrorMessage = "Ya existe un territorio con dicho ID";
                ViewBag.RegionID = new SelectList(db.Region, "RegionID", "RegionDescription");
                return View();
            }
            if (ModelState.IsValid)
            {
                db.Territories.Add(territories);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.RegionID = new SelectList(db.Region, "RegionID", "RegionDescription", territories.RegionID);
            return View(territories);
        }

        // GET: Territories/Edit/5
        public async Task<ActionResult> Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Territories territories = await db.Territories.FindAsync(id);
            if (territories == null)
            {
                return HttpNotFound();
            }
            ViewBag.RegionID = new SelectList(db.Region, "RegionID", "RegionDescription", territories.RegionID);
            return View(territories);
        }

        // POST: Territories/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "TerritoryID,TerritoryDescription,RegionID")] Territories territories, string id)
        {
            territories.TerritoryID = id.Trim();
            if (ModelState.IsValid)
            {
                db.Entry(territories).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.RegionID = new SelectList(db.Region, "RegionID", "RegionDescription", territories.RegionID);
            return View(territories);
        }

        // GET: Territories/Delete/5
        public async Task<ActionResult> Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Territories territories = await db.Territories.FindAsync(id);
            if (territories == null)
            {
                return HttpNotFound();
            }
            return View(territories);
        }

        // POST: Territories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(string id)
        {
            Territories territories = await db.Territories.FindAsync(id);
            EmpTerrModel dB = new EmpTerrModel();
            var a = dB.EmployeeTerritories.Where(x => x.TerritoryID == territories.TerritoryID).ToList();
            if(a.Count() != 0)
            {
                ViewBag.ErrorMessage = $"No se puede eliminar por relaciones";
                return View(territories);
            }

            db.Territories.Remove(territories);
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
