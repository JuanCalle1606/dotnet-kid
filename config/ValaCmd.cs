using System.CommandLine.Parsing;
using System.IO;
using common;
using KYLib.Data;
using KYLib.Data.DataFiles;

namespace config
{
	partial class Program
	{
		static void Vala(ParseResult parseResult, string targetglib, string[] dependencies)
		{
			Config nconfig = new();
			//get global options
			var rel = parseResult.ValueForOption(Release);
			var format = parseResult.ValueForOption(Format);
			var name = parseResult.ValueForOption(Name);
			var output = parseResult.ValueForOption(Output);

			if (output != null)
				nconfig.Build.OutputDir = output.FullName;

			nconfig.Build.Configuration = rel ? "Release" : "Debug";
			nconfig.Build.Name = name;
			nconfig.Build.Dependencies = new ValaDeps();
			if (dependencies != null)
				foreach (var item in dependencies)
					nconfig.Build.Dependencies.Deps.Add(item);


			var degubProc = rel ? "" : " --debug";

			nconfig.Build.Cmd.Add(new()
			{
				Comment = "Build the source code into {OutputDir} using {Task}",
				Task = "valac",
				args = $"{{CurrentDir}}/*.vala{degubProc} --target-glib={targetglib} {{Dependencies}} -o {{Name}} {{OutRedirect}} {{OutputDir}}",
				OutRedirect = "-d"
			});

			if (format)
				nconfig.Format();
			Files.Save<JsonFile>(nconfig, ".kyd");
		}
	}
}