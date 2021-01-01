using Umbraco.Core.Models;
using uSeoToolkit.Umbraco8.Core.Models.DocumentTypeSettings.Business;

namespace uSeoToolkit.Umbraco8.Core.Services.DocumentTypeSettings
{
    public interface IDocumentTypeSettingsService
    {
        void Set(DocumentTypeSettingsDto model);
        DocumentTypeSettingsDto Get(int id);

        bool IsEnabled(IContent content);
    }
}
