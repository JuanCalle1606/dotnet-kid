using System.Collections.Generic;
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
		[DP] public string args;

		/// <summary>
		/// Argumento que se usa para redirigir la salida del comando.
		/// </summary>	
		[DP] public string OutRedirect;

		/// <summary>
		/// Lista de condiciones
		/// </summary>
		[DP] public List<Condition> Conditions;

	}
}