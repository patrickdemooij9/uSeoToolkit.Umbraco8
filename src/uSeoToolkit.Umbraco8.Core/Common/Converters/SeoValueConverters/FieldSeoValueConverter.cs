using System;
using Umbraco.Core.Models.PublishedContent;
using Umbraco.Web;
using uSeoToolkit.Umbraco8.Core.Interfaces.Converters;
using uSeoToolkit.Umbraco8.Core.Models.Converters;

namespace uSeoToolkit.Umbraco8.Core.Common.Converters.SeoValueConverters
{
    public class FieldSeoValueConverter : ISeoValueConverter
    {
        public Type FromValue => typeof(FieldsModel);
        public Type ToValue => typeof(string);
        public object Convert(object value, IPublishedContent currentContent)
        {
            if (!(value is FieldsModel model))
                return null;

            foreach (var field in model.Fields)
            {
                var returnValue = currentContent.Value<string>(field);
                if (!string.IsNullOrWhiteSpace(returnValue))
                    return returnValue;
            }

            return null;
        }
    }
}
