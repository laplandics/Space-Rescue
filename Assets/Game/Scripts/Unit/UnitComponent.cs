using UnityEngine;

public abstract class UnitComponent : MonoBehaviour
{
    public Unit owner;

    public virtual void Initialize(Unit unit) { owner = unit; }

    public virtual void Deinitialize()
    {
        var observer = owner.FindSystem<UnitObserver>();
        observer.OnComponentRemoved?.Invoke(this);
    }
}