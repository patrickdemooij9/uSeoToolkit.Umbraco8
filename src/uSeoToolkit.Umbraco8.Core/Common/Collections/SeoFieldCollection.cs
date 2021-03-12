using System;
using System.Collections.Generic;
using System.Linq;
using Umbraco.Core.Composing;
using uSeoToolkit.Umbraco8.Core.Interfaces;
using uSeoToolkit.Umbraco8.Core.Interfaces.SeoField;
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
            return this.OrderBy(it => GetWeight(it.GetType()));
        }

        private int GetWeight(Type type)
        {
            var attr = type.GetCustomAttributes(typeof(WeightAttribute), false).OfType<WeightAttribute>().SingleOrDefault();
            return attr?.Weight ?? 0;
        }
    }
}
