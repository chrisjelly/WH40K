using System;

namespace JellyDev.WH40K.Domain.SharedKernel.ValueObjects
{
    /// <summary>
    /// Value object for an amount of something
    /// </summary>
    public class Amount : Value<Amount>
    {
        /// <summary>
        /// Underlying value
        /// </summary>
        public int Value { get; internal set; }

        /// <summary>
        /// Protected constructor
        /// </summary>
        protected Amount() { }

        /// <summary>
        /// Internal constructor
        /// </summary>
        /// <param name="value">The underlying value to set</param>
        internal Amount(int value) => Value = value;

        /// <summary>
        /// Implicit operator from value object to underlying value
        /// </summary>
        /// <param name="valObj">Value object</param>
        public static implicit operator int(Amount valObj) => valObj.Value;

        /// <summary>
        /// Factory method for creating the value object
        /// </summary>
        /// <param name="value">The underlying value to use</param>
        /// <returns>The created value object</returns>
        public static Amount FromInt(int value)
        {
            if (value < 0) throw new ArgumentOutOfRangeException(nameof(value));
            return new Amount(value);            
        }
    }
}
