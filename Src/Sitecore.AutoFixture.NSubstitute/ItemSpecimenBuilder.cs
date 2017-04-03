using NSubstitute;
using Ploeh.AutoFixture.Kernel;
using Sitecore.Data;
using Sitecore.Data.Items;
using Sitecore.Globalization;

namespace Sitecore.AutoFixture.NSubstitute
{
    public class ItemSpecimentBuilder : ISpecimenBuilder
    {
        public static string DefaultItemName = "SitecoreItemName";
       
        public object Create(object request, ISpecimenContext context)
        {
            if (!typeof(Item).Equals(request))
            {
                return new NoSpecimen(request);
            }
            
            var itemId = ID.NewID;
            var language = Substitute.For<Language>();
            var definition = new ItemDefinition(itemId, DefaultItemName, ID.NewID, ID.NewID);
            var data = new ItemData(definition, language, Sitecore.Data.Version.First, new FieldList());
            var database = Substitute.For<Database>();
            var item = Substitute.For<Item>(itemId, data, database);
            item.Name = DefaultItemName;

            return item;
        }
    }
}

