using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace ReviewProj.WebUI.HtmlHelpers
{
    public class LocalizedDisplayNameAttribute : DisplayNameAttribute
    {
        public LocalizedDisplayNameAttribute(string resourceKey)
        {
            ResourceKey = resourceKey;
        }

        public override string DisplayName
        {
            get
            {
                string displayName = Resources.Resource.ResourceManager.GetString(ResourceKey);
                return string.IsNullOrEmpty(displayName) ? string.Format("[[{0}]]", ResourceKey) : displayName;
            }
        }

        private string ResourceKey { get; set; }
    }
}