using System.Collections.Generic;

namespace barren_land
{
	/// <summary>
	/// An interface used to represent classes that will find the areas of contiguous fertile land.
	/// </summary>
	internal interface ILandCrawler
	{
		/// <summary>
		/// Find the contiguous areas of fertile ground in the provided array
		/// </summary>
		/// <param name="landMass">an array representing a parcel of land</param>
		/// <param name="barrenIndicator">The integer used to represent a barren parcel of land.</param>
		/// <param name="fertileIndicator">The integer used to represent a fertile parcel of land.</param>
		/// <returns>A list of the contiguous areas of fertile land, sorted from smallest to largest.</returns>
		List<long> FindFertileGround(int[,] landMass, int barrenIndicator, int fertileIndicator);
	}
}
