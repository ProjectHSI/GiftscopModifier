using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GiftscopModifier.Core.Abstractions.SaveData.File.SubData.Types.Location.Enums
{
    public enum RoomType
    {
		// Pseudolevels
		// These ones exist in the game (day garage loading screen) but cause the game to go back to the title screen.
		PseudoLevel1 = 0x00,
		PseudoLevel2 = 0x01,

		// GiftPlane
        GiftPlane = 0x02,

		// Even Care
        EvenCareLobby = 0x03,
		EvenCareHallway = 0x04,
		EvenCareAmber = 0x05,
		EvenCareRoneth = 0x06,
		EvenCarePen = 0x07,
		EvenCareCorner = 0x08,
		EvenCareRainyRandice = 0x09,

		// Newmaker Plane
		NewMakerPlane = 0x0B,

		// Underground
		UndergroundEntrance = 0x0C,
		UndergroundHallway = 0x0D,
		UndergroundPhone = 0x0E,
		UndergroundBigHallway = 0x0F,
		UndergroundRoad = 0x10,
		UndergroundShed = 0x11,
		UndergroundFlower = 0x12,
		UndergroundCareNLM = 0x13,
		UndergroundToolQuitterTJunction = 0x14,
		UndergroundToolHallway = 0x15,
		UndergroundTool = 0x16,
		UndergroundQuitterHallway = 0x17,
		UndergroundQuitterBuildingHallway = 0x18,
		// Missing entry 0x19
		UndergroundQuitterRoom = 0x1A,
		// Missing entry 0x1B
		UndergroundGGAA = 0x1C,
		UndergroundChildLibrary = 0x1D,
		UndergroundChildLibraryRoom = 0x1E,
	}
}
