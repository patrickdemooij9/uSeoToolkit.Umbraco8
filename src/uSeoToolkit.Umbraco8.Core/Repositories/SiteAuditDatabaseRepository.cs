using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using Umbraco.Core.Scoping;
using Umbraco.Core.Persistence;
using Umbraco.Web;
using uSeoToolkit.Umbraco8.Core.Models.SiteAudit.Database;
using System.Linq;
using Newtonsoft.Json;
using Umbraco.Core.Models.PublishedContent;
using uSeoToolkit.Umbraco8.Core.Common.Enums;
using uSeoToolkit.Umbraco8.Core.Interfaces.SiteAudit;
using uSeoToolkit.Umbraco8.Core.Models.SiteAudit.Business;

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

        public SiteAuditDto Add(SiteAuditDto model)
        {
            return Update(model);
        }

        public void Delete(int id)
        {
            using (var scope = _scopeProvider.CreateScope())
            {
                foreach (var page in scope.Database.Fetch<SiteAuditPageEntity>(scope.SqlContext.Sql().SelectAll()
                    .From<SiteAuditPageEntity>()
                    .Where<SiteAuditPageEntity>(it => it.AuditId == id)))
                {
                    foreach (var result in scope.Database.Fetch<SiteAuditCheckResultEntity>(scope.SqlContext.Sql()
                        .SelectAll()
                        .From<SiteAuditCheckResultEntity>()
                        .Where<SiteAuditCheckResultEntity>(it => it.PageId == page.Id)))
                    {
                        scope.Database.Delete(result);
                    }
                    scope.Database.Delete(page);
                }
                foreach (var check in scope.Database.Fetch<SiteAuditCheckEntity>(scope.SqlContext.Sql()
                    .SelectAll()
                    .From<SiteAuditCheckEntity>()
                    .Where<SiteAuditCheckEntity>(it => it.AuditId == id)))
                {
                    scope.Database.Delete(check);
                }

                scope.Database.Delete<SiteAuditEntity>(id);

                scope.Complete();
            }
        }

        public void SaveCrawledPage(SiteAuditDto audit, CrawledPageDto page)
        {
            using (var scope = _scopeProvider.CreateScope())
            {
                var pageEntity = new SiteAuditPageEntity
                {
                    AuditId = audit.Id,
                    StatusCode = page.StatusCode,
                    Url = page.PageUrl.ToString()
                };
                scope.Database.Insert(pageEntity);
                scope.Complete();

                foreach (var result in page.Results)
                {
                    scope.Database.Insert(new SiteAuditCheckResultEntity
                    {
                        PageId = pageEntity.Id,
                        CheckId = result.Check.Id,
                        ResultId = (int)result.Result,
                        ExtraValues = JsonConvert.SerializeObject(result.ExtraValues)
                    });
                }
                scope.Complete();
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
                            .Select(it => _siteCheckCollection.GetAll().FirstOrDefault(s => s.Id == it.CheckGuid)).ToList(),
                            CrawledPages = new ConcurrentQueue<CrawledPageDto>(scope.Database.Fetch<SiteAuditPageEntity>(scope.SqlContext.Sql()
                                .SelectAll()
                                .From<SiteAuditPageEntity>()
                                .Where<SiteAuditPageEntity>(it => it.AuditId == entity.Id)).Select(it => Map(scope, it)))
                        };
                    }
                }
            }
        }

        public SiteAuditDto Update(SiteAuditDto model)
        {
            var isNew = model.Id == 0;
            using (var ctx = _umbracoContextFactory.EnsureUmbracoContext())
            {
                var entity = new SiteAuditEntity
                {
                    Id = isNew ? 0 : model.Id,
                    Name = model.Name,
                    StartingNodeId = ctx.UmbracoContext.Content.GetByRoute(new Uri(model.StartingUrl.ToString()).AbsolutePath).Id,
                    MaxPagesToCrawl = model.MaxPagesToCrawl ?? 0,
                    DelayBetweenRequests = model.DelayBetweenRequests
                };
                using (var scope = _scopeProvider.CreateScope())
                {
                    if (isNew)
                        scope.Database.Insert(entity);
                    else
                        scope.Database.Update(entity);

                    scope.Complete();
                    //TODO: Probably save pages and results here again?
                }

                var currentSiteChecks = GetChecksByAudit(entity.Id).ToArray();
                using (var scope = _scopeProvider.CreateScope())
                {
                    foreach (var newCheck in model.SiteChecks.Where(it => currentSiteChecks.All(x => x.CheckGuid != it.Id)))
                    {
                        var checkEntity = new SiteAuditCheckEntity { AuditId = entity.Id, CheckGuid = newCheck.Id };
                        scope.Database.Insert(checkEntity);
                    }

                    foreach (var deletedCheck in currentSiteChecks.Where(it =>
                        model.SiteChecks.All(x => x.Id != it.CheckGuid)))
                    {
                        scope.Database.Delete(deletedCheck);
                    }

                    scope.Complete();
                }

                model.Id = entity.Id;
                return model;
            }
        }

        private IEnumerable<SiteAuditCheckEntity> GetChecksByAudit(int auditId)
        {
            using (var scope = _scopeProvider.CreateScope())
            {
                return scope.Database.Fetch<SiteAuditCheckEntity>(scope.SqlContext.Sql()
                    .SelectAll()
                    .From<SiteAuditCheckEntity>()
                    .Where<SiteAuditCheckEntity>(it => it.AuditId == auditId));
            }
        }

        //TODO: Move to mapper
        private CrawledPageDto Map(IScope scope, SiteAuditPageEntity entity)
        {
            var dto = new CrawledPageDto
            {
                PageUrl = new Uri(entity.Url),
                StatusCode = entity.StatusCode
            };
            foreach (var result in scope.Database.Fetch<SiteAuditCheckResultEntity>(scope.SqlContext.Sql()
                .SelectAll()
                .From<SiteAuditCheckResultEntity>()
                .Where<SiteAuditCheckResultEntity>(it => it.PageId == entity.Id)))
            {
                dto.Results.Add(new PageCrawlResult
                {
                    Check = _siteCheckCollection.GetCheckByAlias(result.CheckId),
                    Result = (SiteCrawlResultType)result.ResultId,
                    ExtraValues = JsonConvert.DeserializeObject<Dictionary<string, string>>(result.ExtraValues)
                });
            }

            return dto;
        }
    }
}
