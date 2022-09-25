using JoyTechAPITest.Database;
using JoyTechAPITest.Models;
using JoyTechAPITest.Services.UserService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using Microsoft.EntityFrameworkCore;

namespace JoyTechAPITest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private DatabaseContext db;
        private readonly IUserService _userService;
        public UserController(DatabaseContext context, IUserService userService)
        {
            db = context;
            _userService = userService;
        }
        [HttpGet("Orders"), Authorize(Roles = "User, Admin")]
        public async Task<ActionResult<IEnumerable<Order>>> Get()
        {
            var target_user = db.Users.FirstOrDefault(x => x.Username == _userService.GetMyName());
            var dborders = await db.Orders.Include(x => x.InOrderProducts).ThenInclude(x => x.Product).ToListAsync();
            var target_user_orders = dborders.Where(x => x.UserId == target_user.Id);
            target_user_orders = target_user_orders.OrderBy(x=>x.OrderSum).ToList();
            return Ok(target_user_orders);
        }

        [HttpGet("MyProfile"), Authorize(Roles = "User, Admin")]
        public async Task<User> GetName()
        {
           var name = db.Users.Where(x=>x.Username == _userService.GetMyName()).FirstOrDefault();
            return name;
        }
        [HttpGet("Orders/{id}"), Authorize(Roles = "User, Admin")]
        public async Task<ActionResult<Order>> Get(int id)
        {
            var target_user = db.Users.FirstOrDefault(x => x.Username == _userService.GetMyName());
            var dborders = await db.Orders.Include(x => x.InOrderProducts).ThenInclude(x=>x.Product).ToListAsync();
            var target_user_orders = dborders.Where(x => x.UserId == target_user.Id);
            var res = target_user_orders.FirstOrDefault(x => x.Id == id);
            return Ok(res);
        }
    }
}
