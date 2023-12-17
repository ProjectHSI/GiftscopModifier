using GiftscopModifier.Core.Abstractions.SaveData.File.SubData.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.Marshalling;
using System.Text;
using System.Threading.Tasks;

namespace GiftscopModifier.Core.Abstractions.SaveData.File.SubData.Types.Version.Types
{
	public sealed class Generation : Interfaces.ISubData
	{
		internal static SubDataType SubDataType = Enums.SubDataType.Generation;

		public Generation()
		{ }

		private byte _GenerationNumber = 8;
		public byte GenerationNumber
		{
			get
			{
				return _GenerationNumber;
			}
			set
			{
				if (value == null)
				{
					value = 8;
				}

				if (value > 0xF)
				{
					throw new ArgumentOutOfRangeException(nameof(value), "\"value\"'s length must be in the range of 0-15.");
				}

				_GenerationNumber = value;
			}
		}

		void Interfaces.ISubData.BuildSubData(ref List<byte> bytes)
		{
			// Check the data type.
			if (bytes[0] != (( byte ) Generation.SubDataType))
			{
				throw new InvalidOperationException("The sub data type was not valid for this sub data container.");
			}

			bytes.RemoveAt(0);

			// Object initalization code.
			_GenerationNumber = ( byte ) (bytes.TakeAndRemove(1)[0] - 0x30);
		}

		List<byte> Interfaces.ISubData.BuildBytes()
		{
			List<byte> bytes = [];

			bytes.Add(( byte ) SubDataType);

			bytes.Add(( byte ) (_GenerationNumber + 0x30));

            return bytes;
		}
	}
}
