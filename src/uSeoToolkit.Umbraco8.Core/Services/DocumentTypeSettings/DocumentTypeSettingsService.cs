using Umbraco.Core.Models;
using uSeoToolkit.Umbraco8.Core.Interfaces;
using uSeoToolkit.Umbraco8.Core.Models.DocumentTypeSettings.Business;

namespace uSeoToolkit.Umbraco8.Core.Services.DocumentTypeSettings
{
    public class DocumentTypeSettingsService : IDocumentTypeSettingsService
    {
        private readonly IRepository<DocumentTypeSettingsDto> _repository;

        public DocumentTypeSettingsService(IRepository<DocumentTypeSettingsDto> repository)
        {
            _repository = repository;
        }

        public void Set(DocumentTypeSettingsDto model)
        {
            var exists = _repository.Get(model.Content.Id) != null;
            if (exists)
                _repository.Update(model);
            else
                _repository.Add(model);
        }

        public DocumentTypeSettingsDto Get(int id)
        {
            return _repository.Get(id);
        }

        public bool IsEnabled(IContent content)
        {
            return Get(content.ContentTypeId)?.EnableSeoSettings is true;
        }
    }
}
