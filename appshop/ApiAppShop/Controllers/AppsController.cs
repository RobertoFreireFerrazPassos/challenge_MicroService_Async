using ApiAppShop.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace ApiAppShop.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AppsController : ControllerBase
    {
        private static readonly string[] Apps = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        public AppsController()
        {
        }

        [HttpGet]
        public IEnumerable<string> Get()
        {            
            return Apps.ToList();
        }

        [HttpPost("purchase")]
        public string Purchase([FromBody] PurchaseModel purchaseModel)
        {
            return purchaseModel.App + " Purchased";
        }
    }
}
