using Moq;
using NUnit.Framework;
using Umbraco.Core;
using Umbraco.Core.Models;
using Umbraco.Core.Models.PublishedContent;
using uSeoToolkit.Umbraco8.Core.Common.Providers;
using uSeoToolkit.Umbraco8.Core.Interfaces;
using uSeoToolkit.Umbraco8.Core.Models.DocumentTypeSettings.Business;
using uSeoToolkit.Umbraco8.Core.Services.DocumentTypeSettings;
using uSeoToolkit.Umbraco8.Tests.Mocks;

namespace uSeoToolkit.Umbraco8.Tests.Common.Providers
{
    [TestFixture]
    public class DefaultMetaTagsProviderTests
    {
        [Test]
        public void TestSimpleField()
        {
            var contentMock = new Mock<IPublishedContent>();
            contentMock.Setup(it => it.ContentType).Returns(new Mock<IPublishedContentType>().Object);
            var seoField = new SeoFieldMock
            {
                Title = "Test",
                Alias = "test",
                Editor = new SeoFieldEditorMock(getValueFunc: (content, value) => "Test")
            };
            var documentTypeSettingsService = new Mock<IDocumentTypeSettingsService>();
            documentTypeSettingsService.Setup(it => it.Get(It.IsAny<int>())).Returns(new DocumentTypeSettingsDto
            {
                EnableSeoSettings = true
            });
            var seoFieldCollection = new Mock<ISeoFieldCollection>();
            seoFieldCollection.Setup(it => it.GetAll()).Returns(seoField.AsEnumerableOfOne());
            seoFieldCollection.Setup(it => it.Get("test")).Returns(seoField);

            var metaTagsProvider = new DefaultMetaTagsProvider(documentTypeSettingsService.Object, seoFieldCollection.Object);

            var metaTags = metaTagsProvider.Get(contentMock.Object);

            Assert.IsTrue(metaTags.GetValue("test")?.Equals("Test"));
        }
    }
}
