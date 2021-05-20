using System;
using Umbraco.Core.Models.PublishedContent;
using Umbraco.Web;
using uSeoToolkit.Umbraco8.Core.Interfaces.Converters;

namespace uSeoToolkit.Umbraco8.Core.Common.Converters.SeoValueConverters
{
    public class PublishedContentSeoValueConverter : ISeoValueConverter
    {
        public Type FromValue => typeof(IPublishedContent);
        public Type ToValue => typeof(string);
        public object Convert(object value, IPublishedContent currentContent)
        {
            return (value as IPublishedContent)?.Url(mode: UrlMode.Absolute);
        }
    }
}
