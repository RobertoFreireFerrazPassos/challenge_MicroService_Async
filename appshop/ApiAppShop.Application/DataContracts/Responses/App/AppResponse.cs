using System.Collections.Generic;

namespace ApiAppShop.Application.DataContracts
{
    public class AppResponse
    {
        public IEnumerable<App> Apps { get; set; }
    }

    public class App
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
    }
}
