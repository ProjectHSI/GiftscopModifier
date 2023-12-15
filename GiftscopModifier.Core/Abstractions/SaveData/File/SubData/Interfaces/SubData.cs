using GiftscopModifier.Core.Abstractions.SaveData.File.SubData.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GiftscopModifier.Core.Abstractions.SaveData.File.SubData.Interfaces
{
	internal interface ISubData
	{
		internal static SubDataType SubDataType;
		internal void BuildSubData(ref List<byte> bytes)
		{
			throw new NotImplementedException();
		}

		internal List<byte> BuildBytes()
		{
			throw new NotImplementedException();
		}
	}
}
