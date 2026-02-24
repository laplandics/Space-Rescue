using System;
using UnityEngine;

[Serializable]
public abstract class UnitSystem
{
    [ReadOnly] public string systemName;
    
    public virtual void Initialize(Unit unit) {}
    public virtual void Deinitialize() {}
}

[Serializable]
public abstract class UnitConstructor<T> : UnitSystem where T : Unit
{
    protected T Owner;

    public override void Initialize(Unit unit)
    { if (unit is not T owner) return; Owner = owner; }

    protected GameObject GenerateObject(string objectName, (bool setTransform, Vector3 position, Quaternion rotation) transform)
    {
        var component = new GameObject(objectName);
        component.transform.SetParent(Owner.gameObject.transform, false);
        if (!transform.setTransform) return component;
        component.transform.localPosition = transform.position;
        component.transform.localRotation = transform.rotation;
        return component;
    }

    protected UnitComponent GenerateScript(string scriptTypeName, GameObject component)
    {
        var scriptType = Type.GetType(scriptTypeName);
        var script = Owner.CreateComponent(scriptType, component);
        return script;
    }
    
    protected void GenerateSprite(string spritePath, int renderOrder, GameObject component)
    {
        var sprite = Loader.Load<Sprite>(spritePath);
        var spriteRenderer = component.AddComponent<SpriteRenderer>();
        spriteRenderer.sprite = sprite;
        spriteRenderer.sortingOrder = renderOrder;
    }
}

[Serializable]
public abstract class UnitInitializer<T> : UnitSystem where T : Unit
{
    protected T Owner;

    public override void Initialize(Unit unit)
    { if (unit is not T owner) return; Owner = owner; }
}

[Serializable]
public class UnitColliderBuilder : UnitSystem
{
    private Unit _owner;
    
    public override void Initialize(Unit unit)
    { _owner = unit; var observer = unit.FindSystem<UnitObserver>(); observer.OnComponentBuilt += RebuildCollider; }

    private void RebuildCollider(UnitComponent _)
    {
        if (_owner.GetComponent<Rigidbody2D>() == null)
        { var rigidbody = _owner.gameObject.AddComponent<Rigidbody2D>(); rigidbody.bodyType = RigidbodyType2D.Static; }
        if (_owner.GetComponent<CompositeCollider2D>() == null)
        {
            var collider = _owner.gameObject.AddComponent<CompositeCollider2D>();
            collider.geometryType = CompositeCollider2D.GeometryType.Polygons;
            collider.vertexDistance = 0.05f;
            var layerName = _owner.GetConfig().unitLayer;
            _owner.gameObject.layer = LayerMask.NameToLayer(layerName);
        }
        var components = _owner.GetAllComponents();
        foreach (var component in components)
        {
            if (component.GetComponent<SpriteRenderer>() == null) continue;
            if (component.GetComponent<PolygonCollider2D>() != null) continue;
            var collider = component.gameObject.AddComponent<PolygonCollider2D>();
            collider.compositeOperation = Collider2D.CompositeOperation.Merge;
        }
    }
}

[Serializable]
public abstract class UnitInteractor : UnitSystem
{
    protected Unit Owner;
    protected GameInputs Inputs;

    public override void Initialize(Unit unit)
    { Owner = unit; Inputs = G.GetService<InputService>().GetInputs(); }
    
    public abstract void ShowUi();
    public abstract void HideUi();

    public override void Deinitialize()
    { Owner = null; Inputs = null; }
}

[Serializable]
public abstract class UnitSelector : UnitSystem
{
    private Unit _owner;
    private UnitInteractor _interactor;
    private UnitObserver _observer;

    public override void Initialize(Unit unit)
    {
        _owner = unit;
        _interactor = _owner.FindSystem<UnitInteractor>();
        _observer = _owner.FindSystem<UnitObserver>();
        if (_interactor == null) throw new Exception($"{_owner.name} has no UnitInteractor");
        if (_observer == null) throw new Exception($"{_owner.name} has no UnitObserver");
    }

    public virtual void OnHoverEnter()
    {
        _observer.OnUnitHovered?.Invoke();
    }

    public virtual void OnHoverExit()
    {
        _observer.OnUnitUnhovered?.Invoke();
    }

    public virtual void OnSelect()
    {
        _interactor.ShowUi();
        _observer.OnUnitSelected?.Invoke();
    }

    public virtual void OnDeselect()
    {
        _observer.OnUnitDeselected?.Invoke();
        _interactor.HideUi();
    }

    public override void Deinitialize()
    {
        _owner = null;
        _interactor = null;
        _observer = null;
    }
}

[Serializable]
public abstract class UnitMover : UnitSystem
{
    
}

[Serializable]
public abstract class UnitObserver : UnitSystem
{
    public Action<UnitComponent> OnComponentBuilt;
    public Action<UnitComponent> OnComponentRemoved;
    public Action OnUnitHovered;
    public Action OnUnitUnhovered;
    public Action OnUnitSelected;
    public Action OnUnitDeselected;
}