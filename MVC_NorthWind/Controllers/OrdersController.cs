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
using RestSharp;
using Newtonsoft.Json;

namespace MVC_NorthWind.Controllers
{
    public class OrdersController : Controller
    {
        private DBNorthWind db = new DBNorthWind();
        string url = "https://localhost:44311";
        // GET: Orders
        public ActionResult Index()
        {
            return View();
        }
        public async Task<ActionResult> TodasOrdenes()
        {
            var client = new RestClient(url);
            var request = new RestRequest($"api/Orders", Method.GET);
            IRestResponse<List<Orders>> response = await client.ExecuteAsync<List<Orders>>(request);
            List<Orders> result = JsonConvert.DeserializeObject<List<Orders>>(response.Content);
            return View(result);
        }

        // GET: Orders/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var client = new RestClient(url);
            var request = new RestRequest($"api/Orders/{id}", Method.GET);
            IRestResponse<List<Orders>> response = await client.ExecuteAsync<List<Orders>>(request);
            Orders orders = JsonConvert.DeserializeObject<Orders>(response.Content);

            if (orders == null)
            {
                return HttpNotFound();
            }
            return View(orders);
        }

        // GET: Orders/Create
        public ActionResult Create()
        {
            ViewBag.CustomerID = new SelectList(db.Customers, "CustomerID", "CompanyName");
            ViewBag.EmployeeID = new SelectList(db.Employees, "EmployeeID", "LastName");
            ViewBag.ShipVia = new SelectList(db.Shippers, "ShipperID", "CompanyName");
            ViewBag.ProductID = new SelectList(db.Products, "ProductID", "ProductName");

            return View();
        }

        // POST: Orders/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "OrderID,CustomerID,EmployeeID,OrderDate,RequiredDate,ShippedDate,ShipVia,Freight,ShipName,ShipAddress,ShipCity,ShipRegion,ShipPostalCode,ShipCountry")] Orders orders)
        {
            var fecha1 = Convert.ToDateTime(orders.OrderDate).ToString("d");
            var fecha2 = Convert.ToDateTime(orders.RequiredDate).ToString("d");
            var fecha3 = Convert.ToDateTime(orders.ShippedDate).ToString("d");

            if (ModelState.IsValid)
            {
                var client = new RestClient(url);
                var request = new RestRequest("/api/Orders", Method.POST);
                request.AddParameter("OrderID", orders.OrderID);
                request.AddParameter("CustomerID", orders.CustomerID);
                request.AddParameter("EmployeeID", orders.EmployeeID);
                request.AddParameter("OrderDate", fecha1);
                request.AddParameter("RequiredDate", fecha2);
                request.AddParameter("ShippedDate", fecha3);
                request.AddParameter("ShipVia", orders.ShipVia);
                request.AddParameter("Freight", orders.Freight);
                request.AddParameter("ShipName", orders.ShipName);
                request.AddParameter("ShipAddress", orders.ShipAddress);
                request.AddParameter("ShipCity", orders.ShipCity);
                request.AddParameter("ShipRegion", orders.ShipRegion);
                request.AddParameter("ShipPostalCode", orders.ShipPostalCode);
                request.AddParameter("ShipCountry", orders.ShipCountry);

                await client.ExecuteAsync(request);
                ViewBag.CustomerID = new SelectList(db.Customers, "CustomerID", "CompanyName", orders.CustomerID);
                ViewBag.EmployeeID = new SelectList(db.Employees, "EmployeeID", "LastName", orders.EmployeeID);
                ViewBag.ShipVia = new SelectList(db.Shippers, "ShipperID", "CompanyName", orders.ShipVia);
                ViewBag.ProductID = new SelectList(db.Products, "ProductID", "ProductName");
                var a = db.Orders.ToList().Last().OrderID;
                ViewBag.OrderID = a;

                return View(orders);
                //return RedirectToAction("TodasOrdenes");
            }

            ViewBag.CustomerID = new SelectList(db.Customers, "CustomerID", "CompanyName", orders.CustomerID);
            ViewBag.EmployeeID = new SelectList(db.Employees, "EmployeeID", "LastName", orders.EmployeeID);
            ViewBag.ShipVia = new SelectList(db.Shippers, "ShipperID", "CompanyName", orders.ShipVia);
            ViewBag.ProductID = new SelectList(db.Products, "ProductID", "ProductName");

            return View(orders);
        }

