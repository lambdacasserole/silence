namespace Silence
{
	/// <summary>
	/// Represents the application configuration file.
	/// </summary>
	public class ConfigurationFile : JsonSerializable<ConfigurationFile>
	{
		/// <summary>
		/// The code for the language the application is configured to use.
		/// </summary>
		public string LanguageCode { get; set; }

		/// <summary>
		/// Shortcuts
		/// </summary>
		public int PlayShortcut { get; set; }
		public int RecordShortcut { get; set; }
		public int CaptureShortcut { get; set; }

		/// <summary>
		/// Rectangle capture area
		/// </summary>
		public int CaptureWidth { get; set; }
		public int CaptureHeight { get; set; }
		public string FilePrefix { get; set; }
		public string Offset { get; set; }
		public bool AppendScript { get; set; }

		/// <summary>
		/// The application's base theme color.
		/// </summary>
		public ThemeColor ThemeColor { get; set; }

		/// <summary>
		/// Initializes a new instance of the application configuration file.
		/// </summary>
		public ConfigurationFile()
		{
			LanguageCode = "en";
			ThemeColor = new ThemeColor
			{
				R = 37,
				G = 0, 
				B = 64	
			};
			PlayShortcut = -1;
			RecordShortcut = -1;
			CaptureShortcut = -1;
			CaptureWidth = 100;
			CaptureHeight = 100;
			Offset = "50,50";
			FilePrefix = "img";
			AppendScript = true;
		}
	}
}
