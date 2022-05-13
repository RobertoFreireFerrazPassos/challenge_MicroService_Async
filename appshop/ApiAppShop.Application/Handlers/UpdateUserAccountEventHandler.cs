using ApiAppShop.Domain.DomainServices;
using ApiAppShop.Domain.Dtos;
using ApiAppShop.Domain.Entities;
using ApiAppShop.Domain.Handlers;
using AutoMapper;
using System.Linq;
using System.Threading.Tasks;

namespace ApiAppShop.Application.Handlers
{
    public class UpdateUserAccountEventHandler : IUserAccountEventHandler
    {
        private readonly IMapper _mapper;

        private readonly IUserAccountDomainService _userAccountDomainService;

        private readonly UserAccountEntity _userAccount;

        private readonly AppDto _newPurchaseApp;

        public UpdateUserAccountEventHandler(
            IMapper mapper,
            IUserAccountDomainService userAccountDomainService,
            UserAccountEntity userAccount,
            AppDto newPurchaseApp)
        {
            _mapper = mapper;

            _userAccountDomainService = userAccountDomainService;

            _userAccount = userAccount;

            _newPurchaseApp = newPurchaseApp;
        }

        public async Task Run()
        {
            var oldUserAccount = new UserAccountEntity()
            {
                Id = _userAccount.Id,
                UserId = _userAccount.UserId,
                Apps = _userAccount.Apps.ToList()
            };

            var apps = _userAccount.Apps.ToList();

            apps.Add(_mapper.Map<AppEntity>(_newPurchaseApp));

            _userAccount.Apps = apps;

            await _userAccountDomainService.UpdateAsync(_userAccount, oldUserAccount);
        }
    }
}
