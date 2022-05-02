using ApiAppShop.Domain.Cache;
using ApiAppShop.Domain.Constants;
using ApiAppShop.Domain.DomainServices;
using ApiAppShop.Domain.Entities;
using ApiAppShop.Domain.Repositories;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace ApiAppShop.Application.DomainServices
{
    public class UserAccountDomainService : IUserAccountDomainService
    {
        private readonly ICache _cache;

        private readonly IUserAccountRepository _userAccountRepository;

        private readonly string _appbyuser = CacheKeyPrefixConstants.APP_BY_USER_;

        public UserAccountDomainService(
            IUserAccountRepository userAccountRepository,
            ICache cache
            ) 
        {
            _userAccountRepository = userAccountRepository ??
                throw new ArgumentNullException(nameof(userAccountRepository));
            _cache = cache ??
                throw new ArgumentNullException(nameof(cache));
        }

        public async Task<UserAccountEntity> GetAsync(string userId)
        {
            var userAccount = GetUserAccountInCache(userId);            

            if ((userAccount is null) || userAccount.Apps.ToList().Count == 0)
            {
                userAccount = await _userAccountRepository.GetAsync(userId);
            }

            return userAccount;

            UserAccountEntity GetUserAccountInCache(string userId)
            {
                var result = _cache.Get(BuildKey(userId));

                if (result == null)
                {
                    return default(UserAccountEntity);
                }

                return new UserAccountEntity()
                {
                    UserId = userId,
                    Apps = JsonSerializer.Deserialize<IEnumerable<AppEntity>>(result)
                };
            }
        }

        public async Task UpdateAsync(UserAccountEntity userAccount)
        {
            await _userAccountRepository.ReplaceAsync(userAccount);

            SetUserAccountInCache(userAccount);
        }

        public async Task CreateAsync(UserAccountEntity userAccount)
        {
            await _userAccountRepository.SetAsync(userAccount);

            SetUserAccountInCache(userAccount);
        }

        private void SetUserAccountInCache(UserAccountEntity userAccount)
        {
            string value = JsonSerializer.Serialize(userAccount.Apps);

            _cache.Set(BuildKey(userAccount.UserId), value);
        }

        private string BuildKey(string key)
        {
            return _appbyuser + key;
        }
    }
}
