using DO = Newtonsoft.Json.JsonObjectAttribute;
using DP = Newtonsoft.Json.JsonPropertyAttribute;
namespace common
{
	[DO(Newtonsoft.Json.MemberSerialization.OptIn)]
	public class Condition
	{
		/// <summary>
		/// Texto de entrada
		/// </summary>
		[DP] public string Input = "";

		/// <summary>
		/// Input debe ser igual que esta propiedad
		/// </summary>
		[DP] public string EqualsTo;

		/// <summary>
		/// Input debe ser distinto que esta propiedad
		/// </summary>
		[DP] public string NotEqualsTo;

		/// <summary>
		/// Valida que se cumplan las condiciones
		/// </summary>
		public bool Valid()
		{
			var valid = true;
			if (EqualsTo != null)
				valid &= EqualsTo.Equals(Input);
			if (NotEqualsTo != null)
				valid &= !NotEqualsTo.Equals(Input);
			return valid;
		}
	}
}