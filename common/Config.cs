using System;
using System.Reflection;
using KYLib.Extensions;
using DO = Newtonsoft.Json.JsonObjectAttribute;
using DP = Newtonsoft.Json.JsonPropertyAttribute;

namespace common
{
	[DO(Newtonsoft.Json.MemberSerialization.OptIn)]
	public class Config
	{
		private static Dirs dirs = new();

		/// <summary>
		/// Guarda la configuracion para el comando Build
		/// </summary>
		[DP] public BuildConfig Build = new();

		public void Format()
		{
			Formarter.AutoFormat(Build, dirs);
			Formarter.AutoFormat(Build);

			Formarter.AutoFormat(Build.Cmd, dirs);
			Formarter.AutoFormat(Build.Cmd, Build);
			Formarter.AutoFormat(Build.Cmd);

			foreach (var item in Build.Cmd)
			{
				if (item.Conditions != null)
				{
					Formarter.AutoFormat(item.Conditions, dirs);
					Formarter.AutoFormat(item.Conditions, Build);
					Formarter.AutoFormat(item.Conditions, item);
				}
			}
		}

	}
}