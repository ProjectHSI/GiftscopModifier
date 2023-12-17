using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Drawing;
using TerminalTrueColor;

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
	
	internal record CombinedVisualizationColor
	{
		public Color ForegroundColor { get; }
		public Color BackgroundColor { get; }
		public ConsoleColor FallbackForegroundColor { get; }
		public ConsoleColor FallbackBackgroundColor { get; }

		internal CombinedVisualizationColor(Color foregroundColor,
			Color backgroundColor,
			ConsoleColor fallbackForegroundColor,
			ConsoleColor fallbackBackgroundColor)
		{
			ForegroundColor = foregroundColor;
			BackgroundColor = backgroundColor;
			FallbackForegroundColor = fallbackForegroundColor;
			FallbackBackgroundColor = fallbackBackgroundColor;
		}
	}

	internal record VisualizationSection
	{
		public List<byte> Bytes { get; }
		public CombinedVisualizationColor Color { get; }

		internal VisualizationSection(List<byte> bytes, CombinedVisualizationColor color)
		{
			Bytes = bytes;
			Color = color;
		}
	}

	internal record ByteSection
	{
		public byte Byte { get; }
		public CombinedVisualizationColor Color { get; }

		internal ByteSection(byte Byte, CombinedVisualizationColor color)
		{
			this.Byte = Byte;
			this.Color = color;
		}
	}

	internal class Visualization
	{
		public static readonly Visualization Instance = new();

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
			TrueColor.ColorCheck();

			int bytesDone = 0;

			List<ByteSection>? bytesInLine = null;

			TrueColor.WriteToTerminal("    00 01 02 03 04 05 06 07 08 09 0A 0B 0C 0D 0E 0F",
				Visualization.offsetCounter.ForegroundColor,
				Visualization.offsetCounter.BackgroundColor,
				Visualization.offsetCounter.FallbackForegroundColor,
				Visualization.offsetCounter.FallbackBackgroundColor);

			foreach (VisualizationSection visualizationSection in Visualization.visualizationSections)
			{
				foreach (byte currentByte in visualizationSection.Bytes)
				{
					if (bytesDone % 16 == 0)
					{
						if (bytesInLine != null)
						{
							TrueColor.WriteToTerminal("| ",
								Visualization.offsetCounter.ForegroundColor,
								Visualization.offsetCounter.BackgroundColor,
								Visualization.offsetCounter.FallbackForegroundColor,
								Visualization.offsetCounter.FallbackBackgroundColor);

							foreach (ByteSection Byte in bytesInLine)
							{
								char byteAsString = System.Text.Encoding.ASCII.GetChars([Byte.Byte])[0];

								if (!char.IsAsciiLetter(byteAsString) && !char.IsAsciiDigit(byteAsString))
								{
									byteAsString = '.';
								}

								TrueColor.WriteToTerminal(byteAsString.ToString(),
									Byte.Color.ForegroundColor,
									Byte.Color.BackgroundColor,
									Byte.Color.FallbackForegroundColor,
									Byte.Color.FallbackBackgroundColor);
							}
						}

						Console.WriteLine();

						bytesInLine = [];

						TrueColor.WriteToTerminal(bytesDone.ToString("X3") + " ",
							Visualization.offsetCounter.ForegroundColor,
							Visualization.offsetCounter.BackgroundColor,
							Visualization.offsetCounter.FallbackForegroundColor,
							Visualization.offsetCounter.FallbackBackgroundColor);
					}

					TrueColor.WriteToTerminal(currentByte.ToString("X2"),
							visualizationSection.Color.ForegroundColor,
							visualizationSection.Color.BackgroundColor,
							visualizationSection.Color.FallbackForegroundColor,
							visualizationSection.Color.FallbackBackgroundColor);

#pragma warning disable CS8602 // Dereference of a possibly null reference.
					bytesInLine.Add(new(currentByte, visualizationSection.Color));
#pragma warning restore CS8602 // Dereference of a possibly null reference.

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
				TrueColor.WriteToTerminal("| ",
					Visualization.offsetCounter.ForegroundColor,
					Visualization.offsetCounter.BackgroundColor,
					Visualization.offsetCounter.FallbackForegroundColor,
					Visualization.offsetCounter.FallbackBackgroundColor);

				foreach (ByteSection Byte in bytesInLine)
				{
					string byteAsString = ".";
					string potentialByteString = System.Text.Encoding.ASCII.GetString([Byte.Byte]);

					if (potentialByteString.Length != 0)
					{
						byteAsString = System.Text.Encoding.ASCII.GetString([Byte.Byte]);
					}

					TrueColor.WriteToTerminal(byteAsString,
						Byte.Color.ForegroundColor,
						Byte.Color.BackgroundColor,
						Byte.Color.FallbackForegroundColor,
						Byte.Color.FallbackBackgroundColor);
				}
			}

			Console.WriteLine();
			Console.WriteLine();

			foreach (DataType key in Visualization.visColors.Keys)
            {
				CombinedVisualizationColor color = Visualization.visColors[key];

				TrueColor.WriteToTerminal("█ ",
					color.ForegroundColor,
					color.BackgroundColor,
					color.FallbackForegroundColor,
					color.FallbackBackgroundColor);

				TrueColor.WriteLineToTerminal(" | " + Visualization.DataTypeNames[key],
					Visualization.offsetCounter.ForegroundColor,
					Visualization.offsetCounter.BackgroundColor,
					Visualization.offsetCounter.FallbackForegroundColor,
					Visualization.offsetCounter.FallbackBackgroundColor);
			}
        }
	}
}