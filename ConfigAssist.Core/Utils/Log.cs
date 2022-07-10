namespace PrincessRTFM.SSEUncapConfig.Core.Utils;

using System;
using System.Diagnostics;

public static class Log {
	private static void print(string level, string message, params object[] formatArgs) {
		StackTrace trace = new();
		string origin = trace.GetFrame(2)?.GetMethod()?.Module?.Assembly?.GetName()?.Name?.Trim() ?? "Unknown Source";
		Console.WriteLine(string.Format("[{0}: {1}] {2}", origin, level.ToUpper(), string.Format(message, formatArgs)).Trim());
	}

	public static void Error(string message, params object[] formatArgs) => print("ERROR", message, formatArgs);
	public static void Warn(string message, params object[] formatArgs) => print("WARN", message, formatArgs);
	public static void Info(string message, params object[] formatArgs) => print("INFO", message, formatArgs);
	public static void Debug(string message, params object[] formatArgs) => print("DEBUG", message, formatArgs);
}
