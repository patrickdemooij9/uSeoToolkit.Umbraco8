using System;
using System.Collections.Generic;

namespace uSeoToolkit.Umbraco8.Core.Interfaces.SiteAudit
{
    public interface ISiteCheckCollection
    {
        IEnumerable<ISiteCheck> GetAll();
        ISiteCheck GetCheckByAlias(Guid id);
    }
}
