using JellyDev.WH40K.Domain.SharedKernel.ValueObjects;
using JellyDev.WH40K.Domain.Stratagem;
using JellyDev.WH40K.Domain.Stratagem.ParameterObjects;
using System;
using System.Linq;
using Xunit;

namespace JellyDev.WH40K.Domain.Tests.Stratagem
{
    public class StratagemAggregateTests
    {
        [Fact]
        public void Strategem_cannot_be_created_with_null_value()
        {
            Assert.Throws<ArgumentNullException>(() => new StratagemAggregate(null));
        }

        [Fact]
        public void Stratagem_can_be_created()
        {
            var id = new StratagemId(Guid.NewGuid());
            var phases = new SharedKernel.Phase[] { SharedKernel.Phase.Movement, SharedKernel.Phase.Charge };
            var name = Name.FromString("Test Stratagem");
            var description = Description.FromString("These are the rules for the stratagem.");
            var createStratagemParams = new CreateStratagemParams(id, phases, name, description);         
            var strategem = new StratagemAggregate(createStratagemParams);          

            Assert.Equal(id, strategem.Id);
            Assert.Equal(2, strategem.Phases.Length);
            Assert.True(strategem.Phases.Where(x => x == SharedKernel.Phase.Movement).Count() > 0);
            Assert.True(strategem.Phases.Where(x => x == SharedKernel.Phase.Charge).Count() > 0);
            Assert.Equal(name, strategem.Name);
            Assert.Equal(description, strategem.Description);
        }

        [Fact]
        public void Strategem_cannot_be_updated_with_null_value()
        {
            StratagemAggregate stratagem = CreateBasicStratagem();

            Assert.Throws<ArgumentNullException>(() => stratagem.Update(null));
        }

        [Fact]
        public void Stratagem_can_be_updated()
        {
            StratagemAggregate stratagem = CreateBasicStratagem();
            var phases = new SharedKernel.Phase[] { SharedKernel.Phase.Shooting };
            var name = Name.FromString("Updated Stratagem");
            var description = Description.FromString("This is an updated stratagem.");
            var updateStratagemParams = new UpdateStratagemParams(phases, name, description);
            stratagem.Update(updateStratagemParams);

            Assert.Single(stratagem.Phases);
            Assert.True(stratagem.Phases.Where(x => x == SharedKernel.Phase.Shooting).Count() > 0);
            Assert.Equal(name, stratagem.Name);
            Assert.Equal(description, stratagem.Description);
        }

        /// <summary>
        /// Create a basic stratagem for testing
        /// </summary>
        /// <returns>A basic stratagem for testing</returns>
        private StratagemAggregate CreateBasicStratagem()
        {
            var id = new StratagemId(Guid.NewGuid());
            var phases = new SharedKernel.Phase[] { SharedKernel.Phase.Movement, SharedKernel.Phase.Charge };
            var name = Name.FromString("Test Stratagem");
            var description = Description.FromString("These are the rules for the stratagem.");
            return new StratagemAggregate(new CreateStratagemParams(id, phases, name, description));
        }
    }
}
