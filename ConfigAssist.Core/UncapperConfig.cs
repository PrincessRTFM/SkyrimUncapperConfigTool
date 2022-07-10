namespace PrincessRTFM.SSEUncapConfig.Core;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

using PrincessRTFM.SSEUncapConfig.Core.INI;
using PrincessRTFM.SSEUncapConfig.Core.Utils;

public class UncapperConfig {
	public const int MinimumCap = 100;
	public const int MaximumCap = 999;
	public const int DefaultCap = 999;

	public const float MinimumMult = 0f;
	public const float MaximumMult = 20f;
	public const float DefaultMult = 1f;

	internal INIFile? ini { get; set; }
	private INIEntry[] fields { get; }

	public bool HasDiskPath => this.ini is not null;
	public bool HasExistingFile => this.HasDiskPath && this.ini!.Exists;

	#region SkillCaps
	public int SkillCaps_OneHanded = DefaultCap;
	public int SkillCaps_TwoHanded = DefaultCap;
	public int SkillCaps_Marksman = DefaultCap;
	public int SkillCaps_Block = DefaultCap;
	public int SkillCaps_Smithing = DefaultCap;
	public int SkillCaps_HeavyArmor = DefaultCap;
	public int SkillCaps_LightArmor = DefaultCap;
	public int SkillCaps_Pickpocket = DefaultCap;
	public int SkillCaps_LockPicking = DefaultCap;
	public int SkillCaps_Sneak = DefaultCap;
	public int SkillCaps_Alchemy = DefaultCap;
	public int SkillCaps_SpeechCraft = DefaultCap;
	public int SkillCaps_Alteration = DefaultCap;
	public int SkillCaps_Conjuration = DefaultCap;
	public int SkillCaps_Destruction = DefaultCap;
	public int SkillCaps_Illusion = DefaultCap;
	public int SkillCaps_Restoration = DefaultCap;
	public int SkillCaps_Enchanting = DefaultCap;
	#endregion

	#region SkillFormulaCaps
	public int SkillFormulaCaps_OneHanded = DefaultCap;
	public int SkillFormulaCaps_TwoHanded = DefaultCap;
	public int SkillFormulaCaps_Marksman = DefaultCap;
	public int SkillFormulaCaps_Block = DefaultCap;
	public int SkillFormulaCaps_Smithing = DefaultCap;
	public int SkillFormulaCaps_HeavyArmor = DefaultCap;
	public int SkillFormulaCaps_LightArmor = DefaultCap;
	public int SkillFormulaCaps_Pickpocket = DefaultCap;
	public int SkillFormulaCaps_LockPicking = DefaultCap;
	public int SkillFormulaCaps_Sneak = DefaultCap;
	public int SkillFormulaCaps_Alchemy = DefaultCap;
	public int SkillFormulaCaps_SpeechCraft = DefaultCap;
	public int SkillFormulaCaps_Alteration = DefaultCap;
	public int SkillFormulaCaps_Conjuration = DefaultCap;
	public int SkillFormulaCaps_Destruction = DefaultCap;
	public int SkillFormulaCaps_Illusion = DefaultCap;
	public int SkillFormulaCaps_Restoration = DefaultCap;
	public int SkillFormulaCaps_Enchanting = DefaultCap;
	#endregion

	#region SkillExpGainMults
	public float SkillExpGainMults_OneHanded = DefaultMult;
	public float SkillExpGainMults_TwoHanded = DefaultMult;
	public float SkillExpGainMults_Marksman = DefaultMult;
	public float SkillExpGainMults_Block = DefaultMult;
	public float SkillExpGainMults_Smithing = DefaultMult;
	public float SkillExpGainMults_HeavyArmor = DefaultMult;
	public float SkillExpGainMults_LightArmor = DefaultMult;
	public float SkillExpGainMults_Pickpocket = DefaultMult;
	public float SkillExpGainMults_LockPicking = DefaultMult;
	public float SkillExpGainMults_Sneak = DefaultMult;
	public float SkillExpGainMults_Alchemy = DefaultMult;
	public float SkillExpGainMults_SpeechCraft = DefaultMult;
	public float SkillExpGainMults_Alteration = DefaultMult;
	public float SkillExpGainMults_Conjuration = DefaultMult;
	public float SkillExpGainMults_Destruction = DefaultMult;
	public float SkillExpGainMults_Illusion = DefaultMult;
	public float SkillExpGainMults_Restoration = DefaultMult;
	public float SkillExpGainMults_Enchanting = DefaultMult;

