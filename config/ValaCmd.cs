using System;
using System.CommandLine;
using System.CommandLine.Invocation;
using System.CommandLine.Parsing;
using System.Threading.Tasks;
using static KYLib.ConsoleUtils.Cons;

namespace config
{
	partial class Program
	{
		static int Vala(ParseResult parseResult)
		{
			Error = "debes especificar un nombre";
			Trace();
			vala.Invoke("-h");
			return 1;
		}
	}
}