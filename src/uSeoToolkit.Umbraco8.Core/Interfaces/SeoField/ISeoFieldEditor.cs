using System.Collections.Generic;

namespace uSeoToolkit.Umbraco8.Core.Interfaces.SeoField
{
    public interface ISeoFieldEditor
    {
        string View { get; }
        Dictionary<string, object> Config { get; }
    }
}
