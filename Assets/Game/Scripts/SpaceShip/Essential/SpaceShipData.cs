using System;

[Serializable]
public class SpaceShipData : UnitData
{
    public FrameData frameData;
    public StructureData[] structuresData;
    public WeaponData[] weaponsData;
    
    [Serializable] public class FrameData
    { public SpaceShipFrameType frameType; public SpaceShipComponentTierType tierType; }

    [Serializable] public class StructureData
    { public SpaceShipStructureType structureType; public SpaceShipComponentTierType tierType; }
    
    [Serializable] public class WeaponData
    { public SpaceShipWeaponType weaponType; public SpaceShipComponentTierType tierType; }
}

public enum SpaceShipFrameType
{ Battle, Build, Science, Cargo, Citizen, Mother }

public enum SpaceShipStructureType
{ Generator, Engine, Radar, FuelTank, Shell, Storage, Laboratory }

public enum SpaceShipWeaponType
{ None, Artillery, Hangar, Laser, Railgun }

public enum SpaceShipComponentTierType
{ None, Tier0, Tier1, Tier2, Tier3, Tier4, Tier5 }
