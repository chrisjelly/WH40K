﻿using JellyDev.WH40K.Domain.SharedKernel.ValueObjects;
using JellyDev.WH40K.Domain.Stratagem;
using JellyDev.WH40K.Domain.Stratagem.ParameterObjects;
using System;
using System.Collections.Generic;
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
            var phases = new List<Phase> { Phase.FromEnum(PhaseEnum.Movement), Phase.FromEnum(PhaseEnum.Charge) };
            var name = Name.FromString("Test Stratagem");
            var description = Description.FromString("These are the rules for the stratagem.");
            var commandPoints = Amount.FromInt(2);
            var createStratagemParams = new CreateStratagemParams(id, phases, name, description, commandPoints);         
            var strategem = new StratagemAggregate(createStratagemParams);          

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
            StratagemAggregate stratagem = CreateBasicStratagem();

            Assert.Throws<ArgumentNullException>(() => stratagem.Update(null));
        }

        [Fact]
        public void Stratagem_can_be_updated()
        {
            StratagemAggregate stratagem = CreateBasicStratagem();
            var phases = new List<Phase> { Phase.FromEnum(PhaseEnum.Shooting) };
            var name = Name.FromString("Updated Stratagem");
            var description = Description.FromString("This is an updated stratagem.");
            var commandPoints = Amount.FromInt(5);
            var updateStratagemParams = new UpdateStratagemParams(phases, name, description, commandPoints);
            stratagem.Update(updateStratagemParams);

            Assert.True(stratagem.Phases.Where(x => x.Value == PhaseEnum.Shooting).Count() > 0);
            Assert.Equal(name, stratagem.Name);
            Assert.Equal(description, stratagem.Description);
            Assert.Equal(commandPoints, stratagem.CommandPoints);
        }

        /// <summary>
        /// Create a basic stratagem for testing
        /// </summary>
        /// <returns>A basic stratagem for testing</returns>
        private StratagemAggregate CreateBasicStratagem()
        {
            var id = new StratagemId(Guid.NewGuid());
            var phases = new List<Phase> { Phase.FromEnum(PhaseEnum.Movement), Phase.FromEnum(PhaseEnum.Charge) };
            var name = Name.FromString("Test Stratagem");
            var description = Description.FromString("These are the rules for the stratagem.");
            var commandPoints = Amount.FromInt(2);
            return new StratagemAggregate(new CreateStratagemParams(id, phases, name, description, commandPoints));
        }
    }
}
