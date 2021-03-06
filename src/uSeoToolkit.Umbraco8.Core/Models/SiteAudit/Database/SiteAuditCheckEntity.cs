using NPoco;
using System;
using Umbraco.Core.Persistence.DatabaseAnnotations;

namespace uSeoToolkit.Umbraco8.Core.Models.SiteAudit.Database
{
    [TableName("uSeoToolkitSiteAuditCheck")]
    [PrimaryKey("Id", AutoIncrement = true)]
    public class SiteAuditCheckEntity
    {
        //TODO: See if we can have 2 primary keys here instead of autoincrement
        [PrimaryKeyColumn(AutoIncrement = true)]
        [Column("Id")]
        public int Id { get; set; }

        [ForeignKey(typeof(SiteAuditEntity), Column = "Id", Name = "FK_AuditCheck_Audit")]
        [Column("AuditId")]
        public int AuditId { get; set; }

        [Column("CheckGuid")]
        public Guid CheckGuid { get; set; }
    }
}
