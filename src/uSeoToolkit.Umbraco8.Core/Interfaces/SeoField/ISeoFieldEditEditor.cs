using System.Collections.Generic;
using uSeoToolkit.Umbraco8.Core.Interfaces.SeoValueConverters;

namespace uSeoToolkit.Umbraco8.Core.Interfaces.SeoField
{
    public interface ISeoFieldEditEditor
    {
        string View { get; }
        Dictionary<string, object> Config { get; }
        ISeoValueConverter ValueConverter { get; }
    }
}
