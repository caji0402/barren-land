using System.Collections.Generic;
using System.Drawing;

namespace barren_land
{
	/// <summary>
	/// Class responsible for determining the contiguous fertile land areas after the barren land areas have been denoted.
	/// This implementation uses a recursive method call strategy which works well for smaller land masses but will 
	/// overflow the stack for the default input of 400x600.
	/// </summary>
	internal class RecursiveLandCrawler : ILandCrawler
	{
		/// <summary>
		/// Each contiguous land area will have it's own ID as indicated by this counter.
		/// </summary>
		private int NewFertileIndicator { get; set; }

		/// <summary>
		/// The total area for the contiguous mass of fertile land.
		/// </summary>
		private int FertileLandArea { get; set; }

		/// <summary>
		/// Constructor
		/// </summary>
		internal RecursiveLandCrawler()
		{
			NewFertileIndicator = 1;
			FertileLandArea = 0;
		}

		/// <summary>
		/// Find the contiguous areas of fertile ground in the provided array
		/// </summary>
		/// <param name="landMass">an array representing a parcel of land</param>
		/// <param name="barrenIndicator">The integer used to represent a barren parcel of land.</param>
		/// <param name="fertileIndicator">The integer used to represent a fertile parcel of land.</param>
		/// <returns>A list of the contiguous areas of fertile land, sorted from smallest to largest.</returns>
		public List<long> FindFertileGround(int[,] landMass, int barrenIndicator, int fertileIndicator)
		{
			List<long> fertileLandAreas = new List<long>();
			Point? startLocation = ArrayHelpers.FindNextOccurranceIn2DArray(landMass, fertileIndicator, new Point(0, 0));

			while(startLocation != null)
			{
				// Now that we have our start location, we can begin "filling" in the fertile area that is not closed off by barren land
				FillFertileLand(landMass, startLocation.Value.X, startLocation.Value.Y, fertileIndicator, NewFertileIndicator);
				fertileLandAreas.Add(FertileLandArea);
				FertileLandArea = 0;
				NewFertileIndicator++;
				startLocation = ArrayHelpers.FindNextOccurranceIn2DArray(landMass, fertileIndicator, new Point(0, 0));
			}

			fertileLandAreas.Sort();
			return fertileLandAreas;
		}

		/// <summary>
		/// Once an area of fertile land is found, fill it with a new fertile indicator to indicate that that parcel has been visited.
		/// </summary>
		/// <param name="landMass">The array representing the area of land</param>
		/// <param name="x">The x index to change to the new fertile indicator</param>
		/// <param name="y">The y index to change to the new fertile indicator</param>
		/// <param name="oldValue">The old fertile indicator</param>
		/// <param name="newValue">The new fertile indicator</param>
		private void FillFertileLand(int[,] landMass, int x, int y, int oldValue, int newValue)
		{
			if (x < 0 || x >= landMass.GetLength(0) || y < 0 || y >= landMass.GetLength(1)) return;

			if (landMass[x, y] != oldValue) return;

			landMass[x, y] = newValue;
			FertileLandArea++;

			FillFertileLand(landMass, x + 1, y, oldValue, newValue);
			FillFertileLand(landMass, x - 1, y, oldValue, newValue);
			FillFertileLand(landMass, x, y + 1, oldValue, newValue);
			FillFertileLand(landMass, x, y - 1, oldValue, newValue);
		}
	}
}
