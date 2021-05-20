using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;
using Newtonsoft.Json;
using NUnit.Framework;
using Umbraco.Core;
using Umbraco.Core.Mapping;
using Umbraco.Core.Models;
using Umbraco.Core.Services;
using uSeoToolkit.Umbraco8.Core.Interfaces;
using uSeoToolkit.Umbraco8.Core.Interfaces.SeoField;
using uSeoToolkit.Umbraco8.Core.Mappers;
using uSeoToolkit.Umbraco8.Core.Models.DocumentTypeSettings.Business;
using uSeoToolkit.Umbraco8.Core.Models.DocumentTypeSettings.Database;
using uSeoToolkit.Umbraco8.Core.Models.SeoField;
using uSeoToolkit.Umbraco8.Tests.Mocks;

namespace uSeoToolkit.Umbraco8.Tests.Mappers
{
    [TestFixture]
    public class DocumentTypeSettingsMapperTests
    {
        [Test]
        public void TestDtoToEntity()
        {
            var contentTypeMock = new Mock<IContentType>();
            contentTypeMock.Setup(it => it.Id).Returns(1);
            var dto = new DocumentTypeSettingsDto
            {
                EnableSeoSettings = true,
                Content = contentTypeMock.Object,
                Fields = new Dictionary<ISeoField, object>
                {
                    {new SeoFieldMock {Alias = "Test1"}, "TestValue"},
                    {new SeoFieldMock {Alias = "Test2"}, "Test2Value"}
                },
                Inheritance = null
            };
            var mapDefinition = new DocumentTypeSettingsMapper(new Mock<IContentTypeService>().Object, new Mock<ISeoFieldCollection>().Object);
            var mapper = new UmbracoMapper(new MapDefinitionCollection(mapDefinition.AsEnumerableOfOne()));

            var entity = mapper.Map<DocumentTypeSettingsEntity>(dto);

            Assert.AreEqual(true, entity.EnableSeoSettings);
            Assert.AreEqual(1, entity.NodeId);
            Assert.IsTrue(JsonConvert.DeserializeObject<Dictionary<string, string>>(entity.Fields).Count == 2);
            Assert.IsTrue(JsonConvert.DeserializeObject<Dictionary<string, string>>(entity.Fields).ContainsKey("Test1"));
            Assert.AreEqual(null, entity.InheritanceId);
        }
    }
}
