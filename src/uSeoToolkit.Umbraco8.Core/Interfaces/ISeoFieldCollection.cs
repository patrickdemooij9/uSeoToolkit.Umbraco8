using System.Collections.Generic;
using uSeoToolkit.Umbraco8.Core.Models.SeoField;

namespace uSeoToolkit.Umbraco8.Core.Interfaces
{
    public interface ISeoFieldCollection
    {
        ISeoField Get(string alias);
        IEnumerable<ISeoField> GetAll();
    }
}