	public Dictionary<string, Dictionary<int, float>> SkillExpGainMultsByPlayerLevel = new() {
		{ "OneHanded", new() },
		{ "TwoHanded", new() },
		{ "Marksman", new() },
		{ "Block", new() },
		{ "Smithing", new() },
		{ "HeavyArmor", new() },
		{ "LightArmor", new() },
		{ "Pickpocket", new() },
		{ "LockPicking", new() },
		{ "Sneak", new() },
		{ "Alchemy", new() },
		{ "SpeechCraft", new() },
		{ "Alteration", new() },
		{ "Conjuration", new() },
		{ "Destruction", new() },
		{ "Illusion", new() },
		{ "Restoration", new() },
		{ "Enchanting", new() },
	};
	public Dictionary<string, Dictionary<int, float>> SkillExpGainMultsByBaseSkillLevel = new() {
		{ "OneHanded", new() },
		{ "TwoHanded", new() },
		{ "Marksman", new() },
		{ "Block", new() },
		{ "Smithing", new() },
		{ "HeavyArmor", new() },
		{ "LightArmor", new() },
		{ "Pickpocket", new() },
		{ "LockPicking", new() },
		{ "Sneak", new() },
		{ "Alchemy", new() },
		{ "SpeechCraft", new() },
		{ "Alteration", new() },
		{ "Conjuration", new() },
		{ "Destruction", new() },
		{ "Illusion", new() },
		{ "Restoration", new() },
		{ "Enchanting", new() },
	};
	#endregion

	#region LevelSkillExpMults
	public float LevelSkillExpMults_OneHanded = DefaultMult;
	public float LevelSkillExpMults_TwoHanded = DefaultMult;
	public float LevelSkillExpMults_Marksman = DefaultMult;
	public float LevelSkillExpMults_Block = DefaultMult;
	public float LevelSkillExpMults_Smithing = DefaultMult;
	public float LevelSkillExpMults_HeavyArmor = DefaultMult;
	public float LevelSkillExpMults_LightArmor = DefaultMult;
	public float LevelSkillExpMults_Pickpocket = DefaultMult;
	public float LevelSkillExpMults_LockPicking = DefaultMult;
	public float LevelSkillExpMults_Sneak = DefaultMult;
	public float LevelSkillExpMults_Alchemy = DefaultMult;
	public float LevelSkillExpMults_SpeechCraft = DefaultMult;
	public float LevelSkillExpMults_Alteration = DefaultMult;
	public float LevelSkillExpMults_Conjuration = DefaultMult;
	public float LevelSkillExpMults_Destruction = DefaultMult;
	public float LevelSkillExpMults_Illusion = DefaultMult;
	public float LevelSkillExpMults_Restoration = DefaultMult;
	public float LevelSkillExpMults_Enchanting = DefaultMult;

	public Dictionary<string, Dictionary<int, float>> LevelSkillExpMultsByPlayerLevel = new() {
		{ "OneHanded", new() },
		{ "TwoHanded", new() },
		{ "Marksman", new() },
		{ "Block", new() },
		{ "Smithing", new() },
		{ "HeavyArmor", new() },
		{ "LightArmor", new() },
		{ "Pickpocket", new() },
		{ "LockPicking", new() },
		{ "Sneak", new() },
		{ "Alchemy", new() },
		{ "SpeechCraft", new() },
		{ "Alteration", new() },
		{ "Conjuration", new() },
		{ "Destruction", new() },
		{ "Illusion", new() },
		{ "Restoration", new() },
		{ "Enchanting", new() },
	};
	public Dictionary<string, Dictionary<int, float>> LevelSkillExpMultsByBaseSkillLevel = new() {
		{ "OneHanded", new() },
		{ "TwoHanded", new() },
		{ "Marksman", new() },
		{ "Block", new() },
		{ "Smithing", new() },
		{ "HeavyArmor", new() },
		{ "LightArmor", new() },
		{ "Pickpocket", new() },
		{ "LockPicking", new() },
		{ "Sneak", new() },
		{ "Alchemy", new() },
		{ "SpeechCraft", new() },
		{ "Alteration", new() },
		{ "Conjuration", new() },
		{ "Destruction", new() },
		{ "Illusion", new() },
		{ "Restoration", new() },
		{ "Enchanting", new() },
	};
	#endregion

