using System.Collections.Generic;
using System.Linq;
using Umbraco.Core.Persistence;
using Umbraco.Core.Scoping;
using uSeoToolkit.Umbraco8.Core.Interfaces;
using uSeoToolkit.Umbraco8.Core.Models.DocumentTypeSettings.Business;
using uSeoToolkit.Umbraco8.Core.Models.DocumentTypeSettings.Database;

namespace uSeoToolkit.Umbraco8.Core.Repositories
{
    public class DocumentTypeSettingsRepository : IDocumentTypeSettingsRepository
    {
        private readonly IScopeProvider _scopeProvider;

        public DocumentTypeSettingsRepository(IScopeProvider scopeProvider)
        {
            _scopeProvider = scopeProvider;
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

        public void Add(DocumentTypeSettingsDto model)
        {
            using (var scope = _scopeProvider.CreateScope())
            {
                var entity = MapToEntity(model);
                scope.Database.Insert(entity);
                scope.Complete();
            }
        }

        public void Update(DocumentTypeSettingsDto model)
        {
            using (var scope = _scopeProvider.CreateScope())
            {
                scope.Database.Update(MapToEntity(model));
                scope.Complete();
            }
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
            return new DocumentTypeSettingsDto
            {
                EnableSeoSettings = entity.EnableSeoSettings,
                DefaultTitleFields = entity.DefaultTitleFields.Split(','),
                DefaultDescriptionFields = entity.DefaultDescriptionFields.Split(',')
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
                DefaultTitleFields = string.Join(",", dto.DefaultTitleFields),
                DefaultDescriptionFields = string.Join(",", dto.DefaultDescriptionFields)
            };
        }
    }
}
