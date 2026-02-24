using System;
using UnityEngine;

public class UnitCreationHandler : IDisposable
{
    public void PrepareData(UnitType unitType, out UnitData unitData)
    {
        unitData = null;
        if (unitType == UnitType.EmptyUnit) return;
        var dataTypeName = $"{unitType}Data";
        var dataType = Type.GetType(dataTypeName);
        if (dataType == null) return;
        unitData = (UnitData)Activator.CreateInstance(dataType);
        unitData.configPath = $"Config/{unitType}";
        unitData.unitKey = Guid.NewGuid().ToString();
    }

    public void CreateUnit(UnitType unitType, UnitData unitData)
    {
        if (unitData is null) return;
        var unitScript = Type.GetType($"{unitType}");
        var newUnit = new GameObject("Unit").AddComponent(unitScript) as Unit;
        newUnit?.Initialize(unitData);
    }

    public void Dispose() { }
}