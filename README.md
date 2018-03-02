# Sitecore AutoFixture With NSubstitute
This Library helps you writing unit test for Sitecore. It dependens on Sitecore 8.2+, from this version Sitecore introduced more abstractions which allowed for even easier unit test to written.

## Dependencies
[NSubstitute](http://nsubstitute.github.io/)

[Autofixture](https://github.com/AutoFixture/AutoFixture)

[XUnit](https://xunit.github.io/)

## Howto
In your test project you must refrence Sitecore.Kernel version 8.2+ and use version 1.x of this modules. From Sitecore 9 an forward you should use version 2.x of this module  NOT THE NO REFERENCE version you will need the full version. Likewise with a reference to Sitecore.Logging this however can be the no reference. Simply get them from the offcial Sitecore nuget feed.
With that reference solved simply add this nuget package

```sh
Install-Package Sitecore.AutoFixture.NSubstitute
```

## Example
Given the following class which adds an Item to root Item in Sitecore
```csharp
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
```

The Follwing Test can be written
```csharp
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
```
Defining itemdata as parameters ,ie. so you easily can create items with specific itemname, itemid or templateid                    
```csharp
 public class MyClass
    {
         [Theory, AutoSitecoreData]
        public void MyClass_AddItemInMethod_ShouldGiveCorrectValues([ItemData(itemId:"{bc3f06c9-cac5-433c-ab31-4fa1a149754b}",
                                                                     templateId:"{65ba5663-93af-427f-b579-5f361d6f5c93}",
                                                                     name:"Home")] Item item)
        {

            //Assert
            item.ID.ShouldBeEquivalentTo(ID.Parse("{bc3f06c9-cac5-433c-ab31-4fa1a149754b}"));
            item.Name.ShouldBeEquivalentTo("Home");
            item.TemplateID.ShouldBeEquivalentTo(ID.Parse("{65ba5663-93af-427f-b579-5f361d6f5c93}"));

        }
    }
```