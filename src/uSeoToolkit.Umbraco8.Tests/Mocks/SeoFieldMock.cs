﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using uSeoToolkit.Umbraco8.Core.Interfaces.SeoField;

namespace uSeoToolkit.Umbraco8.Tests.Mocks
{
    public class SeoFieldMock : ISeoField
    {
        public string Title { get; set; }
        public string Alias { get; set; }
        public string Description { get; set; }
        public string View { get; set; }
        public Dictionary<string, object> Config { get; set; }
        public ISeoFieldEditor Editor { get; set; }
        public HtmlString Render(string value)
        {
            throw new NotImplementedException();
        }
    }
}