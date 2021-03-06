using FluentAssertions;
using NSubstitute;
using Sitecore.Abstractions;
using Sitecore.Collections;
using Sitecore.Data;
using Sitecore.Data.Fields;
using Sitecore.Data.Items;
using Sitecore.Globalization;
using Xunit;

namespace Sitecore.AutoFixture.NSubstitute.Tests
{
    public class MyClassTests
    {
        [Theory, AutoSitecoreData]
        public void MyClass_AddItemToWithAutoFixtureRoot_ShouldReturnItem(Item rootItem, Item childItem, Database database, BaseFactory factory)
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
        public void MyClass_AddItemToWithTempalteWithAutoFixtureRoot_ShouldReturnItem(Item rootItem, Item childItem, TemplateItem template, Database database, BaseFactory factory)
        {


            //Arrange
            Database mydb = rootItem.Database;
            mydb.Name.Returns("bla");
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
            rootItem["title"].Should().BeEquivalentTo(("parent"));
            returnItem.ID.Should().BeSameAs(childItem.ID);
            returnItem.Name.Should().BeEquivalentTo(childItem.Name);
            rootItem.Add(childItem.Name, template).Received();
            returnItem.Received()["title"] = "parent_child";
            database.Received().GetTemplate("baseTemplate");
            database.Received().GetItem("/sitecore/content/home");


        }


        [Theory, AutoSitecoreData]
        public void MyClass_AddItemInMethod_ShouldGiveCorrectValues([ItemData(itemId:"{bc3f06c9-cac5-433c-ab31-4fa1a149754b}",
                                                                     templateId:"{65ba5663-93af-427f-b579-5f361d6f5c93}",
                                                                     name:"Home")] Item item)
        {

            //Assert
            item.ID.Should().BeEquivalentTo(ID.Parse("{bc3f06c9-cac5-433c-ab31-4fa1a149754b}"));
            item.Name.Should().BeEquivalentTo(("Home"));
            item.TemplateID.Should().BeEquivalentTo((ID.Parse("{65ba5663-93af-427f-b579-5f361d6f5c93}")));

        }



        [Theory, AutoSitecoreData]
        public void ShowInNavigationField(Item sourceItem, FieldCollection fields, Field checkboxField, string fieldname)
        {
            //arrange - Build expectations
            checkboxField.Value.ReturnsForAnyArgs("1");
            fields[fieldname].ReturnsForAnyArgs(checkboxField);
            sourceItem.Fields.ReturnsForAnyArgs(fields);

            //act
            bool isShown = sourceItem.Fields[fieldname].Value == "1";

            //Assert are expections fulfullid
            isShown.Should().BeTrue();
        }

    }
}
