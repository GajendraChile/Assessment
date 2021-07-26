
namespace SEDOLValidator.Interfaces
{
    /// <summary>
    /// SEDOL Validation Result interface.
    /// </summary>
    public interface ISedolValidationResult
    {
        /// <summary>
        /// Gets the input string.
        /// </summary>
        /// <value>
        /// The input string.
        /// </value>
        string InputString { get; }

        /// <summary>
        /// Gets a value indicating whether the input string is a valid SEDOL.
        /// </summary>
        /// <value>
        /// <c>true</c> if the input string is a valid SEDOL; otherwise, <c>false</c>.
        /// </value>
        bool IsValidSedol { get; }

        /// <summary>
        /// Gets a value indicating whether the input string represents a user defined SEDOL.
        /// </summary>
        /// <value>
        /// <c>true</c> if the input string represents a user defined SEDOL; otherwise, <c>false</c>.
        /// </value>
        bool IsUserDefined { get; }

        /// <summary>
        /// Gets the validation details such as root cause of validation failure.
        /// </summary>
        /// <value>
        /// The validation details.
        /// </value>
        string ValidationDetails { get; }
    }
}