using ApiAppShop.Domain.Cache;
using ApiAppShop.Domain.Constants;
using ApiAppShop.Domain.DomainServices;
using ApiAppShop.Domain.Entities;
using ApiAppShop.Domain.Repositories;
using Microsoft.Extensions.Logging;
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

        private readonly  ILogger<UserAccountDomainService> _logger;

        public UserAccountDomainService(
            IUserAccountRepository userAccountRepository,
            ICache cache,
            ILogger<UserAccountDomainService> logger
            ) 
        {
            _userAccountRepository = userAccountRepository ??
                throw new ArgumentNullException(nameof(userAccountRepository));
            _cache = cache ??
                throw new ArgumentNullException(nameof(cache));
            _logger = logger ??
                throw new ArgumentNullException(nameof(logger));
        }

        public async Task<UserAccountEntity> GetAsync(string userId)
        {
            var userAccount = GetUserAccountInCache(userId);            

            if (userAccount is null)
            {
                userAccount = await _userAccountRepository.GetAsync(userId);
            }

            return userAccount;
        }

        public async Task UpdateAsync(UserAccountEntity userAccount, UserAccountEntity userAccountBackUp = null)
        {
            try
            {
                SetUserAccountInCache(userAccount);

                await _userAccountRepository.ReplaceAsync(userAccount);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);

                _logger.LogInformation(
                    string.Format(ErrorMessageConstants.ROLLING_BACK_CHANGES_FOR_USER_ACCOUNT_0_IN_CACHE, userAccount.UserId)
                    ); 

                SetUserAccountInCache(userAccountBackUp);
            }            
        }

        public async Task CreateAsync(UserAccountEntity userAccount)
        {
            var userAccountBackUp = new UserAccountEntity()
            {
                UserId = userAccount.UserId,
                Apps = new List<AppEntity>() { }
            };

            try
            {
                SetUserAccountInCache(userAccount);

                await _userAccountRepository.SetAsync(userAccount);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);

                _logger.LogInformation(
                    string.Format(ErrorMessageConstants.ROLLING_BACK_CHANGES_FOR_USER_ACCOUNT_0_IN_CACHE, userAccount.UserId)
                    );

                SetUserAccountInCache(userAccountBackUp);
            }
        }

        private void SetUserAccountInCache(UserAccountEntity userAccount)
        {
            string value = JsonSerializer.Serialize(userAccount.Apps);

            _cache.Set(BuildKey(userAccount.UserId), value);
        }

        private UserAccountEntity GetUserAccountInCache(string userId)
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

        private string BuildKey(string key)
        {
            return _appbyuser + key;
        }
    }
}
