namespace PrincessRTFM.SSEUncapConfig.Gui;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;

using ImGuiNET;

using PrincessRTFM.SSEUncapConfig;
using PrincessRTFM.SSEUncapConfig.Core;
using PrincessRTFM.SSEUncapConfig.Core.Utils;
using PrincessRTFM.SSEUncapConfig.Gui.Component;

internal static class GuiTools {
	public const int IntStepBase = 1;
	public const int IntStepFast = 10;
	public const int IntStepTurbo = 25;
	public const int IntStepHyper = 50;

	public const float FloatStepBase = 0.1f;
	public const float FloatStepFast = 0.5f;
	public const float FloatStepTurbo = 1;
	public const float FloatStepHyper = 2;

	public static bool ShiftHeld => ImGui.GetIO().KeyShift;
	public static bool ControlHeld => ImGui.GetIO().KeyCtrl;
	public static bool AltHeld => ImGui.GetIO().KeyAlt;

	public static void CentreCursorForContent(float contentWidth, float regionWidth = 0)
		=> ImGui.SetCursorPosX(ImGui.GetCursorPosX() + (((regionWidth > 0 ? regionWidth : ImGui.GetColumnWidth() + regionWidth) - contentWidth) / 2) - ImGui.GetScrollX());
	public static void CentreCursorForText(string text, float regionWidth = 0)
		=> CentreCursorForContent(ImGui.CalcTextSize(text).X, regionWidth);

	public static bool Table(string id, ImGuiTableFlags flags, Vector2 outerSize, params (string, string)[] columns) {
		if (!ImGui.BeginTable(id, columns.Length, flags, outerSize))
			return false;

		ImGui.TableNextRow();
		for (int i = 0; i < columns.Length; ++i) {
			(string header, string tooltip) = columns[i];
			bool hasTip = !string.IsNullOrWhiteSpace(tooltip);
			if (hasTip)
				header += " ";

			float textWidth = ImGui.CalcTextSize(header).X;

			if (hasTip)
				textWidth += ImGui.CalcTextSize(Icons.InfoCircle.ToIconString()).X;

			ImGui.TableSetColumnIndex(i);
			CentreCursorForContent(textWidth);

			if (hasTip)
				ImGui.BeginGroup();

			Text(header);

			if (hasTip) {
				ImGui.SameLine(ImGui.CalcTextSize(header).X, 0);
				Text(Icons.InfoCircle.ToIconString(), TextColour.Shaded);
				ImGui.EndGroup();
				Tooltip(tooltip);
			}
		}

		ImGui.TableSetupScrollFreeze(0, 1);

		return true;
	}
	public static bool Table(string id, ImGuiTableFlags flags, params (string, string)[] columns)
		=> Table(id, flags, new Vector2(0), columns);

	public static void Tooltip(string text, Vector4? colour = null) {
		if (!ImGui.IsItemHovered())
			return;
		ImGui.BeginTooltip();
		Text(text, Fonts.ScaledFontSize * 40, colour ?? WindowColour.Text);
		ImGui.EndTooltip();
	}

	public static bool Tab(string label) {
		if (!ImGui.BeginTabItem(label))
			return false;
		ImGui.Spacing();
		ImGui.Spacing();
		return true;
	}

	public static void Text(string text, float? wrapWidth = null, Vector4? colour = null) {
		if (colour.HasValue)
			ImGui.PushStyleColor(ImGuiCol.Text, colour.Value);
		if (wrapWidth.HasValue)
			ImGui.PushTextWrapPos(wrapWidth.Value);
		else
			ImGui.PushTextWrapPos(ImGui.GetContentRegionMax().X - ImGui.GetStyle().WindowPadding.X);
		ImGui.TextUnformatted(text);
		if (colour.HasValue)
			ImGui.PopStyleColor();
		if (wrapWidth.HasValue)
			ImGui.PopTextWrapPos();
	}
	public static void Text(string text, Vector4? colour)
		=> Text(text, null, colour);
	public static void Icon(Icons icon, Vector4? colour = null)
		=> Text(icon.ToIconString(), colour);

