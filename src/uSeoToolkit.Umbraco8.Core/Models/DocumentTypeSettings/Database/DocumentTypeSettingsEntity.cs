using NPoco;
using Umbraco.Core.Persistence.DatabaseAnnotations;

namespace uSeoToolkit.Umbraco8.Core.Models.DocumentTypeSettings.Database
{
    [TableName("uSeoToolkitDocumentTypeSettings")]
    [ExplicitColumns]
    [PrimaryKey("NodeId", AutoIncrement = false)]
    public class DocumentTypeSettingsEntity
    {
        [Column("NodeId")]
        [PrimaryKeyColumn(AutoIncrement = false)]
        public int NodeId { get; set; }

        [Column("EnableSeoSettings")]
        public bool EnableSeoSettings { get; set; }

        [Column("DefaultTitleFields")]
        [NullSetting(NullSetting = NullSettings.Null)]
        public string DefaultTitleFields { get; set; }

        [Column("DefaultDescriptionFields")]
        [NullSetting(NullSetting = NullSettings.Null)]
        public string DefaultDescriptionFields { get; set; }
    }
}
