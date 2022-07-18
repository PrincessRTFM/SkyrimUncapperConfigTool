namespace PrincessRTFM.SSEUncapConfig.Gui;

using System.Numerics;

public static class WindowColour {
	private static Vector4 rgba(byte r, byte g, byte b, byte a = 255)
		=> new((float)(r / 255f), (float)(g / 255f), (float)(b / 255f), (float)(a / 255f));
	private static readonly Vector4 none = new(0);

	public static readonly Vector4
		Text = rgba(248, 248, 242),
		TextDisabled = TextColour.Shaded,
		WindowBg = rgba(34, 36, 41),
		ChildBg = none,
		//PopupBg,
		Border = rgba(68, 71, 90),
		BorderShadow = none,
		FrameBg = rgba(17, 17, 22),
		FrameBgHovered = rgba(41, 42, 54),
		FrameBgActive = rgba(89, 93, 117),
		//TitleBg,
		//TitleBgActive,
		//TitleBgCollapsed,
		MenuBarBg = rgba(55, 55, 55),
		ScrollbarBg = none,
		ScrollbarGrab = rgba(68, 71, 90),
		ScrollbarGrabHovered = rgba(98, 114, 164),
		ScrollbarGrabActive = rgba(66, 83, 163),
		CheckMark = rgba(66, 83, 163),
		SliderGrab = rgba(66, 83, 163),
		SliderGrabActive = rgba(66, 83, 163),
		Button = rgba(68, 71, 90),
		ButtonHovered = rgba(98, 114, 164),
		ButtonActive = rgba(66, 83, 163),
		Header = rgba(68, 71, 90),
		HeaderHovered = rgba(66, 83, 163),
		HeaderActive = rgba(66, 83, 163),
		Separator = rgba(110, 110, 128, 128),
		SeparatorHovered = rgba(93, 20, 20, 200),
		SeparatorActive = rgba(93, 20, 20, 241),
		ResizeGrip = rgba(201, 201, 201, 64),
		ResizeGripHovered = rgba(199, 199, 199, 171),
		ResizeGripActive = rgba(80, 250, 123, 200),
		Tab = rgba(68, 71, 90, 0),
		TabHovered = rgba(98, 114, 164),
		TabActive = rgba(66, 83, 163),
		TabUnfocused = rgba(17, 26, 38, 248),
		TabUnfocusedActive = rgba(35, 67, 108),
		//DockingPreview,
		//DockingEmptyBg,
		//PlotLines,
		//PlotLinesHovered,
		//PlotHistogram,
		//PlotHistogramHovered,
		TableHeaderBg = rgba(48, 48, 48),
		TableBorderStrong = rgba(79, 79, 89),
		TableBorderLight = rgba(63, 63, 63),
		TableRowBg = none,
		TableRowBgAlt = rgba(255, 255, 255, 15);
	//DragDropTarget,
	//NavHighlight,
	//NavWindowingHighlight,
	//NavWindowingDimBg,
	//ModalWindowDimBg;
}
