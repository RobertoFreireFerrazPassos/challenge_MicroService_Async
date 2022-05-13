using ApiAppShop.Domain.Handlers;
using System.Threading.Tasks;

namespace ApiAppShop.Application.Handlers
{
    public class NullUserAccountEventHandler : IUserAccountEventHandler
    {
        public async Task Run()
        {
            await Task.CompletedTask;
        }
    }
}
