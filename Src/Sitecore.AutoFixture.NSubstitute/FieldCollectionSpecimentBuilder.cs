using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoFixture.Kernel;
using NSubstitute;
using Sitecore.Abstractions;
using Sitecore.Collections;
using Sitecore.Data;
using Sitecore.Data.Fields;
using Sitecore.Data.Items;
using Sitecore.Globalization;

namespace Sitecore.AutoFixture.NSubstitute
{
    public class FieldCollectionSpecimentBuilder : ISpecimenBuilder
    {
        public object Create(object request, ISpecimenContext context)
        {
            if (!typeof(FieldCollection).Equals(request))
            {
                return new NoSpecimen();
            }
            
            var owner = CreateOwnerItem();
            var fieldCollection = Substitute.For<FieldCollection>(owner);
            return fieldCollection;
        }

        private Item CreateOwnerItem()
        {
            string generatedItemName = Guid.NewGuid().ToString("N");
            var itemId = ID.NewID;
            var language = Substitute.For<Language>();
            var definition = new ItemDefinition(itemId, generatedItemName, ID.NewID, ID.NewID);
            var data = new ItemData(definition, language, Data.Version.First, new FieldList());
            var database = Substitute.For<Database>();
            var item = Substitute.For<Item>(itemId, data, database);
            item.Name = generatedItemName;
            return item;
        }
    }
}
