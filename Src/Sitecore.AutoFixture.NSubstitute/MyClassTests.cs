using FluentAssertions;
using NSubstitute;
using Sitecore.Abstractions;
using Sitecore.Data;
using Sitecore.Data.Items;
using Xunit;

namespace SitecoreAutofixture.NSubstitute
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
            MyClass sut = new MyClass(factory);

            //Act
            Item returnItem = sut.AddToSitecore();

            //Assert
            returnItem.Should().NotBeNull();
            returnItem.ID.Should().BeSameAs(childItem.ID);
            returnItem.Name.Should().BeEquivalentTo(childItem.Name);
            rootItem.Add(Arg.Any<string>(), Arg.Any<TemplateID>()).Received();
            database.GetItem("/sitecore/content/home").Received();

        }
    }
}
