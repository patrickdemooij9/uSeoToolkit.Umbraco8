using System.Collections.Generic;
using uSeoToolkit.Umbraco8.Core.Models.SeoField;

namespace uSeoToolkit.Umbraco8.Core.Models.SeoService
{
    public class MetaTagsModel
    {
        public Dictionary<ISeoField, string> Fields { get; }

        //TODO: Add helper methods for basic meta tags

        public MetaTagsModel(Dictionary<ISeoField, string> fields)
        {
            Fields = fields;
        }
    }
}
