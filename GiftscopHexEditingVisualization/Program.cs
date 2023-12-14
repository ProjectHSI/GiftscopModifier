using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Drawing;

namespace GiftscopHexEditingVisualization
{
	internal enum DataType
	{
		Unknown,
		SubDataHeader,
		Room,
		XPosition,
		YPosition,
		ZPosition,
		NameLength,
		Name,
		Generation,
		VersionLength,
		Version
	}

	internal class TerminalTrueColor
	{
		public static bool TerminalSupportsTrueColor { get; private set; } = false;

		public enum ColorType
		{
			Foreground,
			Background
		}

		public static void WriteToTerminal(string text)
		{
			WriteToTerminal(text, Color.White, Color.Black, ConsoleColor.White, ConsoleColor.Black);
		}

		public static void WriteToTerminal(
			string text,
			Color preferredColor,
			ConsoleColor fallbackColor,
			ColorType colorType)
		{
			if (colorType == ColorType.Foreground)
			{
				WriteToTerminal(text, preferredColor, Color.Black, fallbackColor, ConsoleColor.Black);
			}
			else if (colorType == ColorType.Background)
			{
				WriteToTerminal(text, Color.White, preferredColor, ConsoleColor.White, fallbackColor);
			}
		}

		public static void WriteToTerminal(
			string text,
			Color preferredForegroundColor,
			Color preferredBackgroundColor,
			ConsoleColor fallbackForegroundColor,
			ConsoleColor fallbackBackgroundColor)
		{
			if (TerminalSupportsTrueColor)
			{
				const string ESCCode = "\x1b";
				const string Delimiter = ";";
				const string ForegroundSetCode = "38";
				const string BackgroundSetCode = "48";
				const string ResetCode = "m";
				const string FinalResetString = $"{ESCCode}[0m";

				string foregroundSet =
					$"{ESCCode}" +
					$"[" +
					$"{ForegroundSetCode}" +
					Delimiter +
					$"2" +
					Delimiter +
					$"{preferredForegroundColor.R}" +
					Delimiter +
					$"{preferredForegroundColor.G}" +
					Delimiter +
					$"{preferredForegroundColor.B}" +
					$"{ResetCode}";

				string backgroundSet =
					$"{ESCCode}" +
					$"[" +
					$"{BackgroundSetCode}" +
					Delimiter +
					$"2" +
					Delimiter +
					$"{preferredBackgroundColor.R}" +
					Delimiter +
					$"{preferredBackgroundColor.G}" +
					Delimiter +
					$"{preferredBackgroundColor.B}" +
					$"{ResetCode}";

				Console.Write($"{foregroundSet}{backgroundSet}{text}{FinalResetString}");
				//Console.Write($"{foregroundSet} {backgroundSet} {text} {FinalResetString}\n".Replace("\x1b", "ESC_"));
				//Console.Write($"{ESCCode}[{ForegroundSetCode};2;255;100;0m{ESCCode}[{BackgroundSetCode};2;0;155;255m{text}\x1b[0m\n");
			}
		}

		public static void WriteLineToTerminal(string text)
		{
			WriteLineToTerminal(text, Color.White, Color.Black, ConsoleColor.White, ConsoleColor.Black);
		}

		public static void WriteLineToTerminal(
			string text,
			Color preferredColor,
			ConsoleColor fallbackColor,
			ColorType colorType)
		{
			if (colorType == ColorType.Foreground)
			{
				WriteLineToTerminal(text, preferredColor, Color.Black, fallbackColor, ConsoleColor.Black);
			}
			else if (colorType == ColorType.Background)
			{
				WriteLineToTerminal(text, Color.White, preferredColor, ConsoleColor.White, fallbackColor);
			}
		}

		public static void WriteLineToTerminal(
			string text,
			Color preferredForegroundColor,
			Color preferredBackgroundColor,
			ConsoleColor fallbackForegroundColor,
			ConsoleColor fallbackBackgroundColor)
		{
			WriteToTerminal(text, preferredForegroundColor, preferredBackgroundColor, fallbackForegroundColor, fallbackBackgroundColor);
			Console.Write("\n");
		}

		public static void ColorCheck()
		{
			string? COLORTERM = Environment.GetEnvironmentVariable("COLORTERM");

			TerminalSupportsTrueColor = OperatingSystem.IsWindows() || COLORTERM == "24bit" || COLORTERM == "truecolor";

#if TRUE_COLOR_TEST
			TrueColorTest();
#endif
		}

		#region TrueColorTest
		static void TrueColorTest()
		{
			for (int b = 0; b < 256; b++)
			{
				for (int g = 0; g < 256; g++)
				{
					for (int r = 0; r < 256; r++)
					{
						WriteToTerminal("█", Color.FromArgb(r, g, b), Color.Black, ConsoleColor.Red, ConsoleColor.Black);
					}
					Console.WriteLine();
				}
			}

			for (int r = 0; r < 256; r++)
			{
				for (int c = 0; c < 256; c++)
				{
					Color color = Color.FromArgb(Math.Clamp((255 - c) * 2, 0, 255), Math.Clamp(c * 2, 0, 255), r);

					WriteToTerminal("█", color, Color.Black, ConsoleColor.Red, ConsoleColor.Black);
				}
				Console.WriteLine();
			}
		}
		#endregion
	}
	
