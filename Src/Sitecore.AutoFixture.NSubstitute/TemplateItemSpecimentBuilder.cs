using System;
using AutoFixture.Kernel;
using NSubstitute;

using Sitecore.Data;
using Sitecore.Data.Items;
using Sitecore.Globalization;

namespace Sitecore.AutoFixture.NSubstitute
{
    public class TemplateItemSpecimentBuilder : ISpecimenBuilder
    {
        public object Create(object request, ISpecimenContext context)
        {
            if (!typeof(TemplateItem).Equals(request))
            {
                return new NoSpecimen();
            }

            string generatedItemName = Guid.NewGuid().ToString("N");
            var itemId = ID.NewID;
            var language = Substitute.For<Language>();
            var definition = new ItemDefinition(itemId, generatedItemName, ID.NewID, ID.NewID);
            var data = new ItemData(definition, language, Sitecore.Data.Version.First, new FieldList());
            var database = Substitute.For<Database>();
            var innerItem = Substitute.For<Item>(itemId, data, database);
            innerItem.Name = generatedItemName;
            var templateItem = Substitute.For<TemplateItem>(innerItem);
            return templateItem;
        }
    }
}

