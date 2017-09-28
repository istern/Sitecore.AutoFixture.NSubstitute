using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sitecore.Data;

namespace Sitecore.AutoFixture.NSubstitute
{
    public class ItemDataAttribute : Attribute
    {
        public ID TemplateId { get; set; }

        public ID ItemId { get; set; }

        public string Name { get; set; }
        public ItemDataAttribute(string name = null, string itemId = null, string templateId = null)
        {
            Name = name;
            ItemId = itemId != null ? ID.Parse(itemId) : ID.NewID;
            TemplateId = templateId != null ? ID.Parse(templateId) : ID.NewID;

        }

      
    }
}
