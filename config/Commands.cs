using System.CommandLine;
using System.IO;

namespace config
{
	partial class Program
	{
		static RootCommand root = new("Crea un archivo de configuración para ser usado por mis otros comandos.");

		static Command vala = new Command("vala", "Crea un archivo de configuración para un proyecto de vala.")
		{
			new Option<DirectoryInfo>(
				new[]{"--output","-o"},
				"Directorio de salida del comando Build"
			),
			new Option<string>(
				new[]{"--name","-n"},
				()=>"{CurrentDirName}",
				"Nombre del proyecto"
			),
			new Option<string>(
				new[]{"--target-glib","-t"},
				()=>"2.58",
				"Version de glib de destino"
			),
			new Option<string[]>(
				new[]{"--dependencies","-d"},
				"Lista de dependencias a agregar"
			)
		};

		static Command csharp = new Command("csharp", "Crea un archivo de configuración para un proyecto de C#.");
	}
}
