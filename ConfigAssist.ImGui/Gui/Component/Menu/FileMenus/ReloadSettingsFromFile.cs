namespace PrincessRTFM.SSEUncapConfig.Gui.Component.Menu.FileMenus;

using PrincessRTFM.SSEUncapConfig.Core.Utils;

internal class ReloadSettingsFromFile: IMenuItem {
	public string Name { get; } = "Reload settings from file";
	public Icons? Icon { get; } = Icons.ArrowsRotate;
	public bool Enabled => Program.uncapper.HasExistingFile;
	public virtual string? TooltipDisabled { get; } = "No existing disk file was found to reload settings from.";

	public string ConfirmationPrompt { get; } = "Are you sure you want to discard all changes?";

	public void Trigger() {
		Log.Info("Reloading all values from disk");
		Program.uncapper.Reload();
		Program.original.CopyFrom(Program.uncapper);
	}
}
