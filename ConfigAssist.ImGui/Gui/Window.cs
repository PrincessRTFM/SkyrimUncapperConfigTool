namespace PrincessRTFM.SSEUncapConfig;

using System;
using System.Linq;
using System.Numerics;

using ImGuiNET;

using PrincessRTFM.SSEUncapConfig.Core.Utils;
using PrincessRTFM.SSEUncapConfig.Gui;
using PrincessRTFM.SSEUncapConfig.Gui.Component;
using PrincessRTFM.SSEUncapConfig.Gui.Component.Menu;
using PrincessRTFM.SSEUncapConfig.Gui.Component.SectionPane;
using PrincessRTFM.SSEUncapConfig.Interop;

internal static class Window {
	public const string Title = "SSE Skill Uncapper Configuration Tool";
	public const string ConfirmationPopupTitle = "Confirmation Required";
	public const int Width = 1100;
	public const int Height = 650;

	public static float ItemWidthNarrow => Fonts.ScaledFontSize * 7;
	public static float ItemWidthMedium => ItemWidthNarrow * 2;
	public static float ItemWidthBig => ItemWidthNarrow * 3;

	private static readonly SectionPaneBase?[] sections = new SectionPaneBase?[] {
		new SkillCaps(),
		new ExpRate(
			"Skill XP Rates",
			"These settings control the amount of experience a particular skill receives.",
			"SkillExpGainMults"
		),
		new ExpSubrateContainer(
			"> Subrates: Player Level###SkillXpSubratePlayerLevel",
			() => Program.uncapper.SkillExpGainMultsByPlayerLevel,
			"These settings offer additional modifiers to skill XP gains, based on your character level."
		),
		new ExpSubrateContainer(
			"> Subrates: Skill Level###SkillXpSubrateSkillLevel",
			() => Program.uncapper.SkillExpGainMultsByBaseSkillLevel,
			"These settings offer additional modifiers to skill XP gains, based on that skill's BASE level."
		),
		new ExpRate(
			"Player XP Rate",
			"These settings control the amount of player experience you receive when a skill levels up.",
			"LevelSkillExpMults"
		),
		new ExpSubrateContainer(
			"> Subrates: Player Level###PlayerXpSubratePlayerLevel",
			() => Program.uncapper.LevelSkillExpMultsByPlayerLevel,
			"These settings offer additional modifiers to the amount of player XP you get for levelling a given skill, based on your character level."
		),
		new ExpSubrateContainer(
			"> Subrates: Skill Level###PlayerXpSubrateSkillLevel",
			() => Program.uncapper.LevelSkillExpMultsByBaseSkillLevel,
			"These settings offer additional modifiers to the amount of player XP you get for levelling a given skill, based on that skill's BASE level."
		),
		new PerksAtLevelUp(),
		new StatsAtLevelUp(),
		new CarryWeightAtLevelUp(),
		new LegendarySkills(),
		null,
		new CalculatedActions(),
	};
	private static readonly IMenu[] menus = new IMenu[] {
		new FileMenu(),
		new HelpMenu(),
	};

	public static bool Locked => OpenFileDialog.IsSelecting;

	internal static int selectedPaneIndex = 0;

	public static string? ConfirmationModalPrompt;
	public static Action? ConfirmationModalConfirmed;

	private static void renderDoubleCentredTextUi(Vector4 colour, params string[] lines) {
		ImDrawListPtr draw = ImGui.GetForegroundDrawList();
		ImFontPtr font = ImGui.GetFont();
		float sizeMult = 2f;
		float fontSize = font.FontSize * sizeMult;

		Vector2[] sizes = lines.Select(t => ImGui.CalcTextSize(t) * sizeMult).ToArray();
		Vector2 region = ImGui.GetContentRegionAvail();
		Vector2 spacing = ImGui.GetStyle().ItemSpacing;
		Vector2 drawSize = new(
			sizes.Select(v => v.X).Max(),// + (spacing.X * 2),
			sizes.Select(v => v.Y).Aggregate(0f, (a, b) => a + spacing.Y + b)// + (spacing.Y * 2)
		);
		float topEdge = (region.Y - drawSize.Y) / 2;
		float vOffset = 0;
		uint imColour = ImGui.GetColorU32(colour);

		for (int i = 0; i < lines.Length; ++i) {
			string line = lines[i];
			Vector2 size = sizes[i];
			Vector2 pos = new((region.X - size.X) / 2, topEdge + vOffset);
			draw.AddText(font, fontSize, pos, imColour, line);
			vOffset += size.Y + spacing.Y;
		}
	}