        [HttpPost]
        public async Task<ActionResult> CreateDetail(string idorden, string price, string quant, string disc)
        {
            var productid = Request.Form["ProductID"];

            var client = new RestClient(url);
            var request = new RestRequest("/api/Order_Details", Method.POST);
            request.AddParameter("OrderID", idorden);
            request.AddParameter("ProductID", productid);
            request.AddParameter("UnitPrice", price);
            request.AddParameter("Quantity", quant);
            request.AddParameter("Discount", disc);

            await client.ExecuteAsync(request);

            return RedirectToRoute(new
            {
                controller = "Order_Details",
                action = "Details",
                id = $"{idorden} {productid}"
            });
        }


        // GET: Orders/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var client = new RestClient(url);
            var request = new RestRequest($"api/Orders/{id}", Method.GET);
            IRestResponse<List<Orders>> response = await client.ExecuteAsync<List<Orders>>(request);
            Orders orders = JsonConvert.DeserializeObject<Orders>(response.Content);
            if (orders == null)
            {
                return HttpNotFound();
            }
            ViewBag.CustomerID = new SelectList(db.Customers, "CustomerID", "CompanyName", orders.CustomerID);
            ViewBag.EmployeeID = new SelectList(db.Employees, "EmployeeID", "LastName", orders.EmployeeID);
            ViewBag.ShipVia = new SelectList(db.Shippers, "ShipperID", "CompanyName", orders.ShipVia);
            return View(orders);
        }

        // POST: Orders/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "OrderID,CustomerID,EmployeeID,OrderDate,RequiredDate,ShippedDate,ShipVia,Freight,ShipName,ShipAddress,ShipCity,ShipRegion,ShipPostalCode,ShipCountry")] Orders orders, string id)
        {
            orders.OrderID = Convert.ToInt32(id);
            Orders orderss = await db.Orders.FindAsync(orders.OrderID);

            string fecha1 = string.Empty;
            string fecha2 = string.Empty;
            string fecha3 = string.Empty;

            if (orders.OrderDate == null)
            {
                fecha1 = Convert.ToDateTime(orderss.OrderDate).ToString("yyyy-MM-dd");
            }
            if(orders.RequiredDate == null)
            {
                fecha2 = Convert.ToDateTime(orderss.RequiredDate).ToString("yyyy-MM-dd");
            }
            if (orders.ShippedDate == null)
            {
                fecha3 = Convert.ToDateTime(orderss.ShippedDate).ToString("yyyy-MM-dd");
            }
            if (ModelState.IsValid)
            {
                var client = new RestClient(url);
                var request = new RestRequest($"/api/Orders/{orders.OrderID}", Method.PUT);
                request.AddParameter("OrderID", orders.OrderID);
                request.AddParameter("CustomerID", orders.CustomerID);
                request.AddParameter("EmployeeID", orders.EmployeeID);
                request.AddParameter("OrderDate", fecha1);
                request.AddParameter("RequiredDate", fecha2);
                request.AddParameter("ShippedDate", fecha3);
                request.AddParameter("ShipVia", orders.ShipVia);
                request.AddParameter("Freight", orders.Freight);
                request.AddParameter("ShipName", orders.ShipName);
                request.AddParameter("ShipAddress", orders.ShipAddress);
                request.AddParameter("ShipCity", orders.ShipCity);
                request.AddParameter("ShipRegion", orders.ShipRegion);
                request.AddParameter("ShipPostalCode", orders.ShipPostalCode);
                request.AddParameter("ShipCountry", orders.ShipCountry);
                await client.ExecuteAsync(request);
            }
            ViewBag.CustomerID = new SelectList(db.Customers, "CustomerID", "CompanyName", orders.CustomerID);
            ViewBag.EmployeeID = new SelectList(db.Employees, "EmployeeID", "LastName", orders.EmployeeID);
            ViewBag.ShipVia = new SelectList(db.Shippers, "ShipperID", "CompanyName", orders.ShipVia);
            return RedirectToRoute(new
            {
                controller = "Orders",
                action = "Details",
                id = orders.OrderID
            });
        }

        // GET: Orders/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var client = new RestClient(url);
            var request = new RestRequest($"api/Orders/{id}", Method.GET);
            IRestResponse<List<Orders>> response = await client.ExecuteAsync<List<Orders>>(request);
            Orders orders = JsonConvert.DeserializeObject<Orders>(response.Content);
            if (orders == null)
            {
                return HttpNotFound();
            }
            return View(orders);
        }

        // POST: Orders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Orders orders = db.Orders.Where(x => x.OrderID == id).Single();
            if(orders.Order_Details.Count() != 0)
            {
                ViewBag.ErrorMessage = $"La orden tiene detalles registradas, por lo que no se puede eliminar";
                return View(orders);
            }

            var client = new RestClient(url);
            var request = new RestRequest($"/api/Orders/{id}", Method.DELETE);
            await client.ExecuteAsync(request);

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
