using NPoco;
using System;
using Umbraco.Core.Persistence.DatabaseAnnotations;

namespace uSeoToolkit.Umbraco8.Core.Models.SiteAudit.Database
{
    [TableName("uSeoToolkitSiteAuditCheck")]
    [ExplicitColumns]
    [PrimaryKey("Id")]
    public class SiteAuditCheckEntity
    {
        //TODO: See if we can have 2 primary keys here instead of autoincrement
        [Column("Id")]
        [PrimaryKeyColumn(AutoIncrement = true)]
        public int Id { get; set; }

        [Column("AuditId")]
        [ForeignKey(typeof(SiteAuditEntity), Column = "Id", Name = "FK_AuditCheck_Audit")]
        public int AuditId { get; set; }

        [Column("CheckGuid")]
        public Guid CheckGuid { get; set; }
    }
}
