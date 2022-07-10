namespace PrincessRTFM.SSEUncapConfig.Reflection;

using System;
using System.Reflection;

internal class BoundField {
	protected readonly object? Target;
	protected readonly FieldInfo Field;

	protected BoundField(object? target, FieldInfo field) {
		this.Target = target;
		this.Field = field;
	}
	public BoundField(object target, string name) : this(target, name, BindingFlags.Public | BindingFlags.Instance) { }
	public BoundField(object target, string name, BindingFlags flags) {
		this.Target = target;
		this.Field = target.GetType().GetField(name, flags) ?? throw new FieldAccessException($"Cannot find field {target.GetType().FullName}.{name}");
	}

	public object? Value {
		get => this.Field.GetValue(this.Target);
		set => this.Field.SetValue(this.Target, value);
	}

	public object? GetValue()
		=> this.Value;
	public void SetValue(object? value)
		=> this.Value = value;
}
