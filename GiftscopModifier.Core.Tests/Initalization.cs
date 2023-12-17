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
		public static GiftscopModifier.Core.Abstractions.SaveData.File.File saveFile = new();

		[TestMethod]
		public void InitSaveFile()
		{
			saveFile.fillInData(Properties.Resources.SaveFile.ToList());
		}
	}
}
