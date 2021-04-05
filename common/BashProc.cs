using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using KYLib.Extensions;
using KYLib.System;
using DO = Newtonsoft.Json.JsonObjectAttribute;
using DP = Newtonsoft.Json.JsonPropertyAttribute;

namespace common
{
	[DO(Newtonsoft.Json.MemberSerialization.OptIn)]
	public class BashProc
	{
		/// <summary>
		/// Comentario opcional a mostrar en el cmd.
		/// </summary>
		[DP] public string Comment;

		/// <summary>
		/// Indica donde queremos que se ejecute el proceso.
		/// </summary>
		[DP] public string RunIn = "{CurrentDir}";

		/// <summary>
		/// Indica el programa que se va a ejecutar.
		/// </summary>
		[DP] public string Task;

		/// <summary>
		/// Argumentos adicionales para pasar al programa.
		/// </summary>
		[DP] public string Args;

		/// <summary>
		/// Argumento que se usa para redirigir la salida del comando.
		/// </summary>	
		[DP] public string OutRedirect;

		/// <summary>
		/// Lista de condiciones
		/// </summary>
		[DP] public List<Condition> Conditions;

		public void Run()
		{
			DiscoverFiles();
			Console.WriteLine($"$ {Task} {Args}");
			Bash.RunCommand(Task, Args, RunIn);
		}

		private static Regex fileValidator = new Regex(@"{File:([.\w]+)}");

		private void DiscoverFiles()
		{
			var files = Directory.GetFiles(RunIn).ToList();
			files = files.Select(s => Path.GetFileName(s)).ToList();

			var mat = fileValidator.Matches(Args);

			foreach (Match item in mat)
			{
				var content = item.Groups.Values.ElementAt(1);
				Args = Args.Replace(
					item.ToString(),
					files.FindAll(
						s => s.Contains(content.ToString())
					).ToString(' ')
				);
			}
		}
	}
}