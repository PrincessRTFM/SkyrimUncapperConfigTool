namespace PrincessRTFM.SSEUncapConfig.Interop;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading;

using PrincessRTFM.SSEUncapConfig.Core.Utils;

public class OpenFileDialog {
	public const int MaxFilePathLength = 1023;
	public const int MaxFileTitleLength = 255;

	private static readonly SemaphoreSlim selectorSemaphore = new(1, 1);
	public static bool IsSelecting => selectorSemaphore.CurrentCount == 0;

	[DllImport("Comdlg32.dll", CharSet = CharSet.Auto)]
	private static extern bool GetOpenFileName([In, Out] OpenFileName ofn);

	public static void SelectFile(Action<OpenFileName> successCallback, Action? cancelCallback = null, string? initialDir = null, string title = "Select a file...", params string[][] filterLines) {
		if (selectorSemaphore.Wait(0)) {
			new Thread(() => {
				Log.Debug("Initialising file selector");
				try {
					OpenFileName ofn = new();

					ofn.StructSize = Marshal.SizeOf(ofn);

					ofn.DlgOwner = Program.hwnd;

					List<string> FileTypes = new();
					foreach (string[] line in filterLines) {
						if (line.Length < 1)
							continue;
						FileTypes.Add($"{line[0]}\0{string.Join(";", line.Length > 1 ? line.Skip(1) : line[0])}");
					}
					FileTypes.Add("All files\0*\0");

					ofn.Filter = string.Join("\0", FileTypes);

					ofn.File = new string(new char[MaxFilePathLength + 1]);
					ofn.MaxFile = ofn.File.Length;

					ofn.FileTitle = new string(new char[MaxFileTitleLength + 1]);
					ofn.MaxFileTitle = ofn.FileTitle.Length;

					ofn.InitialDir = initialDir;
					ofn.Title = title;

					Log.Debug($"OFD:InitialDir = {initialDir ?? "[default]"}");
					Log.Debug($"OFD:Title = {title}");

					Log.Debug("Dispatching winapi call");
					if (GetOpenFileName(ofn))
						successCallback(ofn);
					else
						cancelCallback?.Invoke();
					Log.Debug("Dialog closed");
				}
				catch (Exception e) {
					Log.Error(e.Message + "\n" + e.StackTrace ?? "");
				}
				selectorSemaphore.Release();
				Log.Debug("Ending file selection");
			}).Start();
		}
		else {
			Log.Error("Failed to open file dialog");
		}
	}
}
