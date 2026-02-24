using System;
using UnityEngine;

[CreateAssetMenu(fileName = "SpaceStation", menuName = "Config/SpaceStation")]
public class SpaceStationConfig : UnitConfig
{
    [Serializable] public class BayTr
    { public SpaceStationStructureSpotType spotType; public Vector3 position; public Vector3 rotation; }
    [Serializable] public class ConnectorTr
    { public SpaceStationStructureSpotType spotType; public Vector3 position; public Vector3 rotation; }
    
    [Header("Hierarchy settings")]
    public int frameRenderOrder;
    public int baysRenderOrder;
    public int connectorsRenderOrder;
    
    public BayTr[] baysTr;
    public ConnectorTr[] connectorsTr;
    
    [Serializable] public class FrameSettings
    { public SpaceStationFrameType frameType; public float hp; }
    [Serializable] public class BaySettings
    { public SpaceStationBayType bayType; public float hp; }
    [Serializable] public class ConnectorSettings
    { public SpaceStationConnectorType connectorType; }
    
    [Header("Station structure settings")]
    public FrameSettings[] frameSettings;
    public BaySettings[] baySettings;
    public ConnectorSettings[] connectorSettings;
    
    public (Vector3 pos, Quaternion rot) GetBayTr(SpaceStationStructureSpotType spotType)
    {
        foreach (var bayTr in baysTr)
        { if(bayTr.spotType == spotType) return (bayTr.position, Quaternion.Euler(bayTr.rotation)); }
        return (Vector3.zero, Quaternion.identity);
    }
    
    public (Vector3 pos, Quaternion rot) GetConnectorTr(SpaceStationStructureSpotType spotType)
    {
        foreach (var connectorTr in connectorsTr)
        { if(connectorTr.spotType == spotType) return (connectorTr.position, Quaternion.Euler(connectorTr.rotation)); }
        return (Vector3.zero, Quaternion.identity);
    }
}