using System.CommandLine;
using System.CommandLine.Invocation;
using System.CommandLine.Parsing;
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

			//add aliases
			csharp.AddAlias("c#");
			csharp.AddAlias("cs");

			//hadlers
			root.Handler = CommandHandler.Create(() =>
			{
				root.Invoke("-h");
			});
			vala.Handler = CommandHandler.Create<ParseResult>(Vala);

			//add commands
			root.AddCommand(vala);
			root.AddCommand(csharp);
			//return code
			return await root.InvokeAsync(args);
		}
	}
}
