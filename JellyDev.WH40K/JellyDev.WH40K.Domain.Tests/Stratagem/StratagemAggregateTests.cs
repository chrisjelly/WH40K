using JellyDev.WH40K.Domain.Faction;
using JellyDev.WH40K.Domain.SharedKernel.Interfaces;
using JellyDev.WH40K.Domain.SharedKernel.ValueObjects;
using JellyDev.WH40K.Domain.Stratagem;
using JellyDev.WH40K.Domain.Stratagem.ParameterObjects;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace JellyDev.WH40K.Domain.Tests.Stratagem
{
    public class StratagemAggregateTests
    {
        [Fact]
        public void Strategem_cannot_be_created_with_null_values()
        {
            // Arrange
            var id = new StratagemId(Guid.NewGuid());
            var factionId = new FactionId(Guid.NewGuid());
            var phases = new List<Phase> { Phase.FromEnum(PhaseEnum.Movement), Phase.FromEnum(PhaseEnum.Charge) };            
            var name = Name.FromString("Test Stratagem");
            var description = Description.FromString("These are the rules for the stratagem.");
            var commandPoints = Amount.FromInt(2);
            var createStratagemParams = new CreateStratagemParams(id, factionId, phases, name, description, commandPoints);

            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => new StratagemAggregate(null, null));
            Assert.Throws<ArgumentNullException>(() => new StratagemAggregate(createStratagemParams, null));
        }

        [Fact]
        public void Stratagem_cannot_be_created_with_non_existent_faction()
        {
            // Arrange
            var id = new StratagemId(Guid.NewGuid());
            var factionId = new FactionId(Guid.NewGuid());
            var phases = new List<Phase> { Phase.FromEnum(PhaseEnum.Movement), Phase.FromEnum(PhaseEnum.Charge) };
            var name = Name.FromString("Test Stratagem");
            var description = Description.FromString("These are the rules for the stratagem.");
            var commandPoints = Amount.FromInt(2);
            var createStratagemParams = new CreateStratagemParams(id, factionId, phases, name, description, commandPoints);

            var repositoryChecker = new Mock<IRepositoryChecker<FactionId>>();
            repositoryChecker.Setup(x => x.Exists(new FactionId(createStratagemParams.FactionId)))
                .Returns(false);

            // Act & Assert
            Assert.Throws<Exception>(() => new StratagemAggregate(createStratagemParams, repositoryChecker.Object));
        }

        [Fact]
        public void Stratagem_can_be_created()
        {
            // Arrange
            var id = new StratagemId(Guid.NewGuid());
            var factionId = new FactionId(Guid.NewGuid());
            var phases = new List<Phase> { Phase.FromEnum(PhaseEnum.Movement), Phase.FromEnum(PhaseEnum.Charge) };
            var name = Name.FromString("Test Stratagem");
            var description = Description.FromString("These are the rules for the stratagem.");
            var commandPoints = Amount.FromInt(2);
            var createStratagemParams = new CreateStratagemParams(id, factionId, phases, name, description, commandPoints);

            var factionChecker = new Mock<IRepositoryChecker<FactionId>>();
            factionChecker.Setup(x => x.Exists(new FactionId(createStratagemParams.FactionId)))
                .Returns(true);

            // Act
            var strategem = new StratagemAggregate(createStratagemParams, factionChecker.Object);          

            // Assert
            Assert.Equal(id, strategem.Id);
            Assert.True(strategem.Phases.Where(x => x.Value == PhaseEnum.Movement).Count() > 0);
            Assert.True(strategem.Phases.Where(x => x.Value == PhaseEnum.Charge).Count() > 0);
            Assert.Equal(name, strategem.Name);
            Assert.Equal(description, strategem.Description);
            Assert.Equal(commandPoints, strategem.CommandPoints);
        }

        [Fact]
        public void Stratagem_can_be_created_with_no_faction()
        {
            // Arrange
            var id = new StratagemId(Guid.NewGuid());
            var factionId = new FactionId(Guid.Empty);
            var phases = new List<Phase> { Phase.FromEnum(PhaseEnum.Movement), Phase.FromEnum(PhaseEnum.Charge) };
            var name = Name.FromString("Test Stratagem");
            var description = Description.FromString("These are the rules for the stratagem.");
            var commandPoints = Amount.FromInt(2);
            var createStratagemParams = new CreateStratagemParams(id, factionId, phases, name, description, commandPoints);

            var factionChecker = new Mock<IRepositoryChecker<FactionId>>();
            factionChecker.Setup(x => x.Exists(new FactionId(createStratagemParams.FactionId)))
                .Returns(true);

            // Act
            var strategem = new StratagemAggregate(createStratagemParams, factionChecker.Object);

            // Assert
            Assert.Equal(id, strategem.Id);
            Assert.True(strategem.Phases.Where(x => x.Value == PhaseEnum.Movement).Count() > 0);
            Assert.True(strategem.Phases.Where(x => x.Value == PhaseEnum.Charge).Count() > 0);
            Assert.Equal(name, strategem.Name);
            Assert.Equal(description, strategem.Description);
            Assert.Equal(commandPoints, strategem.CommandPoints);
        }

        [Fact]
        public void Strategem_cannot_be_updated_with_null_value()
        {
            // Arrange
            var id = new StratagemId(Guid.NewGuid());
            var factionId = new FactionId(Guid.NewGuid());
            var phases = new List<Phase> { Phase.FromEnum(PhaseEnum.Movement), Phase.FromEnum(PhaseEnum.Charge) };
            var name = Name.FromString("Test Stratagem");
            var description = Description.FromString("These are the rules for the stratagem.");
            var commandPoints = Amount.FromInt(2);
            var createStratagemParams = new CreateStratagemParams(id, factionId, phases, name, description, commandPoints);

            var factionChecker = new Mock<IRepositoryChecker<FactionId>>();
            factionChecker.Setup(x => x.Exists(new FactionId(createStratagemParams.FactionId)))
                .Returns(true);

            StratagemAggregate stratagem = new StratagemAggregate(createStratagemParams, factionChecker.Object);

            var updateStratagemParams = new UpdateStratagemParams(factionId, phases, name, description, commandPoints);

            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => stratagem.Update(null, null));
            Assert.Throws<ArgumentNullException>(() => stratagem.Update(updateStratagemParams, null));
        }

        [Fact]
        public void Stratagem_cannot_be_updated_with_non_existent_faction()
        {
            // Arrange
            var id = new StratagemId(Guid.NewGuid());
            var factionId = new FactionId(Guid.NewGuid());
            var phases = new List<Phase> { Phase.FromEnum(PhaseEnum.Movement), Phase.FromEnum(PhaseEnum.Charge) };
            var name = Name.FromString("Test Stratagem");
            var description = Description.FromString("These are the rules for the stratagem.");
            var commandPoints = Amount.FromInt(2);
            var createStratagemParams = new CreateStratagemParams(id, factionId, phases, name, description, commandPoints);

            var newFactionId = new FactionId(Guid.NewGuid());
            var updateStratagemParams = new UpdateStratagemParams(newFactionId, phases, name, description, commandPoints);

            var factionChecker = new Mock<IRepositoryChecker<FactionId>>();
            factionChecker.Setup(x => x.Exists(new FactionId(createStratagemParams.FactionId)))
                .Returns(true);
            factionChecker.Setup(x => x.Exists(new FactionId(updateStratagemParams.FactionId)))
                .Returns(false);

            StratagemAggregate stratagem = new StratagemAggregate(createStratagemParams, factionChecker.Object);

            // Act & Assert
            Assert.Throws<Exception>(() => stratagem.Update(updateStratagemParams, factionChecker.Object));
        }

        [Fact]
        public void Stratagem_can_be_updated()
        {
            // Arrange
            var id = new StratagemId(Guid.NewGuid());
            var factionId = new FactionId(Guid.NewGuid());
            var phases = new List<Phase> { Phase.FromEnum(PhaseEnum.Movement), Phase.FromEnum(PhaseEnum.Charge) };
            var name = Name.FromString("Test Stratagem");
            var description = Description.FromString("These are the rules for the stratagem.");
            var commandPoints = Amount.FromInt(2);
            var createStratagemParams = new CreateStratagemParams(id, factionId, phases, name, description, commandPoints);

            var newFactionId = new FactionId(Guid.NewGuid());
            var newPhases = new List<Phase> { Phase.FromEnum(PhaseEnum.Shooting) };
            var newName = Name.FromString("Updated Stratagem");
            var newDescription = Description.FromString("This is an updated stratagem.");
            var newCommandPoints = Amount.FromInt(5);
            var updateStratagemParams = new UpdateStratagemParams(newFactionId, newPhases, newName, newDescription, newCommandPoints);

            var factionChecker = new Mock<IRepositoryChecker<FactionId>>();
            factionChecker.Setup(x => x.Exists(new FactionId(createStratagemParams.FactionId)))
                .Returns(true);
            factionChecker.Setup(x => x.Exists(new FactionId(updateStratagemParams.FactionId)))
                .Returns(true);

            StratagemAggregate stratagem = new StratagemAggregate(createStratagemParams, factionChecker.Object);

            // Act
            stratagem.Update(updateStratagemParams, factionChecker.Object);

            // Assert
            Assert.True(stratagem.Phases.Where(x => x.Value == PhaseEnum.Shooting).Count() > 0);
            Assert.Equal(newFactionId, stratagem.FactionId);
            Assert.Equal(newName, stratagem.Name);
            Assert.Equal(newDescription, stratagem.Description);
            Assert.Equal(newCommandPoints, stratagem.CommandPoints);
        }
    }
}
