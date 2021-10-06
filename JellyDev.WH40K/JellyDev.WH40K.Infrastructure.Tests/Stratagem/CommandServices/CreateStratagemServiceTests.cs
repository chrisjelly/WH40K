using JellyDev.WH40K.Domain.SharedKernel.ValueObjects;
using JellyDev.WH40K.Domain.Stratagem;
using JellyDev.WH40K.Infrastructure.SharedKernel;
using JellyDev.WH40K.Infrastructure.Stratagem.CommandServices;
using Moq;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;
using static JellyDev.WH40K.Infrastructure.Stratagem.Commands.V1;

namespace JellyDev.WH40K.Infrastructure.Tests
{
    public class CreateStratagemServiceTests
    {
        [Fact]
        public void CreateStratagemService_throws_exception_when_ID_exists()
        {
            CreateStratagem command = new CreateStratagem
            {
                Id = Guid.NewGuid()
            };
            var repositoryCreator = new Mock<IRepositoryCreator<StratagemAggregate, StratagemId>>();
            repositoryCreator.Setup(x => x.Exists(new StratagemId(command.Id)))
                .Returns(true);
            var unitOfWork = new Mock<IUnitOfWork>();
            var commandSvc = new CreateStratagemService(repositoryCreator.Object, unitOfWork.Object);

            Assert.ThrowsAsync<InvalidOperationException>(() => commandSvc.ExecuteAsync(command));
        }

        [Fact]
        public async Task CreateStratagemService_can_execute_successfully()
        {
            CreateStratagem command = new CreateStratagem
            {
                Id = Guid.NewGuid(),
                Phases = new List<PhaseEnum> { PhaseEnum.Charge },
                Name = "Test",
                Description = "This is a test stratagem."
            };
            var repositoryCreator = new Mock<IRepositoryCreator<StratagemAggregate, StratagemId>>();
            repositoryCreator.Setup(x => x.Exists(new StratagemId(command.Id)))
                .Returns(false);
            var unitOfWork = new Mock<IUnitOfWork>();
            var commandSvc = new CreateStratagemService(repositoryCreator.Object, unitOfWork.Object);
            await commandSvc.ExecuteAsync(command);

            repositoryCreator.Verify(x => x.AddAsync(It.IsAny<StratagemAggregate>()), Times.Once);
            unitOfWork.Verify(x => x.CommitAsync(), Times.Once);
        }
    }
}
