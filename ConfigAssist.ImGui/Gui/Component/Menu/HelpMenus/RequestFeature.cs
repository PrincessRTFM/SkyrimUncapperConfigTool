namespace PrincessRTFM.SSEUncapConfig.Gui.Component.Menu.HelpMenus;

internal class RequestFeature: IMenuItem {
	public string Name { get; } = "Request a feature";
	public Icons? Icon { get; } = Icons.Lightbulb;

	public void Trigger() => Program.OpenUrl("https://github.com/PrincessRTFM/SkyrimUncapperConfigTool/issues/new?assignees=PrincessRTFM&labels=enhancement&template=feature_request.md&title=");
}
