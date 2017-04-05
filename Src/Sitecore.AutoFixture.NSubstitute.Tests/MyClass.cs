using Sitecore.Abstractions;
using Sitecore.Data;
using Sitecore.Data.Items;

namespace Sitecore.AutoFixture.NSubstitute.Tests
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
            childItem["title"] = rootItem["title"]+"_child";
            return childItem;
        }


        public Item AddToSitecoreFromTemplate()
        {
            Database database = _factory.GetDatabase("master");
            Item rootItem = database.GetItem("/sitecore/content/home");
            TemplateItem template = database.GetTemplate("baseTemplate");
            Item childItem = rootItem.Add("child", template);
            childItem["title"] = rootItem["title"] + "_child";
            return childItem;
        }


    }
}
