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
    public class CustomerDemographicsController : Controller
    {
        private DBNorthWind db = new DBNorthWind();

        // GET: CustomerDemographics
        public ActionResult Index()
        {
            return View();
        }

        public async Task<ActionResult> TodosDemographics()
        {
            return View(await db.CustomerDemographics.ToListAsync());
        }

        // GET: CustomerDemographics/Details/5
        public async Task<ActionResult> Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CustomerDemographics customerDemographics = await db.CustomerDemographics.FindAsync(id);
            if (customerDemographics == null)
            {
                return HttpNotFound();
            }
            return View(customerDemographics);
        }

        // GET: CustomerDemographics/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: CustomerDemographics/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "CustomerTypeID,CustomerDesc")] CustomerDemographics customerDemographics)
        {
            if (ModelState.IsValid)
            {
                db.CustomerDemographics.Add(customerDemographics);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(customerDemographics);
        }

        // GET: CustomerDemographics/Edit/5
        public async Task<ActionResult> Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CustomerDemographics customerDemographics = await db.CustomerDemographics.FindAsync(id);
            customerDemographics.CustomerTypeID = customerDemographics.CustomerTypeID.Trim();
            if (customerDemographics == null)
            {
                return HttpNotFound();
            }
            return View(customerDemographics);
        }

        // POST: CustomerDemographics/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "CustomerTypeID,CustomerDesc")] CustomerDemographics customerDemographics, string id)
        {
            customerDemographics.CustomerTypeID = id;
            if (ModelState.IsValid)
            {
                db.Entry(customerDemographics).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToRoute(new
                {
                    controller = "CustomerDemographics",
                    action = "Details",
                    id = customerDemographics.CustomerTypeID
                });
            }
            return View(customerDemographics);
        }

        // GET: CustomerDemographics/Delete/5
        public async Task<ActionResult> Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CustomerDemographics customerDemographics = await db.CustomerDemographics.FindAsync(id);
            if (customerDemographics == null)
            {
                return HttpNotFound();
            }
            return View(customerDemographics);
        }

        // POST: CustomerDemographics/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(string id)
        {
            CustomerDemographics customerDemographics = await db.CustomerDemographics.FindAsync(id);
            db.CustomerDemographics.Remove(customerDemographics);
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
