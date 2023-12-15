using GiftscopModifier.Core.Abstractions.SaveData.File.SubData.Enums;
using GiftscopModifier.Core.Abstractions.SaveData.File.SubData.Interfaces;
using System.Runtime.InteropServices;

namespace GiftscopModifier.Core.Abstractions.SaveData.File.SubData.Types.Position.Types
{
	internal record Position : Interfaces.ISubData
	{
		internal static SubDataType SubDataType = SubDataType.POSITION;

		public Position()
		{}

		public float X { get; set; }
		public float Y { get; set; }
		public float Z { get; set; }

		void Interfaces.ISubData.BuildSubData(ref List<byte> bytes)
		{
			// Check the data type.
			if (bytes[0] != (( byte ) SubDataType))
			{
				throw new InvalidOperationException("The sub data type was not valid for this sub data container.");
			}

			bytes.RemoveAt(0);

			// Object initalization code.
			X = BitConverter.ToSingle(CollectionsMarshal.AsSpan(bytes.TakeAndRemove(4)));
			Z = BitConverter.ToSingle(CollectionsMarshal.AsSpan(bytes.TakeAndRemove(4)));
			Y = BitConverter.ToSingle(CollectionsMarshal.AsSpan(bytes.TakeAndRemove(4)));
		}

		List<byte> Interfaces.ISubData.BuildBytes()
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
