using System.IO;
using Newtonsoft.Json;

namespace Silence
{
	/// <summary>
	/// Represents an object that can be written to and read from a file as JSON.
	/// </summary>
	/// <typeparam name="T">The object type.</typeparam>
	public abstract class JsonSerializable<T>
	{
		/// <summary>
		/// Saves this object to a file as JSON.
		/// </summary>
		/// <param name="filename">The filename to write to.</param>
		public void Save(string filename)
		{
			var writer = new StreamWriter(filename);
			writer.Write(JsonConvert.SerializeObject(this, Formatting.Indented));
			writer.Flush();
			writer.Close();
		}

		/// <summary>
		/// Reads an object of this type from a JSON file.
		/// </summary>
		/// <param name="filename">The filename to read from.</param>
		/// <returns></returns>
		public static T FromFile(string filename)
		{
			var reader = new StreamReader(filename);
			var obj = JsonConvert.DeserializeObject<T>(reader.ReadToEnd());
			reader.Close();
			return obj;
		}
	}
}
