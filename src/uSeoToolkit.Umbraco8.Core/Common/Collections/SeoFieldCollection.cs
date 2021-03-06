﻿using System.Collections.Generic;
using System.Linq;
using Umbraco.Core.Composing;
using uSeoToolkit.Umbraco8.Core.Interfaces;
using uSeoToolkit.Umbraco8.Core.Models.SeoField;

namespace uSeoToolkit.Umbraco8.Core.Common.Collections
{
    public class SeoFieldCollection : BuilderCollectionBase<ISeoField>, ISeoFieldCollection
    {
        public SeoFieldCollection(IEnumerable<ISeoField> items) : base(items)
        {
        }

        public ISeoField Get(string alias)
        {
            return this.FirstOrDefault(it => it.Alias == alias);
        }

        public IEnumerable<ISeoField> GetAll()
        {
            return this;
        }
    }
}