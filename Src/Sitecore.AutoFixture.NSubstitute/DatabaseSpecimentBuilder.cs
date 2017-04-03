using NSubstitute;
using Ploeh.AutoFixture.Kernel;
using Sitecore.Data;

namespace SitecoreAutofixture.NSubstitute
{
    public class DatabaseSpecimentBuilder : ISpecimenBuilder
    {
        public object Create(object request, ISpecimenContext context)
        {
            if (!typeof(Database).Equals(request))
            {
                return new NoSpecimen(request);
            }

            var database = Substitute.For<Database>();
            return database;
        }
    }
}