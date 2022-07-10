namespace PrincessRTFM.SSEUncapConfig.Gui;

using System;
using System.IO;
using System.Runtime.InteropServices;

using ImGuiNET;

using PrincessRTFM.SSEUncapConfig.Core.Utils;

internal static class Fonts {
	public const float BaseFontSizePt = 12f;
	public const float BaseFontSizePx = BaseFontSizePt * 4f / 3f;

	public static float ScaledFontSize { get; private set; }

	private static bool loaded = false;

	public static ImFontPtr Normal { get; private set; }
	public static ImFontPtr Symbols { get; private set; }

	internal static unsafe void load() {
		if (loaded)
			return;
		loaded = true;

		ushort[] symbolRange = new ushort[] { 0xE000, 0xF8FF, 0 };
		GCHandle symHandle = GCHandle.Alloc(symbolRange, GCHandleType.Pinned);
		ImGuiIOPtr io = ImGui.GetIO();
		ImFontAtlasPtr fonts = io.Fonts;
		ImFontConfigPtr fontConfig = ImGuiNative.ImFontConfig_ImFontConfig();
		ScaledFontSize = BaseFontSizePx * io.FontGlobalScale;
		string baseDir = Path.GetDirectoryName(Environment.ProcessPath!)!;

		fontConfig.OversampleH = 2;
		fontConfig.OversampleV = 2;
		fontConfig.PixelSnapH = true;

		Log.Info("Loading default font");
		fonts.AddFontDefault();

		Log.Info("Loading base font");
		Normal = fonts.AddFontFromFileTTF(Path.Combine(baseDir, "Inconsolata-Regular.ttf"), ScaledFontSize, fontConfig);

		Log.Info("Loading symbol font");

		fontConfig.GlyphRanges = symHandle.AddrOfPinnedObject();
		fontConfig.MergeMode = true;
		Symbols = fonts.AddFontFromFileTTF(Path.Combine(baseDir, "FontAwesome6FreeSolid.otf"), ScaledFontSize, fontConfig);

		Log.Info("Building font atlas");
		fonts.Build();

		symHandle.Free();
	}
}
