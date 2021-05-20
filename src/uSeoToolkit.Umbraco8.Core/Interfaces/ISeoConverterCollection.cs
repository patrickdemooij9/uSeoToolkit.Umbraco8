using System;
using uSeoToolkit.Umbraco8.Core.Interfaces.Converters;

namespace uSeoToolkit.Umbraco8.Core.Interfaces
{
    public interface ISeoConverterCollection
    {
        ISeoValueConverter GetConverter(Type fromType, Type toType);
    }
}
