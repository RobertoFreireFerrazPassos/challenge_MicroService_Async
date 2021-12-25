using ApiAppShop.Domain.Constants;
using ApiAppShop.Domain.Entities;
using ApiAppShop.Domain.Repositories;
using ApiAppShop.Repository;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;

namespace ApiAppShop.DataAccess.Repositories
{
    public class AppRepository : Repository<AppEntity>, IAppRepository
    {
        private static readonly string Document = NoSqlDocumentConstants.APPS;
        public AppRepository(IConfiguration configuration) : base(configuration, Document)
        {
        }

        public AppEntity GetApp(string id)
        {
            return GetItem(id);
        }

        public IEnumerable<AppEntity> GetApps()
        {
            return GetItems();
        }

        public void SetApp(AppEntity item)
        {
            SetItem(item);
        }
    }
}
