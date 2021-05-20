using System;
using Umbraco.Core.Models.PublishedContent;

namespace uSeoToolkit.Umbraco8.Core.Interfaces.Converters
{
    public interface ISeoValueConverter
    {
        Type FromValue { get; }
        Type ToValue { get; }

        object Convert(object value, IPublishedContent currentContent);
    }
}