	internal record CombinedVisualizationColor
	{
		public Color foregroundColor { get; }
		public Color backgroundColor { get; }
		public ConsoleColor fallbackForegroundColor { get; }
		public ConsoleColor fallbackBackgroundColor { get; }

		internal CombinedVisualizationColor(Color foregroundColor,
			Color backgroundColor,
			ConsoleColor fallbackForegroundColor,
			ConsoleColor fallbackBackgroundColor)
		{
			this.foregroundColor = foregroundColor;
			this.backgroundColor = backgroundColor;
			this.fallbackForegroundColor = fallbackForegroundColor;
			this.fallbackBackgroundColor = fallbackBackgroundColor;
		}
	}

	internal record VisualizationSection
	{
		public List<byte> bytes { get; }
		public CombinedVisualizationColor color { get; }

		internal VisualizationSection(List<byte> bytes, CombinedVisualizationColor color)
		{
			this.bytes = bytes;
			this.color = color;
		}
	}

	internal record ByteSection
	{
		public byte Byte { get; }
		public CombinedVisualizationColor color { get; }

		internal ByteSection(byte Byte, CombinedVisualizationColor color)
		{
			this.Byte = Byte;
			this.color = color;
		}
	}

	internal class Visualization
	{
		public static readonly Visualization Instance = new Visualization();

		internal static Dictionary<DataType, CombinedVisualizationColor> visColors = new() {
			{ DataType.Unknown, new(Color.FromArgb(255, 0, 255),
				Color.FromArgb(0, 0, 0),
				ConsoleColor.Magenta,
				ConsoleColor.Black) },

			{ DataType.SubDataHeader, new(Color.FromArgb(0, 0, 0),
				Color.FromArgb(255, 255, 0),
				ConsoleColor.Black,
				ConsoleColor.Yellow) },

			{ DataType.Room, new(Color.FromArgb(255, 255, 255),
				Color.FromArgb(255, 0, 255),
				ConsoleColor.Yellow,
				ConsoleColor.Magenta) },

			{ DataType.XPosition, new(Color.FromArgb(0, 0, 0),
				Color.FromArgb(255, 0, 0),
				ConsoleColor.Red,
				ConsoleColor.Black) },

			{ DataType.YPosition, new(Color.FromArgb(0, 0, 0),
				Color.FromArgb(0, 255, 0),
				ConsoleColor.Green,
				ConsoleColor.Black) },

			{ DataType.ZPosition, new(Color.FromArgb(255, 255, 255),
				Color.FromArgb(0, 0, 255),
				ConsoleColor.Blue,
				ConsoleColor.Black) },

			{ DataType.NameLength, new(Color.FromArgb(0, 0, 0),
				Color.FromArgb(127, 127, 127),
				ConsoleColor.Black,
				ConsoleColor.Gray) },

			{ DataType.Name, new(Color.FromArgb(255, 255, 255),
				Color.FromArgb(0, 0, 0),
				ConsoleColor.White,
				ConsoleColor.Black) },

			{ DataType.Generation, new(Color.FromArgb(255, 255, 255),
				Color.FromArgb(127, ( int ) (127 * 0.5), ( int ) (127 * 0.5)),
				ConsoleColor.Black,
				ConsoleColor.Gray) },

			{ DataType.VersionLength, new(Color.FromArgb(0, 0, 0),
				Color.FromArgb(127, 127, ( int ) (127 * 0.5)),
				ConsoleColor.Black,
				ConsoleColor.Gray) },

			{ DataType.Version, new(Color.FromArgb(255, 255, ( int ) (255 * 0.5)),
				Color.FromArgb(0, 0, 0),
				ConsoleColor.White,
				ConsoleColor.Black) },
		};

		internal static Dictionary<DataType, string> DataTypeNames = new()
		{
			{ DataType.Unknown, "Unknown" },
			{ DataType.SubDataHeader, "Sub Data Header/Type" },
			{ DataType.Room, "Room Number" },
			{ DataType.XPosition, "X Position" },
			{ DataType.ZPosition, "Z Position" },
			{ DataType.YPosition, "Y Position" },
			{ DataType.NameLength, "Save File Name Length" },
			{ DataType.Name, "Save File Name" },
			{ DataType.Generation, "Generation" },
			{ DataType.VersionLength, "Version Length" },
			{ DataType.Version, "Version" }
		};

