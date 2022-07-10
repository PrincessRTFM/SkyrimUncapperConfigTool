namespace PrincessRTFM.SSEUncapConfig.Gui.Component.Menu.HelpMenus;

internal class ShareFile: IMenuItem {
	public string Name { get; } = "Share your Uncapper config";
	public Icons? Icon { get; } = Icons.FileUpload;

	public void Trigger() => Program.OpenUrl("https://github.com/PrincessRTFM/SkyrimUncapperConfigTool/discussions/new?category=show-and-tell");
}
