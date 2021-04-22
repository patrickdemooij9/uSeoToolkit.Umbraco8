namespace uSeoToolkit.Umbraco8.Core.Interfaces.SeoValueConverters
{
    public interface ISeoValueConverter
    {
        object ConvertEditorToDatabaseValue(object value);
        object ConvertDatabaseToEditorValue(object value);
        string ConvertDatabaseToSeoValue(object value);

        bool IsEmpty(object value);
    }
}
