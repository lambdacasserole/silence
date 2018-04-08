using System.Collections.Generic;

namespace Silence.Localization
{
    /// <summary>
    /// Represents a language capable of being displayed by the application UI.
    /// </summary>
	public class Language : JsonSerializable<Language>
	{
        /// <summary>
        /// Gets or sets the code for the language.
        /// </summary>
		public string Code { get; set; }

        /// <summary>
        /// Gets or sets the name of the language.
        /// </summary>
		public string Name { get; set; }

        /// <summary>
        /// Gets or sets the dictionary of localized strings under this language.
        /// </summary>
		public Dictionary<string, string> LocalizedStrings { get; set; }

        /// <summary>
        /// Initializes a new instance of a language capable of being displayed by the application UI.
        /// </summary>
		public Language()
		{
			LocalizedStrings = new Dictionary<string, string>();
		}
	}
}
