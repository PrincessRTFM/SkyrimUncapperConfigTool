namespace PrincessRTFM.SSEUncapConfig;

using ImGuiNET;

// Font-Awesome - Version 6

public static class FontAwesomeIconExtensions {
	public static char ToIconChar(this Icons icon) => (char)icon;
	public static string ToIconString(this Icons icon) => string.Empty + (char)icon;
	public static float GetWidth(this Icons icon) => ImGui.CalcTextSize(icon.ToIconString()).X;
}

public enum Icons {

	Folder = 0xF07B,
	FolderOpen = 0xF07C,

	File = 0xF15B,
	FileCode = 0xF1C9,
	FileLines = 0xF15C,
	FloppyDisk = 0xF0C7,

	ExclamationCircle = 0xF06A,
	ExclamationTriangle = 0xF071,
	ExclamationOctagon = 0xE204,
	ExclamationSquare = 0xF321,
	ExclamationHexagon = 0xE417,

	InfoCircle = 0xF05A,
	QuestionCircle = 0xF059,

	Bullhorn = 0xF0A1,
	Bug = 0xF188,
	Gears = 0xF085,
	Lightbulb = 0xF0EB,
	Comment = 0xF086,
	FileUpload = 0xF574,

	ArrowsRotate = 0xF021,
	Trash = 0xF2ED,
	X = 0xF00D,

	PenInSquare = 0xF14B,
	PenToSquare = 0xF044,

	Locked = 0xF023,
	Unlocked = 0xF09C,

}
