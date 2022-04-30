﻿using ApiAppShop.Domain.Cache;
using ApiAppShop.Domain.Constants;
using ApiAppShop.Domain.DomainServices;
using ApiAppShop.Domain.Entities;
using ApiAppShop.Domain.Repositories;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;

namespace ApiAppShop.Application.DomainServices
{
    public class UserAccountDomainService : IUserAccountDomainService
    {
        private readonly IMapper _mapper;

        private readonly ICache _cache;

        private readonly IUserAccountRepository _userAccountRepository;

        private readonly string _appbyuser = CacheKeyPrefixConstants.APP_BY_USER_;

        public UserAccountDomainService(
            IUserAccountRepository userAccountRepository,
            IMapper mapper,
            ICache cache
            ) 
        {
            _userAccountRepository = userAccountRepository ??
                throw new ArgumentNullException(nameof(userAccountRepository));
            _mapper = mapper ??
                throw new ArgumentNullException(nameof(mapper));
            _cache = cache ??
                throw new ArgumentNullException(nameof(cache));
        }

        public UserAccountEntity Get(string userId)
        {
            var userAccount = GetUserAccountInCache(userId);            

            if ((userAccount is null) || userAccount.Apps.ToList().Count == 0)
            {
                userAccount = _userAccountRepository.Get(userId);
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

        public void Update(UserAccountEntity userAccount)
        {
            _userAccountRepository.Replace(_mapper.Map<UserAccountEntity>(userAccount));

            SetUserAccountInCache(userAccount);
        }

        public void Create(UserAccountEntity userAccount)
        {
            _userAccountRepository.Set(_mapper.Map<UserAccountEntity>(userAccount));

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
