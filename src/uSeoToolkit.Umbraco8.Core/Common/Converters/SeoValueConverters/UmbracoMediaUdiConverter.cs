using Umbraco.Core;
using Umbraco.Core.Models.PublishedContent;
using Umbraco.Web;

namespace uSeoToolkit.Umbraco8.Core.Common.Converters.SeoValueConverters
{
    public class UmbracoMediaUdiConverter : UmbracoUdiConverter
    {
        private readonly IUmbracoContextFactory _umbracoContextFactory;

        public UmbracoMediaUdiConverter(IUmbracoContextFactory umbracoContextFactory)
        {
            _umbracoContextFactory = umbracoContextFactory;
        }

        public override string ConvertDatabaseToSeoValue(object value)
        {
            if (!Udi.TryParse(value?.ToString(), out var udi))
                return null;

            using (var ctx = _umbracoContextFactory.EnsureUmbracoContext())
            {
                return ctx.UmbracoContext.Media.GetById(udi)?.Url(mode: UrlMode.Absolute);
            }
        }
    }
}
