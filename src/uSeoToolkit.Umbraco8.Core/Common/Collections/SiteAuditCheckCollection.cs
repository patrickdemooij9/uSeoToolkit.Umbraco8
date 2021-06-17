using System;
using System.Collections.Generic;
using System.Linq;
using Umbraco.Core.Composing;
using uSeoToolkit.Umbraco8.Core.Interfaces.SiteAudit;

namespace uSeoToolkit.Umbraco8.Core.Common.Collections
{
    public class SiteAuditCheckCollection : BuilderCollectionBase<ISiteCheck>, ISiteCheckCollection
    {
        public SiteAuditCheckCollection(IEnumerable<ISiteCheck> items) : base(items)
        {
        }

        public IEnumerable<ISiteCheck> GetAll()
        {
            return this;
        }

        public ISiteCheck GetCheckByAlias(Guid id)
        {
            return GetAll().FirstOrDefault(it => it.Id == id);
        }
    }
}
