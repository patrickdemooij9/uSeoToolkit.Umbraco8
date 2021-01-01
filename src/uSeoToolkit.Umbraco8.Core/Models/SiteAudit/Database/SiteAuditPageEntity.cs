using NPoco;
using Umbraco.Core.Persistence.DatabaseAnnotations;

namespace uSeoToolkit.Umbraco8.Core.Models.SiteAudit.Database
{
    [TableName("uSeoToolkitSiteAuditPage")]
    [ExplicitColumns]
    [PrimaryKey("Id", AutoIncrement = true)]
    public class SiteAuditPageEntity
    {
        [Column("Id")]
        [PrimaryKeyColumn(AutoIncrement = true)]
        public int Id { get; set; }

        [Column("AuditId")]
        [ForeignKey(typeof(SiteAuditEntity), Column = "Id")]
        public int AuditId { get; set; }

        [Column("Url")]
        public string Url { get; set; }

        [Column("StatusCode")]
        public int StatusCode { get; set; }
    }
}
