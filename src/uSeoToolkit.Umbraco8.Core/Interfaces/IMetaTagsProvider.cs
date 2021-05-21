using System;
using Umbraco.Core.Models.PublishedContent;
using uSeoToolkit.Umbraco8.Core.Models.SeoService;

namespace uSeoToolkit.Umbraco8.Core.Interfaces
{
    public interface IMetaTagsProvider
    {
        event EventHandler<MetaTagsModel> BeforeMetaTagsGet;

        MetaTagsModel Get(IPublishedContent content);
    }
}
