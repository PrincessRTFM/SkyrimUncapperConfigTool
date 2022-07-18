namespace PrincessRTFM.SSEUncapConfig.Core.INI;

using System;
using System.Collections.Generic;
using System.Reflection;

using PrincessRTFM.SSEUncapConfig.Core.Utils;

internal class INIEntry {
	private static readonly Dictionary<Type, MethodInfo?> parsers = new();

	public string Section { get; }
	public string Key { get; }
	public dynamic Default { get; }
	public Type Type { get; }
	public UncapperConfig Config { get; }

	private readonly FieldInfo field;

	public INIEntry(UncapperConfig holder, FieldInfo field) {
		this.Config = holder;
		this.field = field;

		string[] parts = field.Name.Split("_");
		string prefix = field.FieldType.Name[..1].ToLower();
		// Only problem is that `float` is actually `Single` so...
		if (field.FieldType == typeof(float))
			prefix = "f";

		this.Section = string.Join("\\", parts[0..^1]);
		this.Key = prefix + parts[^1];
		this.Default = field.GetValue(holder)!;
		this.Type = field.FieldType;
	}

	public void Set(dynamic value) {
		if (!value.GetType().IsAssignableTo(this.Type)) {
			if (!parsers.TryGetValue(this.Type, out MethodInfo? parser)) {
				parser = this.Type.GetMethod(
					"Parse",
					BindingFlags.Static | BindingFlags.Public,
					null,
					new Type[] { typeof(string) },
					null
				);
				parsers.Add(this.Type, parser);
			}
			if (parser is null)
				throw new NullReferenceException($"Unable to retrieve parser method for {this.Type.Name} (cannot find {this.Type.Name}.Parse(string) method)");
			value = parser.Invoke(null, new object?[] { string.Format("{0}", value) })!;
		}
		this.field.SetValue(this.Config, value);
		//Log.Debug($"Set {this.field.Name} to {value}");
	}
	public bool TrySet(dynamic value) {
		try {
			this.Set(value);
			return true;
		}
		catch (Exception ex) {
			Log.Error("Cannot assign {0} {1} to {2}: {3}", value.GetType(), value, this.Type, ex);
			return false;
		}
	}
	public void Reset() => this.TrySet(this.Default);
	public dynamic Value {
		get => this.field.GetValue(this.Config)!;
		set => this.field.SetValue(this.Config, value);
	}
}
