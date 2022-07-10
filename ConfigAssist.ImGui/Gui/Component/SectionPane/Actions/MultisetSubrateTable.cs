namespace PrincessRTFM.SSEUncapConfig.Gui.Component.SectionPane.Actions;

using System;
using System.Collections.Generic;

using ImGuiNET;

internal class MultisetSubrateTable: CalculatedActionBase {
	public override string Details { get; } = "This tool allows you to set a table to be applied to ALL of a particular set of subrates,"
		+ "so you don't need to click each tab and make the same exact changes for eighteen different skills.";
	public override string Title { get; init; } = "Multiset Subrates";

	private readonly Dictionary<int, float> multipliers = new();
	private int selected = 0;

	public override void DrawSettings() {

		float halfway = (ImGui.GetWindowContentRegionMax().X - ImGui.GetWindowContentRegionMin().X) / 2;
		ImGui.RadioButton("Skill XP - Player Level", ref this.selected, 0);
		ImGui.SameLine(halfway);
		ImGui.RadioButton("Player XP - Player Level", ref this.selected, 2);
		ImGui.RadioButton("Skill XP - Base Skill Level", ref this.selected, 1);
		ImGui.SameLine(halfway);
		ImGui.RadioButton("Player XP - Base Skill Level", ref this.selected, 3);

		ImGui.Spacing();
		ImGui.Spacing();
		ImGui.Spacing();

		if (ImGui.Button("Clear table"))
			this.multipliers.Clear();

		ImGui.Spacing();
		ImGui.Spacing();

		GuiTools.MultifieldFloats("MultisetEntries", this.multipliers);
	}
	public override void Execute() {
		Dictionary<string, Dictionary<int, float>> table = this.selected switch {
			0 => Config.SkillExpGainMultsByPlayerLevel,
			1 => Config.SkillExpGainMultsByBaseSkillLevel,
			2 => Config.LevelSkillExpMultsByPlayerLevel,
			3 => Config.LevelSkillExpMultsByBaseSkillLevel,
			_ => throw new NotImplementedException($"Unknown target index {this.selected}"), // unpossible
		};
		foreach ((string label, string skill) in LabeledFields) {
			table[skill].Clear();
			foreach ((int k, float v) in this.multipliers)
				table[skill][k] = v;
		}
	}
}
