using System;
using System.Collections.Generic;
using Umbraco.Core.Scoping;
using Umbraco.Core.Persistence;
using Umbraco.Web;
using uSeoToolkit.Umbraco8.Core.Models.SiteAudit.Database;
using System.Linq;
using SeoToolkit.Core.Interfaces.SiteAudit;
using SeoToolkit.Core.Models.SiteAudit;
using Umbraco.Core.Models.PublishedContent;

namespace uSeoToolkit.Umbraco8.Core.Repositories
{
    public class SiteAuditDatabaseRepository : ISiteAuditRepository
    {
        private readonly IScopeProvider _scopeProvider;
        private readonly ISiteCheckCollection _siteCheckCollection;
        private readonly IUmbracoContextFactory _umbracoContextFactory;

        public SiteAuditDatabaseRepository(IScopeProvider scopeProvider, ISiteCheckCollection siteCheckCollection, IUmbracoContextFactory umbracoContextFactory)
        {
            _scopeProvider = scopeProvider;
            _siteCheckCollection = siteCheckCollection;
            _umbracoContextFactory = umbracoContextFactory;
        }

        public void Add(SiteAuditDto model)
        {
            Update(model);
            using (var ctx = _umbracoContextFactory.EnsureUmbracoContext())
            {
                using (var scope = _scopeProvider.CreateScope())
                {
                    var entity = new SiteAuditEntity { Name = model.Name, StartingNodeId = ctx.UmbracoContext.Content.GetByRoute(model.StartingUrl.AbsolutePath).Id };
                    scope.Database.Insert(entity);
                    scope.Complete();
                    foreach (var check in model.SiteChecks)
                    {
                        scope.Database.Insert(new SiteAuditCheckEntity { AuditId = entity.Id, CheckGuid = check.Id });
                    }
                }
            }
        }

        public void Delete(int id)
        {
            using (var scope = _scopeProvider.CreateScope())
            {
                scope.Database.Delete<SiteAuditEntity>(id);
                scope.Database.Delete<SiteAuditCheckEntity>(scope.SqlContext.Sql().Where<SiteAuditCheckEntity>(it => it.AuditId == id));
            }
        }

        public SiteAuditDto Get(int id)
        {
            return GetAll().FirstOrDefault(it => it.Id == id);
        }

        public IEnumerable<SiteAuditDto> GetAll()
        {
            using (var ctx = _umbracoContextFactory.EnsureUmbracoContext())
            {
                using (var scope = _scopeProvider.CreateScope())
                {
                    var entities = scope.Database.Fetch<SiteAuditEntity>();
                    foreach (var entity in entities)
                    {
                        yield return new SiteAuditDto
                        {
                            Id = entity.Id,
                            Name = entity.Name,
                            StartingUrl = new Uri(ctx.UmbracoContext.Content.GetById(entity.StartingNodeId)?.Url(mode: UrlMode.Absolute)),
                            SiteChecks = scope.Database.Fetch<SiteAuditCheckEntity>(scope.SqlContext.Sql()
                                .SelectAll()
                                .From<SiteAuditCheckEntity>()
                                .Where<SiteAuditCheckEntity>(it => it.AuditId == entity.Id))
                            .Select(it => _siteCheckCollection.GetAll().FirstOrDefault(s => s.Id == it.CheckGuid)).ToList()
                        };
                    }
                }
            }
        }

        public void Update(SiteAuditDto model)
        {
            var isNew = model.Id == 0;
            using (var ctx = _umbracoContextFactory.EnsureUmbracoContext())
            {
                using (var scope = _scopeProvider.CreateScope())
                {
                    var entity = new SiteAuditEntity { Id = isNew ? 0 : model.Id, Name = model.Name, StartingNodeId = ctx.UmbracoContext.Content.GetByRoute(model.StartingUrl.ToString()).Id };
                    if (isNew)
                        scope.Database.Insert(entity);
                    else
                        scope.Database.Update(entity);

                    foreach (var check in model.SiteChecks)
                    {
                        scope.Database.Update(new SiteAuditCheckEntity { AuditId = entity.Id, CheckGuid = check.Id });
                    }
                    scope.Complete();
                }
            }
        }

        public IEnumerable<SiteAuditCheckResultEntity> GetAllResults(int auditId)
        {
            using (var scope = _scopeProvider.CreateScope())
            {
                return scope.Database.Fetch<SiteAuditCheckResultEntity>(scope.SqlContext.Sql().From<SiteAuditCheckResultEntity>().Where<SiteAuditCheckResultEntity>(it => it.SiteAuditId == auditId));
            }
        }
    }
}
