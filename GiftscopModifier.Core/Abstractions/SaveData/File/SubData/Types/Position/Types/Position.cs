using GiftscopModifier.Core.Abstractions.SaveData.File.SubData.Enums;
using GiftscopModifier.Core.Abstractions.SaveData.File.SubData.Interfaces;
using System.Runtime.InteropServices;

namespace GiftscopModifier.Core.Abstractions.SaveData.File.SubData.Types.Position.Types
{
	internal record Position : ISubData
	{
		internal static SubDataType SubDataType = SubDataType.POSITION;

		public Position(float x, float y, float z)
		{
			X = x;
			Y = y;
			Z = z;
		}

		public float X { get; }
		public float Y { get; }
		public float Z { get; }

		static ISubData ISubData.BuildSubData(List<byte> bytes)
		{
			// Check the data type.
			if (bytes[0] != (( byte ) SubDataType))
			{
				throw new InvalidOperationException("The sub data type was not valid for this sub data container.");
			}

			bytes.RemoveAt(0);

			// Object initalization code.
			return new Position(BitConverter.ToSingle(CollectionsMarshal.AsSpan(bytes.TakeAndRemove(4))), BitConverter.ToSingle(CollectionsMarshal.AsSpan(bytes.TakeAndRemove(4))), BitConverter.ToSingle(CollectionsMarshal.AsSpan(bytes.TakeAndRemove(4))));
		}

		List<byte> ISubData.BuildBytes()
		{
			List<byte> bytes = [];

			bytes.Add(( byte ) SubDataType);

			foreach (byte floatByte in BitConverter.GetBytes(X))
			{
				bytes.Add(floatByte);
			}

			foreach (byte floatByte in BitConverter.GetBytes(Z))
			{
				bytes.Add(floatByte);
			}

			foreach (byte floatByte in BitConverter.GetBytes(Y))
			{
				bytes.Add(floatByte);
			}

			return bytes;
		}
	}
}
