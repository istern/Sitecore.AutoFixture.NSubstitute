using AutoFixture.Kernel;
using NSubstitute;
using Sitecore.Data;

namespace Sitecore.AutoFixture.NSubstitute
{
    public class DatabaseSpecimentBuilder : ISpecimenBuilder
    {
        public object Create(object request, ISpecimenContext context)
        {
            if (!typeof(Database).Equals(request))
            {
                return new NoSpecimen();
            }

            var database = Substitute.For<Database>();
            return database;
        }
    }
}