using System;
using AutoFixture;
using AutoFixture.Kernel;
using AutoFixture.Xunit2;

namespace Sitecore.AutoFixture.NSubstitute
{
    public class AutoSitecoreDataAttribute : AutoDataAttribute
    {
        private static Func<IFixture> fixtureFactory = () =>
        {
          var fixture = new Fixture();
          fixture.Customizations.Add(new CompositeSpecimenBuilder(new DatabaseSpecimentBuilder()));
          fixture.Customizations.Add(new CompositeSpecimenBuilder(new ItemSpecimentBuilder()));
          fixture.Customizations.Add(new CompositeSpecimenBuilder(new FactorySpecimentBuilder()));
          fixture.Customizations.Add(new CompositeSpecimenBuilder(new SettingsSpecimentBuilder()));
          fixture.Customizations.Add(new CompositeSpecimenBuilder(new ItemDataSpecimentBuilder()));
          return fixture;
        };


        public AutoSitecoreDataAttribute() : base(fixtureFactory)
        {

        }
    }

    public class InlineAutoSitecoreDataAttribute : InlineAutoDataAttribute
    {
        public InlineAutoSitecoreDataAttribute(params object[] values) : base(new AutoDataAttribute(), values)
        {
        }
    }
}
