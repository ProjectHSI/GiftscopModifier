using GiftscopModifier.Core.Abstractions.SaveData.File;
using GiftscopModifier.Core.Abstractions.SaveData.File.SubData.Enums;
using GiftscopModifier.Core.Abstractions.SaveData.File.SubData.Types.Location.Types;
using GiftscopModifier.Core.Abstractions.SaveData.File.SubData.Types.Position.Types;
using GiftscopModifier.Core.Abstractions.SaveData.File.SubData.Types.Version.Types;
using GiftscopModifier.Core.Tests.Properties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GiftscopModifier.Core.Tests
{
	[TestClass]
	public class Modification
	{
		private static void WaitForInitlization()
		{
			/*bool completed = *///SpinWait.SpinUntil(() => Initalization.DoneInitalization);
			/*Assert.IsTrue(completed, "Initalization failed or took too long.");*/
		}

		[TestMethod]
		public void RoomModification()
		{
			//WaitForInitlization();

			Abstractions.SaveData.File.File saveFile = new();

			saveFile.fillInData([.. Properties.Resources.StartingSaveFile]);

			Assert.IsNotNull(saveFile.SubData[SubDataType.Room], "SubData for Room not found.");

			(( Room ) saveFile.SubData[SubDataType.Room]).RoomType = Abstractions.SaveData.File.SubData.Types.Location.Enums.RoomType.EvenCareCorner;

			byte[] byteArray = [.. saveFile.rebuildData()];

			Assert.IsTrue(byteArray.Length == Resources.RoomModifiedSaveFile.Length, "Length doesn't match.");

			for (int i = 0; i < Math.Min(byteArray.Length, Resources.RoomModifiedSaveFile.Length); i++)
			{
				Assert.IsTrue(byteArray[i] == Resources.RoomModifiedSaveFile[i], $"Byte doesn't match. ({Resources.RoomModifiedSaveFile[i]:X2} v. {byteArray[i]:X2}. I {i}.)");
			}
		}

		[TestMethod]
		public void XModification()
		{
			//WaitForInitlization();

			Abstractions.SaveData.File.File saveFile = new();

			saveFile.fillInData([.. Properties.Resources.StartingSaveFile]);

			Assert.IsNotNull(saveFile.SubData[SubDataType.Position], "SubData for Position not found.");

			(( Position ) saveFile.SubData[SubDataType.Position]).X = -5.5f;

			byte[] byteArray = [.. saveFile.rebuildData()];

			Assert.IsTrue(byteArray.Length == Resources.XModifiedSaveFile.Length, "Length doesn't match.");

			for (int i = 0; i < Math.Min(byteArray.Length, Resources.XModifiedSaveFile.Length); i++)
			{
				Assert.IsTrue(byteArray[i] == Resources.XModifiedSaveFile[i], $"Byte doesn't match. ({Resources.XModifiedSaveFile[i]:X2} v. {byteArray[i]:X2}. I {i}.)");
			}
		}

		[TestMethod]
		public void ZModification()
		{
			//WaitForInitlization();

			Abstractions.SaveData.File.File saveFile = new();

			saveFile.fillInData([.. Properties.Resources.StartingSaveFile]);

			Assert.IsNotNull(saveFile.SubData[SubDataType.Position], "SubData for Position not found.");

			(( Position ) saveFile.SubData[SubDataType.Position]).Z = 5f;

			byte[] byteArray = [.. saveFile.rebuildData()];

			Assert.IsTrue(byteArray.Length == Resources.ZModifiedSaveFile.Length, "Length doesn't match.");

			for (int i = 0; i < Math.Min(byteArray.Length, Resources.ZModifiedSaveFile.Length); i++)
			{
				Assert.IsTrue(byteArray[i] == Resources.ZModifiedSaveFile[i], $"Byte doesn't match. ({Resources.ZModifiedSaveFile[i]:X2} v. {byteArray[i]:X2}. I {i}.)");
			}
		}

		[TestMethod]
		public void YModification()
		{
			//WaitForInitlization();

			Abstractions.SaveData.File.File saveFile = new();

			saveFile.fillInData([.. Properties.Resources.StartingSaveFile]);

			Assert.IsNotNull(saveFile.SubData[SubDataType.Position], "SubData for Position not found.");

			(( Position ) saveFile.SubData[SubDataType.Position]).Y = -0.5f;

			byte[] byteArray = [.. saveFile.rebuildData()];

			Assert.IsTrue(byteArray.Length == Resources.YModifiedSaveFile.Length, "Length doesn't match.");

			for (int i = 0; i < Math.Min(byteArray.Length, Resources.YModifiedSaveFile.Length); i++)
			{
				Assert.IsTrue(byteArray[i] == Resources.YModifiedSaveFile[i], $"Byte doesn't match. ({Resources.YModifiedSaveFile[i]:X2} v. {byteArray[i]:X2}. I {i}.)");
			}
		}

		[TestMethod]
		public void SaveNameModification()
		{
			//WaitForInitlization();

            Abstractions.SaveData.File.File saveFile = new();

			saveFile.fillInData([.. Properties.Resources.StartingSaveFile]);

			Assert.IsNotNull(saveFile.SubData[SubDataType.SaveName], "SubData for SaveName not found.");

			(( SaveName ) saveFile.SubData[SubDataType.SaveName]).SaveNameString = "iftscopModifierG";

			byte[] byteArray = [.. saveFile.rebuildData()];

			Assert.IsTrue(byteArray.Length == Resources.NameModifiedSaveFile.Length, "Length doesn't match.");

			for (int i = 0; i < Math.Min(byteArray.Length, Resources.NameModifiedSaveFile.Length); i++)
			{
				Assert.IsTrue(byteArray[i] == Resources.NameModifiedSaveFile[i], $"Byte doesn't match. ({Resources.NameModifiedSaveFile[i]:X2} v. {byteArray[i]:X2}. I {i}.)");
			}
        }

		[TestMethod]
		public void VersionModification()
		{
			//WaitForInitlization();

			Abstractions.SaveData.File.File saveFile = new();

			saveFile.fillInData([.. Properties.Resources.StartingSaveFile]);

			Assert.IsNotNull(saveFile.SubData[SubDataType.Version], "SubData for SaveName not found.");

			((Abstractions.SaveData.File.SubData.Types.Version.Types.Version) saveFile.SubData[SubDataType.Version]).VersionString = "2.0-trolledbygm";

			byte[] byteArray = [.. saveFile.rebuildData()];

			Assert.IsTrue(byteArray.Length == Resources.VersionModifiedSaveFile.Length, "Length doesn't match.");

			for (int i = 0; i < Math.Min(byteArray.Length, Resources.VersionModifiedSaveFile.Length); i++)
			{
				Assert.IsTrue(byteArray[i] == Resources.VersionModifiedSaveFile[i], $"Byte doesn't match. ({Resources.VersionModifiedSaveFile[i]:X2} v. {byteArray[i]:X2}. I {i}.)");
			}
		}

		[TestMethod]
		public void GenerationModification()
		{
			//WaitForInitlization();

			Abstractions.SaveData.File.File saveFile = new();

			saveFile.fillInData([.. Properties.Resources.StartingSaveFile]);

			Assert.IsNotNull(saveFile.SubData[SubDataType.Generation], "SubData for Position not found.");

			(( Generation ) saveFile.SubData[SubDataType.Generation]).GenerationNumber = 15;

			byte[] byteArray = [.. saveFile.rebuildData()];

			Assert.IsTrue(byteArray.Length == Resources.GenerationModifiedSaveFile.Length, "Length doesn't match.");

			for (int i = 0; i < Math.Min(byteArray.Length, Resources.GenerationModifiedSaveFile.Length); i++)
			{
				Assert.IsTrue(byteArray[i] == Resources.GenerationModifiedSaveFile[i], $"Byte doesn't match. ({Resources.GenerationModifiedSaveFile[i]:X2} v. {byteArray[i]:X2}. I {i}.)");
			}
		}
	}
}
