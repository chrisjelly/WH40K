using JellyDev.WH40K.Domain.Stratagem;
using System;
using System.Linq;
using Xunit;

namespace JellyDev.WH40K.Domain.Tests.Stratagem
{
    public class StratagemAggregateTests
    {
        [Fact]
        public void Strategem_cannot_be_created_with_null_values()
        {
            var strategem = new StratagemAggregate(new StratagemId(Guid.NewGuid()));

            Assert.Throws<ArgumentNullException>(() => new StratagemAggregate(null));
            Assert.Throws<ArgumentNullException>(() => strategem.Create(null));
        }

        [Fact]
        public void Stratagem_can_be_created()
        {
            var id = new StratagemId(Guid.NewGuid());
            var phases = new SharedKernel.Phase[] { SharedKernel.Phase.Movement, SharedKernel.Phase.Charge };
            
            var strategem = new StratagemAggregate(id);          
            strategem.Create(phases);

            Assert.Equal(id, strategem.Id);
            Assert.Equal(2, strategem.Phases.Length);
            Assert.True(strategem.Phases.Where(x => x == SharedKernel.Phase.Movement).Count() > 0);
            Assert.True(strategem.Phases.Where(x => x == SharedKernel.Phase.Charge).Count() > 0);
        }
    }
}
