using FluentAssertions;
using NSubstitute;
using Sitecore.Abstractions;
using Sitecore.Collections;
using Sitecore.Data;
using Sitecore.Data.Fields;
using Sitecore.Data.Items;

using Xunit;

namespace Sitecore.AutoFixture.NSubstitute.Tests
{
    public class MyClassTests
    {
        [Theory, AutoSitecoreData]
        public void MyClass_AddItemToWithAutoFixtureRoot_ShouldReturnItem(Item rootItem,Item childItem,Database database,IFactory factory)
        {
            //Arrange
            database.GetItem("/sitecore/content/home").Returns(rootItem);
            factory.GetDatabase("master").Returns(database);
            rootItem.Add(Arg.Any<string>(), Arg.Any<TemplateID>()).ReturnsForAnyArgs(childItem);
            rootItem["title"].Returns("parent");
            MyClass sut = new MyClass(factory);

            //Act
            Item returnItem = sut.AddToSitecore();
            returnItem["test"] = "T";
            //Assert
            returnItem.Should().NotBeNull();
            returnItem.ID.Should().BeSameAs(childItem.ID);
            returnItem.Name.Should().BeEquivalentTo(childItem.Name);
            rootItem.Add(Arg.Any<string>(), Arg.Any<TemplateID>()).Received();
            returnItem.Received()["title"] = "parent_child";
            database.GetItem("/sitecore/content/home").Received();
           
        }

        [Theory, AutoSitecoreData]
        public void MyClass_AddItemToWithTempalteWithAutoFixtureRoot_ShouldReturnItem(Item rootItem, Item childItem,TemplateItem template, Database database, IFactory factory)
        {
            //Arrange
            database.GetItem("/sitecore/content/home").Returns(rootItem);
            database.GetTemplate("baseTemplate").Returns(template);
            factory.GetDatabase("master").Returns(database);
            rootItem.Add(childItem.Name, template).ReturnsForAnyArgs(childItem);
            rootItem["title"].Returns("parent");
            MyClass sut = new MyClass(factory);

            //Act
            Item returnItem = sut.AddToSitecoreFromTemplate();
            returnItem["test"] = "T";
            //Assert
            returnItem.Should().NotBeNull();
            returnItem.ID.Should().BeSameAs(childItem.ID);
            returnItem.Name.Should().BeEquivalentTo(childItem.Name);
            rootItem.Add(childItem.Name,template).Received();
            returnItem.Received()["title"] = "parent_child";
            database.Received().GetTemplate("baseTemplate");
            database.Received().GetItem("/sitecore/content/home");
       

        }
    }
}
