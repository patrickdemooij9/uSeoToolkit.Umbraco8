using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
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

        public DocumentTypeSettingsRepository(IScopeProvider scopeProvider, ServiceContext services)
        {
            _scopeProvider = scopeProvider;
            _services = services;
        }

        public IEnumerable<DocumentTypeSettingsDto> GetAll()
        {
            using (var scope = _scopeProvider.CreateScope())
            {
                return scope.Database.Fetch<DocumentTypeSettingsEntity>(scope.SqlContext.Sql()
                    .SelectAll()
                    .From<DocumentTypeSettingsEntity>()).Select(MapToDto);
            }
        }

        public DocumentTypeSettingsDto Get(int id)
        {
            using (var scope = _scopeProvider.CreateScope())
            {
                return MapToDto(scope.Database.FirstOrDefault<DocumentTypeSettingsEntity>(scope.SqlContext.Sql()
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

        private DocumentTypeSettingsDto MapToDto(DocumentTypeSettingsEntity entity)
        {
            if (entity is null)
                return null;
            var inheritance = entity.InheritanceId != null ? _services.ContentTypeService.Get(entity.InheritanceId.Value) : null;
            return new DocumentTypeSettingsDto
            {
                EnableSeoSettings = entity.EnableSeoSettings,
                Fields = string.IsNullOrWhiteSpace(entity.Fields) ? new Dictionary<string, string>() : JsonConvert.DeserializeObject<Dictionary<string, string>>(entity.Fields),
                Inheritance = inheritance
            };
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
