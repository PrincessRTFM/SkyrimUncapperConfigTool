namespace PrincessRTFM.SSEUncapConfig.Gui.Component.Menu.FileMenus;

internal class SelectIniFile: IMenuItem {
	public string Name { get; } = "Select SkyrimUncapper.ini file...";
	public Icons? Icon { get; } = Icons.FolderOpen;

	public void Trigger() => Program.OpenUncapperFile();
}
