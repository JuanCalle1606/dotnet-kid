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
		/// <summary>
		/// Guarda la configuracion para el comando Build
		/// </summary>
		[DP] public BuildConfig Build = new();
	}

}