	public static void TextCentred(string text, float? wrapWidth = null, Vector4? colour = null) {
		float size = (
			wrapWidth.HasValue
				? ImGui.CalcTextSize(text, false, wrapWidth.Value)
				: ImGui.CalcTextSize(text, false)
			).X;
		float space = ImGui.GetColumnWidth();

		ImGui.SetCursorPosX(ImGui.GetCursorPosX() + ((space - size) / 2) - ImGui.GetScrollX());
		Text(text, wrapWidth, colour);
	}
	public static void TextCentred(string text, Vector4? colour)
		=> TextCentred(text, null, colour);

	public static void FakeGetInt(string label, int value)
		=> GetInt(label, ref value, value, value);
	public static bool GetInt(string label, ref int val, int min, int max) {
		if (min == max) {
			if (ImGui.InputInt(label, ref val, 0, 0, ImGuiInputTextFlags.ReadOnly))
				val = min;
			Tooltip("This value cannot be changed.");
			return false;
		}

		if (ImGui.InputInt(label, ref val, ShiftHeld ? IntStepTurbo : IntStepBase, ShiftHeld ? IntStepHyper : IntStepFast, ImGuiInputTextFlags.CharsDecimal)) {
			val = Math.Min(Math.Max(val, min), max);
			return true;
		}
		if (AltHeld) {
			Tooltip(
				$"Acceptable range: [{min}, {max}]"
				+ $"\n+/-{IntStepBase:D2} no keys"
				+ $"\n+/-{IntStepFast:D2} control"
				+ $"\n+/-{IntStepTurbo:D2} shift"
				+ $"\n+/-{IntStepHyper:D2} control+shift"
			);
		}

		return false;
	}
	//=> ImGui.DragInt(label, ref val, 0.5f, min, max, $"{min} <= [%i] <= {max}", ImGuiSliderFlags.AlwaysClamp);

	public static void FakeGetFloat(string label, float value)
		=> GetFloat(label, ref value, value, value);
	public static bool GetFloat(string label, ref float val, float min, float max) {
		if (min == max) {
			if (ImGui.InputFloat(label, ref val, 0, 0, "%.2f", ImGuiInputTextFlags.ReadOnly))
				val = min;
			Tooltip("This value cannot be changed.");
			return false;
		}

		if (ImGui.InputFloat(label, ref val, ShiftHeld ? FloatStepTurbo : FloatStepBase, ShiftHeld ? FloatStepHyper : FloatStepFast, "%.2f", ImGuiInputTextFlags.CharsDecimal)) {
			val = Math.Min(Math.Max(val, min), max);
			return true;
		}
		if (AltHeld) {
			Tooltip(
				$"Acceptable range: [{min:F2}, {max:F2}]"
				+ $"\n+/-{FloatStepBase:F1} no keys"
				+ $"\n+/-{FloatStepFast:F1} control"
				+ $"\n+/-{FloatStepTurbo:F1} shift"
				+ $"\n+/-{FloatStepHyper:F1} control+shift"
			);
		}

		return false;
	}
	//=> ImGui.DragFloat(label, ref val, 0.05f, min, max, $"{min:F2} <= [%.2f] <= {max:F2}", ImGuiSliderFlags.AlwaysClamp);