	#region Stats at level up
	public Dictionary<int, int> PerksAtLevelUp = new();

	public Dictionary<string, Dictionary<int, int>> StatAtLevelUp = new() {
		{ "Health", new() },
		{ "Magicka", new() },
		{ "Stamina", new() },
	};

	public Dictionary<string, Dictionary<int, int>> CarryWeightAtStatLevelUp = new() {
		{ "Health", new() },
		{ "Magicka", new() },
		{ "Stamina", new() },
	};
	#endregion

	#region LegendarySkill
	public bool LegendarySkill_LegendaryKeepSkillLevel = false;
	public bool LegendarySkill_HideLegendaryButton = false;
	public int LegendarySkill_SkillLevelEnableLegendary = 100;
	public int LegendarySkill_SkillLevelAfterLegendary = 0;
	#endregion

	public UncapperConfig() {
		this.fields = this.GetType()
			.GetFields(BindingFlags.Public | BindingFlags.Instance)
			.Where(f => f.Name.Contains('_'))
			.Select(f => new INIEntry(this, f))
			.ToArray();
	}

	public UncapperConfig(string filepath) : this() {
		this.LoadFrom(filepath);
	}

	public bool Reload() {
		if (this.ini is null) {
			Log.Warn("Cannot load settings, INI file not available");
			return false;
		}
		if (!this.ini.Exists) {
			Log.Warn("Cannot load settings, INI file does not exist");
			return false;
		}
		Dictionary<INIEntry, string> loadedRawValues = new();
		Dictionary<string, Dictionary<int, float>> loadedSkillExpGainMultsByPlayerLevel = new();
		Dictionary<string, Dictionary<int, float>> loadedSkillExpGainMultsByBaseSkillLevel = new();
		Dictionary<string, Dictionary<int, float>> loadedLevelSkillExpMultsByPlayerLevel = new();
		Dictionary<string, Dictionary<int, float>> loadedLevelSkillExpMultsByBaseSkillLevel = new();
		Dictionary<int, int> loadedPerksAtLevelUp; // not nested, so it's directly assigned below
		Dictionary<string, Dictionary<int, int>> loadedStatAtLevelUp = new();
		Dictionary<string, Dictionary<int, int>> loadedCarryWeightAtStatLevelUp = new();

		Log.Info("Loading settings from {0}", this.ini.Filepath);
		try {

			// The simplest: reflected fields can be automatically filled in, nice and easy
			Log.Debug("Loading simple settings");
			foreach (INIEntry setting in this.fields) {
				loadedRawValues[setting] = this.ini.Get(setting.Section, setting.Key, string.Format("{0}", setting.Default).ToLower());
			}
			// Next step: (the singular) non-nested dictionary loading for perk points at level up
			loadedPerksAtLevelUp = this.loadDynamicInts("PerksAtLevelUp");
			// A little more complicated: nested dictionary loading for the different stats at level up
			// Update: well, it _was_ complicated, until I saw that I was basically copy-pasting the same code for each bit, and made it a separate function
			foreach (string stat in this.StatAtLevelUp.Keys) {
				loadedStatAtLevelUp[stat] = this.loadDynamicInts($"{stat}AtLevelUp");
				loadedCarryWeightAtStatLevelUp[stat] = this.loadDynamicInts($"CarryWeightAt{stat}LevelUp");
			}
			// The most "complicated" part: XP rate multipliers
			// See above note for why "complicated" is in quotes lol
			foreach (string skill in this.SkillExpGainMultsByPlayerLevel.Keys) {
				loadedSkillExpGainMultsByPlayerLevel[skill] = this.loadDynamicFloats($@"SkillExpGainMults\CharacterLevel\{skill}");
				loadedSkillExpGainMultsByBaseSkillLevel[skill] = this.loadDynamicFloats($@"SkillExpGainMults\BaseSkillLevel\{skill}");
				loadedLevelSkillExpMultsByPlayerLevel[skill] = this.loadDynamicFloats($@"LevelSkillExpMults\CharacterLevel\{skill}");
				loadedLevelSkillExpMultsByBaseSkillLevel[skill] = this.loadDynamicFloats($@"LevelSkillExpMults\BaseSkillLevel\{skill}");
			}

			Log.Debug("File loaded, registering all settings");
			foreach (KeyValuePair<INIEntry, string> kv in loadedRawValues) {
				kv.Key.Set(kv.Value);
			}
			this.PerksAtLevelUp = loadedPerksAtLevelUp;
			this.StatAtLevelUp = loadedStatAtLevelUp;
			this.CarryWeightAtStatLevelUp = loadedCarryWeightAtStatLevelUp;
			this.SkillExpGainMultsByPlayerLevel = loadedSkillExpGainMultsByPlayerLevel;
			this.SkillExpGainMultsByBaseSkillLevel = loadedSkillExpGainMultsByBaseSkillLevel;
			this.LevelSkillExpMultsByPlayerLevel = loadedLevelSkillExpMultsByPlayerLevel;
			this.LevelSkillExpMultsByBaseSkillLevel = loadedLevelSkillExpMultsByBaseSkillLevel;
			Log.Info("Load complete");
		}
		catch (Exception ex) {
			Log.Error("Failed to load settings: {0}", ex.Message);
			Log.Debug(ex.StackTrace ?? "No stack trace available");
		}

		return true;
	}
	public bool LoadFrom(string path) {
		this.ini = new(path);
		return this.Reload();
	}

