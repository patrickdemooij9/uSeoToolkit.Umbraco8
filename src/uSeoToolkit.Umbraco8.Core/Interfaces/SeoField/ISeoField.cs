using System.Collections.Generic;
using System.Web;

namespace uSeoToolkit.Umbraco8.Core.Interfaces.SeoField
{
    public interface ISeoField
    {
        string Title { get; }
        string Alias { get; }
        string Description { get; }
        string View { get; }
        Dictionary<string, object> Config { get; }
        ISeoFieldEditor Editor { get; }
        //ISeoFieldRenderer Renderer { get; }

        HtmlString Render(string value);
    }
}
