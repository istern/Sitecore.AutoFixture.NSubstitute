using Sitecore.Abstractions;
using Sitecore.Data;
using Sitecore.Data.Items;

namespace SitecoreAutofixture.NSubstitute
{
    public class MyClass
    {
        private readonly IFactory _factory;

        public MyClass(IFactory factory)
        {
            _factory = factory;
        }
        public Item AddToSitecore()
        {
            Database database = _factory.GetDatabase("master");
            Item rootItem = database.GetItem("/sitecore/content/home");
            Item childItem = rootItem.Add("child", new TemplateID(ID.NewID));
            return childItem;
        }

        
    }
}
