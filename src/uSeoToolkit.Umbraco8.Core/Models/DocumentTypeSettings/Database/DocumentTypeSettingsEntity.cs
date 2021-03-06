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

        [Column("Fields")]
        [NullSetting(NullSetting = NullSettings.Null)]
        [SpecialDbType(SpecialDbTypes.NTEXT)]
        public string Fields { get; set; }

        [Column("InheritanceId")]
        [NullSetting(NullSetting = NullSettings.Null)]
        public int? InheritanceId { get; set; }
    }
}
