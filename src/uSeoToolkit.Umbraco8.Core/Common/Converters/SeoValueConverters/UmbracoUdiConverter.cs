using System;
using Umbraco.Core;
using Umbraco.Core.Models;
using uSeoToolkit.Umbraco8.Core.Interfaces.SeoValueConverters;

namespace uSeoToolkit.Umbraco8.Core.Common.Converters.SeoValueConverters
{
    public abstract class UmbracoUdiConverter : ISeoValueConverter
    {
        public object ConvertEditorToDatabaseValue(object value)
        {
            return Udi.TryParse(value?.ToString(), out _) ? value : null;
        }

        public object ConvertDatabaseToEditorValue(object value)
        {
            return value;
        }

        public abstract string ConvertDatabaseToSeoValue(object value);

        public bool IsEmpty(object value)
        {
            return !Udi.TryParse(value?.ToString(), out _);
        }
    }
}
