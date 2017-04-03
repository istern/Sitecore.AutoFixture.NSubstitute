using NSubstitute;
using Ploeh.AutoFixture.Kernel;
using Sitecore.Abstractions;

namespace SitecoreAutofixture.NSubstitute
{
    public class FactorySpecimentBuilder : ISpecimenBuilder
    {
        public object Create(object request, ISpecimenContext context)
        {
            if (!typeof(IFactory).Equals(request))
            {
                return new NoSpecimen(request);
            }

           
            var factory = Substitute.For<IFactory>();
            return factory;
        }
    }
}