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
    public class UpdateStratagemParamsTests
    {
        [Fact]
        public void UpdateStrategemParams_cannot_be_created_with_null_values()
        {
            // Arrange
            var factionId = new FactionId(Guid.NewGuid());
            var phases = new List<Phase> { Phase.FromEnum(PhaseEnum.Movement), Phase.FromEnum(PhaseEnum.Shooting) };
            var name = Name.FromString("Test Stratagem");
            var description = Description.FromString("These are the rules for the stratagem.");
            var commandPoints = Amount.FromInt(2);

            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => new UpdateStratagemParams(null, null, null, null, null));
            Assert.Throws<ArgumentNullException>(() => new UpdateStratagemParams(factionId, null, null, null, null));
            Assert.Throws<ArgumentNullException>(() => new UpdateStratagemParams(factionId, phases, null, null, null));
            Assert.Throws<ArgumentNullException>(() => new UpdateStratagemParams(factionId, phases, name, null, null));
            Assert.Throws<ArgumentNullException>(() => new UpdateStratagemParams(factionId, phases, name, description, null));
        }

        [Fact]
        public void UpdateStrategemParams_can_create_with_distinct_phases_only()
        {
            // Arrange
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
            var updateStratagemParams = new UpdateStratagemParams(factionId, phases, name, description, commandPoints);

            // Assert
            Assert.Equal(2, updateStratagemParams.Phases.Count);
            Assert.True(updateStratagemParams.Phases.Where(x => x.Value == PhaseEnum.Movement).Count() > 0);
            Assert.True(updateStratagemParams.Phases.Where(x => x.Value == PhaseEnum.Charge).Count() > 0);
            Assert.Equal(name, updateStratagemParams.Name);
            Assert.Equal(description, updateStratagemParams.Description);
            Assert.Equal(commandPoints, updateStratagemParams.CommandPoints);
        }
    }
}
