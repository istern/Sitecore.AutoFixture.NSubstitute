﻿using AutoFixture;
using AutoFixture.Kernel;


namespace Sitecore.AutoFixture.NSubstitute
{
    public class AutoSitecoreCustomization 
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
            fixture.Customizations.Add(new CompositeSpecimenBuilder(new ItemDataSpecimentBuilder()));
        }

    }
}
