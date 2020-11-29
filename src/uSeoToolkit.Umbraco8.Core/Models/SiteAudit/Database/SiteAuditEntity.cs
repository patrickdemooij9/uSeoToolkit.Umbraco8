using NPoco;
using Umbraco.Core.Persistence.DatabaseAnnotations;

namespace uSeoToolkit.Umbraco8.Core.Models.SiteAudit.Database
{
    [TableName("uSeoToolkitSiteAudit")]
    [ExplicitColumns]
    [PrimaryKey("Id", AutoIncrement = true)]
    public class SiteAuditEntity
    {
        [Column("Id")]
        [PrimaryKeyColumn(AutoIncrement = true)]
        public int Id { get; set; }

        [Column("Name")]
        public string Name { get; set; }

        [Column("StartingNodeId")]
        public int StartingNodeId { get; set; }
    }
}
