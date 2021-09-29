using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;

namespace JellyDev.WH40K.Domain.SharedKernel
{
    /// <summary>
    /// Value object
    /// </summary>
    /// <typeparam name="T">The type of value object</typeparam>
    public abstract class Value<T> where T : Value<T>
    {
        /// <summary>
        /// The members of the value object
        /// </summary>
        private static readonly Member[] Members = GetMembers().ToArray();

        /// <summary>
        /// Check equality between value objects
        /// </summary>
        /// <param name="other">The other value object</param>
        /// <returns>True if the value objects are equal</returns>
        public override bool Equals(object other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;

            var members = Members;

            return other.GetType() == typeof(T) && Members.All(m =>
            {
                var otherValue = m.GetValue(other);
                var thisValue = m.GetValue(this);
                return m.IsNonStringEnumerable
                    ? GetEnumerableValues(otherValue).SequenceEqual(GetEnumerableValues(thisValue))
                    : (otherValue?.Equals(thisValue) ?? thisValue == null);
            });
        }

        /// <summary>
        /// Get the hash code
        /// </summary>
        /// <returns>The hash code</returns>
        public override int GetHashCode() =>
            CombineHashCodes(
                Members.Select(m => m.IsNonStringEnumerable
                    ? CombineHashCodes(GetEnumerableValues(m.GetValue(this)))
                    : m.GetValue(this)));

        /// <summary>
        /// Equality check for value objects
        /// </summary>
        /// <param name="left">The left side of the equality check</param>
        /// <param name="right">The right side of the equality check</param>
        /// <returns>True if the value objects are equal</returns>
        public static bool operator ==(Value<T> left, Value<T> right) => Equals(left, right);

        /// <summary>
        /// Non-equality check for value objects
        /// </summary>
        /// <param name="left">The left side of the non-equality check</param>
        /// <param name="right">The right side of the non-equality check</param>
        /// <returns>True if the value objects are non-equal</returns>
        public static bool operator !=(Value<T> left, Value<T> right) => !Equals(left, right);

        /// <summary>
        /// Render the members of the value object as a string
        /// </summary>
        /// <returns>The members of the value object as a string</returns>
        public override string ToString()
        {
            if (Members.Length == 1)
            {
                var m = Members[0];
                var value = m.GetValue(this);
                return m.IsNonStringEnumerable
                    ? $"{string.Join("|", GetEnumerableValues(value))}"
                    : value.ToString();
            }

            var values = Members.Select(m =>
            {
                var value = m.GetValue(this);
                return m.IsNonStringEnumerable
                    ? $"{m.Name}:{string.Join("|", GetEnumerableValues(value))}"
                    : m.Type != typeof(string)
                        ? $"{m.Name}:{value}"
                        : value == null
                            ? $"{m.Name}:null"
                            : $"{m.Name}:\"{value}\"";
            });
            return $"{typeof(T).Name}[{string.Join("|", values)}]";
        }

        /// <summary>
        /// Get the members of the value object
        /// </summary>
        /// <returns>The members of the value object</returns>
        private static IEnumerable<Member> GetMembers()
        {
            var t = typeof(T);
            const BindingFlags flags = BindingFlags.Instance | BindingFlags.Public;
            while (t != typeof(object))
            {
                if (t == null) continue;
                foreach (var p in t.GetProperties(flags)) yield return new Member(p);
                foreach (var f in t.GetFields(flags)) yield return new Member(f);
                t = t.BaseType;
            }
        }

        /// <summary>
        /// Get the enumerable values of an object
        /// </summary>
        /// <param name="obj">The object</param>
        /// <returns>The enumerable values of an object</returns>
        private static IEnumerable<object> GetEnumerableValues(object obj)
        {
            var enumerator = ((IEnumerable)obj).GetEnumerator();
            while (enumerator.MoveNext()) yield return enumerator.Current;
        }

        /// <summary>
        /// Combine hash codes for enumerable objects
        /// </summary>
        /// <param name="objs">The enumerable objects</param>
        /// <returns>The hash codes for the enumerable objects</returns>
        private static int CombineHashCodes(IEnumerable<object> objs)
        {
            unchecked
            {
                return objs.Aggregate(17, (current, obj) => current * 59 + (obj?.GetHashCode() ?? 0));
            }
        }

        /// <summary>
        /// Value object member
        /// </summary>
        private struct Member
        {
            /// <summary>
            /// Name of the member
            /// </summary>
            public readonly string Name;

            /// <summary>
            /// Delegate for getting the value of a member
            /// </summary>
            public readonly Func<object, object> GetValue;

            /// <summary>
            /// True if the member is non-string enumerable
            /// </summary>
            public readonly bool IsNonStringEnumerable;

            /// <summary>
            /// The type of the member
            /// </summary>
            public readonly Type Type;

            /// <summary>
            /// Create a value object member
            /// </summary>
            /// <param name="info">Member information</param>
            public Member(MemberInfo info)
            {
                switch (info)
                {
                    case FieldInfo field:
                        Name = field.Name;
                        GetValue = obj => field.GetValue(obj);
                        IsNonStringEnumerable = typeof(IEnumerable).IsAssignableFrom(field.FieldType) &&
                                                field.FieldType != typeof(string);
                        Type = field.FieldType;
                        break;
                    case PropertyInfo prop:
                        Name = prop.Name;
                        GetValue = obj => prop.GetValue(obj);
                        IsNonStringEnumerable = typeof(IEnumerable).IsAssignableFrom(prop.PropertyType) &&
                                                prop.PropertyType != typeof(string);
                        Type = prop.PropertyType;
                        break;
                    default:
                        throw new ArgumentException("Member is not a field or property", info.Name);
                }
            }
        }
    }
}
