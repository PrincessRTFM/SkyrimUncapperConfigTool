namespace PrincessRTFM.SSEUncapConfig.Gui.Component.SectionPane;

using System;
using System.Collections.Generic;

using ImGuiNET;

using PrincessRTFM.SSEUncapConfig.Gui.Component;

internal class ExpSubrate: SectionPaneBase {
	public override string Title { get; init; }
	private readonly string skill;
	private readonly Func<Dictionary<string, Dictionary<int, float>>> getLookupTable;

	public ExpSubrate(string label, string skill, Func<Dictionary<string, Dictionary<int, float>>> retriever) {
		this.Title = label;
		this.skill = skill;
		this.getLookupTable = retriever;
	}

	public override void DrawContents() {
		GuiTools.Text("These fields determine an additional XP multiplier based on a threshold for the relevant value, listed in the tooltip for this section."
			+ " The left column is checked for the highest match that isn't higher than your current level, and the right column is the experience modifier value."
			+ " This lets you set a rough XP curve as you play, so that you or your skills can level faster or slower as you progress.");
		ImGui.Spacing();
		ImGui.Separator();
		ImGui.Spacing();
		GuiTools.MultifieldFloats($"XpSubrate{this.skill}", this.getLookupTable()[this.skill]);
	}
}
