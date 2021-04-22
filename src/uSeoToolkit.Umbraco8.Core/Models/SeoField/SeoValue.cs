namespace uSeoToolkit.Umbraco8.Core.Models.SeoField
{
    public class SeoValue
    {
        public string FieldAlias { get; }
        public object Value { get; set; }
        public bool IsUserValue { get; set; }

        public SeoValue(string fieldAlias, object value)
        {
            FieldAlias = fieldAlias;
            Value = value;
        }
    }
}
