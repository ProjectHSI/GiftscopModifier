using GiftscopModifier.Core.Abstractions.SaveData.File.SubData.Enums;
using GiftscopModifier.Core.Abstractions.SaveData.File.SubData.Interfaces;
using GiftscopModifier.Core.Abstractions.SaveData.File.SubData.Types.Location.Enums;

namespace GiftscopModifier.Core.Abstractions.SaveData.File.SubData.Types.Location.Types
{
	internal record Room : ISubData
	{
		internal static SubDataType SubDataType = SubDataType.ROOM;

		public Room(RoomType roomType)
		{
			RoomType = roomType;
		}

		public RoomType RoomType { get; }

		static ISubData ISubData.BuildSubData(List<byte> bytes)
		{
			// Check the data type.
			if (bytes[0] != (( byte ) SubDataType))
			{
				throw new InvalidOperationException("The sub data type was not valid for this sub data container.");
			}

			bytes.RemoveAt(0);

			// Object initalization code.
			return new Room((RoomType) bytes.TakeAndRemove(1)[0]);

		}

		List<byte> ISubData.BuildBytes()
		{
			List<byte> bytes = [];

			bytes.Add(( byte ) SubDataType);

			bytes.Add(( byte ) RoomType);

			return bytes;
		}
	}
}
