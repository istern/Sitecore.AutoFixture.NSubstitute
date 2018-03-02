using AutoFixture.Kernel;
using NSubstitute;
using Sitecore.Abstractions;

namespace Sitecore.AutoFixture.NSubstitute
{
    public class FactorySpecimentBuilder : ISpecimenBuilder
    {
        public object Create(object request, ISpecimenContext context)
        {
            if (!typeof(BaseFactory).Equals(request))
            {
                return new NoSpecimen();
            }

           
            var factory = Substitute.For<BaseFactory>();
            return factory;
        }
    }
}