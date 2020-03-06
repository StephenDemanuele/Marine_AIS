namespace AIS.Parser
{
	internal enum TalkerId
	{
		AB,
		AD,
		AI,
		AN,
		AR,
		AS,
		AT,
		AX,
		BS,
		SA
	}

    public enum Talkers
    {
        Undefined,
        BaseAIS,
        DependentAISBase,
        MobileAIS,
        AidToNavigation,
        Receiving,
        LimitedBase,
        Transmitting,
        Repeater,
        Deprecated,
        PhysicalStore
    }

	public enum NavStatus
	{
        Unset = -1,
		UnderwayUsingEngine = 0,
		AtAnchor = 1,
		NotUnderCommand = 2,
		RestrictedManeuvrability = 3,
		ConstrainedByHerDraught = 4,
		Moored = 5,
		Aground = 6,
		EngagedInFishing = 7,
		UnderWaySailing = 8,
		Reserved = 9,
		ReservedAsWell = 10,
		PowerDrivenVesselTowingAstern = 11,
		PowerDrivenVesselPushingAheadOrTowingAlongside = 12,
		ReservedFutureUse = 13,
		AIS_SART_ = 14,
		Undefined = 15
	}

    public enum ShipCargoTypes
    {
        WIG_HazA = 21,
        WIG_HazB = 22,
        WIG_HazC = 23,
        WIG_HazD = 24,
        Fishing = 30,
        Towing = 31,
        TowingLengthExceeds200mBreadthExceeds25m = 32,
        UnderwaterOps = 33,
        DivingOps = 34,
        MilitaryOps = 35,
        Sailing = 36,
        PleasureCraft = 37,
        HighSpeedCraft = 40,
        HighSpeedCraft_HazA = 41,
        HighSpeedCraft_HazB = 42,
        HighSpeedCraft_HazC = 43,
        HighSpeedCraft_HazD = 44,
        HighSpeedCraft_NoAddInfo = 49,
        PilotVessel = 50,
        SearchRescueVessel = 51,
        Tug = 52,
        PortTender = 53,
        AntiPollutionEquipment = 54,
        LawEnforcementVessel = 55,
        SpareLocalVessel1 = 56,
        SpareLocalVessel2 = 57,
        MedicalTransport = 58,
        ShipsAndAircraftofStatesNotPartyToArmedConflict = 59,
        PassengerShip = 60,
        PassengerShip_HazA = 61,
        PassengerShip_HazB = 62,
        PassengerShip_HazC = 63,
        PassengerShip_HazD = 64,
        PassengerShip_NoAddInfo = 69,
        CargoShip = 70,
        CargoShip_HazA = 71,
        CargoShip_HazB = 72,
        CargoShip_HazC = 73,
        CargoShip_HazD = 74,
        CargoShip_NoAddInfo = 79,
        Tanker = 80,
        Tanker_HazA = 81,
        Tanker_HazB = 82,
        Tanker_HazC = 83,
        Tanker_HazD = 84,
        Tanker_NoAddInfo = 89,
        OtherType = 90,
        OtherType_HazA = 91,
        OtherType_HazB = 92,
        OtherType_HazC = 93,
        OtherType_HazD = 94,
        Othertype_95 = 95,
        OtherType_NoAddInfo = 99,
    }

    public enum PositionFixingDeviceType
    {
        Undefined = 0,
        GPS = 1,
        GLONASS = 2,
        GPS_GLONASS = 3,
        LORAN_C = 4,
        CHAYKA = 5,
        INTEGRATED_NAV_SYSTEM = 6,
        SURVEYED = 7,
        GALILEO = 8,
        UNUSED_9 = 9,
        UNUSED_10 = 10,
        UNUSED_11 = 11,
        UNUSED_12 = 12,
        UNUSED_13 = 13,
        UNUSED_14 = 14
    }
}