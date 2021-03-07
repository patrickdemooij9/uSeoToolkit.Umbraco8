using System.Collections.Generic;
using System.Web;

namespace uSeoToolkit.Umbraco8.Core.Models.SeoField
{
    public interface ISeoField
    {
        string Title { get; }
        string Alias { get; }
        string Description { get; }
        string View { get; }
        Dictionary<string, object> Config { get; }

        HtmlString Render(string value);
    }
}
