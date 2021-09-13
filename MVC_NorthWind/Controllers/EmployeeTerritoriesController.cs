using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MVC_NorthWind.Models.Model2;
using MVC_NorthWind.Models;

namespace MVC_NorthWind.Controllers
{
    public class EmployeeTerritoriesController : Controller
    {
        private EmpTerrModel db = new EmpTerrModel();

        // GET: EmployeeTerritories
        public ActionResult Index()
        {
            return View();
        }
        public async Task<ActionResult> TodosEmpTer()
        {
            return View(await db.EmployeeTerritories.ToListAsync());
        }

        // GET: EmployeeTerritories/Details/5
        public async Task<ActionResult> Details(string id)
        {
    
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            string[] ID = id.Split();
            int employ = Convert.ToInt32(ID[0]);
            string territ = ID[1];

            var employeeTerritories = await db.EmployeeTerritories.Where(x => x.EmployeeID == employ && x.TerritoryID == territ).SingleAsync();

            if (employeeTerritories == null)
            {
                return HttpNotFound();
            }
            return View(employeeTerritories);
        }

        // GET: EmployeeTerritories/Create
        public ActionResult Create()
        {
            DBNorthWind dB = new DBNorthWind();
            ViewBag.EmployeeID = new SelectList(dB.Employees, "EmployeeID", "LastName");
            ViewBag.TerritoryID = new SelectList(dB.Territories, "TerritoryID", "TerritoryDescription");

            return View();
        }

        // POST: EmployeeTerritories/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "EmployeeID,TerritoryID")] EmployeeTerritories employeeTerritories)
        {
            if (ModelState.IsValid)
            {
                db.EmployeeTerritories.Add(employeeTerritories);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(employeeTerritories);
        }

        // GET: EmployeeTerritories/Edit/5
        public async Task<ActionResult> Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            string[] ID = id.Split();
            int employ = Convert.ToInt32(ID[0]);
            string territ = ID[1];

            var employeeTerritories = await db.EmployeeTerritories.Where(x => x.EmployeeID == employ && x.TerritoryID == territ).SingleAsync();

            if (employeeTerritories == null)
            {
                return HttpNotFound();
            }
            DBNorthWind dB = new DBNorthWind();

            ViewBag.EmployeeID = new SelectList(dB.Employees, "EmployeeID", "LastName");
            ViewBag.TerritoryID = new SelectList(dB.Territories, "TerritoryID", "TerritoryDescription");
            return View(employeeTerritories);
        }

        // POST: EmployeeTerritories/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "EmployeeID,TerritoryID")] EmployeeTerritories employeeTerritories)
        {
            if (ModelState.IsValid)
            {
                db.Entry(employeeTerritories).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(employeeTerritories);
        }

        // GET: EmployeeTerritories/Delete/5
        public async Task<ActionResult> Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            string[] ID = id.Split();
            int employ = Convert.ToInt32(ID[0]);
            string territ = ID[1];

            var employeeTerritories = await db.EmployeeTerritories.Where(x => x.EmployeeID == employ && x.TerritoryID == territ).SingleAsync();

            if (employeeTerritories == null)
            {
                return HttpNotFound();
            }
            return View(employeeTerritories);
        }

        // POST: EmployeeTerritories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(string id)
        {
            string[] ID = id.Split();
            int employ = Convert.ToInt32(ID[0]);
            string territ = ID[1];

            var employeeTerritories = await db.EmployeeTerritories.Where(x => x.EmployeeID == employ && x.TerritoryID == territ).SingleAsync();
            db.EmployeeTerritories.Remove(employeeTerritories);
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
