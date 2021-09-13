using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using API_NorthWind.Models;

namespace API_NorthWind.Controllers
{
    public class Order_DetailsController : ApiController
    {
        private DBNorthwind db = new DBNorthwind();

        // GET: api/Order_Details
        public IQueryable<Order_Details> GetOrder_Details()
        {
            return db.Order_Details;
        }

        // GET: api/Order_Details/5
        [ResponseType(typeof(Order_Details))]
        public async Task<IHttpActionResult> GetOrder_Details(string id)
        {
            string[] ID = id.Split();
            int orderid = Convert.ToInt32(ID[0]);
            int productid = Convert.ToInt32(ID[1]);

            var order_Details = await db.Order_Details.Where(x => x.OrderID == orderid && x.ProductID == productid).SingleAsync();

            if (order_Details == null)
            {
                return NotFound();
            }

            return Ok(order_Details);
        }

        // PUT: api/Order_Details/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutOrder_Details(string id, Order_Details order_Details)
        {
            string[] ID = id.Split();
            int orderid = Convert.ToInt32(ID[0]);
            int productid = Convert.ToInt32(ID[1]);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (orderid != order_Details.OrderID && productid != order_Details.ProductID)
            {
                return BadRequest();
            }

            db.Entry(order_Details).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!Order_DetailsExists(orderid))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Order_Details
        [ResponseType(typeof(Order_Details))]
        public IHttpActionResult PostOrder_Details(Order_Details order_Details)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Order_Details.Add(order_Details);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (Order_DetailsExists(order_Details.OrderID))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = order_Details.OrderID }, order_Details);
        }

        // DELETE: api/Order_Details/5
        [ResponseType(typeof(Order_Details))]
        public async Task<IHttpActionResult> DeleteOrder_Details(string id)
        {
            string[] ID = id.Split();
            int orderid = Convert.ToInt32(ID[0]);
            int productid = Convert.ToInt32(ID[1]);

            var order_Details = await db.Order_Details.Where(x => x.OrderID == orderid && x.ProductID == productid).SingleAsync();
            if (order_Details == null)
            {
                return NotFound();
            }

            db.Order_Details.Remove(order_Details);
            db.SaveChanges();

            return Ok(order_Details);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool Order_DetailsExists(int id)
        {
            return db.Order_Details.Count(e => e.OrderID == id) > 0;
        }
    }
}