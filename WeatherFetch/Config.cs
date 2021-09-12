using System;
using System.IO;
using System.Text.Json;
using System.Text.Json.Serialization;
using WeatherFetch.Api;

#nullable enable

namespace WeatherFetch
{
	// represents the config file where preferences and keys are stored
	// null values are represented with empty strings in the stored file so that, when reading, null values (empty strings) can be differentiated from missing fields
	// this would then allow detecting when the config file is missing fields, specifically, new fields used by a newer version of the software (ie. new fields will be automatically added to the config file)
	public class Config
	{
		public static readonly string DefaultLocation;

		[JsonPropertyName("wapi-key")]
		public string? WApiKey { get; set; } = null;

		[JsonIgnore]
		public bool NeedsUpgrade = false;

		/// <summary>
		/// Only call this before using <see cref="ReadyData"/>.
		/// </summary>
		/// <returns></returns>
		private bool IsAnyValueNull() => WApiKey is null;

		/// <summary>
		/// Readies data after reading it from the config file. Replaces empty strings with nulls
		/// </summary>
		private void ReadyData() => WApiKey = WApiKey is "" ? null : WApiKey;
		
		/// <summary>
		/// Prepares data for writing to the config file. Replaces nulls with empty strings.
		/// </summary>
		private void PrepareForWrite() => WApiKey ??= "";

		public void StoreConfig(string path)
		{
			PrepareForWrite();
			string json = JsonSerializer.Serialize(this, new JsonSerializerOptions { WriteIndented = true });
			File.WriteAllText(path, json);
		}

		public IWeatherApi GetWeatherApi() => new WeatherApi(this);

		// static

		static Config()
		{
			char slash = Path.DirectorySeparatorChar;
			string configPath = $"{Environment.GetFolderPath(Environment.SpecialFolder.UserProfile)}{slash}.config";

			if (Directory.Exists(configPath))
				// TODO: test this on linux
				Directory.CreateDirectory(configPath).Attributes |= FileAttributes.Hidden;
			
			DefaultLocation = $"{configPath}{slash}weatherfetch.config";
		}

		public static Config LoadConfig(string path)
		{
			Config config;
			
			if (File.Exists(path))
			{
				config = JsonSerializer.Deserialize<Config>(File.ReadAllText(path))!;
				config.NeedsUpgrade = config.IsAnyValueNull(); // any missing fields? mark config to know to later save (upgrade) it
			}
			else
			{
				config = new Config
				{
					NeedsUpgrade = true
				};
			}

			config.ReadyData();

			return config;
		}
	}
}
