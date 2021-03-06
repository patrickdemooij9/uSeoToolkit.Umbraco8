﻿using System.Collections.Generic;
using Moq;
using NUnit.Framework;
using Umbraco.Core;
using Umbraco.Core.Logging;
using Umbraco.Core.Models;
using Umbraco.Core.Models.PublishedContent;
using uSeoToolkit.Umbraco8.Core.Common.Providers;
using uSeoToolkit.Umbraco8.Core.Interfaces;
using uSeoToolkit.Umbraco8.Core.Interfaces.SeoField;
using uSeoToolkit.Umbraco8.Core.Interfaces.Services;
using uSeoToolkit.Umbraco8.Core.Models.DocumentTypeSettings.Business;
using uSeoToolkit.Umbraco8.Core.Services.DocumentTypeSettings;
using uSeoToolkit.Umbraco8.Core.Services.SeoValueService;
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
                EnableSeoSettings = true,
                Fields = new Dictionary<ISeoField, object>
                {
                    {seoField, "Test"}
                }
            });
            var seoFieldCollection = new Mock<ISeoFieldCollection>();
            seoFieldCollection.Setup(it => it.GetAll()).Returns(seoField.AsEnumerableOfOne());
            seoFieldCollection.Setup(it => it.Get("test")).Returns(seoField);
            var seoValueService = new Mock<ISeoValueService>();
            var seoConverterCollection = new Mock<ISeoConverterCollection>();
            var logger = new Mock<ILogger>();

            var metaTagsProvider = new DefaultMetaTagsProvider(documentTypeSettingsService.Object, seoFieldCollection.Object, seoValueService.Object, seoConverterCollection.Object, logger.Object);

            var metaTags = metaTagsProvider.Get(contentMock.Object);

            Assert.IsTrue(metaTags.GetValue<string>("test")?.Equals("Test"));
        }

        [Test]
        public void TestMetaTagsWithEventHandling()
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
                EnableSeoSettings = true,
                Fields = new Dictionary<ISeoField, object>
                {
                    {seoField, "Test"}
                }
            });
            var seoFieldCollection = new Mock<ISeoFieldCollection>();
            seoFieldCollection.Setup(it => it.GetAll()).Returns(seoField.AsEnumerableOfOne());
            seoFieldCollection.Setup(it => it.Get("test")).Returns(seoField);
            var seoValueService = new Mock<ISeoValueService>();
            var seoConverterCollection = new Mock<ISeoConverterCollection>();
            var logger = new Mock<ILogger>();

            var metaTagsProvider = new DefaultMetaTagsProvider(documentTypeSettingsService.Object, seoFieldCollection.Object, seoValueService.Object, seoConverterCollection.Object, logger.Object);

            metaTagsProvider.BeforeMetaTagsGet += (source, args) =>
            {
                args.SetValue("test", "ThisIsADifferentTestValue");
            };

            var metaTags = metaTagsProvider.Get(contentMock.Object);

            Assert.IsTrue(metaTags.GetValue<string>("test")?.Equals("ThisIsADifferentTestValue"));
        }
    }
}
