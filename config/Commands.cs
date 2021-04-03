using System.CommandLine;

namespace config
{
	partial class Program
	{
		static RootCommand root = new("Crea un archivo de configuración para ser usado por mis otros comandos.");

		static Command vala = new Command("vala", "Crea un archivo de configuración para un proyecto de vala.");

		static Command csharp = new Command("csharp", "Crea un archivo de configuración para un proyecto de C#.");
	}
}
