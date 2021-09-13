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
    public class CategoriesController : Controller
    {
        private DBNorthWind db = new DBNorthWind();

        // GET: Categories
        public ActionResult Index()
        {
            return View();
        }

        public async Task<ActionResult> TodasCategorias()
        {
            return View(await db.Categories.ToListAsync());
        }

        // GET: Categories/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Categories categories = await db.Categories.FindAsync(id);
            if (categories == null)
            {
                return HttpNotFound();
            }
            return View(categories);
        }

        // GET: Categories/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Categories/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "CategoryID,CategoryName,Description,Picture")] Categories categories)
        {
            HttpPostedFileBase file = Request.Files["ImageData"];
            categories.Picture = ConvertToBytes(file);
            if (ModelState.IsValid)
            {
                db.Categories.Add(categories);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(categories);
        }

        public byte[] ConvertToBytes(HttpPostedFileBase image)
        {
            byte[] imageBytes = null;
            BinaryReader reader = new BinaryReader(image.InputStream);
            imageBytes = reader.ReadBytes((int)image.ContentLength);
            return imageBytes;
        }

        // GET: Categories/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Categories categories = await db.Categories.FindAsync(id);
            if (categories == null)
            {
                return HttpNotFound();
            }
            return View(categories);
        }

        // POST: Categories/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "CategoryID,CategoryName,Description,Picture")] Categories categories, string id)
        {
            categories.CategoryID = Convert.ToInt32(id);
            var a = db.Categories.Single(x => x.CategoryID == categories.CategoryID);

            HttpPostedFileBase file = Request.Files["ImageData"];
            if(file.ContentLength != 0)
            {
                categories.Picture = ConvertToBytes(file);
            }
            else { categories.Picture = a.Picture; }
            if (ModelState.IsValid)
            {
                var local = db.Set<Categories>()
                         .Local
                         .FirstOrDefault(f => f.CategoryID == categories.CategoryID);
                if (local != null)
                {
                    db.Entry(local).State = EntityState.Detached;
                }
                db.Entry(categories).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("TodasCategorias");
            }
            return View(categories);
        }

        // GET: Categories/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Categories categories = await db.Categories.FindAsync(id);
            if (categories == null)
            {
                return HttpNotFound();
            }
            return View(categories);
        }

        // POST: Categories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Categories categories = await db.Categories.FindAsync(id);
            if (categories.Products.Count() != 0)
            {
                ViewBag.ErrorMessage = $"La categoria tiene productos registrados, por lo que no se puede eliminar";
                return View(categories);
            }
            db.Categories.Remove(categories);
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
