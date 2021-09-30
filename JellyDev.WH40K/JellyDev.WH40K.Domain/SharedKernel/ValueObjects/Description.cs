

namespace JellyDev.WH40K.Domain.SharedKernel.ValueObjects
{
    /// <summary>
    /// Value object for the description of something
    /// </summary>
    public class Description : Value<Description>
    {
        /// <summary>
        /// Underlying value
        /// </summary>
        public string Value { get; internal set; }

        /// <summary>
        /// Protected constructor
        /// </summary>
        protected Description() { }

        /// <summary>
        /// Internal constructor
        /// </summary>
        /// <param name="value">The underlying value to set</param>
        internal Description(string value) => Value = value;

        /// <summary>
        /// Implicit operator from value object to underlying value
        /// </summary>
        /// <param name="valObj">Value object</param>
        public static implicit operator string(Description valObj) => valObj.Value;

        /// <summary>
        /// Factory method for creating the value object
        /// </summary>
        /// <param name="value">The underlying value to use</param>
        /// <returns>The created value object</returns>
        public static Description FromString(string value) => new Description(value);
    }
}
