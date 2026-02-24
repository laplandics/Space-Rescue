using System;
using UnityEngine;

[Serializable]
public class SpaceStationConstructor : UnitConstructor<SpaceStation>
{
    private SpaceStationConfig _config;
    private UnitObserver _observer;

    public override void Initialize(Unit owner)
    {
        base.Initialize(owner);
        var data = Owner.GetData<SpaceStationData>();
        _config = Loader.Load<SpaceStationConfig>(data.configPath);
        _observer = Owner.FindSystem<UnitObserver>();
    }

    public void ConstructFrame(SpaceStationFrameType frameType)
    {
        var frameName = $"{frameType}_Frame";
        var frameObj = GenerateObject($"{frameType}", (false, Vector3.zero, Quaternion.identity));
        var frame = GenerateScript($"{nameof(SpaceStation)}{frameType}Frame", frameObj);
        var path = $"{nameof(SpaceStation)}/{frameName}";
        GenerateSprite(path, _config.frameRenderOrder, frameObj);
        _observer.OnComponentBuilt?.Invoke(frame);
    }

    public void ConstructBay(SpaceStationBayType bayType, SpaceStationStructureSpotType spotType)
    {
        var bayName = $"{bayType}_Bay";
        var posRotPair = _config.GetBayTr(spotType);
        var bayObj = GenerateObject($"{bayType}", (true, posRotPair.pos, posRotPair.rot));
        var bay = GenerateScript($"{nameof(SpaceStation)}{bayType}Bay", bayObj);
        var path = $"{nameof(SpaceStation)}/{bayName}";
        GenerateSprite(path, _config.baysRenderOrder, bayObj);
        _observer.OnComponentBuilt?.Invoke(bay);
    }

    public void ConstructConnector(SpaceStationConnectorType connectorType, SpaceStationStructureSpotType spotType)
    {
        var connectorName = $"{connectorType}_Connector";
        var posRotPair = _config.GetConnectorTr(spotType);
        var connectorObj = GenerateObject($"{connectorType}", (true, posRotPair.pos, posRotPair.rot));
        var connector = GenerateScript($"{nameof(SpaceStation)}{connectorType}Connector", connectorObj);
        var path = $"{nameof(SpaceStation)}/{connectorName}";
        GenerateSprite(path, _config.connectorsRenderOrder, connectorObj);
        _observer.OnComponentBuilt?.Invoke(connector);
    }

    public override void Deinitialize()
    {
        Owner = null;
        _config = null;
    }
}