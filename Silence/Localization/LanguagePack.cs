using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;

namespace Silence.Localization
{
	public class LanguagePack
	{
		private readonly Dictionary<string, Language> _languages;

		public string SelectedLanguageCode { get; private set; }

		public string GetLocalizedString(string key)
		{
			return _languages[SelectedLanguageCode].LocalizedStrings[key];
		}

		public void SelectLanguage(string code)
		{
			if (!_languages.ContainsKey(code))
			{
				throw new ArgumentException("No language with this code was found in the language pack.");
			}
			SelectedLanguageCode = code;
		}

		public LanguagePack(string directory, string pattern = "lang_*.json")
		{
			if (!Directory.Exists(directory))
			{
				throw new DirectoryNotFoundException("The language pack directory could not be found.");
			}

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
