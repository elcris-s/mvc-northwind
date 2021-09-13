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
using System.IO;

namespace MVC_NorthWind.Controllers
{
    public class EmployeesController : Controller
    {
        private DBNorthWind db = new DBNorthWind();

        // GET: Employees
        public ActionResult Index()
        {
            return View();
        }

        public async Task<ActionResult> TodosEmpleados()
        {
            var employees = db.Employees.Include(e => e.Employees2);
            return View(await employees.ToListAsync());
        }
        // GET: Employees/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Employees employees = await db.Employees.FindAsync(id);
            if (employees == null)
            {
                return HttpNotFound();
            }
            return View(employees);
        }

        // GET: Employees/Create
        public ActionResult Create()
        {
            ViewBag.ReportsTo = db.Employees.Select(x => new SelectListItem
            {
                Text = x.FirstName + " " + x.LastName,
                Value = x.EmployeeID.ToString()
            });
            return View();
        }

        // POST: Employees/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "EmployeeID,LastName,FirstName,Title,TitleOfCourtesy,BirthDate,HireDate,Address,City,Region,PostalCode,Country,HomePhone,Extension,Photo,Notes,ReportsTo,PhotoPath")] Employees employees)
        {
            HttpPostedFileBase file = Request.Files["ImageData"];
            employees.Photo = ConvertToBytes(file);
            employees.PhotoPath = "BD";
            if (ModelState.IsValid)
            {
                db.Employees.Add(employees);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.ReportsTo = db.Employees.Select(x => new SelectListItem
            {
                Text = x.FirstName + " " + x.LastName,
                Value = x.EmployeeID.ToString()
            });
            return View(employees);
        }
        public byte[] ConvertToBytes(HttpPostedFileBase image)
        {
            byte[] imageBytes = null;
            BinaryReader reader = new BinaryReader(image.InputStream);
            imageBytes = reader.ReadBytes((int)image.ContentLength);
            return imageBytes;
        }

        // GET: Employees/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Employees employees = await db.Employees.FindAsync(id);
            if (employees == null)
            {
                return HttpNotFound();
            }
            ViewBag.ReportsTo = db.Employees.Select(x => new SelectListItem
            {
                Text = x.FirstName + " " + x.LastName,
                Value = x.EmployeeID.ToString()
            });
            return View(employees);
        }

        // POST: Employees/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "EmployeeID,LastName,FirstName,Title,TitleOfCourtesy,BirthDate,HireDate,Address,City,Region,PostalCode,Country,HomePhone,Extension,Photo,Notes,ReportsTo,PhotoPath")] Employees employees, string id)
        {
            employees.EmployeeID = Convert.ToInt32(id);
            var emp = await db.Employees.FindAsync(employees.EmployeeID);
            HttpPostedFileBase file = Request.Files["ImageData"];
            if(file.ContentLength == 0)
            {
                employees.Photo = emp.Photo;
            }
            else
            {
                employees.Photo = ConvertToBytes(file);
            }

            if (ModelState.IsValid)
            {
                var local = db.Set<Employees>()
                         .Local
                         .FirstOrDefault(f => f.EmployeeID == employees.EmployeeID);
                if (local != null)
                {
                    db.Entry(local).State = EntityState.Detached;
                }
                db.Entry(employees).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.ReportsTo = db.Employees.Select(x => new SelectListItem
            {
                Text = x.FirstName + " " + x.LastName,
                Value = x.EmployeeID.ToString()
            });
            return View(employees);
        }

        // GET: Employees/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Employees employees = await db.Employees.FindAsync(id);
            if (employees == null)
            {
                return HttpNotFound();
            }
            return View(employees);
        }


        // POST: Employees/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Employees employees = await db.Employees.FindAsync(id);
            if (employees.Orders.Count() != 0)
            {
                ViewBag.ErrorMessage = $"El empleado tiene ordenes registradas, por lo que no se puede eliminar";
                return View(employees);
            }

            if (db.Employees.Where(x => x.ReportsTo == id) != null)
            {
                var lst = db.Employees.Where(x => x.ReportsTo == id);
                foreach (var x in lst.ToList())
                {
                    x.ReportsTo = null;
                    var local = db.Set<Employees>()
                         .Local
                         .FirstOrDefault(f => f.EmployeeID == x.EmployeeID);
                    if (local != null)
                    {
                        db.Entry(local).State = EntityState.Detached;
                    }
                    db.Entry(x).State = EntityState.Modified;
                    await db.SaveChangesAsync();
                }
            }

            db.Employees.Remove(employees);
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
