using uSeoToolkit.Umbraco8.Core.Interfaces.SeoField;

namespace uSeoToolkit.Umbraco8.Core.Models.SeoFieldPreviewers
{
    public class BaseTagsFieldPreviewer : ISeoFieldPreviewer
    {
        public string Title => "Google";
        public string View => "~/App_Plugins/uSeoToolkit/Interface/Previewers/BaseTags/baseTagsFieldPreviewer.html";
    }
}
