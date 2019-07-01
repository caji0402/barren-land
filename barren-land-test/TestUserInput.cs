using Microsoft.VisualStudio.TestTools.UnitTesting;
using barren_land;
using System;

namespace barren_land_test
{
    [TestClass]
    public class TestUserInput
    {
		/// <summary>
		/// Context used to write output to.
		/// </summary>
		public TestContext TestContext { get; set; }

		/// <summary>
		/// Initial test - sanity check
		/// </summary>
        [TestMethod]
        public void SanityCheck()
        {
            Assert.AreEqual(true, true);
        }

		/// <summary>
		/// Test that an empty input string results in a parsing failure.
		/// </summary>
        [TestMethod]
        public void TestEmptyInput()
        {
            InputParserResult result = InputParser.ParseInput("", 400, 600);
			Console.WriteLine("Does this work?");
            Assert.AreEqual(result.Success, false);
        }

		/// <summary>
		/// Test that a string that does not begin with a curly brace results in a parsing failure.
		/// </summary>
		[TestMethod]
        public void TestBeginsWithCurlyBrace()
        {
            InputParserResult result = InputParser.ParseInput("\"111 111 222 222\"}", 400, 600);
			Assert.AreEqual(result.Success, false);
        }

		/// <summary>
		/// Test that a string that does not end with a curly brace results in a parsing failure.
		/// </summary>
		[TestMethod]
		public void TestEndsWithCurlyBrace()
		{
			InputParserResult result = InputParser.ParseInput("{\"111 111 222 222\"", 400, 600);
			Assert.AreEqual(result.Success, false);
		}

		/// <summary>
		/// Test that a string which does not contain four points representing the rectangle results in a parsing failure.
		/// </summary>
		[TestMethod]
		public void TestNotFourNumbersInARectangle()
		{
			InputParserResult result = InputParser.ParseInput("{\"111 111 222\"}", 400, 600);
			Assert.AreEqual(result.Success, false);
		}

		/// <summary>
		/// Test that rectangles which are not comma and space delimited results in a parsing failure.
		/// </summary>
		[TestMethod]
		public void TestRectanglesNotCommaSpaceDelimited()
		{
			InputParserResult result = InputParser.ParseInput("{\"111 111 222 222\",\"111 222 333 444\"}", 400, 600);
			Assert.AreEqual(result.Success, false);
		}

		/// <summary>
		/// Test that an otherwise properly formatted string that contains a character other than a quote before 
		/// the ending curly brace results in a parsing failure.
		/// </summary>
		[TestMethod]
		public void TestEndsWithCharacterThatIsNotAQuote()
		{
			InputParserResult result = InputParser.ParseInput("{\"111 111 399 222\", }", 400, 600);
			Assert.AreEqual(result.Success, false);
		}

		/// <summary>
		/// Test that a rectangle with a width greater than the maximum allowed will result in a parsing failure.
		/// </summary>
		[TestMethod]
		public void TestInvalidRectangleTooLong()
		{
			InputParserResult result = InputParser.ParseInput("{\"111 111 400 222\"}", 400, 600);
			Assert.AreEqual(result.Success, false);
		}

		/// <summary>
		/// Test that a rectangle with a height greater than the maximum allowed will result in a parsing failure.
		/// </summary>
		[TestMethod]
		public void TestInvalidRectangleTooTall()
		{
			InputParserResult result = InputParser.ParseInput("{\"111 111 222 600\"}", 400, 600);
			Assert.AreEqual(result.Success, false);
		}

		/// <summary>
		/// Test that user input that contains characters that are not numbers will result in a parsing error.
		/// </summary>
		[TestMethod]
		public void TestNotANumber()
		{
			InputParserResult result = InputParser.ParseInput("{\"111 111 4OO 222\"}", 400, 600);
			Assert.AreEqual(result.Success, false);
		}

		/// <summary>
		/// Test that user inputs that includes a rectangle with a negative width will result in a parsing error.
		/// </summary>
		[TestMethod]
		public void TestNegativeWidthRectangle()
		{
			InputParserResult result = InputParser.ParseInput("{\"111 111 50 222\"}", 400, 600);
			Assert.AreEqual(result.Success, false);
		}

		/// <summary>
		/// Test that a negative user input will result in a parsing error.
		/// </summary>
		[TestMethod]
		public void TestNegativeUserInput()
		{
			InputParserResult result = InputParser.ParseInput("{\"111 111 -222 222\"}", 400, 600);
			Assert.AreEqual(result.Success, false);
		}

		/// <summary>
		/// Test a valid input that contains a single rectangle with coordinates {"0 0 399 599"} 
		/// </summary>
		[TestMethod]
		public void TestValidInputSingleRectangle()
		{
			InputParserResult result = InputParser.ParseInput("{\"0 0 399 599\"}", 400, 600);
			Assert.AreEqual(result.Success, true);
		}

		/// <summary>
		/// Test a valid input that contains four rectangles with coordinates 
		/// {“48 192 351 207”, “48 392 351 407”, “120 52 135 547”, “260 52 275 547”} 
		/// </summary>
		[TestMethod]
		public void TestValidInputFourRectangles()
		{
			InputParserResult result = InputParser.ParseInput("{\"48 192 351 207\", \"48 392 351 407\", \"120 52 135 547\", \"260 52 275 547\"}", 400, 600);
			TestContext.WriteLine($"{result.Message}");
			Assert.AreEqual(result.Success, true);
		}
    }
}
