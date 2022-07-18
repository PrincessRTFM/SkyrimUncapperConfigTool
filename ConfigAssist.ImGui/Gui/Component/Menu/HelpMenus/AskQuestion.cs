namespace PrincessRTFM.SSEUncapConfig.Gui.Component.Menu.HelpMenus;

internal class AskQuestion: IMenuItem {
	public string Name { get; } = "Ask a question";
	public Icons? Icon { get; } = Icons.Bullhorn;

	public void Trigger() => Program.OpenUrl("https://github.com/PrincessRTFM/SkyrimUncapperConfigTool/discussions/new?category=q-a");
}