	private Dictionary<int, int> loadDynamicInts(string section) {
		Log.Debug($"Loading dynamic section: {section}");
		string[] keys = this.ini!.Keys(section);
		Log.Debug($"{keys.Length} entr{(keys.Length == 1 ? "y" : "ies")} in section");
		Dictionary<int, int> loaded = new();
		foreach (string key in keys) {
			if (!int.TryParse(key, out int level))
				continue;
			string value = this.ini.Get(section, key);
			if (string.IsNullOrWhiteSpace(value))
				continue;
			if (!int.TryParse(value, out int number))
				continue;
			loaded[level] = number;
		}
		return loaded;
	}
	private static void saveDynamicInts(INIFile ini, string section, Dictionary<int, int> table) {
		Log.Debug($"Writing dynamic section: {section}");
		ini.Clear(section);
		foreach (int k in table.Keys) {
			ini.Set(section, k.ToString(), table[k].ToString());
		}
	}
	private Dictionary<int, float> loadDynamicFloats(string section) {
		Log.Debug($"Loading dynamic section: {section}");
		string[] keys = this.ini!.Keys(section);
		Log.Debug($"{keys.Length} entr{(keys.Length == 1 ? "y" : "ies")} in section");
		Dictionary<int, float> loaded = new();
		foreach (string key in keys) {
			if (!int.TryParse(key, out int level))
				continue;
			string value = this.ini.Get(section, key);
			if (string.IsNullOrWhiteSpace(value))
				continue;
			if (!float.TryParse(value, out float number))
				continue;
			loaded[level] = number;
		}
		return loaded;
	}
	private static void saveDynamicFloats(INIFile ini, string section, Dictionary<int, float> table) {
		Log.Debug($"Writing dynamic section: {section}");
		ini.Clear(section);
		foreach (int k in table.Keys) {
			ini.Set(section, k.ToString(), table[k].ToString());
		}
	}

