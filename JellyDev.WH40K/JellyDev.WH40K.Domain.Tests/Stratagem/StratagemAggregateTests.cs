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
            var createStratagemParamObj = new CreateStratagem(id, phases, name, description);
            
            var strategem = new StratagemAggregate(createStratagemParamObj);          

            Assert.Equal(id, strategem.Id);
            Assert.Equal(2, strategem.Phases.Length);
            Assert.True(strategem.Phases.Where(x => x == SharedKernel.Phase.Movement).Count() > 0);
            Assert.True(strategem.Phases.Where(x => x == SharedKernel.Phase.Charge).Count() > 0);
        }
    }
}
