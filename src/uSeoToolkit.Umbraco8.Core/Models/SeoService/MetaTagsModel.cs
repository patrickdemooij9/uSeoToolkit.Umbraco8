using System.Collections.Generic;
using System.Linq;
using uSeoToolkit.Umbraco8.Core.Interfaces.SeoField;

namespace uSeoToolkit.Umbraco8.Core.Models.SeoService
{
    public class MetaTagsModel
    {
        public Dictionary<ISeoField, string> Fields { get; }

        //TODO: Add helper methods for basic meta tags

        public MetaTagsModel(Dictionary<ISeoField, string> fields)
        {
            Fields = fields ?? new Dictionary<ISeoField, string>();
        }

        public string GetValue(string alias)
        {
            var keyValue = Fields.FirstOrDefault(it => it.Key.Alias.Equals(alias));
            return keyValue.Key is null ? string.Empty : keyValue.Value;
        }
    }
}
