namespace PrincessRTFM.SSEUncapConfig.Gui.Component;

using ImGuiNET;

using PrincessRTFM.SSEUncapConfig.Core.Utils;

internal interface IMenuItem {
	public string Name { get; }
	public Icons? Icon { get; }
	public virtual bool Enabled => true;

	public virtual string? Tooltip => null;
	public virtual string? TooltipDisabled => "This action is currently unavailable.";

	public virtual string ConfirmationPrompt => string.Empty;

	public bool RequiresConfirmation => !string.IsNullOrWhiteSpace(this.ConfirmationPrompt);
	public string Label => this.Icon.HasValue ? $"{this.Icon.Value.ToIconString()} {this.Name!}" : this.Name!;

	public void Trigger();
	public void Draw() {
		if (ImGui.MenuItem(this.Label, this.Enabled)) {
			Log.Info($"[MENU] Selected menu item: {this.Name}");
			if (this.RequiresConfirmation) {
				Log.Info("[MENU] This action requires confirmation, opening prompt");
				Window.ConfirmationModalPrompt = this.ConfirmationPrompt;
				Window.ConfirmationModalConfirmed = this.Trigger;
			}
			else {
				Log.Info("[MENU] Triggering item action");
				this.Trigger();
			}
		}
		else {
			if (this.Enabled && !string.IsNullOrWhiteSpace(this.Tooltip) && ImGui.IsItemHovered())
				GuiTools.Tooltip(this.Tooltip);
			else if (!this.Enabled && !string.IsNullOrWhiteSpace(this.TooltipDisabled) && ImGui.IsItemHovered())
				GuiTools.Tooltip(this.TooltipDisabled);
		}
	}
}
