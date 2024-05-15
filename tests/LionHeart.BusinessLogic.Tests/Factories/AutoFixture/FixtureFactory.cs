using AutoFixture;

namespace LionHeart.BusinessLogic.Tests.Factories.AutoFixture;

public static class FixtureFactory
{
    public static Fixture Create()
    {
        var fixture = new Fixture();
        return fixture;
    }
}