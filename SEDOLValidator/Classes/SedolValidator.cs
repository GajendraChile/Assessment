using SEDOLValidator.Interfaces;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace SEDOLValidator.Classes
{
    /// <summary>
    /// A Sedol Validator
    /// </summary>
    public class SedolValidator : ISedolValidator
    {
        private List<int> _weightList = new List<int> { 1, 3, 1, 7, 3, 9 };
        private const int CHECK_DIGIT_INDEX = 6;
        private const int SEDOL_LENGTH = 7;
        private const int USER_DEFINED_INDEX = 0;
        private const char USER_DEFINED_CHAR = '9';
        /// <summary>
        /// Accepts string input and returns result of type ISedolValidationResult
        /// </summary>
        /// <param name="input"></param>
        /// <returns>Instance of validation result.</returns>
        public ISedolValidationResult ValidateSedol(string input)
        {
            var result = new SedolValidationResult
            {
                InputString = input,
                IsUserDefined = false,
                IsValidSedol = false,
                ValidationDetails = null
            };

            if (!this.IsValidLength(input))
            {
                result.ValidationDetails = Constants.INPUT_STRING_NOT_VALID_LENGTH;
                return result;
            }
            if (!this.IsAlphaNumeric(input))
            {
                result.ValidationDetails = Constants.INPUT_STRING_NOT_VALID_CHARACTER;
                return result;
            }
            if (this.IsUserDefined(input))
            {
                result.IsUserDefined = true;
            }

            if (this.HasValidCheckDigit(input))
                result.IsValidSedol = true;
            else
                result.ValidationDetails = Constants.CHECKSUM_NOT_VALID;

            return result;
        }

        /// <summary>
        /// Checks wether the checksum provided in the SEDOL matches with calculated checksum
        /// </summary>
        /// <returns>true if provided checksum matches with the calculated; otherwise false </returns>
        public bool HasValidCheckDigit(string input)
        {
            char[] codeValues = input.ToCharArray();
            var weightedSum = 0;
            for (int i = 0; i < this._weightList.Count; i++)
            {
                weightedSum += this._weightList[i] * ConvertToCharIndex(codeValues[i]);
            }
            char checkSum = Convert.ToChar(((10 - (weightedSum % 10)) % 10).ToString());
            return input[CHECK_DIGIT_INDEX] == checkSum;
        }

        /// <summary>
        /// Convert the SEDOL Character into Character index
        /// Letters are converted to Upper then ASCII is generated and 55 is subtracted from resulting ASCII code
        /// 48 is subtracted from ASCII code for numbers
        /// </summary>
        /// <param name="item"></param>
        /// <returns>List of SEDOL Character indexes</returns>
        public int ConvertToCharIndex(char item)
        {
            if (char.IsLetter(item))
                return char.ToUpper(item) - 55;
            else
                return item - 48;
        }

        /// <summary>
        /// Checks if the provided SEDOL contains only allowed characters. 
        /// </summary>
        /// <returns>true if SEDOL contains valid characters; otherwise false </returns>
        private bool IsUserDefined(string input)
        {
            return input[USER_DEFINED_INDEX] == USER_DEFINED_CHAR;
        }

        /// <summary>
        /// Checks if the provided SEDOL contains only allowed characters. 
        /// </summary>
        /// <returns>true if SEDOL contains valid characters; otherwise false </returns>
        private bool IsAlphaNumeric(string input)
        {
            return Regex.IsMatch(input, "^[a-zA-Z0-9]*$");
        }

        /// <summary>
        /// Checks if the provided SEDOL has valid length. 
        /// </summary>
        /// <returns>true if length is valid; otherwise false </returns>
        private bool IsValidLength(string input)
        {
            return !string.IsNullOrEmpty(input) && input.Length == SEDOL_LENGTH;
        }
    }
 }