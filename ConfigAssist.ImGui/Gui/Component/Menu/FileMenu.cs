namespace PrincessRTFM.SSEUncapConfig.Gui.Component.Menu;

using PrincessRTFM.SSEUncapConfig.Gui.Component.Menu.FileMenus;

internal class FileMenu: IMenu {
	public string Name { get; } = "File";
	public Icons? Icon { get; } = Icons.Folder;
	public IMenuItem?[] MenuItems { get; } = new IMenuItem?[] {
		new SelectIniFile(),
		new WriteSettingsToFile(),
		null,
		new ReloadSettingsFromFile(),
		new ResetSettingsToDefault(),
#if DEBUG
		null,
		new SaveDebugCopy(),
#endif
	};
}
