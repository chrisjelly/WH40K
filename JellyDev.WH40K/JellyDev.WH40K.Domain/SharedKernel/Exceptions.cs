using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JellyDev.WH40K.Domain.SharedKernel
{
    /// <summary>
    /// Domain exceptions
    /// </summary>
    public static class Exceptions
    {
        /// <summary>
        /// Invalid entity state exception
        /// </summary>
        public class InvalidEntityStateException : Exception
        {
            /// <summary>
            /// Constructor
            /// </summary>
            /// <param name="entity">The entity in an invalid state</param>
            /// <param name="message">Message</param>
            public InvalidEntityStateException(object entity, string message)
                : base($"Entity {entity.GetType().Name} state change rejected, {message}")
            {

            }
        }
    }
}
