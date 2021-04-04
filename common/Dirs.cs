
using System;
using System.IO;

namespace common
{
	public class Dirs
	{
		/// <summary>
		/// Guarda el directorio actual
		/// </summary>
		public string CurrentDir = Environment.CurrentDirectory;

		/// <summary>
		/// Directorio del usuario actual
		/// </summary>
		public string UserDir = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);

		/// <summary>
		/// Nombre del directorio actual
		/// </summary>
		public string CurrentDirName = Environment.CurrentDirectory.Split(Path.DirectorySeparatorChar)[^1];
	}
}