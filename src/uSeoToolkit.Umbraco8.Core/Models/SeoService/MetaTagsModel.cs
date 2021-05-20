using System.Collections.Generic;
using System.Linq;
using uSeoToolkit.Umbraco8.Core.Interfaces.SeoField;

namespace uSeoToolkit.Umbraco8.Core.Models.SeoService
{
    public class MetaTagsModel
    {
        public Dictionary<ISeoField, object> Fields { get; }

        //TODO: Add helper methods for basic meta tags

        public MetaTagsModel(Dictionary<ISeoField, object> fields)
        {
            Fields = fields ?? new Dictionary<ISeoField, object>();
        }

        public object GetValue(string alias)
        {
            var keyValue = Fields.FirstOrDefault(it => it.Key.Alias.Equals(alias));
            return keyValue.Key is null ? string.Empty : keyValue.Value;
        }
    }
}
