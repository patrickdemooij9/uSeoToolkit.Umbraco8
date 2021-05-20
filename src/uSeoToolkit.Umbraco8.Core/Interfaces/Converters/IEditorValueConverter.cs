namespace uSeoToolkit.Umbraco8.Core.Interfaces.Converters
{
    public interface IEditorValueConverter
    {
        object ConvertEditorToDatabaseValue(object value);
        object ConvertObjectToEditorValue(object value);
        object ConvertDatabaseToObject(object value);

        bool IsEmpty(object value);
    }
}
