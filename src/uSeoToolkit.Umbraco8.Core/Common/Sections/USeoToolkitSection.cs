using Umbraco.Core.Models.Sections;

namespace uSeoToolkit.Umbraco8.Core.Common.Sections
{
    public class USeoToolkitSection : ISection
    {
        public const string SectionAlias = "uSeoToolkit";
        public const string SectionName = "uSeoToolkit";

        public string Alias => SectionAlias;

        public string Name => SectionName;
    }
}