	public bool SaveTo(INIFile file) {
		Log.Info("Writing settings to {0}", file.Filepath);

		try {
			foreach (INIEntry setting in this.fields) {
				file.Set(setting.Section, setting.Key, string.Format("{0}", setting.Value).ToLower());
			}

			foreach (string skill in this.SkillExpGainMultsByPlayerLevel.Keys) {
				saveDynamicFloats(file, $@"SkillExpGainMults\CharacterLevel\{skill}", this.SkillExpGainMultsByPlayerLevel[skill]);
				saveDynamicFloats(file, $@"SkillExpGainMults\BaseSkillLevel\{skill}", this.SkillExpGainMultsByBaseSkillLevel[skill]);
				saveDynamicFloats(file, $@"LevelSkillExpMults\CharacterLevel\{skill}", this.LevelSkillExpMultsByPlayerLevel[skill]);
				saveDynamicFloats(file, $@"LevelSkillExpMults\BaseSkillLevel\{skill}", this.LevelSkillExpMultsByBaseSkillLevel[skill]);
			}

			saveDynamicInts(file, "PerksAtLevelUp", this.PerksAtLevelUp);

			foreach (string stat in this.StatAtLevelUp.Keys) {
				saveDynamicInts(file, $"{stat}AtLevelUp", this.StatAtLevelUp[stat]);
				saveDynamicInts(file, $"CarryWeightAt{stat}LevelUp", this.CarryWeightAtStatLevelUp[stat]);
			}

			file.Save();
			Log.Info("Save complete");
		}
		catch (Exception ex) {
			Log.Error("Failed to save settings: {0}", ex.Message);
			Log.Debug(ex.StackTrace ?? "No stack trace available");
		}

		return true;
	}
	public bool SaveCopy(string path) {
		if (this.ini is null) {
			Log.Warn("Cannot save settings, origin INI file not available");
			return false;
		}

		INIFile copy = new(path);
		//copy.CopyFrom(this.ini); // to copy comments (do we even bother?)

		return this.SaveTo(copy);
	}
	public bool Save() {
		if (this.ini is null) {
			Log.Warn("Cannot save settings, INI file not available");
			return false;
		}

		return this.SaveTo(this.ini);
	}

	public void CopyFrom(UncapperConfig source) {
		Dictionary<string, Dictionary<int, float>> skillExpGainMultsByPlayerLevel = new();
		Dictionary<string, Dictionary<int, float>> skillExpGainMultsByBaseSkillLevel = new();
		Dictionary<string, Dictionary<int, float>> levelSkillExpMultsByPlayerLevel = new();
		Dictionary<string, Dictionary<int, float>> levelSkillExpMultsByBaseSkillLevel = new();
		Dictionary<int, int> perksAtLevelUp = new();
		Dictionary<string, Dictionary<int, int>> statAtLevelUp = new();
		Dictionary<string, Dictionary<int, int>> carryWeightAtStatLevelUp = new();

		for (int i = 0; i < this.fields.Length; ++i) {
			this.fields[i].Value = source.fields[i].Value;
		}

		foreach (string skill in this.SkillExpGainMultsByPlayerLevel.Keys) {
			skillExpGainMultsByPlayerLevel[skill] = new();
			skillExpGainMultsByBaseSkillLevel[skill] = new();
			levelSkillExpMultsByPlayerLevel[skill] = new();
			levelSkillExpMultsByBaseSkillLevel[skill] = new();

			foreach ((int k, float v) in source.SkillExpGainMultsByPlayerLevel[skill])
				skillExpGainMultsByPlayerLevel[skill][k] = v;
			foreach ((int k, float v) in source.SkillExpGainMultsByPlayerLevel[skill])
				skillExpGainMultsByBaseSkillLevel[skill][k] = v;
			foreach ((int k, float v) in source.SkillExpGainMultsByPlayerLevel[skill])
				levelSkillExpMultsByPlayerLevel[skill][k] = v;
			foreach ((int k, float v) in source.SkillExpGainMultsByPlayerLevel[skill])
				levelSkillExpMultsByBaseSkillLevel[skill][k] = v;
		}

		foreach ((int k, int v) in source.PerksAtLevelUp)
			perksAtLevelUp[k] = v;

		foreach (string stat in this.StatAtLevelUp.Keys) {
			statAtLevelUp[stat] = new();
			carryWeightAtStatLevelUp[stat] = new();
			foreach ((int k, int v) in source.StatAtLevelUp[stat])
				statAtLevelUp[stat][k] = v;
			foreach ((int k, int v) in source.CarryWeightAtStatLevelUp[stat])
				carryWeightAtStatLevelUp[stat][k] = v;
		}

		this.SkillExpGainMultsByBaseSkillLevel = skillExpGainMultsByBaseSkillLevel;
		this.SkillExpGainMultsByPlayerLevel = skillExpGainMultsByPlayerLevel;
		this.LevelSkillExpMultsByBaseSkillLevel = levelSkillExpMultsByBaseSkillLevel;
		this.LevelSkillExpMultsByPlayerLevel = levelSkillExpMultsByPlayerLevel;
		this.PerksAtLevelUp = perksAtLevelUp;
		this.StatAtLevelUp = statAtLevelUp;
		this.CarryWeightAtStatLevelUp = carryWeightAtStatLevelUp;

	}
}
