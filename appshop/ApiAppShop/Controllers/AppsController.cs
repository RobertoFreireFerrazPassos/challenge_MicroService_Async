using ApiAppShop.Models;
using ApiAppShop.Repository;
using ApiUser.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace ApiAppShop.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AppsController : ControllerBase
    {
        private Context _context;

        public AppsController(Context context)
        {
            _context = context;
        }

        [HttpGet("setapp")]
        public IActionResult Set()
        {
            _context.SetItem(new UserModel());
            return Ok();
        }

        [HttpPost("getapp")]
        public IActionResult Get(string id)
        {
            return Ok(_context.GetItems<UserModel>(id));
        }

        [HttpPost("purchase")]
        public string Purchase([FromBody] PurchaseModel purchaseModel)
        {
            return purchaseModel.App + " Purchased";
        }
    }
}
