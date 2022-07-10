namespace PrincessRTFM.SSEUncapConfig.Gui.Component.SectionPane.Actions;
using System.Collections.Generic;

using ImGuiNET;

internal class SetAllWeightAtLevelUp: CalculatedActionBase {
	public override string Details { get; } = "Much like the XP subrate multisetter, this tool will set carry weight gained at level up to the same value for all three stats.";
	public override string Title { get; init; } = "Multiset Weight";

	private readonly Dictionary<int, int> values = new();

	public override void DrawSettings() {
		Dictionary<int, int>? source = null;
		GuiTools.Text("Copy from");
		foreach (string stat in Config.CarryWeightAtStatLevelUp.Keys) {
			ImGui.SameLine();
			if (ImGui.SmallButton(stat))
				source = Config.CarryWeightAtStatLevelUp[stat];
		}
		if (source is not null) {
			this.values.Clear();
			foreach ((int k, int v) in source)
				this.values[k] = v;
		}
		ImGui.Spacing();
		ImGui.Spacing();
		GuiTools.MultifieldInts("MultisetWeight", this.values, maxVal: 1000);
	}
	public override void Execute() {
		foreach (string stat in Config.CarryWeightAtStatLevelUp.Keys) {
			Config.CarryWeightAtStatLevelUp[stat].Clear();
			foreach ((int k, int v) in this.values) {
				Config.CarryWeightAtStatLevelUp[stat][k] = v;
			}
		}
	}
}
