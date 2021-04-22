using Umbraco.Core.Migrations;
using Umbraco.Web.Models.ContentEditing;
using uSeoToolkit.Umbraco8.Core.Models.DocumentTypeSettings.Database;
using uSeoToolkit.Umbraco8.Core.Models.SeoSettings.Database;
using uSeoToolkit.Umbraco8.Core.Models.SiteAudit.Database;

namespace uSeoToolkit.Umbraco8.Core.Common.Migrations
{
    public class USeoToolkitMigration : MigrationBase
    {
        public USeoToolkitMigration(IMigrationContext context) : base(context)
        {
        }

        public override void Migrate()
        {
            if (!TableExists("uSeoToolkitSiteAudit"))
            {
                Create.Table<SiteAuditEntity>().Do();
            }
            if (!TableExists("uSeoToolkitSiteAuditCheck"))
            {
                Create.Table<SiteAuditCheckEntity>().Do();
            }
            if (!TableExists("uSeoToolkitSiteAuditPage"))
            {
                Create.Table<SiteAuditPageEntity>().Do();
            }
            if (!TableExists("uSeoToolkitSiteAuditCheckResult"))
            {
                Create.Table<SiteAuditCheckResultEntity>().Do();
                Database.Execute("ALTER TABLE uSeoToolkitSiteAuditCheckResult ALTER COLUMN ExtraValues NTEXT");
            }
            if (!TableExists("uSeoToolkitDocumentTypeSettings"))
            {
                Create.Table<DocumentTypeSettingsEntity>().Do();
            }

            if (!TableExists("uSeoToolkitSeoValue"))
            {
                Create.Table<SeoValueEntity>().Do();
                //Database.Execute("ALTER TABLE [uSeoToolkitSeoValue] ADD CONSTRAINT [PK_uSeoToolkitSeoValue] PRIMARY KEY  ([Member], [MemberGroup]) ");
            }
        }
    }
}
