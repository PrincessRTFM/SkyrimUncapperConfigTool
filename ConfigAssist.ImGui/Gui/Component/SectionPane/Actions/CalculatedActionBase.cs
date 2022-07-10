namespace PrincessRTFM.SSEUncapConfig.Gui.Component.SectionPane.Actions;

using System;
using System.Numerics;

using ImGuiNET;

using PrincessRTFM.SSEUncapConfig.Core.Utils;

internal abstract class CalculatedActionBase: SectionPaneBase {
	public abstract string Details { get; }
	public abstract void Execute();

	public Exception? CaughtException {
		get => this.error;
		protected set {
			if (value is not null)
				this.error = value;
		}
	}
	public bool Errored => this.CaughtException is not null;
	private Exception? error;

	public override bool NoPadding { get; init; } = true;

	public override void DrawContents() {
		bool disabled = this.Errored; // to avoid timing issues

		ImGui.Spacing();
		ImGui.Spacing();
		GuiTools.TextCentred(this.Title);
		ImGui.Spacing();

		if (disabled)
			ImGui.BeginDisabled();

		float btnWidth = ImGui.GetContentRegionAvail().X * 0.8f;
		GuiTools.CentreCursorForContent(btnWidth);
		if (ImGui.Button("Execute", new Vector2(btnWidth, 0))) {
			Log.Info($"Executing {this.GetType().Name}");
			try {
				this.Execute();
			}
			catch (Exception ex) {
				Log.Error($"Failure in {this.GetType().Name}.Execute(): {ex}");
				this.CaughtException = ex;
			}
		}

		if (disabled)
			ImGui.EndDisabled();

		ImGui.Spacing();
		ImGui.Spacing();
		ImGui.Separator();

		Vector2 size = ImGui.GetContentRegionAvail();
		float normalPadding = ImGui.GetStyle().WindowPadding.X;
		size.X -= normalPadding;
		ImGui.SetCursorPosX(normalPadding);

		if (ImGui.BeginChild("TextScroller", size, false)) {
			if (disabled) {
				GuiTools.Text("An error occurred. Please report this to the developer.", TextColour.Warning);
				float buttonWidth = ImGui.CalcTextSize("Copy").X + (ImGui.GetStyle().ItemInnerSpacing.X * 2) + ImGui.GetStyle().WindowPadding.X;
				ImGui.SameLine(ImGui.GetWindowContentRegionMax().X - ImGui.GetWindowContentRegionMin().X - buttonWidth);
				if (ImGui.Button("Copy"))
					ImGui.SetClipboardText(this.CaughtException!.ToString());
				ImGui.Spacing();
				ImGui.Spacing();
				GuiTools.Text(this.CaughtException!.ToString(), TextColour.Shaded);
			}
			else {
				GuiTools.Text(this.Details);
				ImGui.Spacing();
				ImGui.Spacing();
				ImGui.Spacing();
				this.DrawSettings();
				ImGui.Spacing();
				ImGui.Spacing();
				ImGui.Spacing();
				ImGui.Spacing();
				ImGui.Spacing();
			}

			ImGui.EndChild();
		}
	}
	public abstract void DrawSettings();
}
