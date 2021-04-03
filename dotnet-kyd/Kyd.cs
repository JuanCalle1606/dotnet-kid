using System;
using KYLib.ConsoleUtils;
using KYLib.System;
using KYLib.System.Components;

namespace dotnet_kyd
{
	public class Kyd : ConsoleApp
	{
		public Kyd() : base("Dotnet Kyd")
		{
			ConsoleApp.ClearConsole = false;

			AddItem("Build debug", BuildDebug, false);
			AddItem("Build release", BuildRelease, false);
			AddItem("Run debug", RunDebug, true);
			AddItem("Run release", RunRelease, true);
			AddItem("Build and Run debug", BuildRunDebug, true);
			AddItem("Build and Run release", BuildRunRelease, true);

			YadTrayIcon icon = (YadTrayIcon)TrayIconFactory.Create();
			icon.Tooltip = "dotnet Kyd";
			icon.Icon = "/home/koto/Descargas/vivacristorei.png";
			icon.Command += (_, _) => BuildRunDebug();
		}

		private void BuildRunRelease()
		{
			BuildRelease();
			_ = Cons.Key;
			RunRelease();
		}

		private void BuildRunDebug()
		{
			BuildDebug();
			RunDebug();
		}

		private void BuildRelease() => Bash.RunCommand("dotnet build --configuration Release");

		private void BuildDebug() => Bash.RunCommand("dotnet build");

		private void RunRelease() => Bash.RunCommand("dotnet run --no-build --configuration Release");

		private void RunDebug() => Bash.RunCommand("dotnet run --no-build");
	}
}
