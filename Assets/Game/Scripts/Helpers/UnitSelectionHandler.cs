using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class UnitSelectionHandler : IDisposable
{
    private readonly LayerMask _unitLayer;
    private Camera _camera;
    private UnitSelector _currentHover;
    private UnitSelector _currentSelected;
    private GameInputs _inputs;
    
    public UnitSelectionHandler(LayerMask unitLayer)
    {
        _unitLayer = unitLayer;
        _camera = Camera.main;
        _inputs = G.GetService<InputService>().GetInputs();
        _inputs.Units.Enable();
        _inputs.Units.Select.performed += SelectUnit;
    }
    
    public IEnumerator HoverUnit()
    {
        while (true)
        {
            var mousePos = _camera.ScreenToWorldPoint(Mouse.current.position.ReadValue());
            var hoverCollider = Physics2D.OverlapPoint(mousePos, _unitLayer);
            Unit newUnit = null;
            UnitSelector newSelector = null;
            if (hoverCollider != null) newUnit = hoverCollider.GetComponent<Unit>();
            if (newUnit != null) newSelector = newUnit.FindSystem<UnitSelector>();
            if (_currentHover != newSelector)
            { _currentHover?.OnHoverExit(); _currentHover = newSelector; _currentHover?.OnHoverEnter(); }
            yield return null;
        }
    }
    
    private void SelectUnit(InputAction.CallbackContext ctx)
    {
        if (_currentHover == _currentSelected) return;
        _currentSelected?.OnDeselect();
        _currentSelected = _currentHover;
        _currentSelected?.OnSelect();
    }

    public void Dispose()
    {
        _inputs.Units.Select.performed -= SelectUnit;
        _camera = null;
        _inputs = null;
    }
}