namespace PrincessRTFM.SSEUncapConfig;

using System;
using System.Diagnostics;
using System.IO;
using System.Numerics;
using System.Reflection;

using ImGuiNET;

using ImGuiScene;

using PrincessRTFM.SSEUncapConfig.Core;
using PrincessRTFM.SSEUncapConfig.Core.Utils;
using PrincessRTFM.SSEUncapConfig.Gui;
using PrincessRTFM.SSEUncapConfig.Interop;

internal static class Program {

	internal static readonly INIFile settings = new(Path.ChangeExtension(Environment.ProcessPath!, "ini"));

	internal static readonly UncapperConfig defaults = new();
	internal static readonly UncapperConfig original = new();
	internal static readonly UncapperConfig uncapper = new();

	internal static IntPtr hwnd = IntPtr.Zero;

	public static string UncapperFilePath {
		get => settings.Get("Uncapper", "iniPath", @"C:\Program Files (x86)\Steam\steamapps\common\Skyrim Special Edition\Data\SKSE\Plugins\SkyrimUncapper.ini");
		set {
			if (uncapper.LoadFrom(value)) {
				Log.Debug("Copying loaded settings for is-modified comparisons");
				original.CopyFrom(uncapper);
				settings.Set("Uncapper", "iniPath", value);
			}
		}
	}

	public static void OpenUncapperFile(SimpleImGuiScene? sceneToAbort = null) {
		if (!OpenFileDialog.IsSelecting) {
			string? originDir = Path.GetDirectoryName(UncapperFilePath);
			Log.Debug($"Opening file dialog from {originDir ?? "default windows location"}");

			OpenFileDialog.SelectFile(
				(OpenFileName file) => {
					if (File.Exists(file.File)) {
						UncapperFilePath = file.File;
					}
				},
				() => {
					Log.Info("File selection cancelled");
					if (sceneToAbort is not null)
						sceneToAbort.ShouldQuit = true;
				},
				originDir,
				"Select your SkyrimUncapper.ini file",
				new string[] {
					"SkyrimUncapper.ini",
				},
				new string[] {
					"All INI files",
					"*.ini",
				}
			);
		}
	}

	public static void OpenUrl(string address)
		=> Process.Start(new ProcessStartInfo(address) { UseShellExecute = true });

	private static unsafe void Main() {
		using SimpleImGuiScene scene = new(RendererFactory.RendererBackend.DirectX11, new() {
			Title = Window.Title,
			XPos = 50,
			YPos = 90,
			Width = Window.Width,
			Height = Window.Height,
		});

		hwnd = scene.Window.GetHWnd();
		ImGuiStylePtr style = ImGui.GetStyle();

		foreach (ImGuiCol col in Enum.GetValues<ImGuiCol>()) {
			if (col == ImGuiCol.COUNT) {
				continue;
			}

			Vector4? colour = (Vector4?)typeof(WindowColour).GetField(col.ToString(), BindingFlags.Public | BindingFlags.Static)?.GetValue(null);
			if (colour.HasValue) {
				Log.Info($"Setting {col} to {colour}");
				style.Colors[(int)col] = colour.Value;
			}
		}

		Log.Info("Initialising fonts");
		Fonts.load();

		if (File.Exists(UncapperFilePath)) {
			Log.Info("Performing initial load/copy");
			uncapper.LoadFrom(UncapperFilePath);
			original.CopyFrom(uncapper);
		}
		if (!uncapper.HasExistingFile) {
			OpenUncapperFile(scene);
		}

		if (!int.TryParse(settings.Get("GUI.section", "Core", "0"), out Window.selectedPaneIndex))
			Window.selectedPaneIndex = 0;

		scene.OnBuildUI += Window.Draw;

		Log.Info("Starting render loop");
		scene.Run();

		settings.Save();
	}
}
