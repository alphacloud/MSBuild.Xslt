// ReSharper disable HeapView.BoxingAllocation
namespace Alphacloud.MSBuild.Xslt
{
    using System;
    using JetBrains.Annotations;

    /// <summary>
    ///     Contains detailed error message.
    /// </summary>
    [Serializable]
    [PublicAPI]
    public class ErrorMessage : IEquatable<ErrorMessage>
    {
        /// <summary>
        ///     Initializes a new instance of the ErrorMessage.
        /// </summary>
        public ErrorMessage([NotNull] string message, int lineNumber, int columnNumber)
        {
            if (message == null) throw new ArgumentNullException(nameof(message));

            LineNumber = lineNumber;
            ColumnNumber = columnNumber;
            Message = message;
        }

        /// <summary>
        ///     Error line number.
        /// </summary>
        public int LineNumber { get; }

        /// <summary>
        ///     Error column number.
        /// </summary>
        public int ColumnNumber { get; }

        /// <summary>
        ///     Error message.
        /// </summary>
        public string Message { get; }

        /// <summary>
        ///     Indicates whether the current object is equal to another object of the same type.
        /// </summary>
        /// <returns>
        ///     true if the current object is equal to the <paramref name="other" /> parameter; otherwise, false.
        /// </returns>
        /// <param name="other">An object to compare with this object.</param>
        public bool Equals(ErrorMessage other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return LineNumber == other.LineNumber && ColumnNumber == other.ColumnNumber &&
                string.Equals(Message, other.Message);
        }

        /// <summary>
        ///     Determines whether the specified <see cref="T:System.Object" /> is equal to the current <see cref="T:System.Object" />.
        /// </summary>
        /// <returns>
        ///     true if the specified <see cref="T:System.Object" /> is equal to the current <see cref="T:System.Object" />; otherwise, false.
        /// </returns>
        /// <param name="obj">The object to compare with the current object. </param>
        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            return Equals(obj as ErrorMessage);
        }

        /// <summary>
        ///     Serves as a hash function for a particular type.
        /// </summary>
        /// <returns>
        ///     A hash code for the current <see cref="T:System.Object" />.
        /// </returns>
        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = LineNumber;
                hashCode = (hashCode*397) ^ ColumnNumber;
                hashCode = (hashCode*397) ^ Message.GetHashCode();
                return hashCode;
            }
        }

        /// <summary>
        ///     Equality comparison.
        /// </summary>
        /// <param name="left">The left.</param>
        /// <param name="right">The right.</param>
        public static bool operator ==(ErrorMessage left, ErrorMessage right)
        {
            return Equals(left, right);
        }

        /// <summary>
        ///     Inequality comparison.
        /// </summary>
        /// <param name="left">The left.</param>
        /// <param name="right">The right.</param>
        public static bool operator !=(ErrorMessage left, ErrorMessage right)
        {
            return !Equals(left, right);
        }

        /// <summary>
        /// Returns a string that represents the current object.
        /// </summary>
        /// <returns>
        /// A string that represents the current object.
        /// </returns>
        public override string ToString()
        {
            return $"({LineNumber},{ColumnNumber}): {Message}";
        }
    }
}
