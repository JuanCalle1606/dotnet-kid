using System.Collections.Generic;
using KYLib.Extensions;
using DO = Newtonsoft.Json.JsonObjectAttribute;
using DP = Newtonsoft.Json.JsonPropertyAttribute;

namespace common
{
	[DO(Newtonsoft.Json.MemberSerialization.OptIn)]
	public class BuildDeps
	{
		/// <summary>
		/// Prefijo que se usa para pasar las dependencias por linea de comando.
		/// </summary>
		[DP] public string Prefix;

		/// <summary>
		/// Indica si el prefijo debe ser agregado antes de cada dependencia.
		/// </summary>
		[DP] public bool RepeatPrefix;

		/// <summary>
		/// Lista de dependencias.
		/// </summary>
		[DP] public List<string> Deps;


		public override string ToString()
		{
			if (Deps == null) return string.Empty;
			if (!RepeatPrefix)
				return Prefix + " " + Deps.ToString(' ');
			string dev = null;
			foreach (var item in Deps)
				dev += $"{Prefix} {item} ";
			return dev.TrimEnd();
		}
	}

	public class ValaDeps : BuildDeps
	{
		public ValaDeps()
		{
			Prefix = "--pkg";
			RepeatPrefix = true;
			Deps = new();
			Deps.Add("glib-2.0");
			Deps.Add("gobject-2.0");
		}
	}
}