using barren_land;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Drawing;

namespace barren_land_test
{
	[TestClass]
	public class TestArrayHelpers
	{
		/// <summary>
		/// Test that passing in a null array will cause the fill array method to return false;
		/// </summary>
		[TestMethod]
		public void TestIsPointInArrayNullArray()
		{
			Assert.AreEqual(false, ArrayHelpers.IsPointWithin2DArray<int>(null, 0, 0));
		}

		/// <summary>
		/// Test an index outside the upper bound of the array
		/// </summary>
		[TestMethod]
		public void TestIsPointWithin2DArrayYOutsideUpperBound()
		{
			int[,] array = new int[5, 5];
			// Size is 5 so the highest possible index is 4
			Assert.AreEqual(false, ArrayHelpers.IsPointWithin2DArray(array, 0, 5));
		}

		/// <summary>
		/// Test an index outside the lower bound of the array
		/// </summary>
		[TestMethod]
		public void TestIsPointWithin2DArrayYOutsideLowerBound()
		{
			int[,] array = new int[5, 5];
			// Size is 5 so the highest possible index is 4
			Assert.AreEqual(false, ArrayHelpers.IsPointWithin2DArray(array, 0, -1));
		}

		/// <summary>
		/// Test an index outside the upper bound of the array
		/// </summary>
		[TestMethod]
		public void TestIsPointWithin2DArrayXOutsideUpperBound()
		{
			int[,] array = new int[5, 5];
			// Size is 5 so the highest possible index is 4
			Assert.AreEqual(false, ArrayHelpers.IsPointWithin2DArray(array, 5, 0));
		}

		/// <summary>
		/// Test an index outside the lower bound of the array
		/// </summary>
		[TestMethod]
		public void TestIsPointWithin2DArrayXOutsideLowerBound()
		{
			int[,] array = new int[5, 5];
			// Size is 5 so the highest possible index is 4
			Assert.AreEqual(false, ArrayHelpers.IsPointWithin2DArray(array, -1, 0));
		}

		/// <summary>
		/// Test that the call to fill an array with a value succeeds when the inputs are within allowable bounds.
		/// Invalid bounds are not tested since the IsPointWithin2DArray is used to validate inputs and that method is already tested.
		/// </summary>
		[TestMethod]
		public void TestIntFillArrayWithValue()
		{
			int[,] array = new int[5, 5];
			bool success = ArrayHelpers.Fill2DArrayWithRectangleValues(array, 1, 0, 0, 4, 4);
			for (int i = 0; i <= 4; i++)
			{
				for (int j = 0; j <= 4; j++)
				{
					success &= array[i, j] == 1;
				}
			}
			Assert.AreEqual(success, true);
		}

		/// <summary>
		/// Test that the ArrayFill method accurately fills the array with the expected value
		/// </summary>
		[TestMethod]
		public void TestStringFillArrayWithValue()
		{
			// Initialize array, it should be filled with null
			string[,] array = new string[5, 5];
			bool success = true;
			success &= ArrayHelpers.Fill2DArrayWithRectangleValues(array, "new", 0, 0, 4, 4);
			for(int i = 0; i < 5; i++)
			{
				for (int j = 0; j < 5; j++)
				{
					success &= array[i, j].Equals("new");
				}
			}
			Assert.AreEqual(true, success);
		}

		/// <summary>
		/// Test that the ArrayHelpers.FindNextOccurranceIn2DArray method will return false if no occurrance of the search value exists
		/// </summary>
		[TestMethod]
		public void TestFindNextOccurranceNoNextOccurranceExists()
		{
			int[,] array = new int[5, 5];
			Point? foundLocation = ArrayHelpers.FindNextOccurranceIn2DArray(array, 1, new Point(0, 0));
			Assert.AreEqual(foundLocation == null, true);
		}

		/// <summary>
		/// Test that the FindNextOccurranceIn2DArray method can find the next occurrance of the provided object.
		/// </summary>
		[TestMethod]
		public void TestFindNextOccurrance()
		{
			int[,] array = new int[5, 5];
			array[4, 4] = 1;
			Point? foundLocation = ArrayHelpers.FindNextOccurranceIn2DArray(array, 1, new Point(0,0));
			Assert.AreEqual(foundLocation?.X == 4 && foundLocation?.Y == 4, true);
		}
	}
}
