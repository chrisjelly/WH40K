using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JellyDev.WH40K.Domain.SharedKernel.ValueObjects
{
    /// <summary>
    /// Value object for a phase
    /// </summary>
    public class Phase : Value<Phase>
    {
        /// <summary>
        /// Underlying value
        /// </summary>
        public PhaseEnum Value { get; internal set; }

        /// <summary>
        /// Protected constructor
        /// </summary>
        protected Phase() { }

        /// <summary>
        /// Internal constructor
        /// </summary>
        /// <param name="value">The underlying value to set</param>
        internal Phase(PhaseEnum value) => Value = value;

        /// <summary>
        /// Factory method for creating the value object
        /// </summary>
        /// <param name="value">The underlying value to use</param>
        /// <returns>The created value object</returns>
        public static Phase FromString(string value) => new Phase((PhaseEnum)Enum.Parse(typeof(PhaseEnum), value));

        /// <summary>
        /// Factory method for creating the value object
        /// </summary>
        /// <param name="value">The underlying value to use</param>
        /// <returns>The created value object</returns>
        public static Phase FromEnum(PhaseEnum value) => new Phase(value);
    }

    /// <summary>
    /// Enumerated Phase values
    /// </summary>
    public enum PhaseEnum
    {
        Unknown = 0,
        Command = 1,
        Movement = 2,
        Psychic = 3,
        Shooting = 4,
        Charge = 5,
        Fight = 6,
        Morale = 7
    }
}
