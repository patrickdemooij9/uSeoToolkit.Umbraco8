﻿using System.Collections.Generic;
using System.Linq;
using Umbraco.Core.Models;
using uSeoToolkit.Umbraco8.Core.Constants;
using uSeoToolkit.Umbraco8.Core.Interfaces.SeoField;

namespace uSeoToolkit.Umbraco8.Core.Models.DocumentTypeSettings.Business
{
    public class DocumentTypeSettingsDto
    {
        public IContentType Content { get; set; }
        public bool EnableSeoSettings { get; set; }
        public Dictionary<ISeoField, string> Fields { get; set; }
        public IContentType Inheritance { get; set; }

        public DocumentTypeSettingsDto()
        {
            Fields = new Dictionary<ISeoField, string>();
        }

        public string Get(string alias)
        {
            var valuePair = Fields.FirstOrDefault(it => it.Key.Alias.Equals(alias));
            return valuePair.Key is null ? string.Empty : valuePair.Value;
        }

        public string[] GetDefaultTitleFields() => Get(SeoFieldAliasConstants.Title).Split(',');
        public string[] GetDefaultDescriptionFields() => Get(SeoFieldAliasConstants.MetaDescription).Split(',');
    }
}
