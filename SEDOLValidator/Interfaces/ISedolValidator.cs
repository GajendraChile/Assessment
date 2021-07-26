
namespace SEDOLValidator.Interfaces
{
    /// <summary>
    /// Interface representing SEDOL validator.
    /// </summary>
    public interface ISedolValidator
    {
        /// <summary>
        /// Validates the SEDOL.
        /// </summary>
        /// <param name="input">The input.</param>
        /// <returns>Instance of validation result.</returns>
        ISedolValidationResult ValidateSedol(string input);
    }
}