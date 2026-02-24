using System;

[Serializable]
public abstract class UnitData
{
    [ReadOnly] public string unitKey;
    [ReadOnly] public string configPath;
}

public enum UnitType { EmptyUnit, SpaceShip, SpaceStation }