using System.Collections.Generic;
using Umbraco.Core.Models;
using uSeoToolkit.Umbraco8.Core.Constants;

namespace uSeoToolkit.Umbraco8.Core.Models.DocumentTypeSettings.Business
{
    public class DocumentTypeSettingsDto
    {
        public IContentType Content { get; set; }
        public bool EnableSeoSettings { get; set; }
        public Dictionary<string, string> Fields { get; set; }
        public IContentType Inheritance { get; set; }

        public DocumentTypeSettingsDto()
        {
            Fields = new Dictionary<string, string>();
        }

        public string Get(string alias)
        {
            return Fields.ContainsKey(alias) ? Fields[alias] : string.Empty;
        }

        public string[] GetDefaultTitleFields() => Get(SeoFieldAliasConstants.Title).Split(',');
        public string[] GetDefaultDescriptionFields() => Get(SeoFieldAliasConstants.MetaDescription).Split(',');
    }
}
