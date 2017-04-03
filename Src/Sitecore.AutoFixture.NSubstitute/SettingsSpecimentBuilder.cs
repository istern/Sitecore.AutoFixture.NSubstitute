using NSubstitute;
using Ploeh.AutoFixture.Kernel;
using Sitecore.Abstractions;

namespace Sitecore.AutoFixture.NSubstitute
{
    public class SettingsSpecimentBuilder : ISpecimenBuilder
    {
        public object Create(object request, ISpecimenContext context)
        {
            if (!typeof(ISettings).Equals(request))
            {
                return new NoSpecimen();
            }


            var factory = Substitute.For<ISettings>();
            return factory;
        }
    }
}