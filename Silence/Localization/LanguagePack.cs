using System;
using System.Collections.Generic;
using System.IO;

namespace Silence.Localization
{
    /// <summary>
    /// Represents a pack of application languages.
    /// </summary>
	public class LanguagePack
	{
        /// <summary>
        /// A map of language codes to languages.
        /// </summary>
		private readonly Dictionary<string, Language> _languages;

        /// <summary>
        /// Gets the code of the selected language.
        /// </summary>
		public string SelectedLanguageCode { get; private set; }

        /// <summary>
        /// Gets a localized string in the selected language based on a key.
        /// </summary>
        /// <param name="key">The key of the string to get.</param>
        /// <returns></returns>
		public string GetLocalizedString(string key)
		{
			return _languages[SelectedLanguageCode].LocalizedStrings[key];
		}

        /// <summary>
        /// Selects a language.
        /// </summary>
        /// <param name="code">The code of the language to select.</param>
		public void SelectLanguage(string code)
		{
            // Language must exist.
			if (!_languages.ContainsKey(code))
			{
				throw new ArgumentException("No language with this code was found in the language pack.");
			}
			SelectedLanguageCode = code;
		}

        /// <summary>
        /// Initializes a new instance of a pack of application languages.
        /// </summary>
        /// <param name="directory">The directory to load language files from.</param>
        /// <param name="pattern">The filename pattern to load.</param>
		public LanguagePack(string directory, string pattern = "lang_*.json")
		{
            // Language pack directory must exist.
			if (!Directory.Exists(directory))
			{
				throw new DirectoryNotFoundException("The language pack directory could not be found.");
			}

            // Load all languages in.
			_languages = new Dictionary<string, Language>();
			var files = new DirectoryInfo(directory).GetFiles(pattern);
			foreach (var file in files)
			{
				var lang = Language.FromFile(file.FullName);
				_languages.Add(lang.Code, lang);
			}
		}
	}
}
