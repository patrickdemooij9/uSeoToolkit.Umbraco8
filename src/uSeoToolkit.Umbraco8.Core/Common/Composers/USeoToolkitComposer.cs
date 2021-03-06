﻿using Umbraco.Core;
using Umbraco.Core.Composing;
using Umbraco.Core.Mapping;
using Umbraco.Web;
using uSeoToolkit.Umbraco8.Core.Common.Collections;
using uSeoToolkit.Umbraco8.Core.Common.Components;
using uSeoToolkit.Umbraco8.Core.Common.ContentApps;
using uSeoToolkit.Umbraco8.Core.Common.Converters.SeoValueConverters;
using uSeoToolkit.Umbraco8.Core.Common.Dashboards;
using uSeoToolkit.Umbraco8.Core.Common.Hubs;
using uSeoToolkit.Umbraco8.Core.Common.Providers;
using uSeoToolkit.Umbraco8.Core.Common.Sections;
using uSeoToolkit.Umbraco8.Core.Common.SiteAuditChecks;
using uSeoToolkit.Umbraco8.Core.Common.SiteCrawler;
using uSeoToolkit.Umbraco8.Core.Interfaces;
using uSeoToolkit.Umbraco8.Core.Interfaces.Services;
using uSeoToolkit.Umbraco8.Core.Interfaces.SiteAudit;
using uSeoToolkit.Umbraco8.Core.Interfaces.SiteCrawler;
using uSeoToolkit.Umbraco8.Core.Mappers;
using uSeoToolkit.Umbraco8.Core.Models.DocumentTypeSettings.Business;
using uSeoToolkit.Umbraco8.Core.Models.SeoField;
using uSeoToolkit.Umbraco8.Core.Repositories;
using uSeoToolkit.Umbraco8.Core.Services.DocumentTypeSettings;
using uSeoToolkit.Umbraco8.Core.Services.SeoService;
using uSeoToolkit.Umbraco8.Core.Services.SeoValueService;
using uSeoToolkit.Umbraco8.Core.Services.SiteAudit;

namespace uSeoToolkit.Umbraco8.Core.Common.Composers
{
    public class USeoToolkitComposer : IUserComposer
    {
        public void Compose(Composition composition)
        {
            composition.Components().Append<USeoToolkitDatabaseComponent>();

            composition.Sections().Append<USeoToolkitSection>();

            composition.Dashboards().Add<SiteAuditDashboard>();

            composition.ContentApps().Append<USeoToolkitDocumentSettingsContentAppFactory>();
            composition.ContentApps().Append<USeoToolkitSeoSettingsAppFactory>();

            composition.Register(typeof(ISiteAuditRepository), typeof(SiteAuditDatabaseRepository));
            composition.Register(typeof(ISiteCrawler), typeof(SiteCrawler.SiteCrawler));
            composition.Register(typeof(IPageRequester), typeof(DefaultPageUrlRequester));
            composition.Register(typeof(IScheduler), typeof(DefaultScheduler));
            composition.Register(typeof(ILinkParser), typeof(DefaultLinkParser));
            composition.Register(typeof(SiteAuditService), typeof(SiteAuditService), Lifetime.Singleton);
            composition.Register(typeof(IDocumentTypeSettingsService), typeof(DocumentTypeSettingsService));
            composition.Register(typeof(ISeoFieldCollection), typeof(SeoFieldCollection));
            composition.Register(typeof(ISiteCheckCollection), typeof(SiteAuditCheckCollection));
            composition.Register(typeof(IRepository<DocumentTypeSettingsDto>), typeof(DocumentTypeSettingsRepository));
            composition.Register(typeof(IDocumentTypeSettingsService), typeof(DocumentTypeSettingsService));
            composition.Register(typeof(ISeoService), typeof(SeoService));
            composition.Register(typeof(IMetaTagsProvider), typeof(DefaultMetaTagsProvider));
            composition.Register(typeof(SiteAuditHubClientService), Lifetime.Singleton);
            composition.Register(typeof(ISeoValueService), typeof(SeoValueService));
            composition.Register(typeof(ISeoValueRepository), typeof(SeoValueDatabaseRepository));
            composition.Register(typeof(ISeoConverterCollection), typeof(SeoConverterCollection));

            composition.WithCollectionBuilder<SiteAuditCheckCollectionBuilder>()
                .Append<BrokenLinkCheck>();

            composition.WithCollectionBuilder<SeoFieldCollectionBuilder>()
                .Add<SeoTitleField>()
                .Add<SeoDescriptionField>()
                .Add<OpenGraphTitleField>()
                .Add<OpenGraphDescriptionField>()
                .Add<OpenGraphImageField>()
                .Add<CanonicalUrlField>();

            composition.WithCollectionBuilder<SeoConverterCollectionBuilder>()
                .Add<TextSeoValueConverter>()
                .Add<PublishedContentSeoValueConverter>()
                .Add<FieldSeoValueConverter>();

            composition.WithCollectionBuilder<MapDefinitionCollectionBuilder>()
                .Add<DocumentTypeSettingsMapper>();
        }
    }
}
