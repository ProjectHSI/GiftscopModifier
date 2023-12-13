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
		internal abstract static ISubData BuildSubData(List<byte> bytes);

		internal List<byte> BuildBytes();
	}
}
