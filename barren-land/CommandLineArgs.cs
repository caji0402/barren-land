
namespace barren_land
{
	/// <summary>
	/// Class encapsulating the command line arguments for the barren-land program
	/// </summary>
	internal class CommandLineArgs
	{
		/// <summary>
		/// When in interactive mode the user can continue to add inputs until they type "quit"
		/// </summary>
		internal bool Interactive { get; set; }

		/// <summary>
		/// Whether or not to print the array to the console.
		/// </summary>
		internal bool PrintArray { get; set; }

		/// <summary>
		/// The width of the land mass
		/// </summary>
		internal int Width { get; set; } = -1;

		/// <summary>
		/// The height of the land mass
		/// </summary>
		internal int Height { get; set; } = -1;

		/// <summary>
		/// True if the user requested that the help menu be printed.
		/// </summary>
		internal bool PrintHelp { get; set; }

		/// <summary>
		/// An error message, if any.
		/// </summary>
		internal string ErrorMessage { get; set; }

		/// <summary>
		/// Parse the string arguments into an object.
		/// </summary>
		/// <param name="args">The command line arguments</param>
		/// <param name="commandLineArgs">The resulting object containing properties for the command line arguments.</param>
		/// <returns>True if the arguments were properly parsed; otherwise false.</returns>
		internal static bool ParseCommandLineArgs(string[] args, out CommandLineArgs commandLineArgs)
		{
			//TODO: Unit Test
			commandLineArgs = new CommandLineArgs();
			for (int i = 0; i < args.Length; i++)
			{
				string arg = args[i];
				switch (arg)
				{
					case "-i":
						commandLineArgs.Interactive = true;
						break;
					case "-p":
						commandLineArgs.PrintArray = true;
						break;
					case "-w":
						if (i + 1 < args.Length && int.TryParse(args[++i], out int width))
						{
							commandLineArgs.Width = width;
						}
						else
						{
							commandLineArgs.ErrorMessage = "Could not determine the width.";
							return false;
						}
						break;
					case "-h":
						if (i + 1 < args.Length && int.TryParse(args[++i], out int height))
						{
							commandLineArgs.Height = height;
						}
						else
						{
							commandLineArgs.ErrorMessage = "Could not determine the height.";
							return false;
						}
						break;
					case "-?":
					default:
						commandLineArgs.PrintHelp = true;
						return true;
				}
			}
			return true;
		}
	}
}
