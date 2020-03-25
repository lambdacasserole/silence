using System.Drawing;

namespace Silence
{
	/// <summary>
	/// Represents a base theme color for the application.
	/// </summary>
	public class ThemeColor
	{
		/// <summary>
		/// The color's red component.
		/// </summary>
		public int R { get; set; }

		/// <summary>
		/// The color's green component.
		/// </summary>
		public int G { get; set; }

		/// <summary>
		/// The color's blue component.
		/// </summary>
		public int B { get; set; }

		/// <summary>
		/// Initialises a new instance of a theme color.
		/// </summary>
		/// <param name="r">The red component.</param>
		/// <param name="g">The green component.</param>
		/// <param name="b">The blue component.</param>
		public ThemeColor(int r, int g, int b)
		{
			R = r;
			G = g;
			B = b;
		}

		/// <summary>
		/// Initialises a new instance of a theme color.
		/// </summary>
		public ThemeColor() : this(0, 0, 0)
		{
		}

		/// <summary>
		/// Returns this theme color as a Color object.
		/// </summary>
		/// <param name="brightness">The brightness modifier to apply to the returned Color object.</param>
		/// <returns></returns>
		public Color ToColor(int brightness = 0)
		{
			// Add brightness modifier, limit to valid range.
			var r = R + brightness;
			r = r > 255 ? 255 : (r < 0 ? 0 : r);
			var g = G + brightness;
			g = g > 255 ? 255 : (g < 0 ? 0 : g);
			var b = B + brightness;
			b = b > 255 ? 255 : (b < 0 ? 0 : b);

			return Color.FromArgb(255, r, g, b);
		}
	}
}
