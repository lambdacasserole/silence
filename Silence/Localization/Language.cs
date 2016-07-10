using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Silence.Localization
{
	public class Language : JsonSerializable<Language>
	{
		public string Code { get; set; }

		public string Name { get; set; }

		public Dictionary<string, string> LocalizedStrings { get; set; }

		public Language()
		{
			LocalizedStrings = new Dictionary<string, string>();
		}
	}
}
