namespace PrincessRTFM.SSEUncapConfig.Core.Utils;

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

public class INIFile {
	private readonly Dictionary<string, Dictionary<string, string>> ini = new(StringComparer.InvariantCultureIgnoreCase);
	public string Filepath { get; init; }
	public bool Exists => this.Filepath.Length > 0 && File.Exists(this.Filepath);

	public INIFile(string file) {
		this.Filepath = file;

		if (File.Exists(file))
			this.Load();
	}

	public void Load() {
		string txt = File.ReadAllText(this.Filepath);

		Dictionary<string, string> currentSection = new(StringComparer.OrdinalIgnoreCase);

		this.ini[string.Empty] = currentSection;

		IEnumerable<(int, string)> lines = txt.Split("\n", StringSplitOptions.RemoveEmptyEntries)
			.Select((t, i) => (idx: i, text: t.Trim()));

		foreach ((int i, string line) in lines) {

			if (string.IsNullOrWhiteSpace(line) || line[..1] is ";" or "#") {
				currentSection.Add(";" + i.ToString(), line);
				continue;
			}

			if (line.StartsWith("[") && line.EndsWith("]")) {
				currentSection = new Dictionary<string, string>(StringComparer.InvariantCultureIgnoreCase);
				this.ini[line[1..^1]] = currentSection;
				continue;
			}

			int idx = line.IndexOf("=");
			if (idx == -1)
				currentSection[line] = "";
			else
				currentSection[line[..idx].Trim()] = line[(idx + 1)..].Trim();
		}
	}

	public void Save() {
		StringBuilder? sb = new();
		foreach (KeyValuePair<string, Dictionary<string, string>> section in this.ini) {
			if (string.IsNullOrWhiteSpace(section.Key))
				continue;

			sb.AppendFormat("[{0}]", section.Key);
			sb.AppendLine();

			foreach (KeyValuePair<string, string> keyValue in section.Value) {
				if (keyValue.Key.StartsWith(";"))
					sb.Append(keyValue.Value);
				else
					sb.AppendFormat("{0} = {1}", keyValue.Key, keyValue.Value);
				sb.AppendLine();
			}

			if (!endWithCRLF(sb))
				sb.AppendLine();
		}

		File.WriteAllText(this.Filepath, sb.ToString());
	}

	public string Get(string section, string key, string def = "") {
		if (!this.ini.ContainsKey(section))
			return def;

		if (!this.ini[section].ContainsKey(key))
			return def;

		return this.ini[section][key];
	}

	public void Set(string section, string key, string value) {
		if (!this.ini.TryGetValue(section, out Dictionary<string, string>? currentSection)) {
			currentSection = new Dictionary<string, string>();
			this.ini.Add(section, currentSection);
		}

		currentSection[key] = value;
	}

	public void Unset(string section, string key) {
		if (this.ini.TryGetValue(section, out Dictionary<string, string>? table))
			table.Remove(key);
	}

	public void Clear(string section)
		=> this.ini.Remove(section);

	public string[] Keys(string section) => this.ini.ContainsKey(section)
		? this.ini[section]
			.Keys
			.Where(k => !string.IsNullOrWhiteSpace(k) && !k.StartsWith(";"))
			.ToArray()
		: Array.Empty<string>();

	public string[] Sections() => this.ini.Keys.Where(t => !string.IsNullOrWhiteSpace(t)).ToArray();

	public void CopyFrom(INIFile source) {
		this.ini.Clear();
		foreach ((string section, Dictionary<string, string> entries) in source.ini) {
			this.ini[section] = new();
			foreach ((string key, string value) in entries) {
				this.ini[section][key] = value;
			}
		}
	}

	private static bool endWithCRLF(StringBuilder sb) => sb[^2] == '\r' && sb[^1] == '\n';
}
