namespace PrincessRTFM.SSEUncapConfig.Gui.Component.SectionPane;
using System;
using System.IO;
using System.Linq;
using System.Reflection;

using PrincessRTFM.SSEUncapConfig.Core.Utils;
using PrincessRTFM.SSEUncapConfig.Gui.Component.SectionPane.Actions;

internal class CalculatedActions: SectionPaneBase {
	public override string Title { get; init; } = "Utilities";

	public override bool Enabled => this.actions.Length > 0;
	public override string DisabledReason { get; init; } = "No automatic calculations are currently available.";

	public override bool NoPadding { get; init; } = true;

	private readonly CalculatedActionBase[] actions;
	private int active = 0;

	public CalculatedActions() {
		Log.Info("Loading auto-calc operations");
		Assembly assembly = typeof(CalculatedActionBase).Assembly;

		this.actions = assembly
			.GetTypes()
			.Where(t => t.IsSubclassOf(typeof(CalculatedActionBase)))
			.Select(t => Activator.CreateInstance(t))
			.Cast<CalculatedActionBase>()
			.ToArray();

		if (this.actions.Length > 0)
			Log.Debug($"Loaded {this.actions.Length}: {string.Join(", ", this.actions.Select(o => o.GetType().Name))}");
		else
			Log.Debug($"No operations found in {Path.GetFileName(assembly.Location)}");

		if (!int.TryParse(Program.settings.Get("GUI.section", "Operations", "0"), out this.active))
			this.active = 0;
	}

	public override void DrawContents()
		=> GuiTools.SectionPanes("Operations", this.actions, ref this.active);
}
