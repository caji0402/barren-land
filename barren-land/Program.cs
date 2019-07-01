using System;
using System.Collections.Generic;

namespace barren_land
{
    class Program
    {
		/// <summary>
		/// Usage information.
		/// </summary>
		private static string HELP = $"{Environment.NewLine}" +
 			$"This application takes in a set of rectangless in the form of a string and outputs the contiguous fertile {Environment.NewLine}" +
			$"areas of land from smallest to largest.  Rectangles must be in the format {{\"1 1 2 2\", \"3 3 4 4\"}}  {Environment.NewLine}" +
			$"usage: barren-land.exe <-i>|<-w width>|<-h height>|<-p>|<-?>{Environment.NewLine}" +
			$"\t <-i> Interactive mode.  You will be prompted for input until you type 'quit'{Environment.NewLine}" +
			$"\t <-w width> specify the width of the land mass.  400 by default.{Environment.NewLine}" +
			$"\t <-h height> specify the height of the land mass.  600 by default.{Environment.NewLine}" +
			$"\t <-p> print the array of the land mass to the console.  Can only be used with rectangles <= 20x20.{Environment.NewLine}" +
			$"\t <-?> prints this usage{Environment.NewLine}";


		/// <summary>
		/// Default width of the land mass if none was specified.
		/// </summary>
		private const int DEFAULT_WIDTH = 400;

		/// <summary>
		/// Default height of the land mass if none was specified.
		/// </summary>
		private const int DEFAULT_HEIGHT = 600;

		/// <summary>
		/// Do work
		/// </summary>
		/// <param name="args"></param>
		static void Main(string[] args)
        {
			if (!CommandLineArgs.ParseCommandLineArgs(args, out CommandLineArgs commandLineArgs))
			{
				Console.WriteLine($"Unable to parse command line arguments: {commandLineArgs.ErrorMessage}");
				Console.WriteLine(HELP);
				Environment.Exit(1);
			}
			if (commandLineArgs.PrintHelp)
			{
				Console.WriteLine(HELP);
				Environment.Exit(0);
			}

			commandLineArgs.Width = commandLineArgs.Width == -1 ? DEFAULT_WIDTH : commandLineArgs.Width;
			commandLineArgs.Height = commandLineArgs.Height == -1 ? DEFAULT_HEIGHT : commandLineArgs.Height;
			do
			{
				Console.WriteLine("Please enter a properly formatted rectangle or set of rectangles: ");
				string input = Console.ReadLine();
				if (input.Equals("quit"))
				{
					Environment.Exit(0);
				}
				InputParserResult result = InputParser.ParseInput(input, commandLineArgs.Width, commandLineArgs.Height);
				if (!result.Success)
				{
					Console.WriteLine("Unable to parse the input rectangles:");
					Console.WriteLine($"{result.Message}");
					continue;
				}
				ILandCrawler landCrawler = new QueueLandCrawler();
				List<long> fertileAreas = landCrawler.FindFertileGround(result.LandMass, InputParserResult.BARREN_INDICATOR, InputParserResult.FERTILE_INDICATOR);
				Console.Write($"The areas of fertile land are: ");
				fertileAreas.ForEach(x => Console.Write($"{x} "));
				Console.WriteLine($"{Environment.NewLine}{Environment.NewLine}{Environment.NewLine}");
				if (commandLineArgs.PrintArray && commandLineArgs.Width <= 20 && commandLineArgs.Height <= 20)
				{
					ArrayHelpers.PrintArray(result.LandMass);
					Console.WriteLine($"{Environment.NewLine}{Environment.NewLine}{Environment.NewLine}");
				}

				// Cleanup
				result.Dispose();
				landCrawler = null;
			}
			while (commandLineArgs.Interactive);
        }
    }
}
