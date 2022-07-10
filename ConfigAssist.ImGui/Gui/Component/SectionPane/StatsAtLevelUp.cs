namespace PrincessRTFM.SSEUncapConfig.Gui.Component.SectionPane;

using ImGuiNET;

internal class StatsAtLevelUp: SectionPaneBase {
	public override string Title { get; init; } = "Stats At Level Up";
	public override string? Description { get; init; } = "This covers how much health/magicka/stamina you get when you select that stat at level up.";
	public override bool NoPadding { get; init; } = true;

	public override void DrawContents() {
		ImGui.Spacing();
		ImGui.SetCursorPosX(ImGui.GetStyle().WindowPadding.X);
		GuiTools.Text("These fields determine how many stat points you get when you select a given stat at level up."
			+ " The left column is checked for the highest match that isn't higher than your new level, and the right column is the number of stat points you receive for it."
			+ " If you have 1=10 and 10=0 in Health for instance, you'll get 10 points when you select health at level up until you hit level ten, then no points."
			+ " You always get nothing for the two stats you didn't select.");
		ImGui.Spacing();
		GuiTools.TripleColumnStatTable(Config.StatAtLevelUp);
	}
}
