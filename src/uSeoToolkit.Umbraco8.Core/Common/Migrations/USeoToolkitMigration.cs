using Umbraco.Core.Migrations;
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
            if (!TableExists("uSeoToolkitSiteAuditCheckResult"))
            {
                Create.Table<SiteAuditCheckResultEntity>().Do();
                Database.Execute("ALTER TABLE uSeoToolkitSiteAuditCheckResult ALTER COLUMN ExtraValues NVARCHAR(MAX)");
            }
        }
    }
}
