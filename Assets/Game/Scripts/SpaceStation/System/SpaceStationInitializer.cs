using System;

[Serializable]
public class SpaceStationInitializer : UnitInitializer<SpaceStation>
{
    private SpaceStationData _data;
    private SpaceStationConstructor _constructor;

    public override void Initialize(Unit owner)
    {
        base.Initialize(owner);
        _data = Owner.GetData<SpaceStationData>();
        _constructor = Owner.FindSystem<SpaceStationConstructor>();
    }

    public void BeginInitialization()
    {
        InitializePosition();
        InitializeFrame();
        InitializeBays();
        InitializeConnectors();
    }
    
    private void InitializePosition()
    {
        Owner.transform.position = _data.position;
    }

    private void InitializeFrame()
    {
        _constructor.ConstructFrame(_data.frameData.frameType);
    }

    private void InitializeBays()
    {
        if (_data.baysData.Length == 0) return;
        foreach (var bayData in _data.baysData)
        { _constructor.ConstructBay(bayData.bayType, bayData.usedSpot); }
    }
    
    private void InitializeConnectors()
    {
        if (_data.connectorsData.Length == 0) return;
        foreach (var connectorData in _data.connectorsData)
        { _constructor.ConstructConnector(connectorData.connectorType, connectorData.usedSpot); }
    }

    public override void Deinitialize()
    {
        Owner = null;
        _data = null;
        Loader.Unload();
    }
}