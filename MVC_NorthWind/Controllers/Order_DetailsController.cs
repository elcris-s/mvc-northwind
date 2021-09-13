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
    public class Order_DetailsController : Controller
    {
        private DBNorthWind db = new DBNorthWind();
        string url = "https://localhost:44311";

        // GET: Order_Details
        public ActionResult Index()
        {
            return View();
        }

        public async Task<ActionResult> TodosDetalles()
        {
            var client = new RestClient(url);
            var request = new RestRequest($"api/Order_Details", Method.GET);
            IRestResponse<List<Order_Details>> response = await client.ExecuteAsync<List<Order_Details>>(request);
            List<Order_Details> result = JsonConvert.DeserializeObject<List<Order_Details>>(response.Content);
            return View(result);
        }

        // GET: Order_Details/Details/5
        public async Task<ActionResult> Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var client = new RestClient(url);
            var request = new RestRequest($"api/Order_Details/{id}", Method.GET);
            IRestResponse<List<Order_Details>> response = await client.ExecuteAsync<List<Order_Details>>(request);
            Order_Details order_details = JsonConvert.DeserializeObject<Order_Details>(response.Content);

            if (order_details == null)
            {
                return HttpNotFound();
            }
            return View(order_details);
        }

        // GET: Order_Details/Create
        public ActionResult Create()
        {
            ViewBag.OrderID = new SelectList(db.Orders, "OrderID", "OrderID");
            ViewBag.ProductID = new SelectList(db.Products, "ProductID", "ProductName");
            return View();
        }

        // POST: Order_Details/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "OrderID,ProductID,UnitPrice,Quantity,Discount")] Order_Details order_Details)
        {
            if (ModelState.IsValid)
            {
                var client = new RestClient(url);
                var request = new RestRequest("/api/Order_Details", Method.POST);
                request.AddParameter("OrderID", order_Details.OrderID);
                request.AddParameter("ProductID", order_Details.ProductID);
                request.AddParameter("UnitPrice", order_Details.UnitPrice);
                request.AddParameter("Quantity", order_Details.Quantity);
                request.AddParameter("Discount", order_Details.Discount);

                await client.ExecuteAsync(request);

                return RedirectToRoute(new
                {
                    controller = "Order_Details",
                    action = "Details",
                    id = $"{order_Details.OrderID} {order_Details.ProductID}"
                });
            }

            ViewBag.OrderID = new SelectList(db.Orders, "OrderID", "OrderID", order_Details.OrderID);
            ViewBag.ProductID = new SelectList(db.Products, "ProductID", "ProductName", order_Details.ProductID);
            return View(order_Details);
        }

        // GET: Order_Details/Edit/5
        public async Task<ActionResult> Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var client = new RestClient(url);
            var request = new RestRequest($"api/Order_Details/{id}", Method.GET);
            IRestResponse<List<Order_Details>> response = await client.ExecuteAsync<List<Order_Details>>(request);
            Order_Details order_details = JsonConvert.DeserializeObject<Order_Details>(response.Content);
            if (order_details == null)
            {
                return HttpNotFound();
            }
            ViewBag.OrderID = new SelectList(db.Orders, "OrderID", "OrderID", order_details.OrderID);
            ViewBag.ProductID = new SelectList(db.Products, "ProductID", "ProductName", order_details.ProductID);
            return View(order_details);
        }

        // POST: Order_Details/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "OrderID,ProductID,UnitPrice,Quantity,Discount")] Order_Details order_Details, string id)
        {
            string[] ID = id.Split();
            order_Details.OrderID = Convert.ToInt32(ID[0]);
            order_Details.ProductID = Convert.ToInt32(ID[1]);

            if (ModelState.IsValid)
            {
                var client = new RestClient(url);
                var request = new RestRequest($"/api/Order_Details/{order_Details.OrderID} {order_Details.ProductID}", Method.PUT);
                request.AddParameter("OrderID", order_Details.OrderID);
                request.AddParameter("ProductID", order_Details.ProductID);
                request.AddParameter("UnitPrice", order_Details.UnitPrice);
                request.AddParameter("Quantity", order_Details.Quantity);
                request.AddParameter("Discount", order_Details.Discount);
                await client.ExecuteAsync(request);

                return RedirectToRoute(new
                {
                    controller = "Order_Details",
                    action = "Details",
                    id = $"{order_Details.OrderID} {order_Details.ProductID}"
                });
            }
            ViewBag.OrderID = new SelectList(db.Orders, "OrderID", "CustomerID", order_Details.OrderID);
            ViewBag.ProductID = new SelectList(db.Products, "ProductID", "ProductName", order_Details.ProductID);
            return View(order_Details);
        }

        // GET: Order_Details/Delete/5
        public async Task<ActionResult> Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            string[] ID = id.Split();
            int orderid = Convert.ToInt32(ID[0]);
            int productid = Convert.ToInt32(ID[1]);

            var client = new RestClient(url);
            var request = new RestRequest($"api/Order_Details/{id}", Method.GET);
            IRestResponse<List<Order_Details>> response = await client.ExecuteAsync<List<Order_Details>>(request);
            Order_Details order_details = JsonConvert.DeserializeObject<Order_Details>(response.Content);

            if (order_details == null)
            {
                return HttpNotFound();
            }
            return View(order_details);
        }

        // POST: Order_Details/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(string id)
        {
            var client = new RestClient(url);
            var request = new RestRequest($"api/Order_Details/{id}", Method.DELETE);
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