		internal static List<VisualizationSection> visualizationSections = [
			new([0x00], visColors[DataType.SubDataHeader]),
			new([0x02], visColors[DataType.Room]),
			new([0x01], visColors[DataType.SubDataHeader]),
			new([0x00, 0x00, 0xC0, 0xBF], visColors[DataType.XPosition]),
			new([0x00, 0x00, 0x00, 0x00], visColors[DataType.ZPosition]),
			new([0x00, 0x00, 0x00, 0x3F], visColors[DataType.YPosition]),
			new([0x07], visColors[DataType.SubDataHeader]),
			new([0x04], visColors[DataType.NameLength]),
			new([0x54, 0x65, 0x73, 0x74], visColors[DataType.Name]),
			new([0x12], visColors[DataType.SubDataHeader]),
			new([0x38], visColors[DataType.Generation]),
			new([0x1F], visColors[DataType.SubDataHeader]),
			new([0x09], visColors[DataType.VersionLength]),
			new([0x31, 0x2E, 0x32, 0x2D, 0x70, 0x72, 0x65, 0x33, 0x30], visColors[DataType.Version]),
		];

		internal static CombinedVisualizationColor offsetCounter = new(Color.FromArgb(0, 255, 255),
				Color.FromArgb(0, 0, 0),
				ConsoleColor.Cyan,
				ConsoleColor.Black);

		private Visualization()
		{
			
		}
    }

	internal class Program
	{
		static void Main(string[] args)
		{
			TerminalTrueColor.ColorCheck();

			int bytesDone = 0;

			List<ByteSection> bytesInLine = null;

			TerminalTrueColor.WriteToTerminal("    00 01 02 03 04 05 06 07 08 09 0A 0B 0C 0D 0E 0F",
				Visualization.offsetCounter.foregroundColor,
				Visualization.offsetCounter.backgroundColor,
				Visualization.offsetCounter.fallbackForegroundColor,
				Visualization.offsetCounter.fallbackBackgroundColor);

			foreach (VisualizationSection visualizationSection in Visualization.visualizationSections)
			{
				foreach (byte currentByte in visualizationSection.bytes)
				{
					if (bytesDone % 16 == 0)
					{
						if (bytesInLine != null)
						{
							TerminalTrueColor.WriteToTerminal("| ",
								Visualization.offsetCounter.foregroundColor,
								Visualization.offsetCounter.backgroundColor,
								Visualization.offsetCounter.fallbackForegroundColor,
								Visualization.offsetCounter.fallbackBackgroundColor);

							foreach (ByteSection Byte in bytesInLine)
							{
								string byteAsString = ".";

								if (Byte.Byte >= 0x20 && Byte.Byte <= 0x7E)
								{
									byteAsString = System.Text.Encoding.ASCII.GetString([Byte.Byte]);
								}

								TerminalTrueColor.WriteToTerminal(byteAsString,
									Byte.color.foregroundColor,
									Byte.color.backgroundColor,
									Byte.color.fallbackForegroundColor,
									Byte.color.fallbackBackgroundColor);
							}
						}

						Console.WriteLine();

						bytesInLine = new();

						TerminalTrueColor.WriteToTerminal(bytesDone.ToString("X3") + " ",
							Visualization.offsetCounter.foregroundColor,
							Visualization.offsetCounter.backgroundColor,
							Visualization.offsetCounter.fallbackForegroundColor,
							Visualization.offsetCounter.fallbackBackgroundColor);
					}

					TerminalTrueColor.WriteToTerminal(currentByte.ToString("X2"),
							visualizationSection.color.foregroundColor,
							visualizationSection.color.backgroundColor,
							visualizationSection.color.fallbackForegroundColor,
							visualizationSection.color.fallbackBackgroundColor);

					bytesInLine.Add(new(currentByte, visualizationSection.color));

					Console.Write(" ");

					bytesDone++;
				}
			}

			if (bytesDone % 16 != 0)
			{
				while (bytesDone % 16 != 0)
				{
					Console.Write("   ");
					bytesDone++;
				}
			}

			if (bytesInLine != null)
			{
				TerminalTrueColor.WriteToTerminal("| ",
					Visualization.offsetCounter.foregroundColor,
					Visualization.offsetCounter.backgroundColor,
					Visualization.offsetCounter.fallbackForegroundColor,
					Visualization.offsetCounter.fallbackBackgroundColor);

				foreach (ByteSection Byte in bytesInLine)
				{
					string byteAsString = ".";

					if (Byte.Byte >= 0x20 && Byte.Byte <= 0x7E)
					{
						byteAsString = System.Text.Encoding.ASCII.GetString([Byte.Byte]);
					}

					TerminalTrueColor.WriteToTerminal(byteAsString,
						Byte.color.foregroundColor,
						Byte.color.backgroundColor,
						Byte.color.fallbackForegroundColor,
						Byte.color.fallbackBackgroundColor);
				}
			}

			Console.WriteLine();
			Console.WriteLine();

			foreach (DataType key in Visualization.visColors.Keys)
            {
				CombinedVisualizationColor color = Visualization.visColors[key];

				TerminalTrueColor.WriteToTerminal("█ ",
					color.foregroundColor,
					color.backgroundColor,
					color.fallbackForegroundColor,
					color.fallbackBackgroundColor);

				TerminalTrueColor.WriteLineToTerminal(" | " + Visualization.DataTypeNames[key],
					Visualization.offsetCounter.foregroundColor,
					Visualization.offsetCounter.backgroundColor,
					Visualization.offsetCounter.fallbackForegroundColor,
					Visualization.offsetCounter.fallbackBackgroundColor);
			}
        }
	}
}