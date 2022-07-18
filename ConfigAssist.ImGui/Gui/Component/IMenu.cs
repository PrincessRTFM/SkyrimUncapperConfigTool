namespace PrincessRTFM.SSEUncapConfig.Gui.Component;

using ImGuiNET;

internal interface IMenu {
	public string Name { get; }
	public Icons? Icon { get; }
	public virtual bool Enabled => true;

	public string Label => this.Icon.HasValue ? $"{this.Icon.Value.ToIconString()} {this.Name}" : this.Name;

	public IMenuItem?[] MenuItems { get; }

	public void Draw() {
		if (ImGui.BeginMenu(this.Label, this.Enabled && this.MenuItems.Length > 0)) {
			foreach (IMenuItem? item in this.MenuItems) {
				if (item is null) {
					ImGui.Separator();
					ImGui.Spacing();
				}
				else {
					item.Draw();
				}
			}
			ImGui.EndMenu();
		}
	}
}
