
using System.Collections.Generic;
using System.Drawing;

namespace barren_land
{
	class QueueLandCrawler : ILandCrawler
	{
		/// <summary>
		/// Constructor
		/// </summary>
		public QueueLandCrawler()
		{
			NewFertileIndicator = 1;
		}

		/// <summary>
		/// Each contiguous land area will have it's own ID as indicated by this counter.
		/// </summary>
		private int NewFertileIndicator { get; set; }

		/// <summary>
		/// The total area for the contiguous mass of fertile land.
		/// </summary>
		private int FertileLandArea { get; set; }

		/// <summary>
		/// Find the contiguous areas of fertile ground in the provided array
		/// </summary>
		/// <param name="landMass">an array representing a parcel of land</param>
		/// <param name="barrenIndicator">The integer used to represent a barren parcel of land.</param>
		/// <param name="fertileIndicator">The integer used to represent a fertile parcel of land.</param>
		/// <returns>A list of the contiguous areas of fertile land, sorted from smallest to largest.</returns>
		public List<long> FindFertileGround(int[,] landMass, int barrenIndicator, int fertileIndicator)
		{
			List<long> fertileAreas = new List<long>();
			Queue<Point> visitedPoints = new Queue<Point>();
			Point? startLocation = ArrayHelpers.FindNextOccurranceIn2DArray(landMass, fertileIndicator, new Point(0, 0));

			while (startLocation != null)
			{
				landMass[startLocation.Value.X, startLocation.Value.Y] = NewFertileIndicator;
				FertileLandArea++;

				visitedPoints.Enqueue(startLocation.Value);
				while (visitedPoints.Count > 0)
				{
					Point currentPoint = visitedPoints.Dequeue();

					Point west = new Point(currentPoint.X - 1, currentPoint.Y);
					if (ShouldMoveToParcel(landMass, west.X, west.Y, fertileIndicator))
					{
						landMass[west.X, west.Y] = NewFertileIndicator;
						visitedPoints.Enqueue(west);
						FertileLandArea++;
					}

					Point north = new Point(currentPoint.X, currentPoint.Y + 1);
					if (ShouldMoveToParcel(landMass, north.X, north.Y, fertileIndicator))
					{
						landMass[north.X, north.Y] = NewFertileIndicator;
						visitedPoints.Enqueue(north);
						FertileLandArea++;
					}

					Point east = new Point(currentPoint.X + 1, currentPoint.Y);
					if (ShouldMoveToParcel(landMass, east.X, east.Y, fertileIndicator))
					{
						landMass[east.X, east.Y] = NewFertileIndicator;
						visitedPoints.Enqueue(east);
						FertileLandArea++;
					}

					Point south = new Point(currentPoint.X, currentPoint.Y - 1);
					if (ShouldMoveToParcel(landMass, south.X, south.Y, fertileIndicator))
					{
						landMass[south.X, south.Y] = NewFertileIndicator;
						visitedPoints.Enqueue(south);
						FertileLandArea++;
					}
				}
				fertileAreas.Add(FertileLandArea);
				// Update our counters
				FertileLandArea = 0;
				NewFertileIndicator++;

				startLocation = ArrayHelpers.FindNextOccurranceIn2DArray(landMass, fertileIndicator, startLocation.Value);
			}

			fertileAreas.Sort();
			return fertileAreas;
		}

		/// <summary>
		/// Helper method to determine if the crawler should queue up the parcel for processing
		/// </summary>
		/// <param name="landMass">The array representing the land area</param>
		/// <param name="newX">The x value of the point to look at</param>
		/// <param name="newY">The y value of the point to look at</param>
		/// <param name="validIndicator">The value that should be replaced if the spot in the array is found to be equal to it</param>
		/// <returns>True if the point should be processed, otherwise false.</returns>
		private bool ShouldMoveToParcel(int[,] landMass, int newX, int newY, int validIndicator)
		{
			return newX >= 0 && newY >= 0 && newX < landMass.GetLength(0) && newY < landMass.GetLength(1) && landMass[newX, newY] == validIndicator;
		}
	}
}
