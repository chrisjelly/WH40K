using JellyDev.WH40K.Domain.SharedKernel.ValueObjects;
using JellyDev.WH40K.Domain.Stratagem;
using JellyDev.WH40K.Domain.Stratagem.ParameterObjects;
using JellyDev.WH40K.Infrastructure.SharedKernel.Interfaces;
using JellyDev.WH40K.Infrastructure.Stratagem.Commands.V1;
using JellyDev.WH40K.Infrastructure.Stratagem.CommandServices;
using Moq;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace JellyDev.WH40K.Infrastructure.Tests.Stratagem.CommandServices
{
    public class UpdateStratagemServiceTests
    {
        [Fact]
        public void UpdateStratagemService_throws_exception_when_stratagem_does_not_exist()
        {
            // Arrange
            var command = new UpdateStratagem
            {
                Id = Guid.NewGuid()
            };
            var repositoryUpdater = new Mock<IRepositoryUpdater<StratagemAggregate, StratagemId>>();
            repositoryUpdater.Setup(x => x.Load(new StratagemId(command.Id)))
                .Returns((StratagemAggregate)null);
            var unitOfWork = new Mock<IUnitOfWork>();
            var commandSvc = new UpdateStratagemService(repositoryUpdater.Object, unitOfWork.Object);

            // Assert
            Assert.ThrowsAsync<InvalidOperationException>(() => commandSvc.ExecuteAsync(command));
        }

        [Fact]
        public async Task UpdateStratagemService_can_execute_successfully()
        {
            // Arrange
            var phases = new List<Phase> { Phase.FromEnum(PhaseEnum.Shooting) };
            var name = Name.FromString("Test");
            var description = Description.FromString("This is a test stratagem.");
            var commandPoints = Amount.FromInt(2);
            var createStratagemParams = new CreateStratagemParams(new StratagemId(Guid.NewGuid()),
                       phases,
                       name,
                       description,
                       commandPoints);
            var stratagem = new StratagemAggregate(createStratagemParams);

            var repositoryUpdater = new Mock<IRepositoryUpdater<StratagemAggregate, StratagemId>>();
            repositoryUpdater.Setup(x => x.Load(createStratagemParams.Id))
                .Returns(stratagem);
            var unitOfWork = new Mock<IUnitOfWork>();

            var command = new UpdateStratagem
            {
                Id = createStratagemParams.Id,
                Phases = new List<PhaseEnum> { PhaseEnum.Command, PhaseEnum.Movement },
                Name = "Test Update",
                Description = "This is an update."
            };
            var commandSvc = new UpdateStratagemService(repositoryUpdater.Object, unitOfWork.Object);

            // Act
            await commandSvc.ExecuteAsync(command);

            // Assert
            repositoryUpdater.Verify(x => x.Update(It.IsAny<StratagemAggregate>()), Times.Once);
            unitOfWork.Verify(x => x.CommitAsync(), Times.Once);
        }
    }
}
