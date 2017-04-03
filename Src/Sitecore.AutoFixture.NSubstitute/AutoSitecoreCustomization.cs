using Ploeh.AutoFixture;
using Ploeh.AutoFixture.Kernel;

namespace SitecoreAutofixture.NSubstitute
{
    public class AutoSitecoreCustomization : ICustomization
    {
        /// <summary>
        /// Customizes the specified fixture by adding the Sitecore specific specimen builders.
        /// </summary>
        /// <param name="fixture">The fixture to customize.</param>
        public void Customize(IFixture fixture)
        {
            fixture.Customizations.Add(new CompositeSpecimenBuilder(new DatabaseSpecimentBuilder()));
            fixture.Customizations.Add(new CompositeSpecimenBuilder(new ItemSpecimentBuilder()));
            fixture.Customizations.Add(new CompositeSpecimenBuilder(new FactorySpecimentBuilder()));
            fixture.Customizations.Add(new CompositeSpecimenBuilder(new SettingsSpecimentBuilder()));
        }

    }
}
