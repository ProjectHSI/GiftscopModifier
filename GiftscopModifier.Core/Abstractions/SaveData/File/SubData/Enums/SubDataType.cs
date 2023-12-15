using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GiftscopModifier.Core.Abstractions.SaveData.File.SubData.Enums {
	internal enum SubDataType
	{
		Room = 0x00,
		Position = 0x01,
		SaveName = 0x07,
		Generation = 0x12,
		Version = 0x1F,
	}
}
