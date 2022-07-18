namespace PrincessRTFM.SSEUncapConfig.Gui.Component.SectionPane;

using System;
using System.Reflection;

using ImGuiNET;

using PrincessRTFM.SSEUncapConfig.Core;
using PrincessRTFM.SSEUncapConfig.Core.Utils;
using PrincessRTFM.SSEUncapConfig.Gui.Component;

internal class ExpRate: SectionPaneBase {
	private readonly (string, FieldInfo)[] linkedFields = new (string, FieldInfo)[LabeledFields.Length];
	public override string Title { get; init; }
	public readonly string InfoLine;

	internal ExpRate(string title, string info, string fieldPrefix) {
		this.Title = title;
		this.InfoLine = info;

		Type uct = typeof(UncapperConfig);
		Log.Debug($"Binding {fieldPrefix} fields for {title} pane");
		for (int i = 0; i < this.linkedFields.Length; i++) {
			(string label, string id) = LabeledFields[i];
			id = $"{fieldPrefix}_{id}";
			FieldInfo? f = uct.GetField(id);
			if (f is null)
				throw new NullReferenceException($"Unable to retrieve field reference for {id}");
			this.linkedFields[i] = (label, f);
		}
	}


	public override void DrawContents() {

		GuiTools.Text(this.InfoLine + " Right click for reset options.");
		ImGui.Separator();

		foreach ((string label, FieldInfo field) in this.linkedFields) {
			float rate = (float)field.GetValue(Config)!;
			float orig = (float)field.GetValue(Unmodified)!;
			float def = (float)field.GetValue(Defaults)!;
			bool isModified = rate != orig;
			bool isDefault = rate == def;

			if (isModified)
				ImGui.PushStyleColor(ImGuiCol.Text, TextColour.Modified);
			else if (isDefault)
				ImGui.PushStyleColor(ImGuiCol.Text, TextColour.MatchesDefault);

			ImGui.SetNextItemWidth(Window.ItemWidthMedium);
			if (GuiTools.GetFloat($"{label}##ExpRate", ref rate, UncapperConfig.MinimumMult, UncapperConfig.MaximumMult))
				field.SetValue(Config, rate);

			if (isModified || isDefault)
				ImGui.PopStyleColor();

			if (ImGui.BeginPopupContextItem($"{label}##ExpRate")) {
				if (ImGui.Selectable($"Reset to unmodified ({orig:F2})", false, isModified ? ImGuiSelectableFlags.None : ImGuiSelectableFlags.Disabled))
					field.SetValue(Config, field.GetValue(Unmodified));
				if (ImGui.Selectable($"Reset to default ({def:F2})", false, !isDefault ? ImGuiSelectableFlags.None : ImGuiSelectableFlags.Disabled))
					field.SetValue(Config, field.GetValue(Defaults));
				ImGui.Separator();
				if (ImGui.Selectable("Cancel", false))
					ImGui.CloseCurrentPopup();

				ImGui.EndPopup();
			}
		}
	}
}
