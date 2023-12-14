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
		EvenCareNewMakerPlaneEntrance = 0x0A, // Only present in Odd Care.

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
		UndergroundQuitterLeftRoom = 0x19,
		UndergroundQuitterRightRoom = 0x1A,
		UndergroundQuitterNewMakerPlaneExit = 0x1B,
		UndergroundGGAA = 0x1C,
		UndergroundChildLibrary = 0x1D,
		UndergroundChildLibraryRoom = 0x1E,
		UndergroundPartyRoom = 0x1F,
		UndergroundGiftBoxRoom = 0x20,

		// Entries 0x21-0x26 cause the save file to appear as an empty file and thus are not included.

		// School
		SchoolFloor1 = 0x27,
		SchoolNeedlesPiano = 0x28,
		SchoolFloor2 = 0x29,
		SchoolSmallClassroom = 0x2A,
		SchoolFloor3 = 0x2B,
		SchoolGhostRooms = 0x2C,

		// School Basement
		SchoolBasementEntrance = 0x2D,
		SchoolBasementGifts = 0x2E,
		SchoolBasementMachine = 0x2F,

		// Inaccessible
		SortTest = 0x30,

		// Marvin's Recording Rooms
		RoadMap = 0x31,
		CasketsRoom = 0x32,

		// Inaccessible
		WorkZone = 0x33,

		// Soundtrack Easter Egg
		SoundtrackEasterEgg = 0x34,
	}
}
