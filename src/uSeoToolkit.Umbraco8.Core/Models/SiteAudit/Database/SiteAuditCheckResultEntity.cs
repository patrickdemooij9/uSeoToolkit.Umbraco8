using NPoco;
using Umbraco.Core.Persistence.DatabaseAnnotations;

namespace uSeoToolkit.Umbraco8.Core.Models.SiteAudit.Database
{
    [TableName("uSeoToolkitSiteAuditCheckResult")]
    [ExplicitColumns]
    [PrimaryKey("Id")]
    public class SiteAuditCheckResultEntity
    {
        [Column("Id")]
        [PrimaryKeyColumn(AutoIncrement = true)]
        public int Id { get; set; }

        [Column("CheckId")]
        public int CheckId { get; set; }

        [Column("SiteAuditId")]
        [ForeignKey(typeof(SiteAuditEntity), Column = "Id")]
        public int SiteAuditId { get; set; }

        [Column("ResultId")]
        public int ResultId { get; set; }

        //Json object containing any extra values of the result
        [Column("ExtraValues")]
        public string ExtraValues { get; set; }
    }
}
