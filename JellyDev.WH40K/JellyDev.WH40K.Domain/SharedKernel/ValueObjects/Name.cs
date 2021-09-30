

namespace JellyDev.WH40K.Domain.SharedKernel.ValueObjects
{
    /// <summary>
    /// Value object for the name of something
    /// </summary>
    public class Name : Value<Name>
    {
        /// <summary>
        /// Underlying value
        /// </summary>
        public string Value { get; internal set; }

        /// <summary>
        /// Protected constructor
        /// </summary>
        protected Name() { }

        /// <summary>
        /// Internal constructor
        /// </summary>
        /// <param name="value">The underlying value to set</param>
        internal Name(string value) => Value = value;

        /// <summary>
        /// Implicit operator from value object to underlying value
        /// </summary>
        /// <param name="valObj">Value object</param>
        public static implicit operator string(Name valObj) => valObj.Value;

        /// <summary>
        /// Factory method for creating the value object
        /// </summary>
        /// <param name="value">The underlying value to use</param>
        /// <returns>The created value object</returns>
        public static Name FromString(string value) => new Name(value);
    }
}
