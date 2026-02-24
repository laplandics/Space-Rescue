using System.Collections;
using NaughtyAttributes;
using UnityEngine;

public class UnitManager : SceneManager
{
    public UnitType unitType;
    public LayerMask unitLayer;
    [ReadOnly][SerializeReference] public UnitData unitData;

    private UnitSelectionHandler _selector;
    private UnitCreationHandler _creator;
    private Coroutine _hoverCoroutine;

    public override IEnumerator Run()
    {
        _selector = new UnitSelectionHandler(unitLayer);
        _hoverCoroutine = StartCoroutine(_selector.HoverUnit());
        yield break;
    }

    public override IEnumerator End()
    {
        if (_hoverCoroutine != null) StopCoroutine(_hoverCoroutine);
        _selector.Dispose();
        _creator.Dispose();
        _selector = null;
        _creator = null;
        yield break;
    }
    
    [Button]private void PrepareData() { _creator ??= new UnitCreationHandler(); _creator.PrepareData(unitType, out unitData); }
    [Button]private void ClearData() => unitData = null;
    [Button]private void CreateUnit() { _creator ??= new UnitCreationHandler(); _creator.CreateUnit(unitType, unitData); }
}