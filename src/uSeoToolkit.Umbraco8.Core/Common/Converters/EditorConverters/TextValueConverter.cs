using uSeoToolkit.Umbraco8.Core.Interfaces.Converters;

namespace uSeoToolkit.Umbraco8.Core.Common.Converters.EditorConverters
{
    public class TextValueConverter : IEditorValueConverter
    {
        public object ConvertEditorToDatabaseValue(object value)
        {
            return value?.ToString();
        }

        public object ConvertObjectToEditorValue(object value)
        {
            return value?.ToString();
        }

        public object ConvertDatabaseToObject(object value)
        {
            return value?.ToString();
        }

        public bool IsEmpty(object value)
        {
            return string.IsNullOrWhiteSpace(value?.ToString());
        }
    }
}
