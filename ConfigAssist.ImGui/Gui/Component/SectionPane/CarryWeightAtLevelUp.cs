namespace PrincessRTFM.SSEUncapConfig.Gui.Component.SectionPane;

using ImGuiNET;

internal class CarryWeightAtLevelUp: SectionPaneBase {
	public override string Title { get; init; } = "Carry Weight At Level Up";
	public override string? Description { get; init; } = "This covers how much your carry weight limit increases at level up, based on which stat you increase.";
	public override bool NoPadding { get; init; } = true;

	public override void DrawContents() {
		ImGui.Spacing();
		ImGui.SetCursorPosX(ImGui.GetStyle().WindowPadding.X);
		GuiTools.Text("These fields determine how much carry weight capacity you get when you select a given stat at level up."
			+ " The left column is checked for the highest match that isn't higher than your new level, and the right column is the number of carry weight points you receive for it."
			+ " If you have 1=10 and 10=0 in Health for instance, you'll get 10 points of carry weight when you select health at level up until you hit level ten, then no points."
			+ " Only the stat you choose at the level up screen is applied.");
		ImGui.Spacing();
		GuiTools.TripleColumnStatTable(Config.CarryWeightAtStatLevelUp);
	}
}
