using System.Collections.Generic;

namespace uSeoToolkit.Umbraco8.Core.Interfaces.Services
{
    public interface ISeoValueService
    {
        Dictionary<string, object> GetUserValues(int nodeId);
        void AddValues(int nodeId, Dictionary<string, object> values);
        void Delete(int nodeId, string fieldAlias);
    }
}
