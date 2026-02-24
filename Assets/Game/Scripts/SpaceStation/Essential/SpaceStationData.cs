using System;
using UnityEngine;

[Serializable]
public class SpaceStationData : UnitData
{
    [Space] public Vector2 position;
    public FrameData frameData;
    public BayData[] baysData;
    public ConnectorData[] connectorsData;
    
    [Serializable] public class FrameData
    { public SpaceStationFrameType frameType; }
    
    [Serializable] public class BayData
    { public SpaceStationBayType bayType; public SpaceStationStructureSpotType usedSpot; }
    
    [Serializable] public class ConnectorData
    { public SpaceStationConnectorType connectorType; public SpaceStationStructureSpotType usedSpot; }
}

public enum SpaceStationStructureType { Frame, Bay, Connector }

public enum SpaceStationFrameType
{ Command, Mining, Extraction, Production, Ward, Energy, Logistic, Science, Yard, Resident }

public enum SpaceStationBayType
{ Locator }

public enum SpaceStationStructureSpotType
{ Center, Up, Down, Left, Right, LeftUp, LeftDown, RightUp, RightDown }

public enum SpaceStationConnectorType
{ Gun }