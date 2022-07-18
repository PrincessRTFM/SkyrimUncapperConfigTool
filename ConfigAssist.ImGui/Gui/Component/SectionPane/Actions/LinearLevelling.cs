namespace PrincessRTFM.SSEUncapConfig.Gui.Component.SectionPane.Actions;

using System;
using System.Collections.Generic;
using System.Reflection;

using ImGuiNET;

using PrincessRTFM.SSEUncapConfig.Core;
using PrincessRTFM.SSEUncapConfig.Core.Utils;

internal class LinearLevelling: CalculatedActionBase {
	public override string Details { get; } = "This tool will automatically calculate and set player XP rates based on skill XP, in order to keep player levelling speed the same."
		+ " For example, if you set the levelling rate to 1, then your character will level up at the same rate as you would in vanilla.";
	public override string Title { get; init; } = "Linear Levelling";

	private float slope = 1;
	private bool raw = true;
	private bool byCharLevel = true;
	private static readonly Dictionary<string, FieldInfo> playerFields = new();
	private static readonly Dictionary<string, FieldInfo> skillFields = new();

	static LinearLevelling() {
		Type t = typeof(UncapperConfig);
		foreach ((string _, string skill) in LabeledFields) {
			playerFields[skill] = t.GetField($"LevelSkillExpMults_{skill}")!;
			skillFields[skill] = t.GetField($"SkillExpGainMults_{skill}")!;
		}
	}

	public override void DrawSettings() {

		GuiTools.GetFloat("Levelling rate", ref this.slope, 0.1f, 10);

		ImGui.Checkbox("Include by-character-level subrates?", ref this.byCharLevel);
		GuiTools.Tooltip("This tool modifies the by-base-skill-level subrates for per-skill player-XP gains, to flatten the rate at which your character gains XP by levelling those skills."
			+ " However, you can also adjust the rate at which your character gains XP for levelling skills, based on your character's level. If you check this box, those rates will also be adjusted.");

		ImGui.Checkbox("Include base rates?", ref this.raw);
		GuiTools.Tooltip("This tool overwrites the player XP subrates to even out levelling rates to whatever you set in the player XP rates tab, times whatever the rate above is set to."
			+ "\n\nIf this box is checked, it will ALSO overwrite the base player XP rates as well.");

		GuiTools.Text("If you want to COMPLETELY flatten character XP rates to be the vanilla rate times the chosen multiplier, check both of the above boxes."
			+ " This is probably what you want, so the boxes are checked by default.");

	}
	public override void Execute() {
		foreach ((string label, string skill) in LabeledFields) {

			if (this.raw) {
				Log.Info($"Calculating base rate for {label}");
				float v = (float)skillFields[skill].GetValue(Config)!;
				if (v > 0)
					playerFields[skill].SetValue(Config, MathF.Round(this.slope / v, 2));
			}

			Log.Info($"Calculating subrates for {label}");
			Dictionary<int, float> source = Config.SkillExpGainMultsByBaseSkillLevel[skill];
			Dictionary<int, float> dest = Config.LevelSkillExpMultsByBaseSkillLevel[skill];
			dest.Clear();
			foreach ((int k, float v) in source) {
				if (v > 0)
					dest[k] = MathF.Round(this.slope / v, 2);
			}

			if (this.byCharLevel) {
				source = Config.SkillExpGainMultsByPlayerLevel[skill];
				dest = Config.LevelSkillExpMultsByPlayerLevel[skill];
				dest.Clear();
				foreach ((int k, float v) in source) {
					if (v > 0)
						dest[k] = MathF.Round(this.slope / v, 2);
				}
			}

		}
	}
}
