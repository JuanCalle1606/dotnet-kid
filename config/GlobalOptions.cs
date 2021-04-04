using System.CommandLine;
using System.IO;

namespace config
{
	partial class Program
	{
		static Option<bool> Release = new Option<bool>(new[] { "--release", "-r" })
		{
			Description = "Indica si el modo por defecto sera Release",
		};

		static Option<bool> Format = new Option<bool>(new[] { "--format", "-f" })
		{
			Description = "Indica si se deben reemplazar las variables en el archivo de configuración, se recomienda no formatearlas y dejar que se haga de forma automatica al buildear"
		};

		static Option<DirectoryInfo> Output = new Option<DirectoryInfo>(new[] { "--output", "-o" })
		{
			Description = "Directorio de salida del comando Build"
		};

		static Option<string> Name = new Option<string>(new[] { "--name", "-n" }, () => "{CurrentDirName}")
		{
			Description = "Nombre del proyecto y del archivo que sera ejecutado"
		};
	}
}