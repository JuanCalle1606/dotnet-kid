using System;
using System.IO;
using common;
using KYLib.ConsoleUtils;
using KYLib.Data;
using KYLib.Data.DataFiles;

namespace config
{
	partial class Program
	{
		static void FormatCmd(FileInfo file)
		{
			string path = file?.FullName;
			if (string.IsNullOrWhiteSpace(path))
				path = ".kyd";

			Config nconfig;
			try
			{
				nconfig = Files.Load<JsonFile, Config>(path);
			}
			catch (Exception)
			{
				Cons.Error = path.Equals(".kyd") ?
				"No se ha podido encontrar el archivo .kyd en el directorio actual" :
				"El archivo especificado no ha podido ser cargado";
				return;
			}
			nconfig.Format();

			Files.Save<JsonFile>(nconfig, path);
		}
	}
}