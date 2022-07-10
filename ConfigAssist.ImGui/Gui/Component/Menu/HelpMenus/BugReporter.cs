namespace PrincessRTFM.SSEUncapConfig.Gui.Component.Menu.HelpMenus;

internal class BugReporter: IMenuItem {
	public string Name { get; } = "Report a bug";
	public Icons? Icon { get; } = Icons.Bug;

	public void Trigger() => Program.OpenUrl("https://github.com/PrincessRTFM/SkyrimUncapperConfigTool/issues/new?assignees=PrincessRTFM&labels=bug&template=bug_report.md&title=");

}
