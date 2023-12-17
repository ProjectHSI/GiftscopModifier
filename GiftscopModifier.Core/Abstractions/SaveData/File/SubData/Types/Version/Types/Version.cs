using GiftscopModifier.Core.Abstractions.SaveData.File.SubData.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.Marshalling;
using System.Text;
using System.Threading.Tasks;

namespace GiftscopModifier.Core.Abstractions.SaveData.File.SubData.Types.Version.Types
{
	internal class Version : Interfaces.ISubData
	{
		internal static SubDataType SubDataType = SubDataType.Version;

		public Version()
		{ }

		private string _VersionString;
		public string VersionString
		{
			get
			{
				return _VersionString;
			}
			set
			{
				if (value.Length > 0xff)
				{
					throw new ArgumentOutOfRangeException(nameof(value), "\"value\"'s length must be in the range of 0-255.");
				}
			}
		}

		void Interfaces.ISubData.BuildSubData(ref List<byte> bytes)
		{
			// Check the data type.
			if (bytes[0] != (( byte ) SubDataType))
			{
				throw new InvalidOperationException("The sub data type was not valid for this sub data container.");
			}

			bytes.RemoveAt(0);

			// Object initalization code.
			int versionLength = bytes.TakeAndRemove(1).ToArray()[0];
			_VersionString = Encoding.ASCII.GetString(bytes.TakeAndRemove(versionLength).ToArray());
		}

		List<byte> Interfaces.ISubData.BuildBytes()
		{
			List<byte> bytes = [];

			bytes.Add(( byte ) SubDataType);

			bytes.Add(( byte ) _VersionString.Length);
            foreach (byte charByte in Encoding.ASCII.GetBytes(_VersionString))
            {
				bytes.Add(charByte);
            }

            return bytes;
		}
	}
}
