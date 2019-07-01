using System;
using System.Drawing;

namespace barren_land
{
	/// <summary>
	/// Class for helpers related to arrays
	/// </summary>
	internal class ArrayHelpers
	{
		/// <summary>
		/// Test whether the given x and y coordinates are withing the bounds of the provided array
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="array">The array to check with</param>
		/// <param name="x">The x (1st dimension) coordinate to verify.</param>
		/// <param name="y">The y (second dimension) coordinate to verify.</param>
		/// <returns>True if the array is not null and the x and y coordinates lie within the bounds; otherwise false.</returns>
		internal static bool IsPointWithin2DArray<T>(T[,] array, int x, int y)
		{
			return array != null && x >= 0 && x < array.GetLength(0) && y >= 0 && y < array.GetLength(1);
		}

		/// <summary>
		/// Fill a 2D array with the specified rectanglular bounds with a specific value
		/// </summary>
		/// <param name="array">The array to fill the value for</param>
		/// <param name="fillValue">The value to fill into the array</param>
		/// <param name="low_x">the lower left x coordinate to fill</param>
		/// <param name="low_y">the lower left y coordinate to fill</param>
		/// <param name="up_x">The upper left x coordinate to fill</param>
		/// <param name="up_y">The upper right y coordinate to fill</param>
		/// <returns>True if the value was filled into the array, otherwise false.</returns>
		internal static bool Fill2DArrayWithRectangleValues<T>(T[,] array, T fillValue, int low_x, int low_y, int up_x, int up_y)
		{
			// TODO: Add Unit Tests
			if (!IsPointWithin2DArray(array, low_x, low_y) || !IsPointWithin2DArray(array, up_x, up_y) || low_x > up_x || low_y > up_y)
			{
				return false;
			}
			for (int j = low_y; j <= up_y; j++)
			{
				for (int i = low_x; i <= up_x; i++)
				{
					array[i, j] = fillValue;
				}
			}
			return true;
		}

		/// <summary>
		/// Prints the contents of a 2D array to the console.
		/// Use this method with caution as it does not lend itself to arrays of a large size.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="array">The array to printparam>
		internal static void PrintArray<T>(T[,] array)
		{
			if (array == null)
			{
				Console.WriteLine("Could not print the array because the array is null.");
				return;
			}
			for(int i = 0; i < array.GetLength(0); i++)
			{
				for (int j = 0; j < array.GetLength(1); j++)
				{
					Console.Write($"{array[i, j]} ");
				}
				Console.WriteLine("");
			}
		}

		/// <summary>
		/// Find the next occurrance of the search object in the array.  The search will start at the provided point
		/// and traverse along the X axis until the end of that axis is reached.  The search will then resume on the next y plane.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="array">The array to search</param>
		/// <param name="objToFind">The object to find the next occurrance of.</param>
		/// <param name="startLocation">The location to start seaching at.</param>
		/// <returns>Null if the next occurrance was not found, otherwise the point where the object was found.</returns>
		internal static Point? FindNextOccurranceIn2DArray<T>(T[,] array, T objToFind, Point startLocation)
		{
			if (array == null || !IsPointWithin2DArray(array, startLocation.X, startLocation.Y))
			{
				return null;
			}
			for(int i = startLocation.X; i < array.GetLength(0); i++)
			{
				for(int j = startLocation.Y; j < array.GetLength(1); j++)
				{
					if (array[i,j].Equals(objToFind))
					{
						return new Point(i, j);
					}
				}
			}
			return null;
		}
	}
}
