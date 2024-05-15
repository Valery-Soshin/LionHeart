using LionHeart.Core.Interfaces.Repositories;
using Moq;

namespace LionHeart.BusinessLogic.Tests.Factories;

public static class UnitOfWorkFactory
{
    public static IUnitOfWork Create()
    {
        return CreateMock().Object;
    }
    public static Mock<IUnitOfWork> CreateMock()
    {
        var mock = new Mock<IUnitOfWork>();

        mock.Setup(m => m.BeginTransaction())
            .Verifiable();

        mock.Setup(m => m.Rollback())
            .Verifiable();

        mock.Setup(m => m.Commit())
            .Verifiable();

        return mock;
    }
}