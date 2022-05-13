using ApiAppShop.Domain.DomainServices;
using ApiAppShop.Domain.Dtos;
using ApiAppShop.Domain.Entities;
using ApiAppShop.Domain.Handlers;
using AutoMapper;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ApiAppShop.Application.Handlers
{
    public class CreateUserAccountEventHandler : IUserAccountEventHandler
    {
        private readonly IUserAccountDomainService _userAccountDomainService;

        private readonly string _userId;

        private readonly AppDto _newPurchaseApp;

        private readonly IMapper _mapper;

        public CreateUserAccountEventHandler(
            IMapper mapper,
            IUserAccountDomainService userAccountDomainService,
            string userId,
            AppDto newPurchaseApp)
        {
            _mapper = mapper;

            _userAccountDomainService = userAccountDomainService;

            _userId = userId;

            _newPurchaseApp = newPurchaseApp;
        }

        public async Task Run()
        {
            var userAccount = new UserAccountEntity()
            {
                UserId = _userId,
                Apps = new List<AppEntity>() { _mapper.Map<AppEntity>(_newPurchaseApp) }
            };

            await _userAccountDomainService.CreateAsync(userAccount);
        }
    }
}
