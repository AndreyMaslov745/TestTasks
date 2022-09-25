using JoyTechAPITest.Database;
using JoyTechAPITest.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Data;
namespace JoyTechAPITest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {

        private DatabaseContext db;
        public AdminController(DatabaseContext context)
        {
            db = context;
        }

        [HttpGet("UserInfo/{id}"), Authorize(Roles = "Admin")]
        public async Task<ActionResult<User>> Get(int id)
        {
           var target_user = await db.Users.Include(x=>x.Orders).ThenInclude(x=>x.InOrderProducts).ThenInclude(x=>x.Product).FirstOrDefaultAsync(x => x.Id == id);
            if(target_user == null)
            {
                return BadRequest("User not found");
            }
            return Ok(target_user);
        }
        [HttpGet("OrdersInfo"), Authorize(Roles = "Admin")]
        public async Task<ActionResult<IEnumerable<Order>>> Get(DateTime? date = null, string? product_id = "", string? min_price = "", string? max_price = "")
        {

            var allorders =await db.Orders.Include(x => x.InOrderProducts).ThenInclude(x=>x.Product).ToListAsync();
            DateTime? a = (date == null) ? DateTime.MinValue : date;
            allorders = allorders.Where(x => DateTime.Compare((DateTime)a, x.OrderDate) < 0).ToList();
            try
            {
                if (!String.IsNullOrEmpty(product_id))
                {
                    int target_id = Int32.Parse(product_id);
                    allorders = allorders.Where(x => x.InOrderProducts.Any(x => x.ProductId == target_id)).ToList();
                }
                if(!String.IsNullOrEmpty(min_price))
                {
                    decimal price = Decimal.Parse(min_price);
                    allorders = allorders.Where(x => x.OrderSum >= price).ToList();
                }
                if(!String.IsNullOrEmpty(max_price))
                {
                    decimal price = Decimal.Parse(max_price);
                    allorders = allorders.Where(x => x.OrderSum <= price).ToList();
                }

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            return Ok(allorders);
        }
    }
}
