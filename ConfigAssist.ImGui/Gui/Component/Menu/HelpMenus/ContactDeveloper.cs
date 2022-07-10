namespace PrincessRTFM.SSEUncapConfig.Gui.Component.Menu.HelpMenus;

internal class ContactDeveloper: IMenuItem {
	public string Name { get; } = "Contact the developer";
	public Icons? Icon { get; } = Icons.Comment;

	public void Trigger() => Program.OpenUrl("https://github.com/PrincessRTFM/SkyrimUncapperConfigTool/discussions/new?category=general");
}
