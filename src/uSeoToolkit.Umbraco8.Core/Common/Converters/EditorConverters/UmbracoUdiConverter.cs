using Umbraco.Core;
using Umbraco.Core.Models.PublishedContent;
using Umbraco.Web;
using uSeoToolkit.Umbraco8.Core.Interfaces.Converters;

namespace uSeoToolkit.Umbraco8.Core.Common.Converters.EditorConverters
{
    public abstract class UmbracoUdiConverter : IEditorValueConverter
    {
        public object ConvertEditorToDatabaseValue(object value)
        {
            return Udi.TryParse(value?.ToString(), out _) ? value : null;
        }

        public object ConvertObjectToEditorValue(object value)
        {
            if (value is IPublishedContent content)
            {
                return new GuidUdi(content.ItemType.ToString(), content.Key);
            }

            return null;
        }

        public abstract object ConvertDatabaseToObject(object value);

        public bool IsEmpty(object value)
        {
            var udi = value as Udi;
            return udi != null;
        }
    }
}