	public static void SectionPanes(string baseId, SectionPaneBase?[] sections, ref int selectedPaneIndex, bool borders = true) {
		float sectionTextWidth = sections
			.Where(s => s is not null)
			.Select(s => ImGui.CalcTextSize(s!.Title, true).X)
			.Max();
		float selectorWidth = sectionTextWidth
			+ (ImGui.GetStyle().WindowPadding.X * 2)
			+ (ImGui.GetStyle().FramePadding.X * 2)
			+ (ImGui.GetStyle().CellPadding.X * 2)
			+ Icons.InfoCircle.GetWidth();
		float innerHeight = ImGui.GetContentRegionMax().Y;
		if (selectedPaneIndex >= sections.Length)
			selectedPaneIndex = 0;

		ImGui.SetCursorPos(new Vector2(0, 0));
		ImGui.PushID(baseId);

		if (!borders) {
			ImGui.PushStyleColor(ImGuiCol.Border, new Vector4(0));
		}

		if (ImGui.BeginChild("paneSelector", new Vector2(selectorWidth, innerHeight), true)) {
			for (int i = 0; i < sections.Length; ++i) {
				if (sections[i] is null) {
					ImGui.Spacing();
					ImGui.Spacing();
					ImGui.Separator();
					ImGui.Spacing();
					continue;
				}
				SectionPaneBase section = sections[i]!;

				bool hasTip = !string.IsNullOrWhiteSpace(section.Description);

				if (hasTip || !section.Enabled)
					ImGui.BeginGroup();
				if (ImGui.Selectable(section.Title, selectedPaneIndex == i, section.Enabled ? ImGuiSelectableFlags.None : ImGuiSelectableFlags.Disabled)) {
					selectedPaneIndex = i;
					Log.Debug($"[SectionPane:{baseId}] Active section changed to .{i}: {section.Title}");
					Program.settings.Set($"GUI.section", baseId, selectedPaneIndex.ToString());
				}
				if (!section.Enabled) {
					ImGui.SameLine(selectorWidth - (ImGui.GetStyle().CellPadding.X * 2) - ImGui.GetStyle().ItemSpacing.X - Icons.ExclamationCircle.GetWidth());
					Icon(Icons.ExclamationCircle, TextColour.Warning);
					ImGui.EndGroup();
					Tooltip(section.DisabledReason);
				}
				else if (hasTip) {
					ImGui.SameLine(selectorWidth - (ImGui.GetStyle().CellPadding.X * 2) - ImGui.GetStyle().ItemSpacing.X - Icons.InfoCircle.GetWidth());
					Icon(Icons.InfoCircle, TextColour.Shaded);
					ImGui.EndGroup();
					Tooltip(section.Description!);
				}
			}
			ImGui.EndChild();
		}

		SectionPaneBase selected = sections[selectedPaneIndex]!;

		ImGui.SetCursorPos(new Vector2(ImGui.GetItemRectSize().X, 0));
		if (selected.NoPadding)
			ImGui.PushStyleVar(ImGuiStyleVar.WindowPadding, new Vector2(0));
		if (ImGui.BeginChild("paneContent", new Vector2(0, innerHeight), true)) {
			if (selected.NoPadding)
				ImGui.PopStyleVar();
			if (!borders)
				ImGui.PopStyleColor();

			selected.DrawContents();
			ImGui.EndChild();
		}
		else {
			if (selected.NoPadding)
				ImGui.PopStyleVar();
			if (!borders)
				ImGui.PopStyleColor();
		}

		ImGui.PopID();
	}