	private static void renderConfirmationPopup() {
		bool visible = true;
		if (ConfirmationModalConfirmed is null || string.IsNullOrEmpty(ConfirmationModalPrompt))
			return;

		ImGui.OpenPopup(ConfirmationPopupTitle);

		ImGui.SetNextWindowPos(ImGui.GetMainViewport().GetCenter(), ImGuiCond.Appearing, new Vector2(0.5f));
		ImGui.SetNextWindowSize(new Vector2(Fonts.ScaledFontSize * 20, 0), ImGuiCond.Appearing);
		if (ImGui.BeginPopupModal(ConfirmationPopupTitle, ref visible, ImGuiWindowFlags.NoMove | ImGuiWindowFlags.NoResize | ImGuiWindowFlags.NoDecoration)) {
			float btnWidth = (ImGui.GetWindowContentRegionMax().X / 2) - ImGui.GetStyle().ItemSpacing.X;
			GuiTools.Text(ConfirmationModalPrompt ?? "Are you sure?", ImGui.GetWindowContentRegionMax().X);
			ImGui.Spacing();
			ImGui.Spacing();
			ImGui.Spacing();
			ImGui.Spacing();
			ImGui.Separator();
			ImGui.Spacing();
			ImGui.Spacing();
			ImGui.PushStyleColor(ImGuiCol.Text, TextColour.Dangerous);
			if (ImGui.Button("Yes", new Vector2(btnWidth, 0))) {
				Log.Info("Action confirmed, invoking callback");
				ConfirmationModalConfirmed?.Invoke();
				ConfirmationModalConfirmed = null;
				ConfirmationModalPrompt = null;
				ImGui.CloseCurrentPopup();
			}
			ImGui.PopStyleColor();
			ImGui.SetItemDefaultFocus();
			ImGui.SameLine();
			ImGui.PushStyleColor(ImGuiCol.Text, TextColour.Safe);
			if (ImGui.Button("No", new Vector2(btnWidth, 0))) {
				Log.Info("Action rejected");
				ConfirmationModalConfirmed = null;
				ConfirmationModalPrompt = null;
				ImGui.CloseCurrentPopup();
			}
			ImGui.PopStyleColor();
			ImGui.EndPopup();
		}
	}

	public static void Draw() {
		if (selectedPaneIndex < 0 || selectedPaneIndex > sections.Length)
			selectedPaneIndex = 0;

		ImGui.PushFont(Fonts.Normal);

		ImGui.SetNextWindowSize(new Vector2(Width, Height), ImGuiCond.Always);
		ImGui.SetNextWindowPos(new Vector2(0), ImGuiCond.Always);
		ImGui.PushStyleVar(ImGuiStyleVar.WindowPadding, new Vector2(0));
		ImGuiWindowFlags flags = ImGuiWindowFlags.NoDecoration | ImGuiWindowFlags.NoMove | ImGuiWindowFlags.NoScrollbar | ImGuiWindowFlags.NoScrollWithMouse;
		if (!Locked)
			flags |= ImGuiWindowFlags.MenuBar;
		if (!ImGui.Begin(Title, flags))
			return;
		ImGui.PopStyleVar();

		if (Locked) {
			renderDoubleCentredTextUi(
				TextColour.Warning,
				Icons.ExclamationCircle.ToIconString(),
				"UI LOCKED",
				"Select a file to edit"
			);
			ImGui.End();
			return;
		}

		if (ImGui.BeginMenuBar()) {
			foreach (IMenu menu in menus)
				menu.Draw();
			ImGui.EndMenuBar();
		}

		if (!Program.uncapper.HasDiskPath) {
			renderDoubleCentredTextUi(
				TextColour.Warning,
				Icons.ExclamationTriangle.ToIconString(),
				"No INI file is currently loaded",
				"Select a file to edit using the menu"
			);
			ImGui.End();
			return;
		}

		string intro = "Select a configuration section to edit on the left."
			//+ " All numeric drag inputs can be double-clicked or control-clicked to provide a textual input instead."
			+ " Hold control while clicking +/- buttons for numeric inputs to move in larger steps. Hold shift to go even faster."
			+ " Hold alt over numeric inputs for valid ranges and click steps."
		;
		float introWrap = Width - (ImGui.GetStyle().WindowPadding.X * 2);
		float padding = 0;
		ImGui.BeginGroup();
		ImGui.Spacing();
		ImGui.Indent(ImGui.GetStyle().WindowPadding.X);
		GuiTools.Text(intro, introWrap);
		ImGui.EndGroup();
		padding += ImGui.GetItemRectSize().Y - ImGui.GetStyle().ItemSpacing.Y;

		float top = ImGui.GetWindowContentRegionMin().Y + padding + 10;
		float innerHeight = ImGui.GetWindowContentRegionMax().Y - top;

		ImGui.PushStyleColor(ImGuiCol.Border, TextColour.Shaded);

		ImGui.SetCursorPos(new Vector2(0, top));
		ImGui.PushStyleVar(ImGuiStyleVar.WindowPadding, new Vector2(0));
		if (ImGui.BeginChild("CoreContainer", new Vector2(ImGui.GetWindowContentRegionMax().X, innerHeight), false)) {
			ImGui.PopStyleVar();
			GuiTools.SectionPanes("Core", sections, ref selectedPaneIndex, true);
			ImGui.EndChild();
		}
		else {
			ImGui.PopStyleVar();
		}

		ImGui.PopStyleColor();

		ImGui.End();

		renderConfirmationPopup();

		ImGui.PopFont();
	}

}
