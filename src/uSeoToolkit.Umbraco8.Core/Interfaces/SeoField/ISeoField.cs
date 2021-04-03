using System.Collections.Generic;
using System.Web;

namespace uSeoToolkit.Umbraco8.Core.Interfaces.SeoField
{
    public interface ISeoField
    {
        string Title { get; }
        string Alias { get; }
        string Description { get; }
        ISeoFieldEditor Editor { get; }
        //ISeoFieldRenderer Renderer { get; }

        HtmlString Render(string value);
    }
}
