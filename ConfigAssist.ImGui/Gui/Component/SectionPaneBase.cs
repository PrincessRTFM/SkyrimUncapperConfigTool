namespace PrincessRTFM.SSEUncapConfig.Gui.Component;

using System.Linq;

using ImGuiNET;

using PrincessRTFM.SSEUncapConfig.Core;

internal abstract class SectionPaneBase {
	protected static UncapperConfig Config => Program.uncapper;
	protected static UncapperConfig Unmodified => Program.original;
	protected static UncapperConfig Defaults => Program.defaults;
	protected static readonly (string, string)[] LabeledFields = new (string, string)[] {
		("One Handed", "OneHanded"),
		("Two Handed", "TwoHanded"),
		("Archery", "Marksman"),
		("Light Armour", "LightArmor"),
		("Heavy Armour", "HeavyArmor"),
		("Block", "Block"),
		("Pickpocket", "Pickpocket"),
		("Lockpicking", "LockPicking"),
		("Sneak", "Sneak"),
		("Alteration", "Alteration"),
		("Conjuration", "Conjuration"),
		("Destruction", "Destruction"),
		("Illusion", "Illusion"),
		("Restoration", "Restoration"),
		("Smithing", "Smithing"),
		("Alchemy", "Alchemy"),
		("Enchanting", "Enchanting"),
		("Speech", "SpeechCraft"),
	};
	private static float lblTextWidth = 0;
	protected static float LabelTextWidth {
		get {
			if (lblTextWidth == 0)
				lblTextWidth = LabeledFields.Select(p => ImGui.CalcTextSize(p.Item1).X).Max();
			return lblTextWidth;
		}
	}

	public abstract string Title { get; init; }
	public virtual string? Description { get; init; } = null;
	public virtual bool Enabled { get; init; } = true;
	public virtual string DisabledReason { get; init; } = "This section is currently unavailable.";
	public virtual bool NoPadding { get; init; } = false;
	public abstract void DrawContents();
}
