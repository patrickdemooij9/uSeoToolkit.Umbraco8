using System.Linq;
using uSeoToolkit.Umbraco8.Core.Interfaces.SeoValueConverters;

namespace uSeoToolkit.Umbraco8.Core.Common.Converters.SeoValueConverters
{
    public class TextSeoValueConverter : ISeoValueConverter
    {
        public object ConvertEditorToDatabaseValue(object value)
        {
            return value?.ToString();
        }

        public object ConvertDatabaseToEditorValue(object value)
        {
            return value?.ToString();
        }

        public string ConvertDatabaseToSeoValue(object value)
        {
            return value?.ToString();
        }

        public bool IsEmpty(object value)
        {
            return string.IsNullOrWhiteSpace(value?.ToString());
        }
    }
}
