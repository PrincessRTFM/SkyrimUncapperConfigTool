namespace PrincessRTFM.SSEUncapConfig.Gui.Component.SectionPane.Actions;

using System.Collections.Generic;

using PrincessRTFM.SSEUncapConfig.Core;

internal class PlayerLevelCap: CalculatedActionBase {
	public override string Details { get; } = "This action will set all player XP subrates to 0 at the given level, preventing you from gaining ANY character experience past that point."
		+ " Skill XP subrates are not affected, so you'll continue to level your skills up to their caps, set on the Skill Caps page.";
	public override string Title { get; init; } = "Player Level Cap";

	private int levelCap = UncapperConfig.MinimumCap;

	public override void DrawSettings()
		=> GuiTools.GetInt("Maximum character level", ref this.levelCap, UncapperConfig.MinimumCap, UncapperConfig.MaximumCap);
	public override void Execute() {
		foreach ((string label, string skill) in LabeledFields) {
			Dictionary<int, float> table = Config.LevelSkillExpMultsByPlayerLevel[skill];
			table[this.levelCap] = 0;

			foreach (int k in table.Keys) {
				if (k > this.levelCap)
					table.Remove(k);
			}
		}
	}
}
