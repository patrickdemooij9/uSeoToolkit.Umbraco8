﻿using System;
using System.Collections.Generic;
using Umbraco.Core;
using Umbraco.Core.Models.PublishedContent;
using uSeoToolkit.Umbraco8.Core.Common.Converters.EditorConverters;
using uSeoToolkit.Umbraco8.Core.Interfaces.Converters;
using uSeoToolkit.Umbraco8.Core.Interfaces.SeoField;

namespace uSeoToolkit.Umbraco8.Core.Models.SeoFieldEditors
{
    public class SeoFieldPropertyEditor : ISeoFieldEditor
    {
        private readonly string _propertyView;
        private readonly Func<IPublishedContent, object, string> _getValueTransformation;
        public string View => "/App_Plugins/uSeoToolkit/Interface/SeoFieldEditors/PropertyEditor/propertyEditor.html";

        public Dictionary<string, object> Config => new Dictionary<string, object>
        {
            {"view", _propertyView}
        };

        public IEditorValueConverter ValueConverter { get; }

        public SeoFieldPropertyEditor(string propertyView, Func<IPublishedContent, object, string> getValueTransformation = null, IEditorValueConverter valueConverter = null)
        {
            _propertyView = propertyView;
            _getValueTransformation = getValueTransformation;

            ValueConverter = valueConverter ?? new TextValueConverter();
        }

        public object Inherit(object currentValue, object inheritedValue)
        {
            return currentValue?.ToString().Length > 0 ? currentValue : inheritedValue;
        }

        public string GetValue(IPublishedContent content, object value)
        {
            return _getValueTransformation != null ? _getValueTransformation(content, value) : value?.ToString();
        }
    }
}
