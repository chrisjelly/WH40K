using JellyDev.WH40K.Domain.SharedKernel.ValueObjects;
using JellyDev.WH40K.Domain.Faction;
using JellyDev.WH40K.Domain.Faction.ParameterObjects;
using JellyDev.WH40K.Infrastructure.SharedKernel;
using JellyDev.WH40K.Infrastructure.Faction.Commands.V1;
using JellyDev.WH40K.Infrastructure.Faction.CommandServices;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace JellyDev.WH40K.Infrastructure.Tests.Faction
{
    public class DeleteFactionServiceTests
    {
        [Fact]
        public void DeleteFactionService_throws_exception_when_stratagem_does_not_exist()
        {
            // Arrange
            var command = new DeleteFaction
            {
                Id = Guid.NewGuid()
            };
            var repositoryDeleter = new Mock<IRepositoryDeleter<FactionAggregate, FactionId>>();
            repositoryDeleter.Setup(x => x.Load(new FactionId(command.Id)))
                .Returns((FactionAggregate)null);
            var unitOfWork = new Mock<IUnitOfWork>();
            var commandSvc = new DeleteFactionService(repositoryDeleter.Object, unitOfWork.Object);

            // Assert
            Assert.ThrowsAsync<InvalidOperationException>(() => commandSvc.ExecuteAsync(command));
        }

        [Fact]
        public async Task DeleteFactionService_can_execute_successfully()
        {
            // Arrange
            var name = Name.FromString("Test");
            var createFactionParams = new CreateFactionParams(new FactionId(Guid.NewGuid()), name);
            var faction = new FactionAggregate(createFactionParams);

            var repositoryDeleter = new Mock<IRepositoryDeleter<FactionAggregate, FactionId>>();
            repositoryDeleter.Setup(x => x.Load(new FactionId(createFactionParams.Id)))
                .Returns(faction);
            var unitOfWork = new Mock<IUnitOfWork>();

            var command = new DeleteFaction
            {
                Id = createFactionParams.Id
            };
            var commandSvc = new DeleteFactionService(repositoryDeleter.Object, unitOfWork.Object);

            // Act
            await commandSvc.ExecuteAsync(command);

            // Assert
            repositoryDeleter.Verify(x => x.Delete(It.IsAny<FactionAggregate>()), Times.Once);
            unitOfWork.Verify(x => x.CommitAsync(), Times.Once);
        }
    }
}
