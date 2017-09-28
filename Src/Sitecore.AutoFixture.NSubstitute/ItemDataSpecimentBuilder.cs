using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using NSubstitute;
using Ploeh.AutoFixture.Kernel;
using Sitecore.Data;
using Sitecore.Data.Items;
using Sitecore.Globalization;

namespace Sitecore.AutoFixture.NSubstitute
{
    public class ItemDataSpecimentBuilder : ISpecimenBuilder
    {
        public object Create(object request, ISpecimenContext context)
        {
            ParameterInfo info = request as ParameterInfo;
            if(info == null)
            {
                return new NoSpecimen();
            }

            var itemData = (ItemDataAttribute)info.GetCustomAttributes(typeof(ItemDataAttribute)).FirstOrDefault();
            if (itemData == null)
            {
                return new NoSpecimen();
            }

            var itemId = itemData.ItemId;
            var language = Substitute.For<Language>();
            var definition = new ItemDefinition(itemId, itemData.Name, itemData.TemplateId, itemData.TemplateId);
            var data = new ItemData(definition, language, Sitecore.Data.Version.First, new FieldList());
            var database = Substitute.For<Database>();
            var item = Substitute.For<Item>(itemId, data, database);
            item.Name = itemData.Name;
            item.TemplateID.Returns(itemData.TemplateId);

            return item;
        }
    }
}
