using Umbraco.Core.Models.PublishedContent;
using uSeoToolkit.Umbraco8.Core.Models.SeoService;

namespace uSeoToolkit.Umbraco8.Core.Interfaces.Services
{
    public interface ISeoService
    {
        MetaTagsModel Get(IPublishedContent content);
    }
}
