using JellyDev.WH40K.Domain.Faction;
using JellyDev.WH40K.Domain.SharedKernel.ValueObjects;
using JellyDev.WH40K.Domain.Stratagem;
using JellyDev.WH40K.Domain.Stratagem.ParameterObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace JellyDev.WH40K.Domain.Tests.Stratagem.ParameterObjects
{
    public class CreateStratagemParamsTests
    {
        [Fact]
        public void CreateStrategemParams_cannot_be_created_with_null_values()
        {
            // Arrange
            var id = new StratagemId(Guid.NewGuid());
            var factionId = new FactionId(Guid.NewGuid());
            var phases = new List<Phase> { Phase.FromEnum(PhaseEnum.Movement), Phase.FromEnum(PhaseEnum.Charge) };
            var name = Name.FromString("Test Stratagem");
            var description = Description.FromString("These are the rules for the stratagem.");
            var commandPoints = Amount.FromInt(2);

            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => new CreateStratagemParams(null, null, null, null, null, null));
            Assert.Throws<ArgumentNullException>(() => new CreateStratagemParams(id, null, null, null, null, null));
            Assert.Throws<ArgumentNullException>(() => new CreateStratagemParams(id, factionId, null, null, null, null));
            Assert.Throws<ArgumentNullException>(() => new CreateStratagemParams(id, factionId, phases, null, null, null));
            Assert.Throws<ArgumentNullException>(() => new CreateStratagemParams(id, factionId, phases, name, null, null));
            Assert.Throws<ArgumentNullException>(() => new CreateStratagemParams(id, factionId, phases, name, description, null));
        }

        [Fact]
        public void CreateStrategemParams_can_create_with_distinct_phases_only()
        {
            // Arrange
            var id = new StratagemId(Guid.NewGuid());
            var factionId = new FactionId(Guid.NewGuid());
            var phases = new List<Phase> { 
                Phase.FromEnum(PhaseEnum.Movement), 
                Phase.FromEnum(PhaseEnum.Charge),
                Phase.FromEnum(PhaseEnum.Movement)
            };
            var name = Name.FromString("Test Stratagem");
            var description = Description.FromString("These are the rules for the stratagem.");
            var commandPoints = Amount.FromInt(2);

            // Act
            var createStratagemParams = new CreateStratagemParams(id, factionId, phases, name, description, commandPoints);

            // Assert
            Assert.Equal(id, createStratagemParams.Id);
            Assert.Equal(2, createStratagemParams.Phases.Count);
            Assert.True(createStratagemParams.Phases.Where(x => x.Value == PhaseEnum.Movement).Count() > 0);
            Assert.True(createStratagemParams.Phases.Where(x => x.Value == PhaseEnum.Charge).Count() > 0);
            Assert.Equal(name, createStratagemParams.Name);
            Assert.Equal(description, createStratagemParams.Description);
            Assert.Equal(commandPoints, createStratagemParams.CommandPoints);
        }
    }
}
