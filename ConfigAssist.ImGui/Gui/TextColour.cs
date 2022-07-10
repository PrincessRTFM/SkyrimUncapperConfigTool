namespace PrincessRTFM.SSEUncapConfig.Gui;

using System.Numerics;
internal static class TextColour {
	private static Vector4 rgba(byte r, byte g, byte b, byte a = 255)
		=> new((float)(r / 255f), (float)(g / 255f), (float)(b / 255f), (float)(a / 255f));

	public static readonly Vector4
		// red
		Dangerous = rgba(255, 30, 30),
		// green
		Safe = rgba(60, 222, 60),
		// red
		Warning = rgba(200, 25, 35),
		// grey
		Shaded = rgba(128, 128, 128),
		// yellow-ish
		Modified = rgba(205, 205, 0),
		// kinda light green sorta
		MatchesDefault = rgba(50, 205, 50);

}
