namespace PrincessRTFM.SSEUncapConfig.Gui.Component.Menu.FileMenus;

using PrincessRTFM.SSEUncapConfig.Core.Utils;

internal class ResetSettingsToDefault: IMenuItem {
	public string Name { get; } = "Reset to default";
	public Icons? Icon { get; } = Icons.X;

	public string ConfirmationPrompt { get; } = "Are you sure you want to reset EVERYTHING to defaults?";

	public void Trigger() {
		Log.Info("Reloading all values from defaults");
		Program.uncapper.CopyFrom(Program.defaults);
		Program.original.CopyFrom(Program.uncapper);
	}
}