	public static void MultifieldFloats(string id, Dictionary<int, float> rows, float minVal = UncapperConfig.MinimumMult, float maxVal = UncapperConfig.MaximumMult, int maxKey = UncapperConfig.MaximumCap, float defaultForNewRow = 1) {

		if (!rows.TryGetValue(1, out float fallback))
			rows[1] = fallback = 1;

		ImGui.Dummy(ImGui.CalcTextSize(Icons.Trash.ToIconString()) + (ImGui.GetStyle().FramePadding * 2));
		ImGui.SameLine();
		ImGui.SetNextItemWidth(Window.ItemWidthNarrow);
		FakeGetInt($"###MultifieldKey_{id}_1", 1);
		ImGui.SameLine();
		ImGui.SetNextItemWidth(Window.ItemWidthMedium);
		if (GetFloat($"###MultifieldValue_{id}_1", ref fallback, minVal, maxVal))
			rows[1] = fallback;

		foreach ((int initialKey, float initialVal) in rows.OrderBy(kv => kv.Key).Skip(1).ToArray()) { // gotta ToArray() it or we'll be modifying concurrently
			int key = initialKey;
			float val = initialVal;

			bool deleting = ImGui.Button($"{Icons.Trash.ToIconString()}###Delete_{id}_{key}");
			ImGui.SameLine();
			ImGui.SetNextItemWidth(Window.ItemWidthNarrow);
			bool changedKey = GetInt($"###MultifieldKey_{id}_{key}", ref key, 2, maxKey);
			ImGui.SameLine();
			ImGui.SetNextItemWidth(Window.ItemWidthMedium);
			bool changedVal = GetFloat($"###MultifieldValue_{id}_{key}", ref val, minVal, maxVal);

			if (deleting) { // Nice and easy, the simplest branch
				rows.Remove(key);
			}
			else if (changedVal) { // Still simple, just update the existing value
				rows[key] = val;
			}
			else if (changedKey) { // This one's trickier, cause we may have to handle collisions as the key is moved _past_ others
				rows.Remove(initialKey);
				if (rows.ContainsKey(key)) { // Yep, it's colliding - gotta skip OVER this one, in the same direction of the edit
					int directionModifier = key > initialKey
						? 1
						: -1;
					do {
						key += directionModifier;
					} while (rows.ContainsKey(key));
					if (key > maxKey || key < 2) { // That's a negative, ghost rider
						rows[initialKey] = val;
					}
					else {
						rows[key] = val;
					}
				}
				else { // No collisions, just move the value
					rows[key] = val;
				}
			}
		}

		int last = rows.OrderByDescending(kv => kv.Key).First().Key;
		if (last == 1)
			last = 0;

		if (ImGui.Button(Icons.PenToSquare.ToIconString())) {
			int keyIncrease = ControlHeld
				? ShiftHeld
					? IntStepHyper // ctrl+shift
					: IntStepFast // ctrl
				: ShiftHeld
					? IntStepTurbo // shift
					: IntStepBase; // none

			int next = last + keyIncrease;
			if (next <= UncapperConfig.MaximumCap)
				rows[next] = defaultForNewRow;
		}
		Tooltip(
			"Add a new row at the bottom. The left value will be:"
				+ $"\n{last}+{IntStepBase:D2}={last + IntStepBase:D2} no keys"
				+ $"\n{last}+{IntStepFast:D2}={last + IntStepFast:D2} control"
				+ $"\n{last}+{IntStepTurbo:D2}={last + IntStepTurbo:D2} shift"
				+ $"\n{last}+{IntStepHyper:D2}={last + IntStepHyper:D2} control+shift"
		);
	}
	// Second verse, same as the first
	public static void MultifieldInts(string id, Dictionary<int, int> rows, int minVal = UncapperConfig.MinimumCap, int maxVal = UncapperConfig.MaximumCap, int maxKey = UncapperConfig.MaximumCap, int defaultForNewRow = 1) {

		if (!rows.TryGetValue(1, out int fallback))
			rows[1] = fallback = 1;

		ImGui.Dummy(ImGui.CalcTextSize(Icons.Trash.ToIconString()) + (ImGui.GetStyle().FramePadding * 2));
		ImGui.PushItemWidth(Window.ItemWidthNarrow);
		ImGui.SameLine();
		FakeGetInt($"###MultifieldKey_{id}_1", 1);
		ImGui.SameLine();
		if (GetInt($"###MultifieldValue_{id}_1", ref fallback, minVal, maxVal))
			rows[1] = fallback;
		ImGui.PopItemWidth();

		foreach ((int initialKey, int initialVal) in rows.OrderBy(kv => kv.Key).Skip(1).ToArray()) { // gotta ToArray() it or we'll be modifying concurrently
			int key = initialKey;
			int val = initialVal;

			bool deleting = ImGui.Button($"{Icons.Trash.ToIconString()}###Delete_{id}_{key}");
			ImGui.PushItemWidth(Window.ItemWidthNarrow);
			ImGui.SameLine();
			bool changedKey = GetInt($"###MultifieldKey_{id}_{key}", ref key, 2, maxKey);
			ImGui.SameLine();
			bool changedVal = GetInt($"###MultifieldValue_{id}_{key}", ref val, minVal, maxVal);
			ImGui.PopItemWidth();

			if (deleting) { // Nice and easy, the simplest branch
				rows.Remove(key);
			}
			else if (changedVal) { // Still simple, just update the existing value
				rows[key] = val;
			}
			else if (changedKey) { // This one's trickier, cause we may have to handle collisions as the key is moved _past_ others
				rows.Remove(initialKey);
				if (rows.ContainsKey(key)) { // Yep, it's colliding - gotta skip OVER this one, in the same direction of the edit
					int directionModifier = key > initialKey
						? 1
						: -1;
					do {
						key += directionModifier;
					} while (rows.ContainsKey(key));
					if (key > maxKey || key < 2) { // That's a negative, ghost rider
						rows[initialKey] = val;
					}
					else {
						rows[key] = val;
					}
				}
				else { // No collisions, just move the value
					rows[key] = val;
				}
			}
		}

		int last = rows.OrderByDescending(kv => kv.Key).First().Key;
		if (last == 1)
			last = 0;

		if (ImGui.Button(Icons.PenToSquare.ToIconString())) {
			int keyIncrease = ControlHeld
				? ShiftHeld
					? IntStepHyper // ctrl+shift
					: IntStepFast // ctrl
				: ShiftHeld
					? IntStepTurbo // shift
					: IntStepBase; // none

			int next = last + keyIncrease;
			if (next <= UncapperConfig.MaximumCap)
				rows[next] = defaultForNewRow;
		}
		Tooltip(
			"Add a new row at the bottom. The left value will be:"
				+ $"\n{last}+{IntStepBase:D2}={last + IntStepBase:D2} no keys"
				+ $"\n{last}+{IntStepFast:D2}={last + IntStepFast:D2} control"
				+ $"\n{last}+{IntStepTurbo:D2}={last + IntStepTurbo:D2} shift"
				+ $"\n{last}+{IntStepHyper:D2}={last + IntStepHyper:D2} control+shift"
		);
	}

