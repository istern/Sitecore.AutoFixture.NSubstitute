using Ploeh.AutoFixture;
using Ploeh.AutoFixture.Xunit2;

namespace Sitecore.AutoFixture.NSubstitute
{
    public class AutoSitecoreDataAttribute : AutoDataAttribute
    {
        public AutoSitecoreDataAttribute() : base(new Fixture().Customize(new AutoSitecoreCustomization()))
        {
        }
    }

    public class InlineAutoSitecoreDataAttribute : InlineAutoDataAttribute
    {
        public InlineAutoSitecoreDataAttribute(params object[] values): base(new AutoDataAttribute(), values)
        {
        }
    }
}
