using GiftscopModifier.Core.Abstractions.SaveData.File.SubData.Enums;
using GiftscopModifier.Core.Abstractions.SaveData.File.SubData.Interfaces;
using GiftscopModifier.Core.Abstractions.SaveData.File.SubData.Types.Location.Types;
using GiftscopModifier.Core.Abstractions.SaveData.File.SubData.Types.Position.Types;
using GiftscopModifier.Core.Abstractions.SaveData.File.SubData.Types.Version.Types;
using GiftscopModifier.Core.Enums;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using Version = GiftscopModifier.Core.Abstractions.SaveData.File.SubData.Types.Version.Types.Version;

namespace GiftscopModifier.Core.Abstractions.SaveData.File
{
	public class File : IBaseDataInterface
	{
		public Dictionary<SubDataType, SubData.Interfaces.ISubData> SubData { get; } = [];

		private void BuildSubDataForSubDataType(byte SubDataTypeByte, ref List<byte> CurrentDataBytes)
		{
			SubDataType SubDataTypeEnum = (SubDataType) SubDataTypeByte;

			if (!Enum.IsDefined(( SubDataType ) SubDataTypeByte))
			{
				Console.WriteLine($"Unknown SubDataType: 0x{SubDataTypeByte:X2}. Please implement this SubDataType, as this data will be discardded upon filling in information.");

				CurrentDataBytes.RemoveAt(0);

				return;
			}

			ISubData? subData = null;

			switch (SubDataTypeEnum)
			{
				case SubDataType.Room:
					subData = new Room();
					break;

				case SubDataType.Position:
					subData = new Position();
					break;

				case SubDataType.SaveName:
					subData = new SaveName();
					break;

				case SubDataType.Generation:
					subData = new Generation();
					break;

				case SubDataType.Version:
					subData = new Version();
					break;

				default:
					Console.WriteLine($"Unknown SubDataType: 0x{SubDataTypeByte:X2}. Please implement this SubDataType, as this data will be discardded upon filling in information.");

					CurrentDataBytes.RemoveAt(0);

					return;
			}

			SubData[SubDataTypeEnum] = subData;

			if (subData == null)
			{
				Console.WriteLine($"Unknown SubDataType: 0x{SubDataTypeByte:X2}. Please implement this SubDataType, as this data will be discardded upon filling in information.");

				CurrentDataBytes.RemoveAt(0);

				return;
			}

			subData.BuildSubData(ref CurrentDataBytes);

			if (SubDataTypeEnum == SubDataType.Version && (Version) subData != null)
			{
				if (((Version) subData).VersionString != "1.2-pre30")
				{
					Debug.WriteLine("Version is not 1.2-pre30. Possible incompatibilites.");
				}
			}
		}

		public File()
		{}

		public void fillInData(List<byte> Data)
		{
			if (Data.Count == 0)
			{
				return;
			}

			while (Data.Count != 0)
			{
				BuildSubDataForSubDataType(Data.First(), ref Data);
			}
		}

		public List<byte> rebuildData()
		{
			List<byte> Data = [];

			foreach (ISubData SubData in SubData.Values)
			{
				foreach (byte Byte in SubData.BuildBytes())
				{
					Data.Add(Byte);
				}
			}

			return Data;
		}
	}
}