	public static void TripleColumnStatTable(Dictionary<string, Dictionary<int, int>> table) {
		string[] labels = table.Keys.ToArray();
		Vector2 space = ImGui.GetContentRegionAvail();
		float sectionWidth = space.X / 3f;
		float topEdge = ImGui.GetCursorPosY();

		// It MIGHT be possible to hack a togglable scroll-sync feature in using GetScrollY/SetScrollY()
		// across the columns, using IsItemHovered() to identify the source. The problem is that it'd be
		// a lot of work with the current setup, and I'm not even sure it'd scroll smoothly.
		// Hm - maybe a toggle on each header to make it the source?

		for (int i = 0; i < labels.Length; ++i) {
			string header = labels[i];
			ImGui.PushStyleVar(ImGuiStyleVar.WindowPadding, new Vector2(0));

			ImGui.SetCursorPos(new Vector2(sectionWidth * i, topEdge));
			if (ImGui.BeginChild($"StatSection{header}", new Vector2(sectionWidth, space.Y), true)) {

				ImGui.Spacing();
				ImGui.Spacing();

				CentreCursorForText(header);
				Text(header);

				ImGui.Spacing();

				ImGui.PopStyleVar();
				if (ImGui.BeginChild($"StatScroller{header}", new Vector2(0, ImGui.GetContentRegionAvail().Y), true)) {
					MultifieldInts($"StatList{header}", table[header], 0, 1000, defaultForNewRow: 10);

					ImGui.EndChild();
				}

				ImGui.EndChild();
			}
			else {
				ImGui.PopStyleVar();
			}
		}
	}
}
