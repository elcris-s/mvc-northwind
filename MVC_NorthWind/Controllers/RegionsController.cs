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

namespace MVC_NorthWind.Controllers
{
    public class RegionsController : Controller
    {
        private DBNorthWind db = new DBNorthWind();

        // GET: Regions
        public ActionResult Index()
        {
            return View();
        }

        public async Task<ActionResult> TodasRegiones()
        {
            return View(await db.Region.ToListAsync());
        }

        // GET: Regions/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Region region = await db.Region.FindAsync(id);
            if (region == null)
            {
                return HttpNotFound();
            }
            return View(region);
        }

        // GET: Regions/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Regions/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "RegionID,RegionDescription")] Region region)
        {
            var a = db.Region.Where(x => x.RegionID == region.RegionID);
            if(a.Count() != 0)
            {
                ViewBag.ErrorMessage = "Ya existe una region con dicho ID";
                return View();
            }
            if (ModelState.IsValid)
            {
                db.Region.Add(region);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(region);
        }

        // GET: Regions/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Region region = await db.Region.FindAsync(id);
            if (region == null)
            {
                return HttpNotFound();
            }
            return View(region);
        }

        // POST: Regions/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "RegionID,RegionDescription")] Region region, string id)
        {
            region.RegionID = Convert.ToInt32(id);
            if (ModelState.IsValid)
            {
                db.Entry(region).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(region);
        }

        // GET: Regions/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Region region = await db.Region.FindAsync(id);
            if (region == null)
            {
                return HttpNotFound();
            }
            return View(region);
        }

        // POST: Regions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Region region = await db.Region.FindAsync(id);
            if(region.Territories.Count() != 0)
            {
                ViewBag.ErrorMessage = $"La region tiene territorios registrados, por lo que no se puede eliminar";
                return View(region);
            }
            db.Region.Remove(region);
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
