
using barren_land;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace barren_land_test
{
	/// <summary>
	/// Class for testing the implementations of the ILandCrawler interface.
	/// The class is purposely omitting bounds checks and other actions that are already
	/// covered with unit tests for classes/methods used in the call stack in these methods.
	/// </summary>
	[TestClass]
	public class TestLandCrawlers
	{
		/// <summary>
		/// Test that, given the input rectangle {"0 292 399 307"}, the algorithm correctly outputs the areas 116800 116800
		/// </summary>
		[TestMethod]
		public void TestFindFertileGroundSplitAreaSingleRectangleInput()
		{
			int[,] landMass = new int[400, 600];
			ArrayHelpers.Fill2DArrayWithRectangleValues(landMass, InputParserResult.FERTILE_INDICATOR, 0, 0, 399, 599);
			ArrayHelpers.Fill2DArrayWithRectangleValues(landMass, InputParserResult.BARREN_INDICATOR, 0, 292, 399, 307);
			QueueLandCrawler crawler = new QueueLandCrawler();
			List<long> areas = crawler.FindFertileGround(landMass, InputParserResult.BARREN_INDICATOR, InputParserResult.FERTILE_INDICATOR);
			Assert.AreEqual(areas.Count == 2 && areas[0] == 116800 && areas[1] == 116800, true);
		}

		/// <summary>
		/// Test that, given the input rectangles {"48 192 351 207", "48 392 351 407", "120 52 135 547", "260 52 275 547"}
		/// the algorithm correctly outputs the areas 22816 192608 
		/// </summary>
		[TestMethod]
		public void TestFindFertileGroundFourRectangleInput()
		{
			int[,] landMass = new int[400, 600];
			ArrayHelpers.Fill2DArrayWithRectangleValues(landMass, InputParserResult.FERTILE_INDICATOR, 0, 0, 399, 599);
			ArrayHelpers.Fill2DArrayWithRectangleValues(landMass, InputParserResult.BARREN_INDICATOR, 48, 192, 351, 207);
			ArrayHelpers.Fill2DArrayWithRectangleValues(landMass, InputParserResult.BARREN_INDICATOR, 48, 392, 351, 407);
			ArrayHelpers.Fill2DArrayWithRectangleValues(landMass, InputParserResult.BARREN_INDICATOR, 120, 52, 135, 547);
			ArrayHelpers.Fill2DArrayWithRectangleValues(landMass, InputParserResult.BARREN_INDICATOR, 260, 52, 275, 547);
			QueueLandCrawler crawler = new QueueLandCrawler();
			List<long> areas = crawler.FindFertileGround(landMass, InputParserResult.BARREN_INDICATOR, InputParserResult.FERTILE_INDICATOR);
			Assert.AreEqual(areas.Count == 2 && areas[0] == 22816 && areas[1] == 192608, true);
		}
	}
}
