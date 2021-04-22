using System.Collections.Generic;

namespace uSeoToolkit.Umbraco8.Core.Interfaces
{
    public interface ISeoValueRepository
    {
        void Add(int nodeId, string fieldAlias, object value);
        void Update(int nodeId, string fieldAlias, object value);
        void Delete(int nodeId, string fieldAlias);
        bool Exists(int nodeId, string fieldAlias);

        Dictionary<string, object> GetAllValues(int nodeId);
    }
}
