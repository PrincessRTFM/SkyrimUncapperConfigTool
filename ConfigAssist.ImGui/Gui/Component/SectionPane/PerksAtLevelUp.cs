namespace PrincessRTFM.SSEUncapConfig.Gui.Component.SectionPane;

using ImGuiNET;

internal class PerksAtLevelUp: SectionPaneBase {
	public override string Title { get; init; } = "Perks At Level Up";

	public override void DrawContents() {
		GuiTools.Text("These fields determine how many perk points you get when you level up."
			+ " The left column is checked for the highest match that isn't higher than your new level, and the right column is the number of perk points you receive."
			+ " If you have 1=10 and 10=0 for instance, you'll get 10 points when you level up until you hit level ten, then no points.");
		ImGui.Spacing();
		ImGui.Separator();
		ImGui.Spacing();
		GuiTools.MultifieldInts("PerksAtLevelUp", Config.PerksAtLevelUp, 0);
	}
}
