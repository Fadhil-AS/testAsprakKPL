using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using System.Text.Json.Serialization;
using APITokoOnline.Models;
using Microsoft.AspNetCore.Http.HttpResults;

namespace APITokoOnline.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class OrderController
    {
        private const string file = "orders.json";
        private List<Order> loadingOrder() {
            if (System.IO.File.Exists(file)) { 
                var data = System.IO.File.ReadAllText(file);
                return JsonSerializer.Deserialize<List<Order>>(data) ?? new List<Order>();
            }
            return new List<Order>();
        }

        private void SaveOrders(List<Order> orders) { 
            var data = JsonSerializer.Serialize(orders);
            System.IO.File.WriteAllText(file, data);
        }

        [HttpGet]
        public ActionResult<List<Order>> GetOrders() { 
            return loadingOrder();
        }

        [HttpGet("{id}")]
        public ActionResult<Order> GetOrders(int id, ActionResult<Order> notfnd) {
            var orders = loadingOrder();
            var order = orders.FirstOrDefault(o => o.Id == id);
            if (order == null) {
                return null;
            }
            return order;
        }

        [HttpPost]
        public ActionResult<Order> addOrder(Order order) {
            var orders = loadingOrder();
            order.Id = orders.Count > 0 ? orders.Max(o => o.Id) + 1 : 1;
            orders.Add(order);
            SaveOrders(orders);
            return CreatedAtActionResult(nameof(GetOrders), new { id = order.Id }, order);
        }

        private ActionResult<Order> CreatedAtActionResult(string v, object value, Order order)
        {
            throw new NotImplementedException();
        }
    }
}
