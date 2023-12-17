using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GiftscopModifier.Core.Tests
{
	[TestClass]
	public class Initalization
	{
		//public static GiftscopModifier.Core.Abstractions.SaveData.File.File SaveFile = new();
		//public static bool DoneInitalization { get; private set; } = false;

		[TestMethod]
		public void InitSaveFile()
		{
			new Abstractions.SaveData.File.File().fillInData([.. Properties.Resources.StartingSaveFile]);
			//DoneInitalization = true;
		}
	}
}
