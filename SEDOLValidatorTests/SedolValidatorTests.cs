using NUnit.Framework;
using SEDOLValidator;
using SEDOLValidator.Classes;
using SEDOLValidator.Interfaces;

namespace SEDOLValidatorTests
{
    [TestFixture]
    public class Tests
    {
        private SedolValidator sedolValidator;

        [SetUp]
        public void Setup()
        {
            sedolValidator = new SedolValidator();
        }

        [TestCase(null)]
        [TestCase("")]
        [TestCase("12")]
        [TestCase("12345678")]
        public void SedolNotContainingSevenCharacters(string sedol)
        {
            var actual = new SedolValidator().ValidateSedol(sedol);
            var expected = new SedolValidationResult(sedol, false, false, Constants.INPUT_STRING_NOT_VALID_LENGTH);
            AssertValidationResult(expected, actual);
        }

        [TestCase("_&^%@#!")]
        [TestCase("9123_51")]
        [TestCase("VA.CDE8")]
        public void SedolContainingNonAlphanumericCharacters(string sedol)
        {
            var actual = new SedolValidator().ValidateSedol(sedol);
            var expected = new SedolValidationResult(sedol, false, false, Constants.INPUT_STRING_NOT_VALID_CHARACTER);
            AssertValidationResult(expected, actual);
        }

        [TestCase("9123458")]
        [TestCase("9ABCDE1")]
        public void UserDefinedSedolsWithCorrectChecksum(string sedol)
        {
            var actual = new SedolValidator().ValidateSedol(sedol);
            var expected = new SedolValidationResult(sedol, true, true, null);
            AssertValidationResult(expected, actual);
        }

        [TestCase("9123451")]
        [TestCase("9ABCDE8")]
        public void UserDefinedSedolWithInCorrectChecksum(string sedol)
        {
            var actual = new SedolValidator().ValidateSedol(sedol);
            var expected = new SedolValidationResult(sedol, false, true, Constants.CHECKSUM_NOT_VALID);
            AssertValidationResult(expected, actual);
        }

        [TestCase("1234567")]
        [TestCase("2ABCDE3")]
        public void NonUserDefinedSedolWithInCorrectChecksum(string sedol)
        {
            var actual = new SedolValidator().ValidateSedol(sedol);
            var expected = new SedolValidationResult(sedol, false, false, Constants.CHECKSUM_NOT_VALID);
            AssertValidationResult(expected, actual);
        }

        [TestCase("0709954")]
        [TestCase("B0YBKJ7")]
        public void NonUserDefinedSedolWithCorrectChecksum(string sedol)
        {
            var actual = new SedolValidator().ValidateSedol(sedol);
            var expected = new SedolValidationResult(sedol, true, false, null);
            AssertValidationResult(expected, actual);
        }

        [TestCase("0709954")]
        [TestCase("B0YBKJ7")]
        [TestCase("9123458")]
        [TestCase("9ABCDE1")]
        public void SedolHasValidChecksumDigit(string sedol)
        {
            bool result = new SedolValidator().HasValidCheckDigit(sedol);
            Assert.AreEqual(result, true);
        }

        [TestCase("9123451")]
        [TestCase("9ABCDE8")]
        public void SedolHasInValidChecksumDigit(string sedol)
        {
            bool result = new SedolValidator().HasValidCheckDigit(sedol);
            Assert.AreEqual(result, false);
        }

        [TestCase('2')]
        public void ValidNumericConvertToCharIndex(char sedol)
        {
            int result = new SedolValidator().ConvertToCharIndex(sedol);
            Assert.AreEqual(result, 2);
        }

        [TestCase('A')]
        public void ValidConvertToCharIndexResult_Character_A(char sedol)
        {
            int result = new SedolValidator().ConvertToCharIndex(sedol);
            Assert.AreEqual(result, 10);
        }

        [TestCase('a')]
        public void ValidConvertToCharIndexResult_Character_a(char sedol)
        {
            int result = new SedolValidator().ConvertToCharIndex(sedol);
            Assert.AreEqual(result, 10);
        }

        private static void AssertValidationResult(ISedolValidationResult expected, ISedolValidationResult actual)
        {
            Assert.AreEqual(expected.InputString, actual.InputString, "Input String Failed");
            Assert.AreEqual(expected.IsValidSedol, actual.IsValidSedol, "Is Valid Failed");
            Assert.AreEqual(expected.IsUserDefined, actual.IsUserDefined, "Is User Defined Failed");
            Assert.AreEqual(expected.ValidationDetails, actual.ValidationDetails, "Validation Details Failed");
        }

    }
}