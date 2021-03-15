using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using Umbraco.Core.Mapping;
using Umbraco.Core.Persistence;
using Umbraco.Core.Scoping;
using Umbraco.Core.Services;
using uSeoToolkit.Umbraco8.Core.Interfaces;
using uSeoToolkit.Umbraco8.Core.Models.DocumentTypeSettings.Business;
using uSeoToolkit.Umbraco8.Core.Models.DocumentTypeSettings.Database;

namespace uSeoToolkit.Umbraco8.Core.Repositories
{
    public class DocumentTypeSettingsRepository : IDocumentTypeSettingsRepository
    {
        private readonly IScopeProvider _scopeProvider;
        private readonly ServiceContext _services;
        private readonly Lazy<UmbracoMapper> _mapper;

        public DocumentTypeSettingsRepository(IScopeProvider scopeProvider, ServiceContext services, Lazy<UmbracoMapper> mapper)
        {
            _scopeProvider = scopeProvider;
            _services = services;
            _mapper = mapper;
        }

        public IEnumerable<DocumentTypeSettingsDto> GetAll()
        {
            using (var scope = _scopeProvider.CreateScope())
            {
                return scope.Database.Fetch<DocumentTypeSettingsEntity>(scope.SqlContext.Sql()
                    .SelectAll()
                    .From<DocumentTypeSettingsEntity>()).Select(it => _mapper.Value.Map<DocumentTypeSettingsDto>(it));
            }
        }

        public DocumentTypeSettingsDto Get(int id)
        {
            using (var scope = _scopeProvider.CreateScope())
            {
                return _mapper.Value.Map<DocumentTypeSettingsDto>(scope.Database.FirstOrDefault<DocumentTypeSettingsEntity>(scope.SqlContext.Sql()
                    .SelectAll()
                    .From<DocumentTypeSettingsEntity>()
                    .Where<DocumentTypeSettingsEntity>(it => it.NodeId == id)));
            }
        }

        public DocumentTypeSettingsDto Add(DocumentTypeSettingsDto model)
        {
            var entity = MapToEntity(model);
            using (var scope = _scopeProvider.CreateScope())
            {
                scope.Database.Insert(entity);
                scope.Complete();
            }
            return model;
        }

        public DocumentTypeSettingsDto Update(DocumentTypeSettingsDto model)
        {
            using (var scope = _scopeProvider.CreateScope())
            {
                scope.Database.Update(MapToEntity(model));
                scope.Complete();
            }

            return model;
        }

        public void Delete(int id)
        {
            var entity = Get(id);
            if (entity is null)
                return;
            using (var scope = _scopeProvider.CreateScope())
            {
                scope.Database.Delete(entity);
            }
        }

        private DocumentTypeSettingsEntity MapToEntity(DocumentTypeSettingsDto dto)
        {
            if (dto is null)
                return null;
            return new DocumentTypeSettingsEntity
            {
                NodeId = dto.Content.Id,
                EnableSeoSettings = dto.EnableSeoSettings,
                Fields = JsonConvert.SerializeObject(dto.Fields),
                InheritanceId = dto.Inheritance?.Id
            };
        }
    }
}
