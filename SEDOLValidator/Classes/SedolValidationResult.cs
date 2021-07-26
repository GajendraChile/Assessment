using SEDOLValidator.Interfaces;

namespace SEDOLValidator.Classes
{
    /// <summary>
    /// Validation result of the SEDOL Validator
    /// </summary>
    public class SedolValidationResult : ISedolValidationResult
    {
        public string InputString { get; set; }
        public bool IsValidSedol { get; set; }
        public bool IsUserDefined { get; set; }
        public string ValidationDetails { get; set; }

        public SedolValidationResult(
            string inputString,
            bool isValidSedol,
            bool isUserDefined,
            string validationDetails)
        {
            InputString = inputString;
            IsValidSedol = isValidSedol;
            IsUserDefined = isUserDefined;
            ValidationDetails = validationDetails;
        }

        public SedolValidationResult()
        {
        }
    }
}