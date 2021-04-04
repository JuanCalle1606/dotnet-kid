using System;
using System.Collections;
using System.Reflection;
using System.Text.RegularExpressions;

namespace common
{
	public static class Formarter
	{
		public static void AutoFormat(IEnumerable obj, object with)
		{
			foreach (var item in obj)
				AutoFormat(item, with);
		}

		public static void AutoFormat(IEnumerable obj)
		{
			foreach (var item in obj)
				AutoFormat(item, item);
		}

		public static void AutoFormat(object obj, object with)
		{
			if (obj == null)
				throw new ArgumentNullException(nameof(obj));
			if (with is IEnumerable)
				throw new ArgumentException(null, nameof(with));

			var t = obj.GetType();
			var props = t.GetFields(
			BindingFlags.Public | BindingFlags.DeclaredOnly |
			BindingFlags.GetField | BindingFlags.Instance |
			BindingFlags.SetField);

			var stype = typeof(string);

			foreach (var item in props)
			{
				if (item.FieldType == stype)//es un string, formatear
				{
					//se formatean con las variables internas
					item.SetValue(obj, Format(with, item.GetValue(obj).ToString()));
				}
			}
		}

		public static void AutoFormat(object obj) =>
			AutoFormat(obj, obj);

		private static Regex formatValidator = new Regex("\\{\\w+}");

		public static string Format(object source, string input)
		{
			if (!formatValidator.IsMatch(input)) return input;

			var t = source.GetType();
			var props = t.GetFields(
			BindingFlags.Public | BindingFlags.DeclaredOnly |
			BindingFlags.GetField | BindingFlags.Instance);

			string dev = input;
			foreach (var item in props)
				dev = dev.Replace($"{{{item.Name}}}", item.GetValue(source).ToString());

			return dev;
		}
	}
}