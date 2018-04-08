namespace Silence
{
	/// <summary>
	/// Represents the application configuration file.
	/// </summary>
	internal class ConfigurationFile : JsonSerializable<ConfigurationFile>
	{
		/// <summary>
		/// The code for the language the application is configured to use.
		/// </summary>
		public string LanguageCode { get; set; }

		/// <summary>
		/// The application's base theme color.
		/// </summary>
		public ThemeColor ThemeColor { get; set; }

		/// <summary>
		/// Initializes a new instance of the application configuration file.
		/// </summary>
		public ConfigurationFile()
		{
            // UI Defaults.
			LanguageCode = "en";
			ThemeColor = new ThemeColor
			{
				R = 37,
				G = 0, 
				B = 64	
			};
		}
	}
}
