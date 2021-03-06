using NPoco;
using Umbraco.Core.Persistence.DatabaseAnnotations;

namespace uSeoToolkit.Umbraco8.Core.Models.SiteAudit.Database
{
    [TableName("uSeoToolkitSiteAudit")]
    [PrimaryKey("Id", AutoIncrement = true)]
    public class SiteAuditEntity
    {
        [PrimaryKeyColumn(AutoIncrement = true)]
        [Column("Id")]
        public int Id { get; set; }

        [Column("Name")]
        public string Name { get; set; }

        [Column("StartingNodeId")]
        public int StartingNodeId { get; set; }

        [Column("MaxPagesToCrawl")]
        public int MaxPagesToCrawl { get; set; }

        [Column("DelayBetweenRequests")]
        public int DelayBetweenRequests { get; set; }
    }
}
