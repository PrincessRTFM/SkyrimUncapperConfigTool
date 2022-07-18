namespace PrincessRTFM.SSEUncapConfig.Gui.Component.Menu.FileMenus;

internal class SaveDebugCopy: IMenuItem {
	public string Name { get; } = "Save debug copy";
	public Icons? Icon { get; } = Icons.FileCode;

	public void Trigger() => Program.uncapper.SaveCopy("ssu-debug.ini");
}
