namespace PrincessRTFM.SSEUncapConfig.Gui.Component.SectionPane.Actions;

using System.Collections.Generic;

using ImGuiNET;

internal class SetAllStatsAtLevelUp: CalculatedActionBase {
	public override string Details { get; } = "Much like the XP subrate multisetter, this tool will set all three of the stat gains at level up to the same value.";
	public override string Title { get; init; } = "Multiset Stats";

	private readonly Dictionary<int, int> values = new();

	public override void DrawSettings() {
		Dictionary<int, int>? source = null;
		GuiTools.Text("Copy from");
		foreach (string stat in Config.StatAtLevelUp.Keys) {
			ImGui.SameLine();
			if (ImGui.SmallButton(stat))
				source = Config.StatAtLevelUp[stat];
		}
		if (source is not null) {
			this.values.Clear();
			foreach ((int k, int v) in source)
				this.values[k] = v;
		}
		ImGui.Spacing();
		ImGui.Spacing();
		GuiTools.MultifieldInts("MultisetStats", this.values, maxVal: 1000);
	}
	public override void Execute() {
		foreach (string stat in Config.StatAtLevelUp.Keys) {
			Config.StatAtLevelUp[stat].Clear();
			foreach ((int k, int v) in this.values) {
				Config.StatAtLevelUp[stat][k] = v;
			}
		}
	}
}
