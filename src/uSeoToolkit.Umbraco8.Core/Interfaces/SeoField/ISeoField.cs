using System.Collections.Generic;
using System.Web;
using uSeoToolkit.Umbraco8.Core.Interfaces.SeoValueConverters;

namespace uSeoToolkit.Umbraco8.Core.Interfaces.SeoField
{
    public interface ISeoField
    {
        string Title { get; }
        string Alias { get; }
        string Description { get; }
        ISeoFieldEditor Editor { get; }
        ISeoFieldEditEditor EditEditor { get; }
        //ISeoFieldRenderer Renderer { get; }

        HtmlString Render(string value);
    }
}
