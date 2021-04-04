using System.CommandLine;
using System.CommandLine.Invocation;
using System.CommandLine.Parsing;
using System.Reflection;
using System.Threading.Tasks;
using KYLib.ConsoleUtils;

namespace config
{
	partial class Program
	{
		static async Task<int> Main(string[] args)
		{
			//global options
			root.AddGlobalOption(Release);
			root.AddGlobalOption(Format);
			root.AddGlobalOption(Name);
			root.AddGlobalOption(Output);

			//add aliases
			csharp.AddAlias("c#");
			csharp.AddAlias("cs");

			//hadlers
			root.Handler = CommandHandler.Create(() =>
			{
				root.Invoke("-h");
			});

			vala.Handler = CommandHandler.Create(
				typeof(Program).GetMethod("Vala", BindingFlags.Static | BindingFlags.NonPublic),
				null);

			//add commands
			root.AddCommand(vala);
			root.AddCommand(csharp);
			//return code
			return await root.InvokeAsync(args);
		}
	}
}
