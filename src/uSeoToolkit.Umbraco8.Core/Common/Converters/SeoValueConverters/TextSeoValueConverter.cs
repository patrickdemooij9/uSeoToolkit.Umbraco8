﻿using System;
using Umbraco.Core.Models.PublishedContent;
using uSeoToolkit.Umbraco8.Core.Interfaces.Converters;

namespace uSeoToolkit.Umbraco8.Core.Common.Converters.SeoValueConverters
{
    public class TextSeoValueConverter : ISeoValueConverter
    {
        public Type FromValue => typeof(string);
        public Type ToValue => typeof(string);
        public object Convert(object value, IPublishedContent currentContent)
        {
            return value?.ToString();
        }
    }
}
