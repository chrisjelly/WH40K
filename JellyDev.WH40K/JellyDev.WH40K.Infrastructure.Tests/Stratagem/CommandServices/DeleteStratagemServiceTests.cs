using JellyDev.WH40K.Domain.SharedKernel.ValueObjects;
using JellyDev.WH40K.Domain.Stratagem;
using JellyDev.WH40K.Domain.Stratagem.ParameterObjects;
using JellyDev.WH40K.Infrastructure.SharedKernel;
using JellyDev.WH40K.Infrastructure.Stratagem.Commands.V1;
using JellyDev.WH40K.Infrastructure.Stratagem.CommandServices;
using Moq;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace JellyDev.WH40K.Infrastructure.Tests.Stratagem.CommandServices
{
    public class DeleteStratagemServiceTests
    {
        [Fact]
        public void DeleteStratagemService_throws_exception_when_stratagem_does_not_exist()
        {
            // Arrange
            var command = new DeleteStratagem
            {
                Id = Guid.NewGuid()
            };
            var repositoryDeleter = new Mock<IRepositoryDeleter<StratagemAggregate, StratagemId>>();
            repositoryDeleter.Setup(x => x.Load(new StratagemId(command.Id)))
                .Returns((StratagemAggregate)null);
            var unitOfWork = new Mock<IUnitOfWork>();
            var commandSvc = new DeleteStratagemService(repositoryDeleter.Object, unitOfWork.Object);

            // Assert
            Assert.ThrowsAsync<InvalidOperationException>(() => commandSvc.ExecuteAsync(command));
        }

        [Fact]
        public async Task DeleteStratagemService_can_execute_successfully()
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

            var repositoryDeleter = new Mock<IRepositoryDeleter<StratagemAggregate, StratagemId>>();
            repositoryDeleter.Setup(x => x.Load(new StratagemId(createStratagemParams.Id)))
                .Returns(stratagem);
            var unitOfWork = new Mock<IUnitOfWork>();

            var command = new DeleteStratagem
            {
                Id = createStratagemParams.Id
            };
            var commandSvc = new DeleteStratagemService(repositoryDeleter.Object, unitOfWork.Object);

            // Act
            await commandSvc.ExecuteAsync(command);

            // Assert
            repositoryDeleter.Verify(x => x.Delete(It.IsAny<StratagemAggregate>()), Times.Once);
            unitOfWork.Verify(x => x.CommitAsync(), Times.Once);
        }
    }
}
