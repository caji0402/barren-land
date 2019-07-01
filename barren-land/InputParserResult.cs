using System;
using System.Collections.Generic;

namespace barren_land
{
    internal class InputParserResult : IDisposable
    {
		/// <summary>
		/// The value used to initialize the land mass array with to indicate that the parcel of land is barren
		/// </summary>
		internal const int BARREN_INDICATOR = 0;

		/// <summary>
		/// The value used to initialize the land mass array with to indicate that the parcel of land is fertile
		/// </summary>
		internal const int FERTILE_INDICATOR = -1;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="success">Whether or not the argument parsing was successful.</param>
        /// <param name="message">Any messages associated with the argument parsing.</param>
        internal InputParserResult(bool success, string message)
        {
            Success = success;
            Message = message;
        }

		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="success">Whether or not the argument parsing was successful.</param>
		/// <param name="message">Any messages associated with the argument parsing.</param>
		/// <param name="landMass">A 2D array representing the land mass with the barren areas defined.</param>
		internal InputParserResult(bool success, string message, int[,] landMass)
			: this(success, message)
		{
			LandMass = landMass;
		}

        /// <summary>
        /// Whether or not the argument parsing was successful.
        /// </summary>
        internal bool Success { get; set; }

        /// <summary>
        /// Any messages associated with the argument parsing.
        /// </summary>
        internal string Message { get; set; }

		/// <summary>
		/// A 2D array representing the land mass.  After initialization the array will be filled with -1 for fertile land
		/// areas, and 0 for the barren land areas.
		/// </summary>
		internal int[,] LandMass { get; set; }

		/// <summary>
		/// Cleanup
		/// </summary>
		public void Dispose()
		{
			LandMass = null;
		}
	}
}
