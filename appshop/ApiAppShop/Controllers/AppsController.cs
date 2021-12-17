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
        private readonly string table = "Apps";

        public AppsController(Context context)
        {
            _context = context;
        }

        [HttpGet("setapp")]
        public IActionResult Set()
        {
            _context.SetItem(new UserModel(),table);
            return Ok();
        }

        [HttpGet("getapp/{id}")]
        public IActionResult Get(string id)
        {
            return Ok(_context.GetItem<UserModel>(id,table));
        }

        [HttpGet("getapps")]
        public IActionResult Get()
        {
            return Ok(_context.GetItems<UserModel>(table));
        }

        [HttpPost("purchase")]
        public string Purchase([FromBody] PurchaseModel purchaseModel)
        {
            return purchaseModel.App + " Purchased";
        }
    }
}
