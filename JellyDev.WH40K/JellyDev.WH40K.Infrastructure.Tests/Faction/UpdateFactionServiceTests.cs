using JellyDev.WH40K.Domain.SharedKernel.ValueObjects;
using JellyDev.WH40K.Domain.Faction;
using JellyDev.WH40K.Domain.Faction.ParameterObjects;
using JellyDev.WH40K.Infrastructure.SharedKernel.Interfaces;
using JellyDev.WH40K.Infrastructure.Faction.Commands.V1;
using JellyDev.WH40K.Infrastructure.Faction.CommandServices;
using Moq;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace JellyDev.WH40K.Infrastructure.Tests.Faction
{
    public class UpdateFactionServiceTests
    {
        [Fact]
        public void UpdateStratagemService_throws_exception_when_faction_does_not_exist()
        {
            // Arrange
            var command = new UpdateFaction
            {
                Id = Guid.NewGuid()
            };
            var repositoryUpdater = new Mock<IRepositoryUpdater<FactionAggregate, FactionId>>();
            repositoryUpdater.Setup(x => x.Load(new FactionId(command.Id)))
                .Returns((FactionAggregate)null);
            var unitOfWork = new Mock<IUnitOfWork>();
            var commandSvc = new UpdateFactionService(repositoryUpdater.Object, unitOfWork.Object);

            // Assert
            Assert.ThrowsAsync<InvalidOperationException>(() => commandSvc.ExecuteAsync(command));
        }

        [Fact]
        public async Task UpdateFactionService_can_execute_successfully()
        {
            // Arrange
            var name = Name.FromString("Test");
            var createFactionParams = new CreateFactionParams(new FactionId(Guid.NewGuid()), name);
            var faction = new FactionAggregate(createFactionParams);

            var repositoryUpdater = new Mock<IRepositoryUpdater<FactionAggregate, FactionId>>();
            repositoryUpdater.Setup(x => x.Load(createFactionParams.Id))
                .Returns(faction);
            var unitOfWork = new Mock<IUnitOfWork>();

            var command = new UpdateFaction
            {
                Id = createFactionParams.Id,
                Name = "Test Update"
            };
            var commandSvc = new UpdateFactionService(repositoryUpdater.Object, unitOfWork.Object);

            // Act
            await commandSvc.ExecuteAsync(command);

            // Assert
            repositoryUpdater.Verify(x => x.Update(It.IsAny<FactionAggregate>()), Times.Once);
            unitOfWork.Verify(x => x.CommitAsync(), Times.Once);
        }
    }
}
