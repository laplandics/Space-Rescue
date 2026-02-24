using System;

[Serializable]
public class SpaceShipInitializer : UnitInitializer<SpaceShip>
{
    private SpaceShipData _data;
    private SpaceShipConstructor _constructor;

    public override void Initialize(Unit owner)
    {
        base.Initialize(owner);
        _data = Owner.GetData<SpaceShipData>();
        _constructor = Owner.FindSystem<SpaceShipConstructor>();
    }

    public void BeginInitialization()
    {
        InitializeFrame();
        InitializeStructures();
        InitializeWeapons();
    }
    
    private void InitializeFrame()
    {
        var frameType = _data.frameData.frameType;
        var tierType = _data.frameData.tierType;
        _constructor.ConstructFrame(frameType, tierType);
    }

    private void InitializeStructures()
    {
        if (_data.structuresData.Length == 0) return;
        foreach (var structureData in _data.structuresData)
            _constructor.ConstructStructure(structureData.structureType, structureData.tierType);
    }

    private void InitializeWeapons()
    {
        if (_data.weaponsData.Length == 0) return;
        foreach (var weaponData in _data.weaponsData)
            _constructor.ConstructWeapon(weaponData.weaponType, weaponData.tierType);
    }

    public override void Deinitialize()
    {
        Owner = null;
        _data = null;
        Loader.Unload();
    }
}