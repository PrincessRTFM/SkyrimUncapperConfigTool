namespace PrincessRTFM.SSEUncapConfig.Gui.Component.SectionPane;

using System;
using System.Reflection;

using ImGuiNET;

using PrincessRTFM.SSEUncapConfig.Core;
using PrincessRTFM.SSEUncapConfig.Gui.Component;

internal class SkillCaps: SectionPaneBase {
	private static readonly (string, FieldInfo, FieldInfo)[] linkedFields = new (string, FieldInfo, FieldInfo)[LabeledFields.Length];

	public SkillCaps() {
		Type uct = typeof(UncapperConfig);
		for (int i = 0; i < LabeledFields.Length; i++) {
			(string label, string id) = LabeledFields[i];
			string id1 = $"SkillCaps_{id}";
			string id2 = $"SkillFormulaCaps_{id}";
			FieldInfo? f1 = uct.GetField(id1);
			FieldInfo? f2 = uct.GetField(id2);
			if (f1 is null)
				throw new NullReferenceException($"Unable to retrieve field reference for {id1}");
			if (f2 is null)
				throw new NullReferenceException($"Unable to retrieve field reference for {id2}");
			linkedFields[i] = (label, f1, f2);
		}
	}

	public override string Title { get; init; } = "Skill Caps";
	public override string? Description { get; init; } = "This covers both hard caps (what level the skill can reach) and also formula caps (the highest level that will be used in calculations)";
	public override void DrawContents() {

		if (GuiTools.Table(
			"skillCapLayout",
			ImGuiTableFlags.BordersInnerV | ImGuiTableFlags.SizingStretchSame,
			("Skill Hard Caps", "These options represent hard caps - at this level, the skill will no longer gain experience and cannot increase further."),
			("Skill Formula Caps", "These options represent formula caps - any calculations (magic effects, crafting, etc) will be capped at this skill value, should the actual one be higher.")
		)) {

			foreach ((string label, FieldInfo fieldHardCap, FieldInfo fieldFormCap) in linkedFields) {
				int hardCap = (int)fieldHardCap.GetValue(Config)!;
				int hardCapOrig = (int)fieldHardCap.GetValue(Unmodified)!;
				int hardCapDefault = (int)fieldHardCap.GetValue(Defaults)!;
				bool hardCapIsModified = hardCap != hardCapOrig;
				bool hardCapIsDefault = hardCap == hardCapDefault;

				int formCap = (int)fieldFormCap.GetValue(Config)!;
				int formCapOrig = (int)fieldFormCap.GetValue(Unmodified)!;
				int formCapDefault = (int)fieldFormCap.GetValue(Defaults)!;
				bool formCapIsModified = formCap != formCapOrig;
				bool formCapIsDefault = formCap == formCapDefault;

				ImGui.TableNextColumn();
				if (hardCapIsModified)
					ImGui.PushStyleColor(ImGuiCol.Text, TextColour.Modified);
				else if (hardCapIsDefault)
					ImGui.PushStyleColor(ImGuiCol.Text, TextColour.MatchesDefault);
				if (GuiTools.GetInt($"{label}##HardCap", ref hardCap, UncapperConfig.MinimumCap, UncapperConfig.MaximumCap)) {
					if (hardCap >= UncapperConfig.MinimumCap) {
						fieldHardCap.SetValue(Config, hardCap);
						if (formCap > hardCap) {
							formCap = hardCap;
							fieldFormCap.SetValue(Config, formCap);
						}
					}
				}
				if (hardCapIsModified || hardCapIsDefault)
					ImGui.PopStyleColor();

				if (ImGui.BeginPopupContextItem($"{label}##HardCap")) {
					if (ImGui.Selectable($"Reset to unmodified ({hardCapOrig})", false, hardCapIsModified ? ImGuiSelectableFlags.None : ImGuiSelectableFlags.Disabled))
						fieldHardCap.SetValue(Config, fieldHardCap.GetValue(Unmodified));
					if (ImGui.Selectable($"Reset to default ({hardCapDefault})", false, !hardCapIsDefault ? ImGuiSelectableFlags.None : ImGuiSelectableFlags.Disabled))
						fieldHardCap.SetValue(Config, fieldHardCap.GetValue(Defaults));
					ImGui.Separator();
					if (ImGui.Selectable("Cancel", false))
						ImGui.CloseCurrentPopup();

					ImGui.EndPopup();
				}

				ImGui.TableNextColumn();
				if (formCapIsModified)
					ImGui.PushStyleColor(ImGuiCol.Text, TextColour.Modified);
				else if (formCapIsDefault)
					ImGui.PushStyleColor(ImGuiCol.Text, TextColour.MatchesDefault);
				if (GuiTools.GetInt($"{label}##FormulaCap", ref formCap, UncapperConfig.MinimumCap, hardCap))
					fieldFormCap.SetValue(Config, formCap);
				if (formCapIsModified || formCapIsDefault)
					ImGui.PopStyleColor();

				if (ImGui.BeginPopupContextItem($"{label}##FormulaCap")) {
					bool canResetOrig = formCapIsModified && (formCapOrig <= hardCap);
					bool canResetDefault = !formCapIsDefault && (formCapDefault <= hardCap);
					if (ImGui.Selectable($"Reset to unmodified ({formCapOrig})", false, canResetOrig ? ImGuiSelectableFlags.None : ImGuiSelectableFlags.Disabled))
						fieldFormCap.SetValue(Config, formCapOrig);
					if (ImGui.Selectable($"Reset to default ({formCapDefault})", false, canResetDefault ? ImGuiSelectableFlags.None : ImGuiSelectableFlags.Disabled))
						fieldFormCap.SetValue(Config, formCapDefault);
					if (ImGui.Selectable($"Set to hard cap ({hardCap})", false, formCap != hardCap ? ImGuiSelectableFlags.None : ImGuiSelectableFlags.Disabled))
						fieldFormCap.SetValue(Config, hardCap);
					ImGui.Separator();
					if (ImGui.Selectable("Cancel", false))
						ImGui.CloseCurrentPopup();

					ImGui.EndPopup();
				}

			}

			ImGui.EndTable();
		}

	}
}
