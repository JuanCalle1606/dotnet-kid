using System;
using KYLib.ConsoleUtils;
using KYLib.System;

namespace dotnet_kyd
{
	public class Kyd : ConsoleApp
	{
		public Kyd() : base("Dotnet Kyd")
		{
			AddItem("Build debug", BuildDebug, false);
			AddItem("Build release", BuildRelease, false);
			AddItem("Run debug", RunDebug, true);
			AddItem("Run release", RunRelease, true);
			AddItem("Build and Run debug", BuildRunDebug, true);
			AddItem("Build and Run release", BuildRunRelease, true);
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
			_ = Cons.Key;
			RunDebug();
		}

		private void BuildRelease() => Bash.CommandUnix("dotnet build --configuration Release");

		private void BuildDebug() => Bash.CommandUnix("dotnet build");

		private void RunRelease() => Bash.CommandUnix("dotnet run --no-build --configuration Release");

		private void RunDebug() => Bash.CommandUnix("dotnet run --no-build");
	}
}