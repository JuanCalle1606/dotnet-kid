using System;
using System.Collections.Generic;
using System.Reflection;
using KYLib.Extensions;

using DO = Newtonsoft.Json.JsonObjectAttribute;
using DP = Newtonsoft.Json.JsonPropertyAttribute;

namespace common
{
	[DO(Newtonsoft.Json.MemberSerialization.OptIn)]
	public class BuildConfig
	{
		/// <summary>
		/// Nombre del Proyecto.
		/// </summary>
		[DP] public string Name;

		/// <summary>
		/// Configuración que se usara en la contruccion por defecto.
		/// </summary>
		[DP] public string Configuration = "Debug";

		/// <summary>
		/// Directorio en el cual se guardara la salida del programa.
		/// </summary>
		/// <remarks>
		/// ${0}: Directorio actual.
		/// ${1}: Variable <see cref="Configuration"/>.
		/// </remarks>
		[DP] public string OutputDir = "{CurrentDir}/bin/{Configuration}";

		/// <summary>
		/// Lista de comandos a ejecutar.
		/// </summary>
		[DP] public List<BashProc> Cmd = new();

	}
}