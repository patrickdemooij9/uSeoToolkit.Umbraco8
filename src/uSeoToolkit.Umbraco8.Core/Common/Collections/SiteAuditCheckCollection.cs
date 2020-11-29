using System;
using System.Collections.Generic;
using System.Linq;
using SeoToolkit.Core.Interfaces.SiteAudit;
using Umbraco.Core.Composing;

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
