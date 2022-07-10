namespace PrincessRTFM.SSEUncapConfig.Gui.Component.SectionPane;

using System;

using ImGuiNET;

using PrincessRTFM.SSEUncapConfig.Core;
using PrincessRTFM.SSEUncapConfig.Gui.Component;

internal class LegendarySkills: SectionPaneBase {
	public override string Title { get; init; } = "Legendary Skills";
	public override void DrawContents() {

		bool keepLevelIsModified = Config.LegendarySkill_LegendaryKeepSkillLevel != Unmodified.LegendarySkill_LegendaryKeepSkillLevel;
		bool keepLevelIsDefault = Config.LegendarySkill_LegendaryKeepSkillLevel == Defaults.LegendarySkill_LegendaryKeepSkillLevel;

		bool hideButtonIsModified = Config.LegendarySkill_HideLegendaryButton != Unmodified.LegendarySkill_HideLegendaryButton;
		bool hideButtonIsDefault = Config.LegendarySkill_HideLegendaryButton == Defaults.LegendarySkill_HideLegendaryButton;

		bool minLevelIsModified = Config.LegendarySkill_SkillLevelEnableLegendary != Unmodified.LegendarySkill_SkillLevelEnableLegendary;
		bool minLevelIsDefault = Config.LegendarySkill_SkillLevelEnableLegendary == Defaults.LegendarySkill_SkillLevelEnableLegendary;

		bool postLevelIsModified = Config.LegendarySkill_SkillLevelAfterLegendary != Unmodified.LegendarySkill_SkillLevelAfterLegendary;
		bool postLevelIsDefault = Config.LegendarySkill_SkillLevelAfterLegendary == Defaults.LegendarySkill_SkillLevelAfterLegendary;

		if (keepLevelIsModified)
			ImGui.PushStyleColor(ImGuiCol.Text, TextColour.Modified);
		else if (keepLevelIsDefault)
			ImGui.PushStyleColor(ImGuiCol.Text, TextColour.MatchesDefault);
		ImGui.Checkbox("Legendary skills keep skill level", ref Config.LegendarySkill_LegendaryKeepSkillLevel);
		if (keepLevelIsModified || keepLevelIsDefault)
			ImGui.PopStyleColor();
		GuiTools.Tooltip("Strongly recommended to disable");

		ImGui.BeginGroup();
		if (hideButtonIsModified)
			ImGui.PushStyleColor(ImGuiCol.Text, TextColour.Modified);
		else if (hideButtonIsDefault)
			ImGui.PushStyleColor(ImGuiCol.Text, TextColour.MatchesDefault);
		ImGui.Checkbox("Hide legendary button", ref Config.LegendarySkill_HideLegendaryButton);
		if (hideButtonIsModified || hideButtonIsDefault)
			ImGui.PopStyleColor();
		ImGui.SameLine();
		GuiTools.Icon(Icons.InfoCircle, TextColour.Shaded);
		ImGui.EndGroup();
		GuiTools.Tooltip("You can still press spacebar even if the button is hidden");

		ImGui.PushItemWidth(Window.ItemWidthNarrow);

		ImGui.BeginGroup();
		if (minLevelIsModified)
			ImGui.PushStyleColor(ImGuiCol.Text, TextColour.Modified);
		else if (minLevelIsDefault)
			ImGui.PushStyleColor(ImGuiCol.Text, TextColour.MatchesDefault);
		if (GuiTools.GetInt("Minimum skill level to make legendary", ref Config.LegendarySkill_SkillLevelEnableLegendary, UncapperConfig.MinimumCap, UncapperConfig.MaximumCap))
			Config.LegendarySkill_SkillLevelAfterLegendary = Math.Min(Config.LegendarySkill_SkillLevelAfterLegendary, Config.LegendarySkill_SkillLevelEnableLegendary - 1);
		if (minLevelIsModified || minLevelIsDefault)
			ImGui.PopStyleColor();
		ImGui.SameLine();
		GuiTools.Icon(Icons.InfoCircle, TextColour.Shaded);
		ImGui.EndGroup();
		if (ImGui.BeginPopupContextItem("minSkillToLegendaryContextMenu")) {
			if (ImGui.Selectable($"Reset to unmodified ({Unmodified.LegendarySkill_SkillLevelEnableLegendary})", false, minLevelIsModified ? ImGuiSelectableFlags.None : ImGuiSelectableFlags.Disabled))
				Config.LegendarySkill_SkillLevelEnableLegendary = Unmodified.LegendarySkill_SkillLevelEnableLegendary;
			if (ImGui.Selectable($"Reset to default ({Defaults.LegendarySkill_SkillLevelEnableLegendary})", false, !minLevelIsDefault ? ImGuiSelectableFlags.None : ImGuiSelectableFlags.Disabled))
				Config.LegendarySkill_SkillLevelEnableLegendary = Defaults.LegendarySkill_SkillLevelEnableLegendary;
			ImGui.Separator();
			if (ImGui.Selectable("Cancel", false))
				ImGui.CloseCurrentPopup();

			ImGui.EndPopup();
		}
		else {
			GuiTools.Tooltip("If this exceeds the maximum skill cap for a skill, that skill cannot be made legendary.");
		}

		ImGui.BeginGroup();
		if (postLevelIsModified)
			ImGui.PushStyleColor(ImGuiCol.Text, TextColour.Modified);
		else if (postLevelIsDefault)
			ImGui.PushStyleColor(ImGuiCol.Text, TextColour.MatchesDefault);
		GuiTools.GetInt("Skill level after being made legendary", ref Config.LegendarySkill_SkillLevelAfterLegendary, 0, Config.LegendarySkill_SkillLevelEnableLegendary - 1);
		if (postLevelIsModified || postLevelIsDefault)
			ImGui.PopStyleColor();
		ImGui.SameLine();
		GuiTools.Icon(Icons.InfoCircle, TextColour.Shaded);
		ImGui.EndGroup();
		if (ImGui.BeginPopupContextItem("postLegendarySkillLevelContextMenu")) {
			if (ImGui.Selectable($"Reset to unmodified ({Unmodified.LegendarySkill_SkillLevelAfterLegendary})", false, postLevelIsModified ? ImGuiSelectableFlags.None : ImGuiSelectableFlags.Disabled))
				Config.LegendarySkill_SkillLevelAfterLegendary = Unmodified.LegendarySkill_SkillLevelAfterLegendary;
			if (ImGui.Selectable($"Reset to default ({Defaults.LegendarySkill_SkillLevelAfterLegendary})", false, !postLevelIsDefault ? ImGuiSelectableFlags.None : ImGuiSelectableFlags.Disabled))
				Config.LegendarySkill_SkillLevelAfterLegendary = Defaults.LegendarySkill_SkillLevelAfterLegendary;
			ImGui.Separator();
			if (ImGui.Selectable("Cancel", false))
				ImGui.CloseCurrentPopup();

			ImGui.EndPopup();
		}
		else {
			GuiTools.Tooltip("If set to 0, skills will be reset to their default level when made legendary.");
		}

		ImGui.PopItemWidth();
	}
}
