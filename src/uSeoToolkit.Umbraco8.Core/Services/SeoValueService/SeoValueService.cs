using System.Collections.Generic;
using Umbraco.Core;
using uSeoToolkit.Umbraco8.Core.Interfaces;
using uSeoToolkit.Umbraco8.Core.Interfaces.Services;

namespace uSeoToolkit.Umbraco8.Core.Services.SeoValueService
{
    public class SeoValueService : ISeoValueService
    {
        private readonly ISeoValueRepository _repository;

        public SeoValueService(ISeoValueRepository repository)
        {
            _repository = repository;
        }

        public Dictionary<string, object> GetUserValues(int nodeId)
        {
            return _repository.GetAllValues(nodeId);
        }

        public void AddValues(int nodeId, Dictionary<string, object> values)
        {
            foreach (var (key, value) in values)
            {
                if (_repository.Exists(nodeId, key))
                    _repository.Update(nodeId, key, value);
                else
                    _repository.Add(nodeId, key, value);
            }
        }

        public void Delete(int nodeId, string fieldAlias)
        {
            _repository.Delete(nodeId, fieldAlias);
        }
    }
}
