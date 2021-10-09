using JellyDev.WH40K.Domain.Faction;
using JellyDev.WH40K.Infrastructure.Faction.Commands.V1;
using JellyDev.WH40K.Infrastructure.Faction.CommandServices;
using Moq;
using System;
using System.Threading.Tasks;
using Xunit;
using JellyDev.WH40K.Infrastructure.SharedKernel.Interfaces;
using JellyDev.WH40K.Infrastructure.Database.EfCore;

namespace JellyDev.WH40K.Infrastructure.Tests.Faction
{
    public class CreateFactionServiceTests
    {
        [Fact]
        public void FactionStratagemService_throws_exception_when_faction_exists()
        {
            // Arrange
            var command = new CreateFaction
            {
                Id = Guid.NewGuid()
            };
            var repositoryCreator = new Mock<IRepositoryCreator<FactionAggregate, FactionId>>();
            repositoryCreator.Setup(x => x.Exists(new FactionId(command.Id)))
                .Returns(true);
            var unitOfWork = new Mock<IUnitOfWork<FactionDbContext>>();
            var commandSvc = new CreateFactionService(repositoryCreator.Object, unitOfWork.Object);

            // Assert
            Assert.ThrowsAsync<InvalidOperationException>(() => commandSvc.ExecuteAsync(command));
        }

        [Fact]
        public async Task CreateFactionService_can_execute_successfully()
        {
            // Arrange
            var command = new CreateFaction
            {
                Id = Guid.NewGuid(),
                Name = "Test"
            };
            var repositoryCreator = new Mock<IRepositoryCreator<FactionAggregate, FactionId>>();
            repositoryCreator.Setup(x => x.Exists(new FactionId(command.Id)))
                .Returns(false);
            var unitOfWork = new Mock<IUnitOfWork<FactionDbContext>>();
            var commandSvc = new CreateFactionService(repositoryCreator.Object, unitOfWork.Object);

            // Act
            await commandSvc.ExecuteAsync(command);

            // Assert
            repositoryCreator.Verify(x => x.AddAsync(It.IsAny<FactionAggregate>()), Times.Once);
            unitOfWork.Verify(x => x.CommitAsync(), Times.Once);
        }
    }
}
