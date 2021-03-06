using System;
using NPoco;
using Umbraco.Core.Persistence.DatabaseAnnotations;

namespace uSeoToolkit.Umbraco8.Core.Models.SiteAudit.Database
{
    [TableName("uSeoToolkitSiteAuditCheckResult")]
    [PrimaryKey("Id", AutoIncrement = true)]
    public class SiteAuditCheckResultEntity
    {
        [Column("Id")]
        [PrimaryKeyColumn(AutoIncrement = true)]
        public int Id { get; set; }

        [Column("PageId")]
        [ForeignKey(typeof(SiteAuditPageEntity), Column = "Id")]
        public int PageId { get; set; }

        [Column("CheckId")]
        public Guid CheckId { get; set; }

        [Column("ResultId")]
        public int ResultId { get; set; }

        //Json object containing any extra values of the result
        [Column("ExtraValues")]
        public string ExtraValues { get; set; }
    }
}
