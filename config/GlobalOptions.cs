using System.CommandLine;

namespace config
{
	partial class Program
	{
		static Option<bool> Release = new Option<bool>(new[] { "--release", "-r" })
		{
			Description = "Indica si el modo por defecto sera Release",
		};
	}
}