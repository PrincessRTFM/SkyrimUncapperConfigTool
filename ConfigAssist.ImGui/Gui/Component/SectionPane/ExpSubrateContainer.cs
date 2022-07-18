namespace PrincessRTFM.SSEUncapConfig.Gui.Component.SectionPane;

using System;
using System.Collections.Generic;
using System.Linq;

using PrincessRTFM.SSEUncapConfig.Gui.Component;

internal class ExpSubrateContainer: SectionPaneBase {
	public override string Title { get; init; }

	private int active = 0;
	private readonly SectionPaneBase[] subsections;
	public readonly string Id;

	public override bool NoPadding => true;

	public ExpSubrateContainer(string title, Func<Dictionary<string, Dictionary<int, float>>> retriever, string? help = null) {
		this.Title = title;
		this.Description = help;
		this.subsections = LabeledFields
			.Select(p => new ExpSubrate(p.Item1, p.Item2, retriever))
			.ToArray();

		if (title.Contains("###"))
			this.Id = title[(title.IndexOf("###") + 3)..];
		else if (title.Contains("##"))
			this.Id = title.Replace("##", "");
		else
			this.Id = title;

		if (!int.TryParse(Program.settings.Get("GUI.section", this.Id, "0"), out this.active))
			this.active = 0;
	}

	public override void DrawContents()
		=> GuiTools.SectionPanes(this.Id, this.subsections, ref this.active);
}
