using GiftscopModifier.Core.Abstractions.SaveData.File.SubData.Enums;
using GiftscopModifier.Core.Abstractions.SaveData.File.SubData.Interfaces;
using GiftscopModifier.Core.Abstractions.SaveData.File.SubData.Types.Location.Types;
using GiftscopModifier.Core.Abstractions.SaveData.File.SubData.Types.Position.Types;
using GiftscopModifier.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace GiftscopModifier.Core.Abstractions.SaveData.File
{
    internal class File : IBaseDataInterface
    {
		private Dictionary<SubDataType, SubData.Interfaces.ISubData> SubData = [];

		private void BuildSubDataForSubDataType(byte SubDataTypeByte, ref List<byte> CurrentDataBytes)
		{
			SubDataType SubDataTypeEnum = (SubDataType) SubDataTypeByte;

			if (!Enum.IsDefined(typeof(SubDataType), SubDataTypeEnum))
			{
				Console.WriteLine($"Unknown SubDataType: 0x{SubDataTypeByte:X2}. Please implement this SubDataType, as this data will be discardded upon filling in information.");

				CurrentDataBytes.RemoveAt(0);

				return;
			}

			switch (SubDataTypeEnum)
			{
				case SubDataType.ROOM:
					SubData[SubDataTypeEnum] = new Room();
					SubData[SubDataTypeEnum].BuildSubData(ref CurrentDataBytes);
					break;

				case SubDataType.POSITION:
					SubData[SubDataTypeEnum] = new Position();
					SubData[SubDataTypeEnum].BuildSubData(ref CurrentDataBytes);
					break;
			}
		}

		public File()
		{}

		void IBaseDataInterface.fillInData(List<byte> Data)
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

        List<byte> IBaseDataInterface.rebuildData()
        {
			List<byte> Data = new();

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
