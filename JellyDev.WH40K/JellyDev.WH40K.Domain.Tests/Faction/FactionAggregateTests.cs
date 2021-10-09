using JellyDev.WH40K.Domain.SharedKernel.ValueObjects;
using JellyDev.WH40K.Domain.Faction;
using JellyDev.WH40K.Domain.Faction.ParameterObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace JellyDev.WH40K.Domain.Tests.Faction
{
    public class FactionAggregateTests
    {
        [Fact]
        public void Faction_cannot_be_created_with_null_value()
        {
            Assert.Throws<ArgumentNullException>(() => new FactionAggregate(null));
        }

        [Fact]
        public void Faction_can_be_created()
        {
            var id = new FactionId(Guid.NewGuid());
            var name = Name.FromString("Test Faction");
            var createFactionParams = new CreateFactionParams(id, name);
            var faction = new FactionAggregate(createFactionParams);

            Assert.Equal(id, faction.Id);
            Assert.Equal(name, faction.Name);
        }

        [Fact]
        public void Faction_cannot_be_updated_with_null_value()
        {
            FactionAggregate faction = CreateBasicFaction();

            Assert.Throws<ArgumentNullException>(() => faction.Update(null));
        }

        [Fact]
        public void Faction_can_be_updated()
        {
            FactionAggregate faction = CreateBasicFaction();
            var name = Name.FromString("Updated Faction");
            var updateFactionParams = new UpdateFactionParams(name);
            faction.Update(updateFactionParams);

            Assert.Equal(name, faction.Name);
        }

        /// <summary>
        /// Create a basic faction for testing
        /// </summary>
        /// <returns>A basic faction for testing</returns>
        private FactionAggregate CreateBasicFaction()
        {
            var id = new FactionId(Guid.NewGuid());
            var name = Name.FromString("Test Faction");
            return new FactionAggregate(new CreateFactionParams(id, name));
        }
    }
}
