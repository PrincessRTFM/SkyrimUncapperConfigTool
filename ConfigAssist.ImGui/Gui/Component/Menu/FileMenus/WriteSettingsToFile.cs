namespace PrincessRTFM.SSEUncapConfig.Gui.Component.Menu.FileMenus;

internal class WriteSettingsToFile: IMenuItem {
	public string Name { get; } = "Write settings to file";
	public Icons? Icon { get; } = Icons.FloppyDisk;
	public bool Enabled => Program.uncapper.HasDiskPath;
	public virtual string? TooltipDisabled { get; } = "No file is currently loaded. Settings cannot be saved without a disk file to write to.";

	public void Trigger() {
		Program.uncapper.Save();
		Program.original.CopyFrom(Program.uncapper);
	}
}
