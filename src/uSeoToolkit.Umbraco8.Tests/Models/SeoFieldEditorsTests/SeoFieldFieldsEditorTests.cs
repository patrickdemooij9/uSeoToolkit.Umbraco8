using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NUnit.Framework;
using uSeoToolkit.Umbraco8.Core.Models.SeoFieldEditors;
using Assert = NUnit.Framework.Assert;

namespace uSeoToolkit.Umbraco8.Tests.Models.SeoFieldEditorsTests
{
    [TestFixture]
    public class SeoFieldFieldsEditorTests
    {
        [Test]
        [TestCase("Test1,Test2", "Test3,Test4", "Test3,Test4,Test1,Test2")]
        [TestCase("Test1,Test2", null, "Test1,Test2")]
        [TestCase(null, "Test3,Test4", "Test3,Test4")]
        public void TestInheritValue(string value, string inheritedValue, string result)
        {
            var editor = new SeoFieldFieldsEditor(Array.Empty<string>());

            var returnedValue = editor.Inherit(value, inheritedValue);

            Assert.AreEqual(result, returnedValue);
        }
    }
}
