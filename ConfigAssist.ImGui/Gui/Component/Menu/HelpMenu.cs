namespace PrincessRTFM.SSEUncapConfig.Gui.Component.Menu;

using PrincessRTFM.SSEUncapConfig.Gui.Component.Menu.HelpMenus;

internal class HelpMenu: IMenu {
	public string Name { get; } = "Help";
	public Icons? Icon { get; } = Icons.QuestionCircle;
	public IMenuItem?[] MenuItems { get; } = new IMenuItem?[] {
		new BugReporter(),
		new RequestFeature(),
		new AskQuestion(),
		null,
		new ContactDeveloper(),
		null,
		new ShareFile(),
	};
}
