using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace barren_land
{
    /// <summary>
    /// Class containing logic for parsing the input arguments for the barren-land project
    /// </summary>
    internal class InputParser
    {
        #region Methods

		/// <summary>
		/// Parse the user input into an InputParserResult object
		/// </summary>
		/// <param name="input">The string the user has input</param>
		/// <param name="maxWidth">The maximum width of the parcel of land</param>
		/// <param name="maxHeight">The maximum height of the parcel of land.</param>
		/// <returns>An InputParserResult object.  This object will either store error information of will contain a 2D array representing the land mass.</returns>
        internal static InputParserResult ParseInput(string input, int maxWidth, int maxHeight)
        {
			// Subtract one from the maximum width and height right away since we're using a 0-based array to track the area
			maxWidth -= 1;
			maxHeight -= 1;

            // In an ideal world this would be evaluated using a regular expression
            if (string.IsNullOrEmpty(input))
            {
                return new InputParserResult(false, "The entered string was empty");
            }
            if (!input.StartsWith("{"))
            {
                return new InputParserResult(false, "The input string must start with '{'");
            }
            input = input.Remove(0, 1);
            if (!input.EndsWith("}"))
            {
                return new InputParserResult(false, "The input string must end with '}'");
            }
            input = input.Remove(input.Length - 1, 1);
			// At this point the resulting string should not contain the curly braces, just the rectangles.
			// If the string ends with anything other than a quote then we have invalid input.
			if (!input.EndsWith("\""))
			{
				return new InputParserResult(false, "The input string contains extra unexpected characters at the end.");
			}

			string[] rectangleStrings = input.Split(new string[] { ", " }, StringSplitOptions.None);
			int[,] landMass = new int[maxWidth+1, maxHeight+1];
			ArrayHelpers.Fill2DArrayWithRectangleValues(landMass, InputParserResult.FERTILE_INDICATOR, 0, 0, maxWidth, maxHeight);

			//foreach (string rectString in rectangleStrings)
			for(int i = 0; i < rectangleStrings.Length; i++)
			{
				string rectString = rectangleStrings[i];
				// If any rectangle in the collection does not start with or end with a quote at this point we have invalid input
				if (!rectString.StartsWith("\"") || !rectString.EndsWith("\""))
				{
					return new InputParserResult(false, "There were invalid delimiters in the rectangle string.");
				}
				string onlyDigits = rectString.Substring(1, rectString.Length - 2);
				// The string should be only digits separated by quotes at this point
				string[] digits = onlyDigits.Split(' ');
				if (digits.Length != 4)
				{
					return new InputParserResult(false, $"There were {digits.Length} sides of the rectangle specified instead of 4.");
				}

				int[] sides = new int[4];
				for (int j = 0; j < digits.Length; j++)
				{
					if (!int.TryParse(digits[j], out int side))
					{
						return new InputParserResult(false, $"{digits[j]} could not be parsed as an integer.");
					}
					sides[j] = side;
				}
				int low_x = sides[0];
				if (low_x < 0 || low_x > maxWidth)
				{
					return new InputParserResult(false, $"{low_x} is not a valid x starting coordinate.");
				}
				int low_y = sides[1];
				if (low_y < 0 || low_y > maxHeight)
				{
					return new InputParserResult(false, $"{low_y} is not a valid y starting coordinate.");
				}
				int up_x = sides[2];
				if (up_x > maxWidth || up_x < low_x)
				{
					return new InputParserResult(false, $"The resulting width is outside the allowable bounds.");
				}
				int up_y = sides[3];
				if (up_y > maxHeight || up_y < low_y)
				{
					return new InputParserResult(false, $"The resulting height is outside the allowable bounds.");
				}

				// If we've made it this far we have a valid rectangle string with four valid coordinates.
				// Take those coordinates and use them to fill in the barren land area based on those coordinates.
				ArrayHelpers.Fill2DArrayWithRectangleValues(landMass, InputParserResult.BARREN_INDICATOR, low_x, low_y, up_x, up_y);
			}

            return new InputParserResult(true, "This is fine", landMass);
        }

        #endregion
    }
}
