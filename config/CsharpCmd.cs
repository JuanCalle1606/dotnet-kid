using System.Collections.Generic;
using System.CommandLine.Parsing;
using System.IO;
using common;
using KYLib.Data;
using KYLib.Data.DataFiles;

namespace config
{
	partial class Program
	{
		static void Csharp(ParseResult parseResult, bool separeconfig)
		{
			Config nconfig = new();
			//get global options
			var rel = parseResult.ValueForOption(Release);
			var format = parseResult.ValueForOption(Format);
			var name = parseResult.ValueForOption(Name);
			var output = parseResult.ValueForOption(Output);

			if (output != null)
				nconfig.Build.OutputDir = output.FullName;

			nconfig.Build.Configuration = rel ? BuildMode.Release : BuildMode.Debug;
			nconfig.Build.Name = name;

			if (separeconfig)
			{
				nconfig.Build.Cmd.Add(new()
				{
					Comment = "Build the source code into {OutputDir} using {Task}",
					Task = "dotnet",
					Args = "build {OutRedirect} {OutputDir}",
					OutRedirect = "-o",
					Conditions = new()
					{
						new()
						{
							Input = "{Configuration}",
							EqualsTo = "Debug"
						}
					}
				});
				nconfig.Build.Cmd.Add(new()
				{
					Comment = "Build the source code into {OutputDir} using {Task}",
					Task = "dotnet",
					Args = "build -c Release {OutRedirect} {OutputDir}",
					OutRedirect = "-o",
					Conditions = new()
					{
						new()
						{
							Input = "{Configuration}",
							EqualsTo = "Release"
						}
					}
				});
			}
			else
			{
				nconfig.Build.Cmd.Add(new()
				{
					Comment = "Build the source code into {OutputDir} using {Task}",
					Task = "dotnet",
					Args = "build -c {Configuration} {OutRedirect} {OutputDir}",
					OutRedirect = "-o"
				});
			}

			nconfig.Build.Cmd.Add(new()
			{
				Comment = "Clear {OutputDir} from autogenerated files",
				RunIn = "{OutputDir}",
				Task = "rm",
				Args = "-r ref {File:.pdb} {File:.dev.} {File:.deps.} {File:.xml}",
				Conditions = new()
				{
					new()
					{
						Input = "{Configuration}",
						EqualsTo = "Release"
					}
				}
			});


			if (format)
				nconfig.Format();

			Files.Save<JsonFile>(nconfig, ".kyd");
		}
	}
}