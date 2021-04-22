﻿using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using Umbraco.Core.Persistence;
using Umbraco.Core.Scoping;
using uSeoToolkit.Umbraco8.Core.Interfaces;
using uSeoToolkit.Umbraco8.Core.Models.SeoSettings.Database;

namespace uSeoToolkit.Umbraco8.Core.Repositories
{
    public class SeoValueDatabaseRepository : ISeoValueRepository
    {
        private readonly IScopeProvider _scopeProvider;

        public SeoValueDatabaseRepository(IScopeProvider scopeProvider)
        {
            _scopeProvider = scopeProvider;
        }

        public void Add(int nodeId, string fieldAlias, object value)
        {
            using (var scope = _scopeProvider.CreateScope())
            {
                scope.Database.Insert(new SeoValueEntity
                {
                    NodeId = nodeId,
                    Alias = fieldAlias,
                    UserValue = JsonConvert.SerializeObject(value)
                });
                scope.Complete();
            }
        }

        public void Update(int nodeId, string fieldAlias, object value)
        {
            using (var scope = _scopeProvider.CreateScope())
            {
                scope.Database.Update(new SeoValueEntity
                {
                    NodeId = nodeId,
                    Alias = fieldAlias,
                    UserValue = JsonConvert.SerializeObject(value)
                });
                scope.Complete();
            }
        }

        public void Delete(int nodeId, string fieldAlias)
        {
            using (var scope = _scopeProvider.CreateScope())
            {
                scope.Database.Delete(scope.SqlContext.Sql()
                    .Where<SeoValueEntity>(it => it.NodeId == nodeId && it.Alias == fieldAlias));
                scope.Complete();
            }
        }

        public bool Exists(int nodeId, string fieldAlias)
        {
            using (var scope = _scopeProvider.CreateScope())
            {
                return scope.Database.FirstOrDefault<SeoValueEntity>(scope.SqlContext.Sql().SelectAll()
                    .From<SeoValueEntity>().Where<SeoValueEntity>(it => it.NodeId == nodeId && it.Alias == fieldAlias)) != null;
            }
        }

        public Dictionary<string, object> GetAllValues(int nodeId)
        {
            using (var scope = _scopeProvider.CreateScope())
            {
                return scope.Database
                    .Fetch<SeoValueEntity>(scope.SqlContext.Sql().Where<SeoValueEntity>(it => it.NodeId == nodeId))
                    .ToDictionary(it => it.Alias, it => JsonConvert.DeserializeObject(it.UserValue));
            }
        }
    }
}
