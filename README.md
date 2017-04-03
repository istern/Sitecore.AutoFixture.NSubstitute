#Sitecore AutoFixture With NSubstitute
This Library helps you writing unit test for Sitecore. It dependens on Sitecore 8.2+, from this version Sitecore introduced more abstractions which allowed for even easier unit test to written.

##Depencies
[NSubstitute](http://nsubstitute.github.io/)
[Autofixture](https://github.com/AutoFixture/AutoFixture)
[XUnit](https://xunit.github.io/)

##Example
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