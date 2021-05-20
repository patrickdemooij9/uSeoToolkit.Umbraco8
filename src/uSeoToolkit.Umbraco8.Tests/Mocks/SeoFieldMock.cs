using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using uSeoToolkit.Umbraco8.Core.Interfaces.SeoField;

namespace uSeoToolkit.Umbraco8.Tests.Mocks
{
    public class SeoFieldMock : ISeoField
    {
        public string Title { get; set; }
        public string Alias { get; set; }
        public string Description { get; set; }
        public Type FieldType { get; set; }
        public string View { get; set; }
        public ISeoFieldEditor Editor { get; set; }
        public ISeoFieldEditEditor EditEditor { get; set; }
        public HtmlString Render(object value)
        {
            throw new NotImplementedException();
        }
    }
}
