using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GiftscopModifier.Core.Abstractions.SaveData.File.SubData.Types.Location.Enums
{
    public enum RoomType
    {
		// On Startup
		garalina = 0x00,
		title = 0x01,

		// Gift Plane
		gift_plane = 0x02,

		// Even Care
		level1_room1 = 0x03, // EvenCareLobby
		level1_room2 = 0x04, // EvenCareHallway
		level1_room3 = 0x05, // EvenCareAmber
		level1_room4 = 0x06, // EvenCareRoneth
		level1_room5 = 0x07, // EvenCarePen
		level1_room6 = 0x08, // EvenCareCirber
		level1_room7 = 0x09, // EvenCareRainyRandice
		level1_room8 = 0x0A, // Only present in Odd Care. // EvenCareNewMakerPlaneEntrance

		// Newmaker Plane
		newmaker_plane = 0x0B,

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
		
		// Windmill
		Windmill = 0x21,

		// House
		House = 0x22,
		HouseBathroom = 0x23,
		CareBedroom = 0x24,
		MarvinBedroom = 0x25,
		HouseGarage = 0x26,

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
