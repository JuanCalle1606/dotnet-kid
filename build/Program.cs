using System;
using System.CommandLine;
using System.CommandLine.Invocation;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;
using common;
using KYLib.ConsoleUtils;
using KYLib.Data;
using KYLib.Data.DataFiles;
using KYLib.MathFn;

namespace build
{
	class Program
	{
		static RootCommand root = new("Buildea el proyecto actual basado en un archivo .kyd generado por el comando config.")
		{
			new Argument<FileInfo>(
				"file",
				() => new FileInfo(".kyd"),
				"Archivo de configuración usado, si no se especifica se buscara uno en el directorio actual."
			)
			{
				//Arity = ArgumentArity.ZeroOrOne
			}.ExistingOnly()
		};

		static async Task<int> Main(string[] args)
		{
			root.Handler = CommandHandler.Create(
				typeof(Program).GetMethod("Root", BindingFlags.Static | BindingFlags.NonPublic),
				null);
			return await root.InvokeAsync(args);
		}

		static int Root(FileInfo file)
		{
			string path = file.FullName;
			string raw = null;

			Config nconfig;
			try
			{
				raw = File.ReadAllText(path);
				nconfig = Files.Deserialize<JsonFile, Config>(raw);
			}
			catch (Exception)
			{
				Cons.Error = path.Equals(".kyd") ?
				"No se ha podido encontrar el archivo .kyd en el directorio actual" :
				$"El archivo {path} no ha podido ser cargado";
				Cons.Trace();
				root.Invoke("-h");
				return 1;
			}

			//all ok, start building
			if (nconfig.Build.Configuration == BuildMode.Both)
			{
				var debugMode = Files.Deserialize<JsonFile, Config>(raw);
				debugMode.Build.Configuration = BuildMode.Debug;
				debugMode.Format();
				var releaseMode = Files.Deserialize<JsonFile, Config>(raw);
				releaseMode.Build.Configuration = BuildMode.Release;
				releaseMode.Format();
				BuildWith(releaseMode);
				BuildWith(debugMode);
			}
			else
			{
				nconfig.Format();
				BuildWith(nconfig);
			}
			Cons.Line = "Build completed";
			return 0;
		}

		static void BuildWith(Config nconfig)
		{
			Int ntasks = 1, ttask = nconfig.Build.Cmd.Count;
			Cons.Line = $"Building {nconfig.Build.Name} with {ttask} tasks.";
			bool canrun = false;
			Condition lastCon = null;
			foreach (var item in nconfig.Build.Cmd)
			{
				canrun = false;
				Cons.Line = $"Task [{ntasks++}/{ttask}]: {item.Comment}";

				if (item.Conditions == null || item.Conditions.Count == 0)
				{
					Cons.Line = "This task has no conditions.";
					canrun = true;
				}
				else
				{
					Cons.Line = $"Validating {item.Conditions.Count} conditions to run.";
					canrun = item.Conditions.TrueForAll(c =>
					{
						var dev = c.Valid();
						if (!dev) lastCon = c;
						return dev;
					});
				}

				if (canrun)
					item.Run();
				else
					Cons.Line = $"Jumping task due to condition ({lastCon}) is not true";

			}
		}
	}
}
