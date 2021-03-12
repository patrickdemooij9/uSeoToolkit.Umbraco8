using System.Collections.Generic;
using uSeoToolkit.Umbraco8.Core.Interfaces.SeoField;

namespace uSeoToolkit.Umbraco8.Core.Models.SeoFieldEditors
{
    public class SeoFieldFieldsEditor : ISeoFieldEditor
    {
        private readonly string[] _fieldTypes;
        public string View => "/App_Plugins/uSeoToolkit/Interface/SeoFieldEditors/FieldsEditor/fieldsEditor.html";
        public Dictionary<string, object> Config => new Dictionary<string, object>
        {
            {"dataTypes", _fieldTypes}
        };

        public SeoFieldFieldsEditor(string[] fieldTypes)
        {
            _fieldTypes = fieldTypes;
        }
    }
}
