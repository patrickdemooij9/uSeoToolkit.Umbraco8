using System;
using NUnit.Framework;
using uSeoToolkit.Umbraco8.Core.Models.SeoFieldEditors;

namespace uSeoToolkit.Umbraco8.Tests.Models.SeoFieldEditorsTests
{
    [TestFixture]
    public class SeoFieldPropertyEditorTests
    {
        [Test]
        [TestCase("Test1", "Test2", "Test1")]
        [TestCase("Test1", null, "Test1")]
        [TestCase(null, "Test2", "Test2")]
        public void TestInheritValue(string value, string inheritedValue, string result)
        {
            var editor = new SeoFieldPropertyEditor();

            var returnedValue = editor.Inherit(value, inheritedValue);

            Assert.AreEqual(result, returnedValue);
        }
    }
}